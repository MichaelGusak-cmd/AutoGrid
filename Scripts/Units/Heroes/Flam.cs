using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flam : BaseHero
{
    public override void Init() 
    {
        Description = "Increase all Surrounding Units' Power by 1 per Round (Max 4)";
    }
    public override void Action()
    {
        var tiles = GetSurroundingTiles(8);
        for (int i = 0; i < tiles.Count; i++)
        {
            tiles[i].OccupiedUnit.Power += 1;
            if (tiles[i].OccupiedUnit.Power > 4) tiles[i].OccupiedUnit.Power = 4;
        }
    }
}
