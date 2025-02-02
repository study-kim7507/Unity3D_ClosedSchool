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
        "📖 고대 마법서",
        "📖 사라진 역사",
        "📖 금단의 지식",
        "📖 빛과 어둠의 균형"
    };

    private int currentBookCount = 0;
    private List<Book> placedBooks = new List<Book>(); // 배치된 책 목록
    private PuzzleManager puzzleManager;

    private void Start()
    {
        puzzleManager = FindObjectOfType<PuzzleManager>();

        // UI가 연결되지 않았을 경우 오류 방지
        if (bookListPanel == null)
        {
            Debug.LogError("❌ BookListPanel이 연결되지 않았습니다!");
        }
        else
        {
            bookListPanel.SetActive(false); // 시작 시 UI 숨김
        }

        if (bookListText == null)
        {
            Debug.LogError("❌ BookListText가 연결되지 않았습니다!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // 플레이어가 근처로 가면
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
            Debug.Log("책 목록이 표시되었습니다.");
        }
    }

    private void HideBookList()
    {
        if (bookListPanel != null)
        {
            bookListPanel.SetActive(false);
            Debug.Log(" 책 목록이 사라졌습니다.");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Book")) // 책이 들어왔을 때 배치
        {
            Book book = other.GetComponent<Book>();
            if (book != null && !placedBooks.Contains(book))
            {
                if (currentBookCount < bookSlots.Length)
                {
                    PlaceBookInSlot(book);
                    currentBookCount++;

                    Debug.Log($"📖 책 {book.GetBookName()}이 배치됨 ({currentBookCount}/{requiredBooks.Length})");

                    puzzleManager.CheckPuzzleCompletion(currentBookCount, requiredBooks.Length);
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

            // 책 고정 (물리 영향 제거)
            Rigidbody rb = book.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true;
                rb.useGravity = false;
            }

            // 책 충돌 박스 비활성화 (밀리지 않도록)
            Collider bookCollider = book.GetComponent<Collider>();
            if (bookCollider != null)
            {
                bookCollider.enabled = false;
            }

            placedBooks.Add(book);
        }
    }
}