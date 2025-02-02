using UnityEngine;
using TMPro; // TextMeshPro 사용

public class BallStorage : MonoBehaviour
{
    public int totalBallsNeeded = 2; // 필요한 농구공 개수
    private int currentBallCount = 0;
    public PuzzleManager2 puzzleManager2; // 퍼즐 완료 체크
    public TMP_Text ballCountText; // 농구공 개수 표시 UI
    public TMP_Text instructionText; // 보관함을 바라볼 때 보이는 안내 UI

    private bool isPlayerLooking = false;

    private void Start()
    {
        if (ballCountText != null)
        {
            ballCountText.gameObject.SetActive(false); // 게임 시작 시 UI 숨김
        }

        if (instructionText != null)
        {
            instructionText.gameObject.SetActive(false); // 안내 메시지 숨김
        }
    }

    private void Update()
    {
        if (isPlayerLooking && instructionText != null)
        {
            instructionText.gameObject.SetActive(true); // 보관함을 바라보면 UI 표시
        }
        else if (instructionText != null)
        {
            instructionText.gameObject.SetActive(false); // 보관함에서 시선을 떼면 UI 숨김
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Basketball")) // 공이 보관함에 들어오면
        {
            currentBallCount++;
            Debug.Log($"농구공 {currentBallCount}/{totalBallsNeeded} 개가 보관함에 들어갔습니다.");
            UpdateBallCountUI();

            // 모든 공이 보관되면 퍼즐 완료
            if (currentBallCount >= totalBallsNeeded)
            {
                puzzleManager2.CompletePuzzle();
                HideUI(); // UI 숨기기
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Basketball") && currentBallCount < totalBallsNeeded) // 공이 빠져나가면 개수 감소
        {
            currentBallCount--;
            Debug.Log($"농구공이 빠져나갔습니다! 현재 개수: {currentBallCount}/{totalBallsNeeded}");
            UpdateBallCountUI();
        }
    }

    private void UpdateBallCountUI()
    {
        if (ballCountText != null)
        {
            // UI 활성화 및 텍스트 업데이트
            ballCountText.text = $"{currentBallCount}/{totalBallsNeeded}";
            ballCountText.gameObject.SetActive(currentBallCount > 0 && currentBallCount < totalBallsNeeded);
        }
    }

    private void HideUI()
    {
        if (ballCountText != null)
        {
            ballCountText.gameObject.SetActive(false);
        }
    }

    // 플레이어가 보관함을 바라볼 때 실행
    public void StartLookingAtStorage()
    {
        isPlayerLooking = true;
    }

    // 플레이어가 보관함에서 시선을 뗄 때 실행
    public void StopLookingAtStorage()
    {
        isPlayerLooking = false;
    }
}
