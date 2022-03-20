using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileController : MonoBehaviour
{
    // Start is called before the first frame update
    bool isWalkAble;
    bool isReachAble;
    int heightlvl;
    void Start()
    {
           Material tileColor = GetComponent<Renderer>().material;
             if (isReachAble)
               {   
                   tileColor.color = new Color32(41, 81, 243,1);
               }
               else
               {
                   tileColor.color = Color.red;
               }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public bool IsWalkAble
    {
        get { return isWalkAble; }
        set { isWalkAble = value; }
    }
    public bool IsReachAble
    {
        get { return isReachAble; }
        set { isReachAble = value; }
    }
    public int Heightlvl
    {
        get { return heightlvl; }
        set {heightlvl = value; }
    }
}
