using UnityEngine;
using TMPro;

public class Book : MonoBehaviour
{
    [SerializeField] private string bookName; // 책 제목

    private void Start()
    {

    }

    public string GetBookName() // 북존에서 호출할 책 이름 반환
    {
        return bookName;
    }
}
