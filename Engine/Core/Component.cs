using System;

namespace Latte;

public abstract class Component : Behaviour
{
	public Entity Entity { get; private set; }

	public Component()
	{
	}

	public virtual void Attach(Entity e)
	{
		Entity = e;
	}

	public virtual void Distach()
	{
		Entity = null;
	}
}
