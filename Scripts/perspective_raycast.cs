using Godot;
using System;

public partial class perspective_raycast : RayCast3D
{
	bool run = true;
	Vector3 norm;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		AddException(GetNode<RigidBody3D>("/root/Root/Cube"));
		GetNode<RigidBody3D>("/root/Root/Cube").Freeze = true;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		var cPos = GetNode<RigidBody3D>("/root/Root/Cube").GlobalPosition;
		GD.Print("Pos: " + GlobalPosition);
		if (IsColliding() && run) {
			var spaceState = GetWorld3D().DirectSpaceState;
			
			var excl = new Godot.Collections.Array<Rid>();
			excl.Add(GetNode<RigidBody3D>("/root/Root/Cube").GetRid());
			//GD.Print(GlobalTransform.Origin.DistanceTo(GetCollisionPoint()));
			var query1 = PhysicsRayQueryParameters3D.Create(GetParent<Node3D>().GlobalPosition, new Vector3(cPos.X + 0.5f, cPos.Y + 0.5f, cPos.Z + 0.5f) + new Vector3(cPos.X + 0.5f - GetParent<Node3D>().GlobalPosition.X, cPos.Y + 0.5f - GetParent<Node3D>().GlobalPosition.Y, cPos.Z + 0.5f - GetParent<Node3D>().GlobalPosition.Y).Normalized() * 100);
			query1.Exclude = excl;
			query1.HitFromInside = false;
			var results = spaceState.IntersectRay(query1);
			if (results.ContainsKey("position")) {
				GD.Print(GlobalPosition.DistanceTo((Vector3) results["position"]));
				GetNode<CsgSphere3D>("/root/Root/pointer1").GlobalPosition = (Vector3) results["position"];
				//GetNode<CsgSphere3D>("/root/Root/pointer2").GlobalPosition = new Vector3(cPos.X + 0.5f, cPos.Y + 0.5f, cPos.Z + 0.5f).Normalized() * 100;
				GetNode<CsgSphere3D>("/root/Root/pointer3").GlobalPosition = GlobalPosition;
			} 
			//GD.Print(results.Keys.ToString());
			GetNode<RigidBody3D>("/root/Root/Cube").GlobalPosition = GetCollisionPoint();
		}	
		GetNode<CsgSphere3D>("/root/Root/pointer2").GlobalPosition = new Vector3(cPos.X + 0.5f - GlobalPosition.X, cPos.Y + 0.5f - GlobalPosition.X, cPos.Z + 0.5f - GlobalPosition.X).Normalized() * 100;
	}

	public override void _Input(InputEvent @event)
	{	
    	if (@event is InputEventMouseButton eventMouseButton && eventMouseButton.ButtonIndex.Equals(MouseButton.Left)) {
			run = false;
		}
	}
}
