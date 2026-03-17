using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Latte;

public abstract class DrawableComponent : Component
{
	public Color Color = Color.White;
	public Vector2 Origin;
	public SpriteEffects Flip;
	public float Depth;
}
