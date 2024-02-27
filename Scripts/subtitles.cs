using Godot;
using System;

public partial class subtitles : Label3D
{
	string text;
	int time;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (time > -26) {
			time--;
			Color tmpMod = Modulate;
			Color tmpOut = OutlineModulate;
			tmpMod.A = 1f / 25f * Math.Abs(time);
			tmpOut.A = 1f / 25f * Math.Abs(time);
			Modulate = tmpMod;
			OutlineModulate = tmpOut;
			if (time == 0) Text = text;
		}
	}

	public void ChangeTo(string newText) {
		text = newText;
		time = 25;
	}
}
