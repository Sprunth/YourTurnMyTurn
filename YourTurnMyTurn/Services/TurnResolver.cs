using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YourTurnMyTurn.Models;

namespace YourTurnMyTurn.Services
{
    public interface ITurnResolver
    {
        Person NextPerson(string groupId);
    }

    public class TurnResolver : ITurnResolver
    {
        public Person NextPerson(string groupId)
        {
            return new Person(){Name = "rando name"};
        }
    }
}
