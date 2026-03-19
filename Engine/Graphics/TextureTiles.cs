using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Latte;

public sealed class TextureTiles
{
	public Texture2D Texture { get; private set; }

	public readonly int TileWidth;
	public readonly int TileHeight;
	public readonly int TilesCount;

	public readonly Rectangle[] Tiles;

	public TextureTiles(Texture2D texture, int tileWidth, int tileHeight)
	{
		Texture = texture;

		TileWidth = tileWidth;
		TileHeight = tileHeight;

		int width = Texture.Width / TileWidth;
		int height = Texture.Height / TileHeight;

		TilesCount = width * height;

		Tiles = new Rectangle[TilesCount];

		for(int y=0; y<height; y++)
		{
			for(int x=0; x<width; x++)
			{
				Tiles[x + y * width] = new Rectangle(x * tileWidth, y * tileHeight, tileWidth, tileHeight);
			}
		}
	}

	public TextureTiles(string texturePath, int tileWidth, int tileHeight)
	{
		Texture = Engine.Content.Load<Texture2D>(texturePath);

		TileWidth = tileWidth;
		TileHeight = tileHeight;

		int width = Texture.Width / TileWidth;
		int height = Texture.Height / TileHeight;

		TilesCount = width * height;

		Tiles = new Rectangle[TilesCount];

		for(int y=0; y<height; y++)
		{
			for(int x=0; x<width; x++)
			{
				Tiles[x + y * width] = new Rectangle(x * tileWidth, y * tileHeight, tileWidth, tileHeight);
			}
		}
	}

	public TextureRegion CreateRegion(int index)
	{
		if(index < 0 || index >= TilesCount)
			throw new IndexOutOfRangeException($"Index of Source Rectangle is out of range: {index}");

		return new TextureRegion(Texture, Tiles[index]);

	}
}
