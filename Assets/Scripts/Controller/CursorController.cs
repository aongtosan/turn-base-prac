using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject cursorPrefab;

    GameObject cursor;

    int positionX = 0;
    int positionY = 0;

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

    public void cursorMove(Dictionary<string,GameObject> tileMap, int maxWidth,int maxDepth, DirectionEnum.DIRECTIONS dir)
      {

          if (dir == DirectionEnum.DIRECTIONS.UP)
          {
              if ( PositionX + 1 <= maxWidth - 1)
              {
                  tileMap[string.Format(TileEnum.ID_PATTERN_TILE,  PositionX,  PositionY)].GetComponent<TileController>().IsCursorHover = false;

                  cursor.transform.SetParent(tileMap[string.Format(TileEnum.ID_PATTERN_TILE,  PositionX + 1,  PositionY)].transform.parent);
                  cursor.transform.localPosition = new Vector3(0, 3f, 0);

                  tileMap[string.Format(TileEnum.ID_PATTERN_TILE,  PositionX + 1,  PositionY)].GetComponent<TileController>().IsCursorHover = true;

                   PositionX++;

              }
              else
              {
                  tileMap[string.Format(TileEnum.ID_PATTERN_TILE,  PositionX,  PositionY)].GetComponent<TileController>().IsCursorHover = false;
                  cursor.transform.SetParent(tileMap[string.Format(TileEnum.ID_PATTERN_TILE, 0,  PositionY)].transform.parent);
                  cursor.transform.localPosition = new Vector3(0, 3f, 0);
                  tileMap[string.Format(TileEnum.ID_PATTERN_TILE, 0,  PositionY)].GetComponent<TileController>().IsCursorHover = true;
                   PositionX = 0;


              }
          }
          else if (dir == DirectionEnum.DIRECTIONS.DOWN)
          {
              if ( PositionX - 1 >= 0)
              {
                  tileMap[string.Format(TileEnum.ID_PATTERN_TILE,  PositionX,  PositionY)].GetComponent<TileController>().IsCursorHover = false;

                  cursor.transform.SetParent(tileMap[string.Format(TileEnum.ID_PATTERN_TILE,  PositionX - 1,  PositionY)].transform.parent);
                  cursor.transform.localPosition = new Vector3(0, 3f, 0);

                  tileMap[string.Format(TileEnum.ID_PATTERN_TILE,  PositionX - 1,  PositionY)].GetComponent<TileController>().IsCursorHover = true;

                   PositionX--;

              }
              else
              {
                  tileMap[string.Format(TileEnum.ID_PATTERN_TILE,  PositionX,  PositionY)].GetComponent<TileController>().IsCursorHover = false;
                  cursor.transform.SetParent(tileMap[string.Format(TileEnum.ID_PATTERN_TILE, maxWidth - 1,  PositionY)].transform.parent);
                  cursor.transform.localPosition = new Vector3(0, 3f, 0);
                  tileMap[string.Format(TileEnum.ID_PATTERN_TILE, maxWidth - 1,  PositionY)].GetComponent<TileController>().IsCursorHover = true;
                   PositionX = maxWidth - 1;


              }
          }
          else if (dir == DirectionEnum.DIRECTIONS.RIGHT)
          {
              if ( PositionY + 1 <= maxDepth - 1)
              {
                  tileMap[string.Format(TileEnum.ID_PATTERN_TILE,  PositionX,  PositionY)].GetComponent<TileController>().IsCursorHover = false;

                  cursor.transform.SetParent(tileMap[string.Format(TileEnum.ID_PATTERN_TILE,  PositionX,  PositionY + 1)].transform.parent);
                  cursor.transform.localPosition = new Vector3(0, 3f, 0);

                  tileMap[string.Format(TileEnum.ID_PATTERN_TILE,  PositionX,  PositionY + 1)].GetComponent<TileController>().IsCursorHover = true;

                   PositionY++;

              }
              else
              {
                  tileMap[string.Format(TileEnum.ID_PATTERN_TILE,  PositionX,  PositionY)].GetComponent<TileController>().IsCursorHover = false;
                  cursor.transform.SetParent(tileMap[string.Format(TileEnum.ID_PATTERN_TILE,  PositionX, 0)].transform.parent);
                  cursor.transform.localPosition = new Vector3(0, 3f, 0);
                  tileMap[string.Format(TileEnum.ID_PATTERN_TILE,  PositionX, 0)].GetComponent<TileController>().IsCursorHover = true;
                   PositionY = 0;


              }
          }
          else if (dir == DirectionEnum.DIRECTIONS.LEFT)
          {
              if ( PositionY - 1 >= 0)
              {
                  tileMap[string.Format(TileEnum.ID_PATTERN_TILE,  PositionX,  PositionY)].GetComponent<TileController>().IsCursorHover = false;

                  cursor.transform.SetParent(tileMap[string.Format(TileEnum.ID_PATTERN_TILE,  PositionX,  PositionY - 1)].transform.parent);
                  cursor.transform.localPosition = new Vector3(0, 3f, 0);

                  tileMap[string.Format(TileEnum.ID_PATTERN_TILE,  PositionX,  PositionY - 1)].GetComponent<TileController>().IsCursorHover = true;

                   PositionY--;

              }
              else
              {
                  tileMap[string.Format(TileEnum.ID_PATTERN_TILE,  PositionX,  PositionY)].GetComponent<TileController>().IsCursorHover = false;
                  cursor.transform.SetParent(tileMap[string.Format(TileEnum.ID_PATTERN_TILE,  PositionX, maxDepth - 1)].transform.parent);
                  cursor.transform.localPosition = new Vector3(0, 3f, 0);
                  tileMap[string.Format(TileEnum.ID_PATTERN_TILE,  PositionX, maxDepth - 1)].GetComponent<TileController>().IsCursorHover = true;
                   PositionY = maxDepth - 1;


              }
          }
          
      }
      public void intiCursorPosition(Dictionary<string,GameObject> tileMap)
      {

        cursor = new GameObject("CURSOR");
        cursor.transform.SetParent(tileMap[string.Format(TileEnum.ID_PATTERN_TILE, 0, 0)].transform.parent);
        cursor.transform.localPosition = new Vector3(0, 3f, 0);
        cursor = Instantiate(cursorPrefab, cursor.transform);
       
        tileMap[string.Format(TileEnum.ID_PATTERN_TILE,0, 0)].GetComponent<TileController>().IsCursorHover = true;

    
        
      }
}
