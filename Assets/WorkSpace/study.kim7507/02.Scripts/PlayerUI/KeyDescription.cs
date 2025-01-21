using System.Collections;
using TMPro;
using UnityEngine;

public class KeyDescription : MonoBehaviour
{
    [SerializeField] private TMP_Text targetText;
    [SerializeField] private float fadeOutDuration;

    void Update()
    {
        if (Timer.Instance.playTime >= 10.0f)
        {
            StartCoroutine(FadeOutCoroutine());
        }
    }

    private IEnumerator FadeOutCoroutine()
    {
        Color textColor = targetText.color;
        float startAlpha = textColor.a;


        for (float t = 0; t < fadeOutDuration; t += Time.deltaTime)
        {
            float normalizedTime = t / fadeOutDuration;
            textColor.a = Mathf.Lerp(startAlpha, 0, normalizedTime);
            targetText.color = textColor;
            yield return null;
        }

        textColor.a = 0;
        targetText.color = textColor;
        targetText.gameObject.SetActive(false);
    }
}
