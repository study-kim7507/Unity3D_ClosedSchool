using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public GameObject playerUIPanel;

    public TMP_Text keyDescription;
    public Image background;
    public Image crosshair;
    public Image recImage;
    public TMP_Text timer;
    public Image flashImage;
    public TMP_Text interactionDescription;

    private float playTime = 0.0f;
    private Coroutine interactionDescriptionCoroutine;

    private void Start()
    {
        StartCoroutine(BlinkInRecImage());
    }

    private void Update()
    {
        UpdateTimer();

        // 10�ʰ� ������ Ű���� â�� ����������
        if (playTime >= 10.0f && keyDescription.gameObject.activeSelf)
            StartCoroutine(FadeOutKeyDescription());
    }

    private IEnumerator BlinkInRecImage()
    {
        recImage.enabled = true;
        while (true)
        {
            recImage.enabled = !recImage.IsActive();
            yield return new WaitForSeconds(1.5f);
        }
    }

    private void UpdateTimer()
    {
        playTime += Time.deltaTime;

        // ���ݱ��� �÷���Ÿ���� ��, ��, ��, �и��ʷ� ��ȯ
        int hours = (int)(playTime / 3600);
        int minutes = (int)((playTime % 3600) / 60);
        int seconds = (int)(playTime % 60);

        timer.text = string.Format("{0:D2} : {1:D2} : {2:D2}", hours, minutes, seconds);
    }

    private IEnumerator FadeOutKeyDescription()
    {
        Color textColor = keyDescription.color;
        float startAlpha = textColor.a;

        for (float t = 0; t < 3.0f; t += Time.deltaTime)
        {
            float normalizedTime = t / 3.0f;
            textColor.a = Mathf.Lerp(startAlpha, 0, normalizedTime);
            keyDescription.color = textColor;
            yield return null;
        }

        textColor.a = 0;
        keyDescription.color = textColor;
        keyDescription.gameObject.SetActive(false);
    }

    // �÷��̾ ������ ��� ��½�̴� ȿ���� ���� �Լ�
    public void PlayerTakePhoto()
    {
        if (interactionDescriptionCoroutine != null)
            StopCoroutine(interactionDescriptionCoroutine);
        interactionDescriptionCoroutine = StartCoroutine(DisplayInteractionDescriptionCoroutine("������ ������ϴ�."));
        StartCoroutine(FlashCoroutine());
    }

    public void DisplayInteractionDescription(string description)
    {
        if (interactionDescriptionCoroutine != null)
            StopCoroutine(interactionDescriptionCoroutine);
        interactionDescriptionCoroutine = StartCoroutine(DisplayInteractionDescriptionCoroutine(description));
    }

    private IEnumerator FlashCoroutine()
    {
        // ���̵� ��
        float elapsedTime = 0.0f;
        while (elapsedTime < 0.15f)
        {
            float alpha = Mathf.Clamp01(elapsedTime / 0.15f);
            flashImage.color = new Color(1, 1, 1, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // ���̵� �ƿ�
        elapsedTime = 0.0f;
        while (elapsedTime < 0.25f)
        {
            float alpha = Mathf.Clamp01(1 - (elapsedTime / 0.25f));
            flashImage.color = new Color(1, 1, 1, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // ���������� ������ �����ϰ� ����
        flashImage.color = new Color(1, 1, 1, 0);
    }

    private IEnumerator DisplayInteractionDescriptionCoroutine(string description)
    {
        interactionDescription.gameObject.SetActive(true);
        interactionDescription.text = description;

        float elapsedTime = 0.0f;
        while (elapsedTime < 2.0f)
        {
            float alpha = Mathf.Clamp01(elapsedTime / 2.0f);
            interactionDescription.color = new Color(1, 1, 1, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // ���̵� �ƿ�
        elapsedTime = 0.0f;
        while (elapsedTime < 2.0f)
        {
            float alpha = Mathf.Clamp01(1 - (elapsedTime / 2.0f));
            interactionDescription.color = new Color(1, 1, 1, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        interactionDescription.gameObject.SetActive(false);
        interactionDescription.text = "";
    }
}
