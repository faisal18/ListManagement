using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace ListManagement.Code
{
    public class logException
    {
        String strConnString = ConfigurationManager.ConnectionStrings["LMUConnectionString"].ConnectionString;
        public void storeExcepion(Exception e)
        {
            Createlog log = new Createlog();
            log.ExceptionInnerException = (e.InnerException != null)
                      ? e.InnerException.Message
                      : "";
            log.ExceptionMessage = e.Message;
            log.ExceptionSource = e.Source;
            log.ExceptionStackTrace = e.StackTrace;
            insertException(log.ExceptionSource, log.ExceptionMessage, log.ExceptionStackTrace, log.ExceptionInnerException);
            
        }
        public class Createlog
        {
            public string ExceptionInnerException { get; set; }
            public string ExceptionMessage { get; set; }
            public string ExceptionSource { get; set; }
            public string ExceptionStackTrace { get; set; }
        }
        public void insertException(string Source,string Message,string StackTrace,string InnerException)
        {
            DateTime dt;
            dt = DateTime.Now;
            
            SqlConnection con = new SqlConnection(strConnString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "INSERT temp_exception ([Source],[Message],[InnerException],[StackTrace],[Date]) VALUES (@Source, @Message,@InnerException,@StackTrace,@Date) ";
            cmd.Parameters.Add("@Source", SqlDbType.VarChar);
            cmd.Parameters["@Source"].Value = Source;
            cmd.Parameters.Add("@Message", SqlDbType.VarChar);
            cmd.Parameters["@Message"].Value = Message;
            cmd.Parameters.Add("@InnerException", SqlDbType.VarChar);
            cmd.Parameters["@InnerException"].Value = InnerException;
            cmd.Parameters.Add("@StackTrace", SqlDbType.VarChar);
            cmd.Parameters["@StackTrace"].Value = StackTrace;
            cmd.Parameters.Add("@Date", SqlDbType.DateTime);
            cmd.Parameters["@Date"].Value = dt;
            
            cmd.Connection = con;

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}