using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject cursorPrefab;

    GameObject cursor;
    TileController selectedTile = null;
    UnitController selectedUnit = null;

    int positionX = 0; // up down
    int positionY = 0; // left Right

    public int PositionX
    {
        get { return positionX; }
        set { positionX = value; }
    }
    public int PositionY
    {
        get { return positionY; }
        set { positionY = value; }
    }

    public void cursorMove( Dictionary<string,GameObject> tileMap, int maxDepth,int maxWidth, DIRECTIONS dir)
      {
          int currentPositionX = PositionX;
          int currentPositionY = PositionY;

          PositionY += dir == DIRECTIONS.UP ? 1 :  dir == DIRECTIONS.DOWN ? -1 : 0;
          PositionX += dir == DIRECTIONS.RIGHT ? 1 :  dir == DIRECTIONS.LEFT ? -1 : 0;
          
          if(dir == DIRECTIONS.LEFT || dir == DIRECTIONS.RIGHT){
                if ( PositionX  <= maxWidth - 1  && PositionX >= 0)
              {
                    tileMap[string.Format(TileEnum.ID_PATTERN_TILE,  currentPositionX,  PositionY)].GetComponent<TileController>().IsCursorHover = false;

                    cursor.transform.parent.SetParent(tileMap[string.Format(TileEnum.ID_PATTERN_TILE,  PositionX,  PositionY)].transform.parent);              
                    cursor.transform.parent.localPosition = new Vector3(0, 3f, 0);  
                    tileMap[string.Format(TileEnum.ID_PATTERN_TILE,  PositionX,  PositionY)].GetComponent<TileController>().IsCursorHover = true;
                    //Debug.Log(string.Format(TileEnum.ID_PATTERN_TILE,  PositionX,  PositionY));

              }
              else if(PositionX < 0 || PositionX > maxWidth - 1 )
              {
                    tileMap[string.Format(TileEnum.ID_PATTERN_TILE,  currentPositionX,  PositionY)].GetComponent<TileController>().IsCursorHover = false;                
               

                    int overflowIndexX = PositionX < 0 ? maxWidth - 1 : 0;  
                    PositionX = overflowIndexX;
                    cursor.transform.parent.SetParent(tileMap[string.Format(TileEnum.ID_PATTERN_TILE,  PositionX,  PositionY)].transform.parent);
                    cursor.transform.parent.localPosition = new Vector3(0, 3f, 0);  
                    tileMap[string.Format(TileEnum.ID_PATTERN_TILE, overflowIndexX,  PositionY)].GetComponent<TileController>().IsCursorHover = true;
                    //Debug.Log(string.Format(TileEnum.ID_PATTERN_TILE,  PositionX,  PositionY));

              }
        }else{
            if ( PositionY <= maxDepth - 1 && PositionY >= 0)
              {
                  tileMap[string.Format(TileEnum.ID_PATTERN_TILE,  PositionX,  currentPositionY)].GetComponent<TileController>().IsCursorHover = false;
                  cursor.transform.parent.SetParent(tileMap[string.Format(TileEnum.ID_PATTERN_TILE,  PositionX,  PositionY)].transform.parent);
                  cursor.transform.parent.localPosition = new Vector3(0, 3f, 0);  

                  tileMap[string.Format(TileEnum.ID_PATTERN_TILE,  PositionX,  PositionY)].GetComponent<TileController>().IsCursorHover = true;
                  //Debug.Log(string.Format(TileEnum.ID_PATTERN_TILE,  PositionX,  PositionY));

              }
              else if(PositionY < 0 || PositionY > maxDepth -1)
              {
                  tileMap[string.Format(TileEnum.ID_PATTERN_TILE,  PositionX,  currentPositionY)].GetComponent<TileController>().IsCursorHover = false;

                  int overflowIndexY = PositionY < 0 ? maxDepth - 1 : 0; 
                  PositionY = overflowIndexY;
                  cursor.transform.parent.SetParent(tileMap[string.Format(TileEnum.ID_PATTERN_TILE,  PositionX,  PositionY)].transform.parent);
                  cursor.transform.parent.localPosition = new Vector3(0, 3f, 0);  

                  tileMap[string.Format(TileEnum.ID_PATTERN_TILE,  PositionX, overflowIndexY)].GetComponent<TileController>().IsCursorHover = true;
                  //Debug.Log(string.Format(TileEnum.ID_PATTERN_TILE,  PositionX,  PositionY));
                  
              }

        }
          
      }
      public void intiCursorPosition(Dictionary<string,GameObject> tileMap,int initpositionX,int initpositionY)
      {
        cursorPrefab = Resources.Load("Prefabs/cursorMark") as GameObject ;
        cursorPrefab.AddComponent<CursorController>();
        cursor = new GameObject("CURSOR");
        cursor.tag = "Cursor";
        cursor.transform.SetParent(tileMap[string.Format(TileEnum.ID_PATTERN_TILE, initpositionX, initpositionY)].transform.parent);
        cursor.transform.localPosition = new Vector3(0, 3f, 0);
        cursor = Instantiate(cursorPrefab, cursor.transform);
       
        tileMap[string.Format(TileEnum.ID_PATTERN_TILE,0, 0)].GetComponent<TileController>().IsCursorHover = true;

      }
    public TileController SelectedTile
    {
        set { selectedTile = value; }
        get { return selectedTile; }
    }
    public UnitController SelectedUnit
    {
        set { selectedUnit = value; }
        get { return selectedUnit; }
    }
}
