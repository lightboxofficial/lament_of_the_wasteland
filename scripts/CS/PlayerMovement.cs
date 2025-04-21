using Godot;

public partial class PlayerMovement : CharacterBody3D
{
	[Export]
	public float Speed { get; set; } = 5.0f;
	[Export]
	public float JumpForce { get; set; } = 4.9f;
	[Export]
	public float gravity { get; set; } = 9.8f;
	private Vector3 _velocity = Vector3.Zero;
	public override void _PhysicsProcess(double delta)
	{
		var direction = Vector3.Zero;
		// 使用WASD控制移动
		if (Input.IsActionPressed("move_forward"))
		{
			direction.Z -= 1;
		}
		if (Input.IsActionPressed("move_backward"))
		{
			direction.Z += 1;
		}
		if (Input.IsActionPressed("move_left"))
		{
			direction.X -= 1;
		}
		if (Input.IsActionPressed("move_right"))
		{
			direction.X += 1;
		}
		
		// 根据摄像机方向旋转移动向量
		direction = direction.Rotated(Vector3.Up, Rotation.Y).Normalized();
		if (direction != Vector3.Zero)
		{
			direction = direction.Normalized();
		}
		_velocity.X = direction.X * Speed;
		_velocity.Z = direction.Z * Speed;
		if (IsOnFloor())
		{
			if (Input.IsActionJustPressed("jump"))
			{
				_velocity.Y = JumpForce;
			}
		}
		else
		{
			_velocity.Y -= gravity * (float)delta;
		}
		Velocity = _velocity;
		MoveAndSlide();
	}

}
