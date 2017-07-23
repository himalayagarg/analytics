using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class UI_users_cover_letter1 : System.Web.UI.Page
{
    string reqid;
    long labid;
    string userid;

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
        labid = Convert.ToInt64(e64.Decrypt(Request.QueryString.Get("labid").Replace(" ", "+")));
        userid = Session["userid"].ToString();

        load_gsk_address();
        load_lab();
        load_sample_grid();
        load_sender();

        //2 - Auto Print Pop-up
        Page.ClientScript.RegisterStartupScript(this.GetType(), "Print", "javascript:window.print();", true);
    }    

    private void load_gsk_address()
    {
        string name1 = other.getDropdownsbyType("gskname1").Rows[0]["value"].ToString();
        string limited = "Consumer Healthcare Limited";
        if (projects.getProjectByPrjid(requests.getRequestbyReqid(reqid)[0].projectid)[0].projectcategory.ToLower() == "wellness")
        {
            limited = "Asia Private Limited";
        }
        string name2 = other.getDropdownsbyType("gskname2").Rows[0]["value"].ToString();
        string address = other.getDropdownsbyType("gskaddress").Rows[0]["value"].ToString();
        string city = other.getDropdownsbyType("gskcity").Rows[0]["value"].ToString();
        string pin = other.getDropdownsbyType("gskpin").Rows[0]["value"].ToString();
        string state = other.getDropdownsbyType("gskstate").Rows[0]["value"].ToString();
        string country = other.getDropdownsbyType("gskcountry").Rows[0]["value"].ToString();
        string tel = other.getDropdownsbyType("gsktel").Rows[0]["value"].ToString();
        string fax = other.getDropdownsbyType("gskfax").Rows[0]["value"].ToString();
        string website = other.getDropdownsbyType("gskwebsite").Rows[0]["value"].ToString();

        lbl_name1.Text = name1 + "</br>";
        lbl_limited.Text = limited + "</br>";
        lbl_gsk.Text = name2 + "</br>" + address + "</br>" + city + "-" + pin + "</br>" + state + ", " + country + "</br>" + "Tel: " + tel + "</br>" + "Fax: " + fax + "</br>";
        hl_website.Text = website;
        hl_website.NavigateUrl = "http://" + website;

        lbl_gsk0.Text = address + "</br>" + city + "-" + pin + "</br>" + state + ", " + country;
    }

    private void load_lab()
    {
        lbl_date.Text = DateTime.Now.ToString("dd/MM/yyyy");
        lbl_refno.Text = projects.getProjectByPrjid(requests.getRequestbyReqid(reqid)[0].projectid)[0].projectname.Substring(0, 3) + reqid;  //First three letters of project name + reqid

        ds_analytics.m_labsRow lab_row = m_labs.getLabByLabid(labid)[0];
        lbl_lab.Text = "To," + "</br>" + lab_row.contact_person + ",</br>" + lab_row.labname + ",</br>" + lab_row.address + ",</br>" + lab_row.city + ",</br>" + "Phone: " + lab_row.phn1 + ",</br>" + "Email: " + lab_row.email1;

        lbl_labmanager.Text = lab_row.contact_person;        
    }

    private void load_sample_grid()
    {
        DataTable req_sampleInLab_dt = labresult.getDistinctSamplesInaReqToaLab(reqid, labid);
        gv_samples.DataSource = req_sampleInLab_dt;
        gv_samples.DataBind();
    }

    private void load_sender()
    {
        ds_analytics.m_usersRow user_row = m_users.getUserByUserid(userid)[0];
        lbl_sender.Text = user_row.fullname + "</br>" + "Analytical Sciences," + "</br>" + "Mob: " + user_row.mblno + "</br>" + "Email: " + user_row.email;
    }
    
    protected void gv_samples_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)        
        { 
            string sampleid = gv_samples.DataKeys[e.Row.RowIndex].Value.ToString();
            e.Row.Cells[1].Text = sample_pvalue.getPvalueBySampleIDandPropertyID(sampleid, 1);
            e.Row.Cells[2].Text = sample_pvalue.getPvalueBySampleIDandPropertyID(sampleid, 2);
            e.Row.Cells[3].Text = lab_sample_quantity.getQuantityBySampleIDLabID(labid, sampleid);

            DataTable dt_sampletestresult = labresult.getSampleTestResultBySampleIDLabID(sampleid, labid);
            string testname = "";
            string mth_ref = "";
            foreach (DataRow dr in dt_sampletestresult.Rows)
            {
                testname += dr["testname"].ToString() + "</br>";
                mth_ref += dr["mth_ref"].ToString() + "</br>";
            }
            e.Row.Cells[5].Text = testname;
            e.Row.Cells[6].Text = mth_ref;
        }
    }
}