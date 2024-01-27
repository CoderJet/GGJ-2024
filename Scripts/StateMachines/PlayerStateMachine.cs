using Godot;
using System;
using Utilities;

public partial class PlayerStateMachine : StateMachine
{
	[Export] public GameManager gameManager;
	[Export] public Control poolCollection;

	private AnimationPlayer player;
	
	private const string IdleState = "IDLE";
	private const string ThemeSetupState = "SETUP_THEME";
	private const string SetupState = "SETUP";
	private const string ThemePunchlineState = "PUNHLINE_THEME";
	private const string PunchlineState = "PUNCHLINE";
	
	public override void _Ready()
	{
		AddState(IdleState);
		AddState(ThemeSetupState);
		AddState(SetupState);
		AddState(ThemePunchlineState);
		AddState(PunchlineState);

		SetState(IdleState);

		player = poolCollection.GetNode<AnimationPlayer>("AnimationPlayer");
		
		base._Ready();
	}

	public override void Reset()
	{
		
	}

	public override void _Input(InputEvent @event)
	{
		if (!Initialised) return;
		
		if (Input.IsKeyPressed(Key.Space))
			SetState(ThemeSetupState);
	}

	protected override async void EnterState(string newState, string prevState)
	{
		if (!Initialised) return;
		
		switch (newState)
		{
			case IdleState:
				break;
			case ThemeSetupState:
				player.Play("TopicSetup");
				await ToSignal(player, "animation_finished");
				break;
			case SetupState:
				player.Play("Setup");
				break;
		}
	}

	protected override async void ExitState(string prevState, string newState)
	{
		switch (prevState)
		{
			case IdleState:
				break;
			case ThemeSetupState:
				player.PlayBackwards("TopicSetup");
				await ToSignal(player, "animation_finished");
				break;
		}
	}

	protected override void StateLogic(float delta)
	{
		if (!Initialised) return;


		if (((MoodletPoolCollection)poolCollection).SetupTopicMoodlet != null)
		{
			SetState(SetupState);
		}
		// Example:
		// if (State != DeadState)
		//	PlayerNode.HandleMovement();
	}

	protected override string GetTransition(float delta)
	{
		return State;
	}
}
