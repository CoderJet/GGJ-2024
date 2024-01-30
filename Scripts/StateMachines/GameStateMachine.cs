using Godot;
using System;
using System.Threading.Tasks;
using Utilities;

public partial class GameStateMachine : StateMachine
{
	private static string IdleState = "IDLE";
	
	[Export] public MoodletPoolCollection moodletPools;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		moodletPools = GetParent().GetParent().GetNode<MoodletPoolCollection>("UILayer/JokePools");
	}
	
	public override void Reset()
	{
	}
	
	protected override async Task EnterState(string newState, string prevState)
	{
		if (!Initialised) return;
		
	}
	
	protected override async Task ExitState(string prevState, string newState)
	{
	}
	
	protected override void StateLogic(float delta)
	{
	}
	
	protected override string GetTransition(float delta)
	{
		return State;
	}
}
