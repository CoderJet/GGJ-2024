using Godot;
// Plugin includes.
//      Resource Registry
using MonoCustomResourceRegistry;

[Tool]
[RegisteredType(nameof(JokeData), "", nameof(Resource))]
public partial class JokeData : Resource
{
	[ExportCategory("Joke Data")] 
	[Export] public string JokeType1;
	[Export] public string JokeType2;
	[Export] public string JokeType3;
	[Export] public Texture2D DisplayTexture;
}
