using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarWars_HomeProject
{
    class AlreadyExistsException : Exception
    {
        public AlreadyExistsException(string message):base(message)
        {

        }
    }
    class DoesNotExistException : Exception
    {
        public DoesNotExistException(string message) : base(message)
        {

        }
    }
    class CartHasItemsException : Exception
    {
        public CartHasItemsException(string message) : base(message)
        {

        }
    }
    class NotEnoughMoneyException : Exception
    {
        public NotEnoughMoneyException(string message) : base(message)
        {

        }
    }
}
