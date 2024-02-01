using Godot;
using System;

public partial class unfade : CsgBox3D
{
	bool run = true;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		if (run) {
			StandardMaterial3D tmpMat = (StandardMaterial3D) Material;
			Color tmpCol = tmpMat.AlbedoColor;
			tmpCol.A = tmpCol.A - 0.007f;
			tmpMat.AlbedoColor = tmpCol;
			Material = tmpMat;
			if (tmpCol.A <= 0.01) run = false;
			//GD.Print(tmpCol.A);
		}
	}
}
