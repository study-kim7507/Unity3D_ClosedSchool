using UnityEngine;

public class Drawer : MonoBehaviour, IInteractable
{ 
    private bool isOpen; // ���ȴ��� ����

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
        
        if (!isOpen) animator.SetTrigger("Open");   // �������� ���� ���, ��������
        else animator.SetTrigger("Close");          // �����ִ� ���, ��������

        isOpen = !isOpen;                           // ���°� ����
    }
}
