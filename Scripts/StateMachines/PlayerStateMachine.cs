using Godot;
using System;
using System.Threading.Tasks;
using Utilities;

public partial class PlayerStateMachine : StateMachine
{
	[Export] public Control moodletPools;
	[Export] public Button microphone;
	
	private AnimationPlayer animPlayer;
	
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

		moodletPools = GetParent().GetNode<Control>("UILayer/MoodletPools");
		microphone = GetParent().GetNode<Button>("Stage/Mic/Button");
		
		animPlayer = moodletPools.GetNode<AnimationPlayer>("AnimationPlayer");
		
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
				animPlayer.Play("PutBackSetupTopic");
				await ToSignal(animPlayer, "animation_finished");
				animPlayer.Play("PutBackSetup");
				await ToSignal(animPlayer, "animation_finished");
				animPlayer.Play("PutBackPunchlineTopic");
				await ToSignal(animPlayer, "animation_finished");
				animPlayer.Play("PutBackPunchline");
				((MoodletPoolCollection)moodletPools).Reset();
				break;
			case ThemeSetupState:
				animPlayer.Play("ShowSetupTopic");
				break;
			case SetupState:
				animPlayer.Play("ShowSetup");
				break;
			case ThemePunchlineState:
				animPlayer.Play("ShowPunchlineTopic");
				break;
			case PunchlineState:
				animPlayer.Play("ShowPunchline");
				break;
		}
		
		if (animPlayer.IsPlaying())
			await ToSignal(animPlayer, "animation_finished");
	}
	
	protected override async Task ExitState(string prevState, string newState)
	{
		switch (prevState)
		{
			case IdleState:
				break;
			case ThemeSetupState:
				animPlayer.Play("PutAwaySetupTopic");
				break;
			case SetupState:
				animPlayer.Play("PutAwaySetup");
				break;
			case ThemePunchlineState:
				animPlayer.Play("PutAwayPunchlineTopic");
				break;
			case PunchlineState:
				animPlayer.Play("PutAwayPunchline");
				break;
		}
		
		if (animPlayer.IsPlaying())
			await ToSignal(animPlayer, "animation_finished");
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

		if (moodletPools is not MoodletPoolCollection moodletPool) 
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
