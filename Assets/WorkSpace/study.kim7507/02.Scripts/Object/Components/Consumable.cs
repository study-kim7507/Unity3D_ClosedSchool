using UnityEngine;

public interface IConsumable
{
    AudioClip ConsumeSound { get; set; }

    void Consume(PlayerController player);
}
