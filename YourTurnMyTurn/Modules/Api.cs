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
        private PersonHelper personHelper;

        public Api(ITurnResolver turnResolver, GroupHelper groupHelper, PersonHelper personHelper)
        {
            this.turnResolver = turnResolver;
            this.groupHelper = groupHelper;
            this.personHelper = personHelper;

            Get(prefix + "group/{groupId}/next", o => turnResolver.NextPerson(o["groupId"]));
            Get(prefix + "group/{groupId}", o => groupHelper.GroupMemberInfo(o.groupId));
            Post(prefix + "group/{name}", o => groupHelper.CreateGroup(o.name));
            Post(prefix + "group/{groupId}/add/{pid}", o => groupHelper.AddPersonToGroup(o.groupId, o.pid));
            Post(prefix + "group/{groupMemberId}/contribute/{value}", o => groupHelper.AddValueToPersonInGroup(o.groupMemberId, o.value));

            Post(prefix + "person/{name}", o => personHelper.CreatePerson(o.name));
        }
    }
}
    