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
        public string name { get; set; }
        public int Active { get; set; } //Should be int 0 or 1 or 2
        public List<string> ConnectedToNames {get;set;}
        public List<Sprite> ConnectedTo = new List<Sprite>(); //Should be List Of Country List<Country>
    }

    List<Country> Countries = new List<Country>();

    public void ActivationMode(ref Country country)
    {
        if(country.player.color == Colors.DarkRed)
        {
            country.player.color = Colors.Red;
            Brush(ref country);
        }
        else if(country.player.color == Colors.DarkGreen)
        {
            country.player.color = Colors.Green;
            Brush(ref country);

        }
        else if (country.player.color == Colors.DarkViolet)
        {
            country.player.color = Colors.Violet;
            Brush(ref country);

        }
    }

    public void Brush(ref Country country)
    {
        Sprite mySprite = (GetNode((country.name)) as Sprite);
        mySprite.Modulate = country.player.color;

    }


    public void GetSprite(ref Country country)
    {

        List<string> names = country.ConnectedToNames;
        
        
        foreach (string name in names)
        {
            Sprite sprite = (GetNode(name) as Sprite);
            country.ConnectedTo.Add(sprite);
        }
    }
    public void AddCountry(int id,string name,Color color,int Mode,List<string> nodes)
    {
        Country country = new Country();
        country.player.id = id;
        country.name = name;
        country.player.color = color;
        country.Active = Mode;
        country.ConnectedToNames = nodes;

        Brush(ref country);
        GetSprite(ref country);

        Countries.Add(country);
    }
    public override void _Ready()
    {
        AddCountry(1, "1", Colors.DarkRed, 0, new List<string> { "6", "2", "5", "3", "7" });

        AddCountry(1, "2", Colors.DarkRed, 0, new List<string> { "11", "10", "7", "6", "1" });

        AddCountry(1,"3",Colors.DarkRed,0, new List<string> { "1", "6","2","5"});

        AddCountry(1, "4", Colors.Red, 0, new List<string> { "1", "5" });

        AddCountry(1, "5", Colors.DarkRed, 0, new List<string> { "1", "3", "4" });

     

    }

    private void _on_Area2D_input_event(object viewport, object @event, int shape_idx, string name)
    {
        if (Input.IsActionPressed("LM"))
        {
            foreach(Country country in Countries)
            {
                if (country.name == name)
                {
                    country.player.color = Colors.Red;
                    Country Temp = country;
                    Brush(ref Temp);
                }
            }
        
        }
    }
        
}