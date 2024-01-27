using GGJ24.Scripts.Resources;
using Godot;
// Plugin includes.
//      Resource Registry
using MonoCustomResourceRegistry;

[Tool]
[RegisteredType(nameof(MoodletData), "", nameof(Resource))]
public partial class MoodletData : Resource
{
	public enum JokeType
	{
		Left,
		Middle,
		Right
	}
	
	[ExportCategory("Joke Data")] 
	[Export] public Texture2D icon;
	[Export] public JokeType Type;
	[Export] public JokeColour Colour;
	[Export] public int ImpactFactor = 1;
}
