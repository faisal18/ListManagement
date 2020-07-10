using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Net;
using System.Text;
namespace ListManagement
{
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        protected void drugsClicked(object sender, EventArgs e)
        {
            Session["listName"] = "DDC";
            Response.Redirect("ListUpdate.aspx");
        }

        protected void clickServices(object sender, EventArgs e)
        {
            Session["listName"] = "DubaiSpecialServices";
            Response.Redirect("ListUpdate.aspx");
        }
        protected void clickCPT(object sender, EventArgs e)
        {
            Session["listName"] = "DhaCPT";
            Response.Redirect("ListUpdate.aspx");
        }
        protected void clickHCPCS(object sender, EventArgs e)
        {
            Session["listName"] = "DhaHCPCS";
            Response.Redirect("ListUpdate.aspx");
        }

        protected void clickPayers(object sender, EventArgs e)
        {
            Session["listName"] = "Payers";
            Response.Redirect("ListUpdate.aspx");
        }

        protected void clickClinicians(object sender, EventArgs e)
        {
            Session["listName"] = "Clinicians";
            Response.Redirect("ListUpdate.aspx");
        }

        protected void ClickSpecialties(object sender, EventArgs e)
        {
            Session["listName"] = "Specialties";
            Response.Redirect("ListUpdate.aspx");
        }
        protected void clickICD(object sender, EventArgs e)
        {
            Session["listName"] = "ICDs";
            Response.Redirect("ListUpdate.aspx");
        }

        protected void clickDenial(object sender, EventArgs e)
        {
            Session["listName"] = "DhaDenialCodes";
            Response.Redirect("ListUpdate.aspx");
        }

        protected void clickGenerics(object sender, EventArgs e)
        {
            Session["listName"] = "DdcScientific";
            Response.Redirect("ListUpdate.aspx");
        }

        protected void clickROA(object sender, EventArgs e)
        {
            Session["listName"] = "DhaRouteOfAdministration";
            Response.Redirect("ListUpdate.aspx");
        }

        protected void btnProviders_Click(object sender, EventArgs e)
        {
            Session["listName"] = "Providers";
            Response.Redirect("ListUpdate.aspx");
        }

        protected void hplSelfPaid_Click(object sender, EventArgs e)
        {

            Session["listName"] = "DhaSelfPaid";
            Response.Redirect("ListUpdate.aspx");
        }

      
    }
}