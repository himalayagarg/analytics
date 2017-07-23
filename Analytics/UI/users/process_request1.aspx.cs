using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using System.Collections;
using System.Net;
using System.Web;

public partial class UI_users_process_request1 : System.Web.UI.Page
{
    string mode = "edit";
    string user = "requestor";
    string process_reqid = "";
    ds_analytics.requestsRow process_req_row;
    string new_reqid = "";

    string userid;
    int no_samples = 0;
    int no_tests = 0;
    string rtype = null;
    int no_samples_intab = 0;

    DataTable dt_test_sample;

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
        mode = Request.QueryString.Get("mode");
        user = Request.QueryString.Get("user");

        Encryption64 e64 = new Encryption64();
        process_reqid = e64.Decrypt(Request.QueryString.Get("process_reqid").Replace(" ", "+"));
        ds_analytics.requestsDataTable req_dt = requests.getRequestbyReqid(process_reqid);
        process_req_row = req_dt[0];

        userid = Session["userid"].ToString();
        no_samples = Convert.ToInt32(e64.Decrypt(Request.QueryString.Get("nosample").Replace(" ", "+")));
        no_tests = Convert.ToInt32(e64.Decrypt(Request.QueryString.Get("notest").Replace(" ", "+")));
        no_samples_intab = int.Parse(System.Configuration.ConfigurationManager.AppSettings["no_samples_intab"].ToString());        

