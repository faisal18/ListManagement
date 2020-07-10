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
    public class CheckRecordsForInsert
    {
        String strConnString = ConfigurationManager.ConnectionStrings["LMUConnectionString"].ConnectionString;
        public DataTable checkExistingForInsert(DataSet ds, string destination, string source, DataTable dtAll)
        {

            DataTable dtRecords = ds.Tables[0];
            if (source == "DDC")
            {
                dtRecords.Columns.Add("cType");
                dtRecords.Columns.Add("Status");
                foreach (DataRow d in dtRecords.Rows)
                {
                    d["cType"] = "DRUGS";

                    d["Status"] = 1;
                    if (string.IsNullOrEmpty(d["discontinueDate"].ToString()))
                    {
                        d["discontinueDate"] = null;

                    }
                    else if (d["discontinueDate"].ToString().ToUpper() == "NULL")
                    {

                        d["discontinueDate"] = null;
                    }


                }


                var queryXml = dtRecords.AsEnumerable().Select(a => new
                {
                    ID = a["Code"].ToString()
                });

                var queryAll = dtAll.AsEnumerable().Select(b => new
                {
                    ID = b["cCode"].ToString()
                });

                var exceptResultsAB = queryXml.Except(queryAll);
                int abc = exceptResultsAB.Count();
                int abcd = dtRecords.Rows.Count;
                DataTable dtforInsert = new DataTable();
                bool hasNull = exceptResultsAB.Any(i => i.ID == "");
                if ((exceptResultsAB.Count() < 1 && hasNull.Equals(true)) || (exceptResultsAB.Count() == 0))
                {
                }
                else
                {
                    dtforInsert = (from a in dtRecords.AsEnumerable()
                                   join ab in exceptResultsAB on a["Code"].ToString() equals ab.ID
                                   select a).CopyToDataTable();
                }
                for (int i = dtforInsert.Rows.Count - 1; i >= 0; i--)
                {
                    DataRow dr = dtforInsert.Rows[i];
                    if (dr["Code"].ToString() == "")
                        dr.Delete();
                }

                int f = dtforInsert.Rows.Count;
                return dtforInsert;

            }
            else if (source == "DubaiSpecialServices")
            {
                dtRecords.Columns.Add("cType");
                dtRecords.Columns.Add("Status");
                foreach (DataRow d in dtRecords.Rows)
                {
                    d["cType"] = "SERVICES";
                    d["Status"] = 1;
                }




                var queryXml = dtRecords.AsEnumerable().Select(a => new
                {
                    ID = a["code"].ToString()
                });

                var queryAll = dtAll.AsEnumerable().Select(b => new
                {
                    ID = b["cCode"].ToString()
                });

                var exceptResultsAB = queryXml.Except(queryAll);
                int abc = exceptResultsAB.Count();
                int abcd = dtRecords.Rows.Count;
                DataTable dtforInsert = new DataTable();
                bool hasNull = exceptResultsAB.Any(i => i.ID == "");
                if ((exceptResultsAB.Count() < 1 && hasNull.Equals(true)) || (exceptResultsAB.Count() == 0))
                {
                }
                else
                {
                    dtforInsert = (from a in dtRecords.AsEnumerable()
                                   join ab in exceptResultsAB on a["code"].ToString() equals ab.ID
                                   select a).CopyToDataTable();
                }
                int f = dtforInsert.Rows.Count;
                return dtforInsert;


            }
            else if (source == "DhaCPT")
            {
                dtRecords.Columns.Add("cType");
                dtRecords.Columns.Add("Status");
                foreach (DataRow d in dtRecords.Rows)
                {
                    d["cType"] = "CPT";
                    d["Status"] = 1;
                }



                var queryXml = dtRecords.AsEnumerable().Select(a => new
                {
                    ID = a["code"].ToString()
                });

                var queryAll = dtAll.AsEnumerable().Select(b => new
                {
                    ID = b["cCode"].ToString()
                });

                var exceptResultsAB = queryXml.Except(queryAll);
                int abc = exceptResultsAB.Count();
                int abcd = dtRecords.Rows.Count;
                DataTable dtforInsert = new DataTable();
                bool hasNull = exceptResultsAB.Any(i => i.ID == "");
                if ((exceptResultsAB.Count() < 1 && hasNull.Equals(true)) || (exceptResultsAB.Count() == 0))
                {
                }
                else
                {
                    dtforInsert = (from a in dtRecords.AsEnumerable()
                                   join ab in exceptResultsAB on a["code"].ToString() equals ab.ID
                                   select a).CopyToDataTable();
                }
                int f = dtforInsert.Rows.Count;
                return dtforInsert;

            }
            else if (source == "DhaHCPCS")
            {
                dtRecords.Columns.Add("cType");
                dtRecords.Columns.Add("Status");
                foreach (DataRow d in dtRecords.Rows)
                {
                    d["cType"] = "HCPCS";
                    d["Status"] = 1;
                }




                var queryXml = dtRecords.AsEnumerable().Select(a => new
                {
                    ID = a["code"].ToString()
                });

                var queryAll = dtAll.AsEnumerable().Select(b => new
                {
                    ID = b["cCode"].ToString()
                });

                var exceptResultsAB = queryXml.Except(queryAll);
                int abc = exceptResultsAB.Count();
                int abcd = dtRecords.Rows.Count;
                DataTable dtforInsert = new DataTable();
                bool hasNull = exceptResultsAB.Any(i => i.ID == "");
                if ((exceptResultsAB.Count() < 1 && hasNull.Equals(true)) || (exceptResultsAB.Count() == 0))
                {
                }
                else
                {
                    dtforInsert = (from a in dtRecords.AsEnumerable()
                                   join ab in exceptResultsAB on a["code"].ToString() equals ab.ID
                                   select a).CopyToDataTable();
                }
                int f = dtforInsert.Rows.Count;
                return dtforInsert;


            }
            else if (source == "DdcScientific")
            {
                dtRecords.Columns.Add("cType");
                dtRecords.Columns.Add("Status");

                foreach (DataRow d in dtRecords.Rows)
                {
                    d["cType"] = "GENERICS";
                    d["Status"] = 1;
                }



                var queryXml = dtRecords.AsEnumerable().Select(a => new
                {
                    ID = a["scientificCode"].ToString()
                });

                var queryAll = dtAll.AsEnumerable().Select(b => new
                {
                    ID = b["cCode"].ToString()
                });

                var exceptResultsAB = queryXml.Except(queryAll);
                int abc = exceptResultsAB.Count();
                int abcd = dtRecords.Rows.Count;
                DataTable dtforInsert = new DataTable();
                bool hasNull = exceptResultsAB.Any(i => i.ID == "");
                if ((exceptResultsAB.Count() < 1 && hasNull.Equals(true)) || (exceptResultsAB.Count() == 0))
                {
                }
                else
                {
                    dtforInsert = (from a in dtRecords.AsEnumerable()
                                   join ab in exceptResultsAB on a["scientificCode"].ToString() equals ab.ID
                                   select a).CopyToDataTable();
                }
                int f = dtforInsert.Rows.Count;
                return dtforInsert;


            }

            else if (source == "Payers")
            {
                dtRecords.Columns.Add("PayerTypeID");
                dtRecords.Columns.Add("CreatedBy");
                dtRecords.Columns.Add("CreationDate");
                dtRecords.Columns.Add("IsActive");
                foreach (DataRow d in dtRecords.Rows)
                {
                    if (d["Classification"].ToString() == "Insurance")
                    {
                        d["PayerTypeID"] = 1;
                    }
                    else if (d["Classification"].ToString() == "TPA")
                    {
                        d["PayerTypeID"] = 2;
                    }
                    else if (d["Classification"].ToString() == "Broker")
                    {
                        d["PayerTypeID"] = 3;
                    }
                    else if (d["Classification"].ToString() == "Other")
                    {
                        d["PayerTypeID"] = 4;
                    }
                    d["CreatedBy"] = 1;
                    d["CreationDate"] = DateTime.Now;
                    if (d["record_status"].ToString() == "DELETED")
                    {
                        d["IsActive"] = 0;
                    }
                    else
                    {
                        d["IsActive"] = 1;
                    }
                }


                var queryXml = dtRecords.AsEnumerable().Select(a => new
                {
                    ID = a["dhaCode"].ToString()
                });

                var queryAll = dtAll.AsEnumerable().Select(b => new
                {
                    ID = b["PayerCode"].ToString()
                });
                var exceptResultsAB = queryXml.Except(queryAll);
                int abc = exceptResultsAB.Count();
                int abcd = dtRecords.Rows.Count;
                DataTable dtforInsert = new DataTable();
                bool hasNull = exceptResultsAB.Any(i => i.ID == "");
                if ((exceptResultsAB.Count() < 1 && hasNull.Equals(true)) || (exceptResultsAB.Count() == 0))
                {
                }
                else
                {
                    dtforInsert = (from a in dtRecords.AsEnumerable()
                                   join ab in exceptResultsAB on a["dhaCode"].ToString() equals ab.ID
                                   select a).CopyToDataTable();
                }
                int f = dtforInsert.Rows.Count;
                return dtforInsert;

            }
            else if (source == "DhaSelfPaid")
            {
                dtRecords.Columns.Add("PayerTypeID");
                dtRecords.Columns.Add("CreatedBy");
                dtRecords.Columns.Add("CreationDate");
                dtRecords.Columns.Add("IsActive");
                foreach (DataRow d in dtRecords.Rows)
                {


                    d["PayerTypeID"] = 6;

                    d["CreatedBy"] = 1;
                    d["CreationDate"] = DateTime.Now;
                    if (d["record_status"].ToString() == "DELETED")
                    {
                        d["IsActive"] = 0;
                    }
                    else
                    {
                        d["IsActive"] = 1;
                    }
                }


                var queryXml = dtRecords.AsEnumerable().Select(a => new
                {
                    ID = a["eClaimLinkId"].ToString()
                });

                var queryAll = dtAll.AsEnumerable().Select(b => new
                {
                    ID = b["PayerCode"].ToString()
                });
                var exceptResultsAB = queryXml.Except(queryAll);
                int abc = exceptResultsAB.Count();
                int abcd = dtRecords.Rows.Count;
                DataTable dtforInsert = new DataTable();
                bool hasNull = exceptResultsAB.Any(i => i.ID == "");
                if ((exceptResultsAB.Count() < 1 && hasNull.Equals(true)) || (exceptResultsAB.Count() == 0))
                {
                }
                else
                {
                    dtforInsert = (from a in dtRecords.AsEnumerable()
                                   join ab in exceptResultsAB on a["eClaimLinkId"].ToString() equals ab.ID
                                   select a).CopyToDataTable();
                }
                int f = dtforInsert.Rows.Count;
                return dtforInsert;

            }

            else if (source == "Providers")
            {

                dtRecords.Columns.Add("CreatedBy");
                dtRecords.Columns.Add("CreationDate");
                dtRecords.Columns.Add("IsActive");
                for (int i = dtRecords.Rows.Count - 1; i >= 0; i--)
                {
                    DataRow d = dtRecords.Rows[i];

                    d["CreatedBy"] = 1;
                    d["CreationDate"] = DateTime.Now;

                    if (d["record_status"].ToString() == "DELETED")
                    {
                        d["IsActive"] = 0;
                    }
                    else
                    {
                        d["IsActive"] = 1;
                    }

                    //if (d.Table.Columns.Contains("isActive"))
                    //{
                    //    if (d["isActive"].ToString().ToUpper() == "TRUE")
                    //    {

                    //        d["isActive"] = 1;

                    //    }
                    //    else
                    //    {
                    //        d["isActive"] = 0;

                    //    }
                    //}
                    if (d["source"].ToString() == "HAAD")
                        dtRecords.Rows.Remove(d);

                }


                var queryXml = dtRecords.AsEnumerable().Select(a => new
                {
                    ID = a["license"].ToString()
                });

                var queryAll = dtAll.AsEnumerable().Select(b => new
                {
                    ID = b["LicenseID"].ToString()
                });
                var exceptResultsAB = queryXml.Except(queryAll);
                int abc = exceptResultsAB.Count();
                int abcd = dtRecords.Rows.Count;
                DataTable dtforInsert = new DataTable();
                bool hasNull = exceptResultsAB.Any(i => i.ID == "");
                if ((exceptResultsAB.Count() < 1 && hasNull.Equals(true)) || (exceptResultsAB.Count() == 0))
                {
                }
                else
                {
                    dtforInsert = (from a in dtRecords.AsEnumerable()
                                   join ab in exceptResultsAB on a["license"].ToString() equals ab.ID
                                   select a).CopyToDataTable();
                }
                int f = dtforInsert.Rows.Count;
                return dtforInsert;

            }

            else if (source == "Clinicians")
            {






                for (int i = dtRecords.Rows.Count - 1; i >= 0; i--)
                {
                    DataRow dr = dtRecords.Rows[i];
                    if (dr.Table.Columns.Contains("isActive"))
                    {
                        if (dr["isActive"].ToString().ToUpper() == "TRUE")
                        {

                            dr["isActive"] = 1;

                        }
                        else
                        {
                            dr["isActive"] = 0;

                        }
                    }
                    //if (dr.Table.Columns.Contains("activeFrom"))
                    //{
                    //    if (string.IsNullOrEmpty(dr["activeFrom"].ToString()))
                    //    {
                    //        dr["activeFrom"] = null;

                    //    }
                    //    else if (dr["activeFrom"].ToString().ToUpper() == "NULL")
                    //    {

                    //        dr["activeFrom"] = null;
                    //    }
                    //}

                    //if (dr.Table.Columns.Contains("activeTo"))
                    //{
                    //    if (string.IsNullOrEmpty(dr["activeTo"].ToString()))
                    //    {
                    //        dr["activeTo"] = null;

                    //    }
                    //    else if (dr["activeTo"].ToString().ToUpper() == "NULL")
                    //    {

                    //        dr["activeTo"] = null;
                    //    }
                    //}

                    if (dr["source"].ToString() == "HAAD")
                        dtRecords.Rows.Remove(dr);

                }
                var queryXml = dtRecords.AsEnumerable().Select(a => new
                {
                    ID = a["license"].ToString()
                });

                var queryAll = dtAll.AsEnumerable().Select(b => new
                {
                    ID = b["ClinicianLicense"].ToString()
                });
                var exceptResultsAB = queryXml.Except(queryAll);

                DataTable dtforInsert = new DataTable();
                bool hasNull = exceptResultsAB.Any(i => i.ID == "");
                if ((exceptResultsAB.Count() < 1 && hasNull.Equals(true)) || (exceptResultsAB.Count() == 0))
                {
                }
                else
                {
                    dtforInsert = (from a in dtRecords.AsEnumerable()
                                   join ab in exceptResultsAB on a["license"].ToString() equals ab.ID
                                   select a).CopyToDataTable();
                }
                int f = dtforInsert.Rows.Count;
                return dtforInsert;

            }
            else if (source == "ICDs")
            {





                for (int i = dtRecords.Rows.Count - 1; i >= 0; i--)
                {
                    DataRow dr = dtRecords.Rows[i];
                    if (dr["type"].ToString() != "ICD10")
                        dr.Delete();
                }
                var queryXml = dtRecords.AsEnumerable().Select(a => new
                {
                    ID = a["code"].ToString()
                });

                var queryAll = dtAll.AsEnumerable().Select(b => new
                {
                    ID = b["Code"].ToString()
                });
                var exceptResultsAB = queryXml.Except(queryAll);
                int abc = exceptResultsAB.Count();
                int abcd = dtRecords.Rows.Count;
                DataTable dtforInsert = new DataTable();
                bool hasNull = exceptResultsAB.Any(i => i.ID == "");
                if ((exceptResultsAB.Count() < 1 && hasNull.Equals(true)) || (exceptResultsAB.Count() == 0))
                {
                }
                else
                {
                    dtforInsert = (from a in dtRecords.AsEnumerable()
                                   join ab in exceptResultsAB on a["code"].ToString() equals ab.ID
                                   select a).CopyToDataTable();
                }
                int f = dtforInsert.Rows.Count;
                return dtforInsert;

            }
            else if (source == "DhaDenialCodes")
            {



                var queryXml = dtRecords.AsEnumerable().Select(a => new
                {
                    ID = a["code"].ToString()
                });

                var queryAll = dtAll.AsEnumerable().Select(b => new
                {
                    ID = b["Code"].ToString()
                });
                var exceptResultsAB = queryXml.Except(queryAll);
                int abc = exceptResultsAB.Count();
                int abcd = dtRecords.Rows.Count;
                DataTable dtforInsert = new DataTable();
                bool hasNull = exceptResultsAB.Any(i => i.ID == "");
                if ((exceptResultsAB.Count() < 1 && hasNull.Equals(true)) || (exceptResultsAB.Count() == 0))
                {
                }
                else
                {
                    dtforInsert = (from a in dtRecords.AsEnumerable()
                                   join ab in exceptResultsAB on a["code"].ToString() equals ab.ID
                                   select a).CopyToDataTable();
                }
                int f = dtforInsert.Rows.Count;
                return dtforInsert;

            }
            else if (source == "DhaRouteOfAdministration")
            {


                var queryXml = dtRecords.AsEnumerable().Select(a => new
                {
                    ID = a["code"].ToString()
                });

                var queryAll = dtAll.AsEnumerable().Select(b => new
                {
                    ID = b["Code"].ToString()
                });
                var exceptResultsAB = queryXml.Except(queryAll);
                int abc = exceptResultsAB.Count();
                int abcd = dtRecords.Rows.Count;
                DataTable dtforInsert = new DataTable();
                bool hasNull = exceptResultsAB.Any(i => i.ID == "");
                if ((exceptResultsAB.Count() < 1 && hasNull.Equals(true)) || (exceptResultsAB.Count() == 0))
                {
                }
                else
                {
                    dtforInsert = (from a in dtRecords.AsEnumerable()
                                   join ab in exceptResultsAB on a["code"].ToString() equals ab.ID
                                   select a).CopyToDataTable();
                }
                int f = dtforInsert.Rows.Count;
                return dtforInsert;

            }

            else if (source == "Specialties")
            {


                var queryXml = dtRecords.AsEnumerable().Select(a => new
                {
                    ID = a["specialtyId"].ToString()
                });

                var queryAll = dtAll.AsEnumerable().Select(b => new
                {
                    ID = b["SpecialtyID"].ToString()
                });
                var exceptResultsAB = queryXml.Except(queryAll);
                int abc = exceptResultsAB.Count();
                int abcd = dtRecords.Rows.Count;
                DataTable dtforInsert = new DataTable();
                bool hasNull = exceptResultsAB.Any(i => i.ID == "");
                if ((exceptResultsAB.Count() < 1 && hasNull.Equals(true)) || (exceptResultsAB.Count() == 0))
                {
                }
                else
                {
                    dtforInsert = (from a in dtRecords.AsEnumerable()
                                   join ab in exceptResultsAB on a["specialtyId"].ToString() equals ab.ID
                                   select a).CopyToDataTable();
                }
                int f = dtforInsert.Rows.Count;
                return dtforInsert;

            }
            else
            {
                DataTable dt = new DataTable();
                return dt;
            }
        }
    }
}