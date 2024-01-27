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
	// How to distribute the moodlets we assign (e.g. 0.4,0.3,0.2,0.1 would do 40%, 30%, 20%, 10%). 
	[Export] public Array<float> MoodletWeighting;

    public Array<MoodletData> topicSet;
    public Array<MoodletData> setupSet;
    public Array<MoodletData> punchlineSet;

    #endregion

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        crowdManager.OnSpawnersPopulated += OnOnSpawnersPopulated;
        crowdManager.OnCrowdPopulated += OnOnCrowdPopulated;

        // Run moodlet builder to populate MoodletCount random moodlets for the set
        topicSet = MoodletBuilder.Instance.GenerateMoodletList(JokeType.Left, moodletCount);
        setupSet = MoodletBuilder.Instance.GenerateMoodletList(JokeType.Middle, moodletCount);
        punchlineSet = MoodletBuilder.Instance.GenerateMoodletList(JokeType.Right, moodletCount);

        crowdManager.GenerateSpawners();
        crowdManager.GenerateCrowd();

        Logger.Info("Moodlet loop");
        // Split each moodlet 
        for (int i = 0; i < crowdManager.CrowdSize; i++)
        {
            Logger.Info("Moodlet loop inside");
            //cis - comedy is subjective
            Random cis = new Random();
			Array<MoodletData> entity_moods = new Array<MoodletData>();
			entity_moods.Add(topicSet[cis.Next(0, moodletCount)]);
			entity_moods.Add(setupSet[cis.Next(0, moodletCount)]);
			entity_moods.Add(punchlineSet[cis.Next(0, moodletCount)]);


            ((CrowdEntity)crowdManager.crowd[i]).GeneratePersonality(entity_moods);
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

}
