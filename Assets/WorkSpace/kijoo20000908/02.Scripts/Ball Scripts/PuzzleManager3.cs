using UnityEngine;

public class PuzzleManager3 : MonoBehaviour
{
    public GameObject goalEffect; // 골 성공 시 표시할 효과 (예: 파티클)
    private bool goal1Completed = false;
    private bool goal2Completed = false;
    private bool isPuzzleSolved = false;

    // 골대 1에 공이 들어갔을 때 호출
    public void Goal1Scored()
    {
        if (!goal1Completed)
        {
            goal1Completed = true;
            Debug.Log("첫 번째 골대에 공이 들어갔습니다!");
            CheckPuzzleCompletion();
        }
    }

    // 골대 2에 공이 들어갔을 때 호출
    public void Goal2Scored()
    {
        if (!goal2Completed)
        {
            goal2Completed = true;
            Debug.Log("두 번째 골대에 공이 들어갔습니다!");
            CheckPuzzleCompletion();
        }
    }

    // 두 골대에 모두 공이 들어가면 퍼즐 완료
    private void CheckPuzzleCompletion()
    {
        if (goal1Completed && goal2Completed && !isPuzzleSolved)
        {
            isPuzzleSolved = true;
            Debug.Log("퍼즐이 풀렸습니다! (양쪽 골대에 공이 들어감)");

            // 퍼즐 완료 효과
            if (goalEffect != null)
            {
                Instantiate(goalEffect, transform.position, Quaternion.identity);
            }

            // 퍼즐 완료 시 추가 동작 (예: 문 열기, 아이템 지급 등) 가능
        }
    }
}
