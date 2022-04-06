using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseHero : BaseUnit
{

    protected List<Tile> GetAdjacentTiles(int numTiles)
    {
        List<Tile> tiles = new List<Tile>();

        int x = OccupiedTile.xPos;
        int y = OccupiedTile.yPos;
        var addTile = GridManager.Instance.GetTileAtPosition(new Vector2(x - 1, y));
            if (addTile != null) 
                if (addTile.OccupiedUnit != null) tiles.Add(addTile);
        addTile = GridManager.Instance.GetTileAtPosition(new Vector2(x + 1, y));
            if (addTile != null)
                if (addTile.OccupiedUnit != null) tiles.Add(addTile);
        addTile = GridManager.Instance.GetTileAtPosition(new Vector2(x, y - 1));
            if (addTile != null)
                if (addTile.OccupiedUnit != null) tiles.Add(addTile);
        addTile = GridManager.Instance.GetTileAtPosition(new Vector2(x, y + 1));
            if (addTile != null)
                if (addTile.OccupiedUnit != null) tiles.Add(addTile);

        while (tiles.Count > numTiles) {
            tiles.RemoveAt((int)Random.Range(0, tiles.Count - 1));
        }
        return tiles;
    }

    protected List<Tile> GetSurroundingTiles(int numTiles)
    {
        List<Tile> tiles = new List<Tile>();
        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                if (i != 0 || j != 0)
                {
                    var addTile = GridManager.Instance.GetTileAtPosition(new Vector2(OccupiedTile.xPos+i, OccupiedTile.yPos+j));
                    if (addTile != null) 
                        if (addTile.OccupiedUnit != null)
                                tiles.Add(addTile);
                }
            }
        }

        while (tiles.Count > numTiles) {
            tiles.RemoveAt(Random.Range(0, tiles.Count - 1));
        }
        return tiles;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
