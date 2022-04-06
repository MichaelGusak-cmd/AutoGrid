using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skoo : BaseHero
{
    public override void Init() 
    {
        Description = "Heal (Power) Random Adjacent Units for (Power) HP (Max 20)";
    }
    public override void Action()
    {
        var tiles = GetAdjacentTiles(Power);
        for (int i = 0; i < tiles.Count; i++)
        {
            //Debug.Log("Affecting: " + tiles[i].xPos + ", " + tiles[i].yPos);
            tiles[i].OccupiedUnit.HP += Power;
            if (tiles[i].OccupiedUnit.HP > 20) tiles[i].OccupiedUnit.HP = 20;
        }
    }
}
