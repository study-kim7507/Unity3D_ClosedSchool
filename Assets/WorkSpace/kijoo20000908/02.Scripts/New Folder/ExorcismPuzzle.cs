using UnityEngine;

public class ExorcismPuzzle : MonoBehaviour
{
    private int candlesPlaced = 0; // 배치된 양초 개수
    private int candlesLit = 0; // 불이 붙은 양초 개수
    private bool photoPlaced = false; // 사진이 중앙에 놓였는지 여부
    public GameObject magicEffect; // 퇴마 완료 시 효과

    void Start()
    {
        candlesPlaced = 0;
        candlesLit = 0;
        photoPlaced = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Candle")) // 양초 배치 감지
        {
            candlesPlaced++;
            Debug.Log($"양초 배치됨: {candlesPlaced}/4");
        }
        else if (other.CompareTag("Lighter")) // 라이터 사용 시 불 붙이기
        {
            if (candlesPlaced > candlesLit) // 아직 불 안 붙은 양초가 있으면
            {
                candlesLit++;
                Debug.Log($"양초에 불이 붙음: {candlesLit}/4");
            }
        }
        else if (other.CompareTag("Photo")) // 귀신 사진 감지
        {
            photoPlaced = true;
            Debug.Log("귀신 사진이 중앙에 놓임");
        }

        // 퍼즐 완료 조건 체크
        CheckExorcismComplete();
    }

    private void CheckExorcismComplete()
    {
        if (candlesPlaced == 4 && candlesLit == 4 && photoPlaced)
        {
            CompleteExorcism();
        }
    }

    private void CompleteExorcism()
    {
        Debug.Log("퇴마 퍼즐 완료!");
        if (magicEffect != null)
        {
            magicEffect.SetActive(true); // 마법진 효과 활성화
        }
        // 귀신 소멸 이벤트 등 추가 가능
    }
}
