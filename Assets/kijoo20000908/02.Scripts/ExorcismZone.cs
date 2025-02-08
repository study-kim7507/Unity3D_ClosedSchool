using UnityEngine;

public class ExorcismZone : MonoBehaviour
{
    private int candlesCount = 0;
    private int photoCount = 0;
    private bool hasLighter = false;

    public GameObject exorcismEffect; // �� �Ϸ� ȿ��
    public GameObject magicCircle; // ������ ������Ʈ
    public int requiredCandles = 4; // �ʿ��� ���� ����
    public int requiredPhotos = 2; // �ʿ��� �ͽ� ���� ����

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("������ ������Ʈ: " + other.gameObject.name + ", �±�: " + other.tag);

        if (other.CompareTag("Candle"))
        {
            candlesCount++;
            Debug.Log("���� ������: " + candlesCount + "/" + requiredCandles);
        }
        else if (other.CompareTag("Lighter"))
        {
            hasLighter = true;
            Debug.Log("������ ������.");
        }
        else if (other.CompareTag("Photo"))
        {
            photoCount++;
            Debug.Log("�ͽ� ���� ������: " + photoCount + "/" + requiredPhotos);
        }

        CheckExorcismComplete();
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("������Ʈ ����: " + other.gameObject.name + ", �±�: " + other.tag);

        if (other.CompareTag("Candle"))
        {
            candlesCount--;
            if (candlesCount < 0) candlesCount = 0;
            Debug.Log("���� ���ŵ�: " + candlesCount + "/" + requiredCandles);
        }
        else if (other.CompareTag("Lighter"))
        {
            hasLighter = false;
            Debug.Log("������ ���ŵ�.");
        }
        else if (other.CompareTag("Photo"))
        {
            photoCount--;
            if (photoCount < 0) photoCount = 0;
            Debug.Log("�ͽ� ���� ���ŵ�: " + photoCount + "/" + requiredPhotos);
        }
    }

    private void CheckExorcismComplete()
    {
        if (candlesCount >= requiredCandles && photoCount >= requiredPhotos && hasLighter)
        {
            Debug.Log("�� ���� �Ϸ�! 2�� �� �������� ��� ���� �������� ������ϴ�.");

            if (exorcismEffect != null)
            {
                exorcismEffect.SetActive(true); // �� ȿ�� ����
            }

            // �������� ���� ������ ����
            Invoke("DestroyAllObjects", 2f);
        }
    }

    private void DestroyAllObjects()
    {
        // ������ ����
        if (magicCircle != null)
        {
            Destroy(magicCircle);
        }

        // ������ ���� �� ��� ���� �±� ������Ʈ ����
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

        // ���� �� ������ ����
        Destroy(gameObject);
    }
}
