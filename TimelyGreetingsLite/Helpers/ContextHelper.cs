using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TimelyGreetingsLite.Models;

namespace TimelyGreetingsLite.Helpers
{
    public class ContextHelper
    {
        public ContextHelper() { }

        public Int64 GetCurrentUserID(string identityName)
        {
            Int64 userID = 0;
            try
            {
                string userInfo = identityName; // User.Identity.Name;

                string[] userParts = userInfo.Split('|');
                string strUsrID = userParts[1].ToString();

                if (strUsrID != "")
                {
                    userID = Convert.ToInt64(strUsrID);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to get current user id. " + ex.Message, ex.InnerException);
            }
            return userID;

        }
    }
}