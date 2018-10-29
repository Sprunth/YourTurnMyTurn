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
            //TODO: handle case of group member getting 
            db.Open();
            var groupMember = new PersonToGroup { GroupId = groupId, PersonId = personId, ContributedValue = 0 };
            db.Insert(groupMember);
            db.Close();
            return new TextResponse(statusCode: HttpStatusCode.OK, contents: $"{groupMember.Id}");  //returns Id unique to that person in that group
        }

        public decimal AddValueToPersonInGroup(string groupMemberId, decimal value)
        {
            db.Open();
            db.UpdateAdd(() => new PersonToGroup { ContributedValue = value }, where: entry => entry.Id == groupMemberId);
            PersonToGroup updatedUserInGroup = db.Single<PersonToGroup>(entry => entry.Id == groupMemberId);
            db.Close();
            return updatedUserInGroup.ContributedValue;
        }

        public List<Dictionary<string, object>> GroupMemberInfo(string groupId)
        {
            db.Open();
            var q = db.From<PersonToGroup>()
                .Where(personToGroup => personToGroup.GroupId == groupId)
                .Join<Person>()
                .Select<PersonToGroup, Person>(
                    //return the id for their entry in the group for ContributedValue modification
                    (ptg, p) => new {ptg.Id, ptg.ContributedValue, p.Name}
                );
            var groupMembers = db.Select<Dictionary<string, object>>(q);
            db.Close();
            return groupMembers;
        }
    }
}
