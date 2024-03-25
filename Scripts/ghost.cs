using Godot;
using System;

public partial class ghost : MeshInstance3D
{
	int time = 0;
	bool runtime = false;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		if ((GetNode<sequencer>("/root/Sequencer").seqNum == 4 || GetNode<sequencer>("/root/Sequencer").seqNum == 5) && GlobalPosition.DistanceTo(GetNode<CharacterBody3D>("/root/Root/CharacterBody3D/").GlobalPosition) < 3 && !runtime)
		{
			GetNode<sequencer>("/root/Sequencer").seqNum = 3;
			GetNode<AnimationPlayer>("/root/Root/CanvasLayer/Fader/Fade").Play("fade_in");
			runtime = true;
			GetNode<sequencer>("/root/Sequencer").floors = 0;
			GetNode<CsgBox3D>("/root/Root/HallwayBot/Walls/Wall32").UseCollision = false;
			GetNode<AnimationPlayer>("/root/Root/CapeHolder/GhostCape/GhostAnimation").Stop();
			GetNode<Node3D>("/root/Root/CapeHolder").GlobalPosition = Vector3.Zero;
			GetNode<Node3D>("/root/Root/CapeHolder").GlobalRotation = Vector3.Zero;
			GetNode<music>("/root/Music").Stop();
		}

		if (GetNode<sequencer>("/root/Sequencer").seqNum >= 10 && GetNode<sequencer>("/root/Sequencer").seqNum <= 14 && GlobalPosition.DistanceTo(GetNode<CharacterBody3D>("/root/Root/CharacterBody3D/").GlobalPosition) < 3 && !runtime)
		{
			GetNode<sequencer>("/root/Sequencer").seqNum = 10;
			GetNode<AnimationPlayer>("/root/Root/CanvasLayer/Fader/Fade").Play("fade_in");
			GetNode<MeshInstance3D>("/root/Root/Hallway/Doors/DoubleDoor1").Visible = true;
			GetNode<CsgBox3D>("/root/Root/Hallway/Doors/DoubleDoor1/CSGBox3D").UseCollision = true;
			GetNode<Node3D>("/root/Root/Hallway/Doors/DoorHolder1").GlobalRotationDegrees = new Vector3(GetNode<Node3D>("/root/Root/Hallway/Doors/DoorHolder1").GlobalRotationDegrees.X, -90, GetNode<Node3D>("/root/Root/Hallway/Doors/DoorHolder1").GlobalRotationDegrees.Z);
			GetNode<Node3D>("/root/Root/Hallway/Doors/DoorHolder2").GlobalRotationDegrees = new Vector3(GetNode<Node3D>("/root/Root/Hallway/Doors/DoorHolder2").GlobalRotationDegrees.X, 90, GetNode<Node3D>("/root/Root/Hallway/Doors/DoorHolder2").GlobalRotationDegrees.Z);
			GetNode<Node3D>("/root/Root/Hallway/Doors/DoorHolder3").GlobalRotationDegrees = new Vector3(GetNode<Node3D>("/root/Root/Hallway/Doors/DoorHolder3").GlobalRotationDegrees.X, -90, GetNode<Node3D>("/root/Root/Hallway/Doors/DoorHolder3").GlobalRotationDegrees.Z);
			GetNode<Node3D>("/root/Root/Hallway/Doors/DoorHolder4").GlobalRotationDegrees = new Vector3(GetNode<Node3D>("/root/Root/Hallway/Doors/DoorHolder4").GlobalRotationDegrees.X, 90, GetNode<Node3D>("/root/Root/Hallway/Doors/DoorHolder4").GlobalRotationDegrees.Z);
			runtime = true;
			GetNode<music>("/root/Music").Stop();
		}

		if (runtime) time++;

		if (time == 150) {
			runtime = false;
			GetNode<sequencer>("/root/Sequencer").nextSeq = true;
			GetNode<AnimationPlayer>("/root/Root/CanvasLayer/Fader/Fade").Play("fade_out");
			GetNode<Node3D>("/root/Root/CapeHolder").Visible = false;
			time = 0;
		}
	}
}
