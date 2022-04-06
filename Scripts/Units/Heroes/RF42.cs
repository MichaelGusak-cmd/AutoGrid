using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RF42 : BaseHero
{
    public override void Init() 
    {
        Description = "Reduces Power of all Surrounding Units by 1 per Round (Min 1)";
    }
    public override void Action()
    {
        var tiles = GetSurroundingTiles(8);
        for (int i = 0; i < tiles.Count; i++)
        {
            //Debug.Log("Affecting: " + tiles[i].xPos + ", " + tiles[i].yPos);
            tiles[i].OccupiedUnit.Power -= 1;
            if (tiles[i].OccupiedUnit.Power < 1) tiles[i].OccupiedUnit.Power = 1;
        }
    }
}
