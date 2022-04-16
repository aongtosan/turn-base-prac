using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : StatsInfo
{
    // Start is called before the first frame update
    [SerializeField] int move;
    int positionX;
    int positionY;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void moveUnit(){
    
    }
    public Dictionary<string,GameObject> findMovableTile(Dictionary<string , GameObject> tilemap,int tileWidth,int tileDepth,int unitPositionX,int unitPositionY){

        Dictionary<string,GameObject> movableTile = new Dictionary<string, GameObject>();
        // tilemap[string.Format(TileEnum.ID_PATTERN_TILE,unitPositionX,unitPositionY+1)].GetComponent<TileController>().IsCursorHover = true;
        // tilemap[string.Format(TileEnum.ID_PATTERN_TILE,unitPositionX+1,unitPositionY)].GetComponent<TileController>().IsCursorHover = true;
        for(int i=0;i<move;i++){
            for(int j=1;j<=move;j++){
                if(j+unitPositionY<tileDepth){
                    tilemap[string.Format(TileEnum.ID_PATTERN_TILE,unitPositionX,unitPositionY+j)].GetComponent<TileController>().IsCursorHover = true;
                }
            }
        }
        return movableTile;
    }
    public int Move
    {
        get { return move; }
        set { move = value; }
    }
    public int PositionX{
        set{ positionX = value;  }
        get{ return positionX; }
    }
    public int PositionY{
        set{ positionY = value;  }
        get{ return positionY; }
    }
}
