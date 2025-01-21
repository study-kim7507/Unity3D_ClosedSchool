using UnityEngine;

public class PuzzleManager2 : MonoBehaviour
{
    public PuzzleSlot[] slots; // ���� ���� �迭

    [SerializeField] private GameObject rewardItemPrefab;  // ���� ������(���� �Ǵ� �ܼ�)
    [SerializeField] private Transform itemSpawnPoint;    // ������ ���� ��ġ

    private bool isPuzzleSolved = false;

    private void Update()
    {
        if (!isPuzzleSolved && CheckPuzzleSolved())
        {
            isPuzzleSolved = true;
            OnPuzzleSolved();
        }
    }

    // ������ Ǯ�ȴ��� Ȯ��
    private bool CheckPuzzleSolved()
    {
        foreach (PuzzleSlot slot in slots)
        {
            if (!slot.IsCorrect())
                return false;
        }
        return true;
    }

    // ���� ���� �� ������ ����
    private void OnPuzzleSolved()
    {
        Debug.Log("Puzzle Solved! (Managed by PuzzleManager2)");
        RewardPlayer(); // ���� ����
    }

    // ���� ����
    private void RewardPlayer()
    {
        if (rewardItemPrefab != null && itemSpawnPoint != null)
        {
            Instantiate(rewardItemPrefab, itemSpawnPoint.position, Quaternion.identity);
            Debug.Log("�������� ȹ���߽��ϴ�!");
        }
        else
        {
            Debug.LogWarning("���� ������ ������ �Ǵ� ���� ��ġ�� �������� �ʾҽ��ϴ�!");
        }
    }
}
