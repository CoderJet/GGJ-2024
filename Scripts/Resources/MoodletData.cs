using GGJ24.Scripts.Resources;
using Godot;
// Plugin includes.
//      Resource Registry
using MonoCustomResourceRegistry;

[RegisteredType(nameof(MoodletData), "", nameof(Resource))]
public partial class MoodletData : Resource
{
	[ExportCategory("Joke Data")] 
	[Export] public Texture2D icon;
	[Export] public JokeType Type;
	[Export] public JokeColour Colour;
	[Export] public int ImpactFactor = 1;
}
