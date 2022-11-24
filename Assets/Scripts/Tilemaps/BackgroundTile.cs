using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "New Background Tile", menuName = "Tiles/Background Tile")]
public class BackgroundTile : Tile
{
	public List<Sprite> oceanSprites;
	public List<Sprite> desertSprites;
	private Sprite desertSprite;
	private RuleTile oceanSprite;

	public override bool StartUp(Vector3Int position, ITilemap tilemap, GameObject go)
	{
		if(desertSprites.Count == 1) desertSprite = desertSprites[0];
        else desertSprite = desertSprites[Random.Range(0, desertSprites.Count)];

		this.colliderType = ColliderType.None;

		return base.StartUp(position, tilemap, go);
	}

	public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
	{
		base.GetTileData(position, tilemap, ref tileData);

		if(ThemeSwitcher.isDesert) {
			if(desertSprite != null) {
				tileData.sprite = desertSprite;
			}
		}
	}
}
