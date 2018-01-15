using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YourTurnMyTurn.Models
{
    public class Group
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public ISet<string> People { get; set; }
    }
}
