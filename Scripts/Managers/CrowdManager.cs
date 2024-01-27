using Godot;
using System;
using System.Collections.Generic;

public partial class CrowdManager : Node
{
	[Export] public PackedScene CrowdEntity;
	public List<Node2D> crowd;

	public CrowdManager()
	{
		crowd = new List<Node2D>();
	}

	[Signal] public delegate void OnCrowdPopulatedEventHandler();
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GenerateCrowd(12);
	}

	public void GenerateCrowd(int value)
	{
		for (var i = 0; i < value; i++)
		{
			var node = CrowdEntity.Instantiate<Node2D>();
			
			((CrowdEntity)node).TempValue = i + 1;
			// TODO: Setup up the crowd internal data 'ere.

			node.Position = new Vector2(GD.RandRange(-500, 500), GD.RandRange(50, 150));
			
			crowd.Add(node);
		}

		EmitSignal("OnCrowdPopulated");
	}
}
