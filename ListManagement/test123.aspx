<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="test123.aspx.cs" Inherits="ListManagement.test123" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
            Text="Impersonate" />
        <asp:TextBox ID="txtIsImpersonat" runat="server"></asp:TextBox>
    
    </div>
    <asp:TextBox ID="txtError" runat="server" Height="403px" Width="598px"></asp:TextBox>
    <asp:FileUpload ID="FileUpload1" runat="server" />
    <asp:Button ID="btnUpload" runat="server" OnClick="UploadFile" 
        OnClientClick="javascript:showWait();" Text="Upload &amp; Validate" />
    <asp:Button ID="Button2" runat="server" onclick="Button2_Click" 
        Text="download" />
    </form>
</body>
</html>
