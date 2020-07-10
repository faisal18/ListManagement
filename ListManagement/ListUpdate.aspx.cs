using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.SessionState;
using System.Data;
using System.Xml;
using System.Data.SqlClient;
using System.Configuration;
using ListManagement.Code;

namespace ListManagement
{
    public partial class ListUpdate : System.Web.UI.Page
    {
        LMUService.DataSyncServiceClient client = new LMUService.DataSyncServiceClient();

        LMUService.authorizationCode info = new LMUService.authorizationCode();
        String strConnString = ConfigurationManager.ConnectionStrings["LMUConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            string listName = (string)Session["listName"];
            info.key = "l0KAsQFd8qAsWdKpaf1Lgu9D2ZuAVz72";
            info.username = "dhpouser";
            try
            {
                LMUService.versionResult resultVersion = new LMUService.DataSyncServiceClient().currentVersion(info, listName);
                string source = listName;
                int currentVersion = getCurrentVersion(source);
                LMUService.changesResult changes = client.findChanges(info, listName, currentVersion, resultVersion.version);

                string destination;
                destination = checkLookup(source);
                if (changes.records != null)
                {
                    List<int> lst = changes.records.ToList();
                    List<List<int>> splitList = new List<List<int>>();
                    splitList = splitData(300, lst);


                    if (currentVersion < resultVersion.version)
                    {
                        DataTable pubDatatable = new DataTable();

                        pubDatatable = getListRecords(source);

                        if (pubDatatable.Rows.Count > 0)
                        {
                            foreach (List<int> node in splitList)
                            {
                                int[] finalresult;
                                finalresult = node.ToArray();
                                LMUService.recordsResult records = client.findRecords(info, listName, resultVersion.version, finalresult);
                                DataSet ds = new DataSet();
                                XmlDocument xm = new XmlDocument();
                                xm.LoadXml(records.xmlContent);
                                XmlNodeReader xmlReader = new XmlNodeReader(xm);
                                ds.ReadXml(xmlReader);
                                CheckRecordsForInsert checkRecords = new CheckRecordsForInsert();

                                DataTable newRecords = checkRecords.checkExistingForInsert(ds, destination, source, pubDatatable);

                                CheckRecordsForUpdate checkRecordsUpdate = new CheckRecordsForUpdate();
                                DataTable updatedRecords = checkRecordsUpdate.checkExistingForUpdate(ds, destination);
                                if (newRecords.Rows.Count == 0)
                                {
                                }
                                else
                                {
                                    Insert insertFields = new Insert();
                                    insertFields.insert(newRecords, destination, source);
                                    AddNewRecordsForPublicList(ref pubDatatable, newRecords, source);
                                }
                                if (updatedRecords.Rows.Count == 0)
                                {
                                }
                                else
                                {
                                    Update updateFields = new Update();
                                    updateFields.update(updatedRecords, destination, source);

                                }
                            }
                            setUpdatedVersion(resultVersion.version, source);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logException exception = new logException();
                exception.storeExcepion(ex);
            }
        }

        private void AddNewRecordsForPublicList(ref DataTable publicList, DataTable newRecords, string listName)
        {

            if (listName == "Clinicians")
            {
                for (int i = 0; i < newRecords.Rows.Count; i++)
                {
                    DataRow dr = publicList.NewRow();
                    dr["ClinicianLicense"] = newRecords.Rows[i]["license"];

                    if (newRecords.Columns.Contains("facilityName"))
                    {
                        dr["FacilityName"] = newRecords.Rows[i]["facilityName"];
                    }
                    if (newRecords.Columns.Contains("location"))
                    {
                        dr["Location"] = newRecords.Rows[i]["location"];
                    }

                    if (newRecords.Columns.Contains("source"))
                    {
                        dr["Source"] = newRecords.Rows[i]["source"];
                    }

                    if (newRecords.Columns.Contains("username"))
                    {
                        dr["UserName"] = newRecords.Rows[i]["username"];
                    }

                    if (newRecords.Columns.Contains("password"))
                    {
                        dr["Password"] = newRecords.Rows[i]["password"];
                    }

                    dr["SpecialtyId"] = newRecords.Rows[i]["specialtyId"];
                    dr["IsActive"] = newRecords.Rows[i]["isActive"];

                    publicList.Rows.Add(dr);


                }



            }
            else if (listName == "DDC")
            {
                for (int i = 0; i < newRecords.Rows.Count; i++)
                {
                    DataRow dr = publicList.NewRow();
                    dr["cCode"] = newRecords.Rows[i]["code"];

                    if (newRecords.Columns.Contains("tradeName"))
                    {
                        dr["cDesc"] = newRecords.Rows[i]["tradeName"];
                    }

                    if (newRecords.Columns.Contains("cType"))
                    {
                        dr["cType"] = newRecords.Rows[i]["cType"];
                    }

                    if (newRecords.Columns.Contains("Status"))
                    {
                        dr["Status"] = newRecords.Rows[i]["Status"];
                    }

                    if (newRecords.Columns.Contains("discontinueDate"))
                    {
                        dr["ValidTo"] = newRecords.Rows[i]["discontinueDate"];
                    }
                    publicList.Rows.Add(dr);
                }
            }

            else if (listName == "DubaiSpecialServices")
            {
                for (int i = 0; i < newRecords.Rows.Count; i++)
                {
                    DataRow dr = publicList.NewRow();
                    dr["cCode"] = newRecords.Rows[i]["code"];

                    if (newRecords.Columns.Contains("description"))
                    {
                        dr["cDesc"] = newRecords.Rows[i]["description"];
                    }

                    if (newRecords.Columns.Contains("cType"))
                    {
                        dr["cType"] = newRecords.Rows[i]["cType"];
                    }

                    if (newRecords.Columns.Contains("Status"))
                    {
                        dr["Status"] = newRecords.Rows[i]["Status"];
                    }

                    if (newRecords.Columns.Contains("detailedDescription"))
                    {
                        dr["LongDesc"] = newRecords.Rows[i]["detailedDescription"];
                    }
                    publicList.Rows.Add(dr);
                }
            }
            else if (listName == "DhaCPT")
            {
                for (int i = 0; i < newRecords.Rows.Count; i++)
                {
                    DataRow dr = publicList.NewRow();
                    dr["cCode"] = newRecords.Rows[i]["code"];

                    if (newRecords.Columns.Contains("description"))
                    {
                        dr["cDesc"] = newRecords.Rows[i]["description"];
                    }

                    if (newRecords.Columns.Contains("cType"))
                    {
                        dr["cType"] = newRecords.Rows[i]["cType"];
                    }

                    if (newRecords.Columns.Contains("Status"))
                    {
                        dr["Status"] = newRecords.Rows[i]["Status"];
                    }

                    if (newRecords.Columns.Contains("fullDescription"))
                    {
                        dr["LongDesc"] = newRecords.Rows[i]["fullDescription"];
                    }
                    publicList.Rows.Add(dr);
                }
            }

            else if (listName == "DhaHCPCS")
            {
                for (int i = 0; i < newRecords.Rows.Count; i++)
                {
                    DataRow dr = publicList.NewRow();
                    dr["cCode"] = newRecords.Rows[i]["code"];

                    if (newRecords.Columns.Contains("description"))
                    {
                        dr["cDesc"] = newRecords.Rows[i]["description"];
                    }

                    if (newRecords.Columns.Contains("cType"))
                    {
                        dr["cType"] = newRecords.Rows[i]["cType"];
                    }

                    if (newRecords.Columns.Contains("Status"))
                    {
                        dr["Status"] = newRecords.Rows[i]["Status"];
                    }

                    if (newRecords.Columns.Contains("longDescription"))
                    {
                        dr["LongDesc"] = newRecords.Rows[i]["longDescription"];
                    }
                    publicList.Rows.Add(dr);
                }

            }

            else if (listName == "Payers")
            {
                for (int i = 0; i < newRecords.Rows.Count; i++)
                {
                    DataRow dr = publicList.NewRow();
                    dr["PayerCode"] = newRecords.Rows[i]["dhaCode"];

                    if (newRecords.Columns.Contains("name"))
                    {
                        dr["PayerName"] = newRecords.Rows[i]["name"];
                    }

                    if (newRecords.Columns.Contains("PayerTypeID"))
                    {
                        dr["PayerTypeID"] = newRecords.Rows[i]["PayerTypeID"];
                    }

                    if (newRecords.Columns.Contains("CreationDate"))
                    {
                        dr["CreationDate"] = newRecords.Rows[i]["CreationDate"];
                    }

                    if (newRecords.Columns.Contains("IsActive"))
                    {
                        dr["IsActive"] = newRecords.Rows[i]["IsActive"];
                    }
                    publicList.Rows.Add(dr);
                }

            }
            else if (listName == "DhaSelfPaid")
            {
                for (int i = 0; i < newRecords.Rows.Count; i++)
                {
                    DataRow dr = publicList.NewRow();
                    dr["PayerCode"] = newRecords.Rows[i]["eClaimLinkId"];

                    if (newRecords.Columns.Contains("name"))
                    {
                        dr["PayerName"] = newRecords.Rows[i]["name"];
                    }

                    if (newRecords.Columns.Contains("PayerTypeID"))
                    {
                        dr["PayerTypeID"] = newRecords.Rows[i]["PayerTypeID"];
                    }

                    if (newRecords.Columns.Contains("CreationDate"))
                    {
                        dr["CreationDate"] = newRecords.Rows[i]["CreationDate"];
                    }

                    if (newRecords.Columns.Contains("IsActive"))
                    {
                        dr["IsActive"] = newRecords.Rows[i]["IsActive"];
                    }
                    publicList.Rows.Add(dr);
                }

            }
            else if (listName == "Providers")
            {
                for (int i = 0; i < newRecords.Rows.Count; i++)
                {
                    DataRow dr = publicList.NewRow();
                    dr["LicenseID"] = newRecords.Rows[i]["license"];

                    dr["ProviderType"] = newRecords.Rows[i]["typeId"];

                    dr["Name"] = newRecords.Rows[i]["name"];

                    //  dr["IsActive"] = newRecords.Rows[i]["isActive"];

                    dr["ProviderSource"] = newRecords.Rows[i]["source"];
                    dr["Region"] = newRecords.Rows[i]["emirate"];

                    if (newRecords.Columns.Contains("email"))
                    {
                        dr["Email"] = newRecords.Rows[i]["email"];
                    }

                    dr["CreationDate"] = DateTime.Now.ToShortDateString();
                    dr["CreatedBy"] = 1;
                    publicList.Rows.Add(dr);
                }



            }
            else if (listName == "Specialties")
            {
                for (int i = 0; i < newRecords.Rows.Count; i++)
                {
                    DataRow dr = publicList.NewRow();
                    dr["SpecialtyID"] = newRecords.Rows[i]["specialtyId"];

                    if (newRecords.Columns.Contains("specialty"))
                    {
                        dr["SpecialtyDesc"] = newRecords.Rows[i]["specialty"];
                    }

                    if (newRecords.Columns.Contains("specialtyGroup"))
                    {
                        dr["SpecialtyGroup"] = newRecords.Rows[i]["specialtyGroup"];
                    }


                    publicList.Rows.Add(dr);
                }

            }

            else if (listName == "DdcScientific")
            {
                for (int i = 0; i < newRecords.Rows.Count; i++)
                {
                    DataRow dr = publicList.NewRow();
                    dr["cCode"] = newRecords.Rows[i]["scientificCode"];

                    if (newRecords.Columns.Contains("genericStrengthDosageForm"))
                    {
                        dr["cDesc"] = newRecords.Rows[i]["genericStrengthDosageForm"];
                    }

                    if (newRecords.Columns.Contains("cType"))
                    {
                        dr["cType"] = newRecords.Rows[i]["cType"];
                    }

                    if (newRecords.Columns.Contains("Status"))
                    {
                        dr["Status"] = newRecords.Rows[i]["Status"];
                    }
                    publicList.Rows.Add(dr);
                }

            }

            else if (listName == "ICDs")
            {
                for (int i = 0; i < newRecords.Rows.Count; i++)
                {
                    DataRow dr = publicList.NewRow();
                    dr["Code"] = newRecords.Rows[i]["code"];

                    if (newRecords.Columns.Contains("description"))
                    {
                        dr["Description"] = newRecords.Rows[i]["description"];
                    }

                    if (newRecords.Columns.Contains("shortDescription"))
                    {
                        dr["[Short Description]"] = newRecords.Rows[i]["shortDescription"];
                    }

                    if (newRecords.Columns.Contains("vidalID"))
                    {
                        dr["VidalId"] = newRecords.Rows[i]["vidalID"];
                    }
                    publicList.Rows.Add(dr);
                }

            }

            if (listName == "DhaDenialCodes")
            {
                for (int i = 0; i < newRecords.Rows.Count; i++)
                {
                    DataRow dr = publicList.NewRow();
                    dr["Code"] = newRecords.Rows[i]["Code"];



                    if (newRecords.Columns.Contains("Description"))
                    {
                        dr["Description"] = newRecords.Rows[i]["Description"];
                    }
                    publicList.Rows.Add(dr);
                }

            }

            if (listName == "DhaRouteOfAdministration")
            {
                for (int i = 0; i < newRecords.Rows.Count; i++)
                {
                    DataRow dr = publicList.NewRow();
                    dr["Code"] = newRecords.Rows[i]["code"];



                    if (newRecords.Columns.Contains("Name"))
                    {
                        dr["description"] = newRecords.Rows[i]["Name"];
                    }
                    publicList.Rows.Add(dr);
                }

            }
        }

        private DataTable getListRecords(string ListName)
        {

            DataTable dtAll = new DataTable();
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            SqlConnection con = new SqlConnection(strConnString);

            try
            {
                if (ListName == "DDC")
                {
                    cmd.CommandText = "Select * from tblCodes where cType = 'DRUGS' ";
                    cmd.Connection = con;
                    con.Open();
                    da.Fill(dtAll);
                }
                else if (ListName == "DubaiSpecialServices")
                {
                    cmd.CommandText = "Select * from tblCodes where cType = 'SERVICES' ";
                    cmd.Connection = con;
                    con.Open();
                    da.Fill(dtAll);
                }
                else if (ListName == "DhaCPT")
                {
                    cmd.CommandText = "Select * from tblCodes where cType = 'CPT' ";
                    cmd.Connection = con;
                    con.Open();
                    da.Fill(dtAll);
                }
                else if (ListName == "DhaHCPCS")
                {
                    cmd.CommandText = "Select * from tblCodes where cType = 'HCPCS' ";
                    cmd.Connection = con;
                    con.Open();
                    da.Fill(dtAll);
                }
                else if (ListName == "DdcScientific")
                {
                    cmd.CommandText = "Select * from tblCodes where cType = 'GENERICS' ";
                    cmd.Connection = con;
                    con.Open();
                    da.Fill(dtAll);
                }
                else if (ListName == "Payers")
                {
                    cmd.CommandText = "Select * from Payers ";
                    cmd.Connection = con;
                    con.Open();
                    da.Fill(dtAll);
                }
                else if (ListName == "Providers")
                {
                    cmd.CommandText = "Select * from provider ";
                    cmd.Connection = con;
                    con.Open();
                    da.Fill(dtAll);
                }
                else if (ListName == "DhaSelfPaid")
                {
                    cmd.CommandText = "Select * from Payers ";
                    cmd.Connection = con;
                    con.Open();
                    da.Fill(dtAll);
                }
                else if (ListName == "Clinicians")
                {
                    cmd.CommandText = "Select * from Clinicians ";
                    cmd.Connection = con;
                    con.Open();
                    da.Fill(dtAll);
                }
                else if (ListName == "ICDs")
                {
                    cmd.CommandText = "Select * from Diagnosis ";
                    cmd.Connection = con;
                    con.Open();
                    da.Fill(dtAll);
                }

                else if (ListName == "DhaDenialCodes")
                {
                    cmd.CommandText = "Select * from Denials ";
                    cmd.Connection = con;
                    con.Open();
                    da.Fill(dtAll);
                }



                else if (ListName == "Specialties")
                {
                    cmd.CommandText = "Select * from ClinicianSpecialty ";
                    cmd.Connection = con;
                    con.Open();
                    da.Fill(dtAll);
                }
                else if (ListName == "DhaRouteOfAdministration")
                {
                    cmd.CommandText = "Select * from RoutOfAdmin ";
                    cmd.Connection = con;
                    con.Open();
                    da.Fill(dtAll);
                }
                else
                {
                    dtAll = new DataTable();
                    return dtAll;
                }

                return dtAll;

            }
            catch (Exception e)
            {

                logException ex = new logException();
                ex.storeExcepion(e);

                return dtAll;

            }
            finally
            {
                con.Close();
                da.Dispose();
            }



        }

        private void setUpdatedVersion(int newVersion, string source)
        {
            using (SqlConnection conn = new SqlConnection(strConnString))
            {
                SqlCommand cmd =
                    new SqlCommand(
                        "Update tblLookUp Set LastUpdatedVersion = " + newVersion + " , LastUpdatedDate='" +
                        DateTime.Now + "' WHERE SourceTable = '" + source + "'", conn);
                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    logException exception = new logException();
                    exception.storeExcepion(ex);
                }
            }
        }

        private string checkLookup(string source)
        {
            string destination = "";
            try
            {
                SqlConnection con = new SqlConnection(strConnString);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Select Destination From tblLookUp where SourceTable = '" + source + "'";
                cmd.Connection = con;
                con.Open();
                destination = Convert.ToString(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                logException exception = new logException();
                exception.storeExcepion(ex);
            }

            return (destination);
        }

        public List<List<int>> splitData(int width, List<int> lst)
        {

            List<List<int>> dds = new List<List<int>>();

            // Determine how many lists are required 
            int numberOfLists = (lst.Count / width);

            for (int i = 0; i <= numberOfLists; i++)
            {
                List<int> newList = new List<int>();
                newList = lst.Skip(i * width).Take(width).ToList();
                dds.Add(newList);
            }

            return dds;
        }

        private int getCurrentVersion(string source)
        {
            int currentVersion = 0;
            using (SqlConnection conn = new SqlConnection(strConnString))
            {
                SqlCommand cmd =
                    new SqlCommand(
                        "SELECT LastUpdatedVersion FROM tblLookUp WHERE SourceTable = '" + source + "'", conn);
                try
                {
                    conn.Open();
                    currentVersion = (int)cmd.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    logException exception = new logException();
                    exception.storeExcepion(ex);
                }
                return (currentVersion);
            }
        }

    }
}