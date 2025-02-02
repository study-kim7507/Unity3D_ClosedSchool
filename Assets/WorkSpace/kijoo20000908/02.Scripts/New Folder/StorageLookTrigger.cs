using UnityEngine;

public class StorageLookTrigger : MonoBehaviour
{
    public BallStorage ballStorage; // BallStorage ����

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // �÷��̾ ������ ������ ����
        {
            ballStorage.StartLookingAtStorage();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // �÷��̾ �־�����
        {
            ballStorage.StopLookingAtStorage();
        }
    }
}
