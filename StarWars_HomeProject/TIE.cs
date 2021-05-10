using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarWars_HomeProject
{
    //These contain all the tie spaceships (I didn't want to create separate .cs files if that's not a problem)
    class TIE : Fighter
    {
        public TIE(int cost, int combatPower) : base(cost, combatPower)
        {
        }
    }

    class StarFighter : TIE
    {
        public StarFighter(int cost, int combatPower) : base(cost, combatPower)
        {
        }
    }
    class Interceptor : TIE
    {
        public Interceptor(int cost, int combatPower) : base(cost, combatPower)
        {
        }
    }
    class Bomber : TIE
    {
        public Bomber(int cost, int combatPower) : base(cost, combatPower)
        {
        }
    }
    class Defender : TIE
    {
        public Defender(int cost, int combatPower) : base(cost, combatPower)
        {
        }
    }
}
