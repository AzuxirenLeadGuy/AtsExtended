using System;
using System.Threading.Tasks;
namespace MyAtsGame.GameClass;

/// <summary>
/// The Abstract SFMLGame class that needs to be implemented
/// </summary>
public partial class SfmlGameClass
{
	public static Exception? RunGame(in GameConstants constants, in IScene foreground, in IScene background)
	{
		SfmlGameClass game = new(constants, foreground, background);
		Exception? exp = game.Initialize();
		if (exp != null) { return exp; }
		else if ((exp = game.main.Load(
						ref game,
						in game._window)) != null) { return exp; }
		else if ((exp = game.back.Load(
						ref game,
						in game._window)) != null) { return exp; }

		Exception? loading_exception = null;
		bool is_loading = false, exit_called;
		long time = 0;
		do
		{
			var cur_scene = is_loading ? game.back : game.main;
			if ((exp = game.OnBeginFrame()) != null) { return exp; }
			else if ((exp = cur_scene.Update(
								ref game,
								time,
								out var next)) != null) { return exp; }
			else if ((exp = cur_scene.Draw(
								in game._window, time)) != null) { return exp; }
			else if ((exp = game.OnEndFrame(out time, out exit_called)) != null) { return exp; }
			else if (loading_exception != null) { return loading_exception; }
			else if (is_loading) { continue; }
			else if (next != null)
			{
				is_loading = true;
				Task.Run(() =>
				{
					loading_exception = game.main.Destroy(ref game, in game._window);
					if (loading_exception != null) return;
					game.main = next;
					loading_exception = game.main.Load(ref game, in game._window);
					is_loading = false;
				});
			}
		} while (!exit_called);
		return game.main.Destroy(ref game, in game._window) ??
			game.back.Destroy(ref game, in game._window) ??
			game.ShutDown();
	}
}
