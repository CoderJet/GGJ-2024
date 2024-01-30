using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

public partial class CrowdEntity : Node2D
{
	[ExportCategory("Node References")]
    [Export] private Sprite2D Body;
    [Export] private Sprite2D Head;
	[Export] public AnimationPlayer animPlayer;
    [ExportCategory("Dynamic Asset Collections")]
    [Export] private Texture2D[] Bodies;
    [Export] private Texture2D[] Heads;
 
	private Array<MoodletData> moodletData;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
    {
        PlayIdleAnimation();
        RandomizeBody();
    }

	private void RandomizeBody()
    {
        var rand = new Random();
        Body.Texture = Bodies[rand.Next(0, Bodies.Length)];
        Head.Texture = Heads[rand.Next(0, Heads.Length)];
    }
 
	public void SetPersonality(Array<MoodletData> moodlets)
	{
		moodletData = moodlets;
	}
 
	public int Listen(Array<MoodletData> Joke)
	{
		//Check against moodlets.
		var matches = moodletData.Count(Joke.Contains);
 
		switch (matches)
		{
			case 2:
				// Haha, great story Mark!
				PlayLaughingAnimation();
				break;
			case 0:
				// I hate this joke!
				PlayAngryAnimation();
				break;
			case 1:
				// No reaction
				break;
        }
		return matches;
	}
 
	public void PlayIdleAnimation()
	{
		animPlayer.Play("Idle");
	}

	public void PlayLaughingAnimation()
	{
		animPlayer.Play("Laughing");
		//await ToSignal(animPlayer, "animation_finished");
		
	}

	public void PlayAngryAnimation()
	{
		animPlayer.Play("Angry");
		//await ToSignal(animPlayer, "animation_finished");
	}
}
