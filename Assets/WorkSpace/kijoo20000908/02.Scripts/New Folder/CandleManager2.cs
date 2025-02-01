using UnityEngine;

public class CandleManager2 : MonoBehaviour
{
    private int candlesPlaced = 0; // ��ġ�� ���� ����
    private int candlesLit = 0; // ���� ���� ���� ����
    public GameObject magicCircleEffect; // ������ ������ ȿ��

    void Start()
    {
        candlesPlaced = 0;
        candlesLit = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Candle")) // ���ʰ� ��ġ�� �� ����
        {
            candlesPlaced++;
            Debug.Log($"���� ��ġ��: {candlesPlaced}/4");
        }
        else if (other.CompareTag("Lighter")) // �����ͷ� ���� ���� �� ����
        {
            if (candlesPlaced > candlesLit) // ���� �� �� ���� ���ʰ� ������
            {
                candlesLit++;
                Debug.Log($"���ʿ� ���� ����: {candlesLit}/4");
            }
        }

        // ��� ���ʰ� ��ġ�ǰ� ���� �پ����� üũ
        CheckCandlesComplete();
    }

    private void CheckCandlesComplete()
    {
        if (candlesPlaced == 4 && candlesLit == 4)
        {
            Debug.Log("��� ���ʿ� ���� �پ����ϴ�! �� ���� ���� �ܰ� ���� ����.");
            if (magicCircleEffect != null)
            {
                magicCircleEffect.SetActive(true); // ������ Ȱ��ȭ ȿ�� ����
            }
        }
    }
}
