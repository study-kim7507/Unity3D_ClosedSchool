using UnityEngine;

public class PuzzleManager3 : MonoBehaviour
{
    public GameObject goalEffect; // 골 성공 시 표시할 효과 (예: 파티클)
    private bool isPuzzleSolved = false;

    // 퍼즐 상태를 확인
    public void CompletePuzzle()
    {
        if (!isPuzzleSolved)
        {
            isPuzzleSolved = true;
            Debug.Log("퍼즐이 풀렸습니다!");

            // 골 성공 효과 실행
            if (goalEffect != null)
            {
                Instantiate(goalEffect, transform.position, Quaternion.identity);
            }

            // 추가로 퍼즐 완료 로직을 작성하세요 (예: 문 열기, 아이템 생성 등)
        }
    }
}
