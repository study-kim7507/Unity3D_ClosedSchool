using UnityEngine;

public class PuzzleManager3 : MonoBehaviour
{
    public GameObject rewardItemPrefab; // 보상 아이템 (예: 열쇠)
    public Transform rewardSpawnPoint; // 보상 아이템 생성 위치
    public GameObject goalEffect; // 골 성공 시 표시할 효과 (예: 파티클)

    private bool goal1Completed = false;
    private bool goal2Completed = false;
    private bool isPuzzleSolved = false;

    // 첫 번째 골대에 공이 들어갔을 때 호출
    public void Goal1Scored()
    {
        if (!goal1Completed)
        {
            goal1Completed = true;
            Debug.Log("첫 번째 골대에 공이 들어갔습니다!");
            CheckPuzzleCompletion();
        }
    }

    // 두 번째 골대에 공이 들어갔을 때 호출
    public void Goal2Scored()
    {
        if (!goal2Completed)
        {
            goal2Completed = true;
            Debug.Log("두 번째 골대에 공이 들어갔습니다!");
            CheckPuzzleCompletion();
        }
    }

    // 두 골대에 공이 들어갔는지 체크하여 퍼즐 완료
    private void CheckPuzzleCompletion()
    {
        if (goal1Completed && goal2Completed && !isPuzzleSolved)
        {
            isPuzzleSolved = true;
            Debug.Log("퍼즐이 풀렸습니다! (양쪽 골대에 공이 들어감)");

            // 골 성공 효과 실행
            if (goalEffect != null)
            {
                Instantiate(goalEffect, transform.position, Quaternion.identity);
            }

            // 보상 생성
            SpawnReward();
        }
    }

    // 보상을 특정 위치에 생성
    private void SpawnReward()
    {
        if (rewardItemPrefab != null && rewardSpawnPoint != null)
        {
            Instantiate(rewardItemPrefab, rewardSpawnPoint.position, Quaternion.identity);
            Debug.Log("보상이 생성되었습니다!");
        }
        else
        {
            Debug.LogWarning("보상 아이템 프리팹 또는 스폰 위치가 설정되지 않았습니다!");
        }
    }
}
