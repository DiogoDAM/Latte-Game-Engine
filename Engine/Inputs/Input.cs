using System.Collections.Generic;
using System;

using Microsoft.Xna.Framework.Input;

namespace Latte;

public sealed class Input
{
	public KeyboardManager Keyboard { get; private set; }
	public MouseManager Mouse { get; private set; }

	private Dictionary<string, List<IInputAction>> _singleActions;

	public Input()
	{
		Keyboard = new();
		Mouse = new();

		_singleActions = new();
	}

	public void Update(float dt)
	{
		Keyboard.Update();
		Mouse.Update();
	}

	public void CreateInputAction(string name)
	{
		if(_singleActions.ContainsKey(name))
			throw new Exception($"The action name: {name} already exist");

		_singleActions.Add(name, new List<IInputAction>());
	}

	public void AddKeyboardInputAction(string name, Predicate<Keys> predicate, Keys key)
	{
		if(!_singleActions.ContainsKey(name))
			throw new KeyNotFoundException($"The action name: {name} not found");

		_singleActions[name].Add(new KeyboardAction(key, predicate));
	}

	public void ClearInputAction(string name)
	{
		if(!_singleActions.ContainsKey(name))
			throw new KeyNotFoundException($"The action name: {name} not found");

		_singleActions[name].Clear();
	}

	public List<IInputAction> GetInputActionList(string name)
	{
		if(!_singleActions.ContainsKey(name))
			throw new KeyNotFoundException($"The action name: {name} not found");

		return _singleActions[name];
	}

	public bool CheckInputAction(string name)
	{
		if(!_singleActions.ContainsKey(name))
			throw new KeyNotFoundException($"The action name: {name} not found");

		if(_singleActions[name].Count == 0)
			return _singleActions[name][0].IsInAction();
		else
		{
		    foreach(var action in _singleActions[name])
			{
				if(action.IsInAction())
					return true;
			}
		}

		return false;
	}
}
