using UnityEngine;
using System.Collections.Generic;

public class BookZone : MonoBehaviour
{
    [SerializeField] private int requiredBookCount = 4; // 필요한 책 개수
    private int currentBookCount = 0;
    private PuzzleManager puzzleManager;

    private void Start()
    {
        puzzleManager = FindObjectOfType<PuzzleManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Book"))
        {
            Book book = other.GetComponent<Book>();
            if (book != null)
            {
                currentBookCount++;
                Debug.Log($"책 {book.GetBookName()}이 놓여짐 ({currentBookCount}/{requiredBookCount})");

                // 책 없애기 (게임 오브젝트 삭제)
                Destroy(other.gameObject);

                // 퍼즐 상태 체크
                puzzleManager.CheckPuzzleCompletion(currentBookCount, requiredBookCount);
            }
        }
    }
}
