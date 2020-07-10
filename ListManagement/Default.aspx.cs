using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

namespace ListManagement
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            int k=0 ;
            String strConnString = ConfigurationManager.ConnectionStrings["LMUConnectionString"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(strConnString);
            try
            {
                for ( int x=0; x< 20000; x++)
                {
                    k=x;
                    sqlConnection = new SqlConnection(strConnString);
                    sqlConnection.Open();

                    SqlCommand sqlCommand = new SqlCommand("select top (1) payerid from payers", sqlConnection);
                    int id = (int)sqlCommand.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                k.ToString();
                throw ex;
                
            }
        }
    }
}
