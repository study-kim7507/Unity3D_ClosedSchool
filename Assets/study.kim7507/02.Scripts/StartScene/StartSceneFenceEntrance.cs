using UnityEngine;

public class StartSceneFenceEntrance : MonoBehaviour
{
    private AudioSource audioSource;
    private Animation animation;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animation = GetComponent<Animation>();
    }
    public void OpenDoor()
    {
        if (audioSource == null || animation == null) return; 

        audioSource.PlayOneShot(audioSource.clip);
        animation.Play();
    }
}
