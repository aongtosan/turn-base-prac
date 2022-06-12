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
    public void clearTilesState(Dictionary<string,GameObject> tileMap,int stateWidth,int stateDepth){
        for(int i=0;i<stateWidth;i++){
            for(int j=0;j<stateDepth;j++){
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
    public void teleportToTile(GameObject onWalkingUnit,CursorController cursor,Dictionary<string,GameObject> tileMap,TileGenerate tileInfo){
            tileMap[string.Format(TileEnum.ID_PATTERN_TILE,cursor.SelectedUnit.PositionX,cursor.SelectedUnit.PositionY)].GetComponent<TileController>().IsUnitOnTile = false;
            tileMap[string.Format(TileEnum.ID_PATTERN_TILE,cursor.SelectedUnit.PositionX,cursor.SelectedUnit.PositionY)].GetComponent<TileController>().UnitOnTile = null;
            tileMap[string.Format(TileEnum.ID_PATTERN_TILE,cursor.PositionX,cursor.PositionY)].GetComponent<TileController>().IsUnitOnTile = true;
            tileMap[string.Format(TileEnum.ID_PATTERN_TILE,cursor.PositionX,cursor.PositionY)].GetComponent<TileController>().UnitOnTile =  onWalkingUnit   ;
            onWalkingUnit.GetComponent<UnitController>().PositionX = cursor.PositionX;
            onWalkingUnit.GetComponent<UnitController>().PositionY = cursor.PositionY;
            onWalkingUnit.transform.parent = tileMap[string.Format(TileEnum.ID_PATTERN_TILE,cursor.PositionX,cursor.PositionY)].transform;
            onWalkingUnit.transform.localPosition = new Vector3 (0,0.75f,0);
    
    }
    public void walkToTile(GameObject onWalkingUnit,CursorController cursor,Dictionary<string,GameObject> tileMap,TileGenerate tileInfo){
        Debug.Log("walk");
    }
    public void flyToTile(GameObject onWalkingUnit,CursorController cursor,Dictionary<string,GameObject> tileMap,TileGenerate tileInfo){
        Debug.Log("fly");
    }
    public void moveUnitToTile(GameObject onWalkingUnit,CursorController cursor,Dictionary<string,GameObject> tileMap,TileGenerate tileInfo){
            if(MOVETYPE == MOVE_TYPE.TELEPORT){
                teleportToTile(onWalkingUnit,cursor,tileMap,tileInfo);
            }
            if(MOVETYPE == MOVE_TYPE.WALK){
                walkToTile(onWalkingUnit,cursor,tileMap,tileInfo);
            }
            if(MOVETYPE  == MOVE_TYPE.FLY){
                flyToTile(onWalkingUnit,cursor,tileMap,tileInfo);
            }
           
            clearTilesState(tileMap,tileInfo.width,tileInfo.depth);
    
    }
    public void findMovableTile(GameObject onWalkingUnit, Dictionary<string , GameObject> tileMap,int tileWidth,int tileDepth){
        int predictNextPositionX = onWalkingUnit.GetComponent<UnitController>().positionX;//up
        int predictNextPositionY = onWalkingUnit.GetComponent<UnitController>().positionY;//right
        for(int i = 0; i<=move;i++){      
                if(predictNextPositionX<tileWidth) tileMap[string.Format(TileEnum.ID_PATTERN_TILE,predictNextPositionX,predictNextPositionY)].GetComponent<TileController>().IsWalkAble=true;
                for(int j=0;j<=move-i;j++ ){
                    if(predictNextPositionY+j<tileDepth ){
                        if(predictNextPositionX < tileWidth) {
                            tileMap[string.Format(TileEnum.ID_PATTERN_TILE,predictNextPositionX,predictNextPositionY+j)].GetComponent<TileController>().IsWalkAble=true;
                        }
                        if(onWalkingUnit.GetComponent<UnitController>().positionX-i >=0) {
                            tileMap[string.Format(TileEnum.ID_PATTERN_TILE, onWalkingUnit.GetComponent<UnitController>().positionX-i,predictNextPositionY+j)].GetComponent<TileController>().IsWalkAble=true;
                        }
                    }  
                    if(predictNextPositionY-j>=0 )   {
                        if(predictNextPositionX < tileWidth) {
                            tileMap[string.Format(TileEnum.ID_PATTERN_TILE,predictNextPositionX,predictNextPositionY-j)].GetComponent<TileController>().IsWalkAble=true;
                        }
                        if(onWalkingUnit.GetComponent<UnitController>().positionX-i >=0) {
                            tileMap[string.Format(TileEnum.ID_PATTERN_TILE, onWalkingUnit.GetComponent<UnitController>().positionX-i,predictNextPositionY-j)].GetComponent<TileController>().IsWalkAble=true;
                        }  
                    }  
                }
                predictNextPositionX++;

        }
        
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
