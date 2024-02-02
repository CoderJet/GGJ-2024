using System;
using System.Collections.Generic;
using Godot;
using Godot.Collections;

public partial class CrowdManager : Node
{
	[ExportCategory("Crowd Configuration")]
	[Export] public PackedScene CrowdSpawner;
	[Export] public PackedScene CrowdEntity;
	[ExportCategory("Venue Configuration")]
	[Export] public Vector2 VenueSize;
	[Export] public int CrowdSize;

	private List<int> spawnerIndex = new();
    public List<Node2D> spawners = new();
	public List<Node2D> crowd = new();
 
	[Signal] public delegate void OnSpawnersPopulatedEventHandler();
	[Signal] public delegate void OnCrowdPopulatedEventHandler();
 
	public void GenerateSpawners()
	{
		var SpawnSpace = GetViewport().GetVisibleRect().Size;
		//Give some distance from the edge of the room to prevent overlap. 10px each side?
		SpawnSpace.X -= 20;
		//Cut off 150 from the bottom for the stage, and 80 from the top for the UI
		SpawnSpace.Y -= 230;
 
		var SpacingX = SpawnSpace.X / VenueSize.X;
		var SpacingY = (SpawnSpace.Y / VenueSize.Y);
 
		//shrink rows by 5% as we go backwards
		var scale_offset = 20 - VenueSize.Y;
 
		for(var y = 0; y < VenueSize.Y; y++)
		{
			var row_scale = scale_offset / 20;
 
			for(var x = 0; x < VenueSize.X; x++)
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
				float yPos = 140;	
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
		var CrowdCap = Math.Clamp(CrowdSize, 1, (int)(VenueSize.X * VenueSize.Y));
 
        for (var i = 0; i < CrowdCap; i++)
		{	
			var node = CrowdEntity.Instantiate<Node2D>();
			
			// TODO: Setup up the crowd internal data 'ere.
			node.ZIndex = 1;
			node.Position = Vector2.Zero;
 
            crowd.Add(node);
			var index = GD.RandRange(0, spawnerIndex.Count - 1);
            spawners[spawnerIndex[index]].AddChild(node);
			spawnerIndex.RemoveAt(index);
		}
 
		EmitSignal("OnCrowdPopulated");
	}
 
	public int Joke(Array<MoodletData> joke)
	{
		// Initially reaction is neutral
		var reaction = 0;
 
		foreach(var node in crowd)
		{			
			switch(((CrowdEntity)node).Listen(joke))
			{
				case 0:
                    // If they dislike the joke, shift reaction towards negative
                    reaction--;
					break;
				case 1:
					// If they're ambivalent, do nothing
					break;
				default:
					// Otherwise, shift towards positive
					reaction++;
					break;
			}
        }
		return reaction;
	}
}
