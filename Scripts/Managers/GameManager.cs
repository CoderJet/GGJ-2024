using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Utilities;

public partial class GameManager : Node
{
	[ExportCategory("Crowd Config")] 
	[Export] public Node CrowdCollection;
	[Export] public CrowdManager crowdManager;

	#region moodlet_settings
	// Applies to all pools
	[Export] public int moodletCount;
    public Array<MoodletData> moodletSet;
	// How to distribute the moodlets we assign (e.g. 0.4,0.3,0.2,0.1 would do 40%, 30%, 20%, 10%). 
	[Export] public Array<float> MoodletWeighting;
    #endregion

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        crowdManager.OnSpawnersPopulated += OnOnSpawnersPopulated;
        crowdManager.OnCrowdPopulated += OnOnCrowdPopulated;

        // Run moodlet builder to populate MoodletCount random moodlets for the set
        moodletSet = MoodletBuilder.Instance.GenerateMoodletList(moodletCount);

        crowdManager.GenerateSpawners();
        crowdManager.GenerateCrowd();

		// Split each moodlet 
		for(int i = 0; i < crowdManager.CrowdSize; i++)
		{
			//cis - comedy is subjective
			Random cis = new Random();
			((CrowdEntity)crowdManager.crowd[i]).GeneratePersonality(cis.Next(0, moodletCount), cis.Next(0, moodletCount), cis.Next(0, moodletCount));
		}

    }

    private void OnOnSpawnersPopulated()
    {
        Logger.Info("populated spawners: " + crowdManager.spawners.Count);
        foreach (var entity in crowdManager.spawners)
        {
            //Logger.Info(((CrowdEntity)entity).TempValue);

            CrowdCollection.AddChild(entity);
        }
    }
    
	private void OnOnCrowdPopulated()
	{
		foreach (var entity in crowdManager.crowd)
		{
			//var data = crowdEntity.Instantiate() as CrowdEntity;
			//data.SetScript(entity);

			//CrowdEntity t = data as CrowdEntity;
			
			Logger.Info(((CrowdEntity)entity).TempValue);

			//CrowdCollection.AddChild(entity);
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}

	//Returns the first shuffle_size randomly sorted indexes from 1 to maxValue
	public List<int> ShuffleIndexes(int maxValue, int shuffle_size)
	{
		List<int> shuffled_ids = new List<int>();
		List<int> unshuffled = new List<int>();
		for (int i = 0; i < maxValue; i++)
			unshuffled.Add(i);

		Random rand = new Random();

		for(int i = 0; i < shuffle_size; i++)
		{
			int index = rand.Next(unshuffled.Count);
			shuffled_ids.Add(unshuffled[index]);
			unshuffled.RemoveAt(index);
		}

		return shuffled_ids;
	}
}
