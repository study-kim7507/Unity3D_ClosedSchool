using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class BlinkImage : MonoBehaviour
{
    private Image targetImage;

    private void Start()
    {
        targetImage = GetComponent<Image>();
        StartCoroutine(BlinkIn());
    }

    private IEnumerator BlinkIn()
    {
        targetImage.enabled = true;
        while(true)
        {
            targetImage.enabled = !targetImage.IsActive();
            yield return new WaitForSeconds(1.5f);
        }
    }
}
