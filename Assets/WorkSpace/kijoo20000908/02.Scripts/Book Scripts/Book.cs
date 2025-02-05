using UnityEngine;

public class Book : MonoBehaviour
{
    [SerializeField] private string bookName; // 책 제목

    public string GetBookName() // BookZone에서 호출할 책 이름 반환
    {
        return bookName;
    }
}
