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
					conv_door conv_door = GetNode<conv_door>("/root/Root/Hallway/Doors/DoorHolder5/Door14");
					conv_door.open = false;
					conv_door.GetNode<AnimationPlayer>("AnimationPlayer").Play("close");
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
					GetNode<Node3D>("/root/Root/CapeHolder").Visible = false;
					GetNode<AnimationPlayer>("/root/Root/CharacterBody3D/PlayerAnimator").Play("anim_8");
					subtitleArray = new string[800];
					subtitleArray[180] = "Wow, it's giant!!";
					subtitleArray[399] = "So it can change scale of things...";
					subtitleArray[599] = "Maybe I could shrink the ghost so it can't hurt me";
					subtitleArray[799] = " ";
					time = 0;
					break;
				case 9:
					subtitleArray = new string[600];
					subtitleArray[0] = "Hmm it's gone...";
					subtitleArray[299] = "I'd better find it before it finds me";
					subtitleArray[599] = " ";
					time = 0;
					break;
				case 10:
					GetNode<AnimationPlayer>("/root/Root/CharacterBody3D/PlayerAnimator").Play("anim_10");
					GetNode<AnimationPlayer>("/root/Root/CapeHolder/GhostCape/GhostAnimation").Play("ghost_3");
					subtitleArray = new string[1];
					subtitleArray[0] = "There you are, say goodbye!";
					time = 0;
					break;
				case 11:
					subtitleArray = new string[400];
					subtitleArray[0] = "IT DID NOTHING!!!! I have to run through the double door, theres no other way";
					subtitleArray[199] = " ";
					time = 0;
					break;
				case 12:
					GetNode<AnimationPlayer>("/root/Root/CharacterBody3D/PlayerAnimator").Play("anim_11");
					//GetNode<AnimationPlayer>("/root/Root/CapeHolder/GhostCape/GhostAnimation").Play("ghost_3");
					subtitleArray = new string[600];
					subtitleArray[0] = "Arrgh!";
					subtitleArray[199] = "NOO, I'm stuck in a loop";
					subtitleArray[399] = "Let's try the toilet door";
					subtitleArray[599] = " ";
					time = 0;
					break;
				case 13:
					GetNode<AnimationPlayer>("/root/Root/CharacterBody3D/PlayerAnimator").Play("anim_12");
					//GetNode<AnimationPlayer>("/root/Root/CapeHolder/GhostCape/GhostAnimation").Play("ghost_3");
					subtitleArray = new string[400];
					subtitleArray[0] = "What?! It's still the same";
					subtitleArray[199] = "Okay new plan, lets overload the gun and try shooting it again";
					subtitleArray[399] = " ";
					time = 0;
					break;
				case 14:
					GetNode<AnimationPlayer>("/root/Root/CharacterBody3D/PlayerAnimator").Play("anim_13");
					//GetNode<AnimationPlayer>("/root/Root/CapeHolder/GhostCape/GhostAnimation").Play("ghost_3");
					subtitleArray = new string[500];
					subtitleArray[0] = "Lets get the power going";
					subtitleArray[299] = "Eat this!";
					subtitleArray[499] = " ";
					time = 0;
					break;
				case 15:
					GetNode<AnimationPlayer>("/root/Root/CharacterBody3D/PlayerAnimator").Play("anim_14");
					GetNode<Sprite2D>("/root/Root/CanvasLayer/Control2/Manipulator").Visible = false;
					Node3D manipulator = GetNode<Node3D>("/root/Root/Manipulator");
					manipulator.Visible = true;
					//Vector3 tmp = manipulator.GlobalPosition;
					//manipulator.GetParent().RemoveChild(manipulator);
					//GetNode<Node3D>("/root/Root").AddChild(manipulator);
					//manipulator.GlobalPosition = tmp;
					//GetNode<AnimationPlayer>("/root/Root/CapeHolder/GhostCape/GhostAnimation").Play("ghost_3");
					subtitleArray = new string[200];
					subtitleArray[0] = "Aw shit.....SHIT";
					subtitleArray[199] = " ";
					time = 0;
					break;
				case 16:
					GetNode<AnimationPlayer>("/root/Root/CharacterBody3D/PlayerAnimator").Play("anim_15");
					subtitles = GetNode<subtitles>("/root/Root/CanvasLayer/Subtitles");
					informative = GetNode<informative>("/root/Root/CanvasLayer/Informative");
					subtitleArray = new string[2400];
					subtitleArray[0] = "Where am I...";
					subtitleArray[599] = "Woah woah......";
					subtitleArray[749] = "why are you not attacking me?";
					subtitleArray[999] = "I get it now...I never woke up, did I?";
					subtitleArray[1299] = "I'm slowly going crazy from all the pressure and I know that is what you want";
					subtitleArray[1599] = "But now it's all done....";
					subtitleArray[1799] = "the books, the exams, this GAME....it is all finished!";
					subtitleArray[2099] = "You are free to go now my friend.....";
					subtitleArray[2399] = " ";
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
					GetNode<CsgBox3D>("/root/Root/HallwayBot/Walls/Wall32").UseCollision = true;
					seqNum++;
					nextSeq = true;
				}
				else if (floors == 0)
				{
					Vector3 tmp = player.GlobalPosition;
					tmp.Y += 3.4f;
					player.GlobalPosition = tmp;
					if (floors == 1) GetNode<Node3D>("/root/Root/CapeHolder").Visible = true;
					floors++;
					informative.Inform("I thought I was on the first floor");
				}
				else
				{
					Vector3 tmp = player.GlobalPosition;
					tmp.Y += 3.4f;
					player.GlobalPosition = tmp;
					if (floors == 1) GetNode<Node3D>("/root/Root/CapeHolder").Visible = true;
					floors++;
					informative.Inform("Is this still the same floor?");
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
			//manipulator.GetParent().RemoveChild(manipulator);
			//GetNode<Node3D>("/root/Root/CharacterBody3D/CameraRig").AddChild(manipulator);
			//manipulator.GlobalTransform = tmp;
			manipulator.GetNode<CsgBox3D>("Collider").UseCollision = false;
			manipulator.Visible = false;
			//GetNode<Node3D>("/root/Root/CharacterBody3D/Manipulator").GlobalPosition = manipulator.GlobalPosition;
			GetNode<Sprite2D>("/root/Root/CanvasLayer/Control2/Manipulator").Visible = true;
			informative.Inform("Use [RMB] to pick up");
		}
		else if (seqNum == 8 && time == 59)
		{
			GetNode<perspective_raycast>("/root/Root/CharacterBody3D/CameraRig/RayCastCenter").Grab();
		}
		else if (seqNum == 8 && time == 210)
		{
			perspective_raycast perspective_raycast = GetNode<perspective_raycast>("/root/Root/CharacterBody3D/CameraRig/RayCastCenter");
			perspective_raycast.Grab();
			perspective_raycast.allow = true;
		}
		else if (seqNum == 15 && time == 419)
		{
			GetTree().ChangeSceneToFile("res://Scenes/void.tscn");
			seqNum++;
			nextSeq = true;
		}

		if (seqNum == 8 && GetNode<CharacterBody3D>("/root/Root/CharacterBody3D").GlobalPosition.X < 3.97)
		{
			seqNum++;
			nextSeq = true;
		}
		else if (seqNum == 9 && GetNode<CharacterBody3D>("/root/Root/CharacterBody3D").GlobalPosition.Z < 7.77)
		{
			seqNum++;
			nextSeq = true;
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
