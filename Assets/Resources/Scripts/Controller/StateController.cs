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


    private void Start()
    {
        tileMap = new Dictionary<string, GameObject>(tileMapData.getMapInfo());
        cursor.intiCursorPosition(tileMapData.getMapInfo(),0,0);
        playableUnit = unitList.UnitList;
        int k = 0;
        foreach(GameObject playUnit in playableUnit){//init position
             playUnit.GetComponent<UnitController>().initUnitPositionTile(playUnit,tileMap,0,k++);
        } 
         //playableUnit[0].GetComponent<UnitController>().initUnitPositionTile( playableUnit[0],tileMap,0,2);
    }

 

    void cursorHandler(){
            if (Input.GetKeyDown(KeyCode.W)) // UP
            {
                cursor.cursorMove(tileMap, tileMapData.width, tileMapData.depth, DirectionEnum.DIRECTIONS.UP);
            }
            else if (Input.GetKeyDown(KeyCode.S)) //DOWN
            {
                cursor.cursorMove(tileMap, tileMapData.width, tileMapData.depth, DirectionEnum.DIRECTIONS.DOWN);
            }
            else if (Input.GetKeyDown(KeyCode.D)) //RIGHT
            {
                cursor.cursorMove(tileMap, tileMapData.width, tileMapData.depth, DirectionEnum.DIRECTIONS.RIGHT);
            }
            else if (Input.GetKeyDown(KeyCode.A)) //LEFT
            {
                cursor.cursorMove(tileMap, tileMapData.width, tileMapData.depth, DirectionEnum.DIRECTIONS.LEFT);
            }else if (Input.GetKeyDown(KeyCode.X))//SELECT TILE && Move Unit
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

                   // select action
                }
                if(!cursor.SelectedTile.IsUnitOnTile && cursor.SelectedUnit!=null){
                        GameObject onWalkingUnit = TileMap[string.Format(TileEnum.ID_PATTERN_TILE,cursor.SelectedUnit.PositionX,cursor.SelectedUnit.PositionY)].GetComponent<TileController>().UnitOnTile ;
                        TileMap[string.Format(TileEnum.ID_PATTERN_TILE,cursor.SelectedUnit.PositionX,cursor.SelectedUnit.PositionY)].GetComponent<TileController>().IsUnitOnTile = false;
                        TileMap[string.Format(TileEnum.ID_PATTERN_TILE,cursor.SelectedUnit.PositionX,cursor.SelectedUnit.PositionY)].GetComponent<TileController>().UnitOnTile = null;
                        TileMap[string.Format(TileEnum.ID_PATTERN_TILE,cursor.PositionX,cursor.PositionY)].GetComponent<TileController>().IsUnitOnTile = true;
                        TileMap[string.Format(TileEnum.ID_PATTERN_TILE,cursor.PositionX,cursor.PositionY)].GetComponent<TileController>().UnitOnTile =  onWalkingUnit;
                        onWalkingUnit.GetComponent<UnitController>().PositionX = cursor.PositionX;
                        onWalkingUnit.GetComponent<UnitController>().PositionY = cursor.PositionY;
                        onWalkingUnit.transform.parent = TileMap[string.Format(TileEnum.ID_PATTERN_TILE,cursor.PositionX,cursor.PositionY)].transform;
                        onWalkingUnit.transform.localPosition = new Vector3 (0,0.75f,0);
                        Debug.Log( string.Format( " unit name {0} ,positionX = {1},positionY = {2} ",cursor.SelectedUnit.name,cursor.SelectedUnit.PositionX,cursor.SelectedUnit.PositionY ) );
                        //Debug.Log( string.Format( " cursor location positionX = {0},positionY = {1} ",cursor.PositionX,cursor.PositionY ) );
                        cursor.SelectedUnit = null;
                }
              
                
            }
    }
    // IEnumerator waitUntilMoveSuccess(bool moveSuccess){
    //       yield return new WaitUntil(()=> moveSuccess );
    // }
    // Update is called once per frame
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
