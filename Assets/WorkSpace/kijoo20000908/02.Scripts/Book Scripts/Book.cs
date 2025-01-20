using UnityEngine;

public class Book : MonoBehaviour, IInteractable
{
    [SerializeField] private string bookName;  // å �̸�
    [SerializeField] private int bookOrder;    // å�� ���� (���� ����)

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
        Debug.Log($"{bookName}�� ������ ���������ϴ�."); // �ƿ����� Ȱ��ȭ ���� �߰� ����
    }

    public void EndFocus()
    {
        Debug.Log($"{bookName}���� ������ �����Ǿ����ϴ�."); // �ƿ����� ��Ȱ��ȭ ���� �߰� ����
    }

    public void BeginInteract()
    {
        Debug.Log($"{bookName}�� �������ϴ�.");
    }

    public void EndInteract()
    {
        Debug.Log($"{bookName}�� ���ҽ��ϴ�.");
    }

    public void Interact()
    {
        puzzleManager.SelectBook(this);  // ���� �Ŵ����� å ����
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
