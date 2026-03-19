using System;

using Microsoft.Xna.Framework;

namespace Latte;

public sealed class PursuitCameraComponent : Component
{
	private Vector2 _pos;
	public Vector2 Position => _pos + Offset;
	public Vector2 Offset;
	public Vector2 Zoom;
	public float Rotation;

	public Matrix Matrix => Matrix.CreateTranslation(-Position.X, -Position.Y, 0f) *
		Matrix.CreateRotationZ(Rotation) *
		Matrix.CreateScale(Zoom.X, Zoom.Y, 1f);

	public bool IsChasing;

	public float ChaseAmount;

	public PursuitCameraComponent(float chaseAmount=0.1f, bool chase=false)
	{
		ChaseAmount = chaseAmount;
		_pos = Vector2.Zero;
		Offset = Vector2.Zero;
		Zoom = Vector2.One;
		Rotation = 0f;
		IsChasing = chase;
	}

    public override void Update(float dt)
    {
		if(IsChasing)
		{
			_pos.X = MathHelper.Lerp(_pos.X, Entity.Transform.GlobalPosition.X, ChaseAmount);
			_pos.Y = MathHelper.Lerp(_pos.Y, Entity.Transform.GlobalPosition.Y, ChaseAmount);
		}

		Engine.TransformMatrix = Matrix;
    }
}
