using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    [SerializeField] private GameObject rewardPrefab; // �̼� Ŭ���� ���� ������
    [SerializeField] private Transform rewardSpawnPoint; // ������ ������ ��ġ

    public void CheckPuzzleCompletion(int currentBookCount, int requiredBookCount)
    {
        if (currentBookCount >= requiredBookCount) // 4���� å�� ��� ��ġ�Ǿ����� üũ
        {
            CompletePuzzle();
        }
        else
        {
            Debug.Log($"���� å�� �����մϴ�. ({currentBookCount}/{requiredBookCount})");
        }
    }

    private void CompletePuzzle()
    {
        Debug.Log("������ �Ϸ�Ǿ����ϴ�! ��� å�� ��ġ�Ǿ����ϴ�.");
        SpawnReward();
    }

    private void SpawnReward()
    {
        if (rewardPrefab != null && rewardSpawnPoint != null)
        {
            Instantiate(rewardPrefab, rewardSpawnPoint.position, Quaternion.identity);
            Debug.Log("������ �����Ǿ����ϴ�.");
        }
    }
}
