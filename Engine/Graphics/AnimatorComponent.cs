using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Latte;

public sealed class AnimatorComponent : DrawableComponent
{
	private Dictionary<string, Animation> _animations;

	public Animation CurrentAnimation { get; private set; }
	public string CurrentAnimationName { get; private set; }

	public AnimationFrame CurrentAnimationFrame => CurrentAnimation.CurrentFrame;

	public float CurrentFrameDuration => CurrentAnimation.CurrentFrameTime;

	public TextureTiles Frames { get; private set; }

	public Vector2 Offset;

	public delegate void CurrentAnimationEndedHandler();

	public event CurrentAnimationEndedHandler CurrentAnimationEnded;

	private int _framesWidth;
	public override int Width { get { return _framesWidth; } }
	private int _framesHeight;
	public override int Height { get { return _framesHeight; } }

	public AnimatorComponent(Texture2D texture, int framesWidth, int framesHeight)
	{
		_animations = new();
		_framesWidth = framesWidth;
		_framesHeight = framesHeight;
		Frames = new(texture, _framesWidth, _framesHeight);
	}

	public AnimatorComponent(string texturePath, int framesWidth, int framesHeight)
	{
		_animations = new();
		_framesWidth = framesWidth;
		_framesHeight = framesHeight;
		Frames = new(texturePath, _framesWidth, _framesHeight);
	}

	public void AddAnimation(string name, float duration, bool looping, params int[] framesIndexes)
	{
		if(_animations.ContainsKey(name))
			throw new Exception("AnimatorComponent just have the animation name: " + name);

		Rectangle[] frames = new Rectangle[framesIndexes.Length];
		for(int i=0; i<frames.Length; i++)
		{
			frames[i] = Frames.Tiles[framesIndexes[i]];
		}

		_animations.Add(name, new Animation(frames, duration, looping));
		_animations[name].AnimationEnded += OnAnimationEnded;
	}

	public void PlayAnimation(string name)
	{
		if(!_animations.ContainsKey(name))
			throw new KeyNotFoundException($"AnimatorComponent the animation name: {name} not found");

		if(CurrentAnimationName == name)
			return;

		CurrentAnimation?.Stop();

		CurrentAnimation = _animations[name];

		CurrentAnimation.Reset();
		CurrentAnimation.Play();
		CurrentAnimationName = name;
	}

	public void Play()
	{
		CurrentAnimation.Play();
	}

	public void Reset()
	{
		CurrentAnimation.Reset();
	}

	public void Stop()
	{
		CurrentAnimation.Stop();
	}

	public override void Update(float dt)
	{
		CurrentAnimation.Update(dt);
	}

	public override void Draw()
	{
		Engine.SpriteBatch.Draw(Frames.Texture,
				Entity.Transform.GlobalPosition + Offset,
				CurrentAnimation.CurrentFrame.Frame,
				Color,
				Entity.Transform.GlobalRotation,
				Origin,
				Entity.Transform.GlobalScale,
				Flip,
				Depth);

	}

	private void OnAnimationEnded()
	{
		CurrentAnimationEnded?.Invoke();
	}

    protected override void Dispose(bool disposable)
    {
		if(disposable && !Disposed)
		{
			CurrentAnimationEnded = null;

			foreach(var animation in _animations.Values)
			{
				animation.Dispose();
			}

			CurrentAnimation = null;
			_animations = null;
			Frames = null;

			Disposed = true;
		}
    }
}
