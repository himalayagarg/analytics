using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Security.Cryptography;
using System.IO;

public partial class UI_users_reqaction_byme : System.Web.UI.Page
{
    string reqid;
    ds_analytics.requestsRow req_row;

    protected override PageStatePersister PageStatePersister
    {
        get
        {
            //return base.PageStatePersister; for solve connection reset by server problem.
            return new SessionPageStatePersister(this);
        }
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        Encryption64 e64 = new Encryption64();
        reqid = e64.Decrypt(Request.QueryString.Get("reqid").Replace(" ", "+"));
        ds_analytics.requestsDataTable req_dt = requests.getRequestbyReqid(reqid);
        if (req_dt.Count > 0)
        {
            req_row = req_dt[0];
        }
        else
        {
            Response.Write("<script>alert('Error in request.');</script>");
        }        
    } 

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            set_panels();
            bind_req_header();
            bind_req_samples();
            bind_req_tests();
            bind_test_samples();
        }
        else
        {
            remove_extra_tabs();
        }
    }

    private void set_panels()
    {        
        pnl_sendingtolab.Visible = false;       //3        
        pnl_declined.Visible = false;           //4
        pnl_inlab.Visible = false;              //5
        pnl_vieweditresult.Visible = false;     //6

        switch (req_row.statusid)
        {
            case 2:
                CollapsiblePanelExtender1.Collapsed = false;
                break;
            case 3:
                pnl_sendingtolab.Visible = true;
                tb_comment_sendingLab.Text = req_row.receive_cmnt;
                CollapsiblePanelExtender1.Collapsed = false;
                break;
            case 4:
                pnl_declined.Visible = true;
                tb_comment4.Text = req_row.receive_cmnt;
                CollapsiblePanelExtender1.Collapsed = false;
                break;
            case 5:
                pnl_inlab.Visible = true;
                //tb_comment_fromLab.Text = req_row.lab_cmnt;
                CollapsiblePanelExtender1.Collapsed = false;
                break;
            case 6:
                pnl_vieweditresult.Visible = true;
                lbl_resultdate.Text = req_row.resultdate.ToString("dd/MM/yyyy");
                tb_remark_fromResult.Text = req_row.result_remark;
                tb_comment_fromResult.Text = req_row.result_cmnt;

                DataTable dt_result = labresult.getLabResult_ByJoining(req_row.reqid);
                
                DataRow[] d_row = dt_result.Select("isfor_report='True'");
                DataTable dt = dt_result.Clone();
                dt_result.Dispose();
                foreach (DataRow dr in d_row)
                {
                    dt.ImportRow(dr);
                }
                gv_view_result.DataSource = dt;
                gv_view_result.DataBind();
                break;
            default:
                break;
        }
    }

    private void remove_extra_tabs()
    {
        ds_analytics.req_samplesDataTable req_samp_dt = req_samples.getSamplesByReqid(reqid);
        int no_samples = req_samp_dt.Rows.Count;
        int no_samples_intab = int.Parse(System.Configuration.ConfigurationManager.AppSettings["no_samples_intab"].ToString());

        //removing extra tabpanels
        int no_tabs = Convert.ToInt32(Math.Ceiling(no_samples / Convert.ToDouble(no_samples_intab)));
        while (TabContainer1.Tabs.Count > no_tabs)
        {
            TabContainer1.Tabs.RemoveAt(TabContainer1.Tabs.Count - 1);
        }
    }

    private void bind_req_header()
    {
        Encryption64 e64 = new Encryption64();
        hl_print_request.NavigateUrl = "~/UI/users/print_request.aspx?reqid=" + e64.Encrypt(reqid);
        hl_pdf_request.NavigateUrl = "~/UI/users/print_request.aspx?reqid=" + e64.Encrypt(reqid)+"&action=pdf";
        //setting labels and InfoBox
        lbl_requestor.Text = "(Request by- " + m_users.getFullnameByuserid(req_row.reqfrom) + ")";
        ds_analytics.projectsRow prj_row = projects.getProjectByPrjid(req_row.projectid)[0];
        lbl_project.Text = prj_row.projectname;
        lbl_prj_type.Text = prj_row.projecttype;
        lbl_prj_category.Text = prj_row.projectcategory;
        lbl_prj_brand.Text = prj_row.projectbrand;
        lbl_typeanalysis.Text = req_row.analysistype;
        lbl_requestid.Text = req_row.reqid;        
        lbl_lead.Text = m_users.getFullnameByuserid(req_row.responsible);
        lbl_typerequest.Text = req_row.reqtype;

        lbl_status.Text = other.getStatustext(req_row.statusid);
        tb_addinfo.Text = req_row.req_cmnt;
    }

    private void bind_req_samples()
    {
        ds_analytics.req_samplesDataTable req_samp_dt = req_samples.getSamplesByReqid(reqid);
        int no_samples = req_samp_dt.Rows.Count;
        int no_samples_intab = int.Parse(System.Configuration.ConfigurationManager.AppSettings["no_samples_intab"].ToString());

        //removing extra tabpanels
        int no_tabs = Convert.ToInt32(Math.Ceiling(no_samples / Convert.ToDouble(no_samples_intab)));
        while (TabContainer1.Tabs.Count > no_tabs)
        {
            TabContainer1.Tabs.RemoveAt(TabContainer1.Tabs.Count - 1);
        }

        //removing extra samples from last tab
        int sample_inlastgrid = no_samples % no_samples_intab;
        int columns_tokeep = sample_inlastgrid + 2;                         //2 columns for Property ID, Name
        string gv_last = "GridView" + no_tabs.ToString();

        //loop through tabcontainer
        int tab_no = 1;
        AjaxControlToolkit.TabContainer container = (AjaxControlToolkit.TabContainer)TabContainer1;
        foreach (object obj in container.Controls)
        {
            if (obj is AjaxControlToolkit.TabPanel)
            {
                AjaxControlToolkit.TabPanel tabPanel = (AjaxControlToolkit.TabPanel)obj;
                {
                    GridView gv = ((GridView)(tabPanel.FindControl("GridView" + tab_no.ToString())));
                    if ((gv.ID == gv_last) && (sample_inlastgrid != 0))
                    {
                        while (gv.Columns.Count > columns_tokeep)
                        {
                            //removing extra samples from last tab
                            gv.Columns.RemoveAt(gv.Columns.Count - 1);
                        }
                    }
                    gv.DataSource = get_dt_for_gv(tab_no, no_samples_intab, gv.Columns.Count - 2, req_samp_dt); //2 columns for Property ID, Name
                    gv.DataBind();
                }
                tab_no++;
            }
        }
    }

    private DataTable get_dt_for_gv(int tab_num, int num_samples_intab, int num_samples_ingrid, DataTable dt_samples)
    {
        //Adding columns
        DataTable dt = new DataTable();
        dt.Columns.Add(new DataColumn("propertyid", typeof(System.Int32)));
        dt.Columns.Add(new DataColumn("propertyname", typeof(System.String)));
        for (int i = 1; i <= num_samples_ingrid; i++)
        {
            int sample_no = (tab_num - 1) * num_samples_intab + i;
            dt.Columns.Add(new DataColumn((sample_no).ToString(), typeof(System.String)));
        }

        ////Adding Rows
        //1 setting number of rows using property count
        DataTable dt_prop = other.getProperties();
        foreach (DataRow dr_prop in dt_prop.Rows)
        {
            DataRow dr_new = dt.NewRow();
            dr_new["propertyid"] = dr_prop["propertyid"];
            dr_new["propertyname"] = dr_prop["propertyname"];
            dt.Rows.Add(dr_new);
        }

        for (int i = 1; i <= num_samples_ingrid; i++)
        {
            int sample_no = (tab_num - 1) * num_samples_intab + i;
            string sampleid = dt_samples.Rows[sample_no - 1]["sampleid"].ToString();
            DataTable dt_pvalue = sample_pvalue.getPvaluesBySampleid(sampleid);
            foreach (DataRow dr in dt_pvalue.Rows)
            {
                int propertyid = Convert.ToInt32(dr["propertyid"]);
                dt.Rows[propertyid - 1][sample_no.ToString()] = dr["pvalue"].ToString();
            }
        }

        return dt;
    }

    private void bind_req_tests()
    {
        ds_analytics.req_testsDataTable req_test_dt = req_tests.getTestsbyReqid(reqid);
        gv_tests.DataSource = req_test_dt;
        gv_tests.DataBind();
    }

    private void bind_test_samples()
    {
        //Getting req_tests table from the database
        ds_analytics.req_testsDataTable dt_test = req_tests.getTestsbyReqid(reqid);

        //Make test_sample DataTable to bind to Gridview
        DataTable dt_test_sample = new DataTable();
        dt_test_sample.Columns.Add(new DataColumn("Test", typeof(System.String)));

        for (int i = 1; i <= req_samples.countSamplesByReqId(reqid); i++)
        {
            dt_test_sample.Columns.Add(new DataColumn(i.ToString(), typeof(System.Boolean)));
        }
        //building Datatable for gridview using test_samples in server
        foreach (ds_analytics.req_testsRow dr in dt_test.Rows)
        {
            DataRow dr_new = dt_test_sample.NewRow();
            dr_new["Test"] = dr.testname;

            ds_analytics.test_samplesDataTable ts_dt = test_samples.getTest_SamplesByTestid(dr.test_id);
            int i = 1;
            foreach (ds_analytics.test_samplesRow ts_row in ts_dt.Rows)
            {
                dr_new[i.ToString()] = ts_row.isselected;
                i++;
            }
            dt_test_sample.Rows.Add(dr_new);
        }

        //Databind
        gv_test_sample.DataSource = dt_test_sample;
        gv_test_sample.DataBind();
    }

    protected void GridView_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
            e.Row.CssClass = "row";
    }

    protected void gv_tests_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
            e.Row.CssClass = "row";
    }
    
    protected void gv_view_result_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.CssClass = "row";
            //SaveAs Dialog box appears only if hyperlink is bind using code behind
            HyperLink hl_attach = ((HyperLink)e.Row.FindControl("hlink_attach"));
            if ((hl_attach.Text != null) && (hl_attach.Text != string.Empty))
            {
                hl_attach.NavigateUrl = "~/upload/labresult_coa/"+hl_attach.Text;
            }
        }
    }

    /// <summary>
    /// Requestor able to Cancel(Delete) Request if the request is declined
    /// </summary>
    protected void btn_cancel_request_Click(object sender, EventArgs e)
    {
        bool done = requests.delete_Request(reqid);
        if (done)
        {
            string url = "home.aspx";
            ClientScript.RegisterStartupScript(this.GetType(), "callfunction", "alert('Request has been cancelled.');window.location.href = '" + url + "';", true);
        }
        else
        {
            string url = "home.aspx";
            ClientScript.RegisterStartupScript(this.GetType(), "callfunction", "alert('Problem in cancelling the Request.');window.location.href = '" + url + "';", true);
        }
    }

    /// <summary>
    /// Requestor able to Edit Request if the request is declined
    /// </summary>
    protected void btn_edit_request_Click(object sender, EventArgs e)
    {
        Encryption64 e64 = new Encryption64();
        string nosample = e64.Encrypt(req_samples.countSamplesByReqId(reqid).ToString());
        string notest = e64.Encrypt(req_tests.countTestsByReqId(reqid).ToString());
        string rtype = e64.Encrypt(req_row.reqtype);
        Response.Redirect("~/UI/users/process_request1.aspx?mode=edit&user=requestor&process_reqid="+e64.Encrypt(reqid)+"&nosample="+nosample+"&notest="+notest+"&rtype="+rtype);
    }
}