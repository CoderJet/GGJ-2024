using Godot;
using System;
using Godot.Collections;

public partial class MoodletPoolCollection : Control
{
	[Export] public Control SetupTopics;
	[Export] public Control Setup;
	[Export] public Control PunchlineTopics;
	[Export] public Control Punchline;

	[Signal]
	public delegate void SetupMoodletSetEventHandler(MoodletData topic, MoodletData Setup);
	
	public MoodletData SetupTopicMoodlet;
	public MoodletData SetupMoodlet;
	public MoodletData PunchlineTopicsMoodlet;
	public MoodletData PunchlineMoodlet;

	private Array<MoodletData> moodletCollection;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		((MoodletPool)SetupTopics).OnMoodletSelected += OnSetupTopicsMoodletSelected;
		((MoodletPool)Setup).OnMoodletSelected += OnSetupMoodletSelected;
		((MoodletPool)PunchlineTopics).OnMoodletSelected += OnPunchlineTopicsMoodletSelected;
		((MoodletPool)Punchline).OnMoodletSelected += OnPunchlineMoodletSelected;
		
		((MoodletPool)SetupTopics).GenerateMoodletEntities(MoodletBuilder.Instance.GenerateMoodletList(JokeType.Left));
		((MoodletPool)Setup).GenerateMoodletEntities(MoodletBuilder.Instance.GenerateMoodletList(JokeType.Middle));
		((MoodletPool)PunchlineTopics).GenerateMoodletEntities(MoodletBuilder.Instance.GenerateMoodletList(JokeType.Left));
		((MoodletPool)Punchline).GenerateMoodletEntities(MoodletBuilder.Instance.GenerateMoodletList(JokeType.Middle));
	}

	private void OnSetupTopicsMoodletSelected(MoodletData data)
	{
		SetupTopicMoodlet = data;
	}

	private void OnSetupMoodletSelected(MoodletData data)
	{
		SetupMoodlet = data;
		EmitSignal("SetupMoodletSet", SetupTopicMoodlet, SetupMoodlet);
	}

	private void OnPunchlineTopicsMoodletSelected(MoodletData data)
	{
		PunchlineTopicsMoodlet = data;
	}

	private void OnPunchlineMoodletSelected(MoodletData data)
	{
		PunchlineMoodlet = data;
		EmitSignal("SetupMoodletSet", PunchlineTopicsMoodlet, PunchlineMoodlet);
	}

	public void Reset()
	{
		SetupTopicMoodlet = null;
		SetupMoodlet = null;
		PunchlineTopicsMoodlet = null;
		PunchlineMoodlet = null;
	}
}
