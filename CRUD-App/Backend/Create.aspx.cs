using System;

namespace Backend
{
    public partial class Create : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Page load logic can be added here
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            // Save logic will be implemented here
            // For now, just redirect to the default page
            Response.Redirect("~/Default.aspx");
        }
    }
}
