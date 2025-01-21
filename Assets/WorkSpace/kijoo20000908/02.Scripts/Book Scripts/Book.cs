using UnityEngine;

public class Book : MonoBehaviour, IInteractable
{
    [SerializeField] private string bookName;  // 책 이름
    [SerializeField] private int bookOrder;    // 책의 순서 (정답 순서)

    private bool isPlacedCorrectly = false;

    public string GetBookName() => bookName;
    public int GetBookOrder() => bookOrder;

    private PuzzleManager puzzleManager;

    private void Start()
    {
        puzzleManager = FindObjectOfType<PuzzleManager>();
    }

    public void BeginFocus()
    {
        Debug.Log($"{bookName}에 초점이 맞춰졌습니다."); // 아웃라인 활성화 로직 추가 가능
    }

    public void EndFocus()
    {
        Debug.Log($"{bookName}에서 초점이 해제되었습니다."); // 아웃라인 비활성화 로직 추가 가능
    }

    public void BeginInteract()
    {
        Debug.Log($"{bookName}을 집었습니다.");
    }

    public void EndInteract()
    {
        Debug.Log($"{bookName}을 놓았습니다.");
    }

    public void Interact()
    {
        puzzleManager.SelectBook(this);  // 퍼즐 매니저에 책 선택
    }

    public bool IsPlacedCorrectly()
    {
        return isPlacedCorrectly;
    }

    public void SetPlacedCorrectly(bool value)
    {
        isPlacedCorrectly = value;
    }
}
