using Godot;
using System.Collections.Generic;
using Godot.Collections;

public class Game : Sprite
{
    public void __initZoom()
    {
        AnimationPlayer ZCamera = GetNode("ZCamera") as AnimationPlayer;
        Animation animation = ZCamera.GetAnimation("Zoom");
        int idx = 0;
        float value = 1.0f;
        for (int i = 0; i < 22; i++)
        {
            animation.TrackSetKeyValue(idx, i, new Vector2(value, value));

            value -= (1/60f);
        }

    }
    public void zoom(Vector2 pos,string name)
    {
       
        AnimationPlayer ZCamera = GetNode("ZCamera") as AnimationPlayer;
        Animation animation = ZCamera.GetAnimation("Zoom");
        Vector2 camera_pos = (GetNode("ZCamera/Camera2D") as Camera2D).Position;
        float distance = pos.DistanceTo(camera_pos);
        Vector2 speed = new Vector2(distance/22, distance/22);
        Vector2 dir = pos.DirectionTo(camera_pos);

        Vector2 newPos = camera_pos;
        GD.Print(newPos);
        for(int i = 0; i < 22; i++)
        {
            animation.TrackSetKeyValue(1, i,newPos);
            newPos -= (dir * speed);
        }
        ZCamera.Play("Zoom");
    }
    public Animation AddAnimation(string name)
    {
        Animation animation = new Animation();
        int index = 0;
        foreach (string neighbor in Countries[name].ConnectedToNames)
        {
            animation.AddTrack(Animation.TrackType.Value);
            animation.TrackSetPath(index, $"{(neighbor).ToInt()}:modulate");

            animation.SetLength(2.4f);
            animation.SetStep(0.6f);
            animation.SetLoop(true);
            animation.TrackInsertKey(index, 0, new Color());
            animation.TrackSetKeyValue(index, 0, Countries[neighbor].player.color);

            animation.TrackInsertKey(index, 0.6f, new Color());
            animation.TrackSetKeyValue(index, 1, Colors.Black);

            animation.TrackInsertKey(index, 1.2f, new Color());
            animation.TrackSetKeyValue(index, 2, Countries[neighbor].player.color);


            animation.TrackInsertKey(index, 1.8f, new Color());
            animation.TrackSetKeyValue(index, 3, Colors.Black);

            animation.TrackInsertKey(index, 2.4f, new Color());
            animation.TrackSetKeyValue(index, 4, Countries[neighbor].player.color);

            index++;
        }
        return animation;
    }
    public void __Init__AttackAnimation()
    {
        AnimationPlayer AttackPlayer = GetNode("Attack") as AnimationPlayer;


       
        foreach (string name in Countries.Keys)
        {
            AttackPlayer.AddAnimation(name,AddAnimation(name));

        }
    }

    public class Player
    {
        public int id { get; set; }
        public Color color { get; set; }
    }
    public class Country
    {
        public Player player = (new Player());
        public int Active { get; set; } //Should be int 0 or 1 or 2
        public List<string> ConnectedToNames {get;set;}
        public List<Country> ConnectedTo = new List<Country>(); //Should be List Of Country List<Country>
    }

    System.Collections.Generic.Dictionary<string, Country> Countries = new System.Collections.Generic.Dictionary<string, Country>(); 

    public void ToLight(string name)
    {
        if (Countries[name].player.color == Colors.DarkRed)
        {
            Countries[name].player.color = Colors.Red;
            Brush(name);
        }
        else if (Countries[name].player.color == Colors.DarkGreen)
        {
            Countries[name].player.color = Colors.Green;
            Brush(name);

        }
        else if (Countries[name].player.color == Colors.DarkViolet)
        {
            Countries[name].player.color = Colors.Violet;
            Brush(name);

        }

    }
    public void ActivationMode(string name)
    {
        ToLight(name);
        AnimationPlayer animation = GetNode("Attack") as AnimationPlayer;
        animation.Play(name);
    }
    public void ToDark(string name)
    {
        if (Countries[name].player.color == Colors.Red)
        {
            Countries[name].player.color = Colors.DarkRed;
            Brush(name);

        }
        else if (Countries[name].player.color == Colors.Green)
        {
            Countries[name].player.color = Colors.DarkGreen;
            Brush(name);

        }
        else if (Countries[name].player.color == Colors.Violet)
        {
            Countries[name].player.color = Colors.DarkViolet;
            Brush(name);

        }
    }
    public void DeactivationMode(string name)
    {

        ToDark(name);
        foreach (string neighbor in Countries[name].ConnectedToNames)
        {
            Brush(neighbor);
        }

    }

    public void Brush(string name)
    {
        Sprite mySprite = (GetNode((name)) as Sprite);
        try
        {
            mySprite.Modulate = (Countries[name].player.color);
        }
        catch { GD.Print("Country not found, please add it first"); }

    }

    public void Rest()
    {
        foreach(string name in Countries.Keys)
        {
            DeactivationMode(name);
        }
    }

    public void ConnectCountries()
    {
        foreach(string name in Countries.Keys)
        {
            foreach(string neighbor in Countries[name].ConnectedToNames)
            {
                Countries[name].ConnectedTo.Add(Countries[neighbor]);
            }
        }
    }
    public void AddCountry(int id,string name,Color color,int Mode,List<string> nodes)
    {
        Country country = new Country();
        country.player.id = id;
         country.player.color = color;
         country.Active = Mode;
         country.ConnectedToNames = nodes;
        Countries.Add(name, country);

        Brush(name);



    }
    public override void _Ready()
    {
        AddCountry(1, "1", Colors.DarkRed, 0, new List<string> { "6", "2", "5", "3", "7" });

        AddCountry(1, "2", Colors.DarkRed, 0, new List<string> { "11", "10", "7", "6", "1" });

        AddCountry(1,"3",Colors.DarkRed,0, new List<string> { "1", "6","2","5"});

        AddCountry(1, "4", Colors.DarkRed, 0, new List<string> { "1", "5" });

        AddCountry(1, "5", Colors.DarkRed, 0, new List<string> { "1", "3", "4" });

        AddCountry(2, "6", Colors.DarkGreen, 0, new List<string> { "11", "10", "2","1","3" });

        AddCountry(2, "7", Colors.DarkRed, 0, new List<string> { "8", "12", "13", "10", "2", "1" });

        AddCountry(1, "8", Colors.DarkRed, 0, new List<string> { });

        AddCountry(2, "10", Colors.DarkGreen, 0, new List<string> { });

        AddCountry(3, "11", Colors.DarkViolet, 0, new List<string> { });

        AddCountry(1, "12", Colors.DarkRed, 0, new List<string> { });

        AddCountry(1, "13", Colors.DarkRed, 0, new List<string> { });

        ConnectCountries(); // Graph Builder

        foreach (string country in Countries["1"].ConnectedToNames) // Test Graph Builder
        {
           GD.Print("1", " --> ", country);
        }


        __initZoom();
        __Init__AttackAnimation();
    }

    private void _on_Area2D_input_event(object viewport, object @event, int shape_idx, string name)
    {
        if (Input.IsActionPressed("LM"))
        {
            Rest(); // Disable Last Action
            try
            {
                zoom(GetGlobalMousePosition(),name);
                ActivationMode(name);
            }
            catch
            {
                GD.Print($" Key {name} Not Added Yet Please Add it");
            }
        }
        
    }
        
}