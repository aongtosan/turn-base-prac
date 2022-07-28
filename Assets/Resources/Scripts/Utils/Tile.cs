using UnityEngine;

public class Tile{
   public int x;
   public int y;
   public Tile (int x,int y){
    this.x = x;
    this.y = y;
   }
   public string getLocation(){
        return (string.Format("tile location x:{0},y{1}",x,y));
   }
}