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
        Dictionary<string, object> NextPerson(string groupId);
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
        
        public Dictionary<string, object> NextPerson(string groupId)
        {
            db.Open();
            //Get group member with lowest value
            var q = db.From<PersonToGroup>()
                .Join<Person>()
                .OrderBy(e => e.Value)
                .Select<PersonToGroup, Person>(
                    (ptg, p) => new { p.Id, ptg.Value, p.Name }
                );

            var nextPerson = db.Single<Dictionary<string, object>>(q);
            db.Close();
            return nextPerson;
        }
    }
}
