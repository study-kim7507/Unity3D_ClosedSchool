using UnityEngine;

public interface IInteractable
{
    void BeginFocus(GameObject withItem = null);          
    void EndFocus(GameObject withItem = null);            
    void Interact(GameObject withItem = null);                        // ��ȣ�ۿ� ����
}
