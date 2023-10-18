using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile 
{
    public int x;
    public int y;

    public Tile(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public override string ToString() => x + "," + y;
}
