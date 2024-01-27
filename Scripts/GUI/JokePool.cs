using Godot;
using System;
using System.Collections.Generic;
using Godot.Collections;

[Tool]
public partial class JokePool : Control
{
	[Export] public string Title;
	[Export] public Label PoolTitle;
	[Export] public Array<Texture2D> moodletIcons;

	private List<Texture2D> usedMoodlets = new();
	private Node moodlets;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		PoolTitle.Text = Title;

		moodlets = GetNode<Node>("Moodlets");
		
		foreach (var item in moodlets.GetChildren())
		{
			Texture2D icon;
			
			do
			{
				icon = moodletIcons.PickRandom();
			} while (usedMoodlets.Contains(icon));
			
			usedMoodlets.Add(icon);
			((MoodletEntity)item).SetIcon(icon);
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
