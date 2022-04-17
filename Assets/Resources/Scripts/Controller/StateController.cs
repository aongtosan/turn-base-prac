using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : MonoBehaviour
{
    // Start is called before the first frame update


    private Dictionary<string, GameObject> tileMap;
    
   [SerializeField]
    private CursorController cursor;
   [SerializeField]
    TileGenerate tileMapData;
   

    private void Start()
    {
        tileMap = new Dictionary<string, GameObject>(tileMapData.getMapInfo());
        cursor.intiCursorPosition(tileMapData.getMapInfo(),0,0);
        GetComponent<UnitController>().initUnitPositionTile(tileMap,0,0);

        //initUnitPositionTile();
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
            }
        
         if (Input.GetKeyDown(KeyCode.X))//SELECT TILE
        {
            cursor.SelectedTile = TileMap[string.Format(TileEnum.ID_PATTERN_TILE,cursor.PositionX,cursor.PositionY)].GetComponent<TileController>();
            TileMap[string.Format(TileEnum.ID_PATTERN_TILE,cursor.PositionX,cursor.PositionY)].GetComponent<TileController>().IsCursorSelect = true;
        

        }
    }
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
