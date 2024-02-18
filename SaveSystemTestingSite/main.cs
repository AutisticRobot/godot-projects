using Godot;
using Godot.Collections;
using System;
using System.IO;

public partial class main : Node2D
{
	[Export] string filePath;
	[Export] string fileName;

	public int loadState = 0; //checks with main script to see if it should load or just wait;
	public Dictionary data = new();
	private string loadedData;


	string globalPath;
	string dirPath;
	string path;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		globalPath = ProjectSettings.GlobalizePath("user://");
	
	}


	public void Save()
	{
		dirPath = Path.Join(globalPath, filePath);
		path = Path.Join(dirPath, fileName);

		if(!Directory.Exists(dirPath))
		{
			Directory.CreateDirectory(dirPath);
		}

		GD.Print(path);
		string json = Json.Stringify(data);

		try
		{
			File.WriteAllText(path, json);
		}
		catch (System.Exception e)
		{
			GD.Print(e);
		}

	}
	public void Load()
	{
		dirPath = Path.Join(globalPath, filePath);
		path = Path.Join(dirPath, fileName);

		if(!File.Exists(path)) return;

		GD.Print(path);

		try
		{
			loadedData = File.ReadAllText(path);
		}
		catch (System.Exception e)
		{
			GD.Print(e);
		}

		Json jsonData = new Json();

		Error error = jsonData.Parse(loadedData);

		if(error != Error.Ok)
		{
			GD.Print(error);
			return;
		}

		data = (Dictionary)jsonData.Data;


		loadState++;
	}
}
