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

	public override string ToString()
	{
		return $"Transform: (GlobalPosition: {GlobalPosition}, GlobalRotatin: {GlobalRotation}, GlobalScale: {GlobalScale})";
	}
}
