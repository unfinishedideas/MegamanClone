using Godot;
using System;

public class Player : KinematicBody2D
{
    // System.Diagnostics.Debug.WriteLine("this is a console log");

    // speed variables
    public const float topSpeed = 150.0f;
    public const float accel = 30.0f;
    public const float deccel = 40.0f;
    public float currentAccel = 0.0f;
    // airtime variables
    public const float jumpForce = 250.0f;
    public const float gravity = 700.0f;
    public const float maxFallSpeed = 4000.0f;
    public const bool isOnFloor = true;
    // animation variables
    public bool facingRight = true;
    // attacking variables
    public bool canFire = true;
    // misc variables
    public float muzzleDistanceAmt = 15f;


    Vector2 velocity;

    // Called when the node enters the scene tree for the first time.
    // public override void _Ready() {      }

    public override void _Process(float delta)
	{
        MoveInputCheck();
        ActionInputCheck();
        UpdateAnimation();
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
		// Jumping
		if (Input.IsActionPressed("jump") && IsOnFloor()) {
			velocity.y -= jumpForce;
		}

		// Moving left and right
		if (Input.IsActionPressed("moveRight")) {
			if (!facingRight) {
				velocity.x = 0;
				facingRight = true;
                muzzleDistanceAmt = Math.Abs(muzzleDistanceAmt);
                Position2D muzzleSpot = (Position2D)GetNode("Muzzle");
                muzzleSpot.Position = new Vector2(muzzleDistanceAmt, 0);
			}
			
			currentAccel = accel; // eventually write function that adds curve instead of just assigning?

			if (velocity.x < topSpeed) {
				velocity.x += currentAccel;
			}
		} else if (Input.IsActionPressed("moveLeft")) {
			if (facingRight) {
				velocity.x = 0;
				facingRight = false;
                muzzleDistanceAmt *= -1;
                Position2D muzzleSpot = (Position2D)GetNode("Muzzle");
                muzzleSpot.Position = new Vector2(muzzleDistanceAmt, 0);
			}
			
			currentAccel = accel; // eventually write function that adds curve instead of just assigning?
			
			if (velocity.x > -topSpeed) {
				velocity.x -= currentAccel;
			}
		} else {
			VelocityToZero(1f);
		}
    }

    public void VelocityToZero(float modifier)
	{
		currentAccel = deccel * modifier; // eventually write function that adds curve instead of just assigning?

		if (velocity.x < 0) {
			velocity.x += currentAccel;
			if (velocity.x > 0) {
				velocity.x = 0;
			}
		} else if (velocity.x > 0) {
			velocity.x -= currentAccel;
			if (velocity.x < 0) {
				velocity.x = 0;
			}
		}
	}

    void ActionInputCheck()
    {
        Timer atkCooldownTimer = (Timer)GetNode("timers/atk_cooldown");

        if (atkCooldownTimer.IsStopped()) {
			canFire = true;
		}

        if (Input.IsActionPressed("fire")  && facingRight == true && canFire == true) {
            System.Diagnostics.Debug.WriteLine("BOOM! Right");
            canFire = false;
            atkCooldownTimer.Start();
        } else if (Input.IsActionPressed("fire")  && facingRight == false && canFire == true) {
            System.Diagnostics.Debug.WriteLine("BOOM! Left");
            canFire = false;
            atkCooldownTimer.Start();
        }
    }

    void UpdateAnimation()
    {
        AnimatedSprite sprite = (AnimatedSprite)GetNode("AnimatedSprite");

        if (velocity.x > 0) {
            sprite.Play("runRight");
        } else if (velocity.x < 0) {
            sprite.Play("runLeft");
        } else {
            if (facingRight == true) {
                sprite.Play("idleRight");
            } else {
                sprite.Play("idleLeft");
            }
        }
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
