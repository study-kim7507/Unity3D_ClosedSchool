using UnityEngine;

public class AA_Battery : MonoBehaviour, IConsumable
{ 
    [SerializeField] private AudioClip consumeSound;
    public AudioClip ConsumeSound
    {
        get => consumeSound;
        set => consumeSound = value;
    }

    public void Consume(PlayerController player)
    {
        player.flashlight.remainBattery = 100.0f;
        PlayerUI.instance.DisplayInteractionDescription("������ ���͸��� ��ü�Ͽ���.\n��а� ���͸� ������ ���� �� ����.");
    }
}
