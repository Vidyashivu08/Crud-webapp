using System;

namespace Backend
{
    public partial class Edit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string id = Request.QueryString["id"];
                
                if (!string.IsNullOrEmpty(id))
                {
                    // TODO: Fetch record details from database
                    txtName.Text = "Existing Name for ID: " + id;
                    txtDescription.Text = "Existing Description for ID: " + id;
                }
                else
                {
                    Response.Redirect("~/Default.aspx");
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            // Save logic will be implemented here
            // For now, just redirect to the default page
            Response.Redirect("~/Default.aspx");
        }
    }
}
