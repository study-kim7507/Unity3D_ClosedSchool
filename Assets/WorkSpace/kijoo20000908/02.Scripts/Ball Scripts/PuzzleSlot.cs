using UnityEngine;

public class PuzzleSlot : MonoBehaviour
{
    public int slotID; // ������ ID (1, 2, 3)
    private bool isCorrect = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Basketball")) // �±״� "Basketball"�� ����
        {
            Ball ball = other.GetComponent<Ball>();
            if (ball != null && ball.ballID == slotID) // �� ID�� ���� ID ��
            {
                isCorrect = true;
                Debug.Log($"���� {slotID}: �ùٸ� �󱸰��� ��ġ�Ǿ����ϴ�!");
            }
            else
            {
                Debug.Log($"���� {slotID}: �߸��� �󱸰��� ��ġ�Ǿ����ϴ�!");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Basketball")) // �±״� "Basketball"�� ����
        {
            Ball ball = other.GetComponent<Ball>();
            if (ball != null && ball.ballID == slotID) // �� ID�� ���� ID ��
            {
                isCorrect = false;
                Debug.Log($"���� {slotID}: �ùٸ� �󱸰��� ���ŵǾ����ϴ�.");
            }
        }
    }

    public bool IsCorrect()
    {
        return isCorrect;
    }
}
