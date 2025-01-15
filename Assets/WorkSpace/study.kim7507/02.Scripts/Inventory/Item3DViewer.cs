using UnityEngine;

public class Item3DViewer : MonoBehaviour
{
    public GameObject item3DViewerCanvas;
    public Transform itemPos;

    bool isOpen = false;

    private void Start()
    {
        item3DViewerCanvas.SetActive(isOpen);
    }

    public void OpenItem3DViewer(GameObject item)
    {
        if (!isOpen) isOpen = true;
        item3DViewerCanvas.SetActive(isOpen);

        GameObject go = Instantiate(item, itemPos);
        go.layer = LayerMask.NameToLayer("UI");

        go.AddComponent<Rotation>();
        go.transform.localScale *= 1000.0f;
    }
}
