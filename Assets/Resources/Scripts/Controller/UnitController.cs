using System.Collections;
using System.Collections.Generic;
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
    List<Tile> visitedTile;
    Tile masterTile;
    public void Awake(){
        pathTile = new List<Tile>();
        moveComplete = false;
        doAction = ActionType.NONE;
        possibleTile = new List<Tile>();
        visitedTile = new List<Tile>();
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
        /*foreach(Tile t in possibleTile)
        {
            tileMap[string.Format(TileEnum.ID_PATTERN_TILE, t.x, t.y)].GetComponent<TileController>().IsWalkAble = false;
        }*/
        setMoveAbleTile(masterTile, tileMap, false);
        possibleTile.Clear();
        visitedTile.Clear();
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
        //pathTile = findingPath(positionX, positionY, cursor.PositionX, cursor.PositionY, pathTile);
        getWalkPath(cursor, positionX, positionY,masterTile);
        setMoveAbleTile(masterTile, tileMap, false);
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
    public void findMovableTile( Dictionary<string , GameObject> tileMap,int tileWidth,int tileDepth){//-- commit remove old find movable tile
        masterTile = new Tile(positionX, positionY);
        getPossibleTile(tileWidth, tileDepth , StatData.Move , masterTile, tileMap);
        setMoveAbleTile(masterTile, tileMap ,true);
        /*foreach (Tile t in possibleTile.Distinct().ToList())
               {
                   tileMap[string.Format(TileEnum.ID_PATTERN_TILE, t.x, t.y)].GetComponent<TileController>().IsWalkAble = true;
                   //Debug.Log(t.getLocation());
               }*/
        
        //StartCoroutine(delayAnimation2(tileMap,masterTile,true));
        //Debug.Log(possibleTile.Distinct().ToList().Count);

    }
    public void setMoveAbleTile(Tile t, Dictionary<string, GameObject> tileMap,bool flag)
    {
        if(t == null)
        {

        }
        else
        {
           //if (t.x >=0 && t.y >=0) 
            tileMap[string.Format(TileEnum.ID_PATTERN_TILE, t.x, t.y)].GetComponent<TileController>().IsWalkAble = flag;
            setMoveAbleTile(t.onTopTile,tileMap,flag);
            setMoveAbleTile(t.onRightTile, tileMap,flag);   
            setMoveAbleTile(t.onLeftTile, tileMap, flag);
            setMoveAbleTile(t.onDownTile, tileMap, flag);
        }
    }
    public void getWalkPath(CursorController cursor, int positionX, int positionY, Tile tile)
    {
            //Debug.Log(tile.getLocation());

      /*  if (tile.onTopTile != null)
        {
            Debug.Log(tile.onTopTile.getLocation());
            tileMap[string.Format(TileEnum.ID_PATTERN_TILE, tile.onTopTile.x, tile.onTopTile.y)].GetComponent<TileController>().IsAttackArea = true;
        }
        if (tile.onLeftTile != null)
        {
            Debug.Log(tile.onLeftTile.getLocation());
            tileMap[string.Format(TileEnum.ID_PATTERN_TILE, tile.onLeftTile.x, tile.onLeftTile.y)].GetComponent<TileController>().IsAttackArea = true;
        }
        if (tile.onRightTile != null)
        {
            Debug.Log(tile.onRightTile.getLocation());
            tileMap[string.Format(TileEnum.ID_PATTERN_TILE, tile.onRightTile.x, tile.onRightTile.y)].GetComponent<TileController>().IsAttackArea = true;
        }
        if (tile.onDownTile != null)
        {
            Debug.Log(tile.onDownTile.getLocation());
            tileMap[string.Format(TileEnum.ID_PATTERN_TILE, tile.onDownTile.x, tile.onDownTile.y)].GetComponent<TileController>().IsAttackArea = true;
        }*/
            //tileMap[string.Format(TileEnum.ID_PATTERN_TILE, tile.x, tile.y)].GetComponent<TileController>().IsAttackArea = true;

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
    {   //possibleTile.Add(currentTile);

        if (moveCount == 0)
        {
            return;
        }
        if (currentTile == null) return;
        
        else
        {
            moveCount--;
            if (statData.MOVETYPE != StatsInfo.MOVE_TYPE.WALK)
            {
                if (currentTile.y + 1 >= 0 && currentTile.y + 1 < tileDepth)
                    currentTile.onTopTile = new Tile(currentTile.x, currentTile.y + 1);
                if (currentTile.x + 1 >= 0 && currentTile.x + 1 < tileWidht)
                    currentTile.onRightTile = new Tile(currentTile.x + 1, currentTile.y);
                if (currentTile.x - 1 >= 0 && currentTile.x < tileWidht)
                    currentTile.onLeftTile = new Tile(currentTile.x - 1, currentTile.y);
                if (currentTile.y - 1 >= 0 && currentTile.y < tileDepth)
                    currentTile.onDownTile = new Tile(currentTile.x, currentTile.y - 1);

                getPossibleTile(tileWidht, tileDepth, moveCount, currentTile.onTopTile, tileMap);
                getPossibleTile(tileWidht, tileDepth, moveCount, currentTile.onRightTile, tileMap);
                getPossibleTile(tileWidht, tileDepth, moveCount, currentTile.onLeftTile, tileMap);
                getPossibleTile(tileWidht, tileDepth, moveCount, currentTile.onDownTile, tileMap);
            }
            else
            {
                //up   
                if (currentTile.y + 1 >= 0 && currentTile.y + 1 < tileDepth &&
                    tileMap[string.Format(TileEnum.ID_PATTERN_TILE, currentTile.x, currentTile.y)].GetComponent<TileController>().Heightlvl + statData.Jump
                    >= tileMap[string.Format(TileEnum.ID_PATTERN_TILE, currentTile.x, currentTile.y + 1)].GetComponent<TileController>().Heightlvl)
                {
                    currentTile.onTopTile = new Tile(currentTile.x, currentTile.y + 1);
                    // getPossibleTile(tileWidht, tileDepth, moveCount, currentTile.onTopTile, tileMap);
                  //check new condition mark flag tag ignore

                } 
            //right    
                if (currentTile.x + 1 >= 0 && currentTile.x + 1 < tileWidht &&
                     tileMap[string.Format(TileEnum.ID_PATTERN_TILE, currentTile.x, currentTile.y)].GetComponent<TileController>().Heightlvl + statData.Jump
                    >= tileMap[string.Format(TileEnum.ID_PATTERN_TILE, currentTile.x + 1, currentTile.y )].GetComponent<TileController>().Heightlvl
                    )
                {
                    currentTile.onRightTile = new Tile(currentTile.x + 1, currentTile.y);
                    // getPossibleTile(tileWidht, tileDepth, moveCount, currentTile.onRightTile, tileMap);
                    

                }
                    
             //left   
                if (currentTile.x - 1 >= 0 && currentTile.x-1 < tileWidht &&
                     tileMap[string.Format(TileEnum.ID_PATTERN_TILE, currentTile.x, currentTile.y)].GetComponent<TileController>().Heightlvl + statData.Jump
                    >= tileMap[string.Format(TileEnum.ID_PATTERN_TILE, currentTile.x-1, currentTile.y)].GetComponent<TileController>().Heightlvl
                    )
                {
                    currentTile.onLeftTile = new Tile(currentTile.x - 1, currentTile.y);
                    // getPossibleTile(tileWidht, tileDepth, moveCount, currentTile.onLeftTile, tileMap);
                   

                }
                   
             //down   
                if (currentTile.y - 1 >= 0 && currentTile.y-1 < tileDepth &&
                     tileMap[string.Format(TileEnum.ID_PATTERN_TILE, currentTile.x, currentTile.y)].GetComponent<TileController>().Heightlvl + statData.Jump
                    >= tileMap[string.Format(TileEnum.ID_PATTERN_TILE, currentTile.x, currentTile.y - 1)].GetComponent<TileController>().Heightlvl
                    )
                {
                    currentTile.onDownTile = new Tile(currentTile.x, currentTile.y - 1);
                    //getPossibleTile(tileWidht, tileDepth, moveCount, currentTile.onDownTile, tileMap);
                    

                }
                getPossibleTile(tileWidht, tileDepth, moveCount, currentTile.onTopTile, tileMap);
                getPossibleTile(tileWidht, tileDepth, moveCount, currentTile.onRightTile, tileMap);
                getPossibleTile(tileWidht, tileDepth, moveCount, currentTile.onLeftTile, tileMap);
                getPossibleTile(tileWidht, tileDepth, moveCount, currentTile.onDownTile, tileMap);
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
                yield return new WaitForSeconds(0.001f);
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
