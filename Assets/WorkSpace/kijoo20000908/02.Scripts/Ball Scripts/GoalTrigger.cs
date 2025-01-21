using UnityEngine;

public class GoalTrigger : MonoBehaviour
{
    public PuzzleManager3 puzzleManager3; // 퍼즐 매니저와 연결

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Basketball")) // 공의 태그 확인
        {
            Debug.Log("농구공이 골대에 들어갔습니다!");
            puzzleManager3.CompletePuzzle(); // 퍼즐 완료 호출
        }
    }
}
