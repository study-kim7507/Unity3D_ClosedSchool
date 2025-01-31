using UnityEngine;

public class BallStorage : MonoBehaviour
{
    public int totalBallsNeeded = 3; // 필요한 농구공 개수
    private int currentBallCount = 0;
    public PuzzleManager2 puzzleManager2; // 퍼즐 완료 체크

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Basketball"))
        {
            currentBallCount++;
            Debug.Log($"농구공 {currentBallCount}/{totalBallsNeeded} 개가 보관함에 놓였습니다.");

            Destroy(other.gameObject); // 공을 보관함에 넣으면 삭제

            if (currentBallCount >= totalBallsNeeded)
            {
                puzzleManager2.CompletePuzzle(); // 퍼즐 완료
            }
        }
    }
}
