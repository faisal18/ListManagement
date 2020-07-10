using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace ListManagement
{
    public partial class DSToXML : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
       
protected void btnWriter_Click1(object sender, EventArgs e)
{
    String strConnString = ConfigurationManager.ConnectionStrings["LMUConnectionString"].ConnectionString;


    string mySelectQuery = "Select * From tblCodes1";

    System.IO.FileStream myFileStream = new System.IO.FileStream
                                  ("c:\\mySchema_1.3.xml", System.IO.FileMode.Create);
    System.Xml.XmlTextWriter MyXmlTextWriter = new System.Xml.XmlTextWriter
                                         (myFileStream, System.Text.Encoding.Unicode);
    try
    {

        SqlConnection con = new SqlConnection(strConnString);
        SqlDataAdapter daCust = new SqlDataAdapter(mySelectQuery, con);
        DataSet ds = new DataSet();
        ds.DataSetName = "Drugs";
        daCust.Fill(ds, "tblCodes1");
        


        ds.WriteXml(MyXmlTextWriter, XmlWriteMode.WriteSchema);
       
    }
    catch (System.Exception ex)
    {

        //MessageBox.Show(ex.ToString());
    }

    finally
    {

        MyXmlTextWriter.Close();
        myFileStream.Close();
    }




}
    }
}