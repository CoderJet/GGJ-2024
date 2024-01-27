using Godot;
using System;
using System.Net.Mime;

public partial class MoodletEntity : Control
{
	[Export]
	public Texture2D Icon
	{
		get => textureRect?.Texture;
		set => SetIcon(value);
	}
	
	public void SetIcon(Texture2D value)
	{
		if (textureRect != null)
			textureRect.Texture = value;
	}

	[Export] private TextureRect textureRect;

	public override void _Ready()
	{
		textureRect ??= GetNode<TextureRect>("Select/Icon");
	}
}
