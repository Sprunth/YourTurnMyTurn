using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YourTurnMyTurn.Models
{
    [CompositeIndex(nameof(PersonId), nameof(GroupId))]
    public class PersonToGroup
    {
        public string Id { get; set; }

        [Index]
        public string PersonId { get; set; }

        [Index]
        public string GroupId { get; set; }

        public decimal Value { get; set;  }
    }
}
