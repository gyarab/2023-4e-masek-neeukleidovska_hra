using Godot;
using System;

public partial class portal : Node3D
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
		if (player.GlobalPosition.X /*+ 0.1*/ > camera.GlobalPosition.X) {
			Vector3 tmp = camera.GlobalPosition;
			tmp.Y -= 0.5f;
			Vector3 playerTemp = player.GlobalPosition;
			//tmp.X -= 0.01f;
			playerBody.GlobalPosition = tmp;
			camera.GlobalPosition = playerTemp;
			playerBody.Velocity = new Vector3(-playerBody.Velocity.X, playerBody.Velocity.Y, -playerBody.Velocity.Z);
			Vector3 rot = playerBody.GlobalRotationDegrees;
			rot.Y += 180;
			playerBody.GlobalRotationDegrees = rot;
		}
		GD.Print(playerBody.Position);
	}
}
