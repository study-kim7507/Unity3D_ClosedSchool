using UnityEngine;

public class PuzzleManager3 : MonoBehaviour
{
    public GameObject goalEffect; // �� ���� �� ǥ���� ȿ�� (��: ��ƼŬ)
    private bool goal1Completed = false;
    private bool goal2Completed = false;
    private bool isPuzzleSolved = false;

    // ��� 1�� ���� ���� �� ȣ��
    public void Goal1Scored()
    {
        if (!goal1Completed)
        {
            goal1Completed = true;
            Debug.Log("ù ��° ��뿡 ���� �����ϴ�!");
            CheckPuzzleCompletion();
        }
    }

    // ��� 2�� ���� ���� �� ȣ��
    public void Goal2Scored()
    {
        if (!goal2Completed)
        {
            goal2Completed = true;
            Debug.Log("�� ��° ��뿡 ���� �����ϴ�!");
            CheckPuzzleCompletion();
        }
    }

    // �� ��뿡 ��� ���� ���� ���� �Ϸ�
    private void CheckPuzzleCompletion()
    {
        if (goal1Completed && goal2Completed && !isPuzzleSolved)
        {
            isPuzzleSolved = true;
            Debug.Log("������ Ǯ�Ƚ��ϴ�! (���� ��뿡 ���� ��)");

            // ���� �Ϸ� ȿ��
            if (goalEffect != null)
            {
                Instantiate(goalEffect, transform.position, Quaternion.identity);
            }

            // ���� �Ϸ� �� �߰� ���� (��: �� ����, ������ ���� ��) ����
        }
    }
}
