using UnityEngine;

public class CandleManager2 : MonoBehaviour
{
    private int candlesPlaced = 0; // 배치된 양초 개수
    private int candlesLit = 0; // 불이 붙은 양초 개수
    public GameObject magicCircleEffect; // 마법진 빛나는 효과

    void Start()
    {
        candlesPlaced = 0;
        candlesLit = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Candle")) // 양초가 배치될 때 감지
        {
            candlesPlaced++;
            Debug.Log($"양초 배치됨: {candlesPlaced}/4");
        }
        else if (other.CompareTag("Lighter")) // 라이터로 불을 붙일 때 감지
        {
            if (candlesPlaced > candlesLit) // 아직 불 안 붙은 양초가 있으면
            {
                candlesLit++;
                Debug.Log($"양초에 불이 붙음: {candlesLit}/4");
            }
        }

        // 모든 양초가 배치되고 불이 붙었는지 체크
        CheckCandlesComplete();
    }

    private void CheckCandlesComplete()
    {
        if (candlesPlaced == 4 && candlesLit == 4)
        {
            Debug.Log("모든 양초에 불이 붙었습니다! 퇴마 퍼즐 다음 단계 진행 가능.");
            if (magicCircleEffect != null)
            {
                magicCircleEffect.SetActive(true); // 마법진 활성화 효과 실행
            }
        }
    }
}
