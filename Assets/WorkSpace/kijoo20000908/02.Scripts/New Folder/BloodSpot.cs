using UnityEngine;

public class BloodSpot : MonoBehaviour
{
    public GameObject magicCirclePrefab; // ������ ������
    private bool isActivated = false; // �ߺ� ����

    private void OnTriggerEnter(Collider other)
    {
        if (!isActivated && other.CompareTag("BloodItem")) // �� ������ ����
        {
            SpawnMagicCircle();
            Destroy(other.gameObject); // �� ������ ����
        }
    }

    private void SpawnMagicCircle()
    {
        Instantiate(magicCirclePrefab, transform.position, Quaternion.Euler(90, 0, 0));
        isActivated = true; // �ߺ� ����
        Debug.Log("������ ���� �Ϸ�!");
    }
}
