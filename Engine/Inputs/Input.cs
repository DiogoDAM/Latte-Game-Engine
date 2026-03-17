namespace Latte;

public sealed class Input
{
	public KeyboardManager Keyboard { get; private set; }

	public Input()
	{
		Keyboard = new();
	}

	public void Update(float dt)
	{
		Keyboard.Update();
	}
}
