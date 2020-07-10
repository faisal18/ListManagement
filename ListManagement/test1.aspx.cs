using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using  System.Web.SessionState;
using System.Xml;

namespace ListManagement
{
    public partial class test1 : System.Web.UI.Page
    {
        String strConnString = ConfigurationManager.ConnectionStrings["LMUConnectionString"].ConnectionString;
       
       // string CurrentXMLVersion = HttpContext.Current.Session["CurrentXMLVersion"].ToString();
       
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnReader_Click1(object sender, EventArgs e)
        {
            
            SqlConnection con = new SqlConnection(strConnString);
            string source;
            string destination;
            string fileBasePath = Server.MapPath("~/");
            string fileName = Path.GetFileNameWithoutExtension(this.FileUpload1.FileName);
            string fullFilePath = fileBasePath + "xml/"+ fileName;
            FileUpload1.SaveAs(Server.MapPath("~/xml/" + fileName));
            string myXMLfile = fullFilePath;
           
            string newXmlVersion = fileName.Split('_')[1];

            if (newXmlVersion != Session["CurrentXMLVersion"].ToString())
            {
                XmlDocument xml = new XmlDocument();
                xml.Load(myXMLfile);
                List<XmlDocument> chunks = new List<XmlDocument>();
                chunks = ChunkDocket(xml, 50);
                foreach (XmlDocument xm in chunks)
                {
                    DataSet ds = new DataSet();
                  
                   // System.IO.FileStream fsReadXml = new System.IO.FileStream
                     //   (myXMLfile, System.IO.FileMode.Open);
                    XmlNodeReader xmlReader = new XmlNodeReader(xm);
                    try
                    {
                        ds.ReadXml(xmlReader);

                    }
                    catch (Exception ex)
                    {

                    }
                    finally
                    {
                      //  fsReadXml.Close();
                    }
                    source = ds.DataSetName;
                    destination = checkLookup(source);
                    DataTable newDrugs = checkExistingForInsert(ds);
                    DataTable DrugsUpdte = checkExistingForUpdate(ds);
                    if (newDrugs.Rows.Count == 0)
                    {
                    }
                    else
                    {
                        insert(newDrugs, destination);
                    }
                    if (DrugsUpdte.Rows.Count == 0)
                    {
                    }
                    else
                    {
                        Update(DrugsUpdte, destination);
                    }
                }
                Session["CurrentXMLVersion"] = newXmlVersion;
            }
           
        }
        private string checkLookup(string source)
        {
            string destination;
            SqlConnection con = new SqlConnection(strConnString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select Destination From tblLookUp where SourceTable = '" + source + "'";
            cmd.Connection = con;          
            con.Open();
            destination = Convert.ToString(cmd.ExecuteScalar());
            return (destination);
                        
        }
        private DataTable checkExistingForInsert(DataSet ds)
        {
            SqlConnection con = new SqlConnection(strConnString);
            DataTable dt = ds.Tables[0];
            DataTable dtAllDrugs = new DataTable();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select * from tblCodes1";
            cmd.Connection = con;
            con.Open();
           
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            
            da.Fill(dtAllDrugs);
            con.Close();
            da.Dispose();
            //to get new codes
            var query1 = dt.AsEnumerable().Select(a => new
            {
                ID = a["CCode"].ToString()
            });

            var query2 = dtAllDrugs.AsEnumerable().Select(b => new
            {
                ID = b["CCode"].ToString()
            });

            var exceptResultsAB = query1.Except(query2);
            DataTable dtforInsert = new DataTable();
            if(exceptResultsAB.Count() == 0)
            {
            }
            else
            {
            dtforInsert = (from a in dt.AsEnumerable()
                                  join ab in exceptResultsAB on a["CCode"].ToString() equals ab.ID
                                  select a).CopyToDataTable();
            }
            return (dtforInsert);
           
        }

        private DataTable checkExistingForUpdate(DataSet ds)
        {
            SqlConnection con = new SqlConnection(strConnString);
            DataTable dt = ds.Tables[0];
            if (dt.Columns.Contains("LongDesc"))
            {
            }
            else
            {
                dt.Columns.Add("LongDesc");
            }

            DataTable dtAllDrugs = new DataTable();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select * from tblCodes1";
            cmd.Connection = con;
            con.Open();

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(dtAllDrugs);
            con.Close();
            da.Dispose();
            // to get updated codes
            var results = from table1 in dt.AsEnumerable()
                          join table2 in dtAllDrugs.AsEnumerable() on table1.Field<string>("CCode") equals table2.Field<string>("CCode")
                          where table1.Field<string>("cDesc") != table2.Field<string>("cDesc") || table1.Field<string>("LongDesc") != table2.Field<string>("LongDesc") || table1.Field<String>("cType") != table2.Field<String>("cType")
                          select table1;
            DataTable dtforUpdate = new DataTable();

            if (results.Count() == 0)
            {
            }
            else
            {
                dtforUpdate = results.CopyToDataTable();
            }
            return (dtforUpdate);
        }
        private void insert(DataTable newDrugs, string destination)
        {
            if (newDrugs.Columns.Contains("LongDesc"))
            {
            }
            else
            {
                newDrugs.Columns.Add("LongDesc");
            }
            if (newDrugs.Columns.Contains("VidalGeneric"))
            {
            }
            else
            {
                newDrugs.Columns.Add("VidalGeneric");
            }
            SqlBulkCopy sqlBulk = new SqlBulkCopy(strConnString);

            sqlBulk.DestinationTableName = destination;
            sqlBulk.ColumnMappings.Add("CCode", "CCode");
            sqlBulk.ColumnMappings.Add("cDesc", "cDesc");
            sqlBulk.ColumnMappings.Add("LongDesc", "LongDesc");
            sqlBulk.ColumnMappings.Add("cType", "cType");
            sqlBulk.ColumnMappings.Add("ID", "ID");
            sqlBulk.ColumnMappings.Add("VidalGeneric", "VidalGeneric");


            sqlBulk.WriteToServer(newDrugs);

 
        }
        private void Update(DataTable DrugsUpdate, string destination)
        {
            SqlConnection conn = new SqlConnection(strConnString);
          

            string upStmt = "Update tblCodes1 Set CDesc= @Cdesc,LongDesc = @LongDesc,cType = @cType,ID = @ID, VidalGeneric =@VidalGeneric  Where CCode =@CCode";
            SqlCommand cmd = new SqlCommand(upStmt, conn);                           
            conn.Open();
            cmd.Parameters.AddWithValue("@CCode","");
            cmd.Parameters.AddWithValue("@Cdesc", "");
            cmd.Parameters.AddWithValue("@LongDesc","");
            cmd.Parameters.AddWithValue("@cType", "");
            cmd.Parameters.AddWithValue("@ID", 0);
            cmd.Parameters.AddWithValue("@VidalGeneric", 0);
                foreach(DataRow dr in DrugsUpdate.Rows)
                {
                cmd.Parameters["@CCode"].Value= dr["CCode"];
                cmd.Parameters["@Cdesc"].Value = dr["CDesc"];
                cmd.Parameters["@LongDesc"].Value = dr["LongDesc"];
                cmd.Parameters["@cType"].Value = dr["cType"];
                cmd.Parameters["@ID"].Value = dr["ID"];
                cmd.Parameters["@VidalGeneric"].Value = dr["VidalGeneric"];
                cmd.ExecuteNonQuery();
                 }

            conn.Close();
        
    
        }
        public class tblCodes
        {
            public string CCode { get; set; }
            public string cDesc { get; set; }
            public string LongDesc {get;set;}
            public string cType { get; set; }
            public int ID { get; set; }
            public int VidalGeneric { get; set; }
        }
        public List<XmlDocument> ChunkDocket(XmlDocument docket, int chunkSize)
        {
            List<XmlDocument> newDockets = new List<XmlDocument>();
            //            
            int orderCount = docket.SelectNodes("//Drugs/tblCodes1").Count;
            int chunkStart = 0;
            XmlDocument newDocket = null;
            XmlElement root = null;
            XmlNodeList chunk = null;

            while (chunkStart < orderCount)
            {
                newDocket = new XmlDocument();
                root = newDocket.CreateElement("Drugs");
                newDocket.AppendChild(root);

                chunk = docket.SelectNodes(String.Format("//Drugs/tblCodes1[position() > {0} and position() <= {1}]", chunkStart, chunkStart + chunkSize));

                chunkStart += chunkSize;

                XmlNode targetNode = null;
                foreach (XmlNode c in chunk)
                {
                    targetNode = newDocket.ImportNode(c, true);
                    root.AppendChild(targetNode);
                }

                newDockets.Add(newDocket);
            }

            return newDockets;
        }

    }
}