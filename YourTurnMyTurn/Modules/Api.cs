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
            /* allow CORS */
            Options("/{catchAll*}", parameters => new Response {StatusCode = HttpStatusCode.Accepted});

            After += (Context) =>
            {
                Context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                Context.Response.Headers.Add("Access-Control-Allow-Methods", "PUT, GET, POST, DELETE, OPTIONS");
                Context.Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, x-requested-with, Authorization, Accept, Origin");
            };
            /* CORS end */

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
    