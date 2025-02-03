using UnityEngine;

public class act4 : MonoBehaviour
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
            mannequinAnimator.SetTrigger("Wall");
            audioSource.Play();
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }
}
