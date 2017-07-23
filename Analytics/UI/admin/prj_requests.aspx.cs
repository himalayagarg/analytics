using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class UI_admin_prj_requests : System.Web.UI.Page
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
        Encryption64 e64 = new Encryption64();
        if (!IsPostBack)
        {
            ((MenuItem)((Menu)Master.FindControl("Menu1")).FindItem("Projects")).Selected = true;
            ViewState["SortOrder"] = " ASC";

            long prjid = Convert.ToInt64(e64.Decrypt(Request.QueryString.Get("prj").Replace(" ", "+")));

            //Header Bound
            ds_analytics.projectsRow prj_row = projects.getProjectByPrjid(prjid)[0];
            lbl_name.Text = prj_row.projectname;
            lbl_type.Text = prj_row.projecttype;
            lbl_category.Text = prj_row.projectcategory;
            lbl_brand.Text = prj_row.projectbrand;            
            if (!prj_row.IscreatedateNull()) lbl_start.Text = prj_row.createdate.ToString("dd/MM/yyyy");
            if (!prj_row.IscompletiondateNull()) lbl_end.Text = prj_row.completiondate.ToString("dd/MM/yyyy");
            if (!prj_row.IsbudgetNull()) lbl_budget.Text = Convert.ToString(prj_row.budget);
            cb_active.Checked = prj_row.isactive;

            //GridView
            ds_analytics.requestsDataTable req_dt = requests.getAllRequestsByProjID(prjid);
            gv_requests.DataSource = req_dt;
            gv_requests.DataBind();
            ViewState["dtStored"] = req_dt;
        }
        else
        {
 
        }
    }
    
    protected void gv_requests_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.CssClass = "row";

            //From
            e.Row.Cells[5].Text = m_users.getFullnameByuserid(e.Row.Cells[5].Text);
            //To
            e.Row.Cells[6].Text = m_users.getFullnameByuserid(e.Row.Cells[6].Text);
            //Status
            e.Row.Cells[8].Text = other.getStatustext(Convert.ToInt32(e.Row.Cells[8].Text));
            //View Request
            string req_id = Convert.ToString(gv_requests.DataKeys[e.Row.RowIndex].Value);
            string userid = Convert.ToString(Session["userid"]);

            Encryption64 e64 = new Encryption64();
            string url = "view_request.aspx?re=" + e64.Encrypt(req_id.ToString()) + "&us=" + e64.Encrypt(userid);

            ImageButton ib_view = ((ImageButton)e.Row.FindControl("imgbtn_View"));            
            ib_view.Attributes.Add("onclick", "javascript:window.open('"+url+"');");
        }
    }
    
    protected void gv_requests_Sorting(object sender, GridViewSortEventArgs e)
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
        gv_requests.DataSource = dt;
        gv_requests.DataBind();
        ViewState["dtStored"] = dt;
    }        
   
    protected void imgbtn_responsible_Click(object sender, ImageClickEventArgs e)
    {
        //request
        GridViewRow gv_row = (GridViewRow)((sender as ImageButton).NamingContainer);
        string req_id = Convert.ToString(gv_requests.DataKeys[gv_row.RowIndex].Value);
        ds_analytics.requestsRow req_row = requests.getRequestbyReqid(req_id)[0];
        lbl_reqid.Text = req_row.reqid;
        lbl_reqfrom.Text = m_users.getFullnameByuserid(req_row.reqfrom);
        lbl_reqto.Text = m_users.getFullnameByuserid(req_row.responsible);
        ViewState["req_row"] = req_row;

        //lead
        ddl_chng_reqto.DataSource = m_users.getAllActiceReceivers();
        ddl_chng_reqto.DataTextField = "fullname";
        ddl_chng_reqto.DataValueField = "userid";
        ddl_chng_reqto.DataBind();
        ddl_chng_reqto.SelectedValue = req_row.responsible;

        pnl_MPExt.Show();
    }
    
    protected void btnSave_Click(object sender, EventArgs e)
    {
        ds_analytics.requestsRow req_row = ((ds_analytics.requestsRow)ViewState["req_row"]);
        req_row.responsible = ddl_chng_reqto.SelectedValue;
        requests.update_Request(req_row);

        Encryption64 e64 = new Encryption64();
        Response.Redirect("~/UI/admin/prj_requests.aspx?prj=" + e64.Encrypt(req_row.projectid.ToString()));
    }
    
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        pnl_MPExt.Hide();
    }
}