using UnityEngine;
using TMPro; // TextMeshPro ���

public class GoalTrigger : MonoBehaviour
{
    public PuzzleManager3 puzzleManager3; // ���� �Ŵ���
    public int requiredGoals = 3; // �ʿ��� �� Ƚ��
    private int currentGoals = 0; // ���� �� ���� Ƚ��
    public TMP_Text goalCountText; // �� ���� UI �ؽ�Ʈ

    private void Start()
    {
        if (goalCountText != null)
        {
            goalCountText.gameObject.SetActive(false); // UI ó���� ����
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Basketball")) // �󱸰��� ��뿡 ���� ��
        {
            currentGoals++;
            Debug.Log($"�� ����! ���� �� ��: {currentGoals}/{requiredGoals}");
            UpdateGoalCountUI();

            if (currentGoals >= requiredGoals) // 3���� ������ ���� �Ϸ�
            {
                puzzleManager3.CompletePuzzle();
                HideUI(); // ���� �Ϸ� �� UI ����
            }
        }
    }

    private void UpdateGoalCountUI()
    {
        if (goalCountText != null)
        {
            goalCountText.text = $"{currentGoals}/{requiredGoals}";
            goalCountText.gameObject.SetActive(true); // UI Ȱ��ȭ
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
