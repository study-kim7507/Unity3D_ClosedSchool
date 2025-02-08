using UnityEngine;
using TMPro;

public class PuzzleHint : MonoBehaviour
{
    public TextMeshProUGUI hintText; // "정답지" 텍스트 UI
    public GameObject hintBackgroundPanel; // "정답지" 배경 패널
    public string targetTag = "HintBook"; // 감지할 태그

    private bool isLookingAtPuzzle = false; // 현재 문제집을 바라보고 있는지 여부
    private PlayerController playerController;
    void Start()
    {
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();

        if (hintText != null) hintText.gameObject.SetActive(false); // 처음에는 힌트 숨김
        if (hintBackgroundPanel != null) hintBackgroundPanel.SetActive(false); // 배경 패널도 숨김
    }

    void Update()
    {
        CheckPlayerView();
    }

    // 플레이어가 특정 태그를 가진 오브젝트를 바라보고 있는지 확인
    void CheckPlayerView()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 2.5f)) // 시야 범위 5m로 제한
        {
            if (hit.transform.CompareTag(targetTag)) // 특정 태그를 가진 오브젝트 감지
            {
                if (!isLookingAtPuzzle)
                {
                    playerController.canOpenInventory = true;
                    isLookingAtPuzzle = true;
                    ShowHint();
                }
            }
            else
            {
                if (isLookingAtPuzzle)
                {
                    playerController.canOpenInventory = false;
                    isLookingAtPuzzle = false;
                    HideHint();
                }
            }
        }
        else
        {
            if (isLookingAtPuzzle)
            { 
                playerController.canOpenInventory = false;
                isLookingAtPuzzle = false;
                HideHint();
            }
        }
    }

    // "정답지" 텍스트와 배경 보이기
    void ShowHint()
    {
        if (GetComponent<AnswerChecker>() != null && (GetComponent<AnswerChecker>().isShownAnswerField || GetComponent<AnswerChecker>().isShownWrongAnswerPanel)) return;
        if (hintText != null) hintText.gameObject.SetActive(true);
        if (hintBackgroundPanel != null) hintBackgroundPanel.SetActive(true);
    }

    // "정답지" 텍스트와 배경 숨기기
    void HideHint()
    {
        if (hintText != null) hintText.gameObject.SetActive(false);
        if (hintBackgroundPanel != null) hintBackgroundPanel.SetActive(false);
    }
}
