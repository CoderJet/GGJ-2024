using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class CrowdEntity : Node2D
{
	public int TempValue;

	public List<MoodletData> moodletData;

    [Export] Sprite2D Body;
    [Export] Sprite2D Head;

    [Export] Texture2D[] Bodies;
    [Export] Texture2D[] Heads;
    
	public void SetValue(int value)
	{
		TempValue = value;
	}	
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
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

	public void GeneratePersonality()
	{
		
	}
}
