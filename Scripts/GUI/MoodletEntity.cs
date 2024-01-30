using Godot;
using System;
using System.Net.Mime;
using GGJ24.Scripts.Resources;

public partial class MoodletEntity : Button
{
	[Export] private TextureRect textureRect;
	private MoodletData moodletData;
	
	public override void _Ready()
	{
		textureRect ??= GetNode<TextureRect>("Icon");
	}
	
	public void SetMoodlet(MoodletData value)
	{
		moodletData = value;
		
		if (textureRect != null)
			textureRect.Texture = moodletData.icon;
	}
	
	public MoodletData GetMoodlet() => moodletData;
}
