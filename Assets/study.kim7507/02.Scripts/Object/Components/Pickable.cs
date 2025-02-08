using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Collider))]
public class Pickable : MonoBehaviour
{
    public string itemName;
    public string itemDescription;
    public Sprite itemImage;
    public string PrefabName;
    [HideInInspector] public GameObject itemObjectPrefab;

    private void Start()
    {
        itemObjectPrefab = PrefabManager.Instance.GetOriginalPrefab(PrefabName);
    }
}