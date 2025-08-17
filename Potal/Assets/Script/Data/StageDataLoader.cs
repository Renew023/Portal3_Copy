using System.Collections.Generic;

using UnityEngine;



public class StageDataLoader
{
        
    List<StageData> datas = new List<StageData>();
    Dictionary<int, StageData> dataDict = new Dictionary<int, StageData>();
    private int dataCount = 0;
    
   
    public void JsonToData()
    {

        //LoadAll로 변경   
        TextAsset[] jsons = Resources.LoadAll<TextAsset>("Json/StageData");

       
        dataCount = jsons.Length;
        //뭘해겠어? 텍스트를 이제 구분지어서 리스트로 담아야겠지??
        //하나만 가져올꺼야




        for (int i = 0; i < jsons.Length; i++)
        {
            StageData data2 = JsonUtility.FromJson<StageData>(jsons[i].ToString());
            datas.Add(data2); //
          //  Debug.Log($"이름 : {data2.PrefabEntries}+ {data2.PrefabEntries[0].prefabPath}");
        }


        //값을 가져옴
        for (int i = 0; i < datas.Count; i++)
        {
            if (dataDict.ContainsKey(i) == false)
            { 
            dataDict.Add(i,datas[i]); //캐싱
            }
            //스테이지 명을 키로 만들고    
        
        }

    }

    public StageData GetStageData(int key)
    {
        if (dataDict[key] != null)
        {
            return dataDict[key];
        }
        else 
        {
            return null;
        }
    }

    //많은 스테이지 데이터를 가져와서 하나만 말고 다 로드해보자

    public int GetDataNum()
    {
       
        return dataCount;
    }

        
}
