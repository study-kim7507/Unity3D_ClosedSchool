using System.Collections;
using UnityEngine;

public class ExorcismPlace : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject exorcismCircle;
    [SerializeField] private GameObject interactionMessage;

    private AudioSource audioSource;
    private bool isDrawed = false;

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
        if (withItem != null && withItem.GetComponent<PaintBucket>() != null && !isDrawed)
        {
            exorcismCircle.SetActive(true);
            audioSource.PlayOneShot(audioSource.clip);
            withItem.GetComponent<PaintBucket>().Draw();
            StartCoroutine(AppearExorcismCircleCoroutine(exorcismCircle));

            isDrawed = true;
            interactionMessage.SetActive(false);
            Destroy(withItem);
            GetComponent<Collider>().enabled = false;
        }
        else if ((withItem == null || withItem.GetComponent<PaintBucket>() == null) && !isDrawed)
        {
            PlayerUI.instance.DisplayInteractionDescription("퇴마진을 그리기 위해서는 페인트가 필요할 것 같다.");
        }
    }

    private IEnumerator AppearExorcismCircleCoroutine(GameObject go)
    {
        Material material = go.GetComponentInChildren<Renderer>().material;
        
        Color initialColor = material.color;
        initialColor.a = 0.0f;
        material.color = initialColor;

        float duration = 5.0f;
        float elapsedTime = 0.0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime; 
            float alpha = Mathf.Clamp01(elapsedTime / duration);
            Color color = material.color;
            color.a = alpha;
            material.color = color;

            yield return null;
        }

        Color finalColor = material.color;
        finalColor.a = 1.0f;
        material.color = finalColor;
    }
}
