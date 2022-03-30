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

    [SerializeField]
    GameObject chestPrefab;

    string tileId;
    int heightlvl;
    const string NOT_REACHABLE = "Not Reachable";
    void Start()
    {
        if (isContainItem)
        {
            generateTreasure(new GameObject("treasure"));
        }
        Material tileColor = GetComponent<Renderer>().material;
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
        if (isCursorHover)
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
            tileColor.color = Color.green;
        }
        if (tileId.Contains(NOT_REACHABLE))
        {
            Material tileColor = GetComponent<Renderer>().material;
            tileColor.color = Color.red;
        }
    }
    public void generateTreasure(GameObject treasure)
    {  
        treasure.transform.SetParent(transform);
        treasure.transform.localPosition = new Vector3(0, 0.5f, 0);
        Instantiate(chestPrefab, treasure.transform);
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
    public int Heightlvl
    {
        get { return heightlvl; }
        set {heightlvl = value; }
    }
}
