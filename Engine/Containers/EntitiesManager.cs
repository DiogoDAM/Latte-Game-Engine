using System;
using System.Collections.Generic;
using System.Collections;

namespace Latte;

public sealed class EntitiesManager : IDisposable, IEnumerable, IEnumerable<Entity>
{
	public List<Entity> Entities { get; private set; }
	private HashSet<Entity> _toAdd;
	private HashSet<Entity> _toRemove;

	public bool Disposed { get; private set; }

	int Count => Entities.Count;

	public EntitiesManager()
	{
		Entities = new();
		_toAdd = new();
		_toRemove = new();
	}

	public void Add(Entity e)
	{
		if(_toAdd.Contains(e))
			return;

		if(Entities.Contains(e))
			return;

		_toAdd.Add(e);
	}

	public bool Remove(Entity e)
	{
		if(_toAdd.Contains(e))
			return _toAdd.Remove(e);

		if(Entities.Contains(e))
		{
			_toRemove.Add(e);
			return true;
		}

		return false;
	}

	public void Clear()
	{
		_toAdd.Clear();
		_toRemove.Clear();

		for(int i=0; i<Entities.Count; i++)
		{
			var e = Entities[i];
			Entities.RemoveAt(i);

			e.Removed();
			if(e.ToDispose)
				e.Dispose();
		}
	}

	public void Update(float dt)
	{
		UpdateList();

		foreach(var e in Entities)
		{
			if(e.CanUpdate)
				e.Update(dt);
		}
	}

	public void Draw()
	{
		foreach(var e in Entities)
		{
			if(e.CanDraw)
				e.Draw();
		}
	}

	public void DebugDraw()
	{
		foreach(var e in Entities)
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
				Entities.Add(e);
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
				Entities.Remove(e);
				e.Removed();
				if(e.ToDispose)
					e.Dispose();
			}
			_toRemove.Clear();
		}
	}

    public IEnumerator GetEnumerator()
    {
		return Entities.GetEnumerator();
    }

    IEnumerator<Entity> IEnumerable<Entity>.GetEnumerator()
    {
		return Entities.GetEnumerator();
    }

    public void Dispose()
    {
		if(!Disposed)
		{
			for(int i=0; i<Entities.Count; i++)
			{
				var e = Entities[i];
				Entities.RemoveAt(i);

				e.Removed();
				e.Dispose();
			}

			_toAdd.Clear();
			_toRemove.Clear();

			Entities = null;
			_toAdd = null;
			_toRemove = null;

			Disposed = true;
		}
    }

}
