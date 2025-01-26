using UnityEngine;

public class MagicCircleManager : MonoBehaviour
{
    [SerializeField] private Ghost ghost;

    public Candle[] candles; // 양초 배열
    private bool isPuzzleSolved = false;

    void Update()
    {
        if (!isPuzzleSolved && AllCandlesLit())
        {
            isPuzzleSolved = true;
            ActivateMagicCircle();
        }
    }

    private bool AllCandlesLit()
    {
        foreach (Candle candle in candles)
        {
            if (!candle.IsLit()) // 양초 중 하나라도 불이 안 붙었으면 false
                return false;
        }
        return true; // 모든 양초에 불이 붙었으면 true
    }

    private void ActivateMagicCircle()
    {
        Debug.Log("퍼즐이 해결되었습니다! 마법진이 활성화됩니다.");
        // 여기에 마법진 효과 추가

        if (ghost != null)
        {
            ghost.Vanish();
        }
    }
}
