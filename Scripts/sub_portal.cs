using Godot;
using System;

public partial class sub_portal : Node3D
{
	float z;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		z = GetNode<Node3D>("SubViewport/Camera3D").GlobalPosition.Z - GlobalPosition.Z; 
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

    public override void _PhysicsProcess(double delta)
    {
        var player = GetNode<Node3D>("/root/Root/CharacterBody3D/CameraRig");
		var playerBody = GetNode<CharacterBody3D>("/root/Root/CharacterBody3D");
		var camera = GetNode<Node3D>("SubViewport/Camera3D");
		camera.GlobalRotationDegrees = new Vector3(player.GlobalRotationDegrees.X, player.GlobalRotationDegrees.Y + 180, 0);
		camera.GlobalPosition = new Vector3(GlobalPosition.X - 0.1f - (player.GlobalPosition.X - GlobalPosition.X + 0.1f), player.GlobalPosition.Y, GlobalPosition.Z - (player.GlobalPosition.Z - GlobalPosition.Z) + z);
	}
}
