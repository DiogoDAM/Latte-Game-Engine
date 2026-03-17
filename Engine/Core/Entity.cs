using System;

namespace Latte;

public class Entity : Behaviour
{
	public Entity() : base()
	{
	}

	public T GetSceneAs<T>() where T : Scene => (T)Engine.ActiveScene;


	public virtual void Added()
	{
	}

	public virtual void Removed()
	{
	}

	protected override void Dispose(bool disposable)
	{
		if(disposable && !Disposed)
		{
			Disposed = true;
		}
	}
}
