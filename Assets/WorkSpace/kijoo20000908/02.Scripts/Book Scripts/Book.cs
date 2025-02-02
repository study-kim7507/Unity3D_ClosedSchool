using UnityEngine;
using TMPro;

public class Book : MonoBehaviour
{
    [SerializeField] private string bookName; // 책 제목
    [SerializeField] public GameObject nameUI; // 책 제목 UI (TextMeshPro)

    private void Start()
    {
        if (nameUI != null)
        {
            nameUI.SetActive(false); // 처음엔 숨김
            TextMeshPro textComponent = nameUI.GetComponent<TextMeshPro>();
            if (textComponent != null)
            {
                textComponent.text = bookName; // 책 제목 설정
            }
        }
        else
        {
            Debug.LogError($"{gameObject.name}'의 nameUI가 설정되지 않았습니다!");
        }
    }

    public string GetBookName() // 북존에서 호출할 책 이름 반환
    {
        return bookName;
    }

    public void ShowTitle(bool show)
    {
        if (nameUI != null)
        {
            nameUI.SetActive(show);
        }
    }
}
