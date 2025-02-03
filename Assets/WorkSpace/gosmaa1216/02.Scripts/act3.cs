using UnityEngine;

public class act3 : MonoBehaviour
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
            mannequinAnimator.SetTrigger("Run");
            audioSource.Play();
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }
}
