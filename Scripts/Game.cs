using Godot;
using System.Collections.Generic;
using Godot.Collections;

public class Game : Sprite
{



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
        foreach (string neighbor in Countries[name].ConnectedToNames)
        {
            ToLight(neighbor);
        }

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
            ToDark(neighbor);
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
    }

    private void _on_Area2D_input_event(object viewport, object @event, int shape_idx, string name)
    {
        if (Input.IsActionPressed("LM"))
        {
            Rest(); // Disable Last Action
            try
            {
                ActivationMode(name);
            }
            catch
            {
                GD.Print($" Key {name} Not Added Yet Please Add it");
            }
        }
        
    }
        
}