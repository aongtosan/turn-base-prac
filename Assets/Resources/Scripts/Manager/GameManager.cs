using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public UnityEvent OnGameStart;
    public static GameManager instance ;
    
    public enum GAME_STATE{
        WORLDMAP,
        BATTLE,
        BASE
    }
    void Awake(){
        instance = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
