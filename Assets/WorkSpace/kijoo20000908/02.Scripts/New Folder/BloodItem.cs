using UnityEngine;

public class BloodItem : MonoBehaviour
{
    public GameObject magicCirclePrefab; // ������ ������
    public Transform spawnPoint; // ������ ���� ��ġ
    private bool isUsed = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !isUsed)
        {
            PlaceMagicCircle();
        }
    }

    private void PlaceMagicCircle()
    {
        Instantiate(magicCirclePrefab, spawnPoint.position, Quaternion.Euler(90, 0, 0));
        isUsed = true;
        Debug.Log("���� �� ����: �������� �����Ǿ����ϴ�.");
        Destroy(gameObject); // ������ ��� �� ����
    }
}
