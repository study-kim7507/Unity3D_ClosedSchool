using UnityEngine;

public class PaintBucket : MonoBehaviour, IInteractable
{
    public void BeginFocus(GameObject withItem = null)
    {

    }

    public void EndFocus(GameObject withItem = null)
    {

    }

    public void Interact(GameObject withItem = null)
    {
        
    }

    public void Draw()
    {
        PlayerUI.instance.DisplayInteractionDescription("����Ʈ�� ����, ������ �׷����� �����ߴ�.");
    }
}
