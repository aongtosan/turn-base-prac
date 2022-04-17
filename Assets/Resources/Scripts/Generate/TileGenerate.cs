using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TileGenerate : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject tilePrefabs;

    
   public int width;
   
   public int depth;
   
   public int height;
   
   public int hillPercentage;
   
   public int treasurePile;


    
    private GameObject terrain;
    private Dictionary<string,GameObject> tileMap;
    

    private void Awake()
    {
        terrain = new GameObject(TileEnum.BASE_PLANE_NAME);
        terrain.transform.SetParent(transform);
        terrain.transform.localPosition = Vector3.zero;
        tileMap = new Dictionary<string, GameObject>();

        tileMap = new Dictionary<string, GameObject> ( createHillTile(width, depth, height, hillPercentage, treasurePile) ) ;
      

    }

  public Dictionary<string, GameObject> createHillTile(int widht,int depth,int limitHeight,int percentageHill,int treasureCount)
    {

        Dictionary<string, GameObject> tileState = new Dictionary<string, GameObject>();
        for (int i = 0; i < depth; i++) // row x
        {
            GameObject row = new GameObject(string.Format(TileEnum.ROW_PATTERN_TILE, i));
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
                    tile.name = string.Format(TileEnum.NAME_PATTERN_TILE, i, w, j);
                    tile.transform.SetParent(row.transform);
                    tile.transform.localPosition = new Vector3(j, w, 0);
                    tile = Instantiate(tilePrefabs, tile.transform);
                    
                    TileController tileProfile = tile.GetComponent<TileController>();
                    tileProfile.TileId = w < tileHeight ?  TileEnum.NOT_REACHABLE : string.Format(TileEnum.ID_PATTERN_TILE, i, j);
                    tileProfile.Heightlvl = w;
                    tileProfile.IsWalkAble = w == tileHeight;
                    tileProfile.IsCursorSelect = false;

                    if (!tileProfile.TileId.Contains(TileEnum.NOT_REACHABLE)) {
                        tileState.Add(tileProfile.TileId, tile);
                    }

                }

            }
        }
        //fix treasure pile
        for(int i  = 0;i < treasureCount; i++)
        {
            int xPosition = Random.Range(0, widht - 1);
            int yPosition = Random.Range(0, depth - 1);
            if(!tileState[string.Format(TileEnum.ID_PATTERN_TILE, xPosition, yPosition)].GetComponent<TileController>().IsContainItem) 
                tileState[string.Format(TileEnum.ID_PATTERN_TILE, xPosition, yPosition)].GetComponent<TileController>().IsContainItem = true;
            else
            {
                i--;
            }
        }

        return tileState;
    }
    // Update is called once per frame
     public Dictionary<string, GameObject> getMapInfo()
    {
        return tileMap;
    }
    public int Depth()
    {
        return depth;
    }
    public int Width()
    {
        return width;
    }
}