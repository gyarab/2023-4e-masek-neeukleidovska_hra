using Godot;
using System;
using System.Drawing;

public partial class portal : CsgBox3D
{
	float z;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		z = GetNode<Node3D>("SubViewport/CameraRig").GlobalPosition.Z - GlobalPosition.Z; 
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

    public override void _PhysicsProcess(double delta)
    {
        var player = GetNode<Node3D>("/root/Root/CharacterBody3D/CameraRig");
		var playerBody = GetNode<CharacterBody3D>("/root/Root/CharacterBody3D");
		var camera = GetNode<Node3D>("SubViewport/CameraRig/Camera3D");
		var cameraRig = GetNode<Node3D>("SubViewport/CameraRig");
		var subViewport = GetNode<SubViewport>("SubViewport");
		subViewport.Size = new Vector2I(GetViewport().GetWindow().Size.X / 2, GetViewport().GetWindow().Size.Y / 2);
		camera.GlobalRotationDegrees = new Vector3(player.GlobalRotationDegrees.X, player.GlobalRotationDegrees.Y + 180, 0);
		cameraRig.GlobalPosition = new Vector3(GlobalPosition.X - 0.1f - (player.GlobalPosition.X - GlobalPosition.X + 0.1f), 
		player.GlobalPosition.Y, GlobalPosition.Z - (player.GlobalPosition.Z - GlobalPosition.Z) + z);
		if (player.GlobalPosition.X /*+ 0.1*/ > cameraRig.GlobalPosition.X && player.GlobalPosition.Z < GlobalPosition.Z + 
		Size.Z / 2 && player.GlobalPosition.Z > GlobalPosition.Z - Size.Z / 2) {
			Vector3 tmp = cameraRig.GlobalPosition;
			tmp.Y -= 0.5f;
			Vector3 playerTemp = player.GlobalPosition;
			//tmp.X -= 0.01f;
			playerBody.GlobalPosition = tmp;
			cameraRig.GlobalPosition = playerTemp;
			playerBody.Velocity = new Vector3(-playerBody.Velocity.X, playerBody.Velocity.Y, -playerBody.Velocity.Z);
			Vector3 rot = playerBody.GlobalRotationDegrees;
			rot.Y += 180;
			playerBody.GlobalRotationDegrees = rot;
		}
	}

	public RayCast3D Recalculate(RayCast3D before) {
		RayCast3D after = GetNode<RayCast3D>("RayCast");
		Vector3 pos = before.GetCollisionPoint();
		pos.Z = GlobalPosition.Z + z - (pos.Z - GlobalPosition.Z);
		after.GlobalPosition = pos;
		Vector3 rot = before.GlobalRotationDegrees;
		rot.Y += 180;
		after.GlobalRotationDegrees = rot;
		return after;
	}
}