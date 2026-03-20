using System;

namespace Latte;

public sealed class ComponentNotFoundException : Exception
{
	public ComponentNotFoundException(string message) : base(message)
	{
	}
}
