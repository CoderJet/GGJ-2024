using Godot;
using System;
using System.Collections.Generic;
using System.IO;
using Godot.Collections;
using Utilities;
using FileAccess = Godot.FileAccess;
using System.Linq;
using System.Diagnostics;

public enum JokeType
{
    Left,
    Middle,
    Right
}

public partial class MoodletBuilder : Node
{
	public static MoodletBuilder Instance => instance ?? new MoodletBuilder();
	private static MoodletBuilder instance;
		
	private static Array<MoodletData> moodletCollection;

    public MoodletBuilder()
	{
        // res://Scenes/Resources/Moodlets/
        moodletCollection = new Array<MoodletData>();
        using var dir = DirAccess.Open("res://Scenes/Resources/Moodlets");

        if (dir == null) return;

		dir.ListDirBegin();
		var fileName = dir.GetNext();

		while (fileName != string.Empty)
		{
			var path = $"res://Scenes/Resources/Moodlets/{fileName}";

			if (FileAccess.FileExists(path))
			{
				var t = ResourceLoader.Load<MoodletData>(path);

				moodletCollection.Add(t);
			}
			fileName = dir.GetNext();
		}
	}
	public Array<MoodletData> GenerateMoodletList(JokeType type, int qty = 4)
	{
		Array<MoodletData> data = new Array<MoodletData>();

        Array<MoodletData> sub_list = new Array<MoodletData>(moodletCollection.Where(x => x.Type == type));
		Logger.Info("SUB LIST " + type + " SIZE: " + sub_list.Count);

        for (var i = 0; i < qty; i++)
		{
            MoodletData temp;
            if (data.Count == sub_list.Count) return data;
            
			do
            {
                temp = sub_list.PickRandom();
			} while (data.Contains(temp));
			data.Add(temp);
		}
		
		return data;
	}
}
