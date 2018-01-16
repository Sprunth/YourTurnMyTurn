using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Nancy;
using Nancy.Responses;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using YourTurnMyTurn.Models;

namespace YourTurnMyTurn.Services
{
    public class GroupHelper
    {
        private IDbConnection db;

        public GroupHelper(IDbConnectionFactory dbConnectionFactory)
        {
            db = dbConnectionFactory.CreateDbConnection();
        }

        public Response CreateGroup(string name)
        {
            var group = new Group() {Name = name};
            db.Open();
            db.Insert(group);
            db.Close();

            return new TextResponse(statusCode: HttpStatusCode.OK, contents: $"Group: {group.Id}");
        }
    }
}
