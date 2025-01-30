using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public void CheckPuzzleCompletion(int currentBookCount, int requiredBookCount)
    {
        if (currentBookCount >= requiredBookCount) // 4개의 책이 모두 들어왔는지 체크
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
        Debug.Log("퍼즐이 완료되었습니다! 모든 책이 올바른 위치에 놓였습니다.");
        // 문 열기, 효과 발생 등의 추가 기능 실행
    }
}
