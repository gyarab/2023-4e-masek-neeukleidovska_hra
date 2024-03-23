using Godot;
using System;

public partial class portal_wall : CsgBox3D
{
	Node3D player;
	Camera3D camera;
	SubViewport subViewport;
	Node3D pointer;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		player = GetNode<Node3D>("/root/Root/CharacterBody3D/CameraRig");
		//pointer = GetNode<Node3D>("/root/Root/Pointer");
		camera = GetNode<Camera3D>("SubViewport/Camera3D");
		subViewport = GetNode<SubViewport>("SubViewport");
		subViewport.Size = new Vector2I(GetViewport().GetWindow().Size.X / 2, GetViewport().GetWindow().Size.Y / 2);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		camera.GlobalRotation = player.GlobalRotation;
		float Z = ((player.GlobalPosition.Z + 1.26f) % 9.07f) - 1.26f;
		if (Z < -1.26f) camera.GlobalPosition = new Vector3(player.GlobalPosition.X, player.GlobalPosition.Y, 8.8f + (((player.GlobalPosition.Z + 1.26f) % 9.07f) - 1.26f));
		else camera.GlobalPosition = new Vector3(player.GlobalPosition.X, player.GlobalPosition.Y, /*((player.GlobalPosition.Z + 1.26f) % 9.07f) - 1.26f*/ player.GlobalPosition.Z + 8.8f);
		//pointer.GlobalPosition = camera.GlobalPosition;
		//GD.Print(camera.GlobalPosition.Z);
	}
}
