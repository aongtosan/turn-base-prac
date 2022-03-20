using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGenerate : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject tilePrefabs;
    [SerializeField]
    int width;
    [SerializeField]
    int depth;
    [SerializeField]
    int height;
    [SerializeField]
    int hillPercentage;
  
    private GameObject terrain;
    private List<GameObject> tileList;

    private void Awake()
    {
        terrain = new GameObject("Terrain");
        terrain.transform.SetParent(transform);
        terrain.transform.localPosition = Vector3.zero;
        tileList = new List<GameObject>();
    }

    void Start()
    {
        createHillTile(width, depth, height,hillPercentage);
       
    }
    void createHillTile(int widht,int depth,int limitHeight,int percentageHill)
    {
        for (int i = 0; i < depth; i++)
        {
            GameObject row = new GameObject(string.Format("row[{0}]", i));
            row.transform.SetParent(terrain.transform);
            row.transform.localPosition = new Vector3(0, 0, i);

            for (int j = 0; j < widht; j++)
            {
               int tileHeight = Random.Range(0, limitHeight);
               int ratio = Random.Range(0,101);
               tileHeight = 100 - percentageHill < ratio ? tileHeight : 0;
                for (int w = 0; w <= tileHeight; w++)
                {
                    GameObject tile = new GameObject();
                    tile.name = string.Format("Tile[{0}][{1}][{2}]", i, w, j);
                    tile.transform.SetParent(row.transform);
                    tile.transform.localPosition = new Vector3(j, w, 0);
                    tile = Instantiate(tilePrefabs, tile.transform);
                    
                   TileController tileProfile = tile.GetComponent<TileController>();
                   tileProfile.Heightlvl = w;
                   tileProfile.IsWalkAble = w<tileHeight? false:true;
                    tileProfile.IsReachAble = tileProfile.IsWalkAble ? tileProfile.IsWalkAble : false;
                }

            }
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}