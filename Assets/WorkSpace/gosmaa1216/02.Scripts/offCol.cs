using UnityEngine;

public class offCol : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameObject.GetComponent<BoxCollider>().enabled = false;
    }

    
}
