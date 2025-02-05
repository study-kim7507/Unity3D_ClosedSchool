using UnityEngine;
using TMPro;

public class PuzzleHint : MonoBehaviour
{
    public TextMeshProUGUI hintText; // "������" �ؽ�Ʈ UI
    public GameObject hintBackgroundPanel; // "������" ��� �г�
    public string targetTag = "HintBook"; // ������ �±�

    private bool isLookingAtPuzzle = false; // ���� �������� �ٶ󺸰� �ִ��� ����
    private PlayerController playerController;
    void Start()
    {
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();

        if (hintText != null) hintText.gameObject.SetActive(false); // ó������ ��Ʈ ����
        if (hintBackgroundPanel != null) hintBackgroundPanel.SetActive(false); // ��� �гε� ����
    }

    void Update()
    {
        CheckPlayerView();
    }

    // �÷��̾ Ư�� �±׸� ���� ������Ʈ�� �ٶ󺸰� �ִ��� Ȯ��
    void CheckPlayerView()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 2.5f)) // �þ� ���� 5m�� ����
        {
            if (hit.transform.CompareTag(targetTag)) // Ư�� �±׸� ���� ������Ʈ ����
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

    // "������" �ؽ�Ʈ�� ��� ���̱�
    void ShowHint()
    {
        if (GetComponent<AnswerChecker>() != null && (GetComponent<AnswerChecker>().isShownAnswerField || GetComponent<AnswerChecker>().isShownWrongAnswerPanel)) return;
        if (hintText != null) hintText.gameObject.SetActive(true);
        if (hintBackgroundPanel != null) hintBackgroundPanel.SetActive(true);
    }

    // "������" �ؽ�Ʈ�� ��� �����
    void HideHint()
    {
        if (hintText != null) hintText.gameObject.SetActive(false);
        if (hintBackgroundPanel != null) hintBackgroundPanel.SetActive(false);
    }
}
