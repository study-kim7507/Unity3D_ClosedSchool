using UnityEngine;
using TMPro;

public class PuzzleHint : MonoBehaviour
{
    public TextMeshProUGUI hintText; // "������" �ؽ�Ʈ UI
    public string targetTag = "PuzzleBook"; // ������ �±�

    private bool isLookingAtPuzzle = false; // ���� �������� �ٶ󺸰� �ִ��� ����

    void Start()
    {
        if (hintText != null)
        {
            hintText.gameObject.SetActive(false); // ó������ ��Ʈ ����
        }
    }

    void Update()
    {
        CheckPlayerView();
    }

    // �÷��̾ Ư�� �±׸� ���� ������Ʈ�� �ٶ󺸰� �ִ��� Ȯ��
    void CheckPlayerView()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // ���콺 ������ �������� ���� �߻�
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
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

    // "������" �ؽ�Ʈ ���̱�
    void ShowHint()
    {
        if (hintText != null && !hintText.gameObject.activeSelf)
        {
            hintText.gameObject.SetActive(true);
        }
    }

    // "������" �ؽ�Ʈ �����
    void HideHint()
    {
        if (hintText != null && hintText.gameObject.activeSelf)
        {
            hintText.gameObject.SetActive(false);
        }
    }
}
