<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Backend.Default" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>CRUD Application</title>
    <link href="~/Styles/Site.css" rel="stylesheet" type="text/css" />
</head>

<body>
    <form id="form1" runat="server">
        <div class="container">
            <h1>Welcome to the CRUD Application</h1>
            
            <div class="card">
                <h2>Sample Record 1</h2>
                <p>This is a sample record description.</p>
                <asp:HyperLink runat="server" NavigateUrl="~/Details.aspx?id=1" 
                    CssClass="button" Text="View Details" />
            </div>
            
            <div class="card">
                <h2>Sample Record 2</h2>
                <p>Another sample record with more details.</p>
                <asp:HyperLink runat="server" NavigateUrl="~/Details.aspx?id=2" 
                    CssClass="button" Text="View Details" />
            </div>
            
            <div style="text-align: center; margin-top: 30px;">
                <asp:HyperLink runat="server" NavigateUrl="~/Create.aspx" 
                    CssClass="button" Text="Create New Record" />
            </div>
        </div>
    </form>
</body>

</html>
