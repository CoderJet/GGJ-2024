using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using Utilities;

public partial class CrowdManager : Node
{
	[Export] public PackedScene CrowdSpawner;
	[Export] public PackedScene CrowdEntity;

	[Export] public Vector2 VenueSize;
	[Export] public int CrowdSize;

	protected List<int> spawnerIndex;
    public List<Node2D> spawners;
	public List<Node2D> crowd;


	public CrowdManager()
	{
		crowd = new List<Node2D>();
		spawners = new List<Node2D>();
        spawnerIndex = new List<int>();
    }

	[Signal] public delegate void OnSpawnersPopulatedEventHandler();
	[Signal] public delegate void OnCrowdPopulatedEventHandler();
	
	// Called when the node enters the scene tree for the first time.res://Scripts/Managers/CrowdManager.cs
	public override void _Ready()
	{
	}

	public void GenerateSpawners()
	{
		Vector2 SpawnSpace = GetViewport().GetVisibleRect().Size;
		//Give some distance from the edge of the room to prevent overlap. 10px each side?
		SpawnSpace.X -= 20;
		//Cut off 150 from the bottom for the stage, and 80 from the top for the UI
		SpawnSpace.Y -= 230;

		var SpacingX = SpawnSpace.X / VenueSize.X;
		var SpacingY = (SpawnSpace.Y / VenueSize.Y);

		//shrink rows by 5% as we go backwards
		var scale_offset = 20 - VenueSize.Y;

		for(int y = 0; y < VenueSize.Y; y++)
		{

			float row_scale = scale_offset / 20;

			for(int x = 0; x < VenueSize.X; x++)
			{
				var node = CrowdSpawner.Instantiate<Node2D>();

				node.Scale = new Vector2(row_scale, row_scale);

				// XPOS
				// SpawnSpace indent
				float xPos = 10;
				// Scaling offset
				xPos += (1- row_scale) * SpacingX * VenueSize.X / 2;
				// Spacing offset
				xPos += (x + 0.5f) * SpacingX * row_scale;

				//YPOS
				// SpawnSpace indent
				float yPos = 160;					
				// Scaling offset
				yPos -= VenueSize.Y * 2;				
				// Spacing offset
				yPos += y * SpacingY * row_scale;


                node.Position = new Vector2(xPos, yPos);
				node.ZIndex = y;
				
				spawnerIndex.Add(spawners.Count);
				spawners.Add(node);
			}
            scale_offset += 1;
        }
        EmitSignal("OnSpawnersPopulated");
    }

	public void GenerateCrowd()
	{
		Random rand = new Random();

		int CrowdCap = Math.Clamp(CrowdSize, 1, (int)(VenueSize.X * VenueSize.Y));

        for (var i = 0; i < CrowdCap; i++)
		{	
			var node = CrowdEntity.Instantiate<Node2D>();
			
			((CrowdEntity)node).TempValue = i + 1;
			// TODO: Setup up the crowd internal data 'ere.

			node.ZIndex = 1;
			node.Position = Vector2.Zero;//new Vector2(GD.RandRange(-500, 500), GD.RandRange(50, 150));

            crowd.Add(node);
			int index = rand.Next(0,spawnerIndex.Count);
            spawners[spawnerIndex[index]].AddChild(node);
			spawnerIndex.RemoveAt(index);
		}

		EmitSignal("OnCrowdPopulated");
	}
}
