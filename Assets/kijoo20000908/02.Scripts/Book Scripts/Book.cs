using UnityEngine;

public class Book : MonoBehaviour
{
    [SerializeField] private string bookName; // å ����

    public string GetBookName() // BookZone���� ȣ���� å �̸� ��ȯ
    {
        return bookName;
    }
}
