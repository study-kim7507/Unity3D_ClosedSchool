using UnityEngine;
using TMPro;
using System.Collections;

public class AnswerChecker : MonoBehaviour
{
    public GameObject answerPanel; // 정답 입력 패널 (TMP_InputField 포함)
    public TMP_InputField answerInput; // 정답 입력 필드
    public TextMeshProUGUI hintText; // "정답지" 표시할 텍스트
    public TextMeshProUGUI wrongAnswerText; // "틀렸습니다" 표시할 텍스트
    public GameObject wrongAnswerPanel;
    public string correctAnswer = "5"; // 정답
    public GameObject rewardPrefab; // 보상 아이템 프리팹
    public Transform rewardSpawnPoint; // 보상 아이템이 생성될 위치
    public AudioSource audioSource; // 오디오 소스
    public AudioClip correctSound; // 정답 시 재생할 소리

    private bool isLookingAtBook = false; // 플레이어가 책을 바라보고 있는지 여부
    private bool isAnswering = false; // 정답 입력 중인지 확인
    private bool isSolved = false; // 퍼즐이 해결되었는지 확인

    void Start()
    {
        answerPanel.SetActive(false); // 처음에는 입력 필드 숨기기
        if (hintText != null) hintText.gameObject.SetActive(false); // "정답지" 텍스트 숨기기
        if (wrongAnswerText != null)
        {
            wrongAnswerText.gameObject.SetActive(false); // "틀렸습니다" 텍스트 숨기기
            wrongAnswerPanel.SetActive(false);
        }
    }

    void Update()
    {
        // 플레이어의 시선이 책을 바라보고 있는지 체크
        CheckPlayerView();

        // E 키를 눌러 정답 입력 필드 열기
        if (isLookingAtBook && !isSolved && Input.GetKeyDown(KeyCode.E))
        {
            ShowAnswerInput();
        }

        // Enter 키로 정답 확인
        if (isAnswering && Input.GetKeyDown(KeyCode.Return))
        {
            CheckAnswer();
        }
    }

    // 플레이어가 실제로 책을 바라보고 있는지 확인하는 함수
    void CheckPlayerView()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // 마우스 포인터 방향으로 레이 발사
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform == transform && !isSolved) // 플레이어가 책을 바라볼 때 (퍼즐 미해결 상태)
            {
                if (!isLookingAtBook)
                {
                    isLookingAtBook = true;
                    ShowHintText();
                }
            }
            else
            {
                if (isLookingAtBook)
                {
                    isLookingAtBook = false;
                    HideHintText();
                }
            }
        }
        else
        {
            if (isLookingAtBook)
            {
                isLookingAtBook = false;
                HideHintText();
            }
        }
    }

    // "정답지" 텍스트를 보이게 함
    void ShowHintText()
    {
        if (hintText != null)
        {
            hintText.gameObject.SetActive(true);
        }
    }

    // "정답지" 텍스트를 숨김
    void HideHintText()
    {
        if (hintText != null)
        {
            hintText.gameObject.SetActive(false);
        }
    }

    // 정답 입력 필드를 보이게 함 (E 키를 눌렀을 때 실행)
    void ShowAnswerInput()
    {
        answerPanel.SetActive(true);
        answerInput.text = ""; // 입력 필드 초기화
        answerInput.ActivateInputField(); // 입력창에 포커스
        isAnswering = true;
    }

    // 정답 확인 함수
    public void CheckAnswer()
    {
        if (answerInput.text.Trim().ToLower() == correctAnswer.ToLower())
        {
            Debug.Log("정답입니다!");
            PlayCorrectSound(); // 정답 소리 재생
            SpawnReward();
            answerPanel.SetActive(false); // 정답 입력창 숨기기
            isAnswering = false;
            isSolved = true; // 퍼즐 해결 상태로 변경
            DisableBookInteraction(); // 책 클릭 불가능하게 변경
        }
        else
        {
            Debug.Log("오답입니다. 다시 시도하세요.");
            HideAnswerInput(); // 오답 시 패널 닫기
            ShowWrongAnswerText(); // "틀렸습니다" 텍스트 표시
        }
    }

    // 정답 입력 패널 숨기기 (오답일 때 실행)
    void HideAnswerInput()
    {
        answerPanel.SetActive(false);
        isAnswering = false;
    }

    // "틀렸습니다" 텍스트를 표시하고 일정 시간 후 자동으로 숨기기
    void ShowWrongAnswerText()
    {
        if (wrongAnswerText != null)
        {
            wrongAnswerText.gameObject.SetActive(true);
            wrongAnswerPanel.SetActive(true);
            StartCoroutine(HideWrongAnswerTextAfterDelay(2f)); // 2초 후 숨기기
        }
    }

    // "틀렸습니다" 텍스트를 일정 시간 후 숨기는 코루틴
    IEnumerator HideWrongAnswerTextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (wrongAnswerText != null)
        {
            wrongAnswerPanel.gameObject.SetActive(false);
            wrongAnswerText.gameObject.SetActive(false);
        }
    }

    // 정답이면 보상 아이템 생성
    void SpawnReward()
    {
        if (rewardPrefab != null)
        {
            Vector3 spawnPosition = rewardSpawnPoint != null ? rewardSpawnPoint.position : transform.position + Vector3.up * 1.5f;
            Instantiate(rewardPrefab, spawnPosition, Quaternion.identity);
            Debug.Log("보상이 생성되었습니다! 위치: " + spawnPosition);
        }
        else
        {
            Debug.LogWarning("보상 프리팹이 설정되지 않았습니다!");
        }
    }

    // 정답 시 소리 재생
    void PlayCorrectSound()
    {
        if (audioSource != null && correctSound != null)
        {
            audioSource.PlayOneShot(correctSound);
        }
    }

    // 책 인터랙션 비활성화
    void DisableBookInteraction()
    {
        GetComponent<Collider>().enabled = false; // 클릭 방지
        if (hintText != null)
        {
            hintText.gameObject.SetActive(false); // "정답지" 텍스트 숨김
        }
        Debug.Log("책이 비활성화되었습니다.");
    }
}


