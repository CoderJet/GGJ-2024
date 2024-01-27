using GGJ24.Scripts.Resources;
using Godot;
// Plugin includes.
//      Resource Registry
using MonoCustomResourceRegistry;

[Tool]
[RegisteredType(nameof(JokeData), "", nameof(Resource))]
public partial class JokeData : Resource
{
	public enum JokeType
	{
		Topic,
		Setup,
		Punchline
	}
	
	[ExportCategory("Joke Data")] 
	[Export] public string Text;
	[Export] public JokeType Type;
	[Export] public JokeColour Colour;
	[Export] public int ImpactFactor = 1;
}
