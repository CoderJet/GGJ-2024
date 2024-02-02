using System.Linq;
using Godot;
using Godot.Collections;
using FileAccess = Godot.FileAccess;
using Utilities;

public partial class MoodletBuilder : Node
{
	#region Singleton Magic
	public static MoodletBuilder Instance => instance ?? new MoodletBuilder();
	private static MoodletBuilder instance;
	#endregion
		
	private static Array<MoodletData> moodletCollection;

	private MoodletBuilder()
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
				path = path.TrimSuffix(".remap");
				Logger.Info(path);
				var t = ResourceLoader.Load<MoodletData>(path);
 
				moodletCollection.Add(t);
			}
			fileName = dir.GetNext();
		}
	}
	public static Array<MoodletData> GenerateMoodletList(JokeType type, int qty = 4)
	{
		var data = new Array<MoodletData>();
		var sub_list = new Array<MoodletData>(moodletCollection.Where(x => x.Type == type));
	
        for (var i = 0; i < qty; i++)
		{
            if (data.Count == sub_list.Count) return data;
            
            MoodletData temp;
			do
            {
                temp = sub_list.PickRandom();
			} while (data.Contains(temp));
			data.Add(temp);
		}
		return data;
	}
}
