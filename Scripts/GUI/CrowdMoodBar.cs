using Godot;
using System;

public partial class CrowdMoodBar : Control
{
	[Export] private ProgressBar moodBar;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void UpdateMoodBar(int value)
	{
		var currentValue = (int)moodBar.Value;
		
		
	}
}
