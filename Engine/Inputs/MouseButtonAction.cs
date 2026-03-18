using System;
using Microsoft.Xna.Framework.Input;

namespace Latte;

public sealed class MouseButtonAction : IInputAction
{
	public byte Button { get; set; }

	public Predicate<byte> Predicate { get; private set; }

	public MouseButtonAction(byte button, Predicate<byte> predicate)
	{
		Button = button;
		Predicate = predicate;
	}

	public bool IsInAction()
	{
		return Predicate(Button);
	}
}
