using Godot;

namespace GGJ24.Scripts.Resources;

public enum JokeColour
{
    Red = 0,
    Blue,
    Green
}

public static class JokeColours
{
    public static Color ToColour(JokeColour color)
    {
        return color switch
        {
            JokeColour.Red => Colors.Red,
            JokeColour.Blue => Colors.Blue,
            JokeColour.Green => Colors.Green,
            _ => Colors.White
        };
    }
}