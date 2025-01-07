using Godot;
using System;

public partial class GameOver : Control
{
	[Export] private Label _gameOverLabel;
	[Export] private Label _pressSpaceLabel;
	[Export] private Timer _timer;
	[Export] private AudioStreamPlayer _gameOverSound;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		SignalManager.Instance.PlaneCrashed += OnPlaneCrashed;
		_timer.Timeout += ChangeLabel;
	}

	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("play") && _pressSpaceLabel.Visible)
		{
			GameManager.Instance.LoadMain();
		}
	}

	public override void _ExitTree()
	{
		SignalManager.Instance.PlaneCrashed -= OnPlaneCrashed;
	}
	
	private void OnPlaneCrashed()
	{
		_gameOverLabel.Visible = true;
		_timer.Start();
		_gameOverSound.Play();
	}

	private void ChangeLabel()
	{
		_gameOverLabel.Visible = false;
		_pressSpaceLabel.Visible = true;
	}
}
