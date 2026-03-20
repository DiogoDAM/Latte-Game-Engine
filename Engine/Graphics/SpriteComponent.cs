using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Latte;

public sealed class SpriteComponent : DrawableComponent
{
	public TextureRegion Region;

	public Vector2 Offset = Vector2.Zero;

	public override int Width { get { return Region.Width; } } 
	public override int Height { get { return Region.Height; } }

	public SpriteComponent(TextureRegion region)
	{
		Region = region;
	}

	public SpriteComponent(Texture2D texture)
	{
		Region = new TextureRegion(texture);
	}

	public SpriteComponent(Texture2D texture, int width, int height)
	{
		Region = new TextureRegion(texture, width, height);
	}

	public SpriteComponent(Texture2D texture, int x, int y, int width, int height)
	{
		Region = new TextureRegion(texture, x, y, width, height);
	}

	public SpriteComponent(Texture2D texture, Rectangle srcRect)
	{
		Region = new TextureRegion(texture, srcRect);
	}

	public SpriteComponent(string texturePath)
	{
		Region = new TextureRegion(texturePath);
	}

	public SpriteComponent(string texturePath, int width, int height)
	{
		Region = new TextureRegion(texturePath, width, height);
	}

	public SpriteComponent(string texturePath, int x, int y, int width, int height)
	{
		Region = new TextureRegion(texturePath, x, y, width, height);
	}

	public SpriteComponent(string texturePath, Rectangle srcRect)
	{
		Region = new TextureRegion(texturePath, srcRect);
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
