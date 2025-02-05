using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExorcismCircle : MonoBehaviour
{
    [SerializeField] ObjectPlacement[] candle;
    [SerializeField] ObjectPlacement photo;

    private float fireElapsedTime = 0.0f;
    [SerializeField] GameObject photoFire;
    [SerializeField] GameObject ghostFire;

    [SerializeField] GameObject libraryGhost;
    [SerializeField] GameObject oneCorridorGhost;

    private bool isGhostSpawned = false;

    private void Update()
    {
        if (CheckComplete() && !isGhostSpawned)
        {
            SpawnFire();
        }
    }

    private bool CheckComplete()
    {
        List<ObjectPlacement> all = new List<ObjectPlacement>(candle);
        all.Add(photo);

        foreach (var obj in all)
        {
            if (!obj.isComplete) return false;
            if (obj.type == Type.Candle && obj.currObject != null && !obj.currObject.GetComponent<Candle>().isFired) return false;
        }

        return true;
    }

    private void SpawnFire()
    {
        photoFire.SetActive(true);
        StartCoroutine(SpawnGhostCoroutine());
    }

    private void SpawnGhost()
    {
        switch (photo.currObject.GetComponent<Photo>().ghostType)
        {
            case GhostType.None:
                break;
            case GhostType.LibraryGhost:
                GetComponent<AudioSource>().Play();
                libraryGhost.SetActive(true);
                ghostFire.SetActive(true);
                isGhostSpawned = true;
                StartCoroutine(EndGame());
                break;
            case GhostType.OneCorridorGhost:
                GetComponent<AudioSource>().Play();
                oneCorridorGhost.SetActive(true);
                ghostFire.SetActive(true);
                isGhostSpawned = true;
                StartCoroutine(EndGame());
                break;
        }
    }

    private IEnumerator SpawnGhostCoroutine()
    {
        fireElapsedTime += Time.deltaTime;
        while (fireElapsedTime <= 5.0f) yield return null;
        photoFire.SetActive(false);
        SpawnGhost();
    }

    private IEnumerator EndGame()
    {
        yield return new WaitForSeconds(5.0f);
        PlayerUI.instance.PlayerGameClearOrGameExit();
    }
}
