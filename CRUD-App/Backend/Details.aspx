<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Details.aspx.cs" Inherits="Backend.Details" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Record Details</title>
    <link href="~/Styles/Site.css" rel="stylesheet" type="text/css" />
</head>

<body>
    <form id="form1" runat="server">
        <div class="container">
            <h1>Record Details</h1>
            
            <div class="card">
                <div>
                    <h2>Record Information</h2>
                    <div style="margin-bottom: 20px;">
                        <asp:Label runat="server" ID="lblName" CssClass="detail-label" />
                    </div>
                    <div>
                        <asp:Label runat="server" ID="lblDescription" CssClass="detail-description" />
                    </div>
                </div>
                
                <div style="text-align: center; margin-top: 30px;">
                    <asp:HyperLink runat="server" NavigateUrl="~/Default.aspx" 
                        CssClass="button" Text="Back to List" />
                </div>
            </div>
        </div>
    </form>
</body>

</html>
