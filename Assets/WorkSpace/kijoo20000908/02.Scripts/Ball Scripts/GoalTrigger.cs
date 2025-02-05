using UnityEngine;
using TMPro; // TextMeshPro ���

public class GoalTrigger : MonoBehaviour
{
    public PuzzleManager3 puzzleManager3; // ���� �Ŵ���
    public int requiredGoals = 3; // �ʿ��� �� Ƚ��
    private int currentGoals = 0; // ���� �� ���� Ƚ��
    public TMP_Text goalCountText; // �� ���� UI �ؽ�Ʈ

    public AudioClip goalSound; // ���� ���� �� �Ҹ�
    public AudioClip puzzleCompleteSound; // ���� �Ϸ� �Ҹ�
    private AudioSource audioSource; // ����� �ҽ�

    private void Start()
    {
        if (goalCountText != null)
        {
            goalCountText.gameObject.SetActive(false); // UI ó���� ����
        }

        // ����� �ҽ� �߰� (���ٸ� �ڵ� �߰�)
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Basketball")) // �󱸰��� ��뿡 ���� ��
        {
            currentGoals++;
            Debug.Log($"�� ����! ���� �� ��: {currentGoals}/{requiredGoals}");

            // �� ȿ���� ���
            if (goalSound != null)
            {
                audioSource.PlayOneShot(goalSound);
            }

            UpdateGoalCountUI();

            if (currentGoals >= requiredGoals) // 3���� ������ ���� �Ϸ�
            {
                puzzleManager3.CompletePuzzle();

                // ���� �Ϸ� �Ҹ� ���
                if (puzzleCompleteSound != null)
                {
                    audioSource.PlayOneShot(puzzleCompleteSound);
                }

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
