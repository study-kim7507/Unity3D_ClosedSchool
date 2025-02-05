using UnityEngine;
using TMPro;
using System.Collections;

public class AnswerChecker : MonoBehaviour
{
    public GameObject answerPanel; // ���� �Է� �г� (TMP_InputField ����)
    public TMP_InputField answerInput; // ���� �Է� �ʵ�
    public TextMeshProUGUI hintText; // "������" ǥ���� �ؽ�Ʈ
    public TextMeshProUGUI wrongAnswerText; // "Ʋ�Ƚ��ϴ�" ǥ���� �ؽ�Ʈ
    public GameObject wrongAnswerPanel;
    public string correctAnswer = "5"; // ����
    public GameObject rewardPrefab; // ���� ������ ������
    public Transform rewardSpawnPoint; // ���� �������� ������ ��ġ
    public AudioSource audioSource; // ����� �ҽ�
    public AudioClip correctSound; // ���� �� ����� �Ҹ�

    private bool isLookingAtBook = false; // �÷��̾ å�� �ٶ󺸰� �ִ��� ����
    private bool isAnswering = false; // ���� �Է� ������ Ȯ��
    private bool isSolved = false; // ������ �ذ�Ǿ����� Ȯ��

    void Start()
    {
        answerPanel.SetActive(false); // ó������ �Է� �ʵ� �����
        if (hintText != null) hintText.gameObject.SetActive(false); // "������" �ؽ�Ʈ �����
        if (wrongAnswerText != null)
        {
            wrongAnswerText.gameObject.SetActive(false); // "Ʋ�Ƚ��ϴ�" �ؽ�Ʈ �����
            wrongAnswerPanel.SetActive(false);
        }
    }

    void Update()
    {
        // �÷��̾��� �ü��� å�� �ٶ󺸰� �ִ��� üũ
        CheckPlayerView();

        // E Ű�� ���� ���� �Է� �ʵ� ����
        if (isLookingAtBook && !isSolved && Input.GetKeyDown(KeyCode.E))
        {
            ShowAnswerInput();
        }

        // Enter Ű�� ���� Ȯ��
        if (isAnswering && Input.GetKeyDown(KeyCode.Return))
        {
            CheckAnswer();
        }
    }

    // �÷��̾ ������ å�� �ٶ󺸰� �ִ��� Ȯ���ϴ� �Լ�
    void CheckPlayerView()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // ���콺 ������ �������� ���� �߻�
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform == transform && !isSolved) // �÷��̾ å�� �ٶ� �� (���� ���ذ� ����)
            {
                if (!isLookingAtBook)
                {
                    isLookingAtBook = true;
                    ShowHintText();
                }
            }
            else
            {
                if (isLookingAtBook)
                {
                    isLookingAtBook = false;
                    HideHintText();
                }
            }
        }
        else
        {
            if (isLookingAtBook)
            {
                isLookingAtBook = false;
                HideHintText();
            }
        }
    }

    // "������" �ؽ�Ʈ�� ���̰� ��
    void ShowHintText()
    {
        if (hintText != null)
        {
            hintText.gameObject.SetActive(true);
        }
    }

    // "������" �ؽ�Ʈ�� ����
    void HideHintText()
    {
        if (hintText != null)
        {
            hintText.gameObject.SetActive(false);
        }
    }

    // ���� �Է� �ʵ带 ���̰� �� (E Ű�� ������ �� ����)
    void ShowAnswerInput()
    {
        answerPanel.SetActive(true);
        answerInput.text = ""; // �Է� �ʵ� �ʱ�ȭ
        answerInput.ActivateInputField(); // �Է�â�� ��Ŀ��
        isAnswering = true;
    }

    // ���� Ȯ�� �Լ�
    public void CheckAnswer()
    {
        if (answerInput.text.Trim().ToLower() == correctAnswer.ToLower())
        {
            Debug.Log("�����Դϴ�!");
            PlayCorrectSound(); // ���� �Ҹ� ���
            SpawnReward();
            answerPanel.SetActive(false); // ���� �Է�â �����
            isAnswering = false;
            isSolved = true; // ���� �ذ� ���·� ����
            DisableBookInteraction(); // å Ŭ�� �Ұ����ϰ� ����
        }
        else
        {
            Debug.Log("�����Դϴ�. �ٽ� �õ��ϼ���.");
            HideAnswerInput(); // ���� �� �г� �ݱ�
            ShowWrongAnswerText(); // "Ʋ�Ƚ��ϴ�" �ؽ�Ʈ ǥ��
        }
    }

    // ���� �Է� �г� ����� (������ �� ����)
    void HideAnswerInput()
    {
        answerPanel.SetActive(false);
        isAnswering = false;
    }

    // "Ʋ�Ƚ��ϴ�" �ؽ�Ʈ�� ǥ���ϰ� ���� �ð� �� �ڵ����� �����
    void ShowWrongAnswerText()
    {
        if (wrongAnswerText != null)
        {
            wrongAnswerText.gameObject.SetActive(true);
            wrongAnswerPanel.SetActive(true);
            StartCoroutine(HideWrongAnswerTextAfterDelay(2f)); // 2�� �� �����
        }
    }

    // "Ʋ�Ƚ��ϴ�" �ؽ�Ʈ�� ���� �ð� �� ����� �ڷ�ƾ
    IEnumerator HideWrongAnswerTextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (wrongAnswerText != null)
        {
            wrongAnswerPanel.gameObject.SetActive(false);
            wrongAnswerText.gameObject.SetActive(false);
        }
    }

    // �����̸� ���� ������ ����
    void SpawnReward()
    {
        if (rewardPrefab != null)
        {
            Vector3 spawnPosition = rewardSpawnPoint != null ? rewardSpawnPoint.position : transform.position + Vector3.up * 1.5f;
            Instantiate(rewardPrefab, spawnPosition, Quaternion.identity);
            Debug.Log("������ �����Ǿ����ϴ�! ��ġ: " + spawnPosition);
        }
        else
        {
            Debug.LogWarning("���� �������� �������� �ʾҽ��ϴ�!");
        }
    }

    // ���� �� �Ҹ� ���
    void PlayCorrectSound()
    {
        if (audioSource != null && correctSound != null)
        {
            audioSource.PlayOneShot(correctSound);
        }
    }

    // å ���ͷ��� ��Ȱ��ȭ
    void DisableBookInteraction()
    {
        GetComponent<Collider>().enabled = false; // Ŭ�� ����
        if (hintText != null)
        {
            hintText.gameObject.SetActive(false); // "������" �ؽ�Ʈ ����
        }
        Debug.Log("å�� ��Ȱ��ȭ�Ǿ����ϴ�.");
    }
}


