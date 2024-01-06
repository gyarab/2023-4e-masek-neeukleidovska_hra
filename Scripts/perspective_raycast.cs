using Godot;
using System;

public partial class perspective_raycast : RayCast3D
{
	bool run = true;
	Vector3 norm;
	float mult;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		AddException(GetNode<RigidBody3D>("/root/Root/Cube"));
		GetNode<RayCast3D>("/root/Root/CharacterBody3D/CameraRig/RayCastCenter/RayCastTopLeft").AddException(GetNode<RigidBody3D>("/root/Root/Cube"));
		GetNode<RayCast3D>("/root/Root/CharacterBody3D/CameraRig/RayCastCenter/RayCastTopRight").AddException(GetNode<RigidBody3D>("/root/Root/Cube"));
		GetNode<RayCast3D>("/root/Root/CharacterBody3D/CameraRig/RayCastCenter/RayCastBotLeft").AddException(GetNode<RigidBody3D>("/root/Root/Cube"));
		GetNode<RayCast3D>("/root/Root/CharacterBody3D/CameraRig/RayCastCenter/RayCastBotRight").AddException(GetNode<RigidBody3D>("/root/Root/Cube"));
		GetNode<RigidBody3D>("/root/Root/Cube").Freeze = true;
		mult = 1 / GetNode<RigidBody3D>("/root/Root/Cube").GlobalPosition.DistanceTo(GlobalPosition);
	}

	public override void _PhysicsProcess(double delta)
	{
		if (IsColliding() && run) {
			var spaceState = GetWorld3D().DirectSpaceState;
			var topLeft = GetNode<RayCast3D>("/root/Root/CharacterBody3D/CameraRig/RayCastCenter/RayCastTopLeft");
			var topRight = GetNode<RayCast3D>("/root/Root/CharacterBody3D/CameraRig/RayCastCenter/RayCastTopRight");
			var botLeft = GetNode<RayCast3D>("/root/Root/CharacterBody3D/CameraRig/RayCastCenter/RayCastBotLeft");
			var botRight = GetNode<RayCast3D>("/root/Root/CharacterBody3D/CameraRig/RayCastCenter/RayCastBotRight");

			var shape = GetNode<CollisionShape3D>("/root/Root/Cube/CollisionShape3D");
			var box = GetNode<CsgBox3D>("/root/Root/Cube/CSGBox3D");
			
			float dist = float.MaxValue;

			if (topLeft.IsColliding()) dist = topLeft.GetCollisionPoint().DistanceTo(GlobalPosition);
			if (topRight.IsColliding()) {
				float tmp = topRight.GetCollisionPoint().DistanceTo(GlobalPosition);
				if (tmp < dist) dist = tmp;
			}
			if (botLeft.IsColliding()) {
				float tmp = botLeft.GetCollisionPoint().DistanceTo(GlobalPosition);
				if (tmp < dist) dist = tmp;
			}
			if (botRight.IsColliding()) {
				float tmp = botRight.GetCollisionPoint().DistanceTo(GlobalPosition);
				if (tmp < dist) dist = tmp;
			}
			if (dist < GetCollisionPoint().DistanceTo(GlobalPosition)) {
				float diff = GetCollisionPoint().DistanceTo(GlobalPosition) - dist;
				GD.Print(diff);
				GetNode<RigidBody3D>("/root/Root/Cube").GlobalPosition = GetCollisionPoint() + (GlobalPosition - GetCollisionPoint()).Normalized() * (diff + Math.Max(box.Scale.X, Math.Max(box.Scale.Y, box.Scale.Z)) / 2);
				/*GetNode<CollisionShape3D>("/root/Root/Cube/CollisionShape3D").Scale = new Vector3(1f, 1f, 1f) * dist * mult;
				GetNode<CsgBox3D>("/root/Root/Cube/CSGBox3D").Scale = new Vector3(1f, 1f, 1f) * dist * mult;*/
				shape.Scale = new Vector3(1f, 1f, 1f) * GetNode<RigidBody3D>("/root/Root/Cube").GlobalPosition.DistanceTo(GlobalPosition) * mult;
				box.Scale = new Vector3(1f, 1f, 1f) * GetNode<RigidBody3D>("/root/Root/Cube").GlobalPosition.DistanceTo(GlobalPosition) * mult;
			}
			else {
				GetNode<RigidBody3D>("/root/Root/Cube").GlobalPosition = GetCollisionPoint() + (GlobalPosition - GetCollisionPoint()).Normalized() * (Math.Max(box.Scale.X, Math.Max(box.Scale.Y, box.Scale.Z)) / 2);
				shape.Scale = new Vector3(1f, 1f, 1f) * GetNode<RigidBody3D>("/root/Root/Cube").GlobalPosition.DistanceTo(GlobalPosition) * mult;
				box.Scale = new Vector3(1f, 1f, 1f) * GetNode<RigidBody3D>("/root/Root/Cube").GlobalPosition.DistanceTo(GlobalPosition) * mult;
			}
			
		}	
	}

	public override void _Input(InputEvent @event)
	{	
    	if (@event is InputEventMouseButton eventMouseButton && eventMouseButton.ButtonIndex.Equals(MouseButton.Left)) {
			run = false;
		}
	}
}
