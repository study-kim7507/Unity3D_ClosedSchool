using UnityEngine;
using System.Collections.Generic;

public class BookZone : MonoBehaviour
{
    [Header("퍼즐 설정")]
    [SerializeField] private Transform[] bookSlots; // 책을 놓을 슬롯 (4개)
    [SerializeField] private List<string> requiredBooks = new List<string>(); // 인스펙터에서 설정 가능

    [Header("퍼즐 클리어 사운드")]
    [SerializeField] private AudioSource puzzleClearAudio; // 퍼즐 클리어 사운드

    [Header("잘못된 책 튕김 효과")]
    [SerializeField] private float ejectForce = 5f; // 잘못된 책 튕겨 나가는 힘

    [Header("퍼즐 완료 보상")]
    [SerializeField] private GameObject rewardPrefab; // 보상 아이템 프리팹
    [SerializeField] private Transform rewardSpawnPoint; // 보상 생성 위치

    private int currentBookCount = 0;
    private bool puzzleCompleted = false;
    private List<Book> placedBooks = new List<Book>(); // 배치된 책 목록

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Book")) // 책이 북존 안에 들어왔을 때
        {
            Book book = other.GetComponent<Book>();
            if (book != null)
            {
                if (IsCorrectBook(book.GetBookName())) // 올바른 책인지 확인
                {
                    if (!placedBooks.Contains(book) && currentBookCount < bookSlots.Length)
                    {
                        Destroy(book.GetComponent<Draggable>());
                        PlaceBookInSlot(book);
                        currentBookCount++;

                        Debug.Log($"책 '{book.GetBookName()}'이 배치됨 ({currentBookCount}/{requiredBooks.Count})");

                        CheckPuzzleCompletion(); // 퍼즐 클리어 확인
                    }
                }
                else
                {
                    Destroy(book.GetComponent<Draggable>());
                    Debug.Log($"잘못된 책 '{book.GetBookName()}'! 튕겨 나갑니다.");
                    EjectBook(other.gameObject); // 잘못된 책 튕겨 나가게 처리
                }
            }
        }
    }

    private bool IsCorrectBook(string bookName)
    {
        return requiredBooks.Contains(bookName);
    }

    private void EjectBook(GameObject book)
    {
        Rigidbody rb = book.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false;
            rb.useGravity = true;
            Vector3 ejectDirection = (book.transform.position - transform.position).normalized;
            rb.AddForce(ejectDirection * ejectForce, ForceMode.Impulse); // 튕겨 나가는 힘 추가
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
        if (!puzzleCompleted && currentBookCount == requiredBooks.Count)
        {
            puzzleCompleted = true;
            Debug.Log("퍼즐이 완료되었습니다!");

            if (puzzleClearAudio != null)
            {
                puzzleClearAudio.Play();
            }

            SpawnReward(); // 보상 아이템 생성
        }
    }

    private void SpawnReward()
    {
        if (rewardPrefab != null && rewardSpawnPoint != null)
        {
            Instantiate(rewardPrefab, rewardSpawnPoint.position, Quaternion.identity);
            Debug.Log("보상이 생성되었습니다.");
        }
        else
        {
            Debug.LogWarning("보상 아이템 프리팹 또는 생성 위치가 설정되지 않았습니다!");
        }
    }
}
