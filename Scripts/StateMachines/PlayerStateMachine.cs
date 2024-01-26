using Godot;
using System;
using Utilities;

public partial class PlayerStateMachine : StateMachine
{
	private const string IdleState = "IDLE";
	private const string JokeState = "JOKING";
	
	public override void _Ready()
	{
		AddState(IdleState);
		AddState(JokeState);
		
		base._Ready();
	}

	public override void Reset()
	{
		throw new NotImplementedException();
	}

	public override void _Input(InputEvent @event)
	{
		if (!Initialised) return;
	}

	protected override void EnterState(string newState, string prevState)
	{
		if (!Initialised) return;
		
		switch (newState)
		{
			case IdleState:
				break;
			case JokeState:
				break;
		}
	}

	protected override void ExitState(string prevState, string newState)
	{
		switch (prevState)
		{
			case IdleState:
				break;
			case JokeState:
				break;
		}
	}

	protected override void StateLogic(float delta)
	{
		if (!Initialised) return;
		
		// Example:
		// if (State != DeadState)
		//	PlayerNode.HandleMovement();
	}

	protected override string GetTransition(float delta)
	{
		return IdleState;
	}
}
