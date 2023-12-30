using System;

namespace AtsUtils;

using KbKey = SFML.Window.Keyboard.Key;

public class KeyListener
{
	public enum KeyState : byte
	{
		Invalid,
		Long_Release,
		Just_Pressed,
		Long_Pressed,
		Just_Release,
	};
	public struct KeyPair
	{
		public KbKey Key = KbKey.Unknown;
		public KeyState State = KeyState.Invalid;
		public KeyPair() { }
	};
	readonly KeyPair[] _pairs;
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
	private int Index(KbKey key) => Array.FindIndex(_pairs, x => x.Key == key);
	public KeyState ViewKey(KbKey key, out int idx) => (idx = Index(key)) >= 0 ? _pairs[idx].State : KeyState.Invalid;
	public KeyState ViewKey(KbKey key) => ViewKey(key, out _);
	public static bool IsDown(KeyState state) => state == KeyState.Just_Pressed || state == KeyState.Long_Pressed;
	public bool IsDown(KbKey key) => IsDown(ViewKey(key));
	private void EventUpdate(int idx, bool press, KeyState state) => _pairs[idx].State = state switch
	{
		KeyState.Long_Release or KeyState.Just_Release => press ? KeyState.Just_Pressed : KeyState.Long_Release,
		_ => press ? KeyState.Long_Pressed : KeyState.Just_Release,
	};
	public void EventUpdate(KbKey key, bool press)
	{
		var state = ViewKey(key, out int idx);
		if (idx == -1)
			return;
		else EventUpdate(idx, press, state);
	}
	public void EndFrame()
	{
		for (int i = _pairs.Length - 1; i >= 0; i--)
		{
			var state = _pairs[i].State;
			EventUpdate(i, IsDown(state), state);
		}
	}
}