using UnityEngine;

public class PuzzleManager2 : MonoBehaviour
{
    public PuzzleSlot[] slots; // 퍼즐 슬롯 배열

    [SerializeField] private GameObject rewardItemPrefab;  // 보상 아이템(열쇠 또는 단서)
    [SerializeField] private Transform itemSpawnPoint;    // 아이템 생성 위치

    private bool isPuzzleSolved = false;

    private void Update()
    {
        if (!isPuzzleSolved && CheckPuzzleSolved())
        {
            isPuzzleSolved = true;
            OnPuzzleSolved();
        }
    }

    // 퍼즐이 풀렸는지 확인
    private bool CheckPuzzleSolved()
    {
        foreach (PuzzleSlot slot in slots)
        {
            if (!slot.IsCorrect())
                return false;
        }
        return true;
    }

    // 퍼즐 성공 시 실행할 동작
    private void OnPuzzleSolved()
    {
        Debug.Log("Puzzle Solved! (Managed by PuzzleManager2)");
        RewardPlayer(); // 보상 지급
    }

    // 보상 지급
    private void RewardPlayer()
    {
        if (rewardItemPrefab != null && itemSpawnPoint != null)
        {
            Instantiate(rewardItemPrefab, itemSpawnPoint.position, Quaternion.identity);
            Debug.Log("아이템을 획득했습니다!");
        }
        else
        {
            Debug.LogWarning("보상 아이템 프리팹 또는 스폰 위치가 설정되지 않았습니다!");
        }
    }
}