        if (!IsPostBack)
        {
            lbl_status.Text = other.getStatustext(1);
                                    
            rtype = e64.Decrypt(Request.QueryString.Get("rtype").Replace(" ", "+"));
            
            set_page_ddl();
            bind_page_ddl();

            set_page_samples();            
            
            bind_page_tests();

            build_dt_test_sample();
            build_gv_test_sample();
            bind_gv_test_sample();

            Page.Validate();
        }
        else
        {
            rtype = Request.Form[ddl_typerequest.UniqueID];

            set_page_samples();

            build_gv_test_sample();

            Page.Validate();
        }
    }

    private void set_page_ddl()
    {        
        //1
        ddl_typeanalysis.DataSource = other.getDropdownsbyType("typeanalysis");
        ddl_typeanalysis.DataTextField = "value";
        ddl_typeanalysis.DataValueField = "value";
        ddl_typeanalysis.DataBind();        

        //2
        DataTable dt_typerequests1 = other.getDropdownsbyType("typerequest");
        #region Adding Extra Request-Types if submodule right given
        ArrayList arr_List_subMod = m_users.getSubModulesByUserid(userid);
        if (arr_List_subMod.Contains("Special Request Types"))
        {
            DataTable dt_typerequests2 = other.getDropdownsbyType("typerequest-at");
            foreach (DataRow dr in dt_typerequests2.Rows)
            {
                dt_typerequests1.ImportRow(dr);
            }
        }
        #endregion
        ddl_typerequest.DataSource = dt_typerequests1;        
        ddl_typerequest.DataTextField = "value";
        ddl_typerequest.DataValueField = "value";
        ddl_typerequest.DataBind();        

        //projects : bind all projects (also Inactive and completed, to allow editing)
        ddl_project.DataSource = projects.getAllProjects();
        ddl_project.DataTextField = "projectname";
        ddl_project.DataValueField = "projectid";
        ddl_project.DataBind();

        //lead
        ddl_lead.DataSource = m_users.getAllActiceReceivers();
        ddl_lead.DataTextField = "fullname";
        ddl_lead.DataValueField = "userid";
        ddl_lead.DataBind();
        //ddl_lead.Items.Remove(ddl_lead.Items.FindByValue(userid));
    }

    private void bind_page_ddl()
    {
        //Default Selection of dropdown as per old request
        ddl_project.SelectedValue = process_req_row.projectid.ToString();
        ds_analytics.projectsRow prj_row = projects.getProjectByPrjid(process_req_row.projectid)[0];
        lbl_prj_type.Text = prj_row.projecttype;
        lbl_prj_category.Text = prj_row.projectcategory;
        lbl_prj_brand.Text = prj_row.projectbrand;
        ddl_typeanalysis.SelectedValue = process_req_row.analysistype;
        if (mode == "edit")
        {
            new_reqid = process_reqid;
            lbl_requestid.Text = new_reqid;
            btn_submit.Text = "Resubmit";
            btn_submit.ToolTip = "Submit request changes";
        }
        else if (mode == "copy")
        {
            //new_reqid = requests.get_new_reqid();
            lbl_requestid.Text = "Request Copied";
            lbl_requestid.ToolTip = "New Request No. will be assigned on Submit";
            btn_submit.Text = "Submit Request";
            btn_submit.ToolTip = "Submit new request using copied request";
        }        
        ddl_lead.SelectedValue = process_req_row.responsible;
        ddl_typerequest.SelectedValue = rtype; // due to change in reqtype postback//
        tb_addinfo.Text = process_req_row.req_cmnt;
    }

    private void set_page_samples()
    {
        ds_analytics.req_samplesDataTable req_samp_dt = req_samples.getSamplesByReqid(process_reqid); 
        //1. Removing extra tabpanels
        int no_tabs = Convert.ToInt32(Math.Ceiling(no_samples / Convert.ToDouble(no_samples_intab)));
        while (TabContainer1.Tabs.Count > no_tabs)
        {
            TabContainer1.Tabs.RemoveAt(TabContainer1.Tabs.Count - 1);
        }

        //2. Removing extra samples from last gridview
        int sample_inlastgrid = no_samples % no_samples_intab;
        int columns_tokeep = sample_inlastgrid + 2;                       //2 columns for Property ID, Name
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
                    DataTable dt_for_gv = get_dt_for_gv(tab_no, no_samples_intab, gv.Columns.Count - 2, req_samp_dt); //2 columns for Property ID, Name
                    gv.DataSource = dt_for_gv;
                    gv.DataBind();

                    //setting dropdown in gridview according to the Property value
                    int col_gv = gv.Columns.Count;
                    for (int i = 2; i <= col_gv - 1; i++)
                    {
                        DropDownList ddl_stor = ((DropDownList)(gv.Rows[gv.Rows.Count - 1].Cells[i].Controls[5]));
                        ddl_stor.SelectedValue = dt_for_gv.Rows[gv.Rows.Count - 1][i].ToString();
                    }
                }
                tab_no++;
            }
        }
    }

    protected void GridView_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //Putting calendars, DropDowns, Validation etc.

        GridView gv = ((GridView)(sender));
        int gridviewno = Convert.ToInt32(gv.ID.Substring(8));
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //1
            e.Row.CssClass = "row";

            for (int cell_no = 2; cell_no < e.Row.Cells.Count; cell_no++)
            {
                //Important(get tb1, tb2 etc. TextBox's ID in the aspx pages are well named, so it can be found when required )
                TextBox tb = (TextBox)e.Row.Cells[cell_no].FindControl("tb" + ((no_samples_intab * (gridviewno - 1)) + (cell_no - 1)).ToString());
                RequiredFieldValidator rfv = (RequiredFieldValidator)e.Row.Cells[cell_no].FindControl("RequiredFieldValidator" + ((no_samples_intab * (gridviewno - 1)) + (cell_no - 1)).ToString());
                rfv.Enabled = true;
                int propertyid = Convert.ToInt32(e.Row.Cells[0].Text);
                if (!other.getIsMandatoryPropertyByTypeRequest(rtype, propertyid))
                {
                    rfv.Enabled = false;
                }

                if (e.Row.RowIndex == 4 || e.Row.RowIndex == 9)
                {
                    //2 Add Calendar Extender
                    AjaxControlToolkit.CalendarExtender aj_ce = new AjaxControlToolkit.CalendarExtender();
                    aj_ce.PopupPosition = AjaxControlToolkit.CalendarPosition.Right;
                    aj_ce.TargetControlID = tb.ID;
                    aj_ce.Format = "dd/MM/yyyy";
                    e.Row.Cells[cell_no].Controls.Add(aj_ce);

                    if (e.Row.RowIndex == 4)
                    {
                        //Not allow future date
                        RangeValidator rv = new RangeValidator();
                        rv.ForeColor = System.Drawing.Color.Red;
                        rv.Type = ValidationDataType.Date;
                        rv.ErrorMessage = "Should not be future date.";
                        rv.ValidationGroup = "submit";
                        rv.Display = ValidatorDisplay.Dynamic;
                        rv.ControlToValidate = tb.ID;
                        rv.MinimumValue = DateTime.MinValue.ToString("dd/MM/yyyy");
                        rv.MaximumValue = DateTime.Today.ToString("dd/MM/yyyy");
                        e.Row.Cells[cell_no].Controls.Add(rv);
                    }

                    if (e.Row.RowIndex == 9)
                    {
                        //Stability Date must be greater or equal to Mkg. Date
                        TextBox tb_mkg = (TextBox)gv.Rows[4].Cells[cell_no].FindControl("tb" + ((no_samples_intab * (gridviewno - 1)) + (cell_no - 1)).ToString());
                        tb.Attributes.Add("onchange", "validate_StabilityDate('" + tb_mkg.ClientID + "','" + tb.ClientID + "');");
                    }

                    //Add Compare Validator
                    CompareValidator cv = new CompareValidator();
                    cv.ForeColor = System.Drawing.Color.Red;
                    cv.Type = ValidationDataType.Date;
                    cv.ErrorMessage = " Invalid Date.";
                    cv.ValidationGroup = "submit";
                    cv.Operator = ValidationCompareOperator.DataTypeCheck;
                    cv.Display = ValidatorDisplay.Dynamic;
                    cv.ControlToValidate = tb.ID;
                    e.Row.Cells[cell_no].Controls.Add(cv);
                }

                if (e.Row.RowIndex == 6 || e.Row.RowIndex == 7)
                {
                    RegularExpressionValidator rx_val = new RegularExpressionValidator();
                    rx_val.ForeColor = System.Drawing.Color.Red;
                    rx_val.ErrorMessage = "Only Numbers";
                    rx_val.ValidationGroup = "submit";
                    rx_val.Display = ValidatorDisplay.Dynamic;
                    rx_val.ControlToValidate = tb.ID;
                    rx_val.ValidationExpression = @"\d+\.{0,1}\d*";
                    e.Row.Cells[cell_no].Controls.Add(rx_val);
                }

                if (e.Row.RowIndex == 11)
                {
                    //3 Remove Textbox where dropdowns should be placed                  
                    tb.Attributes["style"] = "display:none";

                    //Add Dropdown for storage conditions
                    DropDownList ddl = new DropDownList();
                    ddl.ID = "ddl" + gridviewno.ToString() + (e.Row.RowIndex + 1).ToString() + (cell_no - 1).ToString();
                    AddItems_ddl(ddl);

                    e.Row.Cells[cell_no].Controls.Add(ddl);
                }
            }
        }
    }

    protected void AddItems_ddl(DropDownList ddl)
    {
        ddl.DataSource = other.getDropdownsbyType("storagecondition");
        ddl.DataTextField = "value";
        ddl.DataValueField = "value";
        ddl.DataBind();
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

        //2 setting value of the property in the sample columns created above
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

    protected void gv_tests_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.CssClass = "row";
            TextBox tb_testname = ((TextBox)e.Row.FindControl("tb_testname"));            
            tb_testname.Attributes.Add("onkeyup", "chng_testname(this,'" + (e.Row.RowIndex + 1).ToString() + "')");
        }
    }

    private void bind_page_tests()
    {        
        ds_analytics.req_testsDataTable req_test_dt = req_tests.getTestsbyReqid(process_reqid);
        gv_tests.DataSource = req_test_dt;
        gv_tests.DataBind();

        foreach (GridViewRow gvr in gv_tests.Rows)
        {
            DropDownList ddl_ref = ((DropDownList)gvr.FindControl("dd_specification"));
            ddl_ref.SelectedValue = req_test_dt.Rows[gvr.RowIndex]["reference"].ToString();
        }
    }
    
    protected void gv_test_sample_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.CssClass = "row";
        }
    }
    
    protected void gv_test_sample_DataBound(object sender, EventArgs e)
    {
        if (gv_test_sample.EditIndex != -1)
        {
            GridViewRow editRow = gv_test_sample.Rows[gv_test_sample.EditIndex];
            editRow.BackColor = Color.Pink;
            CheckBox cb_all = ((CheckBox)editRow.FindControl("chk_Edit0"));
            cb_all.Attributes.Add("onclick", "chkall(" + (editRow.RowIndex + 1).ToString() + "," + cb_all.ClientID + ")");
        }
    }
    
    protected void imgbtn_Edit_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gv_row = (GridViewRow)((sender as ImageButton).NamingContainer);

        gv_test_sample.EditIndex = gv_row.RowIndex;

        dt_test_sample = (DataTable)Session["dt_test_sample"];
        gv_test_sample.DataSource = dt_test_sample;
        gv_test_sample.DataBind();

        Page.Validate();
    }
    
    protected void imgbtn_Update_Click(object sender, ImageClickEventArgs e)
    {
        dt_test_sample = (DataTable)Session["dt_test_sample"];
        GridViewRow editRow = gv_test_sample.Rows[gv_test_sample.EditIndex];
        for (int i = 2; i <= dt_test_sample.Columns.Count - 1; i++)
        {
            dt_test_sample.Rows[gv_test_sample.EditIndex][i] = ((CheckBox)editRow.Cells[i + 1].Controls[0]).Checked;
        }

        gv_test_sample.EditIndex = -1;
        Session["dt_test_sample"] = (DataTable)dt_test_sample;
        gv_test_sample.DataSource = dt_test_sample;
        gv_test_sample.DataBind();

        Page.Validate();
    }
    
    protected void imgbtn_Cancel_Click(object sender, ImageClickEventArgs e)
    {
        gv_test_sample.EditIndex = -1;

        dt_test_sample = (DataTable)Session["dt_test_sample"];
        gv_test_sample.DataSource = dt_test_sample;
        gv_test_sample.DataBind();

        Page.Validate();
    }

    private void build_dt_test_sample()
    {
        //Getting req_tests table from the database
        ds_analytics.req_testsDataTable dt_test = req_tests.getTestsbyReqid(process_reqid);

        //Make test_sample DataTable to bind to Gridview       
        dt_test_sample = new DataTable();
        dt_test_sample.Columns.Add(new DataColumn("test_id", typeof(System.Int64)));
        dt_test_sample.Columns.Add(new DataColumn("testname", typeof(System.String)));
        dt_test_sample.Columns.Add(new DataColumn("all", typeof(System.Boolean)));
        for (int i = 1; i <= no_samples; i++)
        {
            dt_test_sample.Columns.Add(new DataColumn(i.ToString(), typeof(System.Boolean)));
        }
        //building datatable for gridview
        foreach (ds_analytics.req_testsRow dr in dt_test.Rows)
        {
            DataRow dr_new = dt_test_sample.NewRow();
            dr_new["test_id"] = dr.test_id;
            dr_new["testname"] = dr.testname;
            bool all_selected = true;
            ds_analytics.test_samplesDataTable ts_dt = test_samples.getTest_SamplesByTestid(dr.test_id);
            int i = 1;
            foreach (ds_analytics.test_samplesRow ts_row in ts_dt.Rows)
            {
                dr_new[i.ToString()] = ts_row.isselected;
                i++;
                if (!ts_row.isselected)
                {
                    all_selected = false;
                }
            }
            dr_new["all"] = all_selected;
            dt_test_sample.Rows.Add(dr_new);
        }
        Session["dt_test_sample"] = (DataTable)dt_test_sample;
    }

    private void build_gv_test_sample()
    {
        //Adding All column checkboxes
        add_template_gv("All", "all", "0");
        //Adding sample columns checkboxes
        for (int i = 1; i <= no_samples; i++)
        {
            add_template_gv(i.ToString(), i.ToString(), i.ToString());
        }
    }

    private void bind_gv_test_sample()
    {
        dt_test_sample = (DataTable)Session["dt_test_sample"];
        gv_test_sample.DataSource = dt_test_sample;
        gv_test_sample.DataBind();
    }

    private void add_template_gv(string headertext, string bindfield, string id)
    {
        TemplateField tf = new TemplateField();
        tf.HeaderText = headertext;

        tf.ItemTemplate = new CheckBoxTemplate(ListItemType.Item, "chk_Item" + id, bindfield);
        tf.EditItemTemplate = new CheckBoxTemplate(ListItemType.EditItem, "chk_Edit" + id, bindfield);

        gv_test_sample.Columns.Add(tf);
    }

    protected void ddl_typerequest_SelectedIndexChanged(object sender, EventArgs e)
    {
        Encryption64 e64 = new Encryption64();
        string nosample = e64.Encrypt(no_samples.ToString());
        string notest = e64.Encrypt(no_tests.ToString());
        string reqid = e64.Encrypt(process_reqid);
        Response.Redirect("~/UI/users/process_request1.aspx?mode=" + mode + "&user=" + user + "&process_reqid=" + reqid + "&nosample=" + nosample + "&notest=" + notest + "&rtype=" + e64.Encrypt(rtype));
    }

    protected void btn_submit_Click(object sender, EventArgs e)
    {
        if (mode == "edit")
        {
            //update existing request data

            #region Request                        

            //1. update request
            process_req_row.projectid = Convert.ToInt64(ddl_project.SelectedValue);            
            process_req_row.analysistype = ddl_typeanalysis.SelectedValue;
            process_req_row.reqtype = ddl_typerequest.SelectedValue;
            process_req_row.responsible = ddl_lead.SelectedValue;            
            process_req_row.statusid = 2;
            process_req_row.req_cmnt = tb_addinfo.Text;            
            requests.update_Request(process_req_row);

            #endregion

            #region Samples

            //2. update sample values
            int tab_no = 1;
            AjaxControlToolkit.TabContainer container = (AjaxControlToolkit.TabContainer)TabContainer1;
            foreach (object obj in container.Controls)
            {
                if (obj is AjaxControlToolkit.TabPanel)
                {
                    AjaxControlToolkit.TabPanel tabPanel = (AjaxControlToolkit.TabPanel)obj;
                    {
                        GridView gv = ((GridView)(tabPanel.FindControl("GridView" + tab_no.ToString())));
                        int cols = gv.Columns.Count;
                        for (int i_col = 2; i_col < cols; i_col++)
                        {
                            int sampleno = Convert.ToInt32(gv.Columns[i_col].HeaderText.Substring(6));
                            string sampleid = String.Concat(process_reqid, sampleno.ToString("00"));

                            //insert a row in req_samples table
                            
                            String value = "";
                            int propertyid;
                            foreach (GridViewRow gvr in gv.Rows)
                            {
                                if (gvr.RowType == DataControlRowType.DataRow)
                                {
                                    propertyid = Convert.ToInt32(gvr.Cells[0].Text);
                                    if (gvr.RowIndex != 11)
                                    {
                                        value = ((TextBox)(gvr.Cells[i_col].Controls[1])).Text;
                                    }
                                    else
                                    {
                                        value = ((DropDownList)(gvr.Cells[i_col].Controls[5])).SelectedValue;
                                    }

                                    //update a row in sample_pvalue table
                                    sample_pvalue.updateSample_Pvalue(sampleid, propertyid, value);
                                }
                            }
                        }
                    }
                    tab_no++;
                }
            }
            #endregion

            #region Tests

            foreach (GridViewRow gvr in gv_tests.Rows)
            {
                if (gvr.RowType == DataControlRowType.DataRow)
                {
                    long test_id = Convert.ToInt64(gv_tests.DataKeys[gvr.RowIndex].Value);
                    string testname = ((TextBox)gvr.FindControl("tb_testname")).Text;
                    string reference = ((DropDownList)gvr.FindControl("dd_specification")).SelectedValue;
                    string standard = ((TextBox)gvr.FindControl("tb_standard")).Text;
                    string unit = ((TextBox)gvr.FindControl("tb_unit")).Text;

                    //Update Test Details in req_tests table
                    req_tests.update_Req_Tests(testname, reference, standard, unit, test_id);
                }
            }
            #endregion

            #region Test_Samples

            if (!is_test_sample_selected())
            {
                string errorText = "You have not selected the samples for all tests. You should select at least one sample for each test.";
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "error", "<script>alert('" + errorText + "');</script>");
            }
            else if (gv_test_sample.EditIndex == -1)
            {
                dt_test_sample = (DataTable)Session["dt_test_sample"];
                foreach (DataRow dr in dt_test_sample.Rows)
                {
                    long test_id = Convert.ToInt64(dr["test_id"]);
                    for (int no = 3; no <= dt_test_sample.Columns.Count - 1; no++)
                    {
                        bool isselected = Convert.ToBoolean(dr[no]);
                        string sampleid = process_reqid + (no - 2).ToString("00");
                        //update test_sample
                        test_samples.updateTest_Sample(test_id, sampleid, isselected);
                    }
                }

                //redirect
                string url = "home.aspx";
                ClientScript.RegisterStartupScript(this.GetType(), "callfunction", "alert('Updated Request Submitted Successfully.');window.location.href = '" + url + "';", true);
            }
            else
            {
                string errorText = "Error: Selection of Samples for Test is not correct. Please ensure you have selected samples for each test.";
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "error", "<script>alert('" + errorText + "');</script>");
            }
            #endregion            

            //mail
            do_mail(mode, process_req_row.reqid);
        }
        else if (mode == "copy")
        {
            if (!is_test_sample_selected())
            {
                string errorText = "You have not selected the samples for all tests. You should select at least one sample for each test.";
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "error", "<script>alert('" + errorText + "');</script>");
            }
            else if (gv_test_sample.EditIndex == -1)
            {
                //insert new request data

                #region Request

                string new_reqid = requests.get_new_reqid();
                //Insert Request Details + add info
                requests.insert_request(new_reqid, Convert.ToInt64(ddl_project.SelectedValue), ddl_typeanalysis.SelectedValue, ddl_typerequest.SelectedValue, userid, ddl_lead.SelectedValue, ddl_lead.SelectedValue, null, 2, tb_addinfo.Text);

                #endregion

                #region Samples

                int tab_no = 1;
                AjaxControlToolkit.TabContainer container = (AjaxControlToolkit.TabContainer)TabContainer1;
                foreach (object obj in container.Controls)
                {
                    if (obj is AjaxControlToolkit.TabPanel)
                    {
                        AjaxControlToolkit.TabPanel tabPanel = (AjaxControlToolkit.TabPanel)obj;
                        {
                            GridView gv = ((GridView)(tabPanel.FindControl("GridView" + tab_no.ToString())));
                            int cols = gv.Columns.Count;
                            for (int i_col = 2; i_col < cols; i_col++)
                            {
                                int sampleno = Convert.ToInt32(gv.Columns[i_col].HeaderText.Substring(6));
                                string sampleid = String.Concat(new_reqid, sampleno.ToString("00"));

                                //insert a row in req_samples table
                                req_samples.insert_Req_Samples(sampleid, new_reqid, sampleno, true);

                                String value = "";
                                int propertyid;
                                foreach (GridViewRow gvr in gv.Rows)
                                {
                                    if (gvr.RowType == DataControlRowType.DataRow)
                                    {
                                        propertyid = Convert.ToInt32(gvr.Cells[0].Text);
                                        if (gvr.RowIndex != 11)
                                        {
                                            value = ((TextBox)(gvr.Cells[i_col].Controls[1])).Text;
                                        }
                                        else
                                        {
                                            value = ((DropDownList)(gvr.Cells[i_col].Controls[5])).SelectedValue;
                                        }

                                        //insert a row in sample_pvalue table
                                        sample_pvalue.insertSample_Pvalue(sampleid, propertyid, value);
                                    }
                                }
                            }
                        }
                        tab_no++;
                    }
                }
                #endregion

                #region Tests

                foreach (GridViewRow gvr in gv_tests.Rows)
                {
                    if (gvr.RowType == DataControlRowType.DataRow)
                    {
                        string testname = ((TextBox)gvr.FindControl("tb_testname")).Text;
                        string reference = ((DropDownList)gvr.FindControl("dd_specification")).SelectedValue;
                        string standard = ((TextBox)gvr.FindControl("tb_standard")).Text;
                        string unit = ((TextBox)gvr.FindControl("tb_unit")).Text;

                        //Insert Test Details in req_tests table
                        long? test_id = req_tests.insert_Req_Tests(new_reqid, testname, reference, standard, unit);
                        if (test_id.HasValue)
                        {
                            #region Test-Samples

                            dt_test_sample = (DataTable)Session["dt_test_sample"];
                            for (int no = 3; no <= dt_test_sample.Columns.Count - 1; no++)
                            {
                                //in copy request "dt_test_sample" has one extra column named test_id
                                bool isselected = Convert.ToBoolean(dt_test_sample.Rows[gvr.RowIndex][no]);
                                string sampleid = new_reqid + (no - 2).ToString("00");
                                //Insert Test-Samples Selected in test_samples table
                                test_samples.insertTest_Sample(test_id, sampleid, isselected);
                            }
                            #endregion
                        }
                    }
                }
                #endregion

                //mail
                do_mail(mode, new_reqid);

                //redirect
                string url = "home.aspx";
                ClientScript.RegisterStartupScript(this.GetType(), "callfunction", "alert('Request Submitted Successfully.');window.location.href = '" + url + "';", true);
            }
            else
            {
                string errorText = "You have not selected the samples for tests appropriately. You should select at least one sample for each test.";
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "error", "<script>alert('" + errorText + "');</script>");
            }
        }
    }

    protected void ddl_project_SelectedIndexChanged(object sender, EventArgs e)
    {
        long prj_id = Convert.ToInt64(ddl_project.SelectedValue);
        ds_analytics.projectsRow prj_row = projects.getProjectByPrjid(prj_id)[0];
        lbl_prj_type.Text = prj_row.projecttype;
        lbl_prj_category.Text = prj_row.projectcategory;
        lbl_prj_brand.Text = prj_row.projectbrand;
    }

    private void do_mail(string mode, string submitted_reqid)
    {        
        ds_analytics.requestsDataTable req_dt = requests.getRequestbyReqid(submitted_reqid);
        ds_analytics.requestsRow submitted_req_row = req_dt[0];
        ds_analytics.m_usersRow requestor_row = m_users.getUserByUserid(submitted_req_row.reqfrom)[0];
        string sub = "";
        if (mode == "edit")
        {
            sub = "Analytics: Request " + submitted_req_row.reqid + " resubmitted by " + requestor_row.fullname;
        }
        else if (mode == "copy")
        {
            sub = "Analytics: New Request " + submitted_req_row.reqid + " raised by " + requestor_row.fullname;
        }
        
        string from;
        if (requestor_row.email != "") { from = requestor_row.email; } else { from = "noreply.rnd@gsk.com"; }

        WebClient wclient = new WebClient();
        string url = HttpContext.Current.Request.Url.AbsoluteUri;
        url = url.Replace("UI/users/process_request1.aspx", "Mailer/reqsubmit.htm");
        string pagedata = wclient.DownloadString(url);
        string body = pagedata;

        //mail to receiver
        ds_analytics.m_usersRow rec_row = m_users.getUserByUserid(ddl_lead.SelectedValue)[0];
        if (rec_row != null && rec_row.email != "")
        {
            //1. Creating login page link
            url = HttpContext.Current.Request.Url.AbsoluteUri;
            int len = url.IndexOf("UI");
            string base_url = url.Substring(0, len);
            base_url = base_url + "Default.aspx?login=" + rec_row.userid + "&redirect=";
            //2. Creating request page link
            url = HttpContext.Current.Request.Url.AbsoluteUri;
            len = url.IndexOf("process_request1");
            string redirect_url = url.Substring(0, len);
            Encryption64 e64 = new Encryption64();
            redirect_url = redirect_url + "reqaction.aspx?reqid=" + e64.Encrypt(submitted_req_row.reqid);
            //3. Adding login + redirect link
            string web_link = "<a href='" + base_url + redirect_url + "' target='_blank'>Open Request</a>";
            body = body.Replace("!!~receiver_name~!!", rec_row.fullname);
            body = body.Replace("!!~no_samples~!!", no_samples.ToString());
            body = body.Replace("!!~requestor_name~!!", requestor_row.fullname);
            body = body.Replace("!!~You can open the request at web_link~!!", "You can open the request at " + web_link);

            string[] to = { rec_row.email };
            string[] cc = new string[2];
            string[] bcc = new string[2];
            analyticsmail ana_mail = new analyticsmail();
            ana_mail.sendmails(to, cc, bcc, from, sub, body, "");
        }        
    }

    private bool is_test_sample_selected()
    {
        dt_test_sample = (DataTable)Session["dt_test_sample"];
        foreach (GridViewRow gvr in gv_test_sample.Rows)
        {
            bool isvalid = false;
            if (gvr.RowType == DataControlRowType.DataRow)
            {
                for (int no = 3; no <= dt_test_sample.Columns.Count - 1; no++)
                {
                    bool isselected = Convert.ToBoolean(dt_test_sample.Rows[gvr.RowIndex][no]);
                    if (isselected)
                    {
                        isvalid = true;
                        break;
                    }
                }
                if (isvalid == false)
                {
                    return false;
                }
                else
                {

                }
            }
        }
        return true;
    }
}