using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public void CheckPuzzleCompletion(int currentBookCount, int requiredBookCount)
    {
        if (currentBookCount >= requiredBookCount) // 4���� å�� ��� ���Դ��� üũ
        {
            CompletePuzzle();
        }
        else
        {
            Debug.Log($"���� å�� �����մϴ�. ({currentBookCount}/{requiredBookCount})");
        }
    }

    private void CompletePuzzle()
    {
        Debug.Log("������ �Ϸ�Ǿ����ϴ�! ��� å�� �ùٸ� ��ġ�� �������ϴ�.");
        // �� ����, ȿ�� �߻� ���� �߰� ��� ����
    }
}
