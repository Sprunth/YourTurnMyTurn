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
    public class GroupHelper
    {
        private IDbConnection db;

        public GroupHelper(IDbConnectionFactory dbConnectionFactory)
        {
            db = dbConnectionFactory.CreateDbConnection();
        }

        public bool CreateGroup(Group group)
        {
            db.Open();
            db.Insert(group);
            db.Close(); 

            return true;  // todo: when false?
        }
    }
}
