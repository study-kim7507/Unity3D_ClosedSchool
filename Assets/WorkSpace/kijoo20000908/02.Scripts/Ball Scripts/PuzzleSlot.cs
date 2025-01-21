using UnityEngine;

public class PuzzleSlot : MonoBehaviour
{
    public int slotID; // 슬롯의 ID (1, 2, 3)
    private bool isCorrect = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Basketball")) // 태그는 "Basketball"로 통일
        {
            Ball ball = other.GetComponent<Ball>();
            if (ball != null && ball.ballID == slotID) // 공 ID와 슬롯 ID 비교
            {
                isCorrect = true;
                Debug.Log($"슬롯 {slotID}: 올바른 농구공이 배치되었습니다!");
            }
            else
            {
                Debug.Log($"슬롯 {slotID}: 잘못된 농구공이 배치되었습니다!");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Basketball")) // 태그는 "Basketball"로 통일
        {
            Ball ball = other.GetComponent<Ball>();
            if (ball != null && ball.ballID == slotID) // 공 ID와 슬롯 ID 비교
            {
                isCorrect = false;
                Debug.Log($"슬롯 {slotID}: 올바른 농구공이 제거되었습니다.");
            }
        }
    }

    public bool IsCorrect()
    {
        return isCorrect;
    }
}
