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

    public void BeginFocus(GameObject withItem = null)
    {
        Debug.Log($"{bookName}�� ������ ���������ϴ�."); // �ƿ����� Ȱ��ȭ ���� �߰� ����
    }

    public void EndFocus(GameObject withItem = null)
    {
        Debug.Log($"{bookName}���� ������ �����Ǿ����ϴ�."); // �ƿ����� ��Ȱ��ȭ ���� �߰� ����
    }

    public void BeginInteract(GameObject withItem = null)
    {
        Debug.Log($"{bookName}�� �������ϴ�.");
    }

    public void EndInteract(GameObject withItem = null)
    {
        Debug.Log($"{bookName}�� ���ҽ��ϴ�.");
    }

    public void Interact(GameObject withItem = null)
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
