using UnityEngine;

public class Candle : MonoBehaviour
{
    private bool isLit = false;

    void OnMouseDown()
    {
        if (!isLit)
        {
            isLit = true;
            Debug.Log($"{gameObject.name}에 불이 붙었습니다!");
            GetComponent<Renderer>().material.color = Color.red; // 불이 붙은 큐브의 색상 변경
        }
    }

    public bool IsLit()
    {
        return isLit;
    }
}
