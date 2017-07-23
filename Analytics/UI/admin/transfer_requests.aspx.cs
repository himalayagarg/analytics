using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class UI_admin_transfer_requests : System.Web.UI.Page
{
    protected override PageStatePersister PageStatePersister
    {
        get
        {
            //return base.PageStatePersister; for solve connection reset by server problem.
            return new SessionPageStatePersister(this);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ((MenuItem)((Menu)Master.FindControl("Menu1")).FindItem("Transfer Request")).Selected = true;

            BindDDLuserFrom();            
        }

    }

    private void BindDDLuserFrom()
    {
        DataTable dt = m_users.getAllUsers();
        dt = removeSuperUser(dt);
        DataView dv = dt.AsDataView();
        dv.Sort = "fullname";
        dt = dv.ToTable();
        ddl_userFrom.DataSource = dt;
        ddl_userFrom.DataTextField = "fullname";
        ddl_userFrom.DataValueField = "userid";
        ddl_userFrom.DataBind();
    }

    private DataTable removeSuperUser(DataTable dt)
    {
        DataRow[] dr_superuser = dt.Select("fullname='superuser'");
        foreach (DataRow dr in dr_superuser)
        {
            dr.Delete();
        }
        return dt;
    }
}