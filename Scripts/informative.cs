using Godot;
using System;

public partial class informative : Label
{
	string text;
	int time;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		if (time > -26)
		{
			time--;
			Color tmpMod = LabelSettings.FontColor;
			Color tmpOut = LabelSettings.OutlineColor;
			tmpMod.A = 1f / 25f * Math.Abs(time);
			tmpOut.A = 1f / 25f * Math.Abs(time);
			LabelSettings.FontColor = tmpMod;
			LabelSettings.OutlineColor = tmpOut;
			if (time == 0) Text = text;
		}
		else if (time >= -200)
		{
			time--;
			if (time < 175)
			{
				Color tmpMod = LabelSettings.FontColor;
				Color tmpOut = LabelSettings.OutlineColor;
				tmpMod.A = 1f / 25f * (200 -Math.Abs(time));
				tmpOut.A = 1f / 25f * (200 -Math.Abs(time));
				LabelSettings.FontColor = tmpMod;
				LabelSettings.OutlineColor = tmpOut;
			}
			if (time == -200) Text = " ";
		}
	}

	public void Inform(string newText)
	{
		text = newText;
		time = 25;
	}
}
