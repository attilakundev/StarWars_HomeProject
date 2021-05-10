using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace StarWars_HomeProject
{
    
    class Cart : ChainedList<Spaceship>, ILinkedList
    {
        private ArmsDealer dealer = null;
        private int wouldSpend = 0;
        private int VaderCash = 0;
        public event ListDisplay cartEvent;
        public event NotifyEvent cartNotify;
        public override void AddToList(Spaceship ship, ref int wouldSpend)
        {
            if (ship.Cost > wouldSpend)
            {
                throw new NotEnoughMoneyException("You don't have enough money for this ship to put in your cart.");
            }
            ReallyAdd(ship);
            wouldSpend -= ship.Cost;
            string notify = $"Item put into the cart. You can now only shop for ships lower than {wouldSpend} of your budget.";
            cartNotify?.Invoke(notify);
        }
        public int CalculateTotalCost() // this will calculate the total cost of the ships in the cart
        {
            int totalCost = 0;
            for (int i = 0; i < Length; i++)
            {
                totalCost += this[i].Cost;
            }
            return totalCost;
        }
        public void DisplayThatList()
        {
            if (Length == 0)
            {
                throw new DoesNotExistException("The cart is empty.");
            }
            else
            {
                for (int i = 0; i < Length; i++)
                {
                    cartEvent?.Invoke(this[i], i);
                }
            }
        }
        public void DeleteList(ref int wouldSpend)
        {
            Spaceship[] ships = new Spaceship[length];
            for (int i = 0; i < length; i++)
            {
                ships[i]= this[i];
                dealer.AddToList(ships[i], ref wouldSpend);
                wouldSpend += ships[i].Cost;
            }
            ReallyDeleteList(ref head);
            wouldSpend = VaderCash;
        }
        public void Remove(int positionOfShip, ref int wouldSpend)
        {
                Spaceship ship = this[positionOfShip];
                dealer.AddToList(ship, ref wouldSpend);
                wouldSpend += ship.Cost;
                //length--;
                ReallyRemove(positionOfShip);
                cartNotify?.Invoke($"The ship is now removed from the cart. You can spend {wouldSpend} out of your wallet.");
        }
        public Cart(ArmsDealer dealer, int VaderCash)
        {
            this.dealer = dealer;
            wouldSpend = VaderCash;
            this.VaderCash = VaderCash;
            cartNotify += ConsoleOutput.MessageWriteLine;
            cartEvent += ConsoleOutput.DisplayShips;
        }
    }
}
