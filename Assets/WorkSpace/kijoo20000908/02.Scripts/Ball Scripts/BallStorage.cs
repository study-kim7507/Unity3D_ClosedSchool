using UnityEngine;
using TMPro; // TextMeshPro 사용

public class BallStorage : MonoBehaviour
{
    public int totalBallsNeeded = 10; // 필요한 농구공 개수
    private int currentBallCount = 0; // 현재 보관된 농구공 개수
    public TMP_Text ballCountText; // UI 텍스트 (현재 개수 / 총 개수)
    public PuzzleManager2 puzzleManager2; // 퍼즐 완료 체크

    public AudioClip ballPlacedSound; // 농구공이 정리될 때 소리
    public AudioClip puzzleCompleteSound; // 퍼즐 완료 시 소리
    private AudioSource audioSource; // 오디오 소스

    private void Start()
    {
        if (ballCountText != null)
        {
            ballCountText.gameObject.SetActive(false); // UI 처음엔 숨김
        }

        // 오디오 소스 추가 (없다면 자동 추가)
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Basketball")) // 농구공이 정리 구역에 들어오면
        {
            currentBallCount++;
            Debug.Log($"농구공 {currentBallCount}/{totalBallsNeeded} 개가 정리되었습니다.");
            UpdateBallCountUI();

            // 농구공 정리 소리 재생
            if (ballPlacedSound != null)
            {
                audioSource.PlayOneShot(ballPlacedSound);
            }

            // 모든 공이 정리되면 퍼즐 완료
            if (currentBallCount >= totalBallsNeeded)
            {
                puzzleManager2.CompletePuzzle();

                // 퍼즐 완료 소리 재생
                if (puzzleCompleteSound != null)
                {
                    audioSource.PlayOneShot(puzzleCompleteSound);
                }

                HideUI(); // 퍼즐 완료 후 UI 숨김
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Basketball")) // 농구공이 정리 구역에서 나가면 개수 감소
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
}
