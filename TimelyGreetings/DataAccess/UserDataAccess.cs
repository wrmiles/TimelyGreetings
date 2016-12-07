using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TimelyGreetings.Repository;

namespace TimelyGreetings.DataAccess
{
    
    public class UserDataAccess
    {
        public Int64 GetLoggedInTGUserIDByUserName(string userName)
        {
            TimelyGreetingsEntities tgDB = new TimelyGreetingsEntities();

            var usrID = (from u in tgDB.Users
                         where u.UserName == userName
                         select u).Single().UserID;

            return Convert.ToInt64(usrID);
        }
    }
}