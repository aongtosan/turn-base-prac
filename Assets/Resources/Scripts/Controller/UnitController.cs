using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class UnitController : MonoBehaviour
{
    // Start is called before the first frame update
    
    [SerializeField]
    StatsInfo statData;
    int positionX;
    int positionY;
    int actionPoint;
    bool moveComplete;
    
    ActionType doAction;
    List<Tile> pathTile  ;
    List<Tile> possibleTile;
    public void Awake(){
        pathTile = new List<Tile>();
        moveComplete = false;
        doAction = ActionType.NONE;
        possibleTile = new List<Tile>();
    }
    public void Update(){

    }
    public void action(ActionType act,GameObject onWalkingUnit,CursorController cursor,Dictionary<string,GameObject> tileMap,TileGenerate tileInfo){
        if(act == ActionType.MOVE){
            //moveUnitToTile(onWalkingUnit,cursor,tileMap,tileInfo);
        }else  if(act == ActionType.ATTACK){
            
        }else  if(act == ActionType.ABILITY){
            
        }
    }
    public void attack(){

    }
    public void useAbilty(){
      
    }
    public void clearTilesState(Dictionary<string,GameObject> tileMap,int stateWidth,int stateDepth){
        foreach(Tile t in possibleTile)
        {
            tileMap[string.Format(TileEnum.ID_PATTERN_TILE, t.x, t.y)].GetComponent<TileController>().IsWalkAble = false;
        }
        possibleTile.Clear();
    }
    public void initUnitPositionTile(GameObject unit,Dictionary<string,GameObject> tileMap,int initpositionX,int initPositionY){
            transform.SetParent(tileMap[string.Format(TileEnum.ID_PATTERN_TILE,initpositionX,initPositionY)].transform);
            transform.localPosition = new Vector3(0f,.75f,0f);
            tileMap[string.Format(TileEnum.ID_PATTERN_TILE,initpositionX,initPositionY)].GetComponent<TileController>().IsUnitOnTile = true;
            tileMap[string.Format(TileEnum.ID_PATTERN_TILE,initpositionX,initPositionY)].GetComponent<TileController>().UnitOnTile = unit;
            positionX = initpositionX;
            positionY = initPositionY;
    }
    public void teleportToTile(GameObject onWalkingUnit,CursorController cursor,Dictionary<string,GameObject> tileMap,TileGenerate tileInfo){
        pathTile.Add(new Tile(cursor.PositionX, cursor.PositionY));
        StartCoroutine(delayMovement(onWalkingUnit, tileMap, tileInfo));
    }
    public void walkToTile(GameObject onWalkingUnit,CursorController cursor,Dictionary<string,GameObject> tileMap,TileGenerate tileInfo){
        Debug.Log("walk");     
    }
    public void flyToTile(GameObject onWalkingUnit,CursorController cursor,Dictionary<string,GameObject> tileMap,TileGenerate tileInfo){
        Debug.Log("fly");
        pathTile = findingPath(positionX, positionY, cursor.PositionX, cursor.PositionY, pathTile);
        StartCoroutine( delayMovement(onWalkingUnit, tileMap, tileInfo) );
    }
    public void moveToTile(GameObject onWalkingUnit,Tile targetTile,Dictionary<string,GameObject> tileMap,TileGenerate tileInfo){
            tileMap[string.Format(TileEnum.ID_PATTERN_TILE,PositionX,PositionY)].GetComponent<TileController>().IsUnitOnTile = false;
            tileMap[string.Format(TileEnum.ID_PATTERN_TILE, PositionX, PositionY)].GetComponent<TileController>().UnitOnTile = null;
            tileMap[string.Format(TileEnum.ID_PATTERN_TILE, targetTile.x, targetTile.y)].GetComponent<TileController>().IsUnitOnTile = true;
            tileMap[string.Format(TileEnum.ID_PATTERN_TILE, targetTile.x, targetTile.y)].GetComponent<TileController>().UnitOnTile =  onWalkingUnit   ;
            onWalkingUnit.GetComponent<UnitController>().PositionX = targetTile.x;
            onWalkingUnit.GetComponent<UnitController>().PositionY = targetTile.y;
            onWalkingUnit.transform.parent = tileMap[string.Format(TileEnum.ID_PATTERN_TILE, targetTile.x, targetTile.y)].transform;
            onWalkingUnit.transform.localPosition = new Vector3 (0,0.75f,0);
    }
    public void moveUnit(GameObject onWalkingUnit,CursorController cursor,Dictionary<string,GameObject> tileMap,TileGenerate tileInfo){
           
           
            if(StatsInfo.MOVE_TYPE.TELEPORT == statData.MOVETYPE){
                teleportToTile(onWalkingUnit,cursor,tileMap,tileInfo);
            }
            if(StatsInfo.MOVE_TYPE.WALK == statData.MOVETYPE){
                walkToTile(onWalkingUnit,cursor,tileMap,tileInfo);
            }
            if(StatsInfo.MOVE_TYPE.FLY == statData.MOVETYPE){
                flyToTile(onWalkingUnit,cursor,tileMap,tileInfo);     
            }
            moveComplete = true;
            clearTilesState(tileMap,tileInfo.width,tileInfo.depth);
           
    
    }
    public void findMovableTile( Dictionary<string , GameObject> tileMap,int tileWidth,int tileDepth){
        /* int predictNextPositionX = positionX;//up
         int predictNextPositionY = positionY;//right
         for(int i = 0; i<=statData.Move;i++){      
                 if(predictNextPositionX<tileWidth) tileMap[string.Format(TileEnum.ID_PATTERN_TILE,predictNextPositionX,predictNextPositionY)].GetComponent<TileController>().IsWalkAble=true;
                 for(int j=0;j<=statData.Move-i;j++ ){
                 if(statData.MOVETYPE != StatsInfo.MOVE_TYPE.WALK)
                 {
                     if (predictNextPositionY + j < tileDepth)
                     {
                         if (predictNextPositionX < tileWidth)
                         {
                             tileMap[string.Format(TileEnum.ID_PATTERN_TILE, predictNextPositionX, predictNextPositionY + j)].GetComponent<TileController>().IsWalkAble = true;
                             possibleTile.Add(new Tile(predictNextPositionX, predictNextPositionY + j));
                         }
                         if (positionX - i >= 0)
                         {
                             tileMap[string.Format(TileEnum.ID_PATTERN_TILE, positionX - i, predictNextPositionY + j)].GetComponent<TileController>().IsWalkAble = true;
                             possibleTile.Add(new Tile(positionX - i, predictNextPositionY + j));
                         }
                     }
                     if (predictNextPositionY - j >= 0)
                     {
                         if (predictNextPositionX < tileWidth)
                         {
                             tileMap[string.Format(TileEnum.ID_PATTERN_TILE, predictNextPositionX, predictNextPositionY - j)].GetComponent<TileController>().IsWalkAble = true;
                             possibleTile.Add(new Tile(predictNextPositionX, predictNextPositionY - j));
                         }
                         if (positionX - i >= 0)
                         {
                             tileMap[string.Format(TileEnum.ID_PATTERN_TILE, positionX - i, predictNextPositionY - j)].GetComponent<TileController>().IsWalkAble = true;
                             possibleTile.Add(new Tile(positionX - i, predictNextPositionY - j));
                         }
                     }
                 }
                 else
                 {
                     if (predictNextPositionY + j < tileDepth)
                     {
                         if (predictNextPositionX < tileWidth
                             && tileMap[string.Format(TileEnum.ID_PATTERN_TILE, predictNextPositionX, predictNextPositionY + j)]
                             .GetComponent<TileController>().Heightlvl<= statData.Jump
                             )
                         {
                             tileMap[string.Format(TileEnum.ID_PATTERN_TILE, predictNextPositionX, predictNextPositionY + j)].GetComponent<TileController>().IsWalkAble = true;
                             possibleTile.Add(new Tile(predictNextPositionX, predictNextPositionY + j));
                         }
                         if (
                             positionX - i >= 0
                             && tileMap[string.Format(TileEnum.ID_PATTERN_TILE, positionX - i, predictNextPositionY + j)]
                             .GetComponent<TileController>().Heightlvl <= statData.Jump
                             )
                         {
                             tileMap[string.Format(TileEnum.ID_PATTERN_TILE, positionX - i, predictNextPositionY + j)].GetComponent<TileController>().IsWalkAble = true;
                             possibleTile.Add(new Tile(positionX - i, predictNextPositionY + j));
                         }
                     }
                     if (predictNextPositionY - j >= 0)
                     {
                         if (
                             predictNextPositionX < tileWidth
                             && tileMap[string.Format(TileEnum.ID_PATTERN_TILE, predictNextPositionX, predictNextPositionY - j)]
                             .GetComponent<TileController>().Heightlvl <= statData.Jump
                             )
                         {
                             tileMap[string.Format(TileEnum.ID_PATTERN_TILE, predictNextPositionX, predictNextPositionY - j)].GetComponent<TileController>().IsWalkAble = true;
                             possibleTile.Add(new Tile(predictNextPositionX, predictNextPositionY - j));
                         }
                         if (
                              positionX - i >= 0
                              && tileMap[string.Format(TileEnum.ID_PATTERN_TILE, positionX - i, predictNextPositionY - j)]
                             .GetComponent<TileController>().Heightlvl <= statData.Jump
                             )
                         {
                             tileMap[string.Format(TileEnum.ID_PATTERN_TILE, positionX - i, predictNextPositionY - j)].GetComponent<TileController>().IsWalkAble = true;
                             possibleTile.Add(new Tile(positionX - i, predictNextPositionY - j));
                         }
                     }
                 }

                 }
                 predictNextPositionX++;

         }*/
        getPossibleTile(tileWidth, tileDepth , StatData.Move , new Tile(positionX,positionY),tileMap);
        /*foreach (Tile t in possibleTile.Distinct().ToList())
               {
                   tileMap[string.Format(TileEnum.ID_PATTERN_TILE, t.x, t.y)].GetComponent<TileController>().IsWalkAble = true;
                   Debug.Log(t.getLocation());
               }*/
        StartCoroutine(delayAnimation(tileMap));
        //Debug.Log(possibleTile.Distinct().ToList().Count);

    }
    public List<Tile> findingPath(int unitLocationX,int unitLocationY,int xTargetLocation,int yTargetLocation,List<Tile> path){
        
        if(unitLocationX<xTargetLocation){
            path.Add(new Tile(unitLocationX+1,unitLocationY));
            return findingPath(unitLocationX+1,unitLocationY,xTargetLocation,yTargetLocation, path)  ;
        }else if(unitLocationX>xTargetLocation){
           path.Add(new Tile(unitLocationX-1,unitLocationY));
            return findingPath(unitLocationX-1,unitLocationY,xTargetLocation,yTargetLocation,path);
        }
        else if(unitLocationY>yTargetLocation){
           pathTile.Add(new Tile(unitLocationX,unitLocationY-1));
            return findingPath(unitLocationX,unitLocationY-1,xTargetLocation,yTargetLocation,path);
        }
        else if(unitLocationY<yTargetLocation){
            path.Add(new Tile(unitLocationX,unitLocationY+1));
            return findingPath(unitLocationX,unitLocationY+1,xTargetLocation,yTargetLocation,path);
        }
        return path;
    }
    public void getPossibleTile(int tileWidht,int tileDepth ,int moveCount,Tile currentTile, Dictionary<string, GameObject> tileMap)
    {
   
        if(moveCount == -1)
        {
            return;
        }
        else
        {
            moveCount--;
            //Up
            if (currentTile.y + 1 < tileDepth)
                {
                   
                    if (statData.MOVETYPE != StatsInfo.MOVE_TYPE.WALK)
                    {
                        currentTile.onTopTile = new Tile(currentTile.x, currentTile.y + 1);
                        possibleTile.Add(currentTile);
                        getPossibleTile(tileWidht, tileDepth, moveCount, currentTile.onTopTile, tileMap);
                    }
                    else
                    {
                        if(StatData.Jump + tileMap[string.Format(TileEnum.ID_PATTERN_TILE, currentTile.x, currentTile.y)]
                            .GetComponent<TileController>().Heightlvl
                            >= tileMap[string.Format(TileEnum.ID_PATTERN_TILE, currentTile.x, currentTile.y + 1)].GetComponent<TileController>().Heightlvl)
                        {
                            currentTile.onTopTile = new Tile(currentTile.x, currentTile.y + 1);
                            possibleTile.Add(currentTile);
                            getPossibleTile(tileWidht, tileDepth, moveCount, currentTile.onTopTile, tileMap);
                        }
                    }
                    //getPossibleTile(tileWidht, tileDepth, moveCount, currentTile.onTopTile,tileMap);
                    //Debug.Log(currentTile.onTopTile.getLocation());
                }   
            //Right
                if (currentTile.x + 1 < tileWidht)
                {
                   
                    if (statData.MOVETYPE != StatsInfo.MOVE_TYPE.WALK)
                    {
                        currentTile.onRightTile = new Tile(currentTile.x + 1, currentTile.y);
                        possibleTile.Add(currentTile);
                        getPossibleTile(tileWidht, tileDepth, moveCount, currentTile.onRightTile, tileMap);
                    }
                    else
                    {
                        if (StatData.Jump + tileMap[string.Format(TileEnum.ID_PATTERN_TILE, currentTile.x, currentTile.y)]
                           .GetComponent<TileController>().Heightlvl
                           >= tileMap[string.Format(TileEnum.ID_PATTERN_TILE, currentTile.x+1, currentTile.y)].GetComponent<TileController>().Heightlvl)
                        {
                            currentTile.onRightTile = new Tile(currentTile.x + 1, currentTile.y);
                            possibleTile.Add(currentTile);
                            getPossibleTile(tileWidht, tileDepth, moveCount, currentTile.onRightTile, tileMap);
                        }
                    }
                }
            //Down
                if (currentTile.y - 1 < tileDepth && currentTile.y - 1 >= 0)
                {
                    if (statData.MOVETYPE != StatsInfo.MOVE_TYPE.WALK)
                    {
                        currentTile.onDownTile = new Tile(currentTile.x, currentTile.y - 1);
                        possibleTile.Add(currentTile);
                        getPossibleTile(tileWidht, tileDepth, moveCount, currentTile.onDownTile, tileMap);
                    }
                    else
                    {
                        if (StatData.Jump + tileMap[string.Format(TileEnum.ID_PATTERN_TILE, currentTile.x, currentTile.y)]
                          .GetComponent<TileController>().Heightlvl
                          >= tileMap[string.Format(TileEnum.ID_PATTERN_TILE, currentTile.x , currentTile.y - 1)].GetComponent<TileController>().Heightlvl)
                        {
                            currentTile.onDownTile = new Tile(currentTile.x, currentTile.y - 1);
                            possibleTile.Add(currentTile);
                            getPossibleTile(tileWidht, tileDepth, moveCount, currentTile.onDownTile, tileMap);
                        }
                    }
            }
            //Left
            if (currentTile.x - 1 < tileWidht && currentTile.x - 1 >= 0)
            {
               
                if (statData.MOVETYPE != StatsInfo.MOVE_TYPE.WALK)
                {
                    currentTile.onLeftTile = new Tile(currentTile.x - 1, currentTile.y);
                    possibleTile.Add(currentTile);
                    getPossibleTile(tileWidht, tileDepth, moveCount, currentTile.onLeftTile, tileMap);
                }
                else
                {
                    if (StatData.Jump + tileMap[string.Format(TileEnum.ID_PATTERN_TILE, currentTile.x, currentTile.y)]
                          .GetComponent<TileController>().Heightlvl
                          >= tileMap[string.Format(TileEnum.ID_PATTERN_TILE, currentTile.x - 1, currentTile.y)].GetComponent<TileController>().Heightlvl)
                    {
                        currentTile.onLeftTile = new Tile(currentTile.x - 1, currentTile.y);
                        possibleTile.Add(currentTile);
                        getPossibleTile(tileWidht, tileDepth, moveCount, currentTile.onLeftTile, tileMap);
                    }
                }
            }
            
        }

    }
    public IEnumerator delayMovement(GameObject onWalkingUnit, Dictionary<string, GameObject> tileMap, TileGenerate tileInfo)
    {
        
          foreach(Tile t in pathTile){
            yield return new WaitForSeconds(0.2f);
            moveToTile(onWalkingUnit, t, tileMap, tileInfo);
        }
        pathTile.Clear();
        

    }
    public IEnumerator delayAnimation(Dictionary<string, GameObject> tileMap)
    {
            foreach (Tile t in possibleTile)
            {
                yield return new WaitForSeconds(0.01f);
                tileMap[string.Format(TileEnum.ID_PATTERN_TILE, t.x, t.y)].GetComponent<TileController>().IsWalkAble = true;
                Debug.Log(t.getLocation());
            }
    }
    public int PositionX{
        set{ positionX = value;  }
        get{ return positionX; }
    }
    public int PositionY{
        set{ positionY = value;  }
        get{ return positionY; }
    }
    public bool MoveComplete{
        set{ moveComplete = value; }
        get{ return moveComplete; }
    }
    public int ActionPoint{
        set{actionPoint = value;}
        get{return actionPoint;}
    }
    public StatsInfo StatData{
        set{statData = value;}
        get{return statData;}
    }
    public ActionType DoAction{
         set{doAction = value;}
        get{return doAction;}
    }
}
