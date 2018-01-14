using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nancy;
using YourTurnMyTurn.Services;

namespace YourTurnMyTurn.Modules
{
    public class Api : NancyModule
    {
        private static string prefix = "/api/v1/";

        private ITurnResolver turnResolver;

        public Api(ITurnResolver turnResolver)
        {
            this.turnResolver = turnResolver;

            Get(prefix + "{groupId}/next", o => turnResolver.NextPerson(o.groupId));
        }
    }
}
