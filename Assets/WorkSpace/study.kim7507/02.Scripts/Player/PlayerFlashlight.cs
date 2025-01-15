using UnityEngine;

public class PlayerFlashlight : MonoBehaviour
{
    [SerializeField] private Light flashlightLight;
    private bool isFlashlightOn;                                    // 플래쉬라이트가 켜져있는지 여부를 저장

    public float remainBattery;                                     // 남은 배터리

    private void Start()
    {
        flashlightLight.gameObject.SetActive(false);
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
        flashlightLight.gameObject.SetActive(true);
        isFlashlightOn = !isFlashlightOn;
    }

    public void TurnOff() 
    {
        flashlightLight.gameObject.SetActive(false);
        isFlashlightOn = !isFlashlightOn;
    }
}
