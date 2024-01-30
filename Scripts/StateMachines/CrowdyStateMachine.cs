using Godot;
using System;
using System.Threading.Tasks;
using Utilities;

public partial class CrowdyStateMachine : StateMachine
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}
	
	public override void Reset()
	{
		throw new NotImplementedException();
	}
	
	protected override async Task EnterState(string newState, string prevState)
	{
		//throw new NotImplementedException();
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
