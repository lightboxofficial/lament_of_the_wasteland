using Godot;

public partial class PlayerMovement : CharacterBody3D
{
	[Export]
	public float Speed { get; set; } = 5.0f;

	[Export]
	public float JumpForce { get; set; } = 4.9f;

	[Export]
	public float Gravity { get; set; } = 9.8f;

	private Vector3 _velocity = Vector3.Zero;
	private const float FloorSnapLength = 0.2f;
	public override void _PhysicsProcess(double delta)
	{
		HandleMovement();
		ApplyGravity(delta);
		HandleJump();
		
		// 应用计算后的速度并移动角色
		Velocity = _velocity;
		MoveAndSlide();
	}
	
	/// <summary>
	/// 处理角色的水平移动
	/// </summary>
	private void HandleMovement()
	{
		var direction = GetInputDirection();
		
		// 根据摄像机方向旋转移动向量
		direction = direction.Rotated(Vector3.Up, Rotation.Y);
		
		// 标准化方向向量，避免对角线移动速度更快
		if (direction != Vector3.Zero)
		{
			direction = direction.Normalized();
		}
		
		// 应用水平移动
		_velocity.X = direction.X * Speed;
		_velocity.Z = direction.Z * Speed;
	}
	
	/// <summary>
	/// 获取用户输入方向
	/// </summary>
	private Vector3 GetInputDirection()
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
		
		return direction;
	}
	
	/// <summary>
	/// 处理跳跃逻辑
	/// </summary>
	private void HandleJump()
	{
		if (IsOnFloor() && Input.IsActionJustPressed("jump"))
		{
			_velocity.Y = JumpForce;
		}
	}
	
	/// <summary>
	/// 应用重力
	/// </summary>
	private void ApplyGravity(double delta)
	{
		if (!IsOnFloor())
		{
			_velocity.Y -= Gravity * (float)delta;
		}
		else if (_velocity.Y < 0)
		{
			_velocity.Y = -FloorSnapLength; // 轻微向下的力以保持与地面接触
		}
	}

}
