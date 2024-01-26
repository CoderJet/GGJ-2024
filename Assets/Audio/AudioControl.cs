using Godot;
using System;
using System.IO;
using FileAccess = Godot.FileAccess;

namespace Audio;

public static class AudioControl
{
	enum AudioBus
	{
		Master,
		Music,
		Effects
	}

	public static double GetMasterBusVolumeDb() => GetVolumeDb(AudioBus.Master);
	public static void SetMasterBusVolumeDb(double value) => SetVolumeDb(AudioBus.Master, value);
	public static double GetMusicBusVolumeDb() => GetVolumeDb(AudioBus.Music);
	public static void SetMusicBusVolumeDb(double value) => SetVolumeDb(AudioBus.Music, value);
	public static double GetSfxBusVolumeDb() => GetVolumeDb(AudioBus.Effects);
	public static void SetSfxBusVolumeDb(double value) => SetVolumeDb(AudioBus.Effects, value);

	public static double GetMasterBusVolumeLinear() => GetVolumeLinear(AudioBus.Master);
	public static void SetMasterBusVolumeLinear(double value) => SetVolumeDb(AudioBus.Master, Mathf.LinearToDb(value));
	public static double GetMusicBusVolumeLinear() => GetVolumeLinear(AudioBus.Music);
	public static void SetMusicBusVolumeLinear(double value) => SetVolumeDb(AudioBus.Music, Mathf.LinearToDb(value));
	public static double GetEffectsBusVolumeLinear() => GetVolumeLinear(AudioBus.Effects);
	public static void SetEffectsBusVolumeLinear(double value) => SetVolumeDb(AudioBus.Effects, Mathf.LinearToDb(value));
	
	// %APPDATA%/Godot/app_userdata/Temp
	private static readonly string savePathAudioControl = "user://audio_control.tres"; 
	private static AudioControlData audioData = new();
	private static bool audioDataChanged;

	static AudioControl()
	{
		foreach (AudioBus key in Enum.GetValues(typeof(AudioBus)))
			audioData.Data.Add(Enum.GetName(typeof(AudioBus), key), 0);
		Load();
		LoadBusVolumes();
	}
	
	private static double GetVolumeDb(AudioBus bus)
	{
		return AudioServer.GetBusVolumeDb(GetBusIndex(bus));
	}

	private static double GetVolumeLinear(AudioBus bus)
	{
		return Mathf.DbToLinear(GetVolumeDb(bus));
	}

	private static void SetVolumeDb(AudioBus bus, double value)
	{
		audioData.Data[Enum.GetName(typeof(AudioBus), bus)] = value;
		audioDataChanged = true;
		AudioServer.SetBusVolumeDb(GetBusIndex(bus), (float)value);
	}
	
	private static int GetBusIndex(AudioBus bus)
	{
		var value = AudioServer.GetBusIndex(Enum.GetName(typeof(AudioBus), bus));
		return value;
	}

	private static void LoadBusVolumes()
	{
		foreach (AudioBus key in Enum.GetValues(typeof(AudioBus)))
		{
			var value = Enum.GetName(typeof(AudioBus), key);
			
			if (audioData.Data.ContainsKey(value))
			{
				var busIdx = AudioServer.GetBusIndex(
					Enum.GetName(typeof(AudioBus), key));
				var dbValue = audioData.Data[value].AsSingle();
				
				
				AudioServer.SetBusVolumeDb(busIdx, dbValue);
			}
			else
			{
				AudioServer.SetBusVolumeDb(AudioServer.GetBusIndex(
						Enum.GetName(typeof(AudioBus), key)),0);
			}
		}
	}

	public static void Save()
	{
		var state = ResourceSaver.Save(audioData, savePathAudioControl);

		if (state == Error.Ok)
			audioDataChanged = false;
		else
			GD.PushError("Can't save audio data");
	}

	public static void Load()
	{
		if (!FileAccess.FileExists("user://audio_control.tres")) return;
		
		var data = GD.Load<AudioControlData>(savePathAudioControl);

		if (data == null) return;
			
		audioData = data;
		GD.Print(audioData);
	}
}
