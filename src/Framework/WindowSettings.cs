namespace MyAtsGame.GameClass;

public struct WindowSettings
{
	public uint Width, Height;
	public byte FramePerSeconds;
	public bool VerticalSync;
	public SFML.Window.Styles Style;
	public string GameTitle;
}
