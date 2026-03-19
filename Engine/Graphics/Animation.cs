using System;

using Microsoft.Xna.Framework;

namespace Latte;

public sealed class Animation : IDisposable
{
	public bool IsRunning { get; private set; }
	public bool IsLooping { get; set; }

	public bool Disposed { get; private set; }

	public readonly AnimationFrame[] Frames;

	public int FramesCount => Frames.Length;

	public int CurrentFrameIndex { get; private set; }
	public AnimationFrame CurrentFrame => Frames[CurrentFrameIndex];

	public float CurrentFrameTime { get; private set; }

	public delegate void AnimationEndedHandler();

	public event AnimationEndedHandler AnimationEnded;

	public Animation(Rectangle[] frames, float duration, bool looping=false)
	{
		Frames = new AnimationFrame[frames.Length];

		for(int i=0; i<frames.Length; i++)
		{
			Frames[i] = new AnimationFrame(frames[i], duration);
		}

		IsRunning = true;
		IsLooping = looping;

		CurrentFrameIndex = 0;

		CurrentFrameTime = CurrentFrame.Duration;
	}

	public Animation(Rectangle[] frames, float[] durations, bool looping=false)
	{
		if(frames.Length != durations.Length)
			throw new Exception("frames and durations quantity not equal");

		Frames = new AnimationFrame[frames.Length];

		for(int i=0; i<frames.Length; i++)
		{
			Frames[i] = new AnimationFrame(frames[i], durations[i]);
		}

		IsRunning = true;
		IsLooping = looping;

		CurrentFrameIndex = 0;

		CurrentFrameTime = CurrentFrame.Duration;
	}

	public void Update(float dt)
	{
		if(IsRunning)
		{
			CurrentFrameTime -= dt;

			if(CurrentFrameTime <= 0)
			{
				CurrentFrameIndex++;

				if(CurrentFrameIndex == FramesCount)
				{
					if(!IsLooping)
					{
						IsRunning = false;
						return;
					}
					else
					{
						CurrentFrameIndex = 0;
						CurrentFrameTime = CurrentFrame.Duration;
					}

					AnimationEnded?.Invoke();
				}
				else
				{
					CurrentFrameTime = CurrentFrame.Duration;
				}

			}
		}
	}

	public void Play()
	{
		IsRunning = true;
	}

	public void Reset()
	{
		CurrentFrameIndex = 0;
		CurrentFrameTime = CurrentFrame.Duration;
	}

	public void Stop()
	{
		IsRunning = false;
	}

	public void Dispose()
	{
		for(int i=0; i<Frames.Length; i++)
		{
			Frames[i] = null;
		}

		Disposed = true;
	}
}
