using Godot;
using System;
using System.Threading.Tasks;
using Utilities;

public partial class GameStateMachine : StateMachine
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	public override void Reset()
	{
		throw new NotImplementedException();
	}

	protected override Task EnterState(string newState, string prevState)
	{
		throw new NotImplementedException();
	}

	protected override Task ExitState(string prevState, string newState)
	{
		throw new NotImplementedException();
	}

	protected override void StateLogic(float delta)
	{
		throw new NotImplementedException();
	}

	protected override string GetTransition(float delta)
	{
		throw new NotImplementedException();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
