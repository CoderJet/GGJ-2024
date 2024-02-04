using Godot;
using System;

[Tool]
public partial class AudioSliderControl : Control
{
	[Export]
	private string labelText
	{
		get => GetLabelText();
		set => SetLabelText(value);
	}
	[Export] private Label volumeLabel;
	[Export] private HSlider volumeSlider;
	
	private void SetLabelText(string value)
	{
		if (volumeLabel != null)
			volumeLabel.Text = value;
	}
	
	private string GetLabelText()
	{
		return volumeLabel.Text;
	}

	private double GetVolume()
	{
		return volumeSlider.Value;
	}
	
	/// <summary>
	/// Called when the node enters the scene tree for the first time.
	/// </summary>
	public override void _Ready()
	{
		volumeLabel ??= GetNode<Label>("VolumeLabel");
		volumeSlider ??= GetNode<HSlider>("VolumeSlider");
	}
}
