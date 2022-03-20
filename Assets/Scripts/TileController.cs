using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileController : MonoBehaviour
{
    // Start is called before the first frame update
    bool isWalkAble;
    bool isReachAble;
    bool isContainItem;
   [SerializeField]
    GameObject chestPrefab;
    int heightlvl;
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
        else if (isReachAble)
        {   
            tileColor.color = new Color32(41, 81, 243,1);
        }
        else
        {
            tileColor.color = Color.red;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void generateTreasure(GameObject treasure)
    {  
        treasure.transform.SetParent(transform);
        treasure.transform.localPosition = new Vector3(0, 0.5f, 0);
        Instantiate(chestPrefab, treasure.transform);
    }
    public bool IsWalkAble
    {
        get { return isWalkAble; }
        set { isWalkAble = value; }
    }
    public bool IsReachAble
    {
        get { return isReachAble; }
        set { isReachAble = value; }
    }
    public bool IsContainItem
    {
        get { return isContainItem; }
        set { isContainItem = value; }
    }
    public int Heightlvl
    {
        get { return heightlvl; }
        set {heightlvl = value; }
    }
}
