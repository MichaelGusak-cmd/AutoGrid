using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crab : BaseHero
{
    public override void Init() 
    {
        Description = "Heals itself for (Power) HP each round (Max: 20)";
    }
    public override void Action()
    {
        HP += Power;
        if (HP > 20) HP = 20;
    }
}
