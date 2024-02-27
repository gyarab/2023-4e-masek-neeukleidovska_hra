using Godot;
using Godot.Collections;
using System;

public partial class character_controller : CharacterBody3D
{
	private Vector2 previousMousePosition;
	public const float Speed = 2.1f;
	public const float JumpVelocity = 4.5f;

	public float gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();

	public override void _PhysicsProcess(double delta)
	{
		Vector3 velocity = Velocity;

		//GD.Print(GetNode<Node3D>("CameraRig").RotationDegrees.X);
		if (GetNode<Node3D>("CameraRig").RotationDegrees.X <= 90 && GetNode<Node3D>("CameraRig").RotationDegrees.X >= -90)
		{
			//Camera position
			GetNode<Node3D>("CameraRig").Position = new Vector3(GetNode<Node3D>("CameraRig").Position.X,Mathf.MoveToward(GetNode<Node3D>("CameraRig").Position.Y, 0.5f, 0.03f), GetNode<Node3D>("CameraRig").Position.Z);

			//Item gravity
			PhysicsServer3D.AreaSetParam(GetWorld3D().Space, PhysicsServer3D.AreaParameter.Gravity, 9.8);

			//Player gravity
			gravity = 15;//ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();
			if (!IsOnFloor()) velocity.Y -= gravity * (float)delta;

			// Jump
			if (Input.IsActionJustPressed("ui_jump") && IsOnFloor())
			{
				velocity.Y = JumpVelocity;
			}

			// Input direction
			Vector2 inputDir = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
			Vector3 direction = (Transform.Basis * new Vector3(inputDir.X, 0, inputDir.Y)).Normalized();
			if (IsOnFloor())
			{
				if (direction != Vector3.Zero)
				{
					velocity.X = direction.X * Speed;
					velocity.Z = direction.Z * Speed;
					GetNode<view_bobbing>("CameraRig/WalkingAnimation").Walking(true);
				}
				else
				{
					GetNode<view_bobbing>("CameraRig/WalkingAnimation").Walking(false);
					velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
					velocity.Z = Mathf.MoveToward(Velocity.Z, 0, Speed);
				}
			}
			else if (direction != Vector3.Zero)
			{
				velocity.X = Mathf.MoveToward(Velocity.X, direction.X * Speed, Speed / 20);
				velocity.Z = Mathf.MoveToward(Velocity.Z, direction.Z * Speed, Speed / 20);
			}
		}
		else
		{
			/*//Camera position
			GetNode<Node3D>("CameraRig").Position = new Vector3(GetNode<Node3D>("CameraRig").Position.X,Mathf.MoveToward(GetNode<Node3D>("CameraRig").Position.Y, -0.5f, 0.03f), GetNode<Node3D>("CameraRig").Position.Z);

			//Item gravity
			PhysicsServer3D.AreaSetParam(GetWorld3D().Space, PhysicsServer3D.AreaParameter.Gravity, -9.8);

			//Player gravity
			gravity = -ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();
			if (!IsOnCeiling()) velocity.Y -= gravity * (float)delta;

			// Jump
			if (Input.IsActionJustPressed("ui_jump") && IsOnCeiling())
			{
				velocity.Y = -JumpVelocity;
			}

			// Input direction
			Vector2 inputDir = Input.GetVector("ui_right", "ui_left", "ui_up", "ui_down");
			Vector3 direction = (Transform.Basis * new Vector3(inputDir.X, 0, inputDir.Y)).Normalized();
			if (IsOnCeiling())
			{
				if (direction != Vector3.Zero)
				{
					velocity.X = direction.X * Speed;
					velocity.Z = direction.Z * Speed;
					velocity.X = -velocity.X;
					velocity.Z = -velocity.Z;
				}
				else
				{
					velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
					velocity.Z = Mathf.MoveToward(Velocity.Z, 0, Speed);
				}
			}
			else if (direction != Vector3.Zero)
			{
				velocity.X = Mathf.MoveToward(Velocity.X, -direction.X * Speed, Speed / 20);
				velocity.Z = Mathf.MoveToward(Velocity.Z, -direction.Z * Speed, Speed / 20);
			}*/
		}
		Velocity = velocity;
		MoveAndSlide();

		if (Position.Y < -0.69) {
			Vector3 tmp = GlobalPosition;
			tmp.Y += 3.4f;
			GlobalPosition = tmp;
		} else if (Position.Y > 2.72) {
			Vector3 tmp = GlobalPosition;
			tmp.Y -= 3.4f;
			GlobalPosition = tmp;
		}

		//GD.Print("Position: " + GlobalPosition.Y);
		//PerspectiveRaycast();
		/*if (Position.Z < 0 && Position.Y < 5) {
			Position = new Vector3(Position.X, Position.Y + 10, Position.Z);
		}*/
	}

	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventMouseMotion mouseEvent)
		{
			Vector2 mouseDelta = mouseEvent.Relative;
			float rotationSpeedX = 0.006f;
			float rotationSpeedY = 0.006f;
			//if (GetNode<Node3D>("CameraRig").RotationDegrees.X <= 90 && GetNode<Node3D>("CameraRig").RotationDegrees.X >= -90) rotationSpeedX = 0.006f;
			//else rotationSpeedX = -0.006f;
			RotateObjectLocal(Vector3.Up, -mouseDelta.X * rotationSpeedX);
			GetNode<Node3D>("CameraRig").Rotate(Vector3.Right, -mouseDelta.Y * rotationSpeedY);
			if (GetNode<Node3D>("CameraRig").RotationDegrees.X > 90) {
				Vector3 rd = GetNode<Node3D>("CameraRig").RotationDegrees;
				rd.X = 90;
				GetNode<Node3D>("CameraRig").RotationDegrees = rd;
			} else if (GetNode<Node3D>("CameraRig").RotationDegrees.X < -90) {
				Vector3 rd = GetNode<Node3D>("CameraRig").RotationDegrees;
				rd.X = -90;
				GetNode<Node3D>("CameraRig").RotationDegrees = rd;
			}
			//RotateObjectLocal(Vector3.Right, -mouseDelta.Y * rotationSpeedY);
			//float maxPitch = 80.0f;
			//float minPitch = -80.0f;
			//RotationDegrees = new Vector3(Mathf.Clamp(RotationDegrees.X, minPitch, maxPitch), RotationDegrees.Y, RotationDegrees.Z);
			previousMousePosition = mouseEvent.Position;
		}
	}
	
	/*void PerspectiveRaycast() {
		Dictionary result = GetWorld3D().DirectSpaceState.IntersectRay(PhysicsRayQueryParameters3D.Create(Vector3.Zero, Vector3.Forward));
		GD.Print(result["collider"]);
	}*/

	public override void _Ready()
	{
		Input.MouseMode = Input.MouseModeEnum.Captured;
		DisplayServer.WindowSetMode(DisplayServer.WindowMode.Fullscreen);
	}
}
