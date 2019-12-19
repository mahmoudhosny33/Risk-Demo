using System;
using Godot.Collections;
using Godot;
namespace RiskGame.Scripts
{
    public partial class Game
    {
        public class Country
        {
            public int Active { get; set; } //Should be int 0 or 1 or 2

            public Player owner = new Player();
            public int troops;
            public Country()
            {
                owner = null;
                troops = 0;
            }

            public void LoseCountry(Player newone, int sol)
            {
                this.owner = newone;
                this.troops = sol;

            }
            public bool Attack(Country enemy)
            {
                int x = -1;
                bool res = false;
                GD.Print($"enter number of troops to fight from 1 to {this.troops - 1}");
                ////////////should take the input from gui
                x = 1;

                System.Random rand = new System.Random();
                for (int i = 0; i < x && enemy.troops > 0 && this.troops >= 2; i++)
                {
                    int Attacker_Dice, Attacked_Dice;
                    Attacker_Dice = rand.Next(1, 6);
                    Attacked_Dice = rand.Next(1, 6);
                    GD.Print($"Attacked Dice: {Attacker_Dice} , Attacked Dice {Attacked_Dice} ");
                    if (Attacker_Dice > Attacked_Dice)
                    {
                        enemy.troops--;
                    }
                    else this.troops--;
                }
                if (enemy.troops == 0)
                {

                    GD.Print($"player {this.owner.id} has take country from player {enemy.owner.id}");
                    int f;

                   GD.Print("how many toops you want to transport");
                    ////////////should take input from gui
                    f = 1;

                    enemy.owner.countries--;
                    enemy.LoseCountry(this.owner, f);
                    res = true;
                    this.troops -= f;
                    this.owner.countries++;
                }
                return res;
            }

        }
    }
}
