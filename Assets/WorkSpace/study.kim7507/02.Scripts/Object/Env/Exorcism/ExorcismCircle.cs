using System.Collections.Generic;
using UnityEngine;

public class ExorcismCircle : MonoBehaviour
{
    [SerializeField] ObjectPlacement[] candle;
    [SerializeField] ObjectPlacement photo;

    [SerializeField] GameObject libraryGhost;
    [SerializeField] GameObject oneCorridorGhost;
    private void Update()
    {
        if (CheckComplete())
        {
            SpawnGhost();
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

    private void SpawnGhost()
    {
        // TODO: �ͽ� ���� �Ҹ� ��� (���뽺�����ϴ� �Ҹ�)
        switch (photo.currObject.GetComponent<Photo>().ghostType)
        {
            case GhostType.None:
                break;
            case GhostType.LibraryGhost:
                libraryGhost.SetActive(true);
                break;
            case GhostType.OneCorridorGhost:
                oneCorridorGhost.SetActive(true);
                break;
        }
    }
}
