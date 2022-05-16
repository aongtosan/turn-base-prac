 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : StatsInfo
{
    // Start is called before the first frame update
    [SerializeField] int move;
    int positionX;
    int positionY;
    int actionPoint;
    bool moveComplete;
    
    public void useActionPoint(){

    }
    public void attack(){

    }
    public void useAbilty(){
        
    }
    public void moveUnit(Dictionary<string,GameObject> tileMap,int moveToPositionX,int moveToPositionY,int stateWidth,int stateDepth){
     
    }
    public void clearTilesState(Dictionary<string,GameObject> tileMap,int stateWidth,int stateDepth){
        for(int i=0;i<stateWidth;i++){
            for(int j=0;j<stateWidth;j++){
                tileMap[string.Format(TileEnum.ID_PATTERN_TILE,i,j)].GetComponent<TileController>().IsWalkAble=false;
            }
        }
    }

    public void initUnitPositionTile(GameObject unit,Dictionary<string,GameObject> tileMap,int initpositionX,int initPositionY){
            transform.SetParent(tileMap[string.Format(TileEnum.ID_PATTERN_TILE,initpositionX,initPositionY)].transform);
            transform.localPosition = new Vector3(0f,.75f,0f);
            tileMap[string.Format(TileEnum.ID_PATTERN_TILE,initpositionX,initPositionY)].GetComponent<TileController>().IsUnitOnTile = true;
            tileMap[string.Format(TileEnum.ID_PATTERN_TILE,initpositionX,initPositionY)].GetComponent<TileController>().UnitOnTile = unit;
            positionX = initpositionX;
            positionY = initPositionY;
    }
    public void findMovableTile(Dictionary<string , GameObject> tilemap,int tileWidth,int tileDepth,int unitPositionX,int unitPositionY){

  
        
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
    public int ActionPoint{
        set{actionPoint = value;}
        get{return actionPoint;}
    }
}
