using UnityEngine;

public class Cigarette_Lighter: MonoBehaviour, IInteractable
{
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void BeginFocus(GameObject withItem = null)
    {

    }


    public void EndFocus(GameObject withItem = null)
    {

    }

    public void Interact(GameObject withItem = null)
    {
        
    }

    public void Fire()
    {
        audioSource.PlayOneShot(audioSource.clip);
        PlayerUI.instance.DisplayInteractionDescription("양초에 불이 붙었다.");
    }
}
