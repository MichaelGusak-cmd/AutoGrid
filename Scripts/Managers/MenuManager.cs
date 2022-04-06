using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {
    public static MenuManager Instance;

    [SerializeField] private GameObject _selectedHeroObject,_tileObject,_tileUnitObject, _tileUnitDesc, _goal;
    
    public int goal;
    public int rounds;

    void Awake() {
        rounds = 0;
        Instance = this;
        _goal.SetActive(true);
        _goal.GetComponentInChildren<Text>().text = rounds+"/"+goal;
    }

    public void printGoal()
    {
        if (rounds < goal)
            _goal.GetComponentInChildren<Text>().text = rounds + "/" + goal;
        else
            _goal.GetComponentInChildren<Text>().text = "Survived! Next level upon board fill ("+rounds+"/"+goal+")";
    }

    public void ShowTileInfo(Tile tile) {

        if (tile == null)
        {
            _tileObject.SetActive(false); 
            _tileUnitObject.SetActive(false);
            _tileUnitDesc.SetActive(false);
            return;
        }

        _tileObject.GetComponentInChildren<Text>().text = tile.TileName;
        _tileObject.SetActive(true);

        if (tile.OccupiedUnit) {
            _tileUnitObject.GetComponentInChildren<Text>().text = tile.OccupiedUnit.UnitName;
            _tileUnitDesc.GetComponentInChildren<Text>().text = tile.OccupiedUnit.Description;
            _tileUnitObject.SetActive(true);
            _tileUnitDesc.SetActive(true);
        }
    }

    public void ShowSelectedHero(BaseHero hero) {
        if (hero == null) {
            _selectedHeroObject.SetActive(false);
            return;
        }

        _selectedHeroObject.GetComponentInChildren<Text>().text = hero.UnitName;
        _selectedHeroObject.SetActive(true);
    }
}
