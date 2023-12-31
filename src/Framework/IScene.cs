using System;

using SFML.Graphics;
namespace MyAtsGame.GameClass;
/// <summary>
/// A game can be described as a set of Scenes that contains
/// the focused, small portions of the game.
/// </summary>
/// <typeparam name="GameConstants">The game constants type on which the scene is based on</typeparam>
public abstract class Scene<GameConstants>
{
    /// <summary>Initializes this scene; This method is called only at the start by the game object</summary>
    /// <param name="game">The game running the scene</param>
    /// <returns>returns the exception if any encountered; otherwise returns null</returns>
    public virtual Exception? Load(SfmlGameClass<GameConstants> game) => null;
    /// <summary>
    /// Updates the given scene; also sends the next scene to progress without calling
    /// the Load() function on it. It is called automatically by the game object when needed.
    /// </summary>
    /// <param name="game">The game object being referenced</param>
    /// <param name="delta">The time difference from the previous frame</param>
    /// <param name="next">The reference to the next, uninitialized scene</param>
    /// <returns>returns the exception if any encountered; otherwise returns null</returns>
    public virtual Exception? Update(SfmlGameClass<GameConstants> game, long delta, out Scene<GameConstants>? next)
    {
        next = null;
        return null;
    }
    /// <summary>
    /// Draws/Renders the game to the screen
    /// </summary>
    /// <param name="game">The game object being referenced</param>
    /// <param name="window">The object that can render to the window</param>
    /// <param name="delta">The time difference from the previous frame</param>
    /// <returns>returns the exception if any encountered; otherwise returns null</returns>
    public virtual Exception? Draw(SfmlGameClass<GameConstants> game, in RenderTarget window, long delta) => null;
    /// <summary> Disposes any memory held by the scene.</summary>
    /// <param name="game">The game object being referenced</param>
    /// <returns>returns the exception if any encountered; otherwise returns null</returns>
    public virtual Exception? Destroy(SfmlGameClass<GameConstants> game) => null;
}
