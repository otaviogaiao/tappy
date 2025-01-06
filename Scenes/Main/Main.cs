using Godot;
using System;

public partial class Main : Control
{
	[Export] private Label _highScoreLabel;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_highScoreLabel.Text = ScoreManager.Instance.GetHighScore().ToString().PadLeft(4, '0');
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("play"))
		{
			GameManager.Instance.LoadGame();
		}
	}
}
