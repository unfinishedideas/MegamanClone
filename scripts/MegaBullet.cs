using Godot;
using System;

public class MegaBullet : Area2D
{

    public int speed = 10;
    public int damage = 1;
    public float lifetime = 1;

    Vector2 velocity;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

    public override void Start(Vector2 _position, Vector2 _direction) 
    {
        Timer lifeTimer = (Timer)GetNode("Lifetime");
        lifeTimer.wait_time = lifetime;
        velocity = _position * speed;
    }

    public override void Process(float delta)
    {
        Position.x *= velocity;
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
