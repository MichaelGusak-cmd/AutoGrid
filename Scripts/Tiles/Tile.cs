using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tile : MonoBehaviour {
    public string TileName;
    [SerializeField] protected SpriteRenderer _renderer;
    [SerializeField] private GameObject _highlight;
    [SerializeField] private bool _isWalkable;

    public Vector3 powerVect, hpVect, damageVect;

    public BaseUnit OccupiedUnit;
    public bool Walkable => _isWalkable && OccupiedUnit == null;

    public int xPos;
    public int yPos;


    public virtual void Init(int x, int y, Camera c)
    {
    }
    public abstract void updateText();

    void OnMouseEnter()
    {
        _highlight.SetActive(true);
        MenuManager.Instance.ShowTileInfo(this);
    }

    void OnMouseExit()
    {
        _highlight.SetActive(false);
        MenuManager.Instance.ShowTileInfo(null);
    }

    void OnMouseDown() {
        if(GameManager.Instance.GameState != GameState.RollUnits) return;

        if (OccupiedUnit != null) { //occupied
            if(OccupiedUnit.Faction == Faction.Hero && !OccupiedUnit.moved) //&& SelectionManager.Instance.Contains(OccupiedUnit)) 
                UnitManager.Instance.SetSelectedHero((BaseHero)OccupiedUnit);
            /*else {
                if (UnitManager.Instance.SelectedHero != null) {
                    var enemy = (BaseEnemy)OccupiedUnit;
                    Destroy(enemy.gameObject);
                    UnitManager.Instance.SetSelectedHero(null);
                }
            }*/
        }
        else { //place the unit
            if (UnitManager.Instance.SelectedHero != null) {
                SetUnit(UnitManager.Instance.SelectedHero);
                UnitManager.Instance.SetSelectedHero(null);
                GameManager.Instance.ChangeState(GameState.CombatTurn);
            }
        }

    }

    public void SetUnit(BaseUnit unit) {
        if (unit.OccupiedTile != null) unit.OccupiedTile.OccupiedUnit = null;
        unit.transform.position = transform.position;
        OccupiedUnit = unit;
        OccupiedUnit.moved = !OccupiedUnit.moved;
        unit.OccupiedTile = this;
    }

    public void DestroyUnit()
    {
        if (OccupiedUnit != null)
        {
            var unit = (BaseHero)OccupiedUnit;
            Destroy(unit.gameObject);
            OccupiedUnit = null;
        }
    }
}