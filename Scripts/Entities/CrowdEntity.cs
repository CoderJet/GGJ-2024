using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class CrowdEntity : Node2D
{
	public int TempValue;
 
	public Array<MoodletData> moodletData;
 
    [Export] Sprite2D Body;
    [Export] Sprite2D Head;
 
    [Export] Texture2D[] Bodies;
    [Export] Texture2D[] Heads;
 
	[Export] public AnimationPlayer animPlayer;
    
	public void SetValue(int value)
	{
		TempValue = value;
	}	
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
    {
        Idle();
        RandomizeBody();
    }
 
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
 
	public void RandomizeBody()
    {
        Random rand = new Random();
        Body.Texture = Bodies[rand.Next(0, Bodies.Count())];
        Head.Texture = Heads[rand.Next(0, Heads.Count())];
    }
 
	public void GeneratePersonality(Array<MoodletData> moodlets)
	{
		moodletData = moodlets;
	}
 
	public int Listen(Array<MoodletData> Joke)
	{
		//Check against moodlets.
		int matches = moodletData.Where(x=>Joke.Contains(x)).Count();
 
		switch (matches)
		{
			case 2:
				// Haha, great story Mark!
                animPlayer.CurrentAnimation = "Laughing";
				break;
			case 0:
				// I hate this joke!
                animPlayer.CurrentAnimation = "Angry";
				break;
			case 1:
				// No reaction
				break;
        }
		return matches;
	}
 
	public void Idle()
	{
		animPlayer.CurrentAnimation = "Idle";
	}
}
