using Godot;
using System;

public class Ball : Sprite
{
    public Vector2 screen;
    private bool start = false;
    private Sprite ball;
    private Timer StartTimer;
    private Vector2 pos;
    private Vector2 mov;
    
    [Export]
    public int speed = 100;

    [Export]
    public int acc = 50;


public void GameStart()
{
    GetParent().GetNode<Timer>("StartTimer").Start();
    ball.Position = new Vector2(960,520);

}

public void hit(Area2D test)
{
    pos.x = Mathf.Clamp(pos.x, 148, 1772);

    //mov.x = Math.Abs(mov.x);
    //if(pos.x > 450)
    //{
    //    mov.x = 0 - mov.x;
    //}

    if(pos.x >= 960)
    {
        mov = pos - GetParent().GetNode<Sprite>("P1").Position;
        GD.Print(mov);
    }else{
        mov = pos - GetParent().GetNode<Sprite>("P2").Position;
        GD.Print(mov);
    }
    speed += acc;

}
    
public override void _Ready()
{
    ball = GetParent().GetNode<Sprite>("Ball");
    StartTimer = GetParent().GetNode<Timer>("StartTimer");
}



public override void _Process(float delta)
{
    screen = GetViewport().Size;
    pos = ball.Position;
    double testr = GD.RandRange(1,2);

    if(start)
    {
        mov = mov.Normalized() * speed;
        pos += mov * delta;


        if(30 >= pos.y || pos.y >= screen.y - 30)
        {
            pos.y = Mathf.Clamp(pos.y, 30, screen.y -30);
            mov.y -= mov.y * 2;
        }
    }
    ball.Position = pos;

}

public void _on_Start_button_down()
{
    GetParent().GetNode<Button>("Button").Hide();
    GameStart();
}

public void _on_StartTimer_timeout()
{
    start = true;
    mov.x = (float)GD.RandRange(-1,1);
    mov.y = (float)GD.RandRange(-1,1);
}

}
