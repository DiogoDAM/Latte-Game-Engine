using System;

namespace Latte;

public abstract class Component : Behaviour
{
	public Component() : base()
	{
	}

	public virtual void Added()
	{
	}

	public virtual void Removed()
	{
	}
}
