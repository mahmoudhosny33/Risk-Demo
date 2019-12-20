using Godot;
using System;

public class optionpage : Node2D
{
	
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    public AudioStreamPlayer2D A = new AudioStreamPlayer2D();
    public CheckButton C = new CheckButton();
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        A.Play(0);
    }
    
  // Called every frame. 'delta' is the elapsed time since the previous frame.
   public override void _Process(float delta)
   {
     
   }
    public void _on_CheckButton_pressed(string value)
    {
        if (value == "ON")
            A.Stop();
            

    }

    public void _on_Button_pressed()
	{
	   GetTree().ChangeScene("res://menugame.tscn");
	}

}














