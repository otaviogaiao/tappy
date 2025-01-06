using Godot;
using System;

public partial class SignalManager : Node
{
	public static SignalManager Instance { get; private set; }
	
	[Signal] public delegate void PlaneCrashedEventHandler();
	
	[Signal] public delegate void ScoredEventHandler();
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Instance = this;
	}

	public void EmitOnPlaneDied()
	{
		EmitSignal(SignalName.PlaneCrashed);
	}

	public void EmitOnScored()
	{
		EmitSignal(SignalName.Scored);
	}
}
