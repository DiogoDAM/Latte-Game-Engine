using System;

namespace Latte;

public class Scene : Behaviour
{
	protected EntitiesManager Entities;

	public int EntitiesCount => Entities.Count;

	public Scene()
	{
		Entities = new();
	}

	public void AddEntity(Entity e)
	{
		Entities.Add(e);
	}

	public bool RemoveEntity(Entity e)
	{
		return Entities.Remove(e);
	}

	public virtual void Activate()
	{
	}

	public virtual void Disable()
	{
	}

    public override void Update(float dt)
    {
		Entities.Update(dt);
    }

	public virtual void DrawUi()
	{
	}
}
