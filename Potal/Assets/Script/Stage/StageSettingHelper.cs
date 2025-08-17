using System;
using UnityEngine;

public class StageSettingHelper : MonoBehaviour
{

    public static event Action onCompleted;


    public StageDataLoader dataLoader;

    private int buttonid;
    private int doorid;

    private void Awake()
    {
        dataLoader = new StageDataLoader();
        dataLoader.JsonToData();
        
    }


    public void SettingMap(StageData data)
    {
        foreach (var map in data.PrefabEntries)
        {
            GameObject go = Resources.Load<GameObject>($"Prefabs/RealStagePrefab/{map.prefabPath}");
            Vector3 goPosition = map.position;
            Quaternion goRotaion = Quaternion.Euler(map.rotation);
            Vector3 Scale = map.scale;
            GameObject entryGo = Instantiate(go, goPosition, goRotaion);
            entryGo.transform.localScale = map.scale;
            if (map.prefabPath == "Button")
            {
                entryGo.GetComponent<DoorButton>().SetId(map.connectID);

            }
            else if (map.prefabPath == "Door")
            {

                entryGo.GetComponent<Door>().SetId(map.connectID);

            }


        }


        onCompleted?.Invoke();

    }

    public void FindmapIndex(int stage)
    {
        SettingMap(dataLoader.GetStageData(stage));
    }

    public int GetDatasNum()
    {
        return dataLoader.GetDataNum();
    }
    
}
