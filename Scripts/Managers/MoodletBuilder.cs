using Godot;
using System;

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
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		((JokePool)SetupTopics).OnMoodletSelected += OnSetupTopicsMoodletSelected;
		((JokePool)Setup).OnMoodletSelected += OnSetupMoodletSelected;
		((JokePool)PunchlineTopics).OnMoodletSelected += OnPunchlineTopicsMoodletSelected;
		((JokePool)Punchline).OnMoodletSelected += OnPunchlineMoodletSelected;
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
}
