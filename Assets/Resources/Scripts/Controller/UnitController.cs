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

    bool moveComplete;
    
    public void initUnit(){
        unit = new GameObject("Unit");
        unit.tag = "PlayableUnit";
        unit = Instantiate(Resources.Load("Prefabs/Unit"),unit.transform) as GameObject;
        //unit.AddComponent<UnitController>();
        unit.name = "unit";
        //unit.GetComponent<UnitController>().UnitName = "Unit";
        //unit.GetComponent<UnitController>().Move = 3;
    }
    public void moveUnit(Dictionary<string,GameObject> tileMap,int moveToPositionX,int moveToPositionY,int stateWidth,int stateDepth){
        tileMap[string.Format(TileEnum.ID_PATTERN_TILE, unit.GetComponent<UnitController>().PositionX , unit.GetComponent<UnitController>().PositionY)].GetComponent<TileController>().IsUnitOnTile = false;
        unit.transform.parent.SetParent(tileMap[string.Format(TileEnum.ID_PATTERN_TILE, moveToPositionX, moveToPositionY)].transform.parent);
        unit.transform.localPosition = new Vector3(0, .75f, 0);
        // unit.GetComponent<UnitController>().PositionX = moveToPositionX;
        // unit.GetComponent<UnitController>().PositionY = moveToPositionY;
        // unit.GetComponent<UnitController>().MoveComplete = true;
        tileMap[string.Format(TileEnum.ID_PATTERN_TILE, moveToPositionX, moveToPositionY)].GetComponent<TileController>().IsUnitOnTile = true;
        positionX = moveToPositionX;
        positionY = moveToPositionY;
    }
    public void clearTilesState(Dictionary<string,GameObject> tileMap,int stateWidth,int stateDepth){
        for(int i=0;i<stateWidth;i++){
            for(int j=0;j<stateWidth;j++){
                tileMap[string.Format(TileEnum.ID_PATTERN_TILE,i,j)].GetComponent<TileController>().IsWalkAble=false;
            }
        }
    }

    public void initUnitPositionTile(Dictionary<string,GameObject> tileMap,int initpositionX,int initPositionY){
        unit.transform.parent.SetParent(tileMap[string.Format(TileEnum.ID_PATTERN_TILE, initpositionX, initPositionY)].transform.parent);
        // unit.GetComponent<UnitController>().PositionX = initpositionX;
        // unit.GetComponent<UnitController>().PositionY = initPositionY;
        positionX = initpositionX;
        positionY = initPositionY;
        unit.transform.localPosition = new Vector3(0, .75f, 0);
        tileMap[string.Format(TileEnum.ID_PATTERN_TILE, initpositionX, initPositionY)].GetComponent<TileController>().IsUnitOnTile = true;
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
    public bool MoveComplete{
        set{ moveComplete = value; }
        get{ return moveComplete; }
    }
    public GameObject UnitInfo{
        get{ return unit; }
    }
}
