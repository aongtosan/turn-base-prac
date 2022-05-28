using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : MonoBehaviour
{
    // Start is called before the first frame update


    private Dictionary<string, GameObject> tileMap;
    
   [SerializeField]
   CursorController cursor;
   [SerializeField]
    TileGenerate tileMapData;
    [SerializeField]
    UnitHandler unitList;
    [SerializeField]
    EnemyUnitHandler enemyList;
    List<GameObject> playableUnit;
    List<GameObject> enemyUnit;
    
    STATEPHASE phase;

    bool characterLock = false;

    private void Start()
    {
        tileMap = new Dictionary<string, GameObject>(tileMapData.getMapInfo());
        cursor.intiCursorPosition(tileMapData.getMapInfo(),0,0);
        playableUnit = unitList.UnitList;
        int k = 0;


 
        foreach(GameObject playUnit in playableUnit){//init position
             playUnit.GetComponent<UnitController>().initUnitPositionTile(playUnit,tileMap,0,3);
        } 
         //playableUnit[0].GetComponent<UnitController>().initUnitPositionTile( playableUnit[0],tileMap,0,2);
    }

 

    void cursorHandler(){
            if (Input.GetKeyDown(KeyCode.W)) // UP
            {
                cursor.cursorMove(tileMap, tileMapData.depth,tileMapData.width,  DIRECTIONS.UP);
            }
            else if (Input.GetKeyDown(KeyCode.S)) //DOWN
            {
                cursor.cursorMove(tileMap, tileMapData.depth,tileMapData.width,  DIRECTIONS.DOWN);
            }
            else if (Input.GetKeyDown(KeyCode.D)) //RIGHT
            {
                cursor.cursorMove(tileMap, tileMapData.depth,tileMapData.width,  DIRECTIONS.RIGHT);
            }
            else if (Input.GetKeyDown(KeyCode.A)) //LEFT
            {
                cursor.cursorMove(tileMap, tileMapData.depth,tileMapData.width,  DIRECTIONS.LEFT);
            }
            else if (Input.GetKeyDown(KeyCode.X))//SELECT TILE && Move Unit
            {
                Debug.Log( string.Format( " cursor location positionX = {0},positionY = {1} ",cursor.PositionX,cursor.PositionY ) );
                if(cursor.SelectedTile==null){
                    cursor.SelectedTile = TileMap[string.Format(TileEnum.ID_PATTERN_TILE,cursor.PositionX,cursor.PositionY)].GetComponent<TileController>();
                    TileMap[string.Format(TileEnum.ID_PATTERN_TILE,cursor.PositionX,cursor.PositionY)].GetComponent<TileController>().IsCursorSelect = true;
                }
                if (cursor.SelectedTile!=null){
                    TileMap[cursor.SelectedTile.TileId].GetComponent<TileController>().IsCursorSelect = false;
                    cursor.SelectedTile = TileMap[string.Format(TileEnum.ID_PATTERN_TILE,cursor.PositionX,cursor.PositionY)].GetComponent<TileController>();
                    TileMap[cursor.SelectedTile.TileId].GetComponent<TileController>().IsCursorSelect = true;
                }
                
                
                if(cursor.SelectedTile.IsUnitOnTile){
                    cursor.SelectedUnit = cursor.SelectedTile.UnitOnTile.GetComponent<UnitController>();
                    GameObject onWalkingUnit = TileMap[string.Format(TileEnum.ID_PATTERN_TILE,cursor.SelectedUnit.PositionX,cursor.SelectedUnit.PositionY)].GetComponent<TileController>().UnitOnTile ;
                    if(!characterLock){
                        onWalkingUnit.GetComponent<UnitController>().findMovableTile(onWalkingUnit,tileMap,tileMapData.width,tileMapData.depth);    
                        characterLock = true;
                    }
                    else if(characterLock){
                         onWalkingUnit.GetComponent<UnitController>().clearTilesState(tileMap,tileMapData.width,tileMapData.depth);
                         characterLock = false;
                    }                       

                
                }
                if(!cursor.SelectedTile.IsUnitOnTile && cursor.SelectedUnit!=null && cursor.SelectedTile.IsWalkAble){
                        GameObject onWalkingUnit = TileMap[string.Format(TileEnum.ID_PATTERN_TILE,cursor.SelectedUnit.PositionX,cursor.SelectedUnit.PositionY)].GetComponent<TileController>().UnitOnTile ;
                        onWalkingUnit.GetComponent<UnitController>().moveUnitToTile(onWalkingUnit,cursor,TileMap,tileMapData);  
                        cursor.SelectedUnit = null;
                }
              
              
                
            }
    }
    void Update()
    {
        cursorHandler();
    }
    public Dictionary<string, GameObject> TileMap
    {
        set { tileMap = value; }
        get { return tileMap; }
    }   
}
