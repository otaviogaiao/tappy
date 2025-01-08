using Godot;
using System;

public partial class Plane : CharacterBody2D
{
	[Export] private AnimationPlayer _animationPlayer;
	[Export] private AnimatedSprite2D _sprite;
	[Export] private AudioStreamPlayer _engineSound;
	
	private const float Gravity = 800.0f;
	private const float Power = -250.0f;
	
	public override void _Ready()
	{
	}
	
	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;
		velocity.Y += Gravity * (float)delta;
		Velocity = velocity;
		if (Input.IsActionJustPressed("fly"))
		{
			velocity.Y = Power;
			_animationPlayer.Play("power");
		}

		Velocity = velocity;
		MoveAndSlide();

		if (IsOnFloor())
		{
			Die();
		}
	}

	public void Die()
	{
		SetPhysicsProcess(false);
		_engineSound.Stop();
		_sprite.Stop();
		SignalManager.Instance.EmitOnPlaneDied();
	}
}
