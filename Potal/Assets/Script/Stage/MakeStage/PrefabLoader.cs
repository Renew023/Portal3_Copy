using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PrefabLoader
{
    public Dictionary<string, GameObject> LoadAllPrefabs(string path)
    {
        Dictionary<string, GameObject> prefabs = new();

        GameObject[] prefabArr = Resources.LoadAll<GameObject>(path);

        foreach (GameObject prefab in prefabArr)
        {
            if (prefab != null)
            {
                string key = prefab.name;
                prefabs[key] = prefab;
            }
        }

        return prefabs;
    }
}
