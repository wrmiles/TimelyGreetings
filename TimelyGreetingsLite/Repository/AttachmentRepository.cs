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
    public class AttachmentRepository
    {
        //http://www.c-sharpcorner.com/uploadfile/0c1bb2/crud-operations-in-asp-net-mvc-5-using-dapper-orm/

        public AttachmentRepository() { }

        public SqlConnection con;
        //To Handle connection related activities      
        private void connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["TimelyGreetingsConnection"].ToString();
            con = new SqlConnection(constr);

        }
        //To Add Attachment details      
        public void AddAttachment(Attachment objAttach)
        {

            //Adding the Attachment      
            try
            {
                connection();
                con.Open();
                var _params = new DynamicParameters();
                _params.Add("@AttachmentType", objAttach.AttachmentType);
                _params.Add("@AttachmentSize", objAttach.AttachmentSize);
                _params.Add("@AttachmentData", objAttach.AttachmentData);
                _params.Add("@AttachmentURL", objAttach.AttachmentURL);
                _params.Add("@GreetingID", objAttach.GreetingID);
                
                con.Execute("AddAttachment", _params, commandType: CommandType.StoredProcedure);
                
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
        //To view ALL Attachments with generic list       
        public List<Attachment> GetAllAttachments()
        {
            try
            {
                connection();
                con.Open();
                IList<Attachment> attList = SqlMapper.Query<Attachment>(con, "GetAttachments").ToList();
                
                return attList.ToList();
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

        //To view Attachments by Greeting ID     
        public List<Attachment> GetAttachmentsByGreetingID(Int64 greetingId)
        {
            try
            {
                connection();
                con.Open();
                var _params = new DynamicParameters();
                _params.Add("@GreetingID", greetingId);
                IList<Attachment> attList = SqlMapper.Query<Attachment>(con, "GetAttachmentByGreetingID", _params, commandType: CommandType.StoredProcedure).ToList();

                return attList.ToList();
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
        public Attachment GetAttachmentByID(Int64 attchId)
        {
            try
            {
                connection();
                con.Open();
                var _params = new DynamicParameters();
                _params.Add("@AttachmentID", attchId);
                Attachment attDetail = SqlMapper.Query<Attachment>(con, "GetAttachmentByID", _params, commandType: CommandType.StoredProcedure).SingleOrDefault();

                return attDetail;
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

        //To Update Employee details      
        public void UpdateAttachment(Attachment objAttach)
        {
            try
            {
                connection();
                con.Open();
                con.Execute("UpdateAttachment", objAttach, commandType: CommandType.StoredProcedure);
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
        //To delete Attachment     
        public bool DeleteAttachment(Int64 Id)
        {
            try
            {                
                connection();
                con.Open();
                DynamicParameters param = new DynamicParameters();
                param.Add("@AttachmentID", Id);
                con.Execute("DeleteAttachment", param, commandType: CommandType.StoredProcedure);
                
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