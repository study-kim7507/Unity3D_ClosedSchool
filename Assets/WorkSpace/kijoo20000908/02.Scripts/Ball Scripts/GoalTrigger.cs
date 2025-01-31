using UnityEngine;

public class GoalTrigger : MonoBehaviour
{
    public PuzzleManager3 puzzleManager3;
    public int goalID; // 1: ù ��° ���, 2: �� ��° ���

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Basketball")) // ���� �±� Ȯ��
        {
            Debug.Log($"�󱸰��� ��� {goalID}�� �����ϴ�!");

            if (puzzleManager3 != null)
            {
                if (goalID == 1)
                    puzzleManager3.Goal1Scored();
                else if (goalID == 2)
                    puzzleManager3.Goal2Scored();
            }
        }
    }
}
