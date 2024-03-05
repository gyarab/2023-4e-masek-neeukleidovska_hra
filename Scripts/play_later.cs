using Godot;
using System;

public partial class play_later : AudioStreamPlayer
{
	int time = 0;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (time < 300) {
			time++;
		} else if (time == 300) {
			Play();
			time++;
		}
	}
}
