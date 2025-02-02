using UnityEngine;

public class ExorcismPuzzle : MonoBehaviour
{
    private int candlesPlaced = 0; // ��ġ�� ���� ����
    private int candlesLit = 0; // ���� ���� ���� ����
    private bool photoPlaced = false; // ������ �߾ӿ� �������� ����
    public GameObject magicEffect; // �� �Ϸ� �� ȿ��

    void Start()
    {
        candlesPlaced = 0;
        candlesLit = 0;
        photoPlaced = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Candle")) // ���� ��ġ ����
        {
            candlesPlaced++;
            Debug.Log($"���� ��ġ��: {candlesPlaced}/4");
        }
        else if (other.CompareTag("Lighter")) // ������ ��� �� �� ���̱�
        {
            if (candlesPlaced > candlesLit) // ���� �� �� ���� ���ʰ� ������
            {
                candlesLit++;
                Debug.Log($"���ʿ� ���� ����: {candlesLit}/4");
            }
        }
        else if (other.CompareTag("Photo")) // �ͽ� ���� ����
        {
            photoPlaced = true;
            Debug.Log("�ͽ� ������ �߾ӿ� ����");
        }

        // ���� �Ϸ� ���� üũ
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
        Debug.Log("�� ���� �Ϸ�!");
        if (magicEffect != null)
        {
            magicEffect.SetActive(true); // ������ ȿ�� Ȱ��ȭ
        }
        // �ͽ� �Ҹ� �̺�Ʈ �� �߰� ����
    }
}
