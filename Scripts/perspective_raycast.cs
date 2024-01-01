using Godot;
using System;

public partial class perspective_raycast : RayCast3D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		AddException(GetNode<RigidBody3D>("/root/Root/Cube"));
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		if (IsColliding()) {
			GD.Print(GlobalTransform.Origin.DistanceTo(GetCollisionPoint()));
			GetNode<RigidBody3D>("/root/Root/Cube").Position = GetCollisionPoint();
			Vector3 norm = (GetNode<CharacterBody3D>("/root/Root/CharacterBody3D").Position - GetNode<RigidBody3D>("/root/Root/Cube").Position).Normalized();
			GD.Print(norm);
			/*while (GetNode<RigidBody3D>("/root/Root/Cube").GetCollidingBodies().Count > 0) {
				//GetNode<RigidBody3D>("/root/Root/Cube").Position = GetNode<RigidBody3D>("/root/Root/Cube").Position + norm;
				
			}*/
		}	
	}
}
