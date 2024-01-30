using Godot;
using System;
using System.Threading.Tasks;
using Utilities;

public partial class PlayerStateMachine : StateMachine
{
	[Export] public Control poolCollection;
	[Export] public Button microphone;
	
	private AnimationPlayer player;
	
	private const string IdleState = "IDLE";
	private const string ThemeSetupState = "SETUP_THEME";
	private const string SetupState = "SETUP";
	private const string ThemePunchlineState = "PUNCHLINE_THEME";
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
	}
	
	protected override async Task EnterState(string newState, string prevState)
	{
		if (!Initialised) return;
		
		switch (newState)
		{
			case IdleState:
				player.Play("PutBackSetupTopic");
				await ToSignal(player, "animation_finished");
				player.Play("PutBackSetup");
				await ToSignal(player, "animation_finished");
				player.Play("PutBackPunchlineTopic");
				await ToSignal(player, "animation_finished");
				player.Play("PutBackPunchline");
				((MoodletPoolCollection)poolCollection).Reset();
				break;
			case ThemeSetupState:
				player.Play("ShowSetupTopic");
				break;
			case SetupState:
				player.Play("ShowSetup");
				break;
			case ThemePunchlineState:
				player.Play("ShowPunchlineTopic");
				break;
			case PunchlineState:
				player.Play("ShowPunchline");
				break;
		}
		
		if (player.IsPlaying())
			await ToSignal(player, "animation_finished");
	}
	
	protected override async Task ExitState(string prevState, string newState)
	{
		switch (prevState)
		{
			case IdleState:
				break;
			case ThemeSetupState:
				player.Play("PutAwaySetupTopic");
				break;
			case SetupState:
				player.Play("PutAwaySetup");
				break;
			case ThemePunchlineState:
				player.Play("PutAwayPunchlineTopic");
				break;
			case PunchlineState:
				player.Play("PutAwayPunchline");
				break;
		}
		
		if (player.IsPlaying())
			await ToSignal(player, "animation_finished");
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
		if (microphone.ButtonPressed)
		{
			microphone.ButtonPressed = false;
			return ThemeSetupState;
		}

		if (poolCollection is not MoodletPoolCollection moodletPool) 
			return State;
		
		if (moodletPool.PunchlineMoodlet != null)
			return IdleState;
		if (moodletPool.PunchlineTopicsMoodlet != null)
			return PunchlineState;
		if (moodletPool.SetupMoodlet != null)
			return ThemePunchlineState;
		
		return moodletPool.SetupTopicMoodlet != null ? SetupState : State;
	}
}
