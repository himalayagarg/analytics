using System;
using System.Data;

public partial class sitemaintain : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {        
        DataTable dt_service = other.getDropdownsbyType("serviceuptime");
        if (dt_service.Rows.Count > 0)
        {
            lbl_text.Text = dt_service.Rows[0]["value"].ToString().Replace("\n", "<br>");
        }
    }
    protected void btn_ok_Click(object sender, EventArgs e)
    {
        //allow only superadmin on validating the password
        DataTable dt_superuser = other.getDropdownsbyType("superuser");
        if (dt_superuser.Rows.Count > 0)
        {
            if (dt_superuser.Rows[0]["value"].ToString() == txtbx_pass.Text)
            {
                Session["userid"] = "superuser";
                Response.Redirect("~/UI/admin/home.aspx");
            }
            else
            {

            }
        }
    }
}