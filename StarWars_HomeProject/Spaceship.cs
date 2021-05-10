using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarWars_HomeProject
{
    public abstract class Spaceship : ISpaceShip //I create this because not to have redundant code. I'm going to call this "ancestor of ancestors"
    {
        int cost;
        int combatPower;

        public int Cost { get { return cost; } }
        public int CombatPower { get { return combatPower; } }

        public Spaceship(int cost, int combatPower)
        {
            this.cost = cost;
            this.combatPower = combatPower;
        }
    }
}
