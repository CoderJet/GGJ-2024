using Godot;
using Godot.Collections;

namespace Audio;

public partial class AudioControlData : Resource
{
    [Export] public Dictionary Data = new();
}