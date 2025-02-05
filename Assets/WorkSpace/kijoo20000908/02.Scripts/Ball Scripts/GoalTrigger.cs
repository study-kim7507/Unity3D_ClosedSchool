using UnityEngine;
using TMPro; // TextMeshPro 사용

public class GoalTrigger : MonoBehaviour
{
    public PuzzleManager3 puzzleManager3; // 퍼즐 매니저
    public int requiredGoals = 3; // 필요한 골 횟수
    private int currentGoals = 0; // 현재 골 성공 횟수
    public TMP_Text goalCountText; // 골 개수 UI 텍스트

    public AudioClip goalSound; // 골이 들어갔을 때 소리
    public AudioClip puzzleCompleteSound; // 퍼즐 완료 소리
    private AudioSource audioSource; // 오디오 소스

    private void Start()
    {
        if (goalCountText != null)
        {
            goalCountText.gameObject.SetActive(false); // UI 처음엔 숨김
        }

        // 오디오 소스 추가 (없다면 자동 추가)
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Basketball")) // 농구공이 골대에 들어갔을 때
        {
            currentGoals++;
            Debug.Log($"골 성공! 현재 골 수: {currentGoals}/{requiredGoals}");

            // 골 효과음 재생
            if (goalSound != null)
            {
                audioSource.PlayOneShot(goalSound);
            }

            UpdateGoalCountUI();

            if (currentGoals >= requiredGoals) // 3골을 넣으면 퍼즐 완료
            {
                puzzleManager3.CompletePuzzle();

                // 퍼즐 완료 소리 재생
                if (puzzleCompleteSound != null)
                {
                    audioSource.PlayOneShot(puzzleCompleteSound);
                }

                HideUI(); // 퍼즐 완료 후 UI 숨김
            }
        }
    }

    private void UpdateGoalCountUI()
    {
        if (goalCountText != null)
        {
            goalCountText.text = $"{currentGoals}/{requiredGoals}";
            goalCountText.gameObject.SetActive(true); // UI 활성화
        }
    }

    private void HideUI()
    {
        if (goalCountText != null)
        {
            goalCountText.gameObject.SetActive(false);
        }
    }
}
