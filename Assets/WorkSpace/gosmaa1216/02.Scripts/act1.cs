using UnityEngine;

public class act : MonoBehaviour
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
            mannequinAnimator.SetTrigger("Backbend");
        }
    }
}
