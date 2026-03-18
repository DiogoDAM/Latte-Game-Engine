using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Latte;

public sealed class KeyboardManager 
{
	private KeyboardState _prev;
	private KeyboardState _curr;

	public KeyboardManager()
	{
		_prev = Keyboard.GetState();
		_curr = Keyboard.GetState();
	}

	public void Update()
	{
		_prev = _curr;
		_curr = Keyboard.GetState();
	}

	public bool IsKeyDown(Keys key)
	{
		return _curr.IsKeyDown(key);
	}

	public bool IsKeyUp(Keys key)
	{
		return _curr.IsKeyUp(key);
	}

	public bool IsKeyPressed(Keys key)
	{
		return _curr.IsKeyDown(key) && _prev.IsKeyUp(key);
	}

	public bool IsKeyReleased(Keys key)
	{
		return _curr.IsKeyUp(key) && _prev.IsKeyDown(key);
	}
}
