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
        PlayerUI.instance.DisplayInteractionDescription("페인트를 붓자, 퇴마진이 그려지기 시작했다.");
    }
}
