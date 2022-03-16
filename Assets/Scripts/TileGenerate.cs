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
    int height;
    [SerializeField]
    int depth;
  
    private GameObject terrain;

    private void Awake()
    {
        terrain = new GameObject("Terrain");
        terrain.transform.SetParent(transform);
        terrain.transform.localPosition = Vector3.zero;
    }

    void Start()
    {
        for (int i = 0; i < depth; i++)
        {
            GameObject row = new GameObject(string.Format("row[{0}]",i));
            row.transform.SetParent(terrain.transform);
            row.transform.localPosition = new Vector3(0, 0, i);

            for (int j = 0; j < width; j++)
            {
                GameObject tile = new GameObject( string.Format("Tile[{0}]",j));
                tile.transform.SetParent(row.transform);
                tile.transform.localPosition = new Vector3(j, 0, 0);
                tile = Instantiate(tilePrefabs, tile.transform);
                TileController setWalkable = tile.GetComponent<TileController>();
                //setWalkable.IsWalkAble = Random.Range(0,2) == 1 ? true : false ;
               
            }
        }
        terrain.transform.Translate(-(width / 2), terrain.transform.position.y, depth / 2);
    }

    // Update is called once per frame
    void Update()
    {

    }
}