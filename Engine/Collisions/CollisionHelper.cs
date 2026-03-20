using System;

using Microsoft.Xna.Framework;

namespace Latte;

public static class CollisionHelper
{
	public static Vector2 GetIntesectionAABB(ColliderComponent collider1, ColliderComponent collider2)
	{
		Vector2 pos = Vector2.Zero;
		Rectangle intersection = Rectangle.Intersect(collider1.Shape.AABB, collider2.Shape.AABB);

		if(intersection.Width < intersection.Height)
		{
			if(collider1.Shape.X > collider2.Shape.X)
				pos.X = intersection.Width;
			else 
				pos.X = -intersection.Width;
		}
		else
		{
		    if(collider1.Shape.Y > collider2.Shape.Y)
				pos.Y = intersection.Height;
			else
				pos.Y = -intersection.Height;
		}

		return pos;
	}

	public static Vector2 GetIntesectionAABB(Shape shape1, Shape shape2)
	{
		Vector2 pos = Vector2.Zero;
		Rectangle intersection = Rectangle.Intersect(shape1.AABB, shape2.AABB);

		if(intersection.Width < intersection.Height)
		{
			if(shape1.X > shape2.X)
				pos.X = intersection.Width;
			else 
				pos.X = -intersection.Width;
		}
		else
		{
		    if(shape1.Y > shape2.Y)
				pos.Y = intersection.Height;
			else
				pos.Y = -intersection.Height;
		}

		return pos;
	}
}
