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
    public class OccassionTypeRepository
    {
        public OccassionTypeRepository() { }

        public SqlConnection con;
        //To Handle connection related activities      
        private void connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["TimelyGreetingsConnection"].ToString();
            con = new SqlConnection(constr);

        }
        //To Add Occassion Type details      
        public void AddOccassionType(OccassionType objOcc)
        {

            //Adding the OccassionType      
            try
            {
                connection();
                con.Open();
                con.Execute("AddAttachment", objOcc, commandType: CommandType.StoredProcedure);

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
        //To view ALL OccassionType with generic list       
        public List<OccassionType> GetAllOccassionTypes()
        {
            try
            {
                connection();
                con.Open();
                List<OccassionType> occList = SqlMapper.Query<OccassionType>(con, "GetOccassionTypes").ToList();

                return occList;
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



        //To Get OccassionType By ID     
        public OccassionType GetOccassionTypeByID(int occTypeID)
        {
            try
            {
                connection();
                con.Open();
                var _params = new DynamicParameters();
                _params.Add("OccassionTypeID", occTypeID);
                OccassionType occDetail = SqlMapper.Query<OccassionType>(con, "GetAttachmentByID", _params).SingleOrDefault();

                return occDetail;
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

        //To Update OccassionType details      
        public void UpdateOccassionType(OccassionType objOcc)
        {
            try
            {
                connection();
                con.Open();
                con.Execute("UpdateOccassionType", objOcc, commandType: CommandType.StoredProcedure);
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
        //To delete OccassionType     
        public bool DeleteOccassionType(int Id)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@OccassionTypeID", Id);
                connection();
                con.Open();
                con.Execute("DeleteOccassionType", param, commandType: CommandType.StoredProcedure);

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