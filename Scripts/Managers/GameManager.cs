using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Audio;
using Utilities;

public partial class GameManager : Node
{
	[ExportCategory("Crowd Config")] 
	[Export] private Node2D crowdCollectionNode;
	[Export] private CrowdManager crowdManager;
	[Export] private MoodletPoolCollection moodletPoolCollection;
	[Export] private Control crowdMoodBar;
	[Export] private AudioStreamPlayer sfxAudioPlayer;
 
	[ExportCategory("Moodlet Configuration")] 
	// Applies to all pools
	[Export] private int moodletCount = 4;
	// How to distribute the moodlets we assign (e.g. 0.4,0.3,0.2,0.1 would do 40%, 30%, 20%, 10%). 
	[Export] private Array<float> moodletWeighting;

	private Array<MoodletData> topicSet;
	private Array<MoodletData> setupSet;
	private Array<MoodletData> punchlineSet;
 
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
	    crowdCollectionNode ??= GetParent().GetNode<Node2D>("CrowdCollection");
	    crowdManager ??= GetParent().GetNode<CrowdManager>("CrowdManager");
	    moodletPoolCollection ??= GetParent().GetNode<MoodletPoolCollection>("UILayer/MoodletPools");
	    crowdMoodBar ??= GetParent().GetNode<Control>("UILayer/CrowdMoodBar");
	    sfxAudioPlayer ??= GetParent().GetNode<AudioStreamPlayer>("SfxStreamPlayer");
 
	    if (moodletWeighting == null || moodletWeighting?.Count == 0)
		    moodletWeighting = new Array<float> { 0.4f, 0.3f, 0.2f, 0.1f };
	    
		moodletPoolCollection.Generate(moodletCount);
 
	    moodletPoolCollection.SetupMoodletSet += moodlets => 
	    {
		    var result = crowdManager.Joke(moodlets);
		    ((CrowdMoodBar)crowdMoodBar).UpdateMoodBar(result);
	    
		    if (result == 0)
		    {
			    sfxAudioPlayer.Stream = ResourceLoader.Load<AudioStreamOggVorbis>("res://Assets/Audio/SFX/CricketTrack.tres");
			    sfxAudioPlayer.Play();
		    }
		    else if (result > 0)
		    {
			    sfxAudioPlayer.Stream = ResourceLoader.Load<AudioStreamWav>("res://Assets/Audio/SFX/LaughTrack.tres");
			    sfxAudioPlayer.Play();
		    }
		    else
		    {
			    sfxAudioPlayer.Stream = ResourceLoader.Load<AudioStreamMP3>("res://Assets/Audio/SFX/BooTrack.tres");
			    sfxAudioPlayer.Play();
		    }
	    };
	    
        crowdManager.OnSpawnersPopulated += OnOnSpawnersPopulated;
        crowdManager.OnCrowdPopulated += OnOnCrowdPopulated;
 
        crowdManager.GenerateSpawners();
        crowdManager.GenerateCrowd();
 
        Logger.Info("Moodlet loop");
        // Split each moodlet 
 
        //make a batched index list we can shuffle later
        List<int> topicList = new List<int>();
        for (int mw = 0; mw < moodletWeighting.Count; mw++)
		{
			int crowdChunk = (int)(crowdManager.CrowdSize * moodletWeighting[mw]);
            for (int w = 0; w < crowdChunk;w++)
			{
				topicList.Add(mw);
			}
		}
		while(topicList.Count < crowdManager.CrowdSize)
		{
			topicList.Add(0);
		}
 
		// clone topic indexes into setups and punchlines
		List<int> setupList = new List<int>(topicList);
        List<int> punchlineList = new List<int>(topicList);
 
        for (int i = 0; i < crowdManager.CrowdSize; i++)
        {
			//cis - comedy is subjective
            Random cis = new Random();			
			Array<MoodletData> entity_moods = new Array<MoodletData>();
 
			// Pull back the set of moodlets we generated in the pool
			int topic_index = cis.Next(0, topicList.Count);
			entity_moods.Add(moodletPoolCollection.TopicList[topicList[topic_index]]);
			topicList.RemoveAt(topic_index);
 
            int setup_index = cis.Next(0, setupList.Count);
            entity_moods.Add(moodletPoolCollection.SetupList[setupList[setup_index]]);
            setupList.RemoveAt(setup_index);
 
            int punchline_index = cis.Next(0, punchlineList.Count);
            entity_moods.Add(moodletPoolCollection.PunchlineList[punchlineList[punchline_index]]);
            punchlineList.RemoveAt(punchline_index);
 
			Logger.Info("Crowd personality: "+
				  "T" + entity_moods[0].ResourceName + 
				", S" + entity_moods[1].ResourceName + 
				", P" + entity_moods[2].ResourceName + ".");
 
            ((CrowdEntity)crowdManager.crowd[i]).SetPersonality(entity_moods);
		}
    }
 
    private void OnOnSpawnersPopulated()
    {
        foreach (var entity in crowdManager.spawners)
        {
            //Logger.Info(((CrowdEntity)entity).TempValue);
 
            crowdCollectionNode.AddChild(entity);
        }
    }
    
	private void OnOnCrowdPopulated()
	{
		// foreach (var entity in crowdManager.crowd)
		// {
		// 	var data = crowdEntity.Instantiate() as CrowdEntity;
		// 	data.SetScript(entity);
 	//
		// 	CrowdEntity t = data as CrowdEntity;
		// 				
		// 	CrowdCollection.AddChild(entity);
		// }
	}
}
