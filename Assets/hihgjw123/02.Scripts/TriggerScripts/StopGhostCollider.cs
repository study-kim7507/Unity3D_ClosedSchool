using UnityEngine;

public class StopGhostCollider : MonoBehaviour
{

    public GhostSpawnCollider ghostSpawnCollider;


    private void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag("FollowGhost"))
        {
            Destroy(ghostSpawnCollider.ghost.gameObject);
           ghostSpawnCollider.isSpawned = false;
        }
    }
}
