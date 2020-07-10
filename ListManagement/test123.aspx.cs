using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.IO;

namespace ListManagement
{
    public partial class test123 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


        }

        protected void Button1_Click(object sender, EventArgs e)
        {
        //    txtIsImpersonat.Text = "";
           
        //    UserImpersonation impersonator = new UserImpersonation();
        //    try
        //    {

        //        txtIsImpersonat.Text = impersonator.impersonateUser(NetworkUser, "", NetworkPassword).ToString();
                

        //    }
        //    catch (Exception ex)
        //    {
        //        txtError.Text = ex.InnerException.ToString();
        //    }
        //    finally

        //    {
        //        impersonator.undoimpersonateUser();
            
        //    }
        }

        protected void UploadFile(object sender, EventArgs e)
        {
            string SharedNetworkPath = ConfigurationManager.AppSettings["SharedNetworkPath"].ToString();
            string NetworkUser = ConfigurationManager.AppSettings["NetworkUser"].ToString();
            string NetworkPassword = ConfigurationManager.AppSettings["NetworkPassword"].ToString();
            string IsNetworkUpload = ConfigurationManager.AppSettings["IsNetworkUpload"].ToString();

            UserImpersonation impersonator = new UserImpersonation();
           

           try
           {
               impersonator.impersonateUser(NetworkUser, "", NetworkPassword).ToString();
               string Path = SharedNetworkPath ;
               string FileName = FileUpload1.FileName;
               string sourcefile = Path + "\\\\" + "Submission\\DHA-F-9999998\\2015\\2\\" + FileName;

                



                FileUpload1.PostedFile.SaveAs(sourcefile);


           }
           catch (Exception ex)
           {
               txtError.Text = ex.Message;
           }
           finally
           {
               impersonator.undoimpersonateUser();

           }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            string FileName = "network comman line.txt";
              string SharedNetworkPath = ConfigurationManager.AppSettings["SharedNetworkPath"].ToString();
            string NetworkUser = ConfigurationManager.AppSettings["NetworkUser"].ToString();
            string NetworkPassword = ConfigurationManager.AppSettings["NetworkPassword"].ToString();
            string IsNetworkUpload = ConfigurationManager.AppSettings["IsNetworkUpload"].ToString();
            string Path = SharedNetworkPath;
            string sourcefile = Path + "\\\\" + "Submission\\DHA-F-9999998\\2015\\2\\" + FileName;
            UserImpersonation impersonator = new UserImpersonation();
            try
            {
                


                    impersonator.impersonateUser(NetworkUser, "", NetworkPassword); //No Domain is required


                 

      

              
                    if (!File.Exists(sourcefile))
                    {
                        
                    }else{

                
                HttpContext.Current.Response.ContentType =
                           "application/octet-stream";
                HttpContext.Current.Response.AddHeader("Content-Disposition",
                  "attachment; filename=\"" + FileName + "\"");
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.WriteFile(sourcefile);
                HttpContext.Current.Response.End();
                    }

            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (IsNetworkUpload == "1")
                {


                    impersonator.undoimpersonateUser();

                }

            }
        }
    }
}