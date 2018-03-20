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
            db.Open();
            db.CreateTableIfNotExists<Group>();
            db.CreateTableIfNotExists<PersonToGroup>();
            db.Close();
        }

        public Response CreateGroup(string name)
        {
            var group = new Group() { Name = name };
            db.Open();
            db.Insert(group);
            db.Close();

            return new TextResponse(statusCode: HttpStatusCode.OK, contents: $"Group: {group.Id}");
        }

        public Response AddPersonToGroup(string groupId, string personId)
        {
            db.Open();
            var groupMember = new PersonToGroup { GroupId = groupId, PersonId = personId, ContributedValue = 0 };  //TODO: determine how to assign value is group has been running and members have value
            db.Insert(groupMember);
            db.Close();
            return new TextResponse(statusCode: HttpStatusCode.OK, contents: $"{groupMember}");
        }

        public List<Dictionary<string, object>> GroupMemberInfo(string groupId)
        {
            db.Open();

            var q = db.From<PersonToGroup>()
                .Join<Person>()
                .Select<PersonToGroup, Person>(
                    (ptg, p) => new { p.Id, ptg.ContributedValue, p.Name }
                );
            
            var groupMembers = db.Select<Dictionary<string, object>>(q);
            db.Close();
            return groupMembers;
        }
    }
}
