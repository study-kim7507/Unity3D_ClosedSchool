using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class PrefabData
{
    public string name;
    public GameObject originalPrefab;
}


public class PrefabManager : MonoBehaviour
{
    public static PrefabManager Instance;

    [SerializeField] private List<PrefabData> prefabList;
    private Dictionary<string, GameObject> prefabDictionary;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance);
            return;
        }
        Instance = this;

        prefabDictionary = prefabList.ToDictionary(entry => entry.name, entry => entry.originalPrefab);
    }

    public GameObject GetOriginalPrefab(string name)
    {
        if (prefabDictionary.TryGetValue(name, out GameObject originalPrefab))
        {
            return originalPrefab;
        }
        return null;
    }
}
