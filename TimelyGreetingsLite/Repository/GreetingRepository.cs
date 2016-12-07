using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TimelyGreetingsLite.Models;

namespace TimelyGreetingsLite.Repository
{
    public class GreetingRepository
    {
        public GreetingRepository() { }

        public SqlConnection con;
        //To Handle connection related activities      
        private void connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["TimelyGreetingsConnection"].ToString();
            con = new SqlConnection(constr);

        }
        //To Add Greeting details      
        public void AddGreeting(Greeting objGreet)
        {

            //Add Greeting      
            try
            {
                connection();
                con.Open();
                var _params = new DynamicParameters();
                _params.Add("@UserID", objGreet.UserID);
                _params.Add("@OccassionTypeID", objGreet.OccassionTypeID);                
                _params.Add("@DateToSend", objGreet.DateToSend);                
                _params.Add("@Subject", objGreet.Subject);
                _params.Add("@BodyText", objGreet.BodyText);
                con.Execute("AddGreeting", _params, commandType: CommandType.StoredProcedure);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

        }
        //To view ALL Greetings with generic list       
        public List<Greeting> GetAllGreetings()
        {
            try
            {
                connection();
                con.Open();
                IList<Greeting> greetList = SqlMapper.Query<Greeting>(con, "GetGreetings").ToList();

                return greetList.ToList();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }

        //Get Greetings By UserID        
        public List<Greeting> GetGreetingsByUserID(Int64 userID)
        {
            try
            {
                connection();
                con.Open();
                var _params = new DynamicParameters();
                _params.Add("UserID", userID);
                IList<Greeting> greetList = SqlMapper.Query<Greeting>(con, "GetGreetingsByUserID", _params, commandType: CommandType.StoredProcedure).ToList();

                return greetList.ToList();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }


        //To Get Attachment By ID     
        public Greeting GetGreetingByID(Int64 greetingID)
        {
            try
            {
                connection();
                con.Open();
                var _params = new DynamicParameters();
                _params.Add("GreetingID", greetingID);
                Greeting greetDetail = SqlMapper.Query<Greeting>(con, "GetGreetingByID", _params, commandType: CommandType.StoredProcedure).SingleOrDefault();

                return greetDetail;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }

        //To Update Greeting details      
        public void UpdateGreeting(Greeting objGreet)
        {
            try
            {
                connection();
                con.Open();
                var _params = new DynamicParameters();
                _params.Add("@GreetingID", objGreet.GreetingID);
                _params.Add("@OccassionTypeID", objGreet.OccassionTypeID);                
                _params.Add("@DateToSend", objGreet.DateToSend);                
                _params.Add("@Subject", objGreet.Subject);
                _params.Add("@BodyText", objGreet.BodyText);

                con.Execute("UpdateGreeting", _params, commandType: CommandType.StoredProcedure);
                
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

        }
        //To delete Attachment     
        public bool DeleteGreeting(int Id)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@GreetingID", Id);
                connection();
                con.Open();
                con.Execute("DeleteGreeting", param, commandType: CommandType.StoredProcedure);

                return true;
            }
            catch (Exception ex)
            {
                //Log error as per your need       
                throw ex;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
    }
}