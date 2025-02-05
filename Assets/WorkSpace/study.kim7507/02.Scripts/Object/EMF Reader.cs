using UnityEngine;

public class EMFReader : MonoBehaviour
{
    // 1, 3, 5, 7, 9
    [SerializeField] Renderer[] lv;

    private AudioSource audioSource;
    private GameObject[] ghostObjects;

    private bool isInPlayerHand = false;
    
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // "LibraryGhost"와 "OneCorriDorGhost" 태그가 붙은 모든 오브젝트를 찾고 저장
        GameObject[] libraryGhosts = GameObject.FindGameObjectsWithTag("LibraryGhost");
        GameObject[] oneCorriDorGhosts = GameObject.FindGameObjectsWithTag("OneCorridorGhost");

        // 두 배열을 합쳐서 ghostObjects에 저장
        ghostObjects = new GameObject[libraryGhosts.Length + oneCorriDorGhosts.Length];
        libraryGhosts.CopyTo(ghostObjects, 0);
        oneCorriDorGhosts.CopyTo(ghostObjects, libraryGhosts.Length);
    }

    private void Update()
    {
        if (transform.parent != null) isInPlayerHand = true;
        else isInPlayerHand = false;


        Detect();
    }

    private void Detect()
    {
        float dist;
        FindClosestGhost(out dist);

        // 모든 머티리얼의 emission을 비활성화
        foreach (Renderer renderer in lv)
        {
            DisableEmission(renderer);
        }

        // 거리 범위에 따라 emission 활성화
        if (dist > 0 && dist <= 1)
        {
            EnableEmission(lv[0]);
            EnableEmission(lv[1]);
            EnableEmission(lv[2]);
            EnableEmission(lv[3]);
            EnableEmission(lv[4]);
            PlayBeepSound(1.0f);
        }
        else if (dist > 1 && dist <= 3)
        {
            EnableEmission(lv[0]);
            EnableEmission(lv[1]);
            EnableEmission(lv[2]);
            EnableEmission(lv[3]);
            PlayBeepSound(0.75f);
        }
        else if (dist > 3 && dist <= 5)
        {
            EnableEmission(lv[0]);
            EnableEmission(lv[1]);
            EnableEmission(lv[2]);
            PlayBeepSound(0.5f);
        }
        else if (dist > 5 && dist <= 7)
        {
            EnableEmission(lv[0]);
            EnableEmission(lv[1]);
            PlayBeepSound(0.25f);
        }
        else if (dist > 7 && dist <= 9)
        {
            EnableEmission(lv[0]);
            PlayBeepSound(0.1f);
        }
        else
        {
            StopBeepSound();
        }
    }

    private GameObject FindClosestGhost(out float dist)
    {
        GameObject closestGhost = null;

        dist = Mathf.Infinity;
        Vector3 curPos = transform.position;

        foreach (GameObject ghost in ghostObjects)
        {
            float distance = Vector3.Distance(curPos, ghost.transform.position);
            if (distance < dist)
            {
                dist = distance;
                closestGhost = ghost;
            }
        }

        return closestGhost;
    }

    private void EnableEmission(Renderer renderer)
    {
        if (renderer != null)
        {
            Material material = renderer.sharedMaterial;
            material.EnableKeyword("_EMISSION");
        }
    }

    private void DisableEmission(Renderer renderer)
    {
        if (renderer != null)
        {
            Material material = renderer.sharedMaterial;
            material.DisableKeyword("_EMISSION");
        }
    }

    private void PlayBeepSound(float pitch)
    {
        if (!isInPlayerHand) return;
        if (!audioSource.isPlaying)
        {
            audioSource.pitch = pitch; // 피치를 설정하여 속도 조절
            audioSource.Play(); // 비프 소리 재생
        }
    }

    private void StopBeepSound()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop(); // 비프 소리 중지
        }
    }
}
