using Godot;
using System;
using System.Collections.Generic;
using Godot.Collections;
using Utilities;

[Tool]
public partial class JokePool : Control
{
	[Export] public string Title;
	[Export] public Label PoolTitle;
	[Export] public Array<Texture2D> moodletIcons;

	private List<MoodletData> usedMoodlets = new();
	private Node moodlets;

	[Signal] public delegate void OnMoodletSelectedEventHandler(MoodletData data);
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		PoolTitle.Text = Title;

		moodlets = GetNode<Node>("Moodlets");
		
		foreach (var item in moodlets.GetChildren())
		{
			((Button)item).Pressed += () => OnPressed((Button)item);
		}
	}

	private void OnPressed(Button button)
	{
		var data = ((MoodletEntity)button).GetMoodlet();
		
		EmitSignal("OnMoodletSelected", data);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void GenerateMoodletEntities(Array<MoodletData> data)
	{
		foreach (var item in moodlets.GetChildren())
		{
			MoodletData moodlet;

			 do
			 {
			 	moodlet = data.PickRandom();
			 } while (usedMoodlets.Contains(moodlet));
			
			 usedMoodlets.Add(moodlet);
			((MoodletEntity)item).SetMoodlet(moodlet);
		}
	}
}
