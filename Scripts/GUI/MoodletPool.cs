using Godot;
using System;
using System.Collections.Generic;
using Godot.Collections;
using Utilities;

public partial class MoodletPool : Control
{
	[Export] public string Title;
	[Export] public Label PoolTitle;
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
	
	public void GenerateMoodletEntities(Array<MoodletData> data)
	{
		var usedMoodlets = new Array<MoodletData>();
		
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
