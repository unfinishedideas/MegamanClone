using Godot;
using System;

public class Player : KinematicBody2D
{
    // System.Diagnostics.Debug.WriteLine("this is a console log");

    // speed variables
    public const float topSpeed = 100.0f;
    public const float accel = 6.0f;
    public const float deccel = 4.0f;
    public const float currentAccel = 0.0f;
    // airtime variables
    public const float jumpForce = 80.0f;
    public const float gravity = 80.0f;
    public const float maxFallSpeed = 1000.0f;
    // animation variables
    public bool facingRight = true;
    // attacking variables
    public bool canFire = true;


    Vector2 velocity;

    // Called when the node enters the scene tree for the first time.
    // public override void _Ready() {      }

    public override void _Process(float delta)
	{
        MoveInputCheck();
        ActionInputCheck();
	}

	
	public override void _PhysicsProcess(float delta)
	{
        velocity = MoveAndSlide(velocity, new Vector2(0, -1));
		
		//	--- gravity --- //
		if (velocity.y < maxFallSpeed) {
			velocity.y += gravity * delta;
		}
	}

    void MoveInputCheck()
    {

    }

    void ActionInputCheck()
    {

    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
