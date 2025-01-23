using UnityEngine;

public interface IInteractable
{
    void BeginFocus();          // Outline Ȱ��ȭ �� �÷��̾� ĵ������ �ؽ�Ʈ ��ȭ
    void EndFocus();            // Outline ��Ȱ��ȭ �� �÷��̾� ĵ������ �ؽ�Ʈ ��ȭ
    void BeginInteract();
    void EndInteract();
    void Interact();                        // ��ȣ�ۿ� ����
}
