using UnityEngine;

public class Book : MonoBehaviour
{
    [SerializeField] private string bookName; // √• ¿Ã∏ß

    public string GetBookName()
    {
        return bookName;
    }
}
