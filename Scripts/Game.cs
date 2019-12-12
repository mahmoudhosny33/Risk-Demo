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
        public bool Active { get; set; }
        public List<string> ConnectedToNames {get;set;}
        public List<Sprite> ConnectedTo = new List<Sprite>();
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
        Sprite mySprite2 = (GetNode((country.name)) as Sprite);
        mySprite2.Modulate = country.player.color;

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

    public override void _Ready()
    {
        Country n1 = new Country();
        n1.player.id = 1;
        n1.name = "1";
        n1.player.color = Colors.DarkRed;
        Sprite mySprite = (GetNode(n1.name) as Sprite);
        mySprite.Modulate = n1.player.color;
        n1.Active = false;
        n1.ConnectedToNames = new List<string> { "6", "2", "5","3","7"}; 
        Brush(ref n1);
        GetSprite(ref n1);

        Country n2 = new Country();
        n2.player.id = 1;
        n2.name = "2";
        n2.player.color = Colors.DarkRed;
        n2.Active = false;
        n2.ConnectedToNames = new List<string> { "11", "10", "7", "6", "1" };
        Brush(ref n2);
        GetSprite(ref n2);


        Country n3 = new Country();
        n3.player.id = 1;
        n3.name = "3";
        n3.player.color = Colors.DarkRed;
        n3.Active = false;
        n3.ConnectedToNames = new List<string> { "1", "6","2","5"};
        Brush(ref n3);
        GetSprite(ref n3);


        Country n4 = new Country();
        n4.player.id = 1;
        n4.name = "4";
        n4.player.color = Colors.DarkRed;
        n4.Active = false;
        n4.ConnectedToNames = new List<string> { "1", "5" };
        Brush(ref n4);
        GetSprite(ref n4);

        Country n5 = new Country();
        n5.player.id = 1;
        n5.name = "5";
        n5.player.color = Colors.DarkRed;
        n5.Active = false;
        n5.ConnectedToNames = new List<string> { "1", "3","4" };
        Brush(ref n5);
        GetSprite(ref n5);



        Countries.Add(n1);
        Countries.Add(n2);
        Countries.Add(n3);
        Countries.Add(n4); 
        Countries.Add(n5);

       

    }

    private void _on_Area2D_input_event(object viewport, object @event, int shape_idx, string name)
    {
        if (Input.IsActionPressed("LM"))
        {
            GD.Print(name);
        }
    }
        
}




