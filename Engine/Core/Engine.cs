using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Latte;

public abstract class Engine : Game
{
	private static Engine _instance;
	public static Engine Instance => _instance;

	public static GraphicsDeviceManager Graphics;
	public static new GraphicsDevice GraphicsDevice;
	public static new ContentManager Content;
	public static SpriteBatch SpriteBatch;

	public static int WindowWidth => Graphics.PreferredBackBufferWidth;
	public static int WindowHeight => Graphics.PreferredBackBufferHeight;

	public static int VirtualWidth { get; private set; }
	public static int VirtualHeight { get; private set; }
	
	public static Color ClearColor = Color.CornflowerBlue;

	public Engine(string windowTitle, int windowWidth, int windowHeight, bool isFullScreen) : base()
	{
		_instance = this;

		Graphics = new(this);

		Content = base.Content;
		Content.RootDirectory = "Content";

		SetWindowSize(windowWidth, windowHeight);
		SetVirtualWindowSize(windowWidth, windowHeight);
		SetWindowTitle(windowTitle);
		SetFullScreenMode(isFullScreen);

		IsMouseVisible = true;
	}

    protected override void Initialize()
    {
		GraphicsDevice = base.GraphicsDevice;
		SpriteBatch = new(GraphicsDevice);

        base.Initialize();
    }

    protected override void Update(GameTime gameTime)
    {
		float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
		GraphicsDevice.Clear(ClearColor);

        base.Draw(gameTime);
    }

	public void SetWindowTitle(string title)
	{
		Window.Title = title;
	}

	public static void SetWindowSize(int width, int height)
	{
		Graphics.PreferredBackBufferWidth = width;
		Graphics.PreferredBackBufferHeight = height;
		Graphics.ApplyChanges();
	}

	public static void SetVirtualWindowSize(int width, int height)
	{
		VirtualWidth = width;
		VirtualHeight = height;
	}

	public static void SetFullScreenMode(bool value)
	{
		Graphics.IsFullScreen = value;
		Graphics.ApplyChanges();
	}

	public static void ToggleFullScreenMode()
	{
		Graphics.ToggleFullScreen();
		Graphics.ApplyChanges();
	}
}
