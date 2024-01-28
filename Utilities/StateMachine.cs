// Godot includes.

using System.Threading.Tasks;
using Godot;
using Godot.Collections;

namespace Utilities;

[GlobalClass]
public abstract partial class StateMachine : Node
{
    protected Node Parent;
    protected string State;
    protected bool Initialised;
    private bool StateChanged => State != previousState;
    
    private readonly Array<string> states = new();
    private string previousState;

    public override void _Ready()
    {
        Initialise();
    }

    public override void _PhysicsProcess(double delta)
    {
        StateLogic((float)delta);

        var transition = GetTransition((float)delta);
        SetState(transition);
    }

    public override string ToString()
    {
        return State;
    }

    protected void Initialise()
    {
        Parent = GetParent();
        Logger.Info($"Parent node: {Parent.Name}");
        Initialised = true;
    }

    protected async void SetState(string newState)
    {
        previousState = State;
        State = newState;

        if (!StateChanged) return;

        if (!string.IsNullOrEmpty(previousState))
            await ExitState(previousState, newState);
        if (!string.IsNullOrEmpty(newState))
            await EnterState(newState, previousState);
    }

    protected void AddState(string newState)
    {
        if (states.Contains(newState)) return;
        states.Add(newState);
    }

    public abstract void Reset();

    protected abstract Task EnterState(string newState, string prevState);

    protected abstract Task ExitState(string prevState, string newState);

    protected abstract void StateLogic(float delta);

    protected abstract string GetTransition(float delta);
}
