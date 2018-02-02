using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using YourTurnMyTurn.Models;

namespace YourTurnMyTurn.Services
{
    public interface ITurnResolver
    {
       Person NextPerson(string groupId);
    }

    public class TurnResolver : ITurnResolver
    {
        private IDbConnection db;

        public TurnResolver(IDbConnectionFactory dbConnectionFactory)
        {
            db = dbConnectionFactory.CreateDbConnection();
            db.Open();
            if (db.CreateTableIfNotExists<Group>())
            {
                //db.Insert(new Group());
            }
            db.Close();
        }
        
        public Person NextPerson(string groupId)
        {
            db.Open();
            db.Close();
            return new Person();
        }

        public List<(string Id, decimal Value, string Name)> GroupMemberInfo(string groupId)
        {
            db.Open();
            var q = db.From<PersonToGroup>()
                .Join<Person>()
                .Select<PersonToGroup, Person>(
                    (ptg, p) => new { p.Id, ptg.Value, p.Name }
                );
            var groupMembers = db.Select<(string Id, decimal Value, string Name)>(q);
            db.Close();
            return groupMembers;
        }
    }
}
