using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class utilitiesFunction 
{
     public static T loadUnitDataResource<T>(string filename){
        string readJsonData = File.ReadAllText(string.Format(Application.dataPath+"/Resources/CommonDatas/{0}",filename));
        string unitDatJson = JsonUtility.ToJson(readJsonData);
        //Debug.Log(readJsonData);
        return JsonUtility.FromJson<T>(readJsonData);
    }
}