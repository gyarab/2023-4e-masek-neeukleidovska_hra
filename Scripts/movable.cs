using Godot;
using System;

public partial class movable : RigidBody3D
{
	public Vector3 norm = new Vector3(0, 0, 0);
	public bool run = false;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//max
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

    public override void _IntegrateForces(PhysicsDirectBodyState3D state)
    {	
        //base._IntegrateForces(state);
		Transform3D temp = state.Transform;
		int b = 0;
		while (run && GetNode<RigidBody3D>("/root/Root/Cube").GetContactCount() > 0) {
			temp.Origin.X += norm.X;
			temp.Origin.Y += norm.Y;
			temp.Origin.Z += norm.Z;
			state.Transform = temp;
			GD.Print(GetNode<RigidBody3D>("/root/Root/Cube").GetContactCount());
			b++;
			if (b > 99) {run = false;}
			base._IntegrateForces(state);
			_PhysicsProcess(1f);
		}
		run = false;
	}
}
