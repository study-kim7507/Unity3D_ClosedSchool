using UnityEngine;
using TMPro;

public class PuzzleHint : MonoBehaviour
{
    public TextMeshProUGUI hintText; // "������" �ؽ�Ʈ UI
    public GameObject hintBackgroundPanel; // "������" ��� �г�
    public string targetTag = "HintBook"; // ������ �±�

    private bool isLookingAtPuzzle = false; // ���� �������� �ٶ󺸰� �ִ��� ����

    void Start()
    {
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

    // "������" �ؽ�Ʈ�� ��� ���̱�
    void ShowHint()
    {
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
