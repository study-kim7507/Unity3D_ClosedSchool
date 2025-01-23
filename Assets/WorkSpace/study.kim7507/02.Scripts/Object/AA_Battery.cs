using UnityEngine;

public class AA_Battery : MonoBehaviour, IConsumable
{ 
    public void Consume(PlayerController player)
    {
        player.flashlight.remainBattery = 100.0f;
    }
}
