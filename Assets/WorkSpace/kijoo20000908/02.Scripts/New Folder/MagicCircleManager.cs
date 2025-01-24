using UnityEngine;

public class MagicCircleManager : MonoBehaviour
{
    [SerializeField] private Ghost ghost;

    public Candle[] candles; // ���� �迭
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
            if (!candle.IsLit()) // ���� �� �ϳ��� ���� �� �پ����� false
                return false;
        }
        return true; // ��� ���ʿ� ���� �پ����� true
    }

    private void ActivateMagicCircle()
    {
        Debug.Log("������ �ذ�Ǿ����ϴ�! �������� Ȱ��ȭ�˴ϴ�.");
        // ���⿡ ������ ȿ�� �߰�

        if (ghost != null)
        {
            ghost.Vanish();
        }
    }
}
