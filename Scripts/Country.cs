using System;
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
                System.Console.WriteLine("enter number of troops to fight from 1 to {0}", this.troops - 1);
                ////////////should take the input from gui
                x = 1;

                System.Random rand = new System.Random();
                for (int i = 0; i < x && enemy.troops > 0 && this.troops >= 2; i++)
                {
                    int Attacker_Dice, Attacked_Dice;
                    Attacker_Dice = rand.Next(1, 6);
                    Attacked_Dice = rand.Next(1, 6);
                    System.Console.WriteLine("a: {0} , b {1} ", Attacker_Dice, Attacked_Dice);
                    if (Attacker_Dice > Attacked_Dice)
                    {
                        enemy.troops--;
                    }
                    else this.troops--;
                }
                if (enemy.troops == 0)
                {

                    System.Console.WriteLine("player {0} has take country from player {1}", this.owner.id, enemy.owner.id);
                    int f;

                    System.Console.WriteLine("how many toops you want to transport");
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
