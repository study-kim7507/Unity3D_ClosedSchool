using UnityEngine;

public class act : MonoBehaviour
{
    Animator mannequinAnimator;
    AudioSource audioSource;
    void Start()
    {
        mannequinAnimator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            mannequinAnimator.SetTrigger("Backbend");
            audioSource.Play();
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }
}
