using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarWars_HomeProject
{
    interface ILinkedList
    {
        void Remove(int positionOfShip);
        void DeleteList();
        void AddToList(Spaceship ship, ref int wouldSpend);
    }
}
