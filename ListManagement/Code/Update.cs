using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Data;
using System.Xml;
using System.Data.SqlClient;
using System.Configuration;

namespace ListManagement.Code
{
    public class Update
    {
        String strConnString = ConfigurationManager.ConnectionStrings["LMUConnectionString"].ConnectionString;
        public void update(DataTable DrugsUpdate, string destination, string source)
        {
            SqlConnection conn = new SqlConnection(strConnString);

            if (source == "DDC")
            {
                string upStmt = "Update tblCodes Set cDesc = @cDesc,ValidTo = @ValidTo,Status =@Status  Where cCode= @cCode and cTYPE='DRUGS'";
                SqlCommand cmd = new SqlCommand(upStmt, conn);
                try
                {


                    conn.Open();
                    cmd.Parameters.AddWithValue("@cCode", "");

                    cmd.Parameters.AddWithValue("@cDesc", "");
                    cmd.Parameters.Add("@ValidTo", SqlDbType.DateTime).Value = "";
                    cmd.Parameters.AddWithValue("@Status", "");
                    foreach (DataRow dr in DrugsUpdate.Rows)
                    {
                        cmd.Parameters["@cCode"].Value = dr["code"];
                        cmd.Parameters["@cDesc"].Value = dr["tradeName"];
                        cmd.Parameters["@ValidTo"].Value = (dr["discontinueDate"] != null) ? dr["discontinueDate"] : null;

                        if (dr["Status"].ToString().ToUpper() == "ACTIVE")
                        {
                            cmd.Parameters["@Status"].Value = 1;
                        }
                        else if (dr["Status"].ToString().ToUpper() == "Discontinued".ToUpper())
                        {
                            cmd.Parameters["@Status"].Value = 0;
                        }
                        else
                        {
                            cmd.Parameters["@Status"].Value = 1;


                        }
                        cmd.Parameters["@Status"].Value = dr["Status"];
                        cmd.ExecuteNonQuery();
                    }


                }
                catch (Exception e)
                {
                    logException ex = new logException();
                    ex.storeExcepion(e);
                }
                finally
                {
                    conn.Close();

                }

            }
            else if (source == "DubaiSpecialServices")
            {
                string upStmt = "Update tblCodes Set cDesc = @cDesc,LongDesc = @LongDesc,Status = @Status  Where cCode= @cCode and cTYPE='SERVICES'";
                SqlCommand cmd = new SqlCommand(upStmt, conn);
                try
                {



                    conn.Open();
                    cmd.Parameters.AddWithValue("@cCode", "");
                    cmd.Parameters.AddWithValue("@cDesc", "");
                    cmd.Parameters.AddWithValue("@LongDesc", "");
                    cmd.Parameters.AddWithValue("@Status", "");

                    foreach (DataRow dr in DrugsUpdate.Rows)
                    {
                        cmd.Parameters["@cCode"].Value = dr["code"];
                        cmd.Parameters["@cDesc"].Value = dr["description"];
                        cmd.Parameters["@LongDesc"].Value = dr["detailedDescription"];
                        cmd.Parameters["@Status"].Value = dr["Status"];
                        cmd.ExecuteNonQuery();
                    }


                }
                catch (Exception e)
                {
                    logException ex = new logException();
                    ex.storeExcepion(e);
                }

            }
            else if (source == "DhaCPT")
            {
                string upStmt = "Update tblCodes Set cDesc = @cDesc,LongDesc = @LongDesc,Status = @Status  Where cCode= @cCode and cTYPE='CPT'";
                SqlCommand cmd = new SqlCommand(upStmt, conn);
                try
                {


                    conn.Open();
                    cmd.Parameters.AddWithValue("@cCode", "");
                    cmd.Parameters.AddWithValue("@cDesc", "");
                    cmd.Parameters.AddWithValue("@LongDesc", "");
                    cmd.Parameters.AddWithValue("@Status", "");

                    foreach (DataRow dr in DrugsUpdate.Rows)
                    {
                        cmd.Parameters["@cCode"].Value = dr["code"];
                        cmd.Parameters["@cDesc"].Value = dr["shortdescription"];
                        cmd.Parameters["@LongDesc"].Value = dr["fullDescription"];
                        cmd.Parameters["@Status"].Value = dr["Status"];

                        cmd.ExecuteNonQuery();
                    }


                }
                catch (Exception e)
                {
                    logException ex = new logException();
                    ex.storeExcepion(e);
                }
                finally
                {
                    conn.Close();

                }
            }
            else if (source == "DhaHCPCS")
            {
                string upStmt = "Update tblCodes Set cDesc = @cDesc,LongDesc = @LongDesc,Status = @Status  Where cCode= @cCode  and cTYPE='HCPCS' ";
                SqlCommand cmd = new SqlCommand(upStmt, conn);
                try
                {


                    conn.Open();
                    cmd.Parameters.AddWithValue("@cCode", "");
                    cmd.Parameters.AddWithValue("@cDesc", "");
                    cmd.Parameters.AddWithValue("@LongDesc", "");
                    cmd.Parameters.AddWithValue("@Status", "");
                    foreach (DataRow dr in DrugsUpdate.Rows)
                    {
                        cmd.Parameters["@cCode"].Value = dr["code"];
                        cmd.Parameters["@cDesc"].Value = dr["shortDescription"];
                        cmd.Parameters["@LongDesc"].Value = dr["longDescription"];
                        cmd.Parameters["@Status"].Value = dr["Status"];
                        cmd.ExecuteNonQuery();
                    }


                }
                catch (Exception e)
                {
                    logException ex = new logException();
                    ex.storeExcepion(e);
                }
                finally
                {
                    conn.Close();

                }
            }
            else if (source == "DdcScientific")
            {
                string upStmt = "Update tblCodes Set cDesc = @cDesc,Status = @Status  Where cCode= @cCode and cTYPE='GENERICS'";
                SqlCommand cmd = new SqlCommand(upStmt, conn);
                try
                {

                    conn.Open();
                    cmd.Parameters.AddWithValue("@cCode", "");
                    cmd.Parameters.AddWithValue("@cDesc", "");
                    cmd.Parameters.AddWithValue("@Status", "");

                    foreach (DataRow dr in DrugsUpdate.Rows)
                    {
                        cmd.Parameters["@cCode"].Value = dr["scientificCode"];
                        cmd.Parameters["@cDesc"].Value = dr["genericStrengthDosageForm"];
                        cmd.Parameters["@Status"].Value = dr["Status"];

                        cmd.ExecuteNonQuery();
                    }


                }
                catch (Exception e)
                {
                    logException ex = new logException();
                    ex.storeExcepion(e);
                }
                finally
                {
                    conn.Close();

                }
            }
            else if (source == "Payers")
            {
                string upStmt = "Update Payers Set PayerName = @PayerName,PayerTypeID = @PayerTypeID,IsActive =@IsActive  Where PayerCode= @PayerCode";
                SqlCommand cmd = new SqlCommand(upStmt, conn);
                try
                {


                    conn.Open();
                    cmd.Parameters.AddWithValue("@PayerCode", "");
                    cmd.Parameters.AddWithValue("@PayerName", "");
                    cmd.Parameters.AddWithValue("@PayerTypeID", "");
                    cmd.Parameters.AddWithValue("@IsActive", "");

                    foreach (DataRow dr in DrugsUpdate.Rows)
                    {
                        cmd.Parameters["@PayerCode"].Value = dr["dhacode"];
                        cmd.Parameters["@PayerName"].Value = dr["name"];
                        cmd.Parameters["@PayerTypeID"].Value = dr["PayerTypeID"];
                        cmd.Parameters["@IsActive"].Value = dr["IsActive"];

                        cmd.ExecuteNonQuery();
                    }


                }
                catch (Exception e)
                {
                    logException ex = new logException();
                    ex.storeExcepion(e);
                }
                finally
                {
                    conn.Close();

                }
            }
            else if (source == "Clinicians")
            {
                for (int i = 0; i < DrugsUpdate.Rows.Count; i++)
                {
                    DataRow myRow = DrugsUpdate.Rows[i];
                    //myRow.Table

                    if (!string.IsNullOrEmpty(DrugsUpdate.Rows[i]["password"].ToString()) && (DrugsUpdate.Rows[i]["type"].ToString().ToUpper() == "DHCC"))
                    {
                        DrugsUpdate.Rows[i]["password"] = Encrypt_Decrypt.Decrypt(DrugsUpdate.Rows[i]["password"].ToString());
                    }

                }

                SqlConnection destinationConnection = new SqlConnection(strConnString);
                destinationConnection.Open();


                SqlTransaction transaction = destinationConnection.BeginTransaction(IsolationLevel.ReadUncommitted, "aa");
                try
                {

                    //  destinationConnection.Open();
                    SqlBulkCopy bulkcopy = new SqlBulkCopy(destinationConnection, SqlBulkCopyOptions.FireTriggers, transaction);
                    using (SqlCommand command = new SqlCommand("", destinationConnection, transaction))
                    {

                        // conn.Open();

                        //Creating temp table on database
                        command.CommandText = "CREATE TABLE #TmpClinicianTable([ClinicianLicense] [nvarchar](20) NOT NULL,[ClinicianName] [nvarchar](100) NOT NULL,[FacilityName] [nvarchar](150) NULL,"
                        + "[Location] [nvarchar](150) NULL,[Source] [nvarchar](20) NULL,[IsActive] [int] NULL,[UserName] [nvarchar](50) NULL,[Password] [nvarchar](50) NULL,[SpecialtyId] [varchar](10) NULL)";
                        command.ExecuteNonQuery();


                        // bulkcopy.BulkCopyTimeout = 660;
                        bulkcopy.ColumnMappings.Add("license", "ClinicianLicense");
                        bulkcopy.ColumnMappings.Add("name", "ClinicianName");

                        if (DrugsUpdate.Columns.Contains("facilityName"))
                        {
                            bulkcopy.ColumnMappings.Add("facilityName", "FacilityName");
                        } if (DrugsUpdate.Columns.Contains("location"))
                        {
                            bulkcopy.ColumnMappings.Add("location", "Location");
                        }

                        if (DrugsUpdate.Columns.Contains("source"))
                        {
                            bulkcopy.ColumnMappings.Add("source", "Source");
                        }

                        if (DrugsUpdate.Columns.Contains("username"))
                        {
                            bulkcopy.ColumnMappings.Add("username", "UserName");
                        }

                        if (DrugsUpdate.Columns.Contains("password"))
                        {
                            bulkcopy.ColumnMappings.Add("password", "Password");
                        }
                        bulkcopy.ColumnMappings.Add("specialtyId", "SpecialtyId");
                        bulkcopy.ColumnMappings.Add("isActive", "IsActive");
                        // bulkcopy.ColumnMappings.Add("record_status", "record_status");
                        bulkcopy.DestinationTableName = "#TmpClinicianTable";
                        bulkcopy.WriteToServer(DrugsUpdate);
                        bulkcopy.Close();


                        // Updating destination table, and dropping temp table
                        // command.CommandTimeout = 300;

                        //command.CommandText ="insert into Clinicians (ClinicianLicense,ClinicianName,FacilityName,Location,source,SpecialtyId,username,password,IsActive) (select ClinicianLicense,ClinicianName,FacilityName,Location,source,SpecialtyId,username,password,IsActive from #TmpClinicianTable  where record_status='NEW' )";
                        //command.ExecuteNonQuery();
                        command.CommandText = "Update Clinicians  Set ClinicianName =b.ClinicianName,FacilityName=b.FacilityName,Location = b.Location,Source =b.Source,SpecialtyId=b.SpecialtyId,IsActive=b.IsActive from #TmpClinicianTable b  where Clinicians.ClinicianLicense=b.ClinicianLicense  ";
                        command.ExecuteNonQuery();
                        command.CommandText = "Update Clinicians  Set ClinicianName =b.ClinicianName,FacilityName=b.FacilityName,Location = b.Location,Source =b.Source,SpecialtyId=b.SpecialtyId,IsActive=b.IsActive,Username=b.username,password=b.password from #TmpClinicianTable b  where Clinicians.ClinicianLicense=b.ClinicianLicense and b.UserName is not null and b.UserName <>''  ; DROP TABLE #TmpClinicianTable;";
                        command.ExecuteNonQuery();
                        transaction.Commit();

                    }

                }
                catch (Exception e)
                {
                    if (transaction != null)
                    {
                        transaction.Rollback();
                    }
                    logException ex = new logException();
                    ex.storeExcepion(e);
                }



                finally
                {
                    if (destinationConnection != null)
                    {
                        destinationConnection.Close();
                    }
                }


            }







                //string upStmt = "Update Clinicians Set ClinicianName = @ClinicianName,FacilityName=@FacilityName,Location = @Location,Source =@Source,SpecialtyId=@SpecialtyId,Username=@UserName,Password=@Password,IsActive=@IsActive  Where ClinicianLicense= @ClinicianLicense";
            //SqlCommand cmd = new SqlCommand(upStmt, conn);
            //try
            //{

                //    conn.Open();
            //    cmd.Parameters.AddWithValue("@ClinicianLicense", "");
            //    cmd.Parameters.AddWithValue("@ClinicianName", "");
            //   // cmd.Parameters.AddWithValue("@Profession", "");
            //    cmd.Parameters.AddWithValue("@FacilityName", "");
            //    cmd.Parameters.AddWithValue("@Location", "");
            //    cmd.Parameters.AddWithValue("@Source", "");
            //    cmd.Parameters.AddWithValue("@SpecialtyId", "");
            //    cmd.Parameters.AddWithValue("@UserName", "");
            //    cmd.Parameters.AddWithValue("@Password", "");
            //    cmd.Parameters.AddWithValue("@IsActive", "");
            //    //cmd.Parameters.AddWithValue("@specialtyGroup", "");
            //    //cmd.Parameters.AddWithValue("@activeFrom", DBNull.Value);
            //    //cmd.Parameters.AddWithValue("@activeTo", DBNull.Value);

                //    foreach (DataRow dr in DrugsUpdate.Rows)
            //    {
            //        cmd.Parameters["@ClinicianLicense"].Value = dr["license"];
            //        cmd.Parameters["@ClinicianName"].Value = dr["name"];
            //       // cmd.Parameters["@Profession"].Value = dr["profession"];
            //        if (dr.Table.Columns.Contains("FacilityName"))
            //        {
            //            cmd.Parameters["@FacilityName"].Value = dr["FacilityName"];

                //        }
            //        else
            //        {

                //            cmd.Parameters["@FacilityName"].Value = "";
            //        }

                //        if (dr.Table.Columns.Contains("username"))
            //        {
            //            cmd.Parameters["@UserName"].Value = dr["username"];

                //        }
            //        else
            //        {

                //            cmd.Parameters["@UserName"].Value = "";
            //        }
            //        if (dr.Table.Columns.Contains("password"))
            //        {
            //            cmd.Parameters["@Password"].Value = dr["password"];

                //        }
            //        else
            //        {

                //            cmd.Parameters["@Password"].Value = "";
            //        }
            //        cmd.Parameters["@FacilityName"].Value = dr["FacilityName"];
            //        if (dr.Table.Columns.Contains("Location"))
            //        {
            //            cmd.Parameters["@Location"].Value = dr["Location"];
            //        }
            //        else
            //        {

                //            cmd.Parameters["@Location"].Value = DBNull.Value;
            //        }
            //        cmd.Parameters["@Source"].Value = dr["Source"];
            //        cmd.Parameters["@SpecialtyId"].Value = dr["specialtyId"];
            //        if (dr.Table.Columns.Contains("isActive"))
            //        {
            //            if (dr["isActive"].ToString().ToUpper() == "TRUE")
            //            {

                //                cmd.Parameters["@isActive"].Value = 1;

                //            }
            //            else
            //            {
            //                cmd.Parameters["@isActive"].Value = 0;

                //            }
            //        }

                //        //  cmd.Parameters["@specialtyGroup"].Value = dr["specialtyGroup"];
            //        //if (dr.Table.Columns.Contains("activeFrom"))
            //        //{
            //        //    cmd.Parameters["@activeFrom"].Value = (dr["activeFrom"] != null && !string.IsNullOrEmpty(dr["activeFrom"].ToString())) ? dr["activeFrom"] : DBNull.Value;
            //        //}
            //        //else
            //        //{
            //        //    cmd.Parameters["@activeFrom"].Value = DBNull.Value;
            //        //}
            //        //if (dr.Table.Columns.Contains("activeTo"))
            //        //{
            //        //    cmd.Parameters["@activeTo"].Value = (dr["activeTo"] != null && !string.IsNullOrEmpty(dr["activeTo"].ToString())) ? dr["activeTo"] : DBNull.Value;
            //        //}
            //        //else
            //        //{
            //        //    cmd.Parameters["@activeTo"].Value = DBNull.Value;
            //        //}

                //        cmd.ExecuteNonQuery();
            else if (source == "ICDs")
            {
                string upStmt = "Update Diagnosis Set Description = @Description,[Short Description] =@SD,VidalId =@VidalId  Where Code= @Code";
                SqlCommand cmd = new SqlCommand(upStmt, conn);
                try
                {

                    conn.Open();
                    cmd.Parameters.AddWithValue("@Code", "");
                    cmd.Parameters.AddWithValue("@Description", "");
                    cmd.Parameters.AddWithValue("@SD", "");
                    cmd.Parameters.AddWithValue("@VidalId", "");

                    foreach (DataRow dr in DrugsUpdate.Rows)
                    {
                        cmd.Parameters["@Code"].Value = dr["code"];
                        cmd.Parameters["@Description"].Value = dr["description"];
                        cmd.Parameters["@SD"].Value = dr["shortDescription"];
                        cmd.Parameters["@VidalId"].Value = dr["vidalID"];

                        cmd.ExecuteNonQuery();
                    }


                }
                catch (Exception e)
                {
                    logException ex = new logException();
                    ex.storeExcepion(e);
                }
                finally
                {
                    conn.Close();

                }
            }
            else if (source == "DhaDenialCodes")
            {
                string upStmt = "Update Denials Set Description = @Description  Where Code= @Code";
                SqlCommand cmd = new SqlCommand(upStmt, conn);
                try
                {

                    conn.Open();
                    cmd.Parameters.AddWithValue("@Code", "");
                    cmd.Parameters.AddWithValue("@Description", "");

                    foreach (DataRow dr in DrugsUpdate.Rows)
                    {
                        cmd.Parameters["@Code"].Value = dr["Code"];
                        cmd.Parameters["@Description"].Value = dr["Description"];

                        cmd.ExecuteNonQuery();
                    }


                }
                catch (Exception e)
                {
                    logException ex = new logException();
                    ex.storeExcepion(e);
                }
                finally
                {
                    conn.Close();

                }
            }
            else if (source == "DhaRouteOfAdministration")
            {
                string upStmt = "Update RoutOfAdmin Set Name = @Description  Where Code= @Code";
                SqlCommand cmd = new SqlCommand(upStmt, conn);
                try
                {

                    conn.Open();
                    cmd.Parameters.AddWithValue("@Code", "");
                    cmd.Parameters.AddWithValue("@Description", "");

                    foreach (DataRow dr in DrugsUpdate.Rows)
                    {
                        cmd.Parameters["@Code"].Value = dr["code"];
                        cmd.Parameters["@Description"].Value = dr["description"];

                        cmd.ExecuteNonQuery();
                    }


                }
                catch (Exception e)
                {
                    logException ex = new logException();
                    ex.storeExcepion(e);
                }
                finally
                {
                    conn.Close();

                }
            }
        }
    }
}