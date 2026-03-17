namespace Latte;

public sealed class Input
{
	public KeyboardManager Keyboard { get; private set; }
	public MouseManager Mouse { get; private set; }

	public Input()
	{
		Keyboard = new();
		Mouse = new();
	}

	public void Update(float dt)
	{
		Keyboard.Update();
		Mouse.Update();
	}
}
