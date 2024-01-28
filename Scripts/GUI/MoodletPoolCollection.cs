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
	public delegate void SetupMoodletSetEventHandler(Array<MoodletData> moodlets);
	
	public MoodletData SetupTopicMoodlet;
	public MoodletData SetupMoodlet;
	public MoodletData PunchlineTopicsMoodlet;
	public MoodletData PunchlineMoodlet;

	private Array<MoodletData> moodletCollection;

	public Array<MoodletData> TopicList;
    public Array<MoodletData> SetupList;
    public Array<MoodletData> PunchlineList;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
		((MoodletPool)SetupTopics).OnMoodletSelected += OnSetupTopicsMoodletSelected;
		((MoodletPool)Setup).OnMoodletSelected += OnSetupMoodletSelected;
		((MoodletPool)PunchlineTopics).OnMoodletSelected += OnPunchlineTopicsMoodletSelected;
		((MoodletPool)Punchline).OnMoodletSelected += OnPunchlineMoodletSelected;
	}

	public void Generate(int poolSize)
	{
        TopicList = new Array<MoodletData>(MoodletBuilder.Instance.GenerateMoodletList(JokeType.Left, poolSize));
        SetupList = new Array<MoodletData>(MoodletBuilder.Instance.GenerateMoodletList(JokeType.Middle, poolSize));
        PunchlineList = new Array<MoodletData>(MoodletBuilder.Instance.GenerateMoodletList(JokeType.Right, poolSize));

        ((MoodletPool)SetupTopics).GenerateMoodletEntities(TopicList);
        ((MoodletPool)PunchlineTopics).GenerateMoodletEntities(TopicList);
        ((MoodletPool)Setup).GenerateMoodletEntities(SetupList);
        ((MoodletPool)Punchline).GenerateMoodletEntities(PunchlineList);
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
		EmitSignal("SetupMoodletSet", new Array<MoodletData>{SetupTopicMoodlet, SetupMoodlet, PunchlineTopicsMoodlet, PunchlineMoodlet});
	}

	public void Reset()
	{
		SetupTopicMoodlet = null;
		SetupMoodlet = null;
		PunchlineTopicsMoodlet = null;
		PunchlineMoodlet = null;
	}
}
