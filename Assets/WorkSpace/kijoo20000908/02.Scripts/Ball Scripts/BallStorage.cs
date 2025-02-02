using UnityEngine;
using TMPro; // TextMeshPro ���

public class BallStorage : MonoBehaviour
{
    public int totalBallsNeeded = 2; // �ʿ��� �󱸰� ����
    private int currentBallCount = 0;
    public PuzzleManager2 puzzleManager2; // ���� �Ϸ� üũ
    public TMP_Text ballCountText; // �󱸰� ���� ǥ�� UI
    public TMP_Text instructionText; // �������� �ٶ� �� ���̴� �ȳ� UI

    private bool isPlayerLooking = false;

    private void Start()
    {
        if (ballCountText != null)
        {
            ballCountText.gameObject.SetActive(false); // ���� ���� �� UI ����
        }

        if (instructionText != null)
        {
            instructionText.gameObject.SetActive(false); // �ȳ� �޽��� ����
        }
    }

    private void Update()
    {
        if (isPlayerLooking && instructionText != null)
        {
            instructionText.gameObject.SetActive(true); // �������� �ٶ󺸸� UI ǥ��
        }
        else if (instructionText != null)
        {
            instructionText.gameObject.SetActive(false); // �����Կ��� �ü��� ���� UI ����
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Basketball")) // ���� �����Կ� ������
        {
            currentBallCount++;
            Debug.Log($"�󱸰� {currentBallCount}/{totalBallsNeeded} ���� �����Կ� �����ϴ�.");
            UpdateBallCountUI();

            // ��� ���� �����Ǹ� ���� �Ϸ�
            if (currentBallCount >= totalBallsNeeded)
            {
                puzzleManager2.CompletePuzzle();
                HideUI(); // UI �����
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Basketball") && currentBallCount < totalBallsNeeded) // ���� ���������� ���� ����
        {
            currentBallCount--;
            Debug.Log($"�󱸰��� �����������ϴ�! ���� ����: {currentBallCount}/{totalBallsNeeded}");
            UpdateBallCountUI();
        }
    }

    private void UpdateBallCountUI()
    {
        if (ballCountText != null)
        {
            // UI Ȱ��ȭ �� �ؽ�Ʈ ������Ʈ
            ballCountText.text = $"{currentBallCount}/{totalBallsNeeded}";
            ballCountText.gameObject.SetActive(currentBallCount > 0 && currentBallCount < totalBallsNeeded);
        }
    }

    private void HideUI()
    {
        if (ballCountText != null)
        {
            ballCountText.gameObject.SetActive(false);
        }
    }

    // �÷��̾ �������� �ٶ� �� ����
    public void StartLookingAtStorage()
    {
        isPlayerLooking = true;
    }

    // �÷��̾ �����Կ��� �ü��� �� �� ����
    public void StopLookingAtStorage()
    {
        isPlayerLooking = false;
    }
}
