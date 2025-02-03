using UnityEngine;
using TMPro; // TextMeshPro ���

public class BallStorage : MonoBehaviour
{
    public int totalBallsNeeded = 10; // �ʿ��� �󱸰� ����
    private int currentBallCount = 0; // ���� ������ �󱸰� ����
    public TMP_Text ballCountText; // UI �ؽ�Ʈ (���� ���� / �� ����)
    public PuzzleManager2 puzzleManager2; // ���� �Ϸ� üũ

    public AudioClip ballPlacedSound; // �󱸰��� ������ �� �Ҹ�
    public AudioClip puzzleCompleteSound; // ���� �Ϸ� �� �Ҹ�
    private AudioSource audioSource; // ����� �ҽ�

    private void Start()
    {
        if (ballCountText != null)
        {
            ballCountText.gameObject.SetActive(false); // UI ó���� ����
        }

        // ����� �ҽ� �߰� (���ٸ� �ڵ� �߰�)
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Basketball")) // �󱸰��� ���� ������ ������
        {
            currentBallCount++;
            Debug.Log($"�󱸰� {currentBallCount}/{totalBallsNeeded} ���� �����Ǿ����ϴ�.");
            UpdateBallCountUI();

            // �󱸰� ���� �Ҹ� ���
            if (ballPlacedSound != null)
            {
                audioSource.PlayOneShot(ballPlacedSound);
            }

            // ��� ���� �����Ǹ� ���� �Ϸ�
            if (currentBallCount >= totalBallsNeeded)
            {
                puzzleManager2.CompletePuzzle();

                // ���� �Ϸ� �Ҹ� ���
                if (puzzleCompleteSound != null)
                {
                    audioSource.PlayOneShot(puzzleCompleteSound);
                }

                HideUI(); // ���� �Ϸ� �� UI ����
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Basketball")) // �󱸰��� ���� �������� ������ ���� ����
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
}
