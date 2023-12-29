using System;
using Game = MyAtsGame.GameClass.SfmlGameClass;
using Window = SFML.Graphics.RenderWindow;
namespace MyAtsGame.GameClass;

public interface IScene
{
    Exception? Load(ref Game game, in Window window);
    Exception? Update(ref Game game, long delta, out IScene? next);
    Exception? Draw(in Window window, long delta);
    Exception? Destroy(ref Game game, in Window window);
}
