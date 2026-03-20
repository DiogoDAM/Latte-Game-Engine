using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Latte;

public sealed class ColliderComponent : Component
{
	public Shape Shape { get; private set; }

	public Vector2 Offset;

	public bool CanCollide = true;

	public delegate void CollidedEventHandler(ColliderComponent other);

	public event CollidedEventHandler Collided;

	public Color DebugColor = Color.Red;

	public ColliderComponent(Shape shape)
	{
		Shape = shape;
	}

	public void SetShape(Shape shape) => Shape = shape;

    public override void Update(float dt)
    {
		Shape.X = (int)(Entity.Transform.Position.X + Offset.X);
		Shape.Y = (int)(Entity.Transform.Position.Y + Offset.Y);
    }

	public bool Collides(ColliderComponent col)
	{
		if(CanCollide && col.CanCollide)
			return Shape.Intersects(col.Shape);

		return false;
	}

	public bool Collides(Entity e)
	{
		if(e.TryGetComponent<ColliderComponent>(out ColliderComponent col))
		{
			if(CanCollide && col.CanCollide)
			{
				return Shape.Intersects(col.Shape);
			}
		}
		else
		{
		    throw new ComponentNotFoundException("The Entity does not have ColliderComponent");
		}

		return false;
	}

	public void Collide(ColliderComponent other)
	{
		Collided?.Invoke(other);
	}

	public bool Contains(Vector2 point)
	{
		if(CanCollide)
			return Shape.Contains(point);

		return false;
	}

    public override void DebugDraw()
    {
		Texture2D texture;
		if(Shape is BoxShape box)
		{
			texture = ShapeDrawer.LineRectangle(box.Width, box.Height, Color.White);
			Engine.SpriteBatch.Draw(texture, new Vector2(Shape.X, Shape.Y), null, DebugColor);
		}
    }
}
