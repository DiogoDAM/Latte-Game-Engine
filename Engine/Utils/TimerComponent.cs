namespace Latte;

public sealed class TimerComponent : Component
{
	public float Time { get; set; }
	public float TimeLeft { get; private set; }

	public bool Running { get; private set; }
	public bool Infinite { get; set; }

	public delegate void TimeUpEventHandler();

	public event TimeUpEventHandler TimeUp;

	public TimerComponent(float time, bool infinite=false)
	{
		Time = time;
		TimeLeft = Time;

		Running = true;
		Infinite = infinite;
	}

	public void Pause() => Running = false;
	public void Resume() => Running = true;
	public void Restart() => TimeLeft = Time;

    public override void Update(float dt)
    {
		if(Running)
		{
			TimeLeft -= dt;

			if(TimeLeft <= 0f)
			{
				TimeUp?.Invoke();
				if(!Infinite)
				{
					Running = false;
					return;
				}

				TimeLeft = Time;
			}
		}
    }

    protected override void Dispose(bool disposable)
    {
		if(disposable && !Disposed)
		{
			Running = false;
			TimeUp = null;

			Disposed = true;
		}
    }
}
