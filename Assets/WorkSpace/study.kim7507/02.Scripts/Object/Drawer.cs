using UnityEngine;

public class Drawer : MonoBehaviour, IInteractable
{ 
    private bool isOpen; // 열렸는지 여부

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void BeginFocus()
    {
        throw new System.NotImplementedException();
    }

    public void BeginInteract()
    {
        throw new System.NotImplementedException();
    }

    public void EndFocus()
    {
        throw new System.NotImplementedException();
    }

    public void EndInteract()
    {
        throw new System.NotImplementedException();
    }

    public void Interact()
    {
        
        if (!isOpen) animator.SetTrigger("Open");   // 열려있지 않은 경우, 열리도록
        else animator.SetTrigger("Close");          // 열려있는 경우, 닫히도록

        isOpen = !isOpen;                           // 상태값 반전
    }
}
