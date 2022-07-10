using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
public class PlayableUnitGenerate : MonoBehaviour
{
    public static GameObject generateUnit(){
        GameObject unit = new GameObject("playableunit");
        unit.AddComponent<UnitController>();
        unit.tag = "PlayableUnit";

        StatsInfo unitData = loadUnitDataResource();

        unit.GetComponent<UnitController>().StatData = loadUnitDataResource();
        unit.name =  unit.GetComponent<UnitController>().StatData.UnitName;
        
        GameObject unitModel = Resources.Load("Prefabs/Unit") as GameObject ;
        Instantiate(unitModel,unit.transform);
        return unit;
    }

    public static StatsInfo loadUnitDataResource(){
        string readJsonData = File.ReadAllText(Application.dataPath+"/Resources/CommonDatas/unitData.json");
        string unitDatJson = JsonUtility.ToJson(readJsonData);
        Debug.Log(readJsonData);
        return JsonUtility.FromJson<StatsInfo>(readJsonData);
    }
}