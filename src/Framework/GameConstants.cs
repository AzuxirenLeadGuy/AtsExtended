namespace MyAtsGame.GameClass;

public class GameConstants
{
	public struct WindowSettings
	{
		public uint Width, Height;
		public byte FramePerSeconds;
		public bool VerticalSync;
		public SFML.Window.Styles Style;
	}
	public required string GameTitle { init; get; }
	public required WindowSettings Window {init; get;}
	// TODO Fill contents according to game
}
