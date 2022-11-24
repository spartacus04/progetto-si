using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "New Themed Tile", menuName = "Tiles/Themed Tile")]
public class ThemedTile : Tile
{
	public List<Sprite> oceanSprites;
	public List<Sprite> desertSprites;
	private Sprite desertSprite;
	private Sprite oceanSprite;

	public override bool StartUp(Vector3Int position, ITilemap tilemap, GameObject go)
	{
		if(oceanSprites.Count == 1) oceanSprite = oceanSprites[0];
		else oceanSprite = oceanSprites[Random.Range(0, desertSprites.Count)];

		if(desertSprites.Count == 1) desertSprite = desertSprites[0];
        else desertSprite = desertSprites[Random.Range(0, oceanSprites.Count)];

		return base.StartUp(position, tilemap, go);
	}

	public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
	{
		base.GetTileData(position, tilemap, ref tileData);

		if(ThemeSwitcher.isDesert) {
			if(desertSprite != null) {
				tileData.sprite = desertSprite;
			}
		} else {
			if(oceanSprite != null) {
				tileData.sprite = oceanSprite;
			}
		}
	}
}
