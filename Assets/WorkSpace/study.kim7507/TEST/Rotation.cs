using UnityEngine;

public class Rotation : MonoBehaviour
{
    public float rotationSpeed = 100f; // 회전 속도

    void Update()
    {
        // 매 프레임마다 Y축을 기준으로 회전
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
