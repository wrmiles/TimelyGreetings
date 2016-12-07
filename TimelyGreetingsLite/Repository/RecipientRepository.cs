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
    public class RecipientRepository
    {
        public RecipientRepository() { }

        public SqlConnection con;
        //To Handle connection related activities      
        private void connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["TimelyGreetingsConnection"].ToString();
            con = new SqlConnection(constr);

        }
        //To Add Recipient details      
        public void AddRecipient(Recipient objRecip)
        {

            //Adding the Recipient      
            try
            {
                connection();
                con.Open();
                con.Execute("AddRecipient", objRecip, commandType: CommandType.StoredProcedure);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }

        }
        //To view ALL Recipient with generic list       
        public List<Recipient> GetAllRecipients()
        {
            try
            {
                connection();
                con.Open();
                IList<Recipient> recipList = SqlMapper.Query<Recipient>(con, "GetRecipients").ToList();

                return recipList.ToList();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
        }

        //To view Recipients by Greeting ID     
        public List<Recipient> GetRecipientsByGreetingID()
        {
            try
            {
                connection();
                con.Open();
                IList<Recipient> greetingRecipientList = SqlMapper.Query<Recipient>(con, "GetRecipientByGreetingID").ToList();

                return greetingRecipientList.ToList();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
        }

        //To Get Recipient By ID     
        public Recipient GetRecipientByID()
        {
            try
            {
                connection();
                con.Open();
                Recipient recipDetail = SqlMapper.Query<Recipient>(con, "GetRecipientByID").SingleOrDefault();

                return recipDetail;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
        }

        //To Update Employee details      
        public void UpdateRecipient(Recipient objRecip)
        {
            try
            {
                connection();
                con.Open();
                con.Execute("UpdateRecipient", objRecip, commandType: CommandType.StoredProcedure);
                con.Close();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                con.Close();
            }

        }
        //To delete Recipient     
        public bool DeleteRecipient(int Id)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@RecipientID", Id);
                connection();
                con.Open();
                con.Execute("DeleteRecipient", param, commandType: CommandType.StoredProcedure);

                return true;
            }
            catch (Exception ex)
            {
                //Log error as per your need       
                throw ex;
            }
            finally
            {
                con.Close();
            }
        }
    }
}