using UnityEngine;

public class Ghost : MonoBehaviour
{
    public void Vanish()
    {
        Debug.Log("�ͽ��� �Ҹ��մϴ�.");
        Destroy(gameObject, 2f); // 2�� �� ������Ʈ ����
    }
}
