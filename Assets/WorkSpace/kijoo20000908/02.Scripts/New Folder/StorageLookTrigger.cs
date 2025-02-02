using UnityEngine;

public class StorageLookTrigger : MonoBehaviour
{
    public BallStorage ballStorage; // BallStorage 연결

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // 플레이어가 보관함 가까이 오면
        {
            ballStorage.StartLookingAtStorage();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // 플레이어가 멀어지면
        {
            ballStorage.StopLookingAtStorage();
        }
    }
}
