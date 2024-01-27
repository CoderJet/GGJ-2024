using Godot;
using System;
using System.Collections.Generic;

public partial class CrowdManager : Node
{
	public List<CrowdEntity> crowd;

	public CrowdManager()
	{
		crowd = new List<CrowdEntity>();
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
			var entity = new CrowdEntity(i);
			
			// TODO: Setup up the crowd internal data 'ere.
			
			crowd.Add(entity);
		}

		EmitSignal("OnCrowdPopulated");
	}
}
