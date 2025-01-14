using UnityEngine;

public class PlayerFlashlight : MonoBehaviour
{
    [SerializeField] private Material flashlightOffMaterial;        // 손전등이 꺼져있을 때 머터리얼
    [SerializeField] private Material flashlightOnMaterial;         // 손전등이 켜져있을 때 머터리얼
    [SerializeField] private Light flashlightLight;
    
    private bool isFlashlightOn;                                    // 손전등이 켜져있는지 여부를 저장
    private Renderer flashlightRenderer;

    public float remainBattery;                                     // 남은 배터리

    private void Start()
    {
        flashlightRenderer = GetComponent<Renderer>();
        flashlightLight.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (isFlashlightOn)
        {
            remainBattery -= 0.05f;
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
        flashlightRenderer.material = flashlightOnMaterial;
        flashlightLight.gameObject.SetActive(true);
        isFlashlightOn = !isFlashlightOn;
    }

    public void TurnOff() 
    {
        flashlightRenderer.material = flashlightOffMaterial;
        flashlightLight.gameObject.SetActive(false);
        isFlashlightOn = !isFlashlightOn;
    }
}
