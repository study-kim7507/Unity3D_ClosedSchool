using UnityEngine;

public class Book : MonoBehaviour
{
    [SerializeField] private string bookName; // å �̸�

    public string GetBookName()
    {
        return bookName;
    }
}
