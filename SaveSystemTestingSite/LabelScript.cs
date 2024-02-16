using Godot;
using System;

public partial class LabelScript : Label
{
	[Export] public int count;
	[Export] public int intId;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Text = count.ToString();
	}

	public void Add()
	{
		count++;
	}
	public void Subtract()
	{
		count--;
	}
}
