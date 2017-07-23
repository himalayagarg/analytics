using System.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text.RegularExpressions;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.xml;
using System.Xml;
using iTextSharp.text.html.simpleparser;

public partial class UI_users_print_request : System.Web.UI.Page
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

    protected void Page_Load(object sender, EventArgs e)
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

        if (!IsPostBack)
        {
            bind_req_header();
            bind_req_samples();
            bind_req_tests();
            bind_test_samples();
        }

        else
        {

        }
         
        // Auto Print Pop-up
        Page.ClientScript.RegisterStartupScript(this.GetType(), "Print", "javascript:window.print();", true);
    }

    private void bind_req_header()
    {        
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

        //removing extra samples from last tab
        int sample_inlastgrid = no_samples % no_samples_intab;
        int columns_tokeep = sample_inlastgrid + 2;                         //2 columns for Property ID, Name
        string gv_last = "GridView" + no_tabs.ToString();

        //loop through tabcontainer
        int tab_no = 1;        
        foreach (object obj in Panel2.Controls)
        {
            if ((obj is GridView) && (tab_no<=no_tabs))
            {                
                {
                    GridView gv = ((GridView)(obj));
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
            dt_test_sample.Columns.Add(new DataColumn(i.ToString(), typeof(System.String)));
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

    protected override void Render(HtmlTextWriter writer)
    {
        if (!string.IsNullOrEmpty(Request.QueryString.Get("action"))&&(Request.QueryString.Get("action")=="pdf"))
        {
            MemoryStream mem = new MemoryStream();
            StreamWriter twr = new StreamWriter(mem);
            HtmlTextWriter myWriter = new HtmlTextWriter(twr);
            base.Render(myWriter);
            myWriter.Flush();
            myWriter.Dispose();
            StreamReader strmRdr = new StreamReader(mem);
            strmRdr.BaseStream.Position = 0;
            string pageContent = strmRdr.ReadToEnd();
            strmRdr.Dispose();
            mem.Dispose();
            writer.Write(pageContent);
            CreatePDFDocument(pageContent);
        }
        else
        {
            base.Render(writer);
        }
    }

    public void CreatePDFDocument(string strHtml)
    {
        string strFileName = HttpContext.Current.Server.MapPath("request.pdf");
        // step 1: creation of a document-object
        Document document = new Document();
        // step 2:
        // we create a writer that listens to the document
        PdfWriter.GetInstance(document, new FileStream(strFileName, FileMode.Create));
        StringReader se = new StringReader(strHtml);
        HTMLWorker obj = new HTMLWorker(document);
        document.Open();
        obj.Parse(se);
        document.Close();
        ShowPdf(strFileName);
    }

    public void ShowPdf(string strFileName)
    {
        Response.ClearContent();
        Response.ClearHeaders();
        Response.AddHeader("Content-Disposition", "inline;filename=" + strFileName);
        Response.ContentType = "application/pdf";
        Response.WriteFile(strFileName);
        Response.Flush();
        Response.Clear();
    }
}