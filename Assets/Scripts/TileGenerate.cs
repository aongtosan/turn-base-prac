using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGenerate : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject tilePrefabs;
    [SerializeField]
    GameObject cursorPrefabs;

    [SerializeField]
    int width;
    [SerializeField]
    int depth;
    [SerializeField]
    int height;
    [SerializeField]
    int hillPercentage;
    [SerializeField]
    int treasurePile;

    const string NAME_PATTERN_TILE = "Tile[{0}][{1}][{2}]";
    const string ID_PATTERN_TILE = "Tile[{0}][{1}]";
    const string ROW_PATTERN_TILE = "Row[{0}]";
    const string BASE_PLANE_NAME = "BASE";
    const string NOT_REACHABLE = "Not Reachable";
    const string CURSOR = "Cursor";
    
    private GameObject cursor;
    private GameObject terrain;
    private Dictionary<string,GameObject> tileMap;

    public enum DIRECTIONS
    {
        UP,
        DOWN,
        RIGHT,
        LEFT
    }

    private void Awake()
    {
        terrain = new GameObject(BASE_PLANE_NAME);
        terrain.transform.SetParent(transform);
        terrain.transform.localPosition = Vector3.zero;
        tileMap = new Dictionary<string, GameObject>();
        
        createHillTile(width, depth, height, hillPercentage, treasurePile);
        
        Transform initCursorLoc = tileMap[string.Format(ID_PATTERN_TILE, 0, 0)].transform.parent;
        TileController initCursorLocBlock = tileMap[string.Format(ID_PATTERN_TILE, 0, 0)].GetComponent<TileController>();

        intiCursorPosition(initCursorLoc,initCursorLocBlock);
    }

    CursorController cursorMove(CursorController currCursorPosition,DIRECTIONS dir)
    {
      
        if (dir == DIRECTIONS.UP)
        {
            if(currCursorPosition.PositionX+1 <= width-1)
            {
                tileMap[string.Format(ID_PATTERN_TILE, currCursorPosition.PositionX, currCursorPosition.PositionY)].GetComponent<TileController>().IsCursorHover = false;
                
                cursor.transform.SetParent(tileMap[string.Format(ID_PATTERN_TILE, currCursorPosition.PositionX + 1, currCursorPosition.PositionY)].transform.parent);
                cursor.transform.localPosition = new Vector3(0, 3f, 0);
            
                tileMap[string.Format(ID_PATTERN_TILE, currCursorPosition.PositionX + 1, currCursorPosition.PositionY)].GetComponent<TileController>().IsCursorHover = true;

                currCursorPosition.PositionX++;
               
            }
            else
            {
                tileMap[string.Format(ID_PATTERN_TILE, currCursorPosition.PositionX, currCursorPosition.PositionY)].GetComponent<TileController>().IsCursorHover = false;
                cursor.transform.SetParent(tileMap[string.Format(ID_PATTERN_TILE, 0, currCursorPosition.PositionY)].transform.parent);
                cursor.transform.localPosition = new Vector3(0, 3f, 0);
                tileMap[string.Format(ID_PATTERN_TILE, 0, currCursorPosition.PositionY)].GetComponent<TileController>().IsCursorHover = true;
                currCursorPosition.PositionX = 0;


            }
        }
        else if (dir == DIRECTIONS.DOWN)
        {
            if (currCursorPosition.PositionX - 1 >= 0)
            {
                tileMap[string.Format(ID_PATTERN_TILE, currCursorPosition.PositionX, currCursorPosition.PositionY)].GetComponent<TileController>().IsCursorHover = false;

                cursor.transform.SetParent(tileMap[string.Format(ID_PATTERN_TILE, currCursorPosition.PositionX - 1, currCursorPosition.PositionY)].transform.parent);
                cursor.transform.localPosition = new Vector3(0, 3f, 0);

                tileMap[string.Format(ID_PATTERN_TILE, currCursorPosition.PositionX - 1, currCursorPosition.PositionY)].GetComponent<TileController>().IsCursorHover = true;

                currCursorPosition.PositionX--;

            }
            else
            {
                tileMap[string.Format(ID_PATTERN_TILE, currCursorPosition.PositionX, currCursorPosition.PositionY)].GetComponent<TileController>().IsCursorHover = false;
                cursor.transform.SetParent(tileMap[string.Format(ID_PATTERN_TILE, width - 1, currCursorPosition.PositionY)].transform.parent);
                cursor.transform.localPosition = new Vector3(0, 3f, 0);
                tileMap[string.Format(ID_PATTERN_TILE, width-1, currCursorPosition.PositionY)].GetComponent<TileController>().IsCursorHover = true;
                currCursorPosition.PositionX = width - 1;


            }
        }
        else if (dir == DIRECTIONS.RIGHT)
        {
            if (currCursorPosition.PositionY + 1 <= depth - 1)
            {
                tileMap[string.Format(ID_PATTERN_TILE, currCursorPosition.PositionX, currCursorPosition.PositionY)].GetComponent<TileController>().IsCursorHover = false;

                cursor.transform.SetParent(tileMap[string.Format(ID_PATTERN_TILE, currCursorPosition.PositionX , currCursorPosition.PositionY+1)].transform.parent);
                cursor.transform.localPosition = new Vector3(0, 3f, 0);

                tileMap[string.Format(ID_PATTERN_TILE, currCursorPosition.PositionX, currCursorPosition.PositionY+1)].GetComponent<TileController>().IsCursorHover = true;

                currCursorPosition.PositionY++;

            }
            else
            {
                tileMap[string.Format(ID_PATTERN_TILE, currCursorPosition.PositionX, currCursorPosition.PositionY)].GetComponent<TileController>().IsCursorHover = false;
                cursor.transform.SetParent(tileMap[string.Format(ID_PATTERN_TILE, currCursorPosition.PositionX, 0)].transform.parent);
                cursor.transform.localPosition = new Vector3(0, 3f, 0);
                tileMap[string.Format(ID_PATTERN_TILE, currCursorPosition.PositionX, 0)].GetComponent<TileController>().IsCursorHover = true;
                currCursorPosition.PositionY = 0;


            }
        }
        else if (dir == DIRECTIONS.LEFT)
        {
            if (currCursorPosition.PositionY - 1 >= 0)
            {
                tileMap[string.Format(ID_PATTERN_TILE, currCursorPosition.PositionX, currCursorPosition.PositionY)].GetComponent<TileController>().IsCursorHover = false;

                cursor.transform.SetParent(tileMap[string.Format(ID_PATTERN_TILE, currCursorPosition.PositionX, currCursorPosition.PositionY - 1)].transform.parent);
                cursor.transform.localPosition = new Vector3(0, 3f, 0);

                tileMap[string.Format(ID_PATTERN_TILE, currCursorPosition.PositionX, currCursorPosition.PositionY - 1)].GetComponent<TileController>().IsCursorHover = true;

                currCursorPosition.PositionY--;

            }
            else
            {
                tileMap[string.Format(ID_PATTERN_TILE, currCursorPosition.PositionX, currCursorPosition.PositionY)].GetComponent<TileController>().IsCursorHover = false;
                cursor.transform.SetParent(tileMap[string.Format(ID_PATTERN_TILE, currCursorPosition.PositionX, depth-1)].transform.parent);
                cursor.transform.localPosition = new Vector3(0, 3f, 0);
                tileMap[string.Format(ID_PATTERN_TILE, currCursorPosition.PositionX, depth-1)].GetComponent<TileController>().IsCursorHover = true;
                currCursorPosition.PositionY = depth - 1;


            }
        }
        return currCursorPosition;
    }   
    void intiCursorPosition(Transform position,TileController tileBlock)
    {
        cursor = new GameObject(CURSOR);
        cursor.transform.SetParent(position);
        cursor.transform.localPosition = new Vector3(0, 3f, 0);
        cursor = Instantiate(cursorPrefabs, cursor.transform);
        tileBlock.IsCursorHover = true;
        cursor.GetComponent<CursorController>().PositionX = 0;
        cursor.GetComponent<CursorController>().PositionY = 0;
    }
    void createHillTile(int widht,int depth,int limitHeight,int percentageHill,int treasureCount)
    {
        for (int i = 0; i < depth; i++) // row x
        {
            GameObject row = new GameObject(string.Format(ROW_PATTERN_TILE, i));
            row.transform.SetParent(terrain.transform);
            row.transform.localPosition = new Vector3(0, 0, i);

            for (int j = 0; j < widht; j++) // depth -> column z
            {
               int tileHeight = Random.Range(0, limitHeight);
               int ratio = Random.Range(0,101);
               tileHeight = 100 - percentageHill < ratio ? tileHeight : 0;// random height hill
                for (int w = 0; w <= tileHeight; w++) // height y
                {
                    GameObject tile = new GameObject();
                    tile.name = string.Format(NAME_PATTERN_TILE, i, w, j);
                    tile.transform.SetParent(row.transform);
                    tile.transform.localPosition = new Vector3(j, w, 0);
                    tile = Instantiate(tilePrefabs, tile.transform);
                    
                    TileController tileProfile = tile.GetComponent<TileController>();
                    tileProfile.TileId = w<tileHeight ?  NOT_REACHABLE : string.Format(ID_PATTERN_TILE, i, j);
                    tileProfile.Heightlvl = w;
                    tileProfile.IsWalkAble = w == tileHeight;
                    tileProfile.IsCursorSelect = false;

                    if(!tileProfile.TileId.Contains(NOT_REACHABLE)) tileMap.Add(tileProfile.TileId, tile);
             
                }

            }
        }
        //fix treasure pile
        for(int i  = 0;i < treasureCount; i++)
        {
            int xPosition = Random.Range(0, widht - 1);
            int yPosition = Random.Range(0, depth - 1);
            if(!tileMap[string.Format(ID_PATTERN_TILE, xPosition, yPosition)].GetComponent<TileController>().IsContainItem) 
                tileMap[string.Format(ID_PATTERN_TILE, xPosition, yPosition)].GetComponent<TileController>().IsContainItem = true;
            else
            {
                i--;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.W))
        {
            //Move Position Up
            cursor.GetComponent<CursorController>().PositionX  = cursorMove(cursor.GetComponent<CursorController>(),DIRECTIONS.UP).PositionX;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            //Move Position Down
            cursor.GetComponent<CursorController>().PositionX = cursorMove(cursor.GetComponent<CursorController>(), DIRECTIONS.DOWN).PositionX;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            //Move Position Left
            cursor.GetComponent<CursorController>().PositionY = cursorMove(cursor.GetComponent<CursorController>(), DIRECTIONS.LEFT).PositionY; ;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            //Move Position Right
            cursor.GetComponent<CursorController>().PositionY = cursorMove(cursor.GetComponent<CursorController>(), DIRECTIONS.RIGHT).PositionY; ;
        }
        if (Input.GetKeyDown(KeyCode.X))// Select Tile
        {
            int xPosition = cursor.GetComponent<CursorController>().PositionX;
            int yPosition = cursor.GetComponent<CursorController>().PositionY;

            tileMap[string.Format(ID_PATTERN_TILE, xPosition, yPosition)].GetComponent<TileController>().IsCursorHover = false;
            tileMap[string.Format(ID_PATTERN_TILE, xPosition, yPosition)].GetComponent<TileController>().IsCursorSelect = true;
        }
    }
}