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


	string globalPath = "user://";
	string dirPath;
	string path;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	
	}


	public void Save()
	{
		dirPath = Path.Join(globalPath, filePath);
		path = Path.Join(dirPath, fileName);

		string json = Json.Stringify(data);
/* working using System.IO.File class
		if(!Directory.Exists(dirPath))
		{
			Directory.CreateDirectory(dirPath);
		}
		GD.Print(path);

		try
		{
			File.WriteAllText(path, json);
		}
		catch (System.Exception e)
		{
			GD.Print(e);
		}
		*/
		using Godot.FileAccess file = Godot.FileAccess.Open(path, Godot.FileAccess.ModeFlags.Write);
		file.StoreString(json);

	}
	public void Load()
	{
		dirPath = Path.Join(globalPath, filePath);
		path = Path.Join(dirPath, fileName);

/* working using System.IO.File class
		if(!File.Exists(path)) return;

		GD.Print(path);

		try
		{
			loadedData = File.ReadAllText(path);
		}
		catch (System.Exception e)//not actuialy shur if i care about this as only matters if read/write acces blocked to the save file;
		{
			GD.Print(e);
		}

*/
		using Godot.FileAccess file = Godot.FileAccess.Open(path, Godot.FileAccess.ModeFlags.Read);
		loadedData = file.GetAsText();

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
