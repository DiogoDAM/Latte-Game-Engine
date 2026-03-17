using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System.Collections.Generic;

namespace Latte;

public sealed class TextureAtlas
{
	public Texture2D Texture { get; private set; }

	private Dictionary<string, Rectangle> _regions;

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

	public void AddRegion(string name, int x, int y, int width, int height)
	{
		_regions.Add(name, new Rectangle(x, y, width, height));
	}

	public void AddRegion(string name, Rectangle srcRect)
	{
		_regions.Add(name, srcRect);
	}

	public TextureRegion CreateRegion(string name)
	{
		if(!_regions.ContainsKey(name))
			throw new KeyNotFoundException($"The region with the key name: {name} not found");

		return new TextureRegion(Texture, _regions[name]);
	}

	public SpriteComponent CreateSprite(string name)
	{
		if(!_regions.ContainsKey(name))
			throw new KeyNotFoundException($"The region with the key name: {name} not found");

		return new SpriteComponent(Texture, _regions[name]);
	}
}
