using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Latte;

public sealed class SpriteComponent : DrawableComponent
{
	public TextureRegion Region;

	public Vector2 Offset;

	public SpriteComponent(TextureRegion region)
	{
		Region = region;
	}

	public SpriteComponent(Texture2D texture)
	{
		Region = new(texture);
	}

	public SpriteComponent(Texture2D texture, int width, int height)
	{
		Region = new(texture, width, height);
	}

	public SpriteComponent(Texture2D texture, int x, int y, int width, int height)
	{
		Region = new(texture, x, y, width, height);
	}

	public SpriteComponent(Texture2D texture, Rectangle srcRectangle)
	{
		Region = new(texture, srcRectangle);
	}

	public SpriteComponent(string texturePath)
	{
		Region = new(texturePath);
	}

	public SpriteComponent(string texturePath, int width, int height)
	{
		Region = new(texturePath, width, height);
	}

	public SpriteComponent(string texturePath, int x, int y, int width, int height)
	{
		Region = new(texturePath, x, y, width, height);
	}

	public SpriteComponent(string texturePath, Rectangle srcRectangle)
	{
		Region = new(texturePath, srcRectangle);
	}

    public override void Draw()
    {
		Engine.SpriteBatch.Draw(Region.Texture,
				Entity.Transform.GlobalPosition + Offset,
				Region.SourceRectangle,
				Color,
				Entity.Transform.GlobalRotation,
				Origin,
				Entity.Transform.GlobalScale,
				Flip,
				Depth);
    }
}
