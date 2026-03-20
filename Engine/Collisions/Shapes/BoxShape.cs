using System;
using Microsoft.Xna.Framework;

namespace Latte;

public sealed class BoxShape : Shape
{
	public int Width;
	public int Height;

	public int Left => X;
	public int Right => X + Width;
	public int Top => Y;
	public int Bottom => Y + Height;

	public int HalfWidth => (int)(Width*0.5f);
	public int HalfHeight => (int)(Height*0.5f);

	public override Rectangle AABB { get { return new Rectangle(X, Y, Width, Height); } }

	public BoxShape(int width, int height)
	{
		Width = width;
		Height = height;
	}

	public override bool Intersects(BoxShape other)
	{
		return Left <= other.Right &&
			Right >= other.Left &&
			Top <= other.Bottom &&
			Bottom >= other.Top;
	}

	public override bool Contains(Vector2 vec)
	{
		return vec.X < Right &&
			vec.X > Left &&
			vec.Y < Bottom &&
			vec.Y > Top;
	}
}
