using System;

namespace Latte;

public abstract class Behaviour : IDisposable
{
	public bool Active = true;
	public bool Visible = true;
	public bool Alive = true;
	public bool Debug = Engine.DebugMode;

	public bool Disposed { get; protected set; }

	public bool CanUpdate => Active && Alive && !Disposed;
	public bool CanDraw => Visible && Alive && !Disposed;
	public bool CanDebug => Debug && Alive && !Disposed;

	public Behaviour()
	{
	}

	public virtual void Awake()
	{
	}

	public virtual void Start()
	{
	}

	public virtual void Update(float dt)
	{
	}

	public virtual void Draw()
	{
	}

	public virtual void DebugDraw()
	{
	}

	public void Dispose()
	{
		Dispose(true);
		GC.SuppressFinalize(this);
	}

	protected virtual void Dispose(bool disposable)
	{
		if(disposable && !Disposed)
		{
			Disposed = true;
		}
	}
}
