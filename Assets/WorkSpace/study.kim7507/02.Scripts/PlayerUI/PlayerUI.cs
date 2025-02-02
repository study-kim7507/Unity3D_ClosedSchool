using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public static PlayerUI instance = null;

    public GameObject playerUIPanel;
    public GameObject playerDiePanel;

    public TMP_Text keyDescription;
    public Image background;
    public Image crosshair;
    public Image recImage;
    public TMP_Text timer;
    public Image flashImage;
    public TMP_Text interactionDescription;
    public Image flashlightBattery;
    public Image stamina;

    private float playTime = 0.0f;
    private Coroutine interactionDescriptionCoroutine;

    public PlayerController ownerPlayer;

    private void Start()
    {
        if (instance == null) instance = this;
        else Destroy(this.gameObject);
        
        StartCoroutine(BlinkInRecImage());
    }

    private void Update()
    {
        UpdateTimer();
        UpdateFlashlightBattery();
        UpdatePlayerStamina();

        // 30초가 지나면 키설명 창이 없어지도록
        if (playTime >= 30.0f && keyDescription.gameObject.activeSelf)
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

        // 지금까지 플레이타임을 시, 분, 초, 밀리초로 변환
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

    // 플레이어가 사진을 찍어 번쩍이는 효과를 내는 함수
    public void PlayerTakePhoto()
    {
        if (interactionDescriptionCoroutine != null)
            StopCoroutine(interactionDescriptionCoroutine);
        interactionDescriptionCoroutine = StartCoroutine(DisplayInteractionDescriptionCoroutine("사진을 찍었습니다."));
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
        // 페이드 인
        float elapsedTime = 0.0f;
        while (elapsedTime < 0.15f)
        {
            float alpha = Mathf.Clamp01(elapsedTime / 0.15f);
            flashImage.color = new Color(1, 1, 1, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 페이드 아웃
        elapsedTime = 0.0f;
        while (elapsedTime < 0.25f)
        {
            float alpha = Mathf.Clamp01(1 - (elapsedTime / 0.25f));
            flashImage.color = new Color(1, 1, 1, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 마지막으로 완전히 투명하게 설정
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

        // 페이드 아웃
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

    private void UpdateFlashlightBattery()
    {
        if (ownerPlayer.flashlight.remainBattery >= 70) flashlightBattery.color = new Color(0, 1, 0);
        else if (ownerPlayer.flashlight.remainBattery >= 30) flashlightBattery.color = new Color(1, 1, 0);
        else if (ownerPlayer.flashlight.remainBattery >= 0) flashlightBattery.color = new Color(1, 0, 0);

        flashlightBattery.fillAmount = ownerPlayer.flashlight.remainBattery / 100.0f;
    }

    private void UpdatePlayerStamina()
    {
        if (ownerPlayer.stamina >= 70) stamina.color = new Color(0, 1, 0);
        else if (ownerPlayer.stamina >= 30) stamina.color = new Color(1, 1, 0);
        else if (ownerPlayer.stamina >= 0) stamina.color = new Color(1, 0, 0);

        stamina.fillAmount = ownerPlayer.stamina / 100.0f;
    }

    public void PlayerDie()
    {
        playerDiePanel.SetActive(true);
        StartCoroutine(PlayerDiePanelActiveCoroutine());
    }

    private IEnumerator PlayerDiePanelActiveCoroutine()
    {
        float duration = 5f; 
        float elapsedTime = 0f; 
        Image image = playerDiePanel.GetComponent<Image>(); 

        Color color = image.color; 
        color.a = 0; 
        image.color = color; 

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime; 
            float alpha = Mathf.Clamp01(elapsedTime / duration); 
            color.a = alpha; 
            image.color = color; 
            yield return null; 
        }

        color.a = 1; 
        image.color = color; // 최종 색상 적용
    }
}
