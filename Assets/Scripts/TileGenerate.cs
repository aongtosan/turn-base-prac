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

    private GameObject terrain;
    private Dictionary<string,GameObject> tileMap;
    List<GameObject> tileList;

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
        //intiCursorPosition(initCursorLoc);

        //cursorMove();
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < depth; j++)
            {
                Debug.Log(tileMap[string.Format(ID_PATTERN_TILE, i, j)].GetComponent<TileController>().TileId);
            }
        }
    }

    void cursorMove(Transform changeTile, TileController currtileBlock, TileController nexttileBlock)
    {
       
    }
    void intiCursorPosition(Transform position,TileController tileBlock)
    {
        GameObject cursor = new GameObject(CURSOR);
        cursor.transform.SetParent(position);
        cursor.transform.localPosition = new Vector3(0, 3f, 0);
        cursor = Instantiate(cursorPrefabs, cursor.transform);
        tileBlock.IsCursorSelect = true;
        Debug.Log("selected tile => "+tileBlock.TileId);
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
               tileHeight = 100 - percentageHill < ratio ? Random.Range(1, tileHeight+1) : 0;// random height hill
               //int tresholdTreasureTile = widht * (depth / 2);
                for (int w = 0; w <= tileHeight; w++) // height y
                {
                    GameObject tile = new GameObject();
                    tile.name = string.Format(NAME_PATTERN_TILE, i, w, j);
                    tile.transform.SetParent(row.transform);
                    tile.transform.localPosition = new Vector3(j, w, 0);
                    tile = Instantiate(tilePrefabs, tile.transform);
                    
                    TileController tileProfile = tile.GetComponent<TileController>();
                    tileProfile.TileId = w<tileHeight ?  "Not Reachable" : string.Format(ID_PATTERN_TILE, i, j);
                    tileProfile.Heightlvl = w;
                    tileProfile.IsWalkAble = w == tileHeight;
                    tileProfile.IsCursorSelect = false;

                    if(!tileProfile.TileId.Contains(NOT_REACHABLE)) tileMap.Add(tileProfile.TileId, tile);
             
                }

            }
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}