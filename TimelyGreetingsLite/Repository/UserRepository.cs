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
    public class UserRepository
    {
        public UserRepository() { }

        public SqlConnection con;
        //To Handle connection related activities      
        private void MakeSqlConnection()
        {
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["TimelyGreetingsConnection"].ToString();
                con = new SqlConnection(constr);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            

        }
        //To Add User details      
        public void AddUser(User objUsr)
        {

            //Adding the User      
            try
            {
                MakeSqlConnection();
                con.Open();
                var _params = new DynamicParameters();
                _params.Add("@UserName", objUsr.UserName);
                _params.Add("@PW", objUsr.PW);
                _params.Add("@FirstName", objUsr.FirstName);
                _params.Add("@LastName", objUsr.LastName);
                _params.Add("@EmailAddress", objUsr.EmailAddress);
                _params.Add("@Status", 1, DbType.Int16);
                _params.Add("@UserType", 1, DbType.Int16);
                
                con.Execute("AddUser", _params, commandType: CommandType.StoredProcedure);

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
        //To view ALL Recipient with generic list       
        public List<User> GetAllUsers()
        {
            try
            {
                MakeSqlConnection();
                con.Open();
                IList<User> usrList = SqlMapper.Query<User>(con, "GetUsers").ToList();

                return usrList.ToList();
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



        //To Get User By ID     
        public User GetUserByID(Int64 userID)
        {
            User usrDetail = new User();
            try
            {
                MakeSqlConnection();
                con.Open();
                var _params = new DynamicParameters();
                _params.Add("@UserID", userID);

                //User usrDetail = SqlMapper.Query<User>(con, "GetUserByID", _params).SingleOrDefault();
                

                usrDetail = con.Query<User>("GetUserByID", _params, commandType: CommandType.StoredProcedure).FirstOrDefault();
                
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

            return usrDetail;
        }

        // AUTHENTICATE USER
        //        [dbo].[AuthenticateUser]
        //        @UserID bigint,
        //@PW varchar(20)
        public Int64? AuthenticateUser(string userName, string pw)
        {
            Int64? userIdReturned = null;
            try
            {
                MakeSqlConnection();
                con.Open();
                var _params = new DynamicParameters();
                _params.Add("@UserName", userName);
                _params.Add("@PW", pw);

                userIdReturned = con.Query<int>("AuthenticateUser", _params, commandType: CommandType.StoredProcedure).FirstOrDefault();

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

            return userIdReturned;
        }


        //To Update Employee details      
        public void UpdateUser(User objUsr)
        {
            try
            {
                MakeSqlConnection();
                con.Open();

                var _params = new DynamicParameters();
                _params.Add("@UserID", objUsr.UserID);
                _params.Add("@UserName", objUsr.UserName);
                _params.Add("@PW", objUsr.PW);
                _params.Add("@FirstName", objUsr.FirstName);
                _params.Add("@LastName", objUsr.LastName);
                _params.Add("@EmailAddress", objUsr.EmailAddress);
                _params.Add("@Status", 1, DbType.Int16);
                _params.Add("@UserType", 1, DbType.Int16);

                con.Execute("UpdateUser", _params, commandType: CommandType.StoredProcedure);
                con.Close();
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
        //To delete User     
        public bool DeleteUser(int Id)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@UserID", Id);
                MakeSqlConnection();
                con.Open();
                con.Execute("DeleteUser", param, commandType: CommandType.StoredProcedure);

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