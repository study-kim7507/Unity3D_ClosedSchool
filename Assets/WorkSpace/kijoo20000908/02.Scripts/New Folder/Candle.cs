using UnityEngine;

public class Candle : MonoBehaviour
{
    private bool isLit = false;

    void OnMouseDown()
    {
        if (!isLit)
        {
            isLit = true;
            Debug.Log($"{gameObject.name}�� ���� �پ����ϴ�!");
            GetComponent<Renderer>().material.color = Color.red; // ���� ���� ť���� ���� ����
        }
    }

    public bool IsLit()
    {
        return isLit;
    }
}
