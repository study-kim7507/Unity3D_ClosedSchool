using UnityEngine;
using UnityEngine.UI;

public class FolderB : MonoBehaviour, IPickable
{
    [SerializeField] private string name;              
    [SerializeField] private string description;
    [SerializeField] private Sprite image;
    [SerializeField] private GameObject objectPrefab;   // �����տ��� ���� �߰����� ����, �ν��Ͻ����� �߰��ϵ���

    // IPickable �������̽� ����
    public string ItemName
    {
        get => name;
        set => name = value;
    }

    public string ItemDescription
    {
        get => description;
        set => description = value;
    }

    public Sprite ItemImage
    {
        get => image;
        set => image = value;
    }

    public GameObject ItemObjectPrefab
    {
        get => objectPrefab;
        set => objectPrefab = value;
    }
}
