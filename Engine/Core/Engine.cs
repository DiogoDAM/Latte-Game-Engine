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

	public static bool DebugMode { get; protected set; }  = false;

	public static Scene ActiveScene { get; protected set; }
	private static Scene _nextScene;

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

		if(_nextScene == null)
			throw new NullReferenceException("Starter Scene not selected");

    }

    protected override void Update(GameTime gameTime)
    {
		if(_nextScene != null)
			TransitionToScene();

		float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

		if(ActiveScene.CanUpdate)
			ActiveScene.Update(dt);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
		GraphicsDevice.Clear(ClearColor);

		if(ActiveScene.CanDraw)
		{
			ActiveScene.BeginDraw();
				ActiveScene.Draw();
			ActiveScene.EndDraw();

			ActiveScene.BeginDrawUi();
				ActiveScene.DrawUi();
			ActiveScene.EndDrawUi();
		}

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

	public static void ChangeScene(Scene scene)
	{
		_nextScene = scene;
	}

	private void TransitionToScene()
	{
		ActiveScene?.Disable();
		ActiveScene?.Dispose();
		ActiveScene = null;

		ActiveScene = _nextScene;
		ActiveScene.Activate();
		ActiveScene.Awake();
		ActiveScene.Start();

		_nextScene = null;
	}
}
