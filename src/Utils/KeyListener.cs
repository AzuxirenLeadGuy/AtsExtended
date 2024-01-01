using System;

namespace AtsUtils;

using KbKey = SFML.Window.Keyboard.Key;
/// <summary>
/// KeyListener is a basic Utility for detecting Keyboard key-presses.
/// It is not necessary for your game, but it can prove helpful
/// </summary>
public class KeyListener
{
    /// <summary> The various state of a Key</summary>
    public enum KeyState : byte
    {
        /// <summary>
        /// This value is returned by the `KeyListener.ViewKey()` method
        /// for keys that have not been registered
        /// </summary>
        Invalid,
        /// <summary>
        /// This key is up (not pressed)
        /// for the current and previous frame
        /// </summary>
        Long_Release,
        /// <summary>
        /// This key is down (pressed) in the current
        /// frame, but was up (not pressed) in
        /// the previous frame.
        /// </summary>
        Just_Pressed,
        /// <summary>
        /// This key is down (pressed)
        /// for the current and previous frame
        /// </summary>
        Long_Pressed,
        /// <summary>
        /// This key is up (not pressed) in the current
        /// frame, but was down (pressed) in
        /// the previous frame.
        /// </summary>
        Just_Release,
    };
    /// <summary>
    /// The KeyListener maintains an array of
    /// this type for its evaluations
    /// </summary>
    protected struct KeyPair
    {
        /// <summary>The key being listened</summary>
        public KbKey Key = KbKey.Unknown;
        /// <summary>The state of the key</summary>
        public KeyState State = KeyState.Invalid;
        /// <summary>default Constructor</summary>
        public KeyPair() { }
    };
    /// <summary>The array of keys being listened for</summary>
    readonly KeyPair[] _pairs;
    /// <summary>Default constructor</summary>
    /// <param name="keys">The keys to listen for</param>
    public KeyListener(KbKey[] keys)
    {
        _pairs = new KeyPair[keys.Length];
        for (int i = 0; i < keys.Length; i++)
        {
            _pairs[i] = new KeyPair()
            {
                Key = keys[i],
                State = KeyState.Long_Release,
            };
        }
        Array.Sort(_pairs, (x, y) => x.Key.CompareTo(y.Key));
    }
	/// <summary>Gets the index for the key within the internal array</summary>
	/// <param name="key">The key to search</param>
	/// <returns>A non-negative index for the key if found, otherwise -1</returns>
    protected int Index(KbKey key) => Array.FindIndex(_pairs, x => x.Key == key);
	/// <summary>Returns the state for the key, along with its index in the internal array</summary>
	/// <param name="key">The key to search</param>
	/// <param name="idx">
	/// The out parameter which is index of the key in the internal array.
	/// If found, the index is non-negative, otherwise -1
	/// </param>
	/// <returns>The state of the queried key</returns>
    protected KeyState ViewKey(KbKey key, out int idx) => (idx = Index(key)) >= 0 ? _pairs[idx].State : KeyState.Invalid;
	/// <summary>Checks the state of the queried key</summary>
	/// <param name="key">The queried key</param>
	/// <returns>The state of the queried key</returns>
    public KeyState ViewKey(KbKey key) => ViewKey(key, out _);
	/// <summary>Checks if key is down for the given state</summary>
	/// <param name="state">The state to check for</param>
	/// <returns>true if key is pressed, and false if key is released. Also returns false if key is not registered</returns>
    protected static bool IsDown(KeyState state) => state == KeyState.Just_Pressed || state == KeyState.Long_Pressed;
	/// <summary>Checks if the queried key is down(pressed)</summary>
	/// <param name="key">The key that is queried</param>
	/// <returns>true if key is pressed, and false if key is released. Also returns false if key is not registered</returns>
    public bool IsDown(KbKey key) => IsDown(ViewKey(key));
    private KeyState EventUpdate(int idx, bool press, KeyState state) => _pairs[idx].State = state switch
    {
        KeyState.Long_Release or KeyState.Just_Release => press ? KeyState.Just_Pressed : KeyState.Long_Release,
        _ => press ? KeyState.Long_Pressed : KeyState.Just_Release,
    };
    /// <summary>Updates the key state by the events in an SFML game</summary>
    /// <param name="key">The key detected</param>
    /// <param name="press">if true, the key is down(pressed), otherwise up(released)</param>
	/// <returns>The KeyState of the key that has been set now</returns>
    public KeyState EventUpdate(KbKey key, bool press)
    {
        var state = ViewKey(key, out int idx);
        return idx == -1
            ? KeyState.Invalid
            : EventUpdate(idx, press, state);
    }
    /// <summary>Keylistener called to end the frame</summary>
    public void EndFrame()
    {
        for (int i = _pairs.Length - 1; i >= 0; i--)
        {
            var state = _pairs[i].State;
            EventUpdate(i, IsDown(state), state);
        }
    }
}