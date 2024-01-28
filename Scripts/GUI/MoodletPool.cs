using Godot;
using System;
using System.Collections.Generic;
using Godot.Collections;
using Utilities;

[Tool]
public partial class MoodletPool : Control
{
	[Export] public string Title;
	[Export] public Label PoolTitle;
	[Export] public Array<Texture2D> moodletIcons;

	private List<MoodletData> usedMoodlets = new();
	[Export] public Node moodlets;

	[Signal] public delegate void OnMoodletSelectedEventHandler(MoodletData data);
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		if (PoolTitle == null)
			PoolTitle = GetNode<Label>("Background/Label");
		
		PoolTitle.Text = Title;

		if(moodlets == null)
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
		usedMoodlets.Clear();
		
		foreach (var item in moodlets.GetChildren())
		{
			if (usedMoodlets.Count >= data.Count)
			{
				moodlets.RemoveChild(item);
				continue;
			}
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
