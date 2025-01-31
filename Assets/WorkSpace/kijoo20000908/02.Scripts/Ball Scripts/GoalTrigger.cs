using UnityEngine;

public class GoalTrigger : MonoBehaviour
{
    public PuzzleManager3 puzzleManager3;
    public int goalID; // 1: 첫 번째 골대, 2: 두 번째 골대

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Basketball")) // 공의 태그 확인
        {
            Debug.Log($"농구공이 골대 {goalID}에 들어갔습니다!");

            if (puzzleManager3 != null)
            {
                if (goalID == 1)
                    puzzleManager3.Goal1Scored();
                else if (goalID == 2)
                    puzzleManager3.Goal2Scored();
            }
        }
    }
}
