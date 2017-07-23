using System;
using System.Web.UI;
using System.Collections;

public partial class UI_users_users : System.Web.UI.MasterPage
{
    protected override void AddedControl(Control control, int index)
    {
        // This is necessary because Safari and Chrome browsers don't display the Menu control correctly.
        // Add this to the code in your master page.
        if ((Request.ServerVariables["http_user_agent"].IndexOf("Chrome", StringComparison.CurrentCultureIgnoreCase) != -1)
            || (Request.ServerVariables["http_user_agent"].IndexOf("Safari", StringComparison.CurrentCultureIgnoreCase) != -1)
            || (Request.ServerVariables["http_user_agent"].IndexOf("Firefox", StringComparison.CurrentCultureIgnoreCase) != -1)
            || (Request.ServerVariables["http_user_agent"].IndexOf("Explorer", StringComparison.CurrentCultureIgnoreCase) != -1)
            || (Request.ServerVariables["http_user_agent"].IndexOf("Netscape", StringComparison.CurrentCultureIgnoreCase) != -1)
            || (Request.ServerVariables["http_user_agent"].IndexOf("Opera", StringComparison.CurrentCultureIgnoreCase) != -1)
            || (Request.ServerVariables["http_user_agent"].IndexOf("IE", StringComparison.CurrentCultureIgnoreCase) != -1)
           )
            this.Page.ClientTarget = "uplevel";

        base.AddedControl(control, index);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userid"] == null)
        {
            Response.Redirect("~/Default.aspx");
        }
        if (!IsPostBack)
        {
            string userid = Session["userid"].ToString();
            ds_analytics.m_usersRow user_row = m_users.getUserByUserid(userid)[0];
            lbl_user.Text = user_row.fullname;
            lbl_time.Text = user_row.lastlogintime.ToString("dd/MM/yyyy HH:mm");            

            //Set Role/ModuleList DropDown
            ArrayList arr_mod = m_users.getModulesByUserid(userid);
            ddl_moduleList.DataSource= arr_mod;
            ddl_moduleList.DataBind();
            ddl_moduleList.SelectedValue = "Requests";

            //Setting display of Role Dropdown
            if (arr_mod.Count == 1)
            {
                ddl_moduleList.Visible = false;
            }

            //SetPage According to User Access Rights
            setPageByRights(userid);
        }
        else
        {
            
        }
    }

    private void setPageByRights(string userid)
    {
        ArrayList arr_List_subMod = m_users.getSubModulesByUserid(userid);        
        if (!arr_List_subMod.Contains("Requestor"))
        {
            Menu1.Items[0].Text = "";
            Menu1.Items[0].Enabled = false;
        }
    }

    protected void lb_logout_Click(object sender, EventArgs e)
    {
        string userid = Session["userid"].ToString();
        ds_analytics.m_usersRow user_row = m_users.getUserByUserid(userid)[0];
        user_row.isloggedin = false;
        user_row.lastlogintime = DateTime.Now;
        m_users.update(user_row);

        Session.Abandon();
        Session.RemoveAll();
        Session.Clear();
        
        Response.Redirect("~/Default.aspx?logout");
    }
    
    protected void ddl_moduleList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_moduleList.SelectedValue == "Requests")
        {
            Response.Redirect("~/UI/users/home.aspx");
        }
        else if (ddl_moduleList.SelectedValue == "Admin")
        {
            Response.Redirect("~/UI/admin/home.aspx");
        }
    }
}
