using System.Linq;
using Godot;
using Godot.Collections;

public partial class CrowdEntity : Node2D
{
	[ExportCategory("Node References")]
    [Export] private Sprite2D Body;
    [Export] private Sprite2D Head;
	[Export] private AnimationPlayer animPlayer;
    [ExportCategory("Dynamic Asset Collections")]
    [Export] private Texture2D[] Bodies;
    [Export] private Texture2D[] Heads;

    public int BonesTickled = 1;
    
	private Array<MoodletData> moodletData;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
    {
        PlayIdleAnimation();
        RandomizeBody();
    }

	private void RandomizeBody()
    {
        Body.Texture = Bodies[GD.RandRange(0, Bodies.Length - 1)];
        Head.Texture = Heads[GD.RandRange(0, Heads.Length - 1)];
    }
 
	public void SetPersonality(Array<MoodletData> moodlets)
	{
		moodletData = moodlets;
	}
 
	public int Listen(Array<MoodletData> Joke)
	{
		//Check against moodlets.
		BonesTickled = moodletData.Count(Joke.Contains);
		return BonesTickled;
	}
 
	public void PlayIdleAnimation()
	{
		animPlayer.Play("Idle");
	}

	public void PlayLaughingAnimation()
	{
		animPlayer.Play("Laughing");
	}

	public void PlayAngryAnimation()
	{
		animPlayer.Play("Angry");
	}
}
