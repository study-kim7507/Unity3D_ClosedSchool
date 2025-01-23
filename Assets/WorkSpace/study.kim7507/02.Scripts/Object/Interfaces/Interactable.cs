using UnityEngine;

public interface IInteractable
{
    void BeginFocus();          // Outline 활성화 및 플레이어 캔버스의 텍스트 변화
    void EndFocus();            // Outline 비활성화 및 플레이어 캔버스의 텍스트 변화
    void BeginInteract();
    void EndInteract();
    void Interact();                        // 상호작용 로직
}
