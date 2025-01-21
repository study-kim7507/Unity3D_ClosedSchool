using UnityEngine;

public class PuzzleManager3 : MonoBehaviour
{
    public GameObject goalEffect; // �� ���� �� ǥ���� ȿ�� (��: ��ƼŬ)
    private bool isPuzzleSolved = false;

    // ���� ���¸� Ȯ��
    public void CompletePuzzle()
    {
        if (!isPuzzleSolved)
        {
            isPuzzleSolved = true;
            Debug.Log("������ Ǯ�Ƚ��ϴ�!");

            // �� ���� ȿ�� ����
            if (goalEffect != null)
            {
                Instantiate(goalEffect, transform.position, Quaternion.identity);
            }

            // �߰��� ���� �Ϸ� ������ �ۼ��ϼ��� (��: �� ����, ������ ���� ��)
        }
    }
}
