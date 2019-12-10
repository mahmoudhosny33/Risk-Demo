using Godot;
using System;

public class Game : Node2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Sprite Tile = (Sprite)GetNode("risk").GetNode("Australia-L");
        Tile.Modulate = Color.ColorN("red", 1);

        Sprite Tile2 = (Sprite)GetNode("risk").GetNode("Australia-R");
        Tile2.Modulate = Color.ColorN("red", 1);

    }

    private void _on_Area2D_input_event(object viewport, object @event, int shape_idx)
    {
        Sprite Tile2 = (Sprite)GetNode("risk").GetNode("Australia-R");
        Tile2.Modulate = Color.ColorN("lightcoral", 1);
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}






