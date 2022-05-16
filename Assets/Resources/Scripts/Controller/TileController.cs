using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileController : MonoBehaviour
{
    // Start is called before the first frame update
    bool isWalkAble;
    bool isContainItem;
    bool isCursorSelect;
    bool isCursorHover;
    bool isUnitOnTile;
    GameObject unitOntile;
    GameObject chestPrefab;

    string tileId;
    int heightlvl;
   
    void Start()
    {
        Material tileColor = GetComponent<Renderer>().material;
        if (isContainItem)
        {
            generateTreasure(new GameObject("treasure"));
        }
        //Material tileColor = GetComponent<Renderer>().material;
        if (isContainItem)
        {
            tileColor.color = new Color32(253, 166, 76, 1);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isContainItem)
        {
            Material tileColor = GetComponent<Renderer>().material;
            tileColor.color = new Color32(253, 166, 76, 1);
        }
        else if(isWalkAble){
            Material tileColor = GetComponent<Renderer>().material;
            tileColor.color = new Color32(0, 105, 148,1);
        }
        else if (isCursorHover)
        {
            Material tileColor = GetComponent<Renderer>().material;
            tileColor.color = new Color32(41, 81, 243, 1);
        }
        else if (!isCursorHover)
        {
            Material tileColor = GetComponent<Renderer>().material;
            tileColor.color = Color.white;
        }
        if (isCursorSelect)
        {
            Material tileColor = GetComponent<Renderer>().material;
            tileColor.color = new Color32(60, 179, 113,1);
        }
        if (tileId.Contains(TileEnum.NOT_REACHABLE))
        {
            Material tileColor = GetComponent<Renderer>().material;
            tileColor.color = Color.black;
        }
    }
    public void generateTreasure(GameObject treasure)
    {  
        treasure.transform.SetParent(transform);
        treasure.transform.localPosition = new Vector3(0, 0.5f, 0);
        Instantiate(chestPrefab, treasure.transform);
    }
    public GameObject UnitOnTile
    {
        set { unitOntile = value; }
        get { return unitOntile; }
    }
    public string TileId
    {
        get{ return tileId; }
        set{tileId = value; }
    }
    public bool IsWalkAble
    {
        get { return isWalkAble; }
        set { isWalkAble = value; }
    }
    public bool IsContainItem
    {
        get { return isContainItem; }
        set { isContainItem = value; }
    }
    public bool IsCursorSelect
    {
        get { return isCursorSelect; }
        set { isCursorSelect = value; }
    }
    public bool IsCursorHover
    {
        get { return isCursorHover; }
        set { isCursorHover = value; }
    }
    public bool IsUnitOnTile
    {
        get { return isUnitOnTile; }
        set { isUnitOnTile = value; }
    }
    public int Heightlvl
    {
        get { return heightlvl; }
        set {heightlvl = value; }
    }
    public GameObject ItemSet{
        get{ return chestPrefab; }
        set{ chestPrefab = value; }

    }
}
