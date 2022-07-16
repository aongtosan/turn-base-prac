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

       

        unit.GetComponent<UnitController>().StatData = utilitiesFunction.loadUnitDataResource<StatsInfo>("unitData.json");
        unit.name =  unit.GetComponent<UnitController>().StatData.UnitName;
        
        GameObject unitModel = Resources.Load("Prefabs/Unit") as GameObject ;
        Instantiate(unitModel,unit.transform);
        return unit;
    }

}