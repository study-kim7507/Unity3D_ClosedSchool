using UnityEngine;

public class act3 : MonoBehaviour
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
            mannequinAnimator.SetTrigger("Run");
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }
}
