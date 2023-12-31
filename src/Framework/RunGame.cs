﻿using System;
using System.Threading.Tasks;
namespace MyAtsGame.GameClass;

/// <summary>
/// The Abstract SFMLGame class that needs to be implemented
/// </summary>
public partial class SfmlGameClass<GameConstants>
{
	/// <summary>Runs a game with the custom constants and configuration</summary>
	/// <typeparam name="GameType">The type for SFML Game engine</typeparam>
	/// <param name="init">The configuration for the game</param>
	/// <returns>returns any exceptions if any encountered. Otherwise retruns null</returns>
	public Exception? RunGame<GameType>(GameInit<GameConstants> init)
	where GameType : SfmlGameClass<GameConstants>, new()
	{
		GameType game = new();
		Exception? exp, load_exp = null;
		Scene<GameConstants>? next = null;
		exp = game.Initialize(init) ??
			game._back.Load(game) ??
			game._main.Load(game);
		if (exp != null) return null;
		bool is_loading = false, exit_called = false;
		long delta = 0;
		do
		{
			var scene = is_loading ? game._back : game._main;
			exp = game.OnBeginFrame() ??
				scene.Update(game, delta, out next) ??
				scene.Draw(game, game._window, delta) ??
				game.OnEndFrame(out delta, out exit_called);
			if (exp != null) return exp;
			else if (load_exp != null) return load_exp;
			if (next == null) continue;
			if (next == game._main || next == game._back)
				return new Exception("`next` must either be null, or must be a newly allocated Scene(without any of its inner functions called), and thus cannot be an already existing scene in the game");
			if (next != null)
			{
				is_loading = true;
				Task.Run(() =>
				{
					load_exp = game._main.Destroy(game) ?? next.Load(game);
					game._main = next;
					is_loading = false;
				});
			}
		}
		while (!game._exit_called);
		return init.Background.Destroy(game) ??
			init.Foreground.Destroy(game) ??
			game.ShutDown();
	}
}
