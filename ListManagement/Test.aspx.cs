using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Xml;
using System.Data.SqlClient;
using System.Configuration;

namespace ListManagement
{ 
    public partial class test : System.Web.UI.Page
    {
        LMUService.DataSyncServiceClient aa = new LMUService.DataSyncServiceClient();
        LMUService.authorizationCode info = new LMUService.authorizationCode();
        String strConnString = ConfigurationManager.ConnectionStrings["LMUConnectionString"].ConnectionString;
            
        protected void Page_Load(object sender, EventArgs e)
        {
            string listName = (string)Session["listName"];
            info.key = "l0KAsQFd8qAsWdKpaf1Lgu9D2ZuAVz72";
            info.username = "dhpouser";

            LMUService.versionResult result = aa.currentVersion(info, listName);
            LMUService.changesResult cr = aa.findChanges(info, listName, 0, result.version);
            string source = listName;
            string destination;
            destination = checkLookup(source);
            List<int> lst = cr.records.ToList();
            List<List<int>> splitdd = new List<List<int>>();
            splitdd = splitData(300, lst);
            int currentVersion = getCurrentVersion(source);
            if (currentVersion < result.version)
            {
                foreach (List<int> node in splitdd)
                {
                    int[] finalresult;
                    finalresult = node.ToArray();
                    LMUService.recordsResult rr = aa.findRecords(info, listName, result.version, finalresult);
                    DataSet ds = new DataSet();
                    XmlDocument xm = new XmlDocument();
                    xm.LoadXml(rr.xmlContent);
                    XmlNodeReader xmlReader = new XmlNodeReader(xm);
                    ds.ReadXml(xmlReader);

                    DataTable newDrugs = checkExistingForInsert(ds, destination,source);
                    DataTable updatedDrugs = checkforUpdatedDrugs(ds, destination);
                    if (newDrugs.Rows.Count == 0)
                    {
                    }
                    else
                    {
                        insert(newDrugs, destination, source);
                    }
                    if (updatedDrugs.Rows.Count == 0)
                    {
                    }
                    else
                    {
                        update(updatedDrugs, destination, source);
                    }
                }
                setUpdatedVersion(result.version,source);
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
        private DataTable checkExistingForInsert(DataSet ds,string destination,string source)
        {          
            DataTable dt = ds.Tables[0];
            if (source == "Drugs")
            {
                dt.Columns.Add("cType");
                foreach (DataRow d in dt.Rows)
                {
                    d["cType"] = "Drugs";
                }
            }
            else if (source == "DubaiSpecialServices")
            {
                dt.Columns.Add("cType");
                foreach (DataRow d in dt.Rows)
                {
                    d["cType"] = "SERVICES";
                }
            }
            else if (source == "DhaCPT")
            {
                dt.Columns.Add("cType");
                foreach (DataRow d in dt.Rows)
                {
                    d["cType"] = "CPT";
                }
            }
            else if (source == "DhaDenialCodes")
            {
                dt.Columns.Add("Type");
                //if(dt.Select("code" = 'AUTH%'))
            }
          
            //to get new codes
            if (dt.Select("record_status = 'ADDED'").Count() == 0)
            {
                DataTable dtforInsert = new DataTable();
                return (dtforInsert);
            }
            else
            {

                DataTable dtforInsert = new DataTable();
                dtforInsert = dt.Select("record_status = 'ADDED'").CopyToDataTable();
                return (dtforInsert);
            }

        }
        private void insert(DataTable newDrugs,string destination,string source)
        {
            
            SqlBulkCopy sqlBulk = new SqlBulkCopy(strConnString);
            if (source == "Drugs")
            {
                sqlBulk.DestinationTableName = destination;
                sqlBulk.ColumnMappings.Add("ddcCode", "cCode");
                sqlBulk.ColumnMappings.Add("tradeName", "cDesc");
                sqlBulk.ColumnMappings.Add("cType", "cType");  
               
                sqlBulk.WriteToServer(newDrugs);
            }
            else if (source == "DubaiSpecialServices")
            {
                sqlBulk.DestinationTableName = destination;
                sqlBulk.ColumnMappings.Add("code", "cCode");
                sqlBulk.ColumnMappings.Add("description", "cDesc");
                sqlBulk.ColumnMappings.Add("detailedDescription", "LongDesc");
                sqlBulk.ColumnMappings.Add("cType", "cType");
                sqlBulk.WriteToServer(newDrugs);
            }
            else if (source == "DhaCPT")
            {
                sqlBulk.DestinationTableName = destination;
                sqlBulk.ColumnMappings.Add("code", "cCode");
                sqlBulk.ColumnMappings.Add("shortdescription", "cDesc");
                sqlBulk.ColumnMappings.Add("fullDescription", "LongDesc");
                sqlBulk.ColumnMappings.Add("cType", "cType");
                sqlBulk.WriteToServer(newDrugs);
            }
        }
        
        private DataTable checkforUpdatedDrugs(DataSet ds,string destination)
        {
            DataTable dt = ds.Tables[0];

            //to get updated codes
            if (dt.Select("record_status = 'EDITED'").Count() == 0)
            {
                DataTable dtforUpdate = new DataTable();
                return (dtforUpdate);
            }
            else
            {
                DataTable dtforUpdate = new DataTable();
                dtforUpdate = dt.Select("record_status = 'EDITED'").CopyToDataTable();
                return (dtforUpdate);
            }

        }
        private void update(DataTable DrugsUpdate,string destination,string source)
        {
            SqlConnection conn = new SqlConnection(strConnString);

            if (source == "Drugs")
            {
                string upStmt = "Update tblCodes1 Set cDesc = @cDesc  Where cCode= @cCode";
                SqlCommand cmd = new SqlCommand(upStmt, conn);
                conn.Open();
                cmd.Parameters.AddWithValue("@cCode", "");

                cmd.Parameters.AddWithValue("@cDesc", "");
                
                foreach (DataRow dr in DrugsUpdate.Rows)
                {
                    cmd.Parameters["@cCode"].Value = dr["ddcCode"];
                    cmd.Parameters["@cDesc"].Value = dr["tradeName"];
                    
                    cmd.ExecuteNonQuery();
                }

                conn.Close();
            }
            else if (source == "DubaiSpecialServices")
            {
                string upStmt = "Update tblCodes1 Set cDesc = @cDesc,LongDesc = @LongDesc  Where cCode= @cCode";
                SqlCommand cmd = new SqlCommand(upStmt, conn);
                conn.Open();
                cmd.Parameters.AddWithValue("@cCode", "");
                cmd.Parameters.AddWithValue("@cDesc", "");
                cmd.Parameters.AddWithValue("@LongDesc","");

                foreach (DataRow dr in DrugsUpdate.Rows)
                {
                    cmd.Parameters["@cCode"].Value = dr["code"];
                    cmd.Parameters["@cDesc"].Value = dr["description"];
                    cmd.Parameters["@LongDesc"].Value = dr["detailedDescription"];

                    cmd.ExecuteNonQuery();
                }

                conn.Close();
            }
            else if (source == "DhaCPT")
            {
                string upStmt = "Update tblCodes1 Set cDesc = @cDesc,LongDesc = @LongDesc  Where cCode= @cCode";
                SqlCommand cmd = new SqlCommand(upStmt, conn);
                conn.Open();
                cmd.Parameters.AddWithValue("@cCode", "");
                cmd.Parameters.AddWithValue("@cDesc", "");
                cmd.Parameters.AddWithValue("@LongDesc", "");

                foreach (DataRow dr in DrugsUpdate.Rows)
                {
                    cmd.Parameters["@cCode"].Value = dr["code"];
                    cmd.Parameters["@cDesc"].Value = dr["shortdescription"];
                    cmd.Parameters["@LongDesc"].Value = dr["fullDescription"];

                    cmd.ExecuteNonQuery();
                }

                conn.Close();
            }


        }

        public List<List<int>> splitData(int width, List<int> dd) 
        { 
             List<List<int>> dds = new List<List<int>>();

            // Determine how many lists are required 
            int numberOfLists = (dd.Count / width);

            for (int i = 0; i <= numberOfLists; i++) 
            { 
                 List<int> newdd = new List<int>(); 
                 newdd = dd.Skip(i * width).Take(width).ToList(); 
                 dds.Add(newdd); 
            }

             return dds; 
        }
        private int getCurrentVersion(string source)
        {
            int currentVersion=0;
           using(SqlConnection conn = new SqlConnection(strConnString))
           {
               SqlCommand cmd = new SqlCommand("SELECT LastUpdatedVersion FROM tblLookUp WHERE SourceTable = '" + source + "'", conn);
                try
                {
                    conn.Open();
                    currentVersion = (int)cmd.ExecuteScalar();
                }
                catch (Exception ex)
                {         
                }
                return (currentVersion);
           }           
        }
        private void setUpdatedVersion(int newVersion,string source)
        {
            using (SqlConnection conn = new SqlConnection(strConnString))
            {
                SqlCommand cmd = new SqlCommand("Update tblLookUp Set LastUpdatedVersion = "+newVersion+" WHERE SourceTable = '" + source + "'", conn);
                try
                {
                    conn.Open();
                    
                }
                catch (Exception ex)
                {
                }
                cmd.ExecuteNonQuery();
            }           
        }
    }
}