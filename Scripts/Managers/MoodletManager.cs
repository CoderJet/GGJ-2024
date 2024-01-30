using Godot;
using System;

public partial class MoodletManager : Node
{
	[Export] public MoodletPoolCollection moodletPool;
	
	public MoodletData SetupTopicMoodlet;
	public MoodletData SetupMoodlet;
	public MoodletData PunchlineTopicsMoodlet;
	public MoodletData PunchlineMoodlet;
}
