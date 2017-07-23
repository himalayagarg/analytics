using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;

using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;

public partial class UI_users_home : System.Web.UI.Page
{
    string userid = string.Empty;
    DataTable dt_reqtome;
    DataTable dt_reqbyme;


    //public override void VerifyRenderingInServerForm(Control control)
    //{
    //    return;
    //}

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
            if (Session["userid"] == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            ViewState["SortOrder"] = " ASC";
            userid = Session["userid"].ToString();

            //Binding 1 by Default Filter-All
            dt_reqtome = requests.getAllRequestsbyResponsible(userid);            
            ViewState["dt_req_to_me"] = dt_reqtome;
            gv_bind(gv_req_to_me, dt_reqtome);

            //Binding 2 by Default Filter-All
            dt_reqbyme = requests.getAllRequestsbyReqfrom(userid);            
            ViewState["dt_req_by_me"] = dt_reqbyme;
            gv_bind(gv_req_by_me, dt_reqbyme);

            //Binding ReqCount DataTable
            DataTable dt = other.getAllStatus();
            dt.Columns.Add(new DataColumn("count_tome", typeof(Int32)));
            dt.Columns.Add(new DataColumn("count_byme", typeof(Int32)));
            foreach (DataRow dr in dt.Rows)
            {
                dr["count_tome"] = requests.countReqByStatusResponsible(userid, Convert.ToInt32(dr["statusid"]));
                dr["count_byme"] = requests.countReqByStatusReqfrom(userid, Convert.ToInt32(dr["statusid"]));
            }
            gv_count.DataSource = dt;
            gv_count.DataBind();

            //SetPage According to User Access Rights
            setPageByRights(userid);            
        }
        else
        {
            userid = Session["userid"].ToString();
        }  
    }

    private void setPageByRights(string userid)
    {
        ArrayList arr_List_subMod = m_users.getSubModulesByUserid(userid);
        if (!arr_List_subMod.Contains("Receiver"))
        {
            AjaxControlToolkit.TabContainer container = (AjaxControlToolkit.TabContainer)TabContainer1;
            container.Controls[0].Visible = false;
            gv_count.Columns[2].Visible = false;
        }
        if (!arr_List_subMod.Contains("Requestor"))
        {
            AjaxControlToolkit.TabContainer container = (AjaxControlToolkit.TabContainer)TabContainer1;
            container.Controls[1].Visible = false;
            gv_count.Columns[3].Visible = false;
        }
    }
    
    protected void gv_bind(GridView gv, DataTable dt)
    {
        gv.DataSource = dt;
        gv.DataBind();
    }
    
    protected void gv_req_to_me_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        Encryption64 e64 = new Encryption64();        
        if (e.Row.RowType == DataControlRowType.DataRow)
        {            
            //set url
            DataKey key = gv_req_to_me.DataKeys[e.Row.RowIndex];
            string reqid = key.Value.ToString();
            string url = string.Empty;
            string statusid = e.Row.Cells[5].Text;            
            if (statusid == "1")
            {
                //Requesting
                e.Row.Attributes.Add("ondblclick", "alert('Request is not submitted.')");
            }
            else if (statusid == "2")
            {
                //Requested
                url = "reqaction.aspx?reqid=" + e64.Encrypt(reqid);
            }
            else if (statusid == "3")
            {
                //Approved
                url = "reqaction.aspx?reqid=" + e64.Encrypt(reqid);
            }
            else if (statusid == "4")
            {
                //Declined
                url = "reqaction.aspx?reqid=" + e64.Encrypt(reqid);
            }
            else if (statusid == "5")
            {
                //Lab
                url = "reqaction.aspx?reqid=" + e64.Encrypt(reqid);
            }
            else if (statusid == "6")
            {
                //Result
                url = "reqaction.aspx?reqid=" + e64.Encrypt(reqid);
            }
            else if (statusid == "7")
            {
                //Cancelled                
                e.Row.Attributes.Add("ondblclick", "alert('Request has been cancelled.')");
            }

            //onclick
            if (url != string.Empty)
            {
                e.Row.Attributes.Add("ondblclick", "transferLink('" + url + "')");
            }

            //css
            e.Row.CssClass = "row";
            //fullname
            e.Row.Cells[3].Text = m_users.getFullnameByuserid(e.Row.Cells[3].Text);
            //date
            e.Row.Cells[4].Text = Convert.ToDateTime(e.Row.Cells[4].Text).ToString("dd/MMM/yy hh:mm tt");
            //status must be set in the end
            e.Row.Cells[5].Text = other.getStatustext(Convert.ToInt32(e.Row.Cells[5].Text));
        }
    }

    protected void gv_req_by_me_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            AjaxControlToolkit.HoverMenuExtender ajxhovermenu = (AjaxControlToolkit.HoverMenuExtender)e.Row.FindControl("ahm_1");
            e.Row.ID = e.Row.RowIndex.ToString();
            ajxhovermenu.TargetControlID = e.Row.ID;
        }
    }
    
    protected void gv_req_by_me_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        Encryption64 e64 = new Encryption64();
        if (e.Row.RowType == DataControlRowType.DataRow)
        {            
            //set transfer url on double click
            DataKey key = gv_req_by_me.DataKeys[e.Row.RowIndex];
            string reqid = key.Value.ToString();
            string url = string.Empty;
            string statusid = e.Row.Cells[5].Text;

            //set copy request url
            string nosample = e64.Encrypt(req_samples.countSamplesByReqId(reqid).ToString());
            string notest = e64.Encrypt(req_tests.countTestsByReqId(reqid).ToString());
            string rtype = e64.Encrypt(requests.getRequestbyReqid(reqid)[0].reqtype);
            string copy_url = "~/UI/users/process_request1.aspx?mode=copy&user=requestor&process_reqid=" + e64.Encrypt(reqid) + "&nosample=" + nosample + "&notest=" + notest + "&rtype=" + rtype;
            HyperLink hl_copy = (HyperLink)e.Row.FindControl("hl_copy");
            hl_copy.NavigateUrl = copy_url;

            if (statusid == "1")
            {
                //Request Incomplete
                int no_samples = req_samples.countSamplesByReqId(reqid);
                int no_tests = req_tests.countTestsByReqId(reqid);
                
                // disable copy request
                hl_copy.Visible = false;

                //for draft requests
                //url="request3.aspx?nosample=" + (e64.Encrypt(no_samples.ToString())) + "&notest=" + e64.Encrypt(no_tests.ToString()) + "&reqid=" + e64.Encrypt(reqid);
            }
            else if (statusid == "2")
            {
                //Requested
                url = "reqaction_byme.aspx?reqid=" + e64.Encrypt(reqid);
            }
            else if (statusid == "3")
            {
                //Approved
                url = "reqaction_byme.aspx?reqid=" + e64.Encrypt(reqid);
            }
            else if (statusid == "4")
            {
                //Declined
                url = "reqaction_byme.aspx?reqid=" + e64.Encrypt(reqid);
            }
            else if (statusid == "5")
            {
                //In Progress
                url = "reqaction_byme.aspx?reqid=" + e64.Encrypt(reqid);
            }
            else if (statusid == "6")
            {
                //Complete
                url = "reqaction_byme.aspx?reqid=" + e64.Encrypt(reqid);
            }
            else if (statusid == "7")
            {
                //Draft
                e.Row.Attributes.Add("ondblclick", "alert('Request has been cancelled.')");
                // disable copy request
                hl_copy.Visible = false;
            }

            //onclick
            if (url != string.Empty)
            {
                e.Row.Attributes.Add("ondblclick", "transferLink('" + url + "')");
            }

            //css
            e.Row.CssClass = "row";
            //fullname
            e.Row.Cells[3].Text = m_users.getFullnameByuserid(e.Row.Cells[3].Text);
            //date
            e.Row.Cells[4].Text = Convert.ToDateTime(e.Row.Cells[4].Text).ToString("dd/MMM/yy hh:mm tt");
            //status must be set in the end
            e.Row.Cells[5].Text = other.getStatustext(Convert.ToInt32(e.Row.Cells[5].Text));
        }        
    }

    protected void gv_count_DataBound(object sender, EventArgs e)
    {
        int sum_tome = 0;
        int sum_byme = 0;
        foreach (GridViewRow gvr in gv_count.Rows)
        {
            if (gvr.RowType == DataControlRowType.DataRow)
            {
                gvr.CssClass = "row";

                sum_tome += Convert.ToInt32(gvr.Cells[2].Text);
                sum_byme += Convert.ToInt32(gvr.Cells[3].Text);
            }
        }
        gv_count.FooterRow.Cells[1].Text = "Total: ";
        gv_count.FooterRow.Cells[2].Text = sum_tome.ToString();
        gv_count.FooterRow.Cells[3].Text = sum_byme.ToString();        
    }

    protected void gv_req_to_me_Sorting(object sender, GridViewSortEventArgs e)
    {
        DataTable dt = (DataTable)ViewState["dt_req_to_me"];
        DataView dv = new DataView(dt);
        if (ViewState["SortOrder"] == " ASC")
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
        gv_bind(gv_req_to_me, dt);
    }
    protected void gv_req_by_me_Sorting(object sender, GridViewSortEventArgs e)
    {        
        DataTable dt = (DataTable)ViewState["dt_req_by_me"];
        DataView dv = new DataView(dt);
        if (ViewState["SortOrder"] == " ASC")
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
        gv_bind(gv_req_by_me, dt);
    }
    
    protected void ddl_status_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Binding 1 by Default Filter-All
        dt_reqtome = requests.getAllRequestsbyResponsible(userid);        

        //Binding 2 by Default Filter-All
        dt_reqbyme = requests.getAllRequestsbyReqfrom(userid);        

        string selected_status = ddl_status.SelectedValue;
        if (selected_status == "*")
        {
            Response.Redirect("~/UI/users/home.aspx");
        }
        else
        {
            //1 To Me
            DataTable dt1 = dt_reqtome.Clone();
            DataRow[] dr_arr1 = dt_reqtome.Select("statusid='" + selected_status + "'");
            foreach (DataRow dr in dr_arr1)
            {
                dt1.ImportRow(dr);
            }
            gv_bind(gv_req_to_me, dt1);
            ViewState["dt_req_to_me"] = dt1;

            //2 By Me
            DataTable dt2 = dt_reqbyme.Clone();
            DataRow[] dr_arr2 = dt_reqbyme.Select("statusid='" + selected_status + "'");
            foreach (DataRow dr in dr_arr2)
            {
                dt2.ImportRow(dr);
            }
            gv_bind(gv_req_by_me, dt2);
            ViewState["dt_req_by_me"] = dt2;
        }        
    }
    
    //protected void btn_pdf_Click(object sender, EventArgs e)
    //{
    //    Response.Clear();
    //    Response.Charset = "";
    //    Response.ContentType = "application/pdf";
    //    Response.AddHeader("content-disposition", "attachment;filename=PagetoPDF.pdf");        

    //    StringWriter sw = new StringWriter();
    //    HtmlTextWriter hw = new HtmlTextWriter(sw);
    //    Panel1.RenderControl(hw);
    //    StringReader sr = new StringReader(sw.ToString());

    //    Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 40f, 0f);
    //    HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
    //    PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
    //    pdfDoc.Open();
    //    htmlparser.Parse(sr);
    //    pdfDoc.Close();
    //    Response.Write(pdfDoc);
    //    Response.End();    
    //}    
}