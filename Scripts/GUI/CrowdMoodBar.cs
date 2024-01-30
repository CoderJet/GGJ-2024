using Godot;
using System;

public partial class CrowdMoodBar : Control
{
	[Export] private ProgressBar moodBar;
	
	public void UpdateMoodBar(int value)
	{
		var currentValue = (int)moodBar.Value + value;
	
		moodBar.Value = currentValue;
	}
}
