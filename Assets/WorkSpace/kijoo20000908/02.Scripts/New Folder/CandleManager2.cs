using UnityEngine;

public class CandleManager2 : MonoBehaviour
{
    private int candlesPlaced = 0; // ��ġ�� ���� ����
    private int candlesLit = 0; // ���� ���� ���� ����
    public int requiredCandles = 4; // �ʿ��� ���� ����
    public GameObject magicCircleEffect; // ������ ������ ȿ��

    private bool isCompleted = false; // �ߺ� ���� ����

    void Start()
    {
        candlesPlaced = 0;
        candlesLit = 0;
        Debug.Log("CandleManager ���۵�: ���� ��ġ ���� ��� ��...");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("������ ������Ʈ: " + other.gameObject.name + ", �±�: " + other.tag);

        if (other.CompareTag("Candle")) // ���� ��ġ ����
        {
            candlesPlaced++;
            Debug.Log("���� ��ġ��: " + candlesPlaced + "/" + requiredCandles);

            if (candlesPlaced > requiredCandles) candlesPlaced = requiredCandles;
        }
        else if (other.CompareTag("Lighter")) // �����ͷ� �� ���̱� ����
        {
            if (candlesLit < candlesPlaced) // ��ġ�� ���� �� ���� ���� �� ���� ���
            {
                candlesLit++;
                Debug.Log("���ʿ� ���� ����: " + candlesLit + "/" + requiredCandles);
            }
        }

        CheckPuzzleCompletion();
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("������Ʈ ����: " + other.gameObject.name + ", �±�: " + other.tag);

        if (other.CompareTag("Candle"))
        {
            candlesPlaced--;
            if (candlesPlaced < 0) candlesPlaced = 0;
            Debug.Log("���� ���ŵ�: " + candlesPlaced + "/" + requiredCandles);
        }
    }

    private void CheckPuzzleCompletion()
    {
        if (!isCompleted && candlesPlaced == requiredCandles && candlesLit == requiredCandles)
        {
            Debug.Log("��� ���ʰ� ���� �پ����ϴ�! �� ���� ���� �ܰ� ���� ����.");
            if (magicCircleEffect != null)
            {
                magicCircleEffect.SetActive(true); // ������ Ȱ��ȭ ȿ�� ����
            }
            isCompleted = true; // �ߺ� ���� ����
        }
    }
}
