using UnityEngine;

public class BloodItem : MonoBehaviour
{
    public GameObject magicCirclePrefab; // 마법진 프리팹
    public Transform spawnPoint; // 마법진 생성 위치
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
        Debug.Log("빨간 피 사용됨: 마법진이 생성되었습니다.");
        Destroy(gameObject); // 아이템 사용 후 삭제
    }
}
