using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace Latte;

public sealed class EntitiesManager : IDisposable, IEnumerable, IEnumerable<Entity>
{
	private List<Entity> _entities { get; set; }
	private HashSet<Entity> _toAdd;
	private HashSet<Entity> _toRemove;

	public bool Disposed { get; private set; }

	public int Count => _entities.Count;

	public List<Entity> GetAllEntities() => _entities;

	public EntitiesManager()
	{
		_entities = new();
		_toAdd = new();
		_toRemove = new();
	}

	public void Add(Entity e)
	{
		if(_toAdd.Contains(e))
			return;

		if(_entities.Contains(e))
			return;

		_toAdd.Add(e);
	}

	public bool Remove(Entity e)
	{
		if(_toAdd.Contains(e))
			return _toAdd.Remove(e);

		if(_entities.Contains(e))
		{
			_toRemove.Add(e);
			return true;
		}

		return false;
	}

	public bool Contains(Entity e)
	{
		return _toAdd.Contains(e) || _entities.Contains(e);
	}

	public Entity Find(Predicate<Entity> predicate)
	{
		return _entities.Find(predicate);
	}

	public List<Entity> FindAll(Predicate<Entity> predicate)
	{
		return _entities.FindAll(predicate);
	}

	public T Get<T>() where T : Entity
	{
		return (T)_entities.Find(e => e is T);
	}

	public List<T> GetAll<T>() where T : Entity
	{
		return _entities.FindAll(e => e is T).OfType<T>().ToList<T>();
	}

	public void Clear()
	{
		_toAdd.Clear();
		_toRemove.Clear();

		for(int i=0; i<_entities.Count; i++)
		{
			var e = _entities[i];
			_entities.RemoveAt(i);

			e.Removed();
			if(e.ToDispose)
				e.Dispose();
		}
	}

	public void Update(float dt)
	{
		UpdateList();

		foreach(var e in _entities)
		{
			if(e.CanUpdate)
				e.Update(dt);
		}
	}

	public void Draw()
	{
		foreach(var e in _entities)
		{
			if(e.CanDraw)
				e.Draw();
		}
	}

	public void DebugDraw()
	{
		foreach(var e in _entities)
		{
			if(e.CanDebug)
				e.DebugDraw();
		}
	}

	private void UpdateList()
	{
		if(_toAdd.Count > 0)
		{
			foreach(var e in _toAdd)
			{
				_entities.Add(e);
				e.Added();
				e.Awake();
				e.Start();
			}
			_toAdd.Clear();
		}

		if(_toRemove.Count > 0)
		{
			foreach(var e in _toRemove)
			{
				_entities.Remove(e);
				e.Removed();
				if(e.ToDispose)
					e.Dispose();
			}
			_toRemove.Clear();
		}
	}

    public IEnumerator GetEnumerator()
    {
		return _entities.GetEnumerator();
    }

    IEnumerator<Entity> IEnumerable<Entity>.GetEnumerator()
    {
		return _entities.GetEnumerator();
    }

    public void Dispose()
    {
		if(!Disposed)
		{
			for(int i=0; i<_entities.Count; i++)
			{
				var e = _entities[i];
				_entities.RemoveAt(i);

				e.Removed();
				e.Dispose();
			}

			_toAdd.Clear();
			_toRemove.Clear();

			_entities = null;
			_toAdd = null;
			_toRemove = null;

			Disposed = true;
		}
    }

}
