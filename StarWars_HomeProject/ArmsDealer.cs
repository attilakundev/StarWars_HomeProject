using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarWars_HomeProject
{
    class ArmsDealer : ChainedList<Spaceship>
    {
        static Random r = new Random();
        public event ListDisplay dealerEvent;
        //the ships will be randomly generated, probably i'll generate between 5 and 20, and put them in a list (linked list), and from there Vader can choose from spaceships, and when he buys some spaceships, the event will be called and each ship will be listed. 
        int[,] BestOffer(int VaderMoney) { // 0-1 knapsack algorithm
            int[,] knapsack = new int[length+1, VaderMoney+1];
            for (int i = 0; i <= length; i++)
            {
                for (int j = 0; j <= VaderMoney; j++)
                {
                    if (i == 0 || j == 0) 
                    {
                        knapsack[i, j] = 0; //we fill the "frame" with zeroes
                    }
                    else if (j > this[i-1].Cost && i > 0 && j > 0) {
                        knapsack[i, j] = Math.Max(knapsack[i - 1, j], knapsack[i - 1, j - this[i-1].Cost] + this[i-1].CombatPower);  //if we could find an adequate ship with adequate cost what we are indexing right now, then we put it in the array. (with the combatpower of the previous ones if exists)
                    }
                    else
                    {
                        knapsack[i, j] = knapsack[i - 1, j]; // we just copy the previous "indexed" item
                    }
                }
            }
            return knapsack;
        }
        public void DisplayBestOffer(int VaderMoney) //Displays the best ships for the money.
        {
            bool[] ships = ReturnGoodShips(BestOffer(VaderMoney));
            for (int i = 0; i < ships.Length; i++)
            {
                if (ships[i] == true)
                {
                    dealerEvent?.Invoke(this[i], i);
                    //Console.WriteLine("Ship type: " +FindShip(i).GetType().Name);
                    //Console.WriteLine("Number from stock: " + (i+1));
                    //Console.WriteLine("Cost: " +FindShip(i).Cost);
                    //Console.WriteLine("Combat power: "+FindShip(i).CombatPower);
                }
            }
        }
        bool[] ReturnGoodShips(int[,] knapsack) // This returns the ships adequate for our money
        { 
            int index = knapsack.GetLength(0)-1;
            bool[] ships = new bool[index];
            int maxCost = knapsack.GetLength(1)-1;
            while (index > 0 && maxCost > 0) //this will check if the "knapsack" array's items aren't equal (so then it's a possible solution then for a ship)
            {
                if (knapsack[index, maxCost] != knapsack[index-1, maxCost])
                {
                    ships[index-1] = true; //index-1, because the indexing is a bit different so-to say :) the knapsack array is one bigger than we would expect
                    maxCost -= this[index-1].Cost; //we "move" upwards
                }
                index--;
            }
            return ships;
        }
        public bool[] Goodships(int VaderMoney) //I need this because to use in external algorithm
        {
            return ReturnGoodShips(BestOffer(VaderMoney));
        }
        public void DisplayThatList()
        {
            if (length ==0)
            {
                throw new DoesNotExistException("The shop is literally empty.");
            }
            for (int i = 0; i < length; i++) 
            {
                dealerEvent?.Invoke(this[i], i);
            }
        }
        public ArmsDealer()
        {
            dealerEvent += ConsoleOutput.DisplayShips;
        }
    }
}
