namespace MyAtsGame.GameClass;
/// <summary>The settings to initialize a custom defined game</summary>
public struct GameInit<GameConstants>
{
	/// <summary>The custom game-specific constants</summary>
	public GameConstants Constants;
	/// <summary>The SFML Window settings for the game</summary>
	public WindowSettings Settings;
	/// <summary>The initial Scenes for the game at launch</summary>
	public Scene<GameConstants> Foreground, Background;
}