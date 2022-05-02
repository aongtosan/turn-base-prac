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
        // UnitController unit = GetComponent<UnitController>();
        // unit.initUnit();
        // unit.initUnitPositionTile(tileMap,0,0);
        //Debug.Log(unit);
        // UnitController unit2 = GetComponent<UnitController>();
        // unit2.initUnit();
        // unit2.initUnitPositionTile(tileMap,0,1);
        // //playableUnit = new List<GameObject>();
        //GameObject unit = new GameObject("Unit");
        // unit.AddComponent<UnitController>();
        // playableUnit.Add(unit);
        // playableUnit[0].GetComponent<UnitController>().initUnitPositionTile(tileMap,0,0);
        // unit = GetComponent<UnitController>();
        // unit.initUnitPositionTile(tileMap,0,0);
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
            }else if (Input.GetKeyDown(KeyCode.X))//SELECT TILE
            {

                if(cursor.SelectedTile==null){
                    cursor.SelectedTile = TileMap[string.Format(TileEnum.ID_PATTERN_TILE,cursor.PositionX,cursor.PositionY)].GetComponent<TileController>();
                    TileMap[string.Format(TileEnum.ID_PATTERN_TILE,cursor.PositionX,cursor.PositionY)].GetComponent<TileController>().IsCursorSelect = true;
                }
                if (cursor.SelectedTile!=null){
                    TileMap[cursor.SelectedTile.TileId].GetComponent<TileController>().IsCursorSelect = false;
                    cursor.SelectedTile = TileMap[string.Format(TileEnum.ID_PATTERN_TILE,cursor.PositionX,cursor.PositionY)].GetComponent<TileController>();
                    TileMap[cursor.SelectedTile.TileId].GetComponent<TileController>().IsCursorSelect = true;
                }
                
                
                // if(cursor.SelectedTile.IsUnitOnTile){
                //     cursor.SelectedUnit = unit;
                //     unit.findMovableTile(TileMap,tileMapData.width,tileMapData.depth,unit.PositionX,unit.PositionY) ;
                // }
                // if(cursor.SelectedTile.IsWalkAble){
                //     TileMap[string.Format(TileEnum.ID_PATTERN_TILE,unit.PositionX,unit.PositionY)].GetComponent<TileController>().IsUnitOnTile = false;
                //     unit.moveUnit(tileMap,cursor.PositionX,cursor.PositionY,tileMapData.width,tileMapData.depth);
                //     TileMap[string.Format(TileEnum.ID_PATTERN_TILE,unit.PositionX,unit.PositionY)].GetComponent<TileController>().IsUnitOnTile = true;
                    
                // }
              
                
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
