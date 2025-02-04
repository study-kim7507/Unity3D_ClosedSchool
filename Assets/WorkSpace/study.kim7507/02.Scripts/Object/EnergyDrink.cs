using UnityEngine;

public class EnergyDrink : MonoBehaviour, IConsumable
{
    [SerializeField] private AudioClip consumeSound;

    public AudioClip ConsumeSound
    {
        get => consumeSound;
        set => consumeSound = value;
    }

    public void Consume(PlayerController player)
    {
        player.stamina += 100.0f;
        PlayerUI.instance.DisplayInteractionDescription("스태미나가 회복되었다.\n다시 달릴 수 있을 것 같다.");
    }
}
