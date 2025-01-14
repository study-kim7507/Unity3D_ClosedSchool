using UnityEngine;

public class PlayerFlashlight : MonoBehaviour
{
    [SerializeField] private Light flashlightLight;
    private bool isFlashlightOn;                                    // �÷�������Ʈ�� �����ִ��� ���θ� ����

    public float remainBattery;                                     // ���� ���͸�

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
