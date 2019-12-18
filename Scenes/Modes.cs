using Godot;
using Godot.Collections;

public class Modes : Node2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
   
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        //animation.CurrentAnimation = "Mode_1";
    }

  // Called every frame. 'delta' is the elapsed time since the previous frame.
  public override void _Process(float delta)
  {
        Animation Attacked = new Animation();

        Attacked.AddTrack(Animation.TrackType.Value);
        Attacked.TrackSetPath(0, "AnimationPlayer/1:modulate");
        Attacked.TrackInsertKey(0, 0.0f, Colors.DarkRed,0);
        AnimationPlayer PlayerAnimation = (GetNode("AnimationPlayer") as AnimationPlayer);
        PlayerAnimation.AddAnimation("Attacked", Attacked);


  }
}
