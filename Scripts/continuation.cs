using Godot;
using System;

public partial class continuation : Node3D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GetNode<sequencer>("/root/Sequencer").nextSeq = true;
		GetNode<CharacterBody3D>("/root/Root/CharacterBody3D").GlobalRotationDegrees = Vector3.Zero;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
