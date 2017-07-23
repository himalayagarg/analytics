using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UI_users_request1 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ((MenuItem)((Menu)Master.FindControl("Menu1")).FindItem("Make Request")).Selected = true;
        }
        else
        {
 
        }
    }
    protected void btn_ok_Click(object sender, EventArgs e)
    {
        int no_samples = Convert.ToInt32(tb_samples.Text);
        int no_tests = Convert.ToInt32(tb_tests.Text);
        Encryption64 e64 = new Encryption64();
        Response.Redirect("~/UI/users/request2.aspx?nosample=" + e64.Encrypt(no_samples.ToString()) + "&notest=" + e64.Encrypt(no_tests.ToString()) + "&rtype=" + e64.Encrypt("Pre-formulation Development"));
    }
}