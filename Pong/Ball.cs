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
    private int Sc1 = 0;
    private Label p1Score;
    private int Sc2 = 0;
    private Label p2Score;
    
    [Export]
    public int speed = 100;

    [Export]
    public int acc = 50;

    private int bSpeed;


public void GameStart()
{
    start = false;
    ball.Position = new Vector2(960,520);
    pos = new Vector2(960,520);
    speed = bSpeed;
    GetParent().GetNode<Timer>("StartTimer").Start();

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
    bSpeed = speed;
    p1Score = GetParent().GetNode<Label>("P1score");
    p2Score = GetParent().GetNode<Label>("P2score");
    ball = GetParent().GetNode<Sprite>("Ball");
    StartTimer = GetParent().GetNode<Timer>("StartTimer");
}



public override void _Process(float delta)
{
    screen = GetViewport().Size;
    pos = ball.Position;
    double testr = GD.RandRange(1,2);

    p1Score.Text = Convert.ToString(Sc1);
    p2Score.Text = Convert.ToString(Sc2);

    if(start)
    {
        mov = mov.Normalized() * speed;
        pos += mov * delta;


        if(30 >= pos.y || pos.y >= screen.y - 30)
        {
            pos.y = Mathf.Clamp(pos.y, 30, screen.y -30);
            mov.y -= mov.y * 2;
        }

        if(-30 >= pos.x || pos.x >= screen.x + 30)
        {
            if(pos.x <= screen.x / 2){
                Sc1++;
            }else{
                Sc2++;
            }
            GameStart();
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
