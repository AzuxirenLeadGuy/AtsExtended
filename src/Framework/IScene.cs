using System;
namespace MyAtsGame.GameClass;

public interface IScene<GameConstants>
{
    Exception? Load(SfmlGameClass<GameConstants> game) => null;
    Exception? Update(SfmlGameClass<GameConstants> game, long delta, out IScene<GameConstants>? next)
    {
        next = null;
        return null;
    }
    Exception? Draw(SfmlGameClass<GameConstants> game, long delta) => null;
    Exception? Destroy(SfmlGameClass<GameConstants> game) => null;
}
