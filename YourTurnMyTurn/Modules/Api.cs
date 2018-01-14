using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nancy;
using YourTurnMyTurn.Models;
using YourTurnMyTurn.Services;

namespace YourTurnMyTurn.Modules
{
    public class Api : NancyModule
    {
        private static string prefix = "/api/v1/";

        private ITurnResolver turnResolver;
        private GroupHelper groupHelper;

        public Api(ITurnResolver turnResolver, GroupHelper groupHelper)
        {
            this.turnResolver = turnResolver;
            this.groupHelper = groupHelper;

            Get(prefix + "group/{groupId}/next", o => turnResolver.NextPerson(o["groupId"]));
            Post(prefix + "group/{groupId}", o => groupHelper.CreateGroup(new Group() {Name = o["groupId"]}));
        }
    }
}
