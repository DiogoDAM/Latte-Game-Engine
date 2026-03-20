using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;

namespace Latte;

public sealed class SpriteTextComponent : DrawableComponent
{
	public SpriteFont Font;
	public string Text;

	public TransformComponent Transform;

	public override int Width { get { return (int)Font.MeasureString(Text).X; } } 
	public override int Height { get { return (int)Font.MeasureString(Text).Y; } }

	public Vector2 TextSize() => Font.MeasureString(Text);

	public SpriteTextComponent(SpriteFont font, string text)
	{
		Transform = new();
		Font = font;
		Text = text;
	}

	public SpriteTextComponent(string fontPath, string text)
	{
		Transform = new();
		Font = Engine.Content.Load<SpriteFont>(fontPath);
		Text = text;
	}

    public override void Attach(Entity e)
    {
        base.Attach(e);

		Transform.Parent = Entity.Transform;
    }

    public override void Distach()
    {
        base.Distach();

		Transform.Parent = null;
    }

    public override void Draw()
    {
		Engine.SpriteBatch.DrawString(Font,
				Text,
				Transform.GlobalPosition,
				Color,
				Transform.GlobalRotation,
				Origin,
				Transform.GlobalScale,
				Flip,
				Depth);
    } 
}
