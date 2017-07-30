using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class UI_admin_mis_reqlife : System.Web.UI.Page
{
    public override void VerifyRenderingInServerForm(Control control)
    {
        //###this removes the no forms error by overriding the error
    }
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
            ((MenuItem)((Menu)Master.FindControl("Menu1")).FindItem("MIS")).Selected = true;
            ViewState["SortOrder"] = " ASC";

            ds_analytics.requestsDataTable req_dt = requests.getAllRequests();
            gv_reqs.DataSource = req_dt;
            gv_reqs.DataBind();
            ViewState["dtStored"] = req_dt;
        }
    }
    protected void gv_reqs_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.CssClass = "row";
            string userid = Convert.ToString(Session["userid"]);

            Encryption64 e64 = new Encryption64();

            HyperLink hl_reqno = ((HyperLink)e.Row.Cells[1].FindControl("hl_reqno"));
            hl_reqno.NavigateUrl = "~/UI/admin/view_request.aspx?re=" + e64.Encrypt(hl_reqno.Text) + "&us=" + e64.Encrypt(userid);

            ImageButton imgBtnPDF = ((ImageButton)e.Row.Cells[0].FindControl("imgBtn_PDF"));
            imgBtnPDF.OnClientClick = "javascript:OpenPDFpopup('"+ e64.Encrypt(hl_reqno.Text) + "')";           

            e.Row.Cells[2].Text = projects.getProjectNameByProjectID(Convert.ToInt64(e.Row.Cells[2].Text));
            e.Row.Cells[4].Text = m_users.getFullnameByuserid(e.Row.Cells[4].Text);
            e.Row.Cells[5].Text = m_users.getFullnameByuserid(e.Row.Cells[5].Text);
            e.Row.Cells[6].Text = other.getStatustext(Convert.ToInt32(e.Row.Cells[6].Text));            
        }
    }
    protected void gv_reqs_Sorting(object sender, GridViewSortEventArgs e)
    {
        GridView gv = (GridView)sender;
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
        gv.DataSource = dt;
        gv.DataBind();
        ViewState["dtStored"] = dt;
    }
    protected void gv_reqs_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView gv = (GridView)sender;
        DataTable dt = (DataTable)ViewState["dtStored"];
        gv.DataSource = dt;
        gv.PageIndex = e.NewPageIndex;
        gv.DataBind();
    }
    protected void ddl_searchby_SelectedIndexChanged(object sender, EventArgs e)
    {
        TextBox1.Text = "";        
        TextBox1.Visible = false;        
        DropDownList1.Visible = false;
        btn_search.Visible = false;        
        rfv1.Enabled = false;
        DropDownList1.Items.Clear();
        switch(ddl_searchby.SelectedValue)
        {            
            case "reqid":
                TextBox1.Visible=true;
                rfv1.Enabled = true;
                btn_search.Visible = true;                
                break;
            case "projectid":
                DropDownList1.Visible = true;
                btn_search.Visible = true;
                DropDownList1.DataSource = projects.getAllProjects();
                DropDownList1.DataTextField = "projectname";
                DropDownList1.DataValueField = "projectid";
                DropDownList1.DataBind();
                break;
            case "reqtype":
                DropDownList1.Visible = true;
                btn_search.Visible = true;
                DataTable dt_typerequests1 = other.getDropdownsbyType("typerequest");                                    
                DataTable dt_typerequests2 = other.getDropdownsbyType("typerequest-at");
                foreach (DataRow dr in dt_typerequests2.Rows)
                {
                    dt_typerequests1.ImportRow(dr);
                }
                DropDownList1.DataSource = dt_typerequests1;
                DropDownList1.DataTextField = "value";
                DropDownList1.DataValueField = "value";
                DropDownList1.DataBind();
                break;
            case "reqfrom":
                TextBox1.Visible = true;
                rfv1.Enabled = true;
                btn_search.Visible = true;
                break;
            case "responsible":
                TextBox1.Visible = true;
                rfv1.Enabled = true;
                btn_search.Visible = true;
                break;
            case "statusid":
                DropDownList1.Visible = true;
                btn_search.Visible = true;
                DropDownList1.DataSource = other.getAllStatus();
                DropDownList1.DataTextField = "statustext";
                DropDownList1.DataValueField = "statusid";
                DropDownList1.DataBind();
                break;
            case "reqdate":
                DropDownList1.Visible = true;
                btn_search.Visible = true;
                ListItem li1 = new ListItem("This Week");
                ListItem li2 = new ListItem("Last Week");
                ListItem li3 = new ListItem("This Month");
                ListItem li4 = new ListItem("Last Month");
                ListItem li5 = new ListItem("This Year");
                ListItem li6 = new ListItem("Last Year");
                DropDownList1.Items.Add(li1);
                DropDownList1.Items.Add(li2);
                DropDownList1.Items.Add(li3);
                DropDownList1.Items.Add(li4);
                DropDownList1.Items.Add(li5);
                DropDownList1.Items.Add(li6);
                break;
            default:
                ds_analytics.requestsDataTable req_dt = requests.getAllRequests();
                gv_reqs.DataSource = req_dt;
                gv_reqs.DataBind();
                ViewState["dtStored"] = req_dt;
                break;
        }        
    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        ds_analytics.requestsDataTable req_dt = null ;

        switch (ddl_searchby.SelectedValue)
        {
            case "reqid":
                req_dt = requests.getRequestbyReqid(TextBox1.Text);
                break;
            case "projectid":
                req_dt = requests.getAllRequestsByProjID(Convert.ToInt64(DropDownList1.SelectedValue));
                break;
            case "reqtype":
                req_dt = requests.getAllRequestsByReqType(DropDownList1.SelectedItem.Text);
                break;
            case "reqfrom":
                req_dt = requests.getAllRequestsByReqFromName(TextBox1.Text);
                break;
            case "responsible":
                req_dt = requests.getAllRequestsByResponsibleName(TextBox1.Text);
                break;
            case "statusid":
                req_dt = requests.getAllRequestsByStatusID(Convert.ToInt32(DropDownList1.SelectedValue));
                break;
            case "reqdate":
                DateTime dt_now = DateTime.Now;
                DateTime dt1 = new DateTime();
                DateTime dt2 = new DateTime();
                if (DropDownList1.SelectedItem.Text=="This Week")
                {
                    int diff = DayOfWeek.Monday - dt_now.DayOfWeek;
                    dt1 = dt_now.AddDays(diff);
                    dt2 = dt_now.AddDays(diff + 6);
                }
                else if (DropDownList1.SelectedItem.Text == "Last Week")
                {
                    int diff = DayOfWeek.Monday - dt_now.DayOfWeek;
                    diff -= 7;
                    dt1 = dt_now.AddDays(diff);
                    dt2 = dt_now.AddDays(diff + 6);
                }
                else if (DropDownList1.SelectedItem.Text == "This Month")
                {                    
                    dt1 = dt_now.AddDays(-(dt_now.Day-1));
                    dt2 = dt1.AddMonths(1);
                    dt2 = dt2.AddDays(-1);
                }
                else if (DropDownList1.SelectedItem.Text == "Last Month")
                {
                    dt_now = dt_now.AddMonths(-1);
                    dt1 = dt_now.AddDays(-(dt_now.Day - 1));
                    dt2 = dt1.AddMonths(1);
                    dt2 = dt2.AddDays(-1);
                }
                else if (DropDownList1.SelectedItem.Text == "This Year")
                {
                    dt1 = dt_now.AddDays(-(dt_now.DayOfYear - 1));
                    dt2 = dt1.AddYears(1);
                    dt2 = dt2.AddDays(-1);
                }
                else if (DropDownList1.SelectedItem.Text == "Last Year")
                {
                    dt_now = dt_now.AddYears(-1);
                    dt1 = dt_now.AddDays(-(dt_now.DayOfYear - 1));
                    dt2 = dt1.AddYears(1);
                    dt2 = dt2.AddDays(-1);
                }                
                req_dt = requests.getAllRequestsByReqDateFromTo(dt1, dt2);
                break;
            default:
                break;
        }

        
        gv_reqs.DataSource = req_dt;
        gv_reqs.DataBind();
        ViewState["dtStored"] = req_dt;
    }

    protected void imgbtn_excel_Click(object sender, ImageClickEventArgs e)
    {
        if (gv_reqs.Rows.Count > 0)
        {
            Response.Clear();
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            Response.AppendHeader("content-disposition", "attachment; filename=MIS_ReqLife.xls");
            System.IO.StringWriter sw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(sw);
            gv_reqs.AllowPaging = false;
            gv_reqs.DataSource = (DataTable)ViewState["dtStored"];
            gv_reqs.DataBind();         //rebind after AllowPaging = false
            GridView gv = gv_reqs;
            int countheadercells = gv.Rows[0].Cells.Count;
            for (int i = 1; i <= countheadercells; i++)
            {
                gv.HeaderRow.Cells[i - 1].BackColor = System.Drawing.Color.FromArgb(204, 204, 204);
            }
            this.RemoveControls(gv);    //remove header hyperlinks, can remove extra controls also in the same way
            gv.RenderControl(hw);
            Response.Write(sw.ToString());
            Response.End();
            gv_reqs.AllowPaging = true;
        }
    }
    private void RemoveControls(Control grid)
    {
        Literal literal = new Literal();
        for (int i = 0; i < grid.Controls.Count; i++)
        {
            if (grid.Controls[i] is LinkButton)
            {
                literal.Text = (grid.Controls[i] as LinkButton).Text;
                grid.Controls.Remove(grid.Controls[i]);
                grid.Controls.AddAt(i, literal);
            }                        
            else if (grid.Controls[i] is HyperLink)
            {
                literal.Text = (grid.Controls[i] as HyperLink).Text;
                grid.Controls.Remove(grid.Controls[i]);
                grid.Controls.AddAt(i, literal);
            }            
            if (grid.Controls[i].HasControls())
            {
                RemoveControls(grid.Controls[i]);
            }
        }
    }
}