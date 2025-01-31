using UnityEngine;

public class PuzzleManager2 : MonoBehaviour
{
    public GameObject rewardItemPrefab; // ���� ������
    public Transform rewardSpawnPoint; // ���� ���� ��ġ
    private bool isPuzzleSolved = false;

    public void CompletePuzzle()
    {
        if (!isPuzzleSolved)
        {
            isPuzzleSolved = true;
            Debug.Log("�󱸰� ���� �Ϸ�! ������ �����˴ϴ�.");

            // ���� ����
            if (rewardItemPrefab != null && rewardSpawnPoint != null)
            {
                Instantiate(rewardItemPrefab, rewardSpawnPoint.position, Quaternion.identity);
                Debug.Log("������ �����Ǿ����ϴ�!");
            }
            else
            {
                Debug.LogWarning("���� ������ �Ǵ� ��ġ�� �������� �ʾҽ��ϴ�!");
            }
        }
    }
}
