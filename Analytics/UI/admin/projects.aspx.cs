using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;

public partial class UI_admin_projects : System.Web.UI.Page
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
            ((MenuItem)((Menu)Master.FindControl("Menu1")).FindItem("Projects")).Selected = true;
            ViewState["SortOrder"] = " ASC";

            ds_analytics.projectsDataTable prj_dt = projects.getAllProjects();
            gv_projects.DataSource = prj_dt;
            gv_projects.DataBind();
            ViewState["dtStored"] = prj_dt;

            btn_add.Visible = true;
            //1
            ddl_prj_type.DataSource = other.getDropdownsbyType("typeproject");
            ddl_prj_type.DataTextField = "value";
            ddl_prj_type.DataValueField = "value";
            ddl_prj_type.DataBind();
            //2
            ddl_prj_category.DataSource = other.getDropdownsbyType("categoryproject");
            ddl_prj_category.DataTextField = "value";
            ddl_prj_category.DataValueField = "value";
            ddl_prj_category.DataBind();
            //3
            ddl_prj_brand.DataSource = other.getDropdownsbyType("brandproject");
            ddl_prj_brand.DataTextField = "value";
            ddl_prj_brand.DataValueField = "value";
            ddl_prj_brand.DataBind();
        }
        else
        {

        }
    }
    
    protected void btn_add_Click(object sender, EventArgs e)
    {
        try
        {
            ds_analytics.projectsDataTable prj_dt = new ds_analytics.projectsDataTable();
            ds_analytics.projectsRow prj_row = prj_dt.NewprojectsRow();

            prj_row.projectname = tb_prj_name.Text;
            prj_row.projecttype = ddl_prj_type.SelectedItem.Text;
            prj_row.projectcategory = ddl_prj_category.SelectedItem.Text;
            prj_row.projectbrand = ddl_prj_brand.SelectedItem.Text;            
            if (tb_prj_start.Text == "") prj_row.SetcreatedateNull(); else prj_row.createdate = Convert.ToDateTime(tb_prj_start.Text);
            if (tb_prj_end.Text == "") prj_row.SetcompletiondateNull(); else prj_row.completiondate = Convert.ToDateTime(tb_prj_end.Text).AddHours(23).AddMinutes(59).AddSeconds(59);
            if (tb_prj_budget.Text == "") prj_row.SetbudgetNull(); else prj_row.budget = Convert.ToDouble(tb_prj_budget.Text);
            prj_row.isactive = cb_isactive.Checked;
            
            projects.insert(prj_row);

            string url = "projects.aspx";
            ClientScript.RegisterStartupScript(this.GetType(), "callfunction", "alert('Record Added Successfully.');window.location.href = '" + url + "';", true);
        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "error", "<script>alert('Error in adding record.');</script>");
        }
    }
    
    protected void btn_update_Click(object sender, EventArgs e)
    {
        ds_analytics.projectsRow prj_row = ((ds_analytics.projectsRow)(ViewState["prj_row"]));
        prj_row.projectname = tb_prj_name.Text;
        prj_row.projecttype = ddl_prj_type.SelectedItem.Text;
        prj_row.projectcategory = ddl_prj_category.SelectedItem.Text;
        prj_row.projectbrand = ddl_prj_brand.SelectedItem.Text;
        if (tb_prj_start.Text == "") prj_row.SetcreatedateNull(); else prj_row.createdate = Convert.ToDateTime(tb_prj_start.Text);
        if (tb_prj_end.Text == "") prj_row.SetcompletiondateNull(); else prj_row.completiondate = Convert.ToDateTime(tb_prj_end.Text).AddHours(23).AddMinutes(59).AddSeconds(59);
        if (tb_prj_budget.Text == "") prj_row.SetbudgetNull(); else prj_row.budget = Convert.ToDouble(tb_prj_budget.Text);
        prj_row.isactive = cb_isactive.Checked;

        projects.update(prj_row);

        ds_analytics.projectsDataTable prj_dt = projects.getAllProjects();
        gv_projects.DataSource = prj_dt;
        gv_projects.DataBind();
        ViewState["dtStored"] = prj_dt;

        Reset();
    }
    
    protected void btn_cancel_Click(object sender, EventArgs e)
    {
        Reset();
    }
    
    protected void btn_search_Click(object sender, EventArgs e)
    {
        ds_analytics.projectsDataTable prj_dt = projects.getAllProjectsByName(tb_projectname_search.Text);
        gv_projects.DataSource = prj_dt;
        gv_projects.DataBind();

        ViewState["dtStored"] = prj_dt;
    }
    
    protected void gv_projects_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //1. setting hover
            e.Row.CssClass = "row";

            //2. Active/Inactive
            if (e.Row.Cells[9].Text == "True")
            {
                e.Row.Cells[9].Text = "Active";
            }
            else
            {
                e.Row.Cells[9].Text = "Non-Active";
            }

            //3. Count requests in a project
            long prjid = Convert.ToInt64(gv_projects.DataKeys[e.Row.RowIndex].Value);
            int no_of_req_in_prj = requests.countReqByProjID(prjid);
            ((ImageButton)e.Row.FindControl("imgbtn_View")).ToolTip = "View Requests(" + no_of_req_in_prj.ToString() + ")";
            if (no_of_req_in_prj > 0)
            {
                ((ImageButton)e.Row.FindControl("imgbtn_View")).BorderColor = Color.Green;
            }            
        }
    }
    
    protected void gv_projects_Sorting(object sender, GridViewSortEventArgs e)
    {
        DataTable dt = (DataTable)ViewState["dtStored"];
        DataView dv = new DataView(dt);
        if (ViewState["SortOrder"].ToString() == " ASC")
        {
            dv.Sort = e.SortExpression + " ASC";
            ViewState["SortOrder"] = " DESC";
        }
        else
        {
            dv.Sort = e.SortExpression + " DESC";
            ViewState["SortOrder"] = " ASC";
        }
        dt = dv.ToTable();
        gv_projects.DataSource = dt;
        gv_projects.DataBind();
        ViewState["dtStored"] = dt;
    }
    
    protected void imgbtn_Edit_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gv_row = (GridViewRow)((sender as ImageButton).NamingContainer);
        long prjid = Convert.ToInt64(gv_projects.DataKeys[gv_row.RowIndex].Value);

        ds_analytics.projectsDataTable thisprj_dt = projects.getProjectByPrjid(prjid);
        if (thisprj_dt.Rows.Count > 0)
        {
            ds_analytics.projectsRow prj_row = thisprj_dt[0];

            tb_prj_name.Text = prj_row.projectname;
            ddl_prj_type.SelectedValue = prj_row.projecttype;
            ddl_prj_category.SelectedValue = prj_row.projectcategory;
            ddl_prj_brand.SelectedValue = prj_row.projectbrand;
            if (prj_row.IscreatedateNull()) tb_prj_start.Text = ""; else tb_prj_start.Text = prj_row.createdate.ToString("dd/MM/yyyy");
            if (prj_row.IscompletiondateNull()) tb_prj_end.Text = ""; else tb_prj_end.Text = prj_row.completiondate.ToString("dd/MM/yyyy");
            if (prj_row.IsbudgetNull()) tb_prj_budget.Text = ""; else tb_prj_budget.Text = Convert.ToString(prj_row.budget);
            cb_isactive.Checked = prj_row.isactive;

            ViewState["prj_row"] = prj_row;

            btn_add.Visible = false;
            btn_update.Visible = true;
            btn_cancel.Visible = true;
        }
        else
        {
            ClientScript.RegisterStartupScript(this.GetType(), "error", "<script>alert('Error in reading project details.');</script>");
        }
    }

    protected void imgbtn_Delete_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gv_row = (GridViewRow)((sender as ImageButton).NamingContainer);
        long prj_id = Convert.ToInt64(gv_projects.DataKeys[gv_row.RowIndex].Value);
        int prj_req = requests.getAllRequestsByProjID(prj_id).Rows.Count;
        if (prj_req == 0)
        {
            projects.delete(prj_id);
            ds_analytics.projectsDataTable prj_dt = projects.getAllProjects();
            gv_projects.DataSource = prj_dt;
            gv_projects.DataBind();
            ViewState["dtStored"] = prj_dt;
            ClientScript.RegisterStartupScript(this.GetType(), "error", "<script>alert('Project deleted Successfully.');</script>");
        }
        else
        {
            ClientScript.RegisterStartupScript(this.GetType(), "error", "<script>alert('Project cannot be deleted as some requests exists related to the project.');</script>");
        }
    }

    protected void imgbtn_View_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gv_row = (GridViewRow)((sender as ImageButton).NamingContainer);
        long prjid = Convert.ToInt64(gv_projects.DataKeys[gv_row.RowIndex].Value);

        Encryption64 e64 = new Encryption64();
        Response.Redirect("~/UI/admin/prj_requests.aspx?prj=" + e64.Encrypt(prjid.ToString())) ;
    }

    private void Reset()
    {
        tb_prj_name.Text = "";
        ddl_prj_type.SelectedIndex = 0;
        ddl_prj_category.SelectedIndex = 0;
        ddl_prj_brand.SelectedIndex = 0;
        tb_prj_budget.Text = "";
        tb_prj_start.Text = "";
        tb_prj_end.Text = "";
        cb_isactive.Checked = false;

        btn_add.Visible = true;
        btn_update.Visible = false;
        btn_cancel.Visible = false;
    }
}