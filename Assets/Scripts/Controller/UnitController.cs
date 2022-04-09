using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : StatsInfo
{
    // Start is called before the first frame update
    [SerializeField] int move;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public int Move
    {
        get { return move; }
        set { move = value; }
    }
}
