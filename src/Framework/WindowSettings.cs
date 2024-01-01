namespace MyAtsGame.GameClass;
/// <summary>The settings needed to initialize a window in SFML</summary>
public struct WindowSettings
{
	/// <summary>The dimensions of the window</summary>
	public uint Width, Height;
	/// <summary>The FPS of the game(ignored when VerticalSync is set to true)</summary>
	public byte FramePerSeconds;
	/// <summary>
	/// The setting to enable the vertical sync in the window.
	/// When true, The FramePerSeconds value is ignored
	/// </summary>
	public bool VerticalSync;
	/// <summary>The style of the window</summary>
	public SFML.Window.Styles Style;
	/// <summary>The text to display at the title-bar of the window</summary>
	public string GameTitle;
}
