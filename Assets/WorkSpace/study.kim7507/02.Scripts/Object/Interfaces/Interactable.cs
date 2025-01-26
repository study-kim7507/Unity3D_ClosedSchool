using UnityEngine;

public interface IInteractable
{
    void BeginFocus(GameObject withItem = null);          // Outline Ȱ��ȭ �� �÷��̾� ĵ������ �ؽ�Ʈ ��ȭ
    void EndFocus(GameObject withItem = null);            // Outline ��Ȱ��ȭ �� �÷��̾� ĵ������ �ؽ�Ʈ ��ȭ
    void BeginInteract(GameObject withItem = null);
    void EndInteract(GameObject withItem = null);
    void Interact(GameObject withItem = null);                        // ��ȣ�ۿ� ����
}
