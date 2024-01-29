using Godot;
using System;

public partial class view_bobbing : AnimationPlayer
{
	bool walk = false;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		if (walk && !IsPlaying()) {
			Play("bobbing");
			GetNode<AudioStreamPlayer>("WalkingSound").Play();
		} else if (!walk && IsPlaying()) {
			Stop();
			GetNode<AudioStreamPlayer>("WalkingSound").Stop();
		}
	}

	public void Walking(bool walk) {
		this.walk = walk;
	}
}
