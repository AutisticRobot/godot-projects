using Godot;
using System;

public partial class LabelScript : Label
{
	[Export] public int count;
	[Export] public string intId;

	private main man;
	private int loadState = 0; //checks with main script to see if it should load or just wait;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		man = GetParent<main>();

		if(!man.data.ContainsKey(intId))
		{
			man.data.Add(intId, count);
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(man.loadState > loadState)
		{
			count = (int)man.data[intId];// it is posible, with save editing, to have the data entry not exist;
			loadState++;
		}
		man.data[intId] = count;

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
