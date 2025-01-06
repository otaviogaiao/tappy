using Godot;
using System;

public partial class Game : Node2D
{
	[Export] private Marker2D _spawnUpper;
	[Export] private Marker2D _spawnLower;
	[Export] private Timer _spawnTimer;
	[Export] private Node2D _pipesHolder;
	[Export] private PackedScene _pipeScene;
	
	private bool _isGameOver = false;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_spawnTimer.Timeout += SpawnPipe;
		SignalManager.Instance.PlaneCrashed += OnGameOver;
		
		ScoreManager.Instance.ResetScore();
		SpawnPipe();
	}
	
	public override void _ExitTree()
	{
		SignalManager.Instance.PlaneCrashed -= OnGameOver;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("quit") || (Input.IsActionJustPressed("fly") && _isGameOver))
		{
			GameManager.Instance.LoadMain();
		}
	}

	private void SpawnPipe()
	{
		Pipes node = _pipeScene.Instantiate<Pipes>();
		node.GlobalPosition = new Vector2(GetSpawnX(), GetSpawnY());
		_pipesHolder.AddChild(node);
	}

	private float GetSpawnX()
	{
		return _spawnLower.GlobalPosition.X;
	}

	private float GetSpawnY()
	{
		return (float)GD.RandRange(_spawnUpper.GlobalPosition.Y, _spawnLower.GlobalPosition.Y);
	}

	private void StopPipes()
	{
		_spawnTimer.Stop();
	}

	private void OnGameOver()
	{
		StopPipes();
		_isGameOver = true;
	}
}
