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
    public class Insert
    {
        String strConnString = ConfigurationManager.ConnectionStrings["LMUConnectionString"].ConnectionString;
        public void insert(DataTable newRecords, string destination, string source)
        {

            using (SqlConnection destinationConnection = new SqlConnection(strConnString))
            {
                destinationConnection.Open();
                using (SqlBulkCopy sqlBulk = new SqlBulkCopy(destinationConnection, SqlBulkCopyOptions.FireTriggers,null))
                {
                    try
                    {
                        if (source == "DDC")
                        {

                            sqlBulk.DestinationTableName = destination;
                            sqlBulk.ColumnMappings.Add("code", "cCode");
                            sqlBulk.ColumnMappings.Add("tradeName", "cDesc");
                            sqlBulk.ColumnMappings.Add("cType", "cType");
                            sqlBulk.ColumnMappings.Add("Status", "Status");
                            sqlBulk.ColumnMappings.Add("discontinueDate", "ValidTo");
                            sqlBulk.WriteToServer(newRecords);
                        }
                        else if (source == "DubaiSpecialServices")
                        {

                            sqlBulk.DestinationTableName = destination;
                            sqlBulk.ColumnMappings.Add("code", "cCode");
                            sqlBulk.ColumnMappings.Add("description", "cDesc");
                            sqlBulk.ColumnMappings.Add("detailedDescription", "LongDesc");
                            sqlBulk.ColumnMappings.Add("cType", "cType");
                            sqlBulk.ColumnMappings.Add("Status", "Status");
                            sqlBulk.WriteToServer(newRecords);

                        }
                        else if (source == "DhaCPT")
                        {

                            sqlBulk.DestinationTableName = destination;
                            sqlBulk.ColumnMappings.Add("code", "cCode");
                            sqlBulk.ColumnMappings.Add("shortdescription", "cDesc");
                            sqlBulk.ColumnMappings.Add("fullDescription", "LongDesc");
                            sqlBulk.ColumnMappings.Add("cType", "cType");
                            sqlBulk.ColumnMappings.Add("Status", "Status");
                            sqlBulk.WriteToServer(newRecords);

                        }
                        else if (source == "DhaHCPCS")
                        {

                            sqlBulk.DestinationTableName = destination;
                            sqlBulk.ColumnMappings.Add("code", "cCode");
                            sqlBulk.ColumnMappings.Add("shortDescription", "cDesc");
                            sqlBulk.ColumnMappings.Add("longDescription", "LongDesc");
                            sqlBulk.ColumnMappings.Add("cType", "cType");
                            sqlBulk.ColumnMappings.Add("Status", "Status");
                            sqlBulk.WriteToServer(newRecords);

                        }
                        else if (source == "Specialties")
                        {

                            sqlBulk.DestinationTableName = destination;
                            sqlBulk.ColumnMappings.Add("specialtyId", "SpecialtyID");
                            sqlBulk.ColumnMappings.Add("specialty", "SpecialtyDesc");
                            sqlBulk.ColumnMappings.Add("specialtyGroup", "SpecialtyGroup");
                           
                            sqlBulk.WriteToServer(newRecords);

                        }
                        else if (source == "DdcScientific")
                        {

                            sqlBulk.DestinationTableName = destination;
                            sqlBulk.ColumnMappings.Add("scientificCode", "cCode");
                            sqlBulk.ColumnMappings.Add("genericStrengthDosageForm", "cDesc");
                            sqlBulk.ColumnMappings.Add("cType", "cType");
                            sqlBulk.ColumnMappings.Add("Status", "Status");
                            sqlBulk.WriteToServer(newRecords);

                        }
                        else if (source == "Payers")
                        {

                            sqlBulk.DestinationTableName = destination;
                            sqlBulk.ColumnMappings.Add("dhaCode", "PayerCode");
                            sqlBulk.ColumnMappings.Add("name", "PayerName");
                            sqlBulk.ColumnMappings.Add("PayerTypeID", "PayerTypeID");
                            sqlBulk.ColumnMappings.Add("CreatedBy", "CreatedBy");
                            sqlBulk.ColumnMappings.Add("CreationDate", "CreationDate");
                            sqlBulk.ColumnMappings.Add("IsActive", "IsActive");
                            sqlBulk.WriteToServer(newRecords);

                        }
                        else if (source == "DhaSelfPaid")
                        {

                            sqlBulk.DestinationTableName = destination;
                            sqlBulk.ColumnMappings.Add("eClaimLinkId", "PayerCode");
                            sqlBulk.ColumnMappings.Add("name", "PayerName");
                            sqlBulk.ColumnMappings.Add("PayerTypeID", "PayerTypeID");
                            sqlBulk.ColumnMappings.Add("CreatedBy", "CreatedBy");
                            sqlBulk.ColumnMappings.Add("CreationDate", "CreationDate");
                            sqlBulk.ColumnMappings.Add("IsActive", "IsActive");
                            sqlBulk.WriteToServer(newRecords);

                        }
                        else if (source == "Clinicians")
                        {

                            sqlBulk.DestinationTableName = "Clinicians";
                            sqlBulk.ColumnMappings.Add("license", "ClinicianLicense");
                            sqlBulk.ColumnMappings.Add("name", "ClinicianName");

                            if (newRecords.Columns.Contains("facilityName"))
                            {
                                sqlBulk.ColumnMappings.Add("facilityName", "FacilityName");
                            } if (newRecords.Columns.Contains("location"))
                            {
                                sqlBulk.ColumnMappings.Add("location", "Location");
                            }

                            if (newRecords.Columns.Contains("source"))
                            {
                                sqlBulk.ColumnMappings.Add("source", "Source");
                            }

                            if (newRecords.Columns.Contains("username"))
                            {
                                sqlBulk.ColumnMappings.Add("username", "UserName");
                            }

                            if (newRecords.Columns.Contains("password"))
                            {
                                sqlBulk.ColumnMappings.Add("password", "Password");
                            }
                            sqlBulk.ColumnMappings.Add("specialtyId", "SpecialtyId");
                            sqlBulk.ColumnMappings.Add("isActive", "IsActive");
                            // sqlBulk.ColumnMappings.Add("SpecialtyGroup", "SpecialtyGroup");
                            //if (newRecords.Columns.Contains("activeFrom"))
                            //{
                            //    sqlBulk.ColumnMappings.Add("activeFrom", "LicenseStartDate");
                            //}
                            //if (newRecords.Columns.Contains("activeTo"))
                            //{
                            //    sqlBulk.ColumnMappings.Add("activeTo", "LicenseEndDate");
                            //}



                            sqlBulk.WriteToServer(newRecords);

                        }

                        else if (source == "Providers")
                        {

                            sqlBulk.DestinationTableName = "Provider";
                            sqlBulk.ColumnMappings.Add("license", "LicenseID");
                            sqlBulk.ColumnMappings.Add("name", "Name");
                            sqlBulk.ColumnMappings.Add("typeId", "ProviderType");
                        
                       

                            //if (newRecords.Columns.Contains("email"))
                            //{
                            //    sqlBulk.ColumnMappings.Add("email", "Email");
                            //} 
                            if (newRecords.Columns.Contains("emirate"))
                            {
                                sqlBulk.ColumnMappings.Add("emirate", "Region");
                            }

                            if (newRecords.Columns.Contains("source"))
                            {
                                sqlBulk.ColumnMappings.Add("source", "ProviderSource");
                            }


                            sqlBulk.ColumnMappings.Add("CreatedBy", "CreatedBy");
                            sqlBulk.ColumnMappings.Add("CreationDate", "CreationDate");
                            sqlBulk.ColumnMappings.Add("IsActive", "IsActive");
        
                            sqlBulk.WriteToServer(newRecords);

                        }
                        else if (source == "ICDs")
                        {

                            sqlBulk.DestinationTableName = destination;
                            sqlBulk.ColumnMappings.Add("code", "Code");
                            sqlBulk.ColumnMappings.Add("description", "Description");
                            sqlBulk.ColumnMappings.Add("shortDescription", "[Short Description]");
                            sqlBulk.ColumnMappings.Add("vidalID", "VidalId");
                            sqlBulk.WriteToServer(newRecords);

                        }
                        else if (source == "DhaDenialCodes")
                        {

                            sqlBulk.DestinationTableName = destination;
                            sqlBulk.ColumnMappings.Add("Code", "Code");
                            sqlBulk.ColumnMappings.Add("Description", "Description");
                            sqlBulk.WriteToServer(newRecords);

                        }
                        else if (source == "DhaRouteOfAdministration")
                        {

                            sqlBulk.DestinationTableName = destination;
                            sqlBulk.ColumnMappings.Add("code", "Code");
                            sqlBulk.ColumnMappings.Add("description", "Name");
                            sqlBulk.WriteToServer(newRecords);

                        }

                    }

                    catch (Exception e)
                    {
                        logException ex = new logException();
                        ex.storeExcepion(e);
                    }
                    finally

                    {

                        destinationConnection.Dispose();
                        destinationConnection.Close();
                    
                    }
                }
            }
        }
    }
}