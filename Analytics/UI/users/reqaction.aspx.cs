using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Security.Cryptography;
using System.IO;
using System.Net;
using System.Web;

public partial class UI_users_reqaction : System.Web.UI.Page
{
    //Major Steps in this page..
    //checking postback
    //showing panels based on the statusid of the request
    //binding data to gridviews

    string reqid;    
    ds_analytics.requestsRow req_row;
    DataTable dt_test_sample_global;
    DataTable dt_labs_global;

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
            Response.Write("<script>alert('No request found .');</script>");
        }

        //create dynamic gv_test_sample in Page_Init while sending to lab
        if (req_row.statusid == 3)
        {
            build_gv_ts_lab_selection();

            if (!IsPostBack)
            {                            
                dt_test_sample_global = get_dt_ts_lab_selection();
                bind_gv_ts_lab_selection(dt_test_sample_global);                                              
            }
            else
            {
 
            }
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
        pnl_requested.Visible = false;          //2
        pnl_sendingtolab.Visible = false;       //3        
        pnl_declined.Visible = false;           //4
        pnl_inlab.Visible = false;              //5
        pnl_vieweditresult.Visible = false;     //6
        
        switch(req_row.statusid)
        {
            case 2:
                pnl_requested.Visible = true;
                CollapsiblePanelExtender1.Collapsed = false;
                break;
            case 3:
                pnl_sendingtolab.Visible = true;
                //tb_comment_sendingLab.Text = req_row.receive_cmnt;

                DataTable dt_labs_global = AddColumns_DataTableLabs();
                ViewState["dt_labs"] = dt_labs_global;
                gv_labs.DataSource = dt_labs_global;
                gv_labs.DataBind();                

                break;
            case 4:
                pnl_declined.Visible=true;
                tb_comment4.Text = req_row.receive_cmnt;
                CollapsiblePanelExtender1.Collapsed = false;
                break;                
            case 5:
                pnl_inlab.Visible = true;
                //tb_comment_fromLab.Text = req_row.lab_cmnt;
                Encryption64 e64 = new Encryption64();
                hl_print_lab.NavigateUrl = "~/UI/users/print_doc.aspx?reqid="+e64.Encrypt(req_row.reqid);                
                DataTable dt = labresult.getLabResult_ByJoining(req_row.reqid);
                gv_fill_result.DataSource = dt;
                gv_fill_result.DataBind();                
                break;
            case 6:
                pnl_vieweditresult.Visible = true;
                lbl_resultdate.Text = req_row.resultdate.ToString("dd/MM/yyyy");
                tb_comment_fromResult.Text = req_row.result_cmnt;
                tb_remark_fromResult.Text = req_row.result_remark;

                DataTable dt_result = labresult.getLabResult_ByJoining(req_row.reqid);
                gv_view_result.DataSource = dt_result;
                gv_view_result.DataBind();
                break;
            default:
                break;
        }

    }    

    private void build_gv_ts_lab_selection()
    {
        int no_samples = req_samples.countSamplesByReqId(reqid);
        //Adding Columns for Samples (Button Template)
        for (int i = 1; i <= no_samples; i++)
        {
            add_template_gv(i.ToString(), i.ToString(), i.ToString());
        }
    }

    private DataTable get_dt_ts_lab_selection()
    {                
        int no_samples = req_samples.countSamplesByReqId(reqid);

        //Getting req_tests table from the database
        ds_analytics.req_testsDataTable dt_test = req_tests.getTestsbyReqid(reqid);

        //Make test_sample DataTable Columns to bind to Gridview
        DataTable dt_test_sample = new DataTable();
        dt_test_sample.Columns.Add(new DataColumn("test_id", typeof(System.Int64)));
        dt_test_sample.Columns.Add(new DataColumn("testname", typeof(System.String)));

        for (int i = 1; i <= no_samples; i++)
        {
            dt_test_sample.Columns.Add(new DataColumn(i.ToString(), typeof(System.Boolean)));
        }
        //building Datatable for gridview using test_samples in server
        foreach (ds_analytics.req_testsRow dr in dt_test.Rows)
        {
            DataRow dr_new = dt_test_sample.NewRow();
            dr_new["test_id"] = dr.test_id;
            dr_new["testname"] = dr.testname;

            ds_analytics.test_samplesDataTable ts_dt = test_samples.getTest_SamplesByTestid(dr.test_id);
            int i = 1;
            foreach (ds_analytics.test_samplesRow ts_row in ts_dt.Rows)
            {
                dr_new[i.ToString()] = ts_row.isselected;
                i++;
            }
            dt_test_sample.Rows.Add(dr_new);
        }

        return dt_test_sample;                
    }    

    private void bind_gv_ts_lab_selection(DataTable dt_test_sample)
    {
        //Databind
        gv_ts_lab_selection.DataSource = dt_test_sample;
        gv_ts_lab_selection.DataBind();
    }

    private void add_template_gv(string headertext, string id, string bindfield)
    {
        TemplateField tf = new TemplateField();
        tf.HeaderText = headertext;
        tf.ItemStyle.Width = Unit.Pixel(60);
        tf.ItemTemplate = new ButtonTemplate("btn_item" + id, "Lab(0)", bindfield);

        gv_ts_lab_selection.Columns.Add(tf);
    }

    protected void gv_ts_lab_selection_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        GridView gv = (GridView)sender;

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            long test_id = Convert.ToInt64(gv.DataKeys[e.Row.RowIndex].Value);
            for (int i = 2; i <= gv.Columns.Count - 1; i++)
            {
                if ((e.Row.Cells[i].Controls[0].GetType() == typeof(Button)))
                {
                    Button btn = ((Button)e.Row.Cells[i].Controls[0]);
                    if (btn.Visible == true)
                    {
                        btn.Attributes.Add("onmouseover", "mouse_move(" + e.Row.RowIndex + "," + i + ",'over')");
                        btn.Attributes.Add("onmouseout", "mouse_move(" + e.Row.RowIndex + "," + i + ",'out')");
                        //e.Row.Cells[i].Attributes.Add("onmouseout", "style.backgroundColor='#FFFFFF'; style.color='black'");
                        e.Row.Cells[i].ToolTip = "Click to Select Lab to send the Sample " + gv_ts_lab_selection.HeaderRow.Cells[i].Text + " for " + e.Row.Cells[1].Text + " test.";

                        string sampleid = reqid + Convert.ToInt32(gv.Columns[i].HeaderText).ToString("00");
                        ds_analytics.test_samplesDataTable ts_dt = test_samples.getTestSampleBy_TestidandSampleid(test_id, sampleid);
                        btn.CommandName = "AddLab";                        
                        btn.CommandArgument = ts_dt[0]["ts_id"].ToString();
                    }                    
                }
            }
        }
    }    

    protected void gv_ts_lab_selection_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "AddLab")
        {            
            Button btn = (Button)e.CommandSource;            
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;            
            
            //managing footer
            long ts_id = Convert.ToInt64(e.CommandArgument);
            ds_analytics.test_samplesDataTable ts_dt = test_samples.getTestSampleBy_ts_id(ts_id);
            DataTable dt_labs_global = (DataTable)ViewState["dt_labs"];
            if (dt_labs_global.Rows.Count > 0)
            {
                //Controls in FooterTemplate: very useful (used for lab count also)

                //invisible
                gv_labs.FooterRow.Cells[0].Text = ts_dt[0].ts_id.ToString();
                gv_labs.FooterRow.Cells[0].ToolTip = gvr.RowIndex.ToString() + "-" + btn.ID;
                //visible
                gv_labs.FooterRow.Cells[2].Text = req_tests.getTestbyTestid(ts_dt[0].test_id)[0].testname;
                gv_labs.FooterRow.Cells[2].ToolTip = ts_dt[0].test_id.ToString();
                gv_labs.FooterRow.Cells[3].Text = ts_dt[0].sampleid.Substring(8);
                gv_labs.FooterRow.Cells[3].ToolTip = ts_dt[0].sampleid;
            }
            else
            {
                //Controls in EmptyDataTemplate: very useful (used for lab count also)

                //invisible
                ((Label)gv_labs.Controls[0].Controls[0].FindControl("lbl_empty_ts_id")).Text = ts_dt[0].ts_id.ToString();
                ((Label)gv_labs.Controls[0].Controls[0].FindControl("lbl_empty_ts_id")).ToolTip = gvr.RowIndex.ToString() + "-" + btn.ID;
                
                //visible
                ((Label)gv_labs.Controls[0].Controls[0].FindControl("lbl_empty_test")).Text = req_tests.getTestbyTestid(ts_dt[0].test_id)[0].testname;
                ((Label)gv_labs.Controls[0].Controls[0].FindControl("lbl_empty_test")).ToolTip = ts_dt[0].test_id.ToString();
                ((Label)gv_labs.Controls[0].Controls[0].FindControl("lbl_empty_sample")).Text = ts_dt[0].sampleid.Substring(8);
                ((Label)gv_labs.Controls[0].Controls[0].FindControl("lbl_empty_sample")).ToolTip = ts_dt[0].sampleid;
                ((DropDownList)gv_labs.Controls[0].Controls[0].FindControl("dd_empty_lab")).DataSource = m_labs.getAllActiveLabs();
                ((DropDownList)gv_labs.Controls[0].Controls[0].FindControl("dd_empty_lab")).DataBind();
            }
        }
    }

    protected void dd_lab_DataBound(object sender, EventArgs e)
    {   
        //adding address of labs as title
        DropDownList ddl = (DropDownList)sender;
        foreach (ListItem li in ddl.Items)
        {
            ds_analytics.m_labsRow m_labs_row = m_labs.getLabByLabid(Convert.ToInt64(li.Value))[0];
            li.Attributes.Add("title", m_labs_row.address);            
        }
    }

    protected DataTable AddColumns_DataTableLabs()
    {
        DataTable dt_labs = new DataTable();
        dt_labs.Columns.Add(new DataColumn("btn_id", typeof(System.String)));
        dt_labs.Columns.Add(new DataColumn("ts_id", typeof(System.Int64)));        
        dt_labs.Columns.Add(new DataColumn("labid", typeof(System.Int64)));
        dt_labs.Columns.Add(new DataColumn("mth_ref", typeof(System.String)));        

        return dt_labs;
    }

    protected DataTable AddColumns_DataTableQuantity()
    {
        DataTable dt_quantity = new DataTable();
        dt_quantity.Columns.Add(new DataColumn("labid", typeof(System.Int64)));
        dt_quantity.Columns.Add(new DataColumn("labname", typeof(System.String)));
        dt_quantity.Columns.Add(new DataColumn("sampleid", typeof(System.Int64)));        
        dt_quantity.Columns.Add(new DataColumn("sample_quantity", typeof(System.String)));

        return dt_quantity;
    }

    protected void btn_empty_insert_Click(object sender, EventArgs e)
    {
        if (((Label)gv_labs.Controls[0].Controls[0].FindControl("lbl_empty_ts_id")).Text != "")
        {
            //Adding Lab Count
            string btn_id = ((Label)gv_labs.Controls[0].Controls[0].FindControl("lbl_empty_ts_id")).ToolTip;
            int r_index = Convert.ToInt32(btn_id.Split('-')[0]);
            string btnid = btn_id.Split('-')[1];
            Button btn = (Button)gv_ts_lab_selection.Rows[r_index].FindControl(btnid);
            char[] splitor = { '(', ')' };
            int lab_count = Convert.ToInt32(btn.Text.Split(splitor)[1]);
            btn.Text = "Lab(" + (lab_count + 1).ToString() + ")";

            //Adding row in DataTable
            DataTable dt = (DataTable)ViewState["dt_labs"];
            DataRow dr = dt.NewRow();
            dr["btn_id"] = btn_id;
            dr["ts_id"] = Convert.ToInt64(((Label)gv_labs.Controls[0].Controls[0].FindControl("lbl_empty_ts_id")).Text);
            dr["labid"] = Convert.ToInt64(((DropDownList)gv_labs.Controls[0].Controls[0].FindControl("dd_empty_lab")).SelectedValue);
            dr["mth_ref"] = ((TextBox)gv_labs.Controls[0].Controls[0].FindControl("tb_empty_mr")).Text;
            dt.Rows.Add(dr);
            ViewState["dt_labs"] = dt;

            //Bind
            gv_labs.DataSource = dt;
            gv_labs.DataBind();

            //creating datatable to enter quantity per sample per lab
            create_dt_quantity();
        }
        else
        {
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(this.GetType(), "error", "<script>alert('First select the Sample to select the lab.');</script>");
        }
    }

    protected void btnInsert_Footer_Click(object sender, EventArgs e)
    {
        if (gv_labs.FooterRow.Cells[0].ToolTip != "")
        {
            //Adding Lab Count
            string btn_id = gv_labs.FooterRow.Cells[0].ToolTip;
            int r_index = Convert.ToInt32(btn_id.Split('-')[0]);
            string btnid = btn_id.Split('-')[1];
            Button btn = (Button)gv_ts_lab_selection.Rows[r_index].FindControl(btnid);
            char[] splitor = { '(', ')' };
            int lab_count = Convert.ToInt32(btn.Text.Split(splitor)[1]);
            btn.Text = "Lab(" + (lab_count + 1).ToString() + ")";

            //Adding row in DataTable
            DataTable dt = (DataTable)ViewState["dt_labs"];
            DataRow dr = dt.NewRow();
            dr["btn_id"] = btn_id;
            dr["ts_id"] = Convert.ToInt64(gv_labs.FooterRow.Cells[0].Text);
            dr["labid"] = Convert.ToInt64(((DropDownList)gv_labs.FooterRow.FindControl("dd_labid")).SelectedValue);
            dr["mth_ref"] = ((TextBox)gv_labs.FooterRow.FindControl("tb_mr")).Text;
            dt.Rows.Add(dr);
            ViewState["dt_labs"] = dt;

            //Bind
            gv_labs.DataSource = dt;
            gv_labs.DataBind();

            //creating datatable to enter quantity per sample per lab
            create_dt_quantity();
        }
        else
        {
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(this.GetType(), "error", "<script>alert('First select the Sample to select the lab.');</script>");
        }
    }

    private void create_dt_quantity()
    {
        //creating datatable to enter quantity per sample per lab
        DataTable dt_labs = (DataTable)ViewState["dt_labs"];
        DataTable dt_quantity = AddColumns_DataTableQuantity();
        foreach (DataRow dr_labs in dt_labs.Rows)
        {
            DataRow dr_qty = dt_quantity.NewRow();
            long ts_id = Convert.ToInt64(dr_labs["ts_id"]);
            ds_analytics.test_samplesDataTable ts_dt = test_samples.getTestSampleBy_ts_id(ts_id);
            dr_qty["labid"] = Convert.ToInt64(dr_labs["labid"]);
            dr_qty["labname"] = m_labs.getLabByLabid(Convert.ToInt64(dr_labs["labid"]))[0].labname;
            dr_qty["sampleid"] = ts_dt[0].sampleid;            
            dt_quantity.Rows.Add(dr_qty);
        }

        DataView view = new DataView(dt_quantity);
        dt_quantity = view.ToTable(true, "labid", "labname", "sampleid");
        gv_lab_sample_quantity.DataSource = dt_quantity;
        gv_lab_sample_quantity.DataBind();
        ViewState["dt_quantity"] = dt_quantity;
        ViewState["SortOrder"] = " ASC";
    }

    protected void gv_lab_sample_quantity_Sorting(object sender, GridViewSortEventArgs e)
    {        
        DataTable dt = (DataTable)ViewState["dt_quantity"];
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
        gv_lab_sample_quantity.DataSource = dt;
        gv_lab_sample_quantity.DataBind();
        ViewState["dt_quantity"] = dt;
    }

    protected void gv_lab_sample_quantity_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.CssClass = "row";            
        }
    }

    protected void gv_labs_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "del")
        {            
            GridViewRow gvr = ((GridViewRow)((ImageButton)e.CommandSource).NamingContainer);
            DataTable dt = (DataTable)ViewState["dt_labs"];

            //Reduce row count            
            string btn_id = dt.Rows[gvr.RowIndex]["btn_id"].ToString();
            int r_index = Convert.ToInt32(btn_id.Split('-')[0]);
            string btnid = btn_id.Split('-')[1];
            Button btn = (Button)gv_ts_lab_selection.Rows[r_index].FindControl(btnid);
            char[] splitor = { '(', ')' };
            int lab_count = Convert.ToInt32(btn.Text.Split(splitor)[1]);
            btn.Text = "Lab(" + (lab_count - 1).ToString() + ")";

            //delete
            dt.Rows.RemoveAt(gvr.RowIndex);
            ViewState["dt_labs"] = dt;

            //Bind
            gv_labs.DataSource = dt;
            gv_labs.DataBind();

            //creating datatable to enter quantity per sample per lab
            create_dt_quantity();
        }
    }

    protected void gv_labs_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        GridView gv = (GridView)sender;

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.CssClass = "row";

            long ts_id = Convert.ToInt64(gv.DataKeys[e.Row.RowIndex].Value);
            ds_analytics.test_samplesDataTable ts_dt = test_samples.getTestSampleBy_ts_id(ts_id);
            e.Row.Cells[2].Text = req_tests.getTestbyTestid(ts_dt[0].test_id)[0].testname;
            e.Row.Cells[3].Text = ts_dt[0].sampleid.Substring(8);
            ((Label)e.Row.Cells[4].FindControl("lbl_labid")).Text = m_labs.getLabByLabid(Convert.ToInt64(((Label)e.Row.Cells[4].FindControl("lbl_labid")).Text))[0].labname;
        }
    }

    protected void gv_fill_result_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.CssClass = "row";
        }
    }

    protected void gv_view_result_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.CssClass = "row";
            if (e.Row.RowState == DataControlRowState.Normal)
            {
                HyperLink hl_attach = ((HyperLink)e.Row.FindControl("hlink_attach"));
                if ((hl_attach.Text != null) && (hl_attach.Text != string.Empty))
                {
                    hl_attach.NavigateUrl = "~/upload/labresult_coa/" + hl_attach.Text;
                }
            }
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
        hl_print_request.NavigateUrl = "~/UI/users/print_request.aspx?reqid="+e64.Encrypt(reqid);
        hl_pdf_request.NavigateUrl = "~/UI/users/print_request.aspx?reqid=" + e64.Encrypt(reqid) + "&action=pdf";
        //setting labels and InfoBox
        lbl_requestor.Text = "(Request by- "+m_users.getFullnameByuserid(req_row.reqfrom)+")";
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
            int sample_no = (tab_num-1)*num_samples_intab+i;
            string sampleid = dt_samples.Rows[sample_no - 1]["sampleid"].ToString();
            DataTable dt_pvalue = sample_pvalue.getPvaluesBySampleid(sampleid);
            foreach (DataRow dr in dt_pvalue.Rows)
            {
                int propertyid = Convert.ToInt32(dr["propertyid"]);
                dt.Rows[propertyid-1][sample_no.ToString()] = dr["pvalue"].ToString();
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
    
    protected void btn_approve_Click(object sender, EventArgs e)
    {
        //update request statusid        
        req_row.statusid = 3;
        req_row.receive_cmnt = tb_comment.Text;
        req_row.approvedate = DateTime.Now;
        requests.update_Request(req_row);
        do_mail("approved");

        //redirect
        string url = "home.aspx";
        ClientScript.RegisterStartupScript(this.GetType(), "callfunction", "alert('Request has been approved.');window.location.href = '" + url + "';", true);
    }
    
    protected void btn_decline_Click(object sender, EventArgs e)
    {
        //update request statusid        
        req_row.statusid = 4;
        req_row.receive_cmnt = tb_comment.Text;
        req_row.declinedate = DateTime.Now;
        requests.update_Request(req_row);
        do_mail("declined");

        //redirect
        string url = "home.aspx";
        ClientScript.RegisterStartupScript(this.GetType(), "callfunction", "alert('Request has been declined.');window.location.href = '" + url + "';", true);
    }

    protected void btn_sendlab_Click(object sender, EventArgs e)
    {
        DataTable dt = (DataTable)ViewState["dt_labs"];

        if (dt.Rows.Count > 0)
        {            
            foreach (DataRow dr in dt.Rows)
            {
                //insert lab result row (i.e. labid, mth_ref)
                long ts_id = Convert.ToInt64(dr["ts_id"]);
                long labid = Convert.ToInt64(dr["labid"]);
                string mth_ref = dr["mth_ref"].ToString();
                labresult.insertLabResult(ts_id, labid, mth_ref);
            }

            foreach (GridViewRow gvr in gv_lab_sample_quantity.Rows)
            {
                if (gvr.RowType == DataControlRowType.DataRow)
                {
                    //insert Quantity per lab per sample
                    long labid = Convert.ToInt64(gv_lab_sample_quantity.DataKeys[gvr.RowIndex].Value);
                    string sampleid = gvr.Cells[2].Text;
                    string qty = ((TextBox)(gvr.Cells[3].FindControl("tb_qty"))).Text;
                    lab_sample_quantity.insert_LabSampleQuantity(labid, sampleid, qty);
                }
            }

            //update request
            req_row.statusid = 5;
            req_row.lab_cmnt = tb_sendlab.Text;
            req_row.labdate = DateTime.Now;
            requests.update_Request(req_row);

            //mail_to_each_lab
            DataTable dt_quantity = ((DataTable)ViewState["dt_quantity"]);
            DataView view = new DataView(dt_quantity);
            dt_quantity = view.ToTable(true, "labid");
            foreach (DataRow dr in dt_quantity.Rows)
            {
                mail_to_lab(Convert.ToInt64(dr["labid"]));
            }

            //redirect
            string url = "home.aspx";
            ClientScript.RegisterStartupScript(this.GetType(), "callfunction", "alert('Request has been sent to the Selected Labs.');window.location.href = '" + url + "';", true);            
        }
        else
        {
            ClientScript.RegisterStartupScript(this.GetType(), "callfunction", "alert('You have not selected any lab. Please select the labs first to send the Samples.');", true);
        }
    }    

    protected void btn_submit_result_Click(object sender, EventArgs e)
    {
        bool atleast_one_selected = false;
        foreach (GridViewRow gvr in gv_fill_result.Rows)
        {
            if (gvr.RowType == DataControlRowType.DataRow)
            {
                CheckBox cb = (CheckBox)gvr.FindControl("cb_forreport");
                if (cb.Checked == true)
                {
                    atleast_one_selected = true;
                    break;
                }
            }
        }

        if (atleast_one_selected)
        {
            foreach (GridViewRow gvr in gv_fill_result.Rows)
            {
                if (gvr.RowType == DataControlRowType.DataRow)
                {
                    try
                    {
                        long labresultid = Convert.ToInt64(((DataKey)gv_fill_result.DataKeys[gvr.RowIndex]).Value);
                        string result = ((TextBox)gvr.FindControl("tb_result")).Text;
                        if (result == "")
                        {
                            result = null;
                        }
                        bool isfor_report = ((CheckBox)gvr.FindControl("cb_forreport")).Checked;
                        FileUpload file_upl = (FileUpload)gvr.FindControl("fl_up_attach");
                        string attach_ID = null;
                        if (file_upl.HasFile)
                        {
                            attach_ID = labresultid.ToString() + "_" + file_upl.FileName;
                            file_upl.SaveAs(Server.MapPath("~/upload/labresult_coa/") + attach_ID.ToString());
                        }

                        labresult.updateLabResult(result, isfor_report, attach_ID, labresultid);
                    }
                    catch (Exception ex)
                    {
                        return;
                    }
                }
            }

            req_row.statusid = 6;
            req_row.result_remark = tb_remark_result.Text;
            if (tb_commenting_result.Text != "")
            {
                req_row.result_cmnt = tb_commenting_result.Text;
            }
            else
            {
                req_row.result_cmnt = null;
            }
            req_row.resultdate = DateTime.Now;
            requests.update_Request(req_row);
            do_mail_result();

            //redirect
            string url = "home.aspx";
            ClientScript.RegisterStartupScript(this.GetType(), "callfunction", "alert('Result filled Successfully.');window.location.href = '" + url + "';", true);
        }
        else 
        {
            //Response.Write("<script>alert('At least one result should be published.');</script>");

            ClientScript.RegisterStartupScript(this.GetType(), "callfunction", "alert('At least one result should be published.');", true);
        }
    }
    
    protected void gv_view_result_RowCommand(object sender, GridViewCommandEventArgs e)
    {        
        if (e.CommandName == "editing")
        {
            gv_view_result.EditIndex = Convert.ToInt32(e.CommandArgument);            
            DataTable dt_result = labresult.getLabResult_ByJoining(req_row.reqid);
            gv_view_result.DataSource = dt_result;
            gv_view_result.DataBind();
        }
        else if (e.CommandName == "deleting")
        {            
            //do attachment=NULL in DB
            long labresult_id = Convert.ToInt64(gv_view_result.DataKeys[gv_view_result.EditIndex].Value);

            GridViewRow gvr = gv_view_result.Rows[gv_view_result.EditIndex];
            string tb_result = ((TextBox)gvr.FindControl("tb_result")).Text;
            bool isfor_report = ((CheckBox)gvr.FindControl("cb_forreport_edit")).Checked;
            labresult.updateLabResult(tb_result, isfor_report, null, labresult_id);

            //delete file from folder
            string filename = ((Label)gvr.FindControl("lbl_attach_edit")).Text;
            string completePath = Server.MapPath("~/upload/labresult_coa/"+filename);
            if (File.Exists(completePath))
            {
                File.Delete(completePath);
            }

            //Rebind
            DataTable dt_result = labresult.getLabResult_ByJoining(req_row.reqid);
            gv_view_result.DataSource = dt_result;
            gv_view_result.DataBind();
        }
        else if (e.CommandName == "updating")
        {
            GridViewRow gvr = gv_view_result.Rows[gv_view_result.EditIndex];
            long labresult_id = Convert.ToInt64(gv_view_result.DataKeys[gv_view_result.EditIndex].Value);
            string tb_result = ((TextBox)gvr.FindControl("tb_result")).Text;
            bool isfor_report = ((CheckBox)gvr.FindControl("cb_forreport_edit")).Checked;
            FileUpload fl_upld = ((FileUpload)gvr.FindControl("fl_up_attach"));
            
            if ((fl_upld.Visible == true))
            {
                string attach_ID = null;
                if (fl_upld.HasFile)
                {
                    attach_ID = labresult_id.ToString() + "_" + fl_upld.FileName;
                    fl_upld.SaveAs(Server.MapPath("~/upload/labresult_coa/") + attach_ID.ToString());
                }
                labresult.updateLabResult(tb_result, isfor_report, attach_ID, labresult_id);
            }
            else
            {
                //attachment not changing..                
                string filename = ((Label)gvr.FindControl("lbl_attach_edit")).Text;
                labresult.updateLabResult(tb_result, isfor_report, filename, labresult_id);
            }

            //Rebinding
            gv_view_result.EditIndex = -1;
            DataTable dt_result = labresult.getLabResult_ByJoining(req_row.reqid);
            gv_view_result.DataSource = dt_result;
            gv_view_result.DataBind();
        }
        else if (e.CommandName == "cancelling")
        {
            gv_view_result.EditIndex = -1;
            DataTable dt_result = labresult.getLabResult_ByJoining(req_row.reqid);
            gv_view_result.DataSource = dt_result;
            gv_view_result.DataBind();
        }
    }
    
    protected void gv_view_result_DataBound(object sender, EventArgs e)
    {
        if (gv_view_result.EditIndex != -1)
        {
            GridViewRow gvr = gv_view_result.Rows[gv_view_result.EditIndex];

            Panel pnl_attach = ((Panel)gvr.FindControl("pnl_edit_attach"));
            Label lbl_attach = ((Label)gvr.FindControl("lbl_attach_edit"));
            FileUpload fl_upld = ((FileUpload)gvr.FindControl("fl_up_attach"));
            ImageButton img_btn = ((ImageButton)gvr.FindControl("imgbtn_Delete"));

            pnl_attach.Visible = false;
            fl_upld.Visible = false;

            if ((lbl_attach.Text == null) || (lbl_attach.Text == string.Empty))
            {
                fl_upld.Visible = true;
            }
            else
            {
                pnl_attach.Visible = true;
            }
        }
    }

    private void do_mail(string req_status)
    {        
        string sub = "Analytics: Your Request " + req_row.reqid + " has been " + req_status;
        string from;
        ds_analytics.m_usersRow receiver_row = m_users.getUserByUserid(req_row.responsible)[0];
        if (receiver_row.email != "") { from = receiver_row.email; } else { from = "noreply.rnd@gsk.com"; }

        WebClient wclient = new WebClient();
        string url = HttpContext.Current.Request.Url.AbsoluteUri;
        url = url.Replace("UI/users/reqaction.aspx", "Mailer/reqapprove.htm");
        string pagedata = wclient.DownloadString(url);
        string body = pagedata;

        //mail to requestor
        ds_analytics.m_usersRow requestor_row = m_users.getUserByUserid(req_row.reqfrom)[0];
        if (requestor_row != null && requestor_row.email != "")
        {
            //1. Creating login page link
            url = HttpContext.Current.Request.Url.AbsoluteUri;
            int len = url.IndexOf("UI");
            string base_url = url.Substring(0, len);
            base_url = base_url + "Default.aspx?login=" + req_row.reqfrom + "&redirect=";
            //2. Creating request page link
            url = HttpContext.Current.Request.Url.AbsoluteUri;
            len = url.IndexOf("reqaction");
            string redirect_url = url.Substring(0, len);
            Encryption64 e64 = new Encryption64();
            redirect_url = redirect_url + "reqaction_byme.aspx?reqid=" + e64.Encrypt(req_row.reqid);
            //3. Adding login + redirect link
            string web_link = "<a href='" + base_url + redirect_url + "' target='_blank'>Open Request</a>";
            body = body.Replace("!!~requestor_name~!!", requestor_row.fullname);
            body = body.Replace("!!~req_no~!!", req_row.reqid);
            body = body.Replace("!!~req_decision~!!", req_status);
            body = body.Replace("!!~receiver_name~!!", receiver_row.fullname);
            body = body.Replace("!!~You can open the request at web_link~!!", "You can open the request at " + web_link);

            string[] to = { requestor_row.email };
            string[] cc = new string[2];
            string[] bcc = new string[2];
            analyticsmail ana_mail = new analyticsmail();
            ana_mail.sendmails(to, cc, bcc, from, sub, body, "");
        }        
    }

    private void do_mail_result()
    {
        string sub = "Analytics: Your Request " + req_row.reqid + " has been published with results.";
        string from;
        ds_analytics.m_usersRow receiver_row = m_users.getUserByUserid(req_row.responsible)[0];
        if (receiver_row.email != "") { from = receiver_row.email; } else { from = "noreply.rnd@gsk.com"; }

        WebClient wclient = new WebClient();
        string url = HttpContext.Current.Request.Url.AbsoluteUri;
        url = url.Replace("UI/users/reqaction.aspx", "Mailer/reqresult.htm");
        string pagedata = wclient.DownloadString(url);
        string body = pagedata;

        //mail to requestor
        ds_analytics.m_usersRow requestor_row = m_users.getUserByUserid(req_row.reqfrom)[0];
        if (requestor_row != null && requestor_row.email != "")
        {
            //1. Creating login page link
            url = HttpContext.Current.Request.Url.AbsoluteUri;
            int len = url.IndexOf("UI");
            string base_url = url.Substring(0, len);
            base_url = base_url + "Default.aspx?login=" + req_row.reqfrom + "&redirect=";
            //2. Creating request page link
            url = HttpContext.Current.Request.Url.AbsoluteUri;
            len = url.IndexOf("reqaction");
            string redirect_url = url.Substring(0, len);
            Encryption64 e64 = new Encryption64();
            redirect_url = redirect_url + "reqaction_byme.aspx?reqid=" + e64.Encrypt(req_row.reqid);
            //3. Adding login + redirect link
            string web_link = "<a href='" + base_url + redirect_url + "' target='_blank'>Open Request</a>";
            body = body.Replace("!!~requestor_name~!!", requestor_row.fullname);
            body = body.Replace("!!~req_no~!!", req_row.reqid);            
            body = body.Replace("!!~receiver_name~!!", receiver_row.fullname);
            body = body.Replace("!!~You can open the request at web_link~!!", "You can open the request at " + web_link);

            string[] to = { requestor_row.email };
            string[] cc = new string[2];
            string[] bcc = new string[2];
            analyticsmail ana_mail = new analyticsmail();
            ana_mail.sendmails(to, cc, bcc, from, sub, body, "");
        }
    }

    protected void mail_to_lab(long labid)
    {
        string sub = "GSK: Samples for tests at your lab";
        string from;
        ds_analytics.m_usersRow receiver_row = m_users.getUserByUserid(req_row.responsible)[0];
        if (receiver_row.email != "") { from = receiver_row.email; } else { from = "noreply.rnd@gsk.com"; }

        WebClient wclient = new WebClient();
        string url = HttpContext.Current.Request.Url.AbsoluteUri;
        url = url.Replace("UI/users/reqaction.aspx", "Mailer/reqlab.htm");
        string pagedata = wclient.DownloadString(url);
        string body = pagedata;

        string[] to = new string[2];
        string[] cc = new string[2];
        string[] bcc = new string[2];

        //mail to lab
        ds_analytics.m_labsRow lab_row = m_labs.getLabByLabid(labid)[0];
        if (!lab_row.Isemail1Null() || !lab_row.Isemail2Null())
        {
            if (!lab_row.Isemail1Null() && !lab_row.Isemail2Null())
            {
                //notnull && notnull
                body = body.Replace("!!~lab_manager~!!", lab_row.contact_person);
                to[0] = lab_row.email1;
                cc[0] = lab_row.email2;
            }
            else if (!lab_row.Isemail1Null() && lab_row.Isemail2Null())
            {
                //notnull && null
                body = body.Replace("!!~lab_manager~!!", lab_row.contact_person);
                to[0] = lab_row.email1;                
            }
            else if (lab_row.Isemail1Null() && !lab_row.Isemail2Null())
            {
                //null && notnull
                body = body.Replace("!!~lab_manager~!!", lab_row.key_acc_person);
                to[0] = lab_row.email2;
            }
            analyticsmail ana_mail = new analyticsmail();
            ana_mail.sendmails(to, cc, bcc, from, sub, body, "");
        }
    }
}