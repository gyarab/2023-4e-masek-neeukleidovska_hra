using Godot;
using System;

public partial class chase : Node3D
{
	CharacterBody3D player;
	sequencer sequencer;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		player = GetNode<CharacterBody3D>("/root/Root/CharacterBody3D");
		sequencer = GetNode<sequencer>("/root/Sequencer");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		if (sequencer.seqNum >= 11 && sequencer.seqNum <= 14) {
			GlobalPosition = new Vector3(GlobalPosition.X + (player.GlobalPosition.X - GlobalPosition.X) / 90, GlobalPosition.Y, GlobalPosition.Z + (player.GlobalPosition.Z - GlobalPosition.Z) / 90);
		}
	}
}
