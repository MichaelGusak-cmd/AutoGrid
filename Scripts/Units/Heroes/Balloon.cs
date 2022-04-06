using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : BaseHero
{
    public override void Init() 
    {
        Description = "Attacks (Power) Adjacent Units for (Power)*(Damage) Damage.";
    }
    public override void Action()
    {
        var tiles = GetAdjacentTiles(Power);
        for (int i = 0; i < tiles.Count; i++)
        {
            Debug.Log("Affecting: " + tiles[i].xPos + ", " + tiles[i].yPos);
            tiles[i].OccupiedUnit.HP -= Damage * Power;
        }
    }
}
