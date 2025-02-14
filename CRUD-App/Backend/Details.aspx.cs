using System;

namespace Backend
{
    public partial class Details : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string id = Request.QueryString["id"];
                
                if (!string.IsNullOrEmpty(id))
                {
                    // TODO: Fetch record details from database
                    lblName.Text = "Record Name for ID: " + id;
                    lblDescription.Text = "Description for record ID: " + id;
                }
                else
                {
                    Response.Redirect("~/Default.aspx");
                }
            }
        }
    }
}
