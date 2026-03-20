using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;

namespace Latte;

public class Entity : Behaviour
{
	private Dictionary<Type, Component> _components;
	private Dictionary<Type, DrawableComponent> _drawables;

	public TransformComponent Transform;

	public Entity()
	{
		_components = new();
		_drawables = new();

		Transform = AddComponent<TransformComponent>(new ());
	}

	public Entity(float x, float y)
	{
		_components = new();
		_drawables = new();

		Transform = AddComponent<TransformComponent>(new (new Vector2(x, y)));
	}

	//Methods for handling components
	
	public T AddComponent<T>(T c) where T : Component
	{
		Type type = typeof(T);
		if(!_components.ContainsKey(type))
		{
			_components.Add(type, c);

			if(c is DrawableComponent dc)
			{
				_drawables.Add(type, dc);
			}

			c.Attach(this);
			c.Awake();
			c.Start();

			return c;
		}

		return null;
	}

	public bool RemoveComponent<T>() where T : Component
	{
		Type type = typeof(T);
		if(_components.ContainsKey(type))
		{
			T c = (T)_components[type];

			_components.Remove(type);

			if(type.IsInstanceOfType(typeof(DrawableComponent)))
			{
				_drawables.Remove(type);
			}

			c.Distach();
			if(c.ToDispose)
				c.Dispose();
		}

		return false;
	}

	public bool ContainsComponent<T>() where T : Component
	{
		return _components.ContainsKey(typeof(T));
	}

	public T GetComponent<T>() where T : Component
	{
		return (T)_components[typeof(T)];
	}

	public bool TryGetComponent<T>(out T outC) where T : Component
	{
		outC = (T)_components[typeof(T)];

		return (outC == null) ? false : true;
	}


	// Logic Methods
	public virtual void Added()
	{
	}

	public virtual void Removed()
	{
	}

    public override void Update(float dt)
    {
		foreach(var c in _components.Values)
		{
			if(c.CanUpdate)
				c.Update(dt);
		}
    }

    public override void Draw()
    {
		foreach(var c in _drawables.Values)
		{
			if(c.CanDraw)
				c.Draw();
		}
    }

    public override void DebugDraw()
    {
		foreach(var c in _components.Values)
		{
			if(c.CanDebug)
				c.DebugDraw();
		}
    }

	//Utils methods 
	public T GetSceneAs<T>() where T : Scene => (T)Engine.ActiveScene;


	protected override void Dispose(bool disposable)
	{
		if(disposable && !Disposed)
		{
			Disposed = true;
		}
	}
}
