using UnityEngine;

public class StopGhostCollider : MonoBehaviour
{
    GhostSpawnCollider ghostSpawnCollider;

    private void OnTriggerEnter(Collider other) {


        if(other.CompareTag("FollowGhost"))
        {
            Destroy(ghostSpawnCollider.ghost.gameObject);
        }
    }
}
