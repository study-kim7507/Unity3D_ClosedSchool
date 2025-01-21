using UnityEngine;

public class GoalTrigger : MonoBehaviour
{
    public PuzzleManager3 puzzleManager3; // ���� �Ŵ����� ����

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Basketball")) // ���� �±� Ȯ��
        {
            Debug.Log("�󱸰��� ��뿡 �����ϴ�!");
            puzzleManager3.CompletePuzzle(); // ���� �Ϸ� ȣ��
        }
    }
}
