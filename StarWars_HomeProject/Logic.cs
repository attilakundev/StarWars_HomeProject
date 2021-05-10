using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarWars_HomeProject
{
    public delegate void ListDisplay(Spaceship ship, int index);
    public delegate void NotifyEvent(string message);
    public class Logic
    {
        public Random r = new Random();
        ArmsDealer dealer = null; // LINKED LIST
        Cart cart = null; // Linked List
        VaderShips<Spaceship,int> vaderShips = null; //BST
        int vadercash = 0;
        int wouldSpend = 0;
        bool exceptionIsThrown = true;
        bool alreadyBoughtOffer = false;
        void MenuLogic()
        {
            
            string choice = null;
            while (choice != "E")
            {
                try
                {
                    ConsoleOutput.Menu(vadercash);
                    choice = ConsoleOutput.ReadLine();
                    ConsoleOutput.MessageWriteLine();
                    exceptionIsThrown = false;
                }
                catch (FormatException) {
                    exceptionIsThrown = true;
                    ConsoleOutput.MessageWriteLine("Wrong output. Please try it again.."); 
                }
                if(choice == "1")
                { 
                    
                    try
                    {
                        dealer.DisplayThatList();
                        ConsoleOutput.MessageWrite("Choose a ship to put into the cart: ");
                        int shipChoice = int.Parse(ConsoleOutput.ReadLine());
                        PutShipInCart(shipChoice);
                    }
                    catch (NotEnoughMoneyException e)
                    {
                        ConsoleOutput.MessageWriteLine(e.Message);
                    }
                    catch (FormatException) { ConsoleOutput.MessageWriteLine("Wrong input. Please try again.");}
                    catch (DoesNotExistException e) { ConsoleOutput.MessageWriteLine(e.Message); }

                }
                else if (choice == "2")
                {
                    ConsoleOutput.MessageWrite("Choose a ship to remove from the cart: ");
                    try
                    {
                        cart.DisplayThatList();
                        int shipChoice = int.Parse(ConsoleOutput.ReadLine());
                        RemoveShipFromCart(shipChoice);
                    }
                    catch (DoesNotExistException e)
                    {
                        ConsoleOutput.MessageWriteLine(e.Message);
                    }
                    catch(FormatException)
                    {
                        ConsoleOutput.MessageWriteLine("Wrong output. Please try again.");
                    }
                }
                else if (choice == "3")
                {
                    cart.DeleteList(ref wouldSpend);
                    ConsoleOutput.MessageWriteLine("The cart is emptied.");
                }
                else if(choice == "4")
                {
                    try
                    {
                        cart.DisplayThatList();
                    }
                    catch (DoesNotExistException e)
                    {

                        ConsoleOutput.MessageWriteLine(e.Message);
                    }
                }
                else if(choice == "5") {
                    try
                    {
                        if (alreadyBoughtOffer) throw new AlreadyExistsException("You already bought the offered items from the store.");
                        dealer.DisplayBestOffer(vadercash);
                        ConsoleOutput.MessageWriteLine("Do you want to instantly buy all of these? (yes/no)");
                        string answer = ConsoleOutput.ReadLine();
                        if (answer == "yes")
                        {
                            bool[] GoodShips = dealer.Goodships(vadercash);
                            int removedShips = 0;// when i remove items from the dealer, the list size will decrease, so i need to implement a variable which tracks how many ships had been deleted from the shop.
                            for (int i = 0; i < GoodShips.Length; i++)
                            {
                                if (GoodShips[i] == true)
                                {
                                    Spaceship ship = dealer[i - removedShips];
                                    try
                                    {
                                        vaderShips.InsertShip(ship.Cost, ship);
                                        dealer.Remove(i - removedShips);
                                        removedShips++;
                                        vadercash -= ship.Cost;
                                        wouldSpend -= ship.Cost;
                                    }
                                    catch (AlreadyExistsException e)
                                    {

                                        ConsoleOutput.MessageWriteLine(e.Message);
                                    }
                                    catch (CartHasItemsException e)
                                    {
                                        ConsoleOutput.MessageWriteLine(e.Message);
                                    }
                                }
                            }
                            alreadyBoughtOffer = true;
                        }
                    }
                    catch (DoesNotExistException e)
                    {
                        ConsoleOutput.MessageWriteLine(e.Message);
                    }
                    catch (AlreadyExistsException e)
                    {
                        ConsoleOutput.MessageWriteLine(e.Message);
                    }
                    catch (CartHasItemsException e)
                    {
                        ConsoleOutput.MessageWriteLine(e.Message);
                    }
                    catch (FormatException)
                    {
                        ConsoleOutput.MessageWriteLine("Wrong input. Please try again.");
                    }
                }
                else if (choice == "6")
                {
                    int Length = cart.Length;
                    vadercash -= cart.CalculateTotalCost();
                    for (int i = 0; i < Length; i++)
                    {
                        try
                        {
                            Spaceship ship = cart[i];
                            vaderShips.InsertShip(ship.Cost, ship);
                        }
                        catch (AlreadyExistsException e)
                        {
                            ConsoleOutput.MessageWriteLine(e.Message);
                        }
                        catch(DoesNotExistException e)
                        {
                            ConsoleOutput.MessageWriteLine(e.Message);
                        }
                        
                    }
                    cart.DeleteList();
                    wouldSpend = vadercash;
                }
                else if (choice == "7")
                {
                    try
                    {
                        ConsoleOutput.MessageWriteLine("Your fleet looks like this:");
                        vaderShips.TraversalList();
                    }
                    catch (DoesNotExistException e)
                    {
                        ConsoleOutput.MessageWriteLine(e.Message);
                    }
                }
            }
        }
        public void PutShipInCart(int shipPos)
        {
            try
            {
                cart.AddToList(dealer[shipPos-1], ref wouldSpend);
                dealer.Remove(shipPos-1);
            }
            catch (IndexOutOfRangeException e)
            {

                ConsoleOutput.MessageWriteLine(e.Message);
            }
        }
        public void RemoveShipFromCart(int shipPos)
        {
            try
            {
                cart.Remove(shipPos-1, ref wouldSpend);
            }
            catch (DoesNotExistException e)
            {

                ConsoleOutput.MessageWriteLine(e.Message);
            }
        }

        private void GenerateVaderShips(int limit)
        {
            for (int i = 0; i < limit; i++)
            {
                Spaceship ship = null;
                bool successfulgeneration = false;
                while (!successfulgeneration)
                {
                    switch (r.Next(0, 9)) // Vader has been scammed so he has way more expensive ships than what is available for him.
                    {
                        case 0: ship = new Bomber(r.Next(1000, 3000), r.Next(1, 6)); break;
                        case 1: ship = new StarFighter(r.Next(1000, 3000), r.Next(1, 6)); break;
                        case 2: ship = new Defender(r.Next(1000, 3000), r.Next(1, 6)); break;
                        case 3: ship = new Interceptor(r.Next(1000, 3000), r.Next(1, 6)); break;
                        case 4: ship = new ImperialClass(r.Next(5000, 10000), r.Next(6, 14)); break;
                        case 5: ship = new VictoryClass(r.Next(5000, 10000), r.Next(6, 14)); break;
                        case 6: ship = new ExecutorClass(r.Next(5000, 10000), r.Next(6, 14)); break;
                        case 7: ship = new MaelstromClass(r.Next(5000, 10000), r.Next(6, 14)); break;
                        case 8: ship = new GladiatorClass(r.Next(5000, 10000), r.Next(6, 14)); break;
                    }
                    try
                    {
                        vaderShips.InsertShip(ship.Cost, ship);
                        successfulgeneration = true;
                    }
                    catch (AlreadyExistsException)
                    {
                        //Just i catch this exception so the while loop runs until we can generate it
                    }
                }
            }
        }
        private void GenerateShop(int limit)
        {
            for (int i = 0; i < limit; i++)
            {
                Spaceship ship = null;
                bool successfulgeneration = false;
                while (!successfulgeneration)
                {
                    switch (r.Next(0, 9))
                    {
                        case 0: ship = new Bomber(r.Next(20, 300), r.Next(1, 6)); break;
                        case 1: ship = new StarFighter(r.Next(20, 300), r.Next(1, 6)); break;
                        case 2: ship = new Defender(r.Next(20, 300), r.Next(1, 6)); break;
                        case 3: ship = new Interceptor(r.Next(20, 300), r.Next(1, 6)); break;
                        case 4: ship = new ImperialClass(r.Next(300, 1000), r.Next(6, 14)); break;
                        case 5: ship = new VictoryClass(r.Next(300, 1000), r.Next(6, 14)); break;
                        case 6: ship = new ExecutorClass(r.Next(300, 1000), r.Next(6, 14)); break;
                        case 7: ship = new MaelstromClass(r.Next(300, 1000), r.Next(6, 14)); break;
                        case 8: ship = new GladiatorClass(r.Next(300, 1000), r.Next(6, 14)); break;
                    }
                    try
                    {
                        dealer.ReallyAdd(ship);
                        successfulgeneration = true;
                    }
                    catch (AlreadyExistsException)
                    {
                        //i only catch the error so i can move on...
                    }
                }
               
            }
        }
        
        
        public Logic()
        {
            int fleetsize =0;
            exceptionIsThrown = true;
            string input = null;
            ConsoleOutput.MessageWriteLine("Kun Attila [Neptun code redacted]- Star Wars\n");
            ConsoleOutput.MessageWriteLine("You are Darth Vader. Do you want to upgrade your fleet, don't you? Well, here's your time.");
            while (exceptionIsThrown) // "stupid proof implementation" aka i don't want to get stupid compile errors
            {
                ConsoleOutput.MessageWriteLine("\nHow big is your current fleet ? (don't try to hit more than ~2000/3000 since it will give you an endless loop or slow loop of generation due to the limits (cost is btw 1000-10000)");

                try
                {
                    input = ConsoleOutput.ReadLine();
                    fleetsize = int.Parse(input);
                    exceptionIsThrown = false;
                }
                catch (FormatException)
                {
                    exceptionIsThrown = true;
                    ConsoleOutput.MessageWriteLine("Wrong input. Please try it again..");
                }
            }
            exceptionIsThrown = true; // I need this here so I can reset
            while (exceptionIsThrown)
            {
                ConsoleOutput.MessageWriteLine("How much money do you want to spend to renew your fleet?(consider trying not to give a huge number, bigger than 100000)");
                /*knapsack algorithm works the following: it creates an matrix which
                 * allocates 4 X [ships in the shop] X [money that Vader has](so 
                 * 4X50X10000000 = 2X10^9 byte or 2 GB,but this is not useful to be
                 * honest, that's why you can see this warning). */
                try
                {
                    input = ConsoleOutput.ReadLine();
                    vadercash = int.Parse(input);
                    wouldSpend = vadercash;
                    exceptionIsThrown = false;
                }
                catch (FormatException)
                {
                    exceptionIsThrown = true;
                    ConsoleOutput.MessageWriteLine("Wrong input. Please try it again..");
                }
            }
            dealer = new ArmsDealer();
            vaderShips = new VaderShips<Spaceship,int>();
            GenerateShop(5);
            GenerateVaderShips(fleetsize);
            cart = new Cart(dealer,vadercash);
            MenuLogic();
        }
    }
}
