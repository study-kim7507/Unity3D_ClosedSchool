using UnityEngine;

public class CandleManager2 : MonoBehaviour
{
    private int candlesPlaced = 0; // 배치된 양초 개수
    private int candlesLit = 0; // 불이 붙은 양초 개수
    public int requiredCandles = 4; // 필요한 양초 개수
    public GameObject magicCircleEffect; // 마법진 빛나는 효과

    private bool isCompleted = false; // 중복 실행 방지

    void Start()
    {
        candlesPlaced = 0;
        candlesLit = 0;
        Debug.Log("CandleManager 시작됨: 양초 배치 감지 대기 중...");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("감지된 오브젝트: " + other.gameObject.name + ", 태그: " + other.tag);

        if (other.CompareTag("Candle")) // 양초 배치 감지
        {
            candlesPlaced++;
            Debug.Log("양초 배치됨: " + candlesPlaced + "/" + requiredCandles);

            if (candlesPlaced > requiredCandles) candlesPlaced = requiredCandles;
        }
        else if (other.CompareTag("Lighter")) // 라이터로 불 붙이기 감지
        {
            if (candlesLit < candlesPlaced) // 배치된 양초 중 아직 불이 안 붙은 경우
            {
                candlesLit++;
                Debug.Log("양초에 불이 붙음: " + candlesLit + "/" + requiredCandles);
            }
        }

        CheckPuzzleCompletion();
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("오브젝트 나감: " + other.gameObject.name + ", 태그: " + other.tag);

        if (other.CompareTag("Candle"))
        {
            candlesPlaced--;
            if (candlesPlaced < 0) candlesPlaced = 0;
            Debug.Log("양초 제거됨: " + candlesPlaced + "/" + requiredCandles);
        }
    }

    private void CheckPuzzleCompletion()
    {
        if (!isCompleted && candlesPlaced == requiredCandles && candlesLit == requiredCandles)
        {
            Debug.Log("모든 양초가 불이 붙었습니다! 퇴마 퍼즐 다음 단계 진행 가능.");
            if (magicCircleEffect != null)
            {
                magicCircleEffect.SetActive(true); // 마법진 활성화 효과 실행
            }
            isCompleted = true; // 중복 실행 방지
        }
    }
}
