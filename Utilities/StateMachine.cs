// Godot includes.
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

    protected void SetState(string newState)
    {
        previousState = State;
        State = newState;

        if (!StateChanged) return;

        if (!string.IsNullOrEmpty(previousState))
            ExitState(previousState, newState);
        if (!string.IsNullOrEmpty(newState))
            EnterState(newState, previousState);
    }

    protected void AddState(string newState)
    {
        if (states.Contains(newState)) return;
        states.Add(newState);
    }

    public abstract void Reset();

    protected abstract void EnterState(string newState, string prevState);

    protected abstract void ExitState(string prevState, string newState);

    protected abstract void StateLogic(float delta);

    protected abstract string GetTransition(float delta);
}
