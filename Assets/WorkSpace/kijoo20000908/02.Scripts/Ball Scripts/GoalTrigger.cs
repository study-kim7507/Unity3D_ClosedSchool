using UnityEngine;
using TMPro;
using System.Collections; // TextMeshPro ���

public class GoalTrigger : MonoBehaviour
{
    public PuzzleManager3 puzzleManager3; // ���� �Ŵ���
    public int requiredGoals = 3; // �ʿ��� �� Ƚ��
    private int currentGoals = 0; // ���� �� ���� Ƚ��
    public TMP_Text goalCountText; // �� ���� UI �ؽ�Ʈ

    public AudioClip goalSound; // ���� ���� �� �Ҹ�
    public AudioClip puzzleCompleteSound; // ���� �Ϸ� �Ҹ�
    private AudioSource audioSource; // ����� �ҽ�

    private Coroutine textOnCoroutine = null;

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
            PlayerUI.instance.DisplayInteractionDescription("���� ����.\n�� �� �ͽ��� ����ຸ��..");
            if (textOnCoroutine == null) textOnCoroutine = StartCoroutine(HideUICoroutine());
            else
            {
                StopCoroutine(textOnCoroutine);
                textOnCoroutine = StartCoroutine(HideUICoroutine());
            }

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
                    PlayerUI.instance.DisplayInteractionDescription("�ͽ��� �������, ���� ��ġ�� �ΰ� ����.");
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

    private IEnumerator HideUICoroutine()
    {
        yield return new WaitForSeconds(5.0f);
        HideUI();
    }
}
