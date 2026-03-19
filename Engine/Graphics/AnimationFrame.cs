using Microsoft.Xna.Framework;

namespace Latte;

public sealed class AnimationFrame
{
	public float Duration;

	public Rectangle Frame;

	public AnimationFrame(Rectangle frame, float duration)
	{
		Frame = frame;
		Duration = duration;
	}
}
