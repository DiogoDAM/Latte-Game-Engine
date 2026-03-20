using System;

namespace Latte;

public abstract class Behaviour : IDisposable
{
	public bool Active = true;
	public bool Visible = true;
	public bool Alive = true;
	public bool Debug = false;

	public bool Disposed { get; protected set; }
	public bool ToDispose { get; protected set; }

	public bool CanUpdate => Active && Alive && !Disposed;
	public bool CanDraw => Visible && Alive && !Disposed;
	public bool CanDebug => (Debug || Engine.DebugMode) && Alive && !Disposed;

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

	public void Kill()
	{
		Active = false;
		Visible = false;
		Alive = false;
		Debug = false;
	}

	public void Revive()
	{
		Active = true;
		Visible = true;
		Alive = true;
		Debug = Engine.DebugMode;
	}

	public virtual void Destroy()
	{
		Kill();
		ToDispose = true;
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
