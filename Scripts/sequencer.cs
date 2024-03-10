using Godot;
using System;
using System.Drawing;
using System.IO;

public partial class sequencer : Node
{
	public bool nextSeq = true;
	public int seqNum = 1;
	string[] subtitleArray = new string[0];
	int time = 0;
	subtitles subtitles;
	informative informative;
	byte floors = 0;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		subtitles = GetNode<subtitles>("/root/Root/CanvasLayer/Subtitles");
		informative = GetNode<informative>("/root/Root/CanvasLayer/Informative");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}

	public override void _PhysicsProcess(double delta)
	{
		if (nextSeq)
		{
			nextSeq = false;
			switch (seqNum)
			{
				case 1:
					GetNode<AnimationPlayer>("/root/Root/CanvasLayer/Fader/Fade").Play("fade_out");
					GetNode<AnimationPlayer>("/root/Root/CharacterBody3D/PlayerAnimator").Play("anim_1");
					subtitleArray = new string[800];
					subtitleArray[0] = "Damn, what am I doing here so early?";
					subtitleArray[299] = "I should have checked my alarm before going to sleep";
					subtitleArray[499] = " ";
					subtitleArray[549] = "Okay, first class is English conversation, that's in room number 115";
					subtitleArray[799] = " ";
					time = 0;
					break;
				case 2:
					GetNode<AnimationPlayer>("/root/Root/CharacterBody3D/PlayerAnimator").Play("anim_2");
					subtitleArray = new string[800];
					subtitleArray[99] = "I'm so damn sleepy!";
					subtitleArray[249] = " ";
					subtitleArray[349] = "The lesson starts in fourty minutes, maybe I could get a short nap";
					subtitleArray[599] = "Someone will wake me up anyway....";
					subtitleArray[699] = " ";
					time = 0;
					break;
				case 3:
					subtitles = GetNode<subtitles>("/root/Root/CanvasLayer/Subtitles");
					informative = GetNode<informative>("/root/Root/CanvasLayer/Informative");
					GetNode<AnimationPlayer>("/root/Root/CharacterBody3D/PlayerAnimator").Play("anim_3");
					GetNode<Sprite2D>("/root/Root/CanvasLayer/Fader").Modulate = new Godot.Color(0, 0, 0, 0);
					subtitleArray = new string[500];
					subtitleArray[0] = "What the hell was that?! Why is it dark outside?";
					subtitleArray[199] = "Did I sleep through the whole day? No wayy!";
					subtitleArray[349] = "I have to get the hell out of here";
					subtitleArray[499] = " ";
					time = 0;
					break;
				case 4:
					subtitleArray = new string[650];
					subtitleArray[99] = "Woah what the hell is that?!!";
					subtitleArray[349] = "Is that thing moving?";
					subtitleArray[499] = "HEY STOP STOP!!!";
					subtitleArray[599] = "I have to get to the other staircase, it's behind the double-door";
					time = 0;
					break;
				case 5:
					GetNode<AnimationPlayer>("/root/Root/CharacterBody3D/PlayerAnimator").Play("anim_5");
					GetNode<AnimationPlayer>("/root/Root/CapeHolder/GhostCape/GhostAnimation").Play("ghost_2");
					GetNode<AudioStreamPlayer3D>("/root/Root/CapeHolder/GhostCape/AudioStreamPlayer3D").Stop();
					subtitleArray = new string[200];
					subtitleArray[59] = "Fuck it's locked!!! I have to find an unlocked classroom";
					subtitleArray[199] = " ";
					time = 0;
					break;
				case 6:
					GetNode<AnimationPlayer>("/root/Root/CharacterBody3D/PlayerAnimator").Play("anim_6");
					GetNode<AudioStreamPlayer3D>("/root/Root/CapeHolder/GhostCape/AudioStreamPlayer3D").Stop();
					subtitleArray = new string[400];
					subtitleArray[129] = "Where the hell am I??? This isn't my school, not with THAT THING!";
					subtitleArray[399] = " ";
					time = 0;
					break;
				case 7:
					GetNode<AnimationPlayer>("/root/Root/CharacterBody3D/PlayerAnimator").Play("anim_7");
					subtitleArray = new string[600];
					subtitleArray[0] = "What's this? Looks like some kind of a weapon, could be useful...";
					subtitleArray[249] = " ";
					subtitleArray[399] = "I should try it on that apple";
					subtitleArray[599] = " ";
					time = 0;
					break;
				case 8:
					GetNode<AnimationPlayer>("/root/Root/CharacterBody3D/PlayerAnimator").Play("anim_8");
					GetNode<perspective_raycast>("/root/Root/CharacterBody3D/CameraRig/RayCastCenter").Grab();
					subtitleArray = new string[800];
					subtitleArray[0] = "Wow, it's giant!!";
					subtitleArray[249] = "So it can change scale of things...";
					subtitleArray[599] = "Maybe I could crush that ghost by making something large";
					subtitleArray[799] = " ";
					time = 0;
					break;
			}
		}

		if (seqNum == 1 && time == 149) informative.Inform("Use [WASD] to move and [SPACE] to jump");
		else if (seqNum == 1 && time == 399) informative.Inform("Use [E] to interact");
		else if (seqNum == 2 && time == 540) GetNode<AnimationPlayer>("/root/Root/CanvasLayer/Fader/Fade").Play("fade_in");
		else if (seqNum == 2 && time == 749) GetTree().ChangeSceneToFile("res://Scenes/night.tscn");
		else if (seqNum == 2 && time == 799)
		{
			seqNum++;
			nextSeq = true;
		}
		else if (seqNum == 3)
		{

			CharacterBody3D player = GetNode<CharacterBody3D>("/root/Root/CharacterBody3D");
			if (player.GlobalPosition.Y < -0.69)
			{
				if (floors == 2)
				{
					GetNode<AnimationPlayer>("/root/Root/CapeHolder/GhostCape/GhostAnimation").Play("ghost_1");
					GetNode<AudioStreamPlayer3D>("/root/Root/CapeHolder/GhostCape/AudioStreamPlayer3D").Play();
					GetNode<AnimationPlayer>("/root/Root/CharacterBody3D/PlayerAnimator").Play("anim_4");
					seqNum++;
					nextSeq = true;
				}
				else
				{
					Vector3 tmp = player.GlobalPosition;
					tmp.Y += 3.4f;
					player.GlobalPosition = tmp;
					if (floors == 1) GetNode<Node3D>("/root/Root/CapeHolder").Visible = true;
					floors++;
				}
			}
			else if (player.GlobalPosition.Y > 2.72)
			{
				Vector3 tmp = player.GlobalPosition;
				tmp.Y -= 3.4f;
				player.GlobalPosition = tmp;
			}
		}
		else if (seqNum == 4 && time == 599) informative.Inform("Use [SHIFT] to sprint");
		else if (seqNum == 5 && time == 59) GetNode<AudioStreamPlayer3D>("/root/Root/Hallway/Doors/DoubleDoor1/CSGBox3D/AudioStreamPlayer3D").Play();
		else if (seqNum == 5 && time == 299)
		{
			GetNode<AudioStreamPlayer3D>("/root/Root/Hallway/Doors/DoubleDoor1/CSGBox3D/AudioStreamPlayer3D").Stream = (AudioStream)GD.Load("res://Audio/chase.mp3");
			GetNode<AudioStreamPlayer3D>("/root/Root/Hallway/Doors/DoubleDoor1/CSGBox3D/AudioStreamPlayer3D").Play();
		}
		else if (seqNum == 6 && time == 29)
		{
			AudioStreamPlayer allPurpose = GetNode<AudioStreamPlayer>("/root/Root/CharacterBody3D/AllPurpose");
			allPurpose.Stream = (AudioStream)GD.Load("res://Audio/open.mp3");
			allPurpose.Play();
		}
		else if (seqNum == 6 && time == 119)
		{
			AudioStreamPlayer allPurpose = GetNode<AudioStreamPlayer>("/root/Root/CharacterBody3D/AllPurpose");
			allPurpose.Stream = (AudioStream)GD.Load("res://Audio/open.mp3");
			allPurpose.Play();
		}
		else if (seqNum == 7 && time == 512)
		{
			Node3D manipulator = GetNode<Node3D>("/root/Root/Math/Manipulator");
			Transform3D tmp = manipulator.GlobalTransform;
			manipulator.GetParent().RemoveChild(manipulator);
			GetNode<Node3D>("/root/Root/CharacterBody3D/CameraRig").AddChild(manipulator);
			manipulator.GlobalTransform = tmp;
			manipulator.GetNode<CsgBox3D>("Collider").UseCollision = false;
			informative.Inform("Use [RMB] to pick up");
		}
		else if (seqNum == 8 && time == 119)
		{
			GetNode<perspective_raycast>("/root/Root/CharacterBody3D/CameraRig/RayCastCenter").Grab();
		}

		try
		{
			if (!subtitleArray[time].Equals("")) subtitles.ChangeTo(subtitleArray[time]);
		}
		catch
		{
			//GD.Print(time + " doesnt exist");
		}

		time++;
	}
}
