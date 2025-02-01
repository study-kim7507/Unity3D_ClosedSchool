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
        PlayerUI.instance.DisplayInteractionDescription("손전등 배터리를 교체하였다.\n당분간 배터리 걱정은 없을 것 같다.");
    }
}
