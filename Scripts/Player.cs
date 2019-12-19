using Godot;
using System.Collections.Generic;
using Godot.Collections;
namespace RiskGame.Scripts
{
    public partial class Game
    {
        public class Player
        {
            public int id { get; set; }
            public Color color { get; set; }
            public int notusedTroops { get; set; }
            public int usedTroops { get; set; }
            public int cards { get; set; }

            // public  string name;
            public int contents { get; set; }
            public int countries { get; set; }
            public Player()
            {
                notusedTroops = 35;
                usedTroops = 35;
                cards = 0;
                countries = 0;
            }

        }
    }
}
