<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="ListManagement.Home" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:LinkButton ID="hplDrugs" runat="server" onclick="drugsClicked" >Drugs List</asp:LinkButton><br />
    <asp:LinkButton ID="hplServices" runat="server" onclick="clickServices">Dubai Special Services</asp:LinkButton><br />
    <asp:LinkButton ID="hplCPT" runat="server" onclick="clickCPT">CPT List</asp:LinkButton><br />
    <asp:LinkButton ID="hplDenial" runat="server" onclick="clickHCPCS">DHA HCPCS List</asp:LinkButton><br />
     <asp:LinkButton ID="hplPayers" runat="server" onclick="clickPayers">Payers List</asp:LinkButton><br />
       <asp:LinkButton ID="hplSelfPaid" runat="server" onclick="hplSelfPaid_Click" >Self Paid</asp:LinkButton><br />
     <asp:LinkButton ID="hplDenialCodes" runat="server" onclick="clickDenial">DHA Denial Codes List</asp:LinkButton><br />
     <asp:LinkButton ID="hplClinicians" runat="server" onclick="clickClinicians">Clinicians List</asp:LinkButton><br />
     
      <asp:LinkButton ID="hplICD" runat="server" onclick="clickICD">ICD List</asp:LinkButton><br />
      <asp:LinkButton ID="hplGenerics" runat="server" onclick="clickGenerics">DDC Scientific</asp:LinkButton><br />
      <asp:LinkButton ID="hplROA" runat="server" onclick="clickROA">DHA Route of Administration</asp:LinkButton><br />
      <asp:LinkButton ID="btnSpecialties" runat="server" onclick="ClickSpecialties">Specialties</asp:LinkButton><br />
      <asp:LinkButton ID="btnProviders" runat="server" onclick="btnProviders_Click" >Providers</asp:LinkButton><br />
        <br />
      
    </div>
    </form>
</body>
</html>
