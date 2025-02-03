using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class BookZone : MonoBehaviour
{
    [Header("UI 설정")]
    [SerializeField] private GameObject bookListPanel; // 책 목록 UI 패널
    [SerializeField] private TMP_Text bookListText; // 책 목록 텍스트

    [Header("퍼즐 설정")]
    [SerializeField] private Transform[] bookSlots; // 책을 놓을 슬롯 (4개)
    [SerializeField]
    private string[] requiredBooks =
    {
        "고대 마법서",
        "사라진 역사",
        "금단의 지식",
        "빛과 어둠의 균형"
    };

    [Header("퍼즐 클리어 사운드")]
    [SerializeField] private AudioSource puzzleClearAudio; // 퍼즐 클리어 사운드

    private int currentBookCount = 0;
    private bool puzzleCompleted = false;
    private List<Book> placedBooks = new List<Book>(); // 배치된 책 목록
    private PuzzleManager puzzleManager;

    private void Start()
    {
        puzzleManager = FindObjectOfType<PuzzleManager>();

        if (bookListPanel != null)
        {
            bookListPanel.SetActive(false); // UI 처음엔 숨김
        }

        if (puzzleClearAudio == null)
        {
            Debug.LogError("퍼즐 클리어 사운드가 설정되지 않았습니다!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // 플레이어가 북존 근처로 가면
        {
            ShowBookList();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // 플레이어가 멀어지면 책 목록 숨김
        {
            HideBookList();
        }
    }

    private void ShowBookList()
    {
        if (bookListPanel != null && bookListText != null)
        {
            bookListText.text = "찾아야 할 책 목록\n";
            foreach (string book in requiredBooks)
            {
                bookListText.text += book + "\n";
            }
            bookListPanel.SetActive(true);
        }
    }

    private void HideBookList()
    {
        if (bookListPanel != null)
        {
            bookListPanel.SetActive(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Book")) // 책이 북존 안에 들어왔을 때
        {
            Book book = other.GetComponent<Book>();
            if (book != null && !placedBooks.Contains(book))
            {
                if (currentBookCount < bookSlots.Length)
                {
                    PlaceBookInSlot(book);
                    currentBookCount++;

                    Debug.Log($"책 '{book.GetBookName()}'이 배치됨 ({currentBookCount}/{requiredBooks.Length})");

                    CheckPuzzleCompletion(); // 퍼즐 클리어 확인
                }
            }
        }
    }

    private void PlaceBookInSlot(Book book)
    {
        int slotIndex = placedBooks.Count;
        if (slotIndex < bookSlots.Length)
        {
            book.transform.position = bookSlots[slotIndex].position;
            book.transform.rotation = bookSlots[slotIndex].rotation;

            Rigidbody rb = book.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true;
                rb.useGravity = false;
            }

            Collider bookCollider = book.GetComponent<Collider>();
            if (bookCollider != null)
            {
                bookCollider.enabled = false;
            }

            placedBooks.Add(book);
        }
    }

    private void CheckPuzzleCompletion()
    {
        if (!puzzleCompleted && currentBookCount == requiredBooks.Length)
        {
            puzzleCompleted = true;
            Debug.Log("퍼즐이 완료되었습니다.");
            if (puzzleClearAudio != null)
            {
                puzzleClearAudio.Play();
            }
        }
    }
}
