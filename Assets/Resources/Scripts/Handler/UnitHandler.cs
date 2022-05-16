using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitHandler : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] int playableUnitCount;
    List<GameObject> unitList;
   
    void Awake(){
        unitList = new List<GameObject>();
        //playableUnitCount = 1;
        unitSpawn();
    }
    
    public void unitSpawn(){
        for(int i=0;i<playableUnitCount;i++){
            unitList.Add(PlayableUnitGenerate.generateUnit());
        }
    }

    public List<GameObject> UnitList{
        get{return unitList;}
    }
}
