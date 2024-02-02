using System.Threading.Tasks;
using Godot;
using Utilities;

public partial class CrowdStateMachine : StateMachine
{
	[Export] private CrowdEntity entity;
	
	private const string IdleState = "IDLE";
	private const string LaughState = "LAUGH";
	private const string AngryState = "ANGRY";
	
	public override void _Ready()
	{
		AddState(IdleState);
		AddState(LaughState);
		AddState(AngryState);
		
		SetState(IdleState);

		entity = GetParent().GetNode<CrowdEntity>(".");
		
		base._Ready();
	}
	
	public override void Reset()
	{
		if (!Initialised) return;
	}
	
	protected override async Task EnterState(string newState, string prevState)
	{
		if (!Initialised) return;

		switch (newState)
		{
			case LaughState:
				entity.PlayLaughingAnimation();
				break;
			case AngryState:
				entity.PlayAngryAnimation();
				break;
			case IdleState:
			default:
				entity.PlayIdleAnimation();
				break;
		}
	}
	
	protected override async Task ExitState(string prevState, string newState)
	{
		if (!Initialised) return;
	}
	
	protected override void StateLogic(float delta)
	{
		if (!Initialised) return;
	}
	
	protected override string GetTransition(float delta)
	{
		switch (entity.BonesTickled)
		{
			case 2:
				// Haha, great story Mark!
				return LaughState;
			case 0:
				// I hate this joke!
				return AngryState;
			case 1:
			default:
				// No reaction
				return IdleState;
		}
		//return State;
	}
}
