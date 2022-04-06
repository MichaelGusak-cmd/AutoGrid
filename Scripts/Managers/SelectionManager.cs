using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

public class SelectionManager : MonoBehaviour {
    public static SelectionManager Instance;
    [SerializeField] private int _width;
    [SerializeField] private Tile _unitTile;
    //[SerializeField] private 
    private List<ScriptableUnit> _units;

    private Dictionary<int, Tile> _tiles;
    [SerializeField] private Camera _cam;

    void Awake()
    {
        Instance = this;
        _units = Resources.LoadAll<ScriptableUnit>("Units").ToList();
    }

    public void RollUnits()
    {
        for (int i = 0; i < _tiles.Count; i++)
        {
            _tiles[i].DestroyUnit();
            var randomPrefab = GetRandomUnit<BaseHero>(Faction.Hero);
            var spawnedUnit = Instantiate(randomPrefab);
            spawnedUnit.moved = true;

            _tiles[i].SetUnit(spawnedUnit);
        }
        //GameManager.Instance.ChangeState(GameState.CombatTurn);
    }


    public void GenerateGrid()
    {
        _tiles = new Dictionary<int, Tile>();
        for (int x = 0; x < _width; x++)
        {
            var spawnedTile = Instantiate(_unitTile, new Vector3(x, -1), Quaternion.identity);
            spawnedTile.name = $"UnitTile {x}";

            spawnedTile.Init(x, -1, _cam);
            _tiles[x] = spawnedTile;
        }
    }

    private T GetRandomUnit<T>(Faction faction) where T : BaseUnit
    {
        return (T)_units.Where(u => u.Faction == faction).OrderBy(o => Random.value).First().UnitPrefab;
    }
    /*
    public bool Contains(BaseUnit unit)
    {
        bool output = false;
        for (int i = 0; i < _units.Count; i++)
        {
            if (_units[i].UnitPrefab.Equals(unit))
                output = true;
        }
        return output;
    }*/
}
