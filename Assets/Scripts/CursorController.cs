using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    // Start is called before the first frame update
    int positionX;
    int positionY;

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
}
