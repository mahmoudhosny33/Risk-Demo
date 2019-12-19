using Godot;
using System.Collections.Generic;
using Godot.Collections;

namespace RiskGame.Scripts
{
    public partial class Game
    {
            public void __initZoom()
            {

                try
                {
                    AnimationPlayer ZCamera = GetNode("ZCamera") as AnimationPlayer;
                    Animation animation = ZCamera.GetAnimation("Zoom");

                    int idx = 0;
                    float value = 1.0f;
                    for (int i = 0; i < 22; i++)
                    {
                        animation.TrackSetKeyValue(idx, i, new Vector2(value, value));
                        GD.Print(animation.TrackGetKeyValue(idx, i));
                        value -= (1 / 60f);
                    }
                }
                catch
                {
                    GD.Print("NOt Found");
                }

            }
            public void Zoom(Vector2 pos, string name)
            {

                AnimationPlayer ZCamera = GetNode("ZCamera") as AnimationPlayer;
                Animation animation = ZCamera.GetAnimation("Zoom");
                Vector2 camera_pos = (GetNode("ZCamera/Camera2D") as Camera2D).Position;
                float distance = pos.DistanceTo(camera_pos);
                Vector2 speed = new Vector2(distance / 22, distance / 22);
                Vector2 dir = pos.DirectionTo(camera_pos);

                Vector2 newPos = camera_pos;

                for (int i = 0; i < 22; i++)
                {
                    animation.TrackSetKeyValue(1, i, newPos);
                    newPos -= (dir * speed);
                }
                ZCamera.Play("Zoom");
            }

            public Animation AddAnimation(int name)
            {
                Animation animation = new Animation();
                int index = 0;
                foreach (int neighbor in map[name])
                {
                    animation.AddTrack(Animation.TrackType.Value);
                    animation.TrackSetPath(index, $"{(neighbor)}:modulate");

                    animation.SetLength(2.4f);
                    animation.SetStep(0.6f);
                    animation.SetLoop(true);
                    animation.TrackInsertKey(index, 0, new Color());
                    //animation.TrackSetKeyValue(index, 0,countries[neighbor].owner.color);// Owner Color Not Found

                    animation.TrackInsertKey(index, 0.6f, new Color());
                    animation.TrackSetKeyValue(index, 1, Colors.Black);

                    animation.TrackInsertKey(index, 1.2f, new Color());
                    //  animation.TrackSetKeyValue(index, 2, countries[neighbor].owner.color);


                    animation.TrackInsertKey(index, 1.8f, new Color());
                    animation.TrackSetKeyValue(index, 3, Colors.Black);

                    animation.TrackInsertKey(index, 2.4f, new Color());
                    // animation.TrackSetKeyValue(index, 4, countries[neighbor].owner.color);

                    index++;
                }
                return animation;
            }
            public void __Init__AttackAnimation()
            {
                AnimationPlayer AttackPlayer = GetNode("Attack") as AnimationPlayer;



                for (int name = 0; name < 42; name++)
                {
                    AttackPlayer.AddAnimation(name.ToString(), AddAnimation(name));
                }
            }

        }

}
