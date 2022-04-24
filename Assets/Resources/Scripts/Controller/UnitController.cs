 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : StatsInfo
{
    // Start is called before the first frame update
    GameObject unit;
    [SerializeField] int move;
     int positionX;
     int positionY;
    
    public void moveUnit(Dictionary<string,GameObject> tileMap,int moveToPositionX,int moveToPositionY){
        unit.transform.parent.SetParent(tileMap[string.Format(TileEnum.ID_PATTERN_TILE, moveToPositionX, moveToPositionY)].transform.parent);
        unit.transform.parent.localPosition = new Vector3(0, .75f, 0);
        unit.GetComponent<UnitController>().PositionX = moveToPositionX;
        unit.GetComponent<UnitController>().PositionY = moveToPositionY;
    }

    public void initUnitPositionTile(Dictionary<string,GameObject> tileMap,int initpositionX,int initPositionY){
        unit = new GameObject("Unit");
        unit.tag = "PlayableUnit";
        unit.transform.SetParent(tileMap[string.Format(TileEnum.ID_PATTERN_TILE, initpositionX, initPositionY)].transform.parent);
        unit.transform.localPosition = new Vector3(0, .75f, 0);
        unit = Instantiate(Resources.Load("Prefabs/Unit"),unit.transform) as GameObject;
        unit.GetComponent<UnitController>().move = 3 ;
        tileMap[string.Format(TileEnum.ID_PATTERN_TILE, 0, 0)].GetComponent<TileController>().IsUnitOnTile = true;
    }
    public void findMovableTile(Dictionary<string , GameObject> tilemap,int tileWidth,int tileDepth,int unitPositionX,int unitPositionY){

        for(int i= 0; i < unit.GetComponent<UnitController>().move;i++){
            for(int j= 0;j<=unit.GetComponent<UnitController>().move;j++){
                if(j+unitPositionY<tileDepth){
                    tilemap[string.Format(TileEnum.ID_PATTERN_TILE,PositionX,PositionY+j)].GetComponent<TileController>().IsWalkAble = true;
                }
            }
        }
        //return movableTile;
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
