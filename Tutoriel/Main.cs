using Godot;
using System;

public class Main : Node
{
    
#pragma warning disable 649
    // We assign this in the editor, so we don't need the warning about not being assigned.
    [Export]
    public PackedScene MobScene;
#pragma warning restore 649

    public int Score;

    public override void _Ready()
    {
        GetNode<HUD>("HUD").Readyl();
        Score = 0;
    }
    

    public void gameover()
    {
        GetNode<HUD>("HUD").ShowGameOver();
        GetNode<Timer>("MobTimer").Stop();
        GetNode<Timer>("ScoreTimer").Stop();
    }
    
    public void NewGame()
    {
        Score = 0;
    
        var player = GetNode<Player>("Player");
        var startPosition = GetNode<Position2D>("StartPosition");
        player.Start(startPosition.Position);
    
        GetNode<Timer>("StartTimer").Start();

        var hud = GetNode<HUD>("HUD");
        hud.UpdateScore(Score);
        hud.ShowMessage("Get Ready!");

        GetTree().CallGroup("mobs", "queue_free");
    }

    public void onScoreTimertimeout()
    {
        Score++;
        GetNode<HUD>("HUD").UpdateScore(Score);
    }

    public void onStartTimertimeout()
    {
        GetNode<Timer>("MobTimer").Start();
        GetNode<Timer>("ScoreTimer").Start();
    }

    public void onMobTimertimeout()
    {
        // Note: Normally it is best to use explicit types rather than the `var`
        // keyword. However, var is acceptable to use here because the types are
        // obviously Mob and PathFollow2D, since they appear later on the line.

        // Create a new instance of the Mob scene.
        var mob = (Mob)MobScene.Instance();

        // Choose a random location on Path2D.
        var mobSpawnLocation = GetNode<PathFollow2D>("MobPath/MobSpawnLocation");
        mobSpawnLocation.Offset = GD.Randi();

        // Set the mob's direction perpendicular to the path direction.
        float direction = mobSpawnLocation.Rotation + Mathf.Pi / 2;

        // Set the mob's position to a random location.
        mob.Position = mobSpawnLocation.Position;

        // Add some randomness to the direction.
        direction += (float)GD.RandRange(-Mathf.Pi / 4, Mathf.Pi / 4);
        mob.Rotation = direction;

        // Choose the velocity.
        var velocity = new Vector2((float)GD.RandRange(150.0, 250.0), 0);
        mob.LinearVelocity = velocity.Rotated(direction);

        // Spawn the mob by adding it to the Main scene.
        AddChild(mob);
    }
}
