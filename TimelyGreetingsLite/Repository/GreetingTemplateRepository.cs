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
    public class GreetingTemplateRepository
    {
        public GreetingTemplateRepository() { }

        public SqlConnection con;
        //To Handle connection related activities      
        private void connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["TimelyGreetingsConnection"].ToString();
            con = new SqlConnection(constr);

        }
        //To Add Greeting details      
        public void AddGreetingTemplate(GreetingTemplate objGreetTmplt)
        {

            //Add Greeting      
            try
            {
                connection();
                con.Open();
                var _params = new DynamicParameters();
                _params.Add("@GreetingTemplateName", objGreetTmplt.GreetingTemplateName);
                _params.Add("@OccassionTypeID", objGreetTmplt.OccassionTypeID);                
                _params.Add("@FrontCoverBackgroundColor", objGreetTmplt.FrontCoverBackgroundColor);                
                _params.Add("@FrontCoverBackgroundPatternFilePathName", objGreetTmplt.FrontCoverBackgroundPatternFilePathName);
                _params.Add("@FrontCoverImageFilePathName", objGreetTmplt.FrontCoverImageFilePathName);
                _params.Add("@InsideBackgroundColor", objGreetTmplt.InsideBackgroundColor);
                _params.Add("@InsideBackgroundPatternFilePathName", objGreetTmplt.InsideBackgroundPatternFilePathName);
                _params.Add("@InsideImageFilePathName", objGreetTmplt.InsideImageFilePathName);
                _params.Add("@FrontCoverFont", objGreetTmplt.FrontCoverFont);
                _params.Add("@FrontCoverFontSize", objGreetTmplt.FrontCoverFontSize);
                _params.Add("@FrontCoverFontColor", objGreetTmplt.FrontCoverFontColor);
                _params.Add("@FrontCoverFontWeight", objGreetTmplt.FrontCoverFontWeight);
                _params.Add("@InsideFont", objGreetTmplt.InsideFont);
                _params.Add("@InsideFontSize", objGreetTmplt.InsideFontSize);
                _params.Add("@InsideFontColor", objGreetTmplt.InsideFontColor);
                _params.Add("@InsideFontWeight", objGreetTmplt.InsideFontWeight);
                _params.Add("@MusicPathFileName", objGreetTmplt.MusicPathFileName);

                con.Execute("AddGreetingTemplate", _params, commandType: CommandType.StoredProcedure);

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
        public List<GreetingTemplate> GetAllGreetingTemplates()
        {
            try
            {
                connection();
                con.Open();
                IList<GreetingTemplate> greetTmpList = SqlMapper.Query<GreetingTemplate>(con, "GetGreetingTemplates").ToList();

                return greetTmpList.ToList();
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

        //To view ALL GreetingTemplates By OccassionType ID       
        public List<GreetingTemplate> GetGreetingTemplatesByOccassionTypeID(int occassionTypeID)
        {
            try
            {
                connection();
                con.Open();
                var _params = new DynamicParameters();
                _params.Add("@OccassionTypeID", occassionTypeID);
                IList<GreetingTemplate> greetTmpByOccassionList = SqlMapper.Query<GreetingTemplate>(con, "GetGreetingTemplateByOccassionTypeID", _params, commandType: CommandType.StoredProcedure).ToList();

                return greetTmpByOccassionList.ToList();
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



        //To Get Greeting Template By ID     
        public GreetingTemplate GetGreetingTemplateByID(Int64 greetingTemplateID)
        {
            try
            {
                connection();
                con.Open();
                var _params = new DynamicParameters();
                _params.Add("@GreetingTemplateID", greetingTemplateID);
                GreetingTemplate greetTmpltDetail = SqlMapper.Query<GreetingTemplate>(con, "GetGreetingTemplateByID", _params, commandType: CommandType.StoredProcedure).SingleOrDefault();

                return greetTmpltDetail;
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
        public void UpdateGreetingTemplate(GreetingTemplate objGreetTmplt)
        {
            try
            {
                connection();
                con.Open();
                var _params = new DynamicParameters();
                _params.Add("@GreetingTemplateID", objGreetTmplt.GreetingTemplateID);
                _params.Add("@GreetingTemplateName", objGreetTmplt.GreetingTemplateName);
                _params.Add("@OccassionTypeID", objGreetTmplt.OccassionTypeID);
                _params.Add("@FrontCoverBackgroundColor", objGreetTmplt.FrontCoverBackgroundColor);
                _params.Add("@FrontCoverBackgroundPatternFilePathName", objGreetTmplt.FrontCoverBackgroundPatternFilePathName);
                _params.Add("@FrontCoverImageFilePathName", objGreetTmplt.FrontCoverImageFilePathName);
                _params.Add("@InsideBackgroundColor", objGreetTmplt.InsideBackgroundColor);
                _params.Add("@InsideBackgroundPatternFilePathName", objGreetTmplt.InsideBackgroundPatternFilePathName);
                _params.Add("@InsideImageFilePathName", objGreetTmplt.InsideImageFilePathName);
                _params.Add("@FrontCoverFont", objGreetTmplt.FrontCoverFont);
                _params.Add("@FrontCoverFontSize", objGreetTmplt.FrontCoverFontSize);
                _params.Add("@FrontCoverFontColor", objGreetTmplt.FrontCoverFontColor);
                _params.Add("@FrontCoverFontWeight", objGreetTmplt.FrontCoverFontWeight);
                _params.Add("@InsideFont", objGreetTmplt.InsideFont);
                _params.Add("@InsideFontSize", objGreetTmplt.InsideFontSize);
                _params.Add("@InsideFontColor", objGreetTmplt.InsideFontColor);
                _params.Add("@InsideFontWeight", objGreetTmplt.InsideFontWeight);
                _params.Add("@MusicPathFileName", objGreetTmplt.MusicPathFileName);

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
        public bool DeleteGreetingTemplate(int Id)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@GreetingTemplateID", Id);
                connection();
                con.Open();
                con.Execute("DeleteGreetingTemplate", param, commandType: CommandType.StoredProcedure);

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