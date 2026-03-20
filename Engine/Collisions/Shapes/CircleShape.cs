using System;

using Microsoft.Xna.Framework;

namespace Latte;

public sealed class CircleShape : Shape
{
	public int Radius;

	public int Diameter => Radius * 2;

	public Vector2 Center => new Vector2(X + Radius, Y + Radius);

	public override Rectangle AABB { get { return new Rectangle(X, Y, Diameter, Diameter); } }

	public CircleShape(int radius)
	{
		Radius = radius;
	}

    public override bool Intersects(BoxShape other)
    {
		float closestX = Math.Clamp(Center.X, other.Left, other.Right);
		float closestY = Math.Clamp(Center.Y, other.Top, other.Bottom);

		float dx = Center.X - closestX;
		float dy = Center.Y - closestY;

		return (dx * dx + dy * dy) <= Radius * Radius;
    }

    public override bool Intersects(CircleShape other)
    {
		float dx = Center.X - other.Center.X;
		float dy = Center.Y - other.Center.Y;

		return (dx * dx + dy * dy) <= Radius * Radius;
    }

    public override bool Contains(Vector2 vec)
    {
		float dx = Center.X - vec.X;
		float dy = Center.Y - vec.Y;

		return (dx * dx + dy * dy) <= Radius * Radius;
    }
}
