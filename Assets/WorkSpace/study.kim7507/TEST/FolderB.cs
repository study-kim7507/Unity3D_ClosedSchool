using UnityEngine;
using UnityEngine.UI;

public class FolderB : MonoBehaviour, IPickable
{
    [SerializeField] private string name;              
    [SerializeField] private string description;
    [SerializeField] private Sprite image;
    [SerializeField] private GameObject objectPrefab;   // 프리팹에서 직접 추가하지 말고, 인스턴스에서 추가하도록

    // IPickable 인터페이스 구현
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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
