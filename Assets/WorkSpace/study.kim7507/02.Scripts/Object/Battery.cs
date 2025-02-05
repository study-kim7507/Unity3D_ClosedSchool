using UnityEngine;

public class Battery : MonoBehaviour, IConsumable
{ 
    [SerializeField] private AudioClip consumeSound;
    [SerializeField] private float capacity;
    public AudioClip ConsumeSound
    {
        get => consumeSound;
        set => consumeSound = value;
    }

    public void Consume(PlayerController player)
    {
        player.flashlight.remainBattery = Mathf.Clamp(player.flashlight.remainBattery + capacity, 0.0f, 100.0f);

        if (capacity == 100.0f)
        {
            PlayerUI.instance.DisplayInteractionDescription("������ ���͸��� ���ɷ� ��ü�Ͽ���.\n��а� ���͸� ������ ���� �� ����.");
        }
        else
        {
            PlayerUI.instance.DisplayInteractionDescription("�� ���͸��� �ƴ����� ������� �������� Ű�� ������ �� ���� �� ����.");
        }
        
    }
}
