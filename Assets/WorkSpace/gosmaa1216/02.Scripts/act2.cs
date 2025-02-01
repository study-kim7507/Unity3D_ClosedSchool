using UnityEngine;

public class act2 : MonoBehaviour
{
    Animator mannequinAnimator;

    void Start()
    {
        mannequinAnimator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            mannequinAnimator.SetTrigger("Stand");
        }
    }
}
