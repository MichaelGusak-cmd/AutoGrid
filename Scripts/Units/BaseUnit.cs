using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseUnit : MonoBehaviour {
    public string UnitName;
    
    public string Description;
    public int HP;
    public int Damage;
    public int Power;

    public Tile OccupiedTile;
    public Faction Faction;
    public bool moved;

    void Awake()
    {
        Description = "Default desc set in BaseUnit.cs, void Awake()";
        HP = 5; //default hp
        Damage = 1; //default damage
        Power = 1;
        Init();
    }

    public abstract void Action();
    public abstract void Init();
}
