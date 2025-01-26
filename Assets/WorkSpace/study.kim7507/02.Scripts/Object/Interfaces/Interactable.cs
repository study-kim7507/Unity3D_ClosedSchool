using UnityEngine;

public interface IInteractable
{
    void BeginFocus(GameObject withItem = null);          // Outline 활성화 및 플레이어 캔버스의 텍스트 변화
    void EndFocus(GameObject withItem = null);            // Outline 비활성화 및 플레이어 캔버스의 텍스트 변화
    void BeginInteract(GameObject withItem = null);
    void EndInteract(GameObject withItem = null);
    void Interact(GameObject withItem = null);                        // 상호작용 로직
}
