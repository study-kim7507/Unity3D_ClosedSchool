using UnityEngine;
using UnityEngine.UI;

public interface IPickable
{
    string ItemName { get; set; }
    string ItemDescription { get; set; }
    Sprite ItemImage { get; set; }
    GameObject ItemObjectPrefab { get;  set; }    
}
