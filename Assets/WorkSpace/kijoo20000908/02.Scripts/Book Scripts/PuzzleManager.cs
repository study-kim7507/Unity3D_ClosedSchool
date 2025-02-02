using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    [SerializeField] private GameObject rewardPrefab; // 미션 클리어 보상 프리팹
    [SerializeField] private Transform rewardSpawnPoint; // 보상을 생성할 위치

    public void CheckPuzzleCompletion(int currentBookCount, int requiredBookCount)
    {
        if (currentBookCount >= requiredBookCount) // 4개의 책이 모두 배치되었는지 체크
        {
            CompletePuzzle();
        }
        else
        {
            Debug.Log($"아직 책이 부족합니다. ({currentBookCount}/{requiredBookCount})");
        }
    }

    private void CompletePuzzle()
    {
        Debug.Log("퍼즐이 완료되었습니다! 모든 책이 배치되었습니다.");
        SpawnReward();
    }

    private void SpawnReward()
    {
        if (rewardPrefab != null && rewardSpawnPoint != null)
        {
            Instantiate(rewardPrefab, rewardSpawnPoint.position, Quaternion.identity);
            Debug.Log("보상이 생성되었습니다.");
        }
    }
}
