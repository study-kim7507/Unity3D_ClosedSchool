using UnityEngine;

public class BallStorage : MonoBehaviour
{
    public int totalBallsNeeded = 3; // �ʿ��� �󱸰� ����
    private int currentBallCount = 0;
    public PuzzleManager2 puzzleManager2; // ���� �Ϸ� üũ

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Basketball"))
        {
            currentBallCount++;
            Debug.Log($"�󱸰� {currentBallCount}/{totalBallsNeeded} ���� �����Կ� �������ϴ�.");

            Destroy(other.gameObject); // ���� �����Կ� ������ ����

            if (currentBallCount >= totalBallsNeeded)
            {
                puzzleManager2.CompletePuzzle(); // ���� �Ϸ�
            }
        }
    }
}
