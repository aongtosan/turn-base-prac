using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayableUnitGenerate : MonoBehaviour
{
    public static GameObject generateUnit(){
        GameObject unit = new GameObject("playableunit");
        unit.AddComponent<UnitController>();
        unit.tag = "PlayableUnit";
        unit.GetComponent<UnitController>().name = "Aongtsan";
        GameObject unitModel = Resources.Load("Prefabs/Unit") as GameObject ;
        Instantiate(unitModel,unit.transform);
        return unit;
    }
}