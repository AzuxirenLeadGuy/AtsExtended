namespace MyAtsGame.GameClass;

public struct GameInit<GameConstants>
{
    public GameConstants Constants;
    public WindowSettings Settings;
    public IScene<GameConstants> Foreground, Background;
}