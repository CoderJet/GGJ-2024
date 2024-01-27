using Godot;
using System;
using System.Collections.Generic;
using System.IO;
using Godot.Collections;
using Utilities;
using FileAccess = Godot.FileAccess;

public partial class MoodletBuilder : Control
{
	[Export] public Control SetupTopics;
	[Export] public Control Setup;
	[Export] public Control PunchlineTopics;
	[Export] public Control Punchline;

	private MoodletData SetupTopicMoodlet;
	private MoodletData SetupMoodlet;
	private MoodletData PunchlineTopicsMoodlet;
	private MoodletData PunchlineMoodlet;

	private Array<MoodletData> moodletCollection;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		((JokePool)SetupTopics).OnMoodletSelected += OnSetupTopicsMoodletSelected;
		((JokePool)Setup).OnMoodletSelected += OnSetupMoodletSelected;
		((JokePool)PunchlineTopics).OnMoodletSelected += OnPunchlineTopicsMoodletSelected;
		((JokePool)Punchline).OnMoodletSelected += OnPunchlineMoodletSelected;
		
		// res://Scenes/Resources/Moodlets/
		moodletCollection = new Array<MoodletData>();
		using var dir = DirAccess.Open("res://Scenes/Resources/Moodlets");

		if (dir == null) return;
		
		dir.ListDirBegin();
		var fileName = dir.GetNext();

		while (fileName != string.Empty)
		{
			var path = $"res://Scenes/Resources/Moodlets/{fileName}";

			if (FileAccess.FileExists(path))
			{
				var t = ResourceLoader.Load<MoodletData>(path);

				moodletCollection.Add(t);
			}
			fileName = dir.GetNext();
		}
		
		(SetupTopics as JokePool).GenerateMoodletEntities(GenerateMoodletList());
		(Setup as JokePool).GenerateMoodletEntities(GenerateMoodletList());
		(PunchlineTopics as JokePool).GenerateMoodletEntities(GenerateMoodletList());
		(Punchline as JokePool).GenerateMoodletEntities(GenerateMoodletList());
	}

	private void OnSetupTopicsMoodletSelected(MoodletData data)
	{
		SetupTopicMoodlet = data;
	}

	private void OnSetupMoodletSelected(MoodletData data)
	{
		SetupMoodlet = data;
	}

	private void OnPunchlineTopicsMoodletSelected(MoodletData data)
	{
		PunchlineTopicsMoodlet = data;
	}

	private void OnPunchlineMoodletSelected(MoodletData data)
	{
		PunchlineMoodlet = data;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public Array<MoodletData> GenerateMoodletList(int qty = 4)
	{
		Array<MoodletData> data = new Array<MoodletData>();

		for (var i = 0; i < qty; i++)
		{
			MoodletData temp;
			
			do
			{
				temp = moodletCollection.PickRandom();
			} while (data.Contains(temp));
			data.Add(temp);
		}
		
		return data;
	}
}
