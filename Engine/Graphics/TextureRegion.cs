using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Latte;

public sealed class TextureRegion
{
	public Texture2D Texture { get; private set; }

	public Rectangle SourceRectangle { get; set; }

	public int X => SourceRectangle.X;
	public int Y => SourceRectangle.Y;
	public int Width => SourceRectangle.Width;
	public int Height => SourceRectangle.Height;

	public TextureRegion(Texture2D texture)
	{
		Texture = texture;
		SourceRectangle = new Rectangle(0, 0, texture.Width, texture.Height);
	}

	public TextureRegion(Texture2D texture, int width, int height)
	{
		Texture = texture;
		SourceRectangle = new Rectangle(0, 0, width, height);
	}

	public TextureRegion(Texture2D texture, int x, int y, int width, int height)
	{
		Texture = texture;
		SourceRectangle = new Rectangle(x, y, width, height);
	}

	public TextureRegion(Texture2D texture, Rectangle srcRectangle)
	{
		Texture = texture;
		SourceRectangle = srcRectangle;
	}

	public TextureRegion(string texturePath)
	{
		Texture = Engine.Content.Load<Texture2D>(texturePath);
		SourceRectangle = new Rectangle(0, 0, Texture.Width, Texture.Height);
	}

	public TextureRegion(string texturePath, int width, int height)
	{
		Texture = Engine.Content.Load<Texture2D>(texturePath);
		SourceRectangle = new Rectangle(0, 0, width, height);
	}

	public TextureRegion(string texturePath, int x, int y, int width, int height)
	{
		Texture = Engine.Content.Load<Texture2D>(texturePath);
		SourceRectangle = new Rectangle(x, y, width, height);
	}

	public TextureRegion(string texturePath, Rectangle srcRectangle)
	{
		Texture = Engine.Content.Load<Texture2D>(texturePath);
		SourceRectangle = srcRectangle;
	}
}
