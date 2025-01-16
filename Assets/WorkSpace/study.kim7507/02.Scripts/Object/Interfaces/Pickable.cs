using UnityEngine;
using UnityEngine.UI;

// TODO: 컴포넌트로 변경
public interface IPickable
{
    string ItemName { get; set; }
    string ItemDescription { get; set; }
    Sprite ItemImage { get; set; }
    GameObject ItemObjectPrefab { get;  set; }    
}
