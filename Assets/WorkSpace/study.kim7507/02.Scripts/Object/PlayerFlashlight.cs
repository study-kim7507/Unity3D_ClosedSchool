using UnityEngine;

public class PlayerFlashlight : MonoBehaviour
{
    [SerializeField] private Material flashlightOffMaterial;        // �������� �������� �� ���͸���
    [SerializeField] private Material flashlightOnMaterial;         // �������� �������� �� ���͸���
    [SerializeField] private Light flashlightLight;
    
    private bool isFlashlightOn;                                    // �������� �����ִ��� ���θ� ����
    private Renderer flashlightRenderer;

    public float remainBattery;                                     // ���� ���͸�

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
