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
    public class PersonHelper
    {
        private IDbConnection db;

        public PersonHelper(IDbConnectionFactory dbConnectionFactory)
        {
            db = dbConnectionFactory.CreateDbConnection();
            db.Open();
            db.CreateTableIfNotExists<Person>();
            db.Close();
        }

        public Response CreatePerson(string name)
        {
            var person = new Person() { Name = name };
            db.Open();
            db.Insert(person);
            db.Close();

            return new TextResponse(statusCode: HttpStatusCode.OK, contents: $"Person: {person.Id}");
        }
    }
}
