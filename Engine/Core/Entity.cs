using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;

namespace Latte;

public class Entity : Behaviour
{
	private Dictionary<Type, Component> _components;
	private Dictionary<Type, DrawableComponent> _drawables;

	public TransformComponent Transform;

	public string Tag;

	public readonly Guid Id;

	public Entity()
	{
		_components = new();
		_drawables = new();

		Transform = AddComponent<TransformComponent>(new ());

		Id = Guid.NewGuid();
	}

	public Entity(Vector2 pos)
	{
		_components = new();
		_drawables = new();

		Transform = AddComponent<TransformComponent>(new (pos));

		Id = Guid.NewGuid();
	}

	public Entity(float x, float y)
	{
		_components = new();
		_drawables = new();

		Transform = AddComponent<TransformComponent>(new (new Vector2(x, y)));

		Id = Guid.NewGuid();
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

	public void AddComponent(Component c)
	{
		Type type = c.GetType();
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
		}
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

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(this, obj))
            return true;
        
        if (obj is null || GetType() != obj.GetType())
            return false;
        
		Entity other = (Entity)obj;
        
		return Id == other.Id;
    }

	protected override void Dispose(bool disposable)
	{
		if(disposable && !Disposed)
		{
			Disposed = true;
		}
	}

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override string ToString()
    {
        return base.ToString();
    }
}
