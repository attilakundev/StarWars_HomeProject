using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarWars_HomeProject
{
    public static class ConsoleOutput
    {
        public static void MessageWrite(string message)
        {
            Console.Write(message);
        }
        public static void MessageWriteLine(string message)
        {
            Console.WriteLine(message);
        }

        public static void MessageWriteLine()
        {
            Console.WriteLine();
        }
        public static string ReadLine()
        {
            return Console.ReadLine();
        }

        public static void DisplayShips(Spaceship ship, int index)
        {
            MessageWriteLine("========");
            MessageWriteLine("Ship's number: " + (index + 1));
            MessageWriteLine("Ship type: " + ship.GetType().Name);
            MessageWriteLine("Cost: " + ship.Cost);
            MessageWriteLine("Combat power: " + ship.CombatPower);
        }
        public static void Menu(int vadercash)
        {
            MessageWriteLine("=================================================================");
            MessageWriteLine($"You have {vadercash} Imperial currency.");
            MessageWriteLine("\nThe following menupoints are available for you:");
            MessageWriteLine("1. Check and put a given ship from the stock in your cart");
            MessageWriteLine("2. Remove a given ship from your cart");
            MessageWriteLine("3. Remove ALL ships from your cart");
            MessageWriteLine("4. List the items in your cart");
            MessageWriteLine("5. Check out the best deal for your wallet. You can activate it anytime, but it's the best when you activate it with an empty cart,\nso you'll get the best offers for your money.");
            MessageWriteLine("6. Buy the items from your cart");
            MessageWriteLine("7. Display your current fleet");
            MessageWriteLine("E. Exit");
            MessageWrite("What's your choice?");
        }
    }
}
