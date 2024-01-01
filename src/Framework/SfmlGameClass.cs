using System;

using SFML.Graphics;
using SFML.System;

namespace MyAtsGame.GameClass;
public partial class SfmlGameClass<GameConstants>
{
	/// <summary>The settings to initialize the window</summary>
	protected WindowSettings _settings;
	/// <summary>The window being displayed</summary>
	protected RenderWindow _window = default!;
	/// <summary>The current scenes being managed by the game</summary>
	protected Scene<GameConstants> _back = default!, _main = default!;
	/// <summary>The clock object for measuring time between frames</summary>
	protected Clock _clock = new();
	/// <summary>If true, a signal to exit the game has been recived</summary>
	protected bool _exit_called;
	/// <summary>Initializes the game</summary>
	/// <param name="init">The initializing parameters to launch the game</param>
	/// <returns>returns the exception if any encountered; otherwise returns null</returns>
	protected Exception? Initialize(GameInit<GameConstants> init)
	{
		_back = init.Background;
		_main = init.Foreground;
		_settings = init.Settings;
		_window = new
		(
			new
			(
				_settings.Width,
				_settings.Height
			),
			_settings.GameTitle,
			_settings.Style
		);
		if (_settings.VerticalSync)
			_window.SetVerticalSyncEnabled(true);
		else
			_window.SetFramerateLimit(_settings.FramePerSeconds);
		_window.Closed += (o, e) => _exit_called = true;
		_window.Position = new Vector2i();
		return null;
	}
	/// <summary>The actions to perform start of a frame</summary>
	/// <returns>returns the exception if any encountered; otherwise returns null</returns>
	protected Exception? OnBeginFrame()
	{
		_window.DispatchEvents();
		_window.Clear();
		return null;
	}
	/// <summary>The actions to perform at the end of each frame</summary>
	/// <param name="time">out parameter set to the time between the start and end of the current frame</param>
	/// <param name="exit_called">out parameter set to true if exit has been called in this frame</param>
	/// <returns>returns the exception if any encountered; otherwise returns null</returns>
	protected Exception? OnEndFrame(out long time, out bool exit_called)
	{
		_window.Display();
		time = _clock.Restart().AsMicroseconds();
		exit_called = _exit_called;
		return null;
	}
	/// <summary>Disposes any allocated resources by the game</summary>
	/// <returns>returns the exception if any encountered; otherwise returns null</returns>
	protected Exception? ShutDown()
	{
		_window.Close();
		_window.Dispose();
		_clock.Dispose();
		return null;
	}
	/// <summary>Sends a signal to exit the game</summary>
	/// <returns>true if exit has been called beforehand, otherwise false</returns>
	public bool ExitCall()
	{
		bool prev = _exit_called;
		_exit_called = true;
		return prev;
	}
}