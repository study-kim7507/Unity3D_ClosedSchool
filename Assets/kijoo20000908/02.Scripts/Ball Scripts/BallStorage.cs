using UnityEngine;
using TMPro;
using System.Collections; // TextMeshPro 사용

public class BallStorage : MonoBehaviour
{
    public int totalBallsNeeded = 10; // 필요한 농구공 개수
    private int currentBallCount = 0; // 현재 보관된 농구공 개수
    public TMP_Text ballCountText; // UI 텍스트 (현재 개수 / 총 개수)
    public PuzzleManager2 puzzleManager2; // 퍼즐 완료 체크

    public AudioClip ballPlacedSound; // 농구공이 정리될 때 소리
    public AudioClip puzzleCompleteSound; // 퍼즐 완료 시 소리
    private AudioSource audioSource; // 오디오 소스

    private bool isCompleted = false;
    private bool isTextOn = false;
    
    private Coroutine textOnCoroutine = null;

    private void Start()
    {
        if (ballCountText != null)
        {
            ballCountText.gameObject.SetActive(false); // UI 처음엔 숨김
        }

        // 오디오 소스 추가 (없다면 자동 추가)
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Basketball")) // 농구공이 정리 구역에 들어오면
        {
            if (!isCompleted) PlayerUI.instance.DisplayInteractionDescription("농구공을 더 찾아 정리해야 귀신이 만족할 것 같다.");
            if (textOnCoroutine == null) textOnCoroutine = StartCoroutine(HideUICoroutine());
            else
            {
                StopCoroutine(textOnCoroutine);
                textOnCoroutine = StartCoroutine(HideUICoroutine());
            }
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
                if (puzzleCompleteSound != null && !isCompleted)
                {
                    PlayerUI.instance.DisplayInteractionDescription("어느정도 정리를 하자 귀신이 무언가 두고 갔다.");
                    HideUI(); // 퍼즐 완료 후 UI 숨김
                    audioSource.PlayOneShot(puzzleCompleteSound);
                    isCompleted = true;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Basketball")) // 농구공이 정리 구역에서 나가면 개수 감소
        {
            if (textOnCoroutine == null) textOnCoroutine = StartCoroutine(HideUICoroutine());
            else
            {
                StopCoroutine(textOnCoroutine);
                textOnCoroutine = StartCoroutine(HideUICoroutine());
            }

            currentBallCount--;
            Debug.Log($"농구공이 빠져나갔습니다! 현재 개수: {currentBallCount}/{totalBallsNeeded}");
            UpdateBallCountUI();
        }
    }

    private void UpdateBallCountUI()
    {
        if (ballCountText != null && !isCompleted)
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

    private IEnumerator HideUICoroutine()
    {
        yield return new WaitForSeconds(5.0f);
        HideUI();
    }
}
