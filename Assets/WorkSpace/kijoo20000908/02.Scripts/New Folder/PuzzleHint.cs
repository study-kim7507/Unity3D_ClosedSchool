using UnityEngine;
using TMPro;

public class PuzzleHint : MonoBehaviour
{
    public TextMeshProUGUI hintText; // "문제집" 텍스트 UI
    public string targetTag = "PuzzleBook"; // 감지할 태그

    private bool isLookingAtPuzzle = false; // 현재 문제집을 바라보고 있는지 여부

    void Start()
    {
        if (hintText != null)
        {
            hintText.gameObject.SetActive(false); // 처음에는 힌트 숨김
        }
    }

    void Update()
    {
        CheckPlayerView();
    }

    // 플레이어가 특정 태그를 가진 오브젝트를 바라보고 있는지 확인
    void CheckPlayerView()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // 마우스 포인터 방향으로 레이 발사
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.CompareTag(targetTag)) // 특정 태그를 가진 오브젝트 감지
            {
                if (!isLookingAtPuzzle)
                {
                    isLookingAtPuzzle = true;
                    ShowHint();
                }
            }
            else
            {
                if (isLookingAtPuzzle)
                {
                    isLookingAtPuzzle = false;
                    HideHint();
                }
            }
        }
        else
        {
            if (isLookingAtPuzzle)
            {
                isLookingAtPuzzle = false;
                HideHint();
            }
        }
    }

    // "문제집" 텍스트 보이기
    void ShowHint()
    {
        if (hintText != null && !hintText.gameObject.activeSelf)
        {
            hintText.gameObject.SetActive(true);
        }
    }

    // "문제집" 텍스트 숨기기
    void HideHint()
    {
        if (hintText != null && hintText.gameObject.activeSelf)
        {
            hintText.gameObject.SetActive(false);
        }
    }
}
