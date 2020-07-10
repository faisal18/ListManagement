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
    public class CheckRecordsForUpdate
    {
        String strConnString = ConfigurationManager.ConnectionStrings["LMUConnectionString"].ConnectionString;
        public DataTable checkExistingForUpdate(DataSet ds, string destination)
        {
            try
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
            catch (Exception e)
            {
                logException ex = new logException();
                ex.storeExcepion(e);
                DataTable dt = new DataTable();
                return dt;
            }

        }
    }
}