using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;

public class TilemapHandler : MonoBehaviour, ThemeHandler
{
	public Tilemap waterMap;
	private Tilemap tm;

	void Start()
	{
		tm = GetComponent<Tilemap>();
		var splicedTiles = spliceTiles();

		foreach(KeyValuePair<Vector2, ThemedTile> entry in splicedTiles) {
			waterMap.SetTile(new Vector3Int((int)entry.Key.x, (int)entry.Key.y, 0), entry.Value);
		}

		waterMap.gameObject.SetActive(true);
	}

    public void onDesert()
    {
		waterMap.RefreshAllTiles();
		tm.RefreshAllTiles();
    }

    public void onOcean()
    {
		waterMap.RefreshAllTiles();
		tm.RefreshAllTiles();
    }

	public Dictionary<Vector2, ThemedTile> spliceTiles() {
		var tiles = new Dictionary<Vector2, ThemedTile>();

		var bounds = tm.cellBounds;
		TileBase[] allTiles = tm.GetTilesBlock(bounds);

		for (int x = bounds.xMin; x < bounds.xMax; x++) {
			for (int y = bounds.yMin; y < bounds.yMax; y++) {

				var tile = allTiles[x - bounds.xMin + (y - bounds.yMin) * bounds.size.x] as ThemedTile;

				if(tile != null) {
					if(tile.tileType == ThemedTile.TileType.Water) {
						tiles.Add(new Vector2(x, y), tile);
						tm.SetTile(new Vector3Int(x, y, 0), null);
					}
				}
			}
		}
		
		return tiles;
	}
}
