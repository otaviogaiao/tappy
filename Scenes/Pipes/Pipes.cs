using Godot;
using System;

public partial class Pipes : Node2D
{
	[Export] private float _speed = 120.0f;
	[Export] private VisibleOnScreenNotifier2D _visibleOnScreenNotifier;
	[Export] private Area2D _upperPipe;
	[Export] private Area2D _lowerPipe;
	[Export] private Area2D _laser;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_visibleOnScreenNotifier.ScreenExited += OnScreenNotifierExited;
		_lowerPipe.BodyEntered += OnPipeBodyEntered;
		_upperPipe.BodyEntered += OnPipeBodyEntered;
		_laser.BodyEntered += OnLaserBodyEntered;
		SignalManager.Instance.PlaneCrashed += OnPlaneCrashed;
	}

	public override void _ExitTree()
	{
		SignalManager.Instance.PlaneCrashed -= OnPlaneCrashed;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Position -= new Vector2(_speed * (float)delta, 0);
	}

	private void OnScreenNotifierExited()
	{
		GD.Print("Pipes exited");
		QueueFree();
	}

	private void OnPipeBodyEntered(Node2D body)
	{
		if (body is Plane plane)
		{
			plane.Die();
		}
	}

	private void OnLaserBodyEntered(Node2D body)
	{
		ScoreManager.Instance.IncrementScore();
	}

	private void OnPlaneCrashed()
	{
		SetProcess(false);
	}
}
