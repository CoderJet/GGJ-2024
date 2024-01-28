using Godot;
using System;

public partial class MoodletManager : Node
{
	[Export] public MoodletPoolCollection moodletPool;
	
	public MoodletData SetupTopicMoodlet;
	public MoodletData SetupMoodlet;
	public MoodletData PunchlineTopicsMoodlet;
	public MoodletData PunchlineMoodlet;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
