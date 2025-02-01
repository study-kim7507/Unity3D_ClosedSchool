using UnityEngine;

public class Book : MonoBehaviour
{
    [SerializeField] private string bookName;

    public string GetBookName()
    {
        return bookName;
    }
}
