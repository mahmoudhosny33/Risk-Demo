using Godot;
using System.Collections.Generic;
using Godot.Collections;
namespace RiskGame.Scripts
{

    public partial class Game : Sprite
    {
        //organize turns
        private int turn;
        private int selected = -1;
        private List<int> enemies;
        //graph of the game
        private  List<int>[] map = new List<int>[42];
        private Country[] countries = new Country[42];
        private Player[] players = new Player[3];
        private Player curplayer;
        private int mode = 0;
        public override void _Ready()
        {

            turn = 0;
            curplayer = new Player();
            FillMap();
            __Init__AttackAnimation();
            __initZoom();
            InitPlayers();
            InitCountries();
            Disturb();
            StartTurn();

        }
        //fills the map logically
        private void FillMap()
        {
            // 1 ,2, 35     
            map[0] = new List<int>() { 1, 2, 35 };
            //0, 2 ,3 ,12           
            map[1] = new List<int>() { 0, 2, 3, 12 };
            //0 ,1, 3, 4          
            map[2] = new List<int>() { 0, 1, 3, 4 };
            //1, 2 ,4 ,6 ,5 ,12
            map[3] = new List<int>() { 1, 2, 4, 6, 5, 12 };
            //2 ,3, 7, 6          
            map[4] = new List<int>() { 2, 3, 7, 6 };
            //6 ,3, 12          
            map[5] = new List<int>() { 6, 3, 12 };
            //5 ,3, 4, 7           
            map[6] = new List<int>() { 5, 3, 4, 7 };
            //4 ,6, 8          
            map[7] = new List<int>() { 4, 6, 8 };
            //7 ,9, 10           
            map[8] = new List<int>() { 7, 9, 10 };
            //8 ,10, 11           
            map[9] = new List<int>() { 8, 10, 11 };
            //8, 9 ,11, 22           
            map[10] = new List<int>() { 8, 9, 11, 22 };
            //9, 10
            map[11] = new List<int>() { 9, 10 };
            //1 ,3 ,5, 13
            map[12] = new List<int>() { 1, 3, 5, 13 };
            //12,14,15
            map[13] = new List<int>() { 12, 14, 15 };
            //13,15,16,17
            map[14] = new List<int>() { 13, 15, 16, 17 };
            //13,16,18,14
            map[15] = new List<int>() { 13, 16, 18, 14 };
            //14,15,17,18,19
            map[16] = new List<int>() { 14, 15, 17, 18, 19 };
            //14,16,19,22
            map[17] = new List<int>() { 14, 16, 19, 22 };
            //15,16,19,20,28,27
            map[18] = new List<int>() { 15, 16, 19, 20, 28, 27 };
            //16,17,18,20,21
            map[19] = new List<int>() { 16, 17, 18, 20, 21 };
            //18,19,21,23,28,29
            map[20] = new List<int>() { 18, 19, 21, 23, 28, 29 };
            //19,20,23,22
            map[21] = new List<int>() { 19, 20, 23, 22 };
            //17,10,21,23,24
            map[22] = new List<int>() { 17, 10, 21, 23, 24 };
            //21,22,24,25,26,20
            map[23] = new List<int>() { 21, 22, 24, 25, 26, 20 };
            //22,23,25
            map[24] = new List<int>() { 22, 23, 25 };
            //23,24,26
            map[25] = new List<int>() { 23, 24, 26 };
            //23,25
            map[26] = new List<int>() { 23, 25 };
            //18,28,30,31
            map[27] = new List<int>() { 18, 28, 30, 31 };
            //18,27,30,20,29
            map[28] = new List<int>() { 18, 27, 30, 20, 29 };
            //20,28,30,37
            map[29] = new List<int>() { 20, 28, 30, 37 };
            //27,28,29,34,37,31
            map[30] = new List<int>() { 27, 28, 29, 34, 37, 31 };
            //27,30,34,33,32
            map[31] = new List<int>() { 27, 30, 34, 33, 32 };
            //31,33,35
            map[32] = new List<int>() { 31, 33, 35 };
            //31,32,35,34
            map[33] = new List<int>() { 31, 32, 35, 34 };
            //31,33,36,30,35
            map[34] = new List<int>() { 31, 33, 36, 30, 35 };
            //32,33,36,0,34
            map[35] = new List<int>() { 32, 33, 36, 0, 34 };
            //35,34
            map[36] = new List<int>() { 35, 34 };
            //29,30,38
            map[37] = new List<int>() { 29, 30, 38 };
            //37,39,40
            map[38] = new List<int>() { 37, 39, 40 };
            //38,40,41
            map[39] = new List<int>() { 38, 40, 41 };
            //41,38,39
            map[40] = new List<int>() { 41, 38, 39 };
            //40,39
            map[41] = new List<int>() { 40, 39 };


        }
        private void CheckMap()
        {
            for (int i = 0; i < 42; i++)
            {
                for (int ii = 0; ii < map[i].Count; ii++)
                {
                    bool found = false;
                    for (int iii = 0; iii < map[map[i][ii]].Count; iii++)
                    {
                        if (map[map[i][ii]][iii] == i)
                        {
                            found = true;
                        }
                    }
                    if (found == false)
                    {
                        System.Console.WriteLine("{0},{1}", i, map[i][ii]);
                    }
                }
            }
        }
        private void InitPlayers()
        {
            for (int i = 0; i < 3; i++)
            {
                players[i] = new Player();
                players[i].id = i;
            }
            players[0].color = Colors.DarkGreen;
            players[1].color = Colors.DarkRed;
            players[2].color = Colors.DarkBlue;
        }
        private void InitCountries()
        {
            for (int i = 0; i < 42; i++)
            {
                countries[i] = new Country();
            }
        }
        private void Disturb()
        {
            System.Random rand = new System.Random();
            bool[] arr = new bool[42];
            List<int>[] arr2 = new List<int>[3];
            for (int i = 0; i < 3; i++)
            {
                arr2[i] = new List<int>();
            }
            for (int i = 0; i < 2; i++)
            {
                while (players[i].countries < 14)
                {
                    int f = rand.Next(0, 41);
                    // System.Console.WriteLine(f);
                    if (arr[f] == false)
                    {
                        arr[f] = true;
                        AddCountry(i, f.ToString());
                        arr2[i].Add(f);
                    }

                }
            }

            for (int i = 0; i < 42; i++)
            {
                if (arr[i] == false)
                {
                    arr[i] = true;
                    AddCountry(2, i.ToString());
                    arr2[2].Add(i);
                }
            }

            for (int i = 0; i < 3; i++)
            {
                for (int ii = 0; ii < arr2[i].Count; ii++)
                {
                    int f = rand.Next(0, players[i].notusedTroops / 3);
                    countries[arr2[i][ii]].troops += f;
                    players[i].notusedTroops -= f;
                    if (players[i].notusedTroops == 0) break;
                }
                if (players[i].notusedTroops > 0)
                {
                    while (players[i].notusedTroops > 0)
                    {
                        int f = rand.Next(0, 13);
                        countries[arr2[i][f]].troops += 1;
                        players[i].notusedTroops -= 1;
                    }
                }
            }
        }
        public void ToLight(string name)
        {
            if (players[countries[int.Parse(name)].owner.id].color == Colors.DarkRed)
            {
                players[countries[int.Parse(name)].owner.id].color = Colors.Red;

            }
            else if (players[countries[int.Parse(name)].owner.id].color == Colors.DarkGreen)
            {
                players[countries[int.Parse(name)].owner.id].color = Colors.Green;


            }
            else if (players[countries[int.Parse(name)].owner.id].color == Colors.DarkBlue)
            {

                players[countries[int.Parse(name)].owner.id].color = Colors.Blue;

            }
            Brush(name);
        }
        public void ToDark(string name)
        {

            if (players[countries[int.Parse(name)].owner.id].color == Colors.Red)
            {
                players[countries[int.Parse(name)].owner.id].color = Colors.DarkRed;


            }
            else if (players[countries[int.Parse(name)].owner.id].color == Colors.Green)
            {
                players[countries[int.Parse(name)].owner.id].color = Colors.DarkGreen;


            }
            else if (countries[int.Parse(name)].owner.color == Colors.Blue)
            {
                players[countries[int.Parse(name)].owner.id].color = Colors.DarkBlue;


            }
            Brush(name);
        }
        public void Brush(string name)
        {
            Sprite mySprite = (GetNode((name)) as Sprite);
            try
            {
                mySprite.Modulate = players[countries[int.Parse(name)].owner.id].color;
                mySprite.Update();
                //GD.Print("7a7a");
            }
            catch { GD.Print("Country not found, please add it first"); }
        }
        public void Rest()
        {
            for (int i = 0; i < 42; i++)
            {

                Brush(i.ToString());
            }
        }
        public void AddCountry(int id, string name)
        {
            countries[int.Parse(name)].owner = players[id];
            countries[int.Parse(name)].troops++;
            players[id].countries++;
            players[id].notusedTroops--;
            Brush(name);
        }


