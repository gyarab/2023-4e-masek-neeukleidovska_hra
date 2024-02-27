using Godot;
using System;

public partial class unfade : CsgBox3D
{
	public bool fadeOut = true;
	public bool fadeIn = false;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		if (fadeOut) {
			StandardMaterial3D tmpMat = (StandardMaterial3D) Material;
			Color tmpCol = tmpMat.AlbedoColor;
			tmpCol.A = tmpCol.A - 0.007f;
			tmpMat.AlbedoColor = tmpCol;
			Material = tmpMat;
			if (tmpCol.A <= 0.01) fadeOut = false;
			//GD.Print(tmpCol.A);
		}

		if (fadeIn) {
			StandardMaterial3D tmpMat = (StandardMaterial3D) Material;
			Color tmpCol = tmpMat.AlbedoColor;
			tmpCol.A = tmpCol.A + 0.0055f;
			tmpMat.AlbedoColor = tmpCol;
			Material = tmpMat;
			if (tmpCol.A >= 0.99) fadeIn = false;
		}
	}
}
