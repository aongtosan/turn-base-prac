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
   
   [SerializeField] 
    List<GameObject> playerUnitsPrefabs;

    GameObject unit;

    private void Start()
    {
        tileMap = new Dictionary<string, GameObject>(tileMapData.getMapInfo());
        cursor.intiCursorPosition(tileMapData.getMapInfo());


        unit = new GameObject("Unit1");
        unit.transform.SetParent(tileMap[string.Format(TileEnum.ID_PATTERN_TILE, 0, 0)].transform.parent);
        unit.transform.localPosition = new Vector3(0, .75f, 0);
        unit = Instantiate(playerUnitsPrefabs[0], unit.transform);
        unit.GetComponent<UnitController>().PositionX = 0;
        unit.GetComponent<UnitController>().PositionY = 0;
        
        tileMap[string.Format(TileEnum.ID_PATTERN_TILE, 0, 0)].GetComponent<TileController>().IsUnitOnTile = true;
    }

   
    // Update is called once per frame
    void Update()
    {
        if (cursor.SelectedTile == null)
        {
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
        }
         if (Input.GetKeyDown(KeyCode.X))//SELECT TILE
        {
            cursor.SelectedTile = tileMap[string.Format(TileEnum.ID_PATTERN_TILE,cursor.PositionX, cursor.PositionY) ].GetComponent<TileController>();
            cursor.SelectedTile.IsCursorSelect = true;
            if (cursor.SelectedTile.IsUnitOnTile)
            {
                UnitController selectedUnit = unit.GetComponent<UnitController>();
                selectedUnit.findMovableTile(tileMap,tileMapData.width,tileMapData.depth,selectedUnit.PositionX,selectedUnit.PositionY);
            }
            else
            {
                cursor.SelectedTile.IsCursorSelect = false;
                cursor.SelectedTile = null;
            }
        }
    }
    public Dictionary<string, GameObject> TileMap
    {
        set { tileMap = value; }
        get { return tileMap; }
    }   
}
