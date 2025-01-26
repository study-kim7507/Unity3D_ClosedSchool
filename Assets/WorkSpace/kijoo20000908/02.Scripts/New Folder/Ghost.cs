using UnityEngine;

public class Ghost : MonoBehaviour
{
    public void Vanish()
    {
        Debug.Log("귀신이 소멸합니다.");
        Destroy(gameObject, 2f); // 2초 후 오브젝트 제거
    }
}
