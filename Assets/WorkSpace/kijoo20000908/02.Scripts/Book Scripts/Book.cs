using UnityEngine;
using TMPro;

public class Book : MonoBehaviour
{
    [SerializeField] private string bookName; // å ����

    private void Start()
    {

    }

    public string GetBookName() // �������� ȣ���� å �̸� ��ȯ
    {
        return bookName;
    }
}
