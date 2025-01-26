using UnityEngine;

public class Pepsi : MonoBehaviour, IConsumable
{
    public void Consume(PlayerController player)
    {
        player.stamina = 100.0f;
    }
}
