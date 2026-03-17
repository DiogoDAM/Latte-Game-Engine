using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;

namespace Latte;

public sealed class Canvas
{
	public RenderTarget2D Target { get; private set; }

	private Rectangle _destinationRectangle;

	public SamplerState SamplerState = SamplerState.PointWrap;

	public Canvas()
	{
		Target = new(Engine.GraphicsDevice, Engine.VirtualWidth, Engine.VirtualHeight);
		RecalculateResolution();
	}

	public void SetSize()
	{
		Target = new(Engine.GraphicsDevice, Engine.VirtualWidth, Engine.VirtualHeight);
		RecalculateResolution();
	}

	public void RecalculateResolution()
	{
		var screenSize = Engine.GraphicsDevice.PresentationParameters.Bounds;

        float scaleX = (float)screenSize.Width / Target.Width;
        float scaleY = (float)screenSize.Height / Target.Height;
        float scale = Math.Min(scaleX, scaleY);

        int newWidth = (int)(Target.Width * scale);
        int newHeight = (int)(Target.Height * scale);

        int posX = (screenSize.Width - newWidth) / 2;
        int posY = (screenSize.Height - newHeight) / 2;

        _destinationRectangle = new Rectangle(posX, posY, newWidth, newHeight);
	}

	public void SetRenderTarget()
	{
		Engine.GraphicsDevice.SetRenderTarget(Target);
		Engine.GraphicsDevice.Clear(Engine.ClearColor);
	}

	public void ResetAndDrawRenderTarget()
	{
		Engine.GraphicsDevice.SetRenderTarget(null);
		Engine.GraphicsDevice.Clear(Engine.ClearColor);

		Engine.SpriteBatch.Begin(samplerState: SamplerState);

			Engine.SpriteBatch.Draw(Target, _destinationRectangle, Color.White);

		Engine.SpriteBatch.End();
	}
}
