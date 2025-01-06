using Godot;
using System;

public partial class Hud : Control
{
	[Export] private Label _scoreLabel;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		SignalManager.Instance.Scored += OnScored;
	}

	public override void _ExitTree()
	{
		SignalManager.Instance.Scored -= OnScored;
	}
	
	private void OnScored()
	{
		_scoreLabel.Text = ScoreManager.Instance.GetScore().ToString().PadLeft(4, '0');
	}
}
