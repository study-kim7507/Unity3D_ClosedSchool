using UnityEngine;

public class PlayerFlashlight : MonoBehaviour
{
    [SerializeField] private Light flashlightLight;
    private bool isFlashlightOn;                                    // 플래쉬라이트가 켜져있는지 여부를 저장

    public float remainBattery;                                     // 남은 배터리

    private AudioSource audioSource;
    private void Start()
    {
        flashlightLight.gameObject.SetActive(false);
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (isFlashlightOn)
        {
            remainBattery -= 0.01f;
            if (remainBattery <= 0.0f)
            {
                TurnOff();
            }
        }
    }

    public void ToggleFlashlight()
    {
        if (remainBattery > 0.0f && !isFlashlightOn) TurnOn();
        else if (isFlashlightOn) TurnOff();
    }

    private void TurnOn()
    {
        audioSource.Play();
        flashlightLight.gameObject.SetActive(true);
        isFlashlightOn = !isFlashlightOn;
    }

    public void TurnOff() 
    {
        audioSource.Play();
        flashlightLight.gameObject.SetActive(false);
        isFlashlightOn = !isFlashlightOn;
    }
}
