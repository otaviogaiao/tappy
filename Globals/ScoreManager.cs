using Godot;
using System;

public partial class ScoreManager : Node
{
	public static ScoreManager Instance { get; private set; }

	private uint _score = 0;
	private uint _highScore = 0;

	private const string ScoreFile = "user://tappy.save";
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		ScoreManager.Instance = this;
		LoadScoreFromFile();
	}
	
	public uint GetScore()
	{
		return _score;
	}

	public uint GetHighScore()
	{
		return _highScore;
	}

	public void SetScore(uint score)
	{
		_score = score;
		if (_score > _highScore)
		{
			_highScore = _score;
			SaveScoreToFile();
		}
		SignalManager.Instance.EmitOnScored();
	}

	public void ResetScore()
	{
		SetScore(0);
	}

	public void IncrementScore()
	{
		SetScore(GetScore() + 1);
	}

	private void LoadScoreFromFile()
	{
		using FileAccess file = FileAccess.Open(ScoreFile, FileAccess.ModeFlags.Read);
		if (file != null)
		{
			_highScore = file.GetVar().AsUInt32();
		}
	}

	private void SaveScoreToFile()
	{
		using FileAccess file = FileAccess.Open(ScoreFile, FileAccess.ModeFlags.Write);
		if (file != null)
		{
			file?.StoreVar(_highScore);
		}
		else
		{
			GD.Print("Could not open Score file");
		}
	}
}
