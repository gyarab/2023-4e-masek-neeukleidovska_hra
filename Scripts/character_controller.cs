using Godot;
using System;

public partial class character_controller : CharacterBody3D
{
	private Vector2 previousMousePosition;
	public const float Speed = 5.0f;
	public const float JumpVelocity = 4.5f;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();

	public override void _PhysicsProcess(double delta)
	{
		Vector3 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
			velocity.Y -= gravity * (float)delta;

		// Handle Jump.
		if (Input.IsActionJustPressed("ui_jump") && IsOnFloor()) {
			velocity.Y = JumpVelocity;
		}

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 inputDir = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		Vector3 direction = (Transform.Basis * new Vector3(inputDir.X, 0, inputDir.Y)).Normalized();
		if (direction != Vector3.Zero)
		{
			velocity.X = direction.X * Speed;
			velocity.Z = direction.Z * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
			velocity.Z = Mathf.MoveToward(Velocity.Z, 0, Speed);
		}

		Velocity = velocity;
		MoveAndSlide();
		/*if (Position.Z < 0 && Position.Y < 5) {
			Position = new Vector3(Position.X, Position.Y + 10, Position.Z);
		}*/
	}

	public override void _Input(InputEvent @event) {
		if (@event is InputEventMouseMotion mouseEvent)
        {
            Vector2 mouseDelta = mouseEvent.Relative;
            float rotationSpeed = 0.006f;
            RotateObjectLocal(Vector3.Up, -mouseDelta.X * rotationSpeed);
            GetNode<Node3D>("CameraRig").Rotate(Vector3.Right, -mouseDelta.Y * rotationSpeed);
            float maxPitch = 80.0f;
            float minPitch = -80.0f;
            //var rotationDegrees = RotationDegrees;
            RotationDegrees = new Vector3(Mathf.Clamp(RotationDegrees.X, minPitch, maxPitch), RotationDegrees.Y, RotationDegrees.Z);
            //RotationDegrees = rotationDegrees;
            previousMousePosition = mouseEvent.Position;
        }
	}

    public override void _Ready()
    {
        Input.MouseMode = Input.MouseModeEnum.Captured;
		DisplayServer.WindowSetMode(DisplayServer.WindowMode.Fullscreen);
    }
}
