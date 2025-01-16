using UnityEngine;
using UnityEngine.UI;

public class Photo : MonoBehaviour, IPickable
{
    private string itemName;
    private string itemDescription;
    [SerializeField] private Sprite itemImage;
    [SerializeField] private GameObject itemObjectPrefab;
    [SerializeField] private RawImage capturedImage;

    public string ItemName { get => itemName ; set => itemName = value; }
    public string ItemDescription { get => itemDescription; set => itemDescription = value; }
    public Sprite ItemImage { get => itemImage; set => itemImage = value; }
    public GameObject ItemObjectPrefab { get => itemObjectPrefab; set => itemObjectPrefab = value; }
    public RawImage CapturedImage { get => capturedImage; }

    public void SetCapturedImageUsingTexture2D(Texture2D image)
    {
        capturedImage.texture = image;
    }
}
