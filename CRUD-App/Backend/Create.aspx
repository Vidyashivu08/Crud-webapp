<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Create.aspx.cs" Inherits="Backend.Create" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Create New Record</title>
    <link href="~/Styles/Site.css" rel="stylesheet" type="text/css" />
</head>

<body>
    <form id="form1" runat="server">
        <div class="container">
            <h1>Create New Record</h1>
            
            <div class="card">
                <div>
                    <asp:Label runat="server" Text="Name:" AssociatedControlID="txtName" />
                    <asp:TextBox runat="server" ID="txtName" />
                </div>
                
                <div style="margin-top: 20px;">
                    <asp:Label runat="server" Text="Description:" AssociatedControlID="txtDescription" />
                    <asp:TextBox runat="server" ID="txtDescription" TextMode="MultiLine" Rows="4" />
                </div>
                
                <div style="margin-top: 30px; text-align: center;">
                    <asp:Button runat="server" ID="btnSave" Text="Save" OnClick="btnSave_Click" CssClass="button" />
                    <asp:HyperLink runat="server" NavigateUrl="~/Default.aspx" Text="Cancel" CssClass="button" style="background-color: #e74c3c; margin-left: 10px;" />
                </div>
            </div>
        </div>
    </form>
</body>

</html>
