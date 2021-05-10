using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarWars_HomeProject
{
    //StarDestroyers
    class StarDestroyer : Battleship
    {
        public StarDestroyer(int cost, int combatPower) : base(cost, combatPower)
        {
        }
    }

    class ImperialClass : StarDestroyer
    {
        public ImperialClass(int cost, int combatPower) : base(cost, combatPower)
        {
        }
    }
    class VictoryClass : StarDestroyer
    {
        public VictoryClass(int cost, int combatPower) : base(cost, combatPower)
        {
        }
    }
    class ExecutorClass : StarDestroyer
    {
        public ExecutorClass(int cost, int combatPower) : base(cost, combatPower)
        {
        }
    }
    class MaelstromClass : StarDestroyer
    {
        public MaelstromClass(int cost, int combatPower) : base(cost, combatPower)
        {
        }
    }
    class GladiatorClass : StarDestroyer
    {
        public GladiatorClass(int cost, int combatPower) : base(cost, combatPower)
        {
        }
    }
}
