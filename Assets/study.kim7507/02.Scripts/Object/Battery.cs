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
            PlayerUI.instance.DisplayInteractionDescription("손전등 배터리를 새걸로 교체하였다.\n당분간 배터리 걱정은 없을 것 같다.");
        }
        else
        {
            PlayerUI.instance.DisplayInteractionDescription("새 배터리는 아니지만 어느정도 손전등을 키고 유지할 수 있을 것 같다.");
        }
        
    }
}
