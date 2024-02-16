using Godot;
using Godot.Collections;
using System;

public partial class main : Node2D
{
	public Dictionary data = new();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		data.Add("label0", 0);
		data.Add("text", "Text");

		Dictionary test = new()
        {
            { "testData", 5 }
        };

		data.Add("data", test);

		string json = Json.Stringify(data);

		GD.Print(json);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void Save()
	{

	}
	public void Load()
	{

	}
}
