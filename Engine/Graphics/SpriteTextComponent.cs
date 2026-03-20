using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;

namespace Latte;

public sealed class SpriteTextComponent : DrawableComponent
{
	public SpriteFont Font;
	public string Text;

	public TransformComponent Transform;

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

	public Vector2 TextSize() => Font.MeasureString(Text);
	public float TextWidth() => Font.MeasureString(Text).X;
	public float TextHeight() => Font.MeasureString(Text).Y;

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
