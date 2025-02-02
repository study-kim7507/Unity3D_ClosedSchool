using UnityEngine;
using TMPro;

public class Book : MonoBehaviour
{
    [SerializeField] private string bookName; // å ����
    [SerializeField] public GameObject nameUI; // å ���� UI (TextMeshPro)

    private void Start()
    {
        if (nameUI != null)
        {
            nameUI.SetActive(false); // ó���� ����
            TextMeshPro textComponent = nameUI.GetComponent<TextMeshPro>();
            if (textComponent != null)
            {
                textComponent.text = bookName; // å ���� ����
            }
        }
        else
        {
            Debug.LogError($"{gameObject.name}'�� nameUI�� �������� �ʾҽ��ϴ�!");
        }
    }

    public string GetBookName() // �������� ȣ���� å �̸� ��ȯ
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
