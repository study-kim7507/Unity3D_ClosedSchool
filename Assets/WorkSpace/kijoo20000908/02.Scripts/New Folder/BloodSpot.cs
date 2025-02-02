using UnityEngine;

public class BloodSpot : MonoBehaviour
{
    public GameObject magicCirclePrefab; // 마법진 프리팹
    private bool isActivated = false; // 중복 방지

    private void OnTriggerEnter(Collider other)
    {
        if (!isActivated && other.CompareTag("BloodItem")) // 피 아이템 감지
        {
            SpawnMagicCircle();
            Destroy(other.gameObject); // 피 아이템 제거
        }
    }

    private void SpawnMagicCircle()
    {
        Instantiate(magicCirclePrefab, transform.position, Quaternion.Euler(90, 0, 0));
        isActivated = true; // 중복 방지
        Debug.Log("마법진 생성 완료!");
    }
}
