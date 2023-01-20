using Godot;
using System;

public class Player : Node
{
	[Export]
	public int Speed = 500;

	private Sprite p1;
	private Sprite p2;


		public Vector2 screen;

	public override void _Ready()
	{

screen = GetViewport().Size;
		p1 = GetNode<Sprite>("P1");
	   p2 = GetNode<Sprite>("P2") ;
	}


public Vector2 c2s(Vector2 var)
	{
		var.x = Mathf.Clamp(var.x, 0, screen.x);
		var.y = Mathf.Clamp(var.y, 120, screen.y - 120);

		return(var);

	}



// Called every frame. 'delta' is the elapsed time since the previous frame.
public override void _Process(float delta)
{

	var velocity = Vector2.Zero; //p1
	var velocity2 = Vector2.Zero; //p2

	if (Input.IsActionPressed("p1_up"))
	{
		velocity.y -= Speed * delta;
	}
	if (Input.IsActionPressed("p1_down"))
	{
		velocity.y += Speed * delta;
	}
	if (Input.IsActionPressed("p2_up"))
	{
		velocity2.y -= Speed * delta;
	}
	if (Input.IsActionPressed("p2_down"))
	{
		velocity2.y += Speed * delta;
	}

	p1.Position += velocity;
	p2.Position += velocity2;

	p1.Position = c2s(p1.Position);
	p2.Position = c2s(p2.Position);

}
}
