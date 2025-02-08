using UnityEngine;

public class ExorcismZone : MonoBehaviour
{
    private int candlesCount = 0;
    private int photoCount = 0;
    private bool hasLighter = false;

    public GameObject exorcismEffect; // 퇴마 완료 효과
    public GameObject magicCircle; // 마법진 오브젝트
    public int requiredCandles = 4; // 필요한 양초 개수
    public int requiredPhotos = 2; // 필요한 귀신 사진 개수

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("감지된 오브젝트: " + other.gameObject.name + ", 태그: " + other.tag);

        if (other.CompareTag("Candle"))
        {
            candlesCount++;
            Debug.Log("양초 감지됨: " + candlesCount + "/" + requiredCandles);
        }
        else if (other.CompareTag("Lighter"))
        {
            hasLighter = true;
            Debug.Log("라이터 감지됨.");
        }
        else if (other.CompareTag("Photo"))
        {
            photoCount++;
            Debug.Log("귀신 사진 감지됨: " + photoCount + "/" + requiredPhotos);
        }

        CheckExorcismComplete();
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("오브젝트 나감: " + other.gameObject.name + ", 태그: " + other.tag);

        if (other.CompareTag("Candle"))
        {
            candlesCount--;
            if (candlesCount < 0) candlesCount = 0;
            Debug.Log("양초 제거됨: " + candlesCount + "/" + requiredCandles);
        }
        else if (other.CompareTag("Lighter"))
        {
            hasLighter = false;
            Debug.Log("라이터 제거됨.");
        }
        else if (other.CompareTag("Photo"))
        {
            photoCount--;
            if (photoCount < 0) photoCount = 0;
            Debug.Log("귀신 사진 제거됨: " + photoCount + "/" + requiredPhotos);
        }
    }

    private void CheckExorcismComplete()
    {
        if (candlesCount >= requiredCandles && photoCount >= requiredPhotos && hasLighter)
        {
            Debug.Log("퇴마 퍼즐 완료! 2초 후 마법진과 모든 관련 아이템이 사라집니다.");

            if (exorcismEffect != null)
            {
                exorcismEffect.SetActive(true); // 퇴마 효과 실행
            }

            // 마법진과 관련 아이템 삭제
            Invoke("DestroyAllObjects", 2f);
        }
    }

    private void DestroyAllObjects()
    {
        // 마법진 삭제
        if (magicCircle != null)
        {
            Destroy(magicCircle);
        }

        // 마법진 영역 내 모든 관련 태그 오브젝트 삭제
        GameObject[] candles = GameObject.FindGameObjectsWithTag("Candle");
        GameObject[] photos = GameObject.FindGameObjectsWithTag("Photo");
        GameObject[] lighters = GameObject.FindGameObjectsWithTag("Lighter");

        foreach (GameObject candle in candles)
        {
            Destroy(candle);
        }
        foreach (GameObject photo in photos)
        {
            Destroy(photo);
        }
        foreach (GameObject lighter in lighters)
        {
            Destroy(lighter);
        }

        // 현재 퇴마 영역도 삭제
        Destroy(gameObject);
    }
}
