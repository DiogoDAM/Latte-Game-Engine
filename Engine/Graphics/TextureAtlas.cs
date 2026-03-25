using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System.Collections.Generic;

namespace Latte;

public sealed class TextureAtlas
{
	public Texture2D Texture { get; private set; }

	private Dictionary<string, TextureRegion> _regions;

	public TextureRegion this[string regionName]
	{
		get
		{
			if(!_regions.ContainsKey(regionName))
				throw new KeyNotFoundException("regionName key not found: " + regionName);

			return _regions[regionName];
		}
	}

	public TextureAtlas(Texture2D texture)
	{
		Texture = texture;
		_regions = new();
	}

	public TextureAtlas(string texturePath)
	{
		Texture = Engine.Content.Load<Texture2D>(texturePath);
		_regions = new();
	}

	public void CreateRegion(string name, int x, int y, int width, int height)
	{
		_regions.Add(name, new TextureRegion(Texture, x, y, width, height));
	}

	public void CreateRegion(string name, Rectangle srcRect)
	{
		_regions.Add(name, new TextureRegion(Texture, srcRect));
	}
}
