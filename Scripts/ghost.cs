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
		}

		if (GetNode<sequencer>("/root/Sequencer").seqNum >= 11 && GetNode<sequencer>("/root/Sequencer").seqNum <= 14 && GlobalPosition.DistanceTo(GetNode<CharacterBody3D>("/root/Root/CharacterBody3D/").GlobalPosition) < 3 && !runtime)
		{
			GetNode<sequencer>("/root/Sequencer").seqNum = 10;
			GetNode<AnimationPlayer>("/root/Root/CanvasLayer/Fader/Fade").Play("fade_in");
			GetNode<MeshInstance3D>("/root/Root/Hallway/Doors/DoubleDoor1").Visible = true;
			GetNode<CsgBox3D>("/root/Root/Hallway/Doors/DoubleDoor1/CSGBox3D").UseCollision = true;
			runtime = true;
		}

		if (runtime) time++;

		if (time == 150) {
			runtime = false;
			GetNode<sequencer>("/root/Sequencer").nextSeq = true;
			GetNode<AnimationPlayer>("/root/Root/CanvasLayer/Fader/Fade").Play("fade_out");
			time = 0;
		}
	}
}
