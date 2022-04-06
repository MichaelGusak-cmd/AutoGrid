using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomp : BaseHero
{
    public override void Init() 
    {
        Description = "Attacks (Power*2) Surrounding Units for (Power)*(Damage) Damage.";
    }
    public override void Action()
    {
        var tiles = GetSurroundingTiles(2*Power);
        for (int i = 0; i < tiles.Count; i++)
        {
            //Debug.Log("Affecting: " + tiles[i].xPos + ", " + tiles[i].yPos);
            tiles[i].OccupiedUnit.HP -= Damage * Power;
        }
    }
}
