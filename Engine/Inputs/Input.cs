using System.Collections.Generic;
using System;

using Microsoft.Xna.Framework.Input;
using System.IO;
using System.Xml;
using System.Xml.Linq;

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

	public void AddMouseButtonInputAction(string name, Predicate<byte> predicate, byte button)
	{
		if(!_singleActions.ContainsKey(name))
			throw new KeyNotFoundException($"The action name: {name} not found");

		_singleActions[name].Add(new MouseButtonAction(button, predicate));
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

		if(_singleActions[name].Count == 1)
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

	public void LoadFromXmlFile(string filePath)
	{
		string fullPath = Path.Combine(Engine.Content.RootDirectory, filePath);

		using(XmlReader reader = XmlReader.Create(fullPath))
		{
			XDocument doc = XDocument.Load(reader);
			XElement root = doc.Root;

			var inputActions = root?.Elements("InputAction");

			if(inputActions == null)
				return;

			foreach(var inputAction in inputActions)
			{
				string name = inputAction.Attribute("name")?.Value;
				CreateInputAction(name);

				var keysActions = inputAction?.Elements("Key");

				if(keysActions != null)
				{
					foreach(var keyAction in keysActions)
					{
						Predicate<Keys> keyPredicate;
						string type = keyAction?.Attribute("type")?.Value;

						switch(type)
						{
							case "down": keyPredicate = Keyboard.IsKeyDown; break;
							case "up": keyPredicate = Keyboard.IsKeyUp; break;
							case "pressed": keyPredicate = Keyboard.IsKeyPressed; break;
							case "released": keyPredicate = Keyboard.IsKeyReleased; break;
							default: throw new NotImplementedException("KeyboardInputAction type not implemented: " + type);
						}

						if(Enum.TryParse(keyAction?.Value, true, out Keys key))
							AddKeyboardInputAction(name, keyPredicate, key);
						else
							throw new Exception($"KeyboardInputAction of the name: {name} error to parse key value");
					}
				}

				var mouseButtonActions = inputAction?.Elements("MouseButton");

				if(mouseButtonActions != null)
				{
					foreach(var mouseButtonAction in mouseButtonActions)
					{
						Predicate<byte> mouseButtonPredicate;
						string type = mouseButtonAction?.Attribute("type")?.Value;

						switch(type)
						{
							case "down": mouseButtonPredicate = Mouse.IsButtonDown; break;
							case "up": mouseButtonPredicate = Mouse.IsButtonUp; break;
							case "pressed": mouseButtonPredicate = Mouse.IsButtonPressed; break;
							case "released": mouseButtonPredicate = Mouse.IsButtonReleased; break;
							default: throw new NotImplementedException("MouseButtonInputAction type not implemented: " + type);
						}

						byte button;

						string buttonType = mouseButtonAction?.Value;

						switch(buttonType)
						{
							case "LMB": button = 0; break;
							case "RMB": button = 1; break;
							case "MMB": button = 2; break;
							default: throw new NotImplementedException("MouseButtonInputAction button type not implemented: " + buttonType);
						}

						AddMouseButtonInputAction(name, mouseButtonPredicate, button);
					}
				}
			}
		}
	}
}
