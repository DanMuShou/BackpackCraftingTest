using Godot;

public enum EAnimatedName
{
    IdleUp,
    IdleDown,
    IdleSize,
    WalkUp,
    WalkDown,
    WalkSize,
}

public partial class BaseEntity : CharacterBody2D
{
    [Export] public int MoveSpeed = 200;
    [Export] private AnimatedSprite2D _animated;

    private Vector2 _lastDir;

    public override void _PhysicsProcess(double delta)
    {
        var inputDirection = Input.GetVector(
            "MoveLeft", "MoveRight", "MoveUp", "MoveDown").Normalized();
        Velocity = inputDirection * MoveSpeed;
        MoveAndSlide();

        UpdateAnimation(inputDirection);
    }

    private void UpdateAnimation(Vector2 direction)
    {
        if (direction != Vector2.Zero)
        {
            if (direction.X != 0)
            {
                _animated.Play(EAnimatedName.IdleSize.ToString());
                _animated.FlipH = direction.X < 0;
            }
            else if (direction.Y != 0)
            {
                _animated.Play(direction.Y < 0 ? EAnimatedName.IdleUp.ToString() : EAnimatedName.IdleDown.ToString());
            }

            _lastDir = direction;
        }
        else if (_lastDir != Vector2.Zero)
        {
            if (_lastDir.X != 0)
            {
                _animated.Play(EAnimatedName.IdleSize.ToString());
                _animated.FlipH = _lastDir.X < 0;
            }
            else if (_lastDir.Y != 0)
            {
                _animated.Play(_lastDir.Y < 0 ? EAnimatedName.IdleUp.ToString() : EAnimatedName.IdleDown.ToString());
            }

            _lastDir = Vector2.Zero;
        }
    }
}