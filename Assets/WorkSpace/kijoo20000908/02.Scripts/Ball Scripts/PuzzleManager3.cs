using UnityEngine;

public class PuzzleManager3 : MonoBehaviour
{
    public GameObject rewardItemPrefab; // ���� ������ (��: ����)
    public Transform rewardSpawnPoint; // ���� ������ ���� ��ġ
    public GameObject goalEffect; // �� ���� �� ǥ���� ȿ�� (��: ��ƼŬ)

    private bool goal1Completed = false;
    private bool goal2Completed = false;
    private bool isPuzzleSolved = false;

    // ù ��° ��뿡 ���� ���� �� ȣ��
    public void Goal1Scored()
    {
        if (!goal1Completed)
        {
            goal1Completed = true;
            Debug.Log("ù ��° ��뿡 ���� �����ϴ�!");
            CheckPuzzleCompletion();
        }
    }

    // �� ��° ��뿡 ���� ���� �� ȣ��
    public void Goal2Scored()
    {
        if (!goal2Completed)
        {
            goal2Completed = true;
            Debug.Log("�� ��° ��뿡 ���� �����ϴ�!");
            CheckPuzzleCompletion();
        }
    }

    // �� ��뿡 ���� ������ üũ�Ͽ� ���� �Ϸ�
    private void CheckPuzzleCompletion()
    {
        if (goal1Completed && goal2Completed && !isPuzzleSolved)
        {
            isPuzzleSolved = true;
            Debug.Log("������ Ǯ�Ƚ��ϴ�! (���� ��뿡 ���� ��)");

            // �� ���� ȿ�� ����
            if (goalEffect != null)
            {
                Instantiate(goalEffect, transform.position, Quaternion.identity);
            }

            // ���� ����
            SpawnReward();
        }
    }

    // ������ Ư�� ��ġ�� ����
    private void SpawnReward()
    {
        if (rewardItemPrefab != null && rewardSpawnPoint != null)
        {
            Instantiate(rewardItemPrefab, rewardSpawnPoint.position, Quaternion.identity);
            Debug.Log("������ �����Ǿ����ϴ�!");
        }
        else
        {
            Debug.LogWarning("���� ������ ������ �Ǵ� ���� ��ġ�� �������� �ʾҽ��ϴ�!");
        }
    }
}
