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
        public List<Country> ConnectedTo = new List<Country>(); //Should be List Of Country List<Country>
    }

    List<Country> Countries = new List<Country>();

    public void ToLight(ref Country country)
    {
        if (country.player.color == Colors.DarkRed)
        {
            country.player.color = Colors.Red;
            Brush(ref country);
        }
        else if (country.player.color == Colors.DarkGreen)
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
    public void ActivationMode(ref Country country)
    {
        ToLight(ref country);
        foreach (Country temp in country.ConnectedTo)
        {
            //GD.Print(temp.name);
           Country TempCountry = temp;
           ToLight(ref TempCountry);
        }

    }
    public void ToDark(ref Country country)
    {
        if (country.player.color == Colors.Red)
        {
            country.player.color = Colors.DarkRed;
            Brush(ref country);

        }
        else if (country.player.color == Colors.Green)
        {
            country.player.color = Colors.DarkGreen;
            Brush(ref country);

        }
        else if (country.player.color == Colors.Violet)
        {
            country.player.color = Colors.DarkViolet;
            Brush(ref country);

        }
    }
        public void DeactivationMode(ref Country country)
    {

        ToDark(ref country);
        foreach (Country temp in country.ConnectedTo)
        {
            Country TempCountry = temp;
            ToDark(ref TempCountry);
        }
    }

    public void Brush(ref Country country)
    {
        Sprite mySprite = (GetNode((country.name)) as Sprite);
        mySprite.Modulate = country.player.color;

    }

    public void Rest()
    {
        foreach(Country country in Countries)
        {
            Country Temp = country;
            DeactivationMode(ref Temp);
        }
    }

    public void ConnectCountries()
    {

        foreach (Country country in Countries)
        {
            foreach (string name in country.ConnectedToNames)
            {

                foreach (Country temp in Countries)
                {
                    if(temp.name==name)
                    {
                        country.ConnectedTo.Add(temp);
                    }
                }
            }
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
     

        Countries.Add(country);
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

        foreach (Country country in Countries[0].ConnectedTo) // Test Graph Builder
        {
            GD.Print("1", " --> ", country.name);
        }
    }

    private void _on_Area2D_input_event(object viewport, object @event, int shape_idx, string name)
    {
        if (Input.IsActionPressed("LM"))
        {
            Rest();
            foreach(Country country in Countries)
            {
                if (country.name == name)
                {
                    Country Temp = country;
                    ActivationMode(ref Temp);
                }
            }
        
        }
        
    }
        
}