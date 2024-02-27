using Godot;
using System;

public partial class sequencer : Node3D
{
	public bool nextSeq = true;
	public int seqNum = 1;
	string[] subtitleArray = new string[0];
	int time = 0;
	subtitles subtitles;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		subtitles = GetNode<subtitles>("/root/Root/CharacterBody3D/CameraRig/Camera3D/Subtitles");
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
					subtitleArray = new string[700];
					subtitleArray[149] = "I'm so damn sleepy!";
					subtitleArray[249] = " ";
					subtitleArray[399] = "The lesson starts in fourty minutes, maybe I could get a short nap";
					subtitleArray[599] = "Someone will wake me up anyway....";
					subtitleArray[699] = " ";
					time = 0;
					break;
			}
		}

		if (seqNum == 2 && time == 540) GetNode<unfade>("/root/Root/CharacterBody3D/CameraRig/Camera3D/Fader").fadeIn = true;

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
