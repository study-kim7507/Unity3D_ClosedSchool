using UnityEngine;

public class Pepsi : MonoBehaviour, IConsumable
{
    [SerializeField] private AudioClip consumeSound;

    public AudioClip ConsumeSound 
    { 
        get => consumeSound; 
        set => consumeSound = value; 
    }

    public void Consume(PlayerController player)
    {
        player.stamina += 30.0f;
        PlayerUI.instance.DisplayInteractionDescription("���¹̳��� ȸ���Ǿ���.\n�ٽ� �޸� �� ���� �� ����.");
    }
}
