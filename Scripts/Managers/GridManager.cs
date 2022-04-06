using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class GridManager : MonoBehaviour {
    public static GridManager Instance;
    [SerializeField] private int _width, _height;

    [SerializeField] private Tile _grassTile, _mountainTile;

    [SerializeField] private Camera _cam;
    [SerializeField] private Vector3 powerVect, hpVect, damageVect;

    private Dictionary<Vector2, Tile> _tiles;

    void Awake() {
        Instance = this;
    }

    public void GenerateGrid()
    {
        _tiles = new Dictionary<Vector2, Tile>();
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++) {
                var randomTile = _grassTile; //Random.Range(0, 6) == 3 ? _mountainTile : _grassTile;
                var spawnedTile = Instantiate(randomTile, new Vector3(x, y), Quaternion.identity);
                spawnedTile.name = $"Tile {x} {y}";


                spawnedTile.Init(x, y, _cam);
                spawnedTile.powerVect = powerVect;
                spawnedTile.hpVect = hpVect;
                spawnedTile.damageVect = damageVect;

                _tiles[new Vector2(x, y)] = spawnedTile;
            }
        }

        _cam.transform.position = new Vector3((float)_width / 2 - 0.5f, (float)_height / 2 - 0.5f, -10);

        GameManager.Instance.ChangeState(GameState.RollUnits);
    }

    public void Action()
    {
        //loop through each unit, have them do their action
        //create healers and damage dealers, only do utility if you have time, goodluck
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                var tile = _tiles[new Vector2(x, y)];
                if (tile.OccupiedUnit != null)
                {
                    tile.OccupiedUnit.Action();
                }
            }
        }

        //loop through each unit again, check if hp <= 0, destroy if yes
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                var tile = _tiles[new Vector2(x, y)];
                if (tile.OccupiedUnit != null)
                {
                    if (tile.OccupiedUnit.HP <= 0) 
                        tile.DestroyUnit();
                }
                tile.updateText();
            }
        }
        GameManager.Instance.ChangeState(GameState.RollUnits);
    }

    public bool CheckGameEnd()
    {
        int counter = 0;
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                if (_tiles[new Vector2(x, y)].OccupiedUnit != null)
                    counter++;
            }
        }
        return counter == _width * _height;
    }

    public Tile GetHeroSpawnTile() {
        return _tiles.Where(t => t.Key.x < _width / 2 && t.Value.Walkable).OrderBy(t => Random.value).First().Value;
    }

    public Tile GetEnemySpawnTile()
    {
        return _tiles.Where(t => t.Key.x > _width / 2 && t.Value.Walkable).OrderBy(t => Random.value).First().Value;
    }

    public Tile GetTileAtPosition(Vector2 pos)
    {
        if (_tiles.TryGetValue(pos, out var tile)) return tile;
        return null;
    }
}