using UnityEngine;

public class PuzzleManager3 : MonoBehaviour
{
    public GameObject rewardItemPrefab; // 보상 아이템 (예: 열쇠)
    public Transform rewardSpawnPoint; // 보상 생성 위치
    private bool isPuzzleSolved = false;

    public void CompletePuzzle()
    {
        if (!isPuzzleSolved)
        {
            isPuzzleSolved = true;
            Debug.Log("퍼즐이 완료되었습니다! 보상이 생성됩니다.");

            // 보상 아이템 생성
            if (rewardItemPrefab != null && rewardSpawnPoint != null)
            {
                Instantiate(rewardItemPrefab, rewardSpawnPoint.position, Quaternion.identity);
                Debug.Log("보상이 생성되었습니다!");
            }
            else
            {
                Debug.LogWarning("보상 아이템 또는 위치가 설정되지 않았습니다!");
            }
        }
    }
}