        private void Draft(int a)
        {
            System.Console.WriteLine(" ");
            System.Console.WriteLine("enter  num of troops ");
            //////////should take the number from gui
            int b = 1;
            string c;

            // b = int.Parse(System.Console.ReadLine());
            if (b <= players[turn % 3].notusedTroops && countries[a].owner == players[turn % 3])
            {
                players[turn % 3].notusedTroops -= b;
                players[turn % 3].usedTroops += b;
                countries[a].troops += b;
            }


        }
        private void Attack(int Attacker, int Attacked)
        {
            System.Console.WriteLine("Attack");
            System.Console.WriteLine(" ");
            int curcity;

            if (countries[Attacked].owner != curplayer)
                if (countries[Attacker].Attack(countries[Attacked]))
                {
                    Brush(Attacked.ToString());
                }


        }
        //searches for all countries 1 if you want friend one,0 if you want enimies
        private void ViewAllCities(int state)
        {
            bool[] vis = new bool[42];
            for (int i = 0; i < 42; i++) vis[i] = false;
            DFS(0, vis, state);
        }
        private void DFS(int cur, bool[] vis, int state)
        {
            if (vis[cur] == true) return;
            vis[cur] = true;
            if (state == 1 && countries[cur].owner == players[turn % 3])
            {
                //run any function
                ToLight(cur.ToString());
            }
            else if (state == 0 && countries[cur].owner != players[turn % 3])
            {
                System.Console.Write("{0} ", cur);
                //run any function
            }
            for (int i = 0; i < map[cur].Count; i++)
            {
                DFS(map[cur][i], vis, state);
            }
        }
        private void Fortify(int a, int b)
        {


            /////////should read the input from gui

            System.Console.WriteLine("enter num of troops from 1 to {0}", countries[a].troops - 1);
            /////////should read the input from gui
            int c = 1;
            if (countries[a].troops - c >= 1)
            {
                countries[a].troops -= c;
                countries[b].troops += c;
            }
        }
        private void SelectFriends(int cur)
        {
            bool[] vis = new bool[42];
            for (int i = 0; i < 42; i++) vis[i] = false;
            Tracelands(cur, vis);
        }
        private void Tracelands(int cur, bool[] vis)
        {
            if (vis[cur] == true) return;
            vis[cur] = true;
            // System.Console.Write("{0} ", cur);
            ToLight(cur.ToString());
            for (int i = 0; i < map[cur].Count; i++)
            {
                if (countries[map[cur][i]].owner == players[turn])
                {
                    Tracelands(map[cur][i], vis);
                }
            }
        }
        private void StartTurn()
        {
            ViewAllCities(1);
            System.Console.WriteLine("draft");
            int newtroops = (players[turn % 3].countries) / 3;
            System.Console.WriteLine("player{0},has new{1}troops", turn % 3, newtroops);
            players[turn % 3].notusedTroops += newtroops;


        }
        private void _on_Button_pressed()
        {

            if (this.mode == 0 && players[this.turn].notusedTroops == 0)
            {

                this.mode = 1;
                Rest();

            }
            else if (this.mode == 1)
            {
                this.mode = 2;
                selected = -1;
                Rest();

            }
            else if (this.mode == 2)
            {
                this.mode = 0;
                this.turn = (this.turn + 1) % 3;
                Rest();
                StartTurn();
            }

        }
        private void _on_Area2D_input_event(object viewport, object @event, int shape_idx, string name)
        {

            if (Input.IsActionPressed("LM") && this.mode == 0 && countries[int.Parse(name)].owner == players[turn])
            {
                Draft(int.Parse(name));
                System.Console.WriteLine("still {0}", players[turn].notusedTroops);
            }
            else if (Input.IsActionPressed("LM") && this.mode == 1)
            {
                if (selected == -1)
                {
                    if (countries[int.Parse(name)].owner == players[turn])
                    {
                        selected = int.Parse(name);
                        ToLight(name);
                        for (int i = 0; i < map[int.Parse(name)].Count; i++)
                        {
                            if (countries[map[int.Parse(name)][i]].owner != players[turn])
                            {
                                // ToLight(map[int.Parse(name)][i].ToString());
                                AnimationPlayer animation = GetNode("Attack") as AnimationPlayer;
                                Zoom(GetGlobalMousePosition(), name);
                                animation.Play(name);
                            }
                        }
                    }
                }
                else
                {
                    bool allow = false;
                    int n = int.Parse(name);
                    for (int i = 0; i < map[selected].Count; i++)
                    {
                        if (n == map[selected][i])
                        {
                            allow = true;
                            break;
                        }
                    }
                    if (countries[int.Parse(name)].owner == players[turn])
                    {
                        Rest();
                        selected = -1;
                    }
                    else
                    {
                        if (allow)
                        {

                            countries[selected].Attack(countries[n]);
                        }
                        else Rest();
                    }

                }
            }
            else if (Input.IsActionPressed("LM") && this.mode == 2)
            {
                int n = int.Parse(name);
                if (selected == -1)
                {
                    if (countries[n].owner == players[turn])
                    {
                        selected = n;


                        SelectFriends(n);
                    }
                }
                else
                {
                    if (n == selected || countries[n].owner != players[turn])
                    {
                        selected = -1;
                        Rest();
                    }
                    else
                    {
                        Fortify(selected, n);
                    }
                }
            }

        }

    }

}