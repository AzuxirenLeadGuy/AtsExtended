using System;

using SFML.Graphics;
using SFML.System;

namespace MyAtsGame.GameClass
{
	public partial class SfmlGameClass<GameConstants>
	{
		protected WindowSettings _settings = default;
		protected RenderWindow _window = default!;
		protected IScene<GameConstants> _back = default!, _main = default!;
		protected Clock _clock = new();
		protected bool _exit_called;
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
		protected Exception? OnBeginFrame()
		{
			_window.DispatchEvents();
			_window.Clear();
			return null;
		}
		protected Exception? OnEndFrame(out long time, out bool exit_called)
		{
			_window.Display();
			time = _clock.Restart().AsMicroseconds();
			exit_called = _exit_called;
			return null;
		}
		protected Exception? ShutDown()
		{
			_window.Close();
			_window.Dispose();
			_clock.Dispose();
			return null;
		}
		public bool ExitCall()
		{
			bool prev = _exit_called;
			_exit_called = true;
			return prev;
		}
	}
}
