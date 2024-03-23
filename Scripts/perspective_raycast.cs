using Godot;
using System;
using System.Collections;

public partial class perspective_raycast : RayCast3D
{
	bool carry = false;
	Vector3 norm;
	float mult;
	RigidBody3D toCarry = new RigidBody3D();
	RayCast3D after;
	ArrayList excepted = new ArrayList();
	bool fc = true;
	public bool allow = true;

	sequencer sequencer;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		sequencer = GetNode<sequencer>("/root/Sequencer");
	}

	public void Except(bool exc)
	{
		if (exc)
		{
			AddException(toCarry);
			var topLeft = GetNode<RayCast3D>("/root/Root/CharacterBody3D/CameraRig/RayCastCenter/RayCastTopLeft");
			var topRight = GetNode<RayCast3D>("/root/Root/CharacterBody3D/CameraRig/RayCastCenter/RayCastTopRight");
			var botLeft = GetNode<RayCast3D>("/root/Root/CharacterBody3D/CameraRig/RayCastCenter/RayCastBotLeft");
			var botRight = GetNode<RayCast3D>("/root/Root/CharacterBody3D/CameraRig/RayCastCenter/RayCastBotRight");
			topLeft.AddException(toCarry);
			topRight.AddException(toCarry);
			botLeft.AddException(toCarry);
			botRight.AddException(toCarry);
			var box = toCarry.GetNode<CsgBox3D>("CSGBox3D");
			double xDeg = Math.Tan(box.Scale.Y / 2 / (toCarry.GlobalPosition.DistanceTo(GlobalPosition) - box.Scale.Y / 2)) * (180 / Math.PI);
			double zDeg = Math.Tan(box.Scale.X / 2 / (toCarry.GlobalPosition.DistanceTo(GlobalPosition) - box.Scale.X / 2)) * (180 / Math.PI);
			topLeft.RotationDegrees = new Vector3((float)xDeg, 0, (float)-zDeg);
			topRight.RotationDegrees = new Vector3((float)xDeg, 0, (float)zDeg);
			botLeft.RotationDegrees = new Vector3((float)-xDeg, 0, (float)-zDeg);
			botRight.RotationDegrees = new Vector3((float)-xDeg, 0, (float)zDeg);

			try
			{
				portal port = (portal)GetCollider();
				RayCast3D after = port.Recalculate(this);
				mult = Math.Max(box.Scale.X, Math.Max(box.Scale.Y, box.Scale.Z)) / (toCarry.GlobalPosition.DistanceTo(after.GlobalPosition) + GetCollisionPoint().DistanceTo(GlobalPosition));
			}
			catch
			{
				mult = Math.Max(box.Scale.X, Math.Max(box.Scale.Y, box.Scale.Z)) / toCarry.GlobalPosition.DistanceTo(GlobalPosition);
			}
			toCarry.SetCollisionLayerValue(7, false);
			toCarry.SetCollisionLayerValue(8, true);
		}
		else
		{
			GetNode<RayCast3D>("/root/Root/CharacterBody3D/CameraRig/RayCastCenter/RayCastTopLeft").ClearExceptions();
			GetNode<RayCast3D>("/root/Root/CharacterBody3D/CameraRig/RayCastCenter/RayCastTopRight").ClearExceptions();
			GetNode<RayCast3D>("/root/Root/CharacterBody3D/CameraRig/RayCastCenter/RayCastBotLeft").ClearExceptions();
			GetNode<RayCast3D>("/root/Root/CharacterBody3D/CameraRig/RayCastCenter/RayCastBotRight").ClearExceptions();
			ClearExceptions();
			foreach (RayCast3D ray in excepted)
			{
				ray.ClearExceptions();
			}
			excepted = new ArrayList();
			toCarry.SetCollisionLayerValue(7, true);
			toCarry.SetCollisionLayerValue(8, false);
		}
	}

	public override void _PhysicsProcess(double delta)
	{
		if (IsColliding())
		{
			if (carry)
			{
				//GetNode<Sprite2D>("/root/Root/CanvasLayer/Control/Crosshair").Texture = (Texture2D) GD.Load("res://Textures/hand.png");
				var topLeft = GetNode<RayCast3D>("/root/Root/CharacterBody3D/CameraRig/RayCastCenter/RayCastTopLeft");
				var topRight = GetNode<RayCast3D>("/root/Root/CharacterBody3D/CameraRig/RayCastCenter/RayCastTopRight");
				var botLeft = GetNode<RayCast3D>("/root/Root/CharacterBody3D/CameraRig/RayCastCenter/RayCastBotLeft");
				var botRight = GetNode<RayCast3D>("/root/Root/CharacterBody3D/CameraRig/RayCastCenter/RayCastBotRight");

				var shape = toCarry.GetNode<CollisionShape3D>("CollisionShape3D");
				var box = toCarry.GetNode<CsgBox3D>("CSGBox3D");

				float dist = float.MaxValue;

				if (topLeft.IsColliding())
				{
					try
					{
						portal port = (portal)topLeft.GetCollider();
						RayCast3D later = port.Recalculate(topLeft);
						if (!excepted.Contains(later)) excepted.Add(later);
						later.AddException(toCarry);
						if (later.IsColliding())
						{
							dist = topLeft.GetCollisionPoint().DistanceTo(GlobalPosition) + later.GlobalPosition.DistanceTo(later.GetCollisionPoint());
							//GD.Print("TL: " + dist);
						}
						else
						{
							dist = topLeft.GetCollisionPoint().DistanceTo(GlobalPosition);
						}
					}
					catch
					{
						dist = topLeft.GetCollisionPoint().DistanceTo(GlobalPosition);
					}
				}
				if (topRight.IsColliding())
				{
					float tmp;
					try
					{
						portal port = (portal)topRight.GetCollider();
						RayCast3D later = port.Recalculate(topRight);
						if (!excepted.Contains(later)) excepted.Add(later);
						later.AddException(toCarry);
						if (later.IsColliding())
						{
							tmp = topRight.GetCollisionPoint().DistanceTo(GlobalPosition) + later.GlobalPosition.DistanceTo(later.GetCollisionPoint());
							//GD.Print("TR: " + tmp);
						}
						else
						{
							tmp = topRight.GetCollisionPoint().DistanceTo(GlobalPosition);
						}
					}
					catch
					{
						tmp = topRight.GetCollisionPoint().DistanceTo(GlobalPosition);
					}
					//float tmp = topRight.GetCollisionPoint().DistanceTo(GlobalPosition);
					if (tmp < dist) dist = tmp;
				}
				if (botLeft.IsColliding())
				{
					float tmp;
					try
					{
						portal port = (portal)botLeft.GetCollider();
						RayCast3D later = port.Recalculate(botLeft);
						if (!excepted.Contains(later)) excepted.Add(later);
						later.AddException(toCarry);
						if (later.IsColliding())
						{
							tmp = botLeft.GetCollisionPoint().DistanceTo(GlobalPosition) + later.GlobalPosition.DistanceTo(later.GetCollisionPoint());
							//GD.Print("BL: " + tmp);
						}
						else
						{
							tmp = botLeft.GetCollisionPoint().DistanceTo(GlobalPosition);
						}
					}
					catch
					{
						tmp = botLeft.GetCollisionPoint().DistanceTo(GlobalPosition);
					}
					//float tmp = topRight.GetCollisionPoint().DistanceTo(GlobalPosition);
					if (tmp < dist) dist = tmp;
				}
				if (botRight.IsColliding())
				{
					float tmp;
					try
					{
						portal port = (portal)botRight.GetCollider();
						RayCast3D later = port.Recalculate(botRight);
						if (!excepted.Contains(later)) excepted.Add(later);
						later.AddException(toCarry);
						if (later.IsColliding())
						{
							tmp = botRight.GetCollisionPoint().DistanceTo(GlobalPosition) + later.GlobalPosition.DistanceTo(later.GetCollisionPoint());
							//GD.Print("BR: " + tmp);
						}
						else
						{
							tmp = botRight.GetCollisionPoint().DistanceTo(GlobalPosition);
						}
					}
					catch
					{
						tmp = botRight.GetCollisionPoint().DistanceTo(GlobalPosition);
					}
					//float tmp = topRight.GetCollisionPoint().DistanceTo(GlobalPosition);
					if (tmp < dist) dist = tmp;
				}

				try
				{
					portal port = (portal)GetCollider();
					RayCast3D after = port.Recalculate(this);
					if (!excepted.Contains(after)) excepted.Add(after);
					after.AddException(toCarry);

					if (after.IsColliding())
					{
						if (dist < GetCollisionPoint().DistanceTo(GlobalPosition) + after.GlobalPosition.DistanceTo(after.GetCollisionPoint()))
						{
							float diff = GetCollisionPoint().DistanceTo(GlobalPosition) + after.GetCollisionPoint().DistanceTo(after.GlobalPosition) - dist;
							if (diff > after.GetCollisionPoint().DistanceTo(after.GlobalPosition))
							{
								toCarry.GlobalPosition = GetCollisionPoint() + (GlobalPosition - GetCollisionPoint()).Normalized() * (diff - after.GetCollisionPoint().DistanceTo(after.GlobalPosition) + Math.Max(box.Scale.X, Math.Max(box.Scale.Y, box.Scale.Z)) / 2);
								shape.Scale = new Vector3(1f, 1f, 1f) * toCarry.GlobalPosition.DistanceTo(GlobalPosition) * mult;
								box.Scale = new Vector3(1f, 1f, 1f) * toCarry.GlobalPosition.DistanceTo(GlobalPosition) * mult;
							}
							else
							{
								toCarry.GlobalPosition = after.GetCollisionPoint() + (after.GlobalPosition - after.GetCollisionPoint()).Normalized() * (diff + Math.Max(box.Scale.X, Math.Max(box.Scale.Y, box.Scale.Z)) / 2);
								shape.Scale = new Vector3(1f, 1f, 1f) * (toCarry.GlobalPosition.DistanceTo(after.GlobalPosition) + GetCollisionPoint().DistanceTo(GlobalPosition)) * mult;
								box.Scale = new Vector3(1f, 1f, 1f) * (toCarry.GlobalPosition.DistanceTo(after.GlobalPosition) + GetCollisionPoint().DistanceTo(GlobalPosition)) * mult;
							}

						}
						else
						{
							//GD.Print("recalculated");
							toCarry.GlobalPosition = after.GetCollisionPoint() + (after.GlobalPosition - after.GetCollisionPoint()).Normalized() * (Math.Max(box.Scale.X, Math.Max(box.Scale.Y, box.Scale.Z)) / 2);
							shape.Scale = new Vector3(1f, 1f, 1f) * (toCarry.GlobalPosition.DistanceTo(after.GlobalPosition) + GlobalPosition.DistanceTo(GetCollisionPoint())) * mult;
							box.Scale = new Vector3(1f, 1f, 1f) * (toCarry.GlobalPosition.DistanceTo(after.GlobalPosition) + GlobalPosition.DistanceTo(GetCollisionPoint())) * mult;
							//GetNode<CsgSphere3D>("/root/Root/Pointer").GlobalPosition = after.GetCollisionPoint();
						}
					}
					else
					{
						if (dist < GetCollisionPoint().DistanceTo(GlobalPosition))
						{
							float diff = GetCollisionPoint().DistanceTo(GlobalPosition) - dist;
							toCarry.GlobalPosition = GetCollisionPoint() + (GlobalPosition - GetCollisionPoint()).Normalized() * (diff + Math.Max(box.Scale.X, Math.Max(box.Scale.Y, box.Scale.Z)) / 2);
							shape.Scale = new Vector3(1f, 1f, 1f) * toCarry.GlobalPosition.DistanceTo(GlobalPosition) * mult;
							box.Scale = new Vector3(1f, 1f, 1f) * toCarry.GlobalPosition.DistanceTo(GlobalPosition) * mult;
						}
						else
						{
							toCarry.GlobalPosition = GetCollisionPoint() + (GlobalPosition - GetCollisionPoint()).Normalized() * (Math.Max(box.Scale.X, Math.Max(box.Scale.Y, box.Scale.Z)) / 2);
							shape.Scale = new Vector3(1f, 1f, 1f) * toCarry.GlobalPosition.DistanceTo(GlobalPosition) * mult;
							box.Scale = new Vector3(1f, 1f, 1f) * toCarry.GlobalPosition.DistanceTo(GlobalPosition) * mult;
						}
					}
				}
				catch
				{
					if (dist < GetCollisionPoint().DistanceTo(GlobalPosition))
					{
						float diff = GetCollisionPoint().DistanceTo(GlobalPosition) - dist;
						toCarry.GlobalPosition = GetCollisionPoint() + (GlobalPosition - GetCollisionPoint()).Normalized() * (diff + Math.Max(box.Scale.X, Math.Max(box.Scale.Y, box.Scale.Z)) / 2);
						shape.Scale = new Vector3(1f, 1f, 1f) * toCarry.GlobalPosition.DistanceTo(GlobalPosition) * mult;
						box.Scale = new Vector3(1f, 1f, 1f) * toCarry.GlobalPosition.DistanceTo(GlobalPosition) * mult;
					}
					else
					{
						toCarry.GlobalPosition = GetCollisionPoint() + (GlobalPosition - GetCollisionPoint()).Normalized() * (Math.Max(box.Scale.X, Math.Max(box.Scale.Y, box.Scale.Z)) / 2);
						shape.Scale = new Vector3(1f, 1f, 1f) * toCarry.GlobalPosition.DistanceTo(GlobalPosition) * mult;
						box.Scale = new Vector3(1f, 1f, 1f) * toCarry.GlobalPosition.DistanceTo(GlobalPosition) * mult;
					}
				}
			}
			else
			{
				try
				{
					RigidBody3D coll = (RigidBody3D)GetCollider();
					if (sequencer.seqNum >= 7) GetNode<Sprite2D>("/root/Root/CanvasLayer/Control/Crosshair").Texture = (Texture2D)GD.Load("res://Textures/hand.png");
				}
				catch
				{
					try
					{
						portal port = (portal)GetCollider();
						try
						{
							RayCast3D after = port.Recalculate(this);
							if (!excepted.Contains(after)) excepted.Add(after);
							//after.AddException(toCarry);
							RigidBody3D body = (RigidBody3D)after.GetCollider();
							GetNode<Sprite2D>("/root/Root/CanvasLayer/Control/Crosshair").Texture = (Texture2D)GD.Load("res://Textures/hand.png");
						}
						catch
						{
							GetNode<Sprite2D>("/root/Root/CanvasLayer/Control/Crosshair").Texture = (Texture2D)GD.Load("res://Textures/dot.png");
						}
					}
					catch
					{
						try
						{
							sittable sittable = (sittable)GetCollider();
							if (GetCollisionPoint().DistanceTo(GlobalPosition) < 1.5 && sequencer.seqNum == 1) GetNode<Sprite2D>("/root/Root/CanvasLayer/Control/Crosshair").Texture = (Texture2D)GD.Load("res://Textures/interact.png");
							else GetNode<Sprite2D>("/root/Root/CanvasLayer/Control/Crosshair").Texture = (Texture2D)GD.Load("res://Textures/dot.png");
						}
						catch
						{
							try
							{
								locked_doubledoor locked_doubledoor = (locked_doubledoor)GetCollider();
								if ((GetCollisionPoint().DistanceTo(GlobalPosition) < 1.5 && sequencer.seqNum == 4) || (GetCollisionPoint().DistanceTo(GlobalPosition) < 2 && sequencer.seqNum == 11 && Input.IsActionPressed("ui_sprint"))) GetNode<Sprite2D>("/root/Root/CanvasLayer/Control/Crosshair").Texture = (Texture2D)GD.Load("res://Textures/interact.png");
								else GetNode<Sprite2D>("/root/Root/CanvasLayer/Control/Crosshair").Texture = (Texture2D)GD.Load("res://Textures/dot.png");
							}
							catch
							{
								try
								{
									locked_door locked_door = (locked_door)GetCollider();
									if (GetCollisionPoint().DistanceTo(GlobalPosition) < 1) GetNode<Sprite2D>("/root/Root/CanvasLayer/Control/Crosshair").Texture = (Texture2D)GD.Load("res://Textures/interact.png");
									else GetNode<Sprite2D>("/root/Root/CanvasLayer/Control/Crosshair").Texture = (Texture2D)GD.Load("res://Textures/dot.png");
								}
								catch
								{
									try
									{
										math_door math_door = (math_door)GetCollider();
										if (GetCollisionPoint().DistanceTo(GlobalPosition) < 1 && (sequencer.seqNum == 5 | sequencer.seqNum == 8)) GetNode<Sprite2D>("/root/Root/CanvasLayer/Control/Crosshair").Texture = (Texture2D)GD.Load("res://Textures/interact.png");
										else GetNode<Sprite2D>("/root/Root/CanvasLayer/Control/Crosshair").Texture = (Texture2D)GD.Load("res://Textures/dot.png");
									}
									catch
									{
										try
										{
											manipulator manipulator = (manipulator)GetCollider();
											if (GetCollisionPoint().DistanceTo(GlobalPosition) < 1 && sequencer.seqNum == 6) GetNode<Sprite2D>("/root/Root/CanvasLayer/Control/Crosshair").Texture = (Texture2D)GD.Load("res://Textures/interact.png");
											else GetNode<Sprite2D>("/root/Root/CanvasLayer/Control/Crosshair").Texture = (Texture2D)GD.Load("res://Textures/dot.png");
										}
										catch
										{
											try
											{
												ghost_collider ghost_collider = (ghost_collider)GetCollider();
												GetNode<Sprite2D>("/root/Root/CanvasLayer/Control/Crosshair").Texture = (Texture2D)GD.Load("res://Textures/hand.png");
											}
											catch
											{
												try
												{
													toilet_door toilet_door = (toilet_door)GetCollider();
													if (GetCollisionPoint().DistanceTo(GlobalPosition) < 1.5 && sequencer.seqNum == 12) GetNode<Sprite2D>("/root/Root/CanvasLayer/Control/Crosshair").Texture = (Texture2D)GD.Load("res://Textures/interact.png");
													else GetNode<Sprite2D>("/root/Root/CanvasLayer/Control/Crosshair").Texture = (Texture2D)GD.Load("res://Textures/dot.png");
												}
												catch
												{
													try
													{
														conv_door conv_door = (conv_door)GetCollider();
														if (GetCollisionPoint().DistanceTo(GlobalPosition) < 1.5 && sequencer.seqNum < 4) GetNode<Sprite2D>("/root/Root/CanvasLayer/Control/Crosshair").Texture = (Texture2D)GD.Load("res://Textures/interact.png");
														else GetNode<Sprite2D>("/root/Root/CanvasLayer/Control/Crosshair").Texture = (Texture2D)GD.Load("res://Textures/dot.png");
													}
													catch
													{
														try
														{
															breakers breakers = (breakers)GetCollider();
															if (GetCollisionPoint().DistanceTo(GlobalPosition) < 1.5 && sequencer.seqNum == 13) GetNode<Sprite2D>("/root/Root/CanvasLayer/Control/Crosshair").Texture = (Texture2D)GD.Load("res://Textures/interact.png");
															else GetNode<Sprite2D>("/root/Root/CanvasLayer/Control/Crosshair").Texture = (Texture2D)GD.Load("res://Textures/dot.png");
														}
														catch
														{
															GetNode<Sprite2D>("/root/Root/CanvasLayer/Control/Crosshair").Texture = (Texture2D)GD.Load("res://Textures/dot.png");
														}
													}
												}
											}
										}
									}
								}
							}
						}

					}

				}

			}
		}
		else if (!carry)
		{
			GetNode<Sprite2D>("/root/Root/CanvasLayer/Control/Crosshair").Texture = (Texture2D)GD.Load("res://Textures/dot.png");
		}
	}

	public void Grab()
	{
		if (carry)
		{
			bool inPlayer = false;
			foreach (Node3D collBod in toCarry.GetCollidingBodies())
			{
				if (collBod.Name.Equals("CharacterBody3D"))
				{
					inPlayer = true;
					break;
				}
			}
			if (!inPlayer)
			{
				carry = false;
				toCarry.Freeze = false;
				Except(false);
				toCarry.ContactMonitor = false;
				if (after != null) after.ClearExceptions();
			}
		}
		else if (IsColliding())
		{
			try
			{
				apple coll = (apple)GetCollider();
				toCarry = coll;
				toCarry.Freeze = true;
				Except(true);
				carry = true;
				toCarry.ContactMonitor = true;
				GetNode<Sprite2D>("/root/Root/CanvasLayer/Control/Crosshair").Texture = (Texture2D)GD.Load("res://Textures/hand_hold.png");
			}
			catch
			{ }
		}
	}

	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventKey eventKeyboard && eventKeyboard.Keycode == Key.E && eventKeyboard.Pressed && IsColliding())
		{
			try
			{
				sittable sittable = (sittable)GetCollider();
				if (sequencer.seqNum == 1 && GetCollisionPoint().DistanceTo(GlobalPosition) < 2)
				{
					sequencer.seqNum++;
					sequencer.nextSeq = true;
				}
			}
			catch
			{
				try
				{
					locked_doubledoor locked_doubledoor = (locked_doubledoor)GetCollider();
					if ((sequencer.seqNum == 4 && GetCollisionPoint().DistanceTo(GlobalPosition) < 1.5) || (sequencer.seqNum == 11 && GetCollisionPoint().DistanceTo(GlobalPosition) < 2 && Input.IsActionPressed("ui_sprint")))
					{
						sequencer.seqNum++;
						sequencer.nextSeq = true;
					}
				}
				catch
				{
					try
					{
						locked_door locked_door = (locked_door)GetCollider();
						if (GetCollisionPoint().DistanceTo(GlobalPosition) < 1)
						{
							AudioStreamPlayer allPurpose = GetNode<AudioStreamPlayer>("/root/Root/CharacterBody3D/AllPurpose");
							allPurpose.Stream = (AudioStream)GD.Load("res://Audio/locked.mp3");
							allPurpose.Play();
						}
					}
					catch
					{
						try
						{
							math_door math_door = (math_door)GetCollider();
							if (sequencer.seqNum == 5 && GetCollisionPoint().DistanceTo(GlobalPosition) < 1)
							{
								sequencer.seqNum++;
								sequencer.nextSeq = true;
							}
							else if (sequencer.seqNum == 8 && GetCollisionPoint().DistanceTo(GlobalPosition) < 1)
							{
								AudioStreamPlayer allPurpose = GetNode<AudioStreamPlayer>("/root/Root/CharacterBody3D/AllPurpose");
								allPurpose.Stream = (AudioStream)GD.Load("res://Audio/locked.mp3");
								allPurpose.Play();
								GetNode<informative>("/root/Root/CanvasLayer/Informative").Inform("It got stuck when I slammed it");
							}
						}
						catch
						{
							try
							{
								manipulator manipulator = (manipulator)GetCollider();
								if (sequencer.seqNum == 6 && GetCollisionPoint().DistanceTo(GlobalPosition) < 1)
								{
									manipulator.UseCollision = false;
									sequencer.seqNum++;
									sequencer.nextSeq = true;
								}
							}
							catch
							{
								try
								{
									toilet_door toilet_door = (toilet_door)GetCollider();
									if (sequencer.seqNum == 12 && GetCollisionPoint().DistanceTo(GlobalPosition) < 1.5)
									{
										sequencer.seqNum++;
										sequencer.nextSeq = true;
									}
								}
								catch
								{
									try
									{
										conv_door conv_door = (conv_door)GetCollider();
										if (sequencer.seqNum < 4 && GetCollisionPoint().DistanceTo(GlobalPosition) < 1.5)
										{
											if (conv_door.open)
											{
												conv_door.GetNode<AnimationPlayer>("AnimationPlayer").Play("close");
												conv_door.open = false;
											}
											else
											{
												conv_door.GetNode<AnimationPlayer>("AnimationPlayer").Play("open");
												conv_door.open = true;
											}
										}
									}
									catch
									{
										try
										{
											breakers breakers = (breakers)GetCollider();
											if (sequencer.seqNum == 13)
											{
												sequencer.seqNum++;
												sequencer.nextSeq = true;
											}
										}
										catch
										{

										}
									}
								}
							}
						}
					}
				}
			}
		}

		if (@event is InputEventMouseButton eventMouseButton && eventMouseButton.ButtonIndex.Equals(MouseButton.Right) && eventMouseButton.Pressed && sequencer.seqNum >= 7 && allow)
		{
			/*if (fc) {
				DisplayServer.WindowSetMode(DisplayServer.WindowMode.Fullscreen);
				fc = false;
			}*/
			if (carry)
			{
				bool inPlayer = false;
				foreach (Node3D collBod in toCarry.GetCollidingBodies())
				{
					if (collBod.Name.Equals("CharacterBody3D"))
					{
						inPlayer = true;
						break;
					}
				}
				if (!inPlayer)
				{
					carry = false;
					toCarry.Freeze = false;
					Except(false);
					toCarry.ContactMonitor = false;
					if (after != null) after.ClearExceptions();
				}
			}
			else if (IsColliding())
			{
				try
				{
					math_door coll = (math_door)GetCollider();
					if (sequencer.seqNum >= 8)
					{
						coll.Freeze = false;
						toCarry = coll;
						toCarry.Freeze = true;
						Except(true);
						carry = true;
						toCarry.ContactMonitor = true;
						GetNode<Sprite2D>("/root/Root/CanvasLayer/Control/Crosshair").Texture = (Texture2D)GD.Load("res://Textures/hand_hold.png");
					}
				}
				catch
				{
					try
					{
						apple coll = (apple)GetCollider();
						if (sequencer.seqNum == 7)
						{
							allow = false;
							sequencer.seqNum++;
							sequencer.nextSeq = true;
						}
						else if (allow)
						{
							toCarry = coll;
							toCarry.Freeze = true;
							Except(true);
							carry = true;
							toCarry.ContactMonitor = true;
							GetNode<Sprite2D>("/root/Root/CanvasLayer/Control/Crosshair").Texture = (Texture2D)GD.Load("res://Textures/hand_hold.png");
						}
					}
					catch
					{
						try
						{
							RigidBody3D coll = (RigidBody3D)GetCollider();
							toCarry = coll;
							toCarry.Freeze = true;
							Except(true);
							carry = true;
							toCarry.ContactMonitor = true;
							GetNode<Sprite2D>("/root/Root/CanvasLayer/Control/Crosshair").Texture = (Texture2D)GD.Load("res://Textures/hand_hold.png");
						}
						catch
						{
							try
							{
								portal port = (portal)GetCollider();
								try
								{
									after = port.Recalculate(this);
									if (!excepted.Contains(after)) excepted.Add(after);
									RigidBody3D coll = (RigidBody3D)after.GetCollider();
									toCarry = coll;
									after.AddException(toCarry);
									toCarry.Freeze = true;
									Except(true);
									carry = true;
									toCarry.ContactMonitor = true;
									GetNode<Sprite2D>("/root/Root/CanvasLayer/Control/Crosshair").Texture = (Texture2D)GD.Load("res://Textures/hand_hold.png");
								}
								catch
								{

								}
							}
							catch
							{
								try
								{
									ghost_collider ghost_collider = (ghost_collider)GetCollider();
									if (sequencer.seqNum == 10 || sequencer.seqNum == 14)
									{
										sequencer.seqNum++;
										sequencer.nextSeq = true;
									}
								}
								catch
								{

								}
							}
						}
					}
				}
			}
		}
	}
}
