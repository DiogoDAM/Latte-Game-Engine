using System;

using Microsoft.Xna.Framework;

namespace Latte;

public sealed class TransformComponent : Component
{
	public Vector2 Position = Vector2.Zero;
	public Vector2 Scale = Vector2.One;
	public float Rotation = 0f;

	public TransformComponent Parent;

	public Vector2 GlobalPosition => Position + ((Parent != null) ? Parent.GlobalPosition : Vector2.Zero);
	public Vector2 GlobalScale => Scale * ((Parent != null) ? Parent.GlobalScale : Vector2.One);
	public float GlobalRotation => Rotation + ((Parent != null) ? Parent.GlobalRotation : 0f);

	public TransformComponent()
	{
	}

	public TransformComponent(Vector2 pos)
	{
		Position = pos;
	}

	public TransformComponent(Vector2 pos, float rot)
	{
		Position = pos;
		Rotation = rot;
	}

	public TransformComponent(Vector2 pos, float rot, Vector2 scale)
	{
		Position = pos;
		Rotation = rot;
		Scale = scale;
	}

	public static Vector2 MoveTowards(TransformComponent curr, TransformComponent target, float vel)
	{
		Vector2 dir = target.GlobalPosition - curr.GlobalPosition;

		if(dir.Length() <= vel || dir.Length() == 0)
			return target.GlobalPosition;

		dir.Normalize();

		return curr.GlobalPosition + dir * vel;
	}

	public static Vector2 MoveTowards(Vector2 curr, Vector2 target, float vel)
	{
		Vector2 dir = target - curr;

		if(dir.Length() <= vel || dir.Length() == 0)
			return target;

		dir.Normalize();

		return curr + dir * vel;
	}

	public void MoveTowards(TransformComponent target, float vel)
	{
		Position = MoveTowards(this, target, vel);
	}

	public void MoveTowards(Vector2 target, float vel)
	{
		Position = MoveTowards(GlobalPosition, target, vel);
	}

	public override string ToString()
	{
		return $"Transform: (GlobalPosition: {GlobalPosition}, GlobalRotatin: {GlobalRotation}, GlobalScale: {GlobalScale})";
	}
}
