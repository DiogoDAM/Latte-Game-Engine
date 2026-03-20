using System;
using Microsoft.Xna.Framework;

namespace Latte;

public abstract class Shape
{
	public int X;
	public int Y;

	public virtual Rectangle AABB { get; }

	public bool Intersects(Shape other)
	{
		switch(other)
		{
			case BoxShape box: return Intersects(box);
			default: throw new NotImplementedException("Shape type not implemented");
		}
	}

	public abstract bool Intersects(BoxShape other);

	public abstract bool Contains(Vector2 vec);
}
