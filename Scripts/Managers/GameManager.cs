using Godot;
using System;
using Utilities;

public partial class GameManager : Node
{
	[ExportCategory("Crowd Config")] 
	[Export] public Node CrowdCollection;
	[Export] public PackedScene crowdEntity;
	[Export] public CrowdManager crowdManager;
	[Export] public JokeBuilder jokeBuilder;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		crowdManager.OnCrowdPopulated += OnOnCrowdPopulated;
	}

	private void OnOnCrowdPopulated()
	{
		foreach (var entity in crowdManager.crowd)
		{
			var data = crowdEntity.Instantiate();
			crowdEntity.SetScript(entity);
			
			Logger.Info(((CrowdEntity)data).TempValue);

			CrowdCollection.AddChild(data);
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}
}
