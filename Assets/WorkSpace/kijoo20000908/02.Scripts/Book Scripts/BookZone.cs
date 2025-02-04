using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class BookZone : MonoBehaviour
{
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

        if (puzzleClearAudio == null)
        {
            Debug.LogError("퍼즐 클리어 사운드가 설정되지 않았습니다!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {

    }

    private void OnTriggerExit(Collider other)
    {

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
