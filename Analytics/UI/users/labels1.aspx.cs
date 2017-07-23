using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class UI_users_labels1 : System.Web.UI.Page
{
    string reqid;
    long labid;
    string req_reference;

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
        req_reference = projects.getProjectByPrjid(requests.getRequestbyReqid(reqid)[0].projectid)[0].projectname.Substring(0,3) + reqid;  //First three letters of project name + reqid

        if (!IsPostBack)
        {
            DataTable dt_SampleTestLab = labresult.getSampleTestLabByJoining(reqid);
            //1 - Filter for only selected lab
            DataRow[] d_rows = dt_SampleTestLab.Select("labid = '"+labid.ToString()+"'");
            DataTable dt_SampleTestLab_filtered = dt_SampleTestLab.Clone();
            foreach (DataRow dr in d_rows)
            {
                dt_SampleTestLab_filtered.ImportRow(dr);
            }

            //2 - Finding Unique Samples
            DataView view = new DataView(dt_SampleTestLab_filtered);
            dt_SampleTestLab_filtered = view.ToTable(true, "sampleid", "sample_quantity");

            //3 - Bind Repeater
            Repeater1.DataSource = dt_SampleTestLab_filtered;
            Repeater1.DataBind();

            //4 - Bind Lab Address etc.
            ds_analytics.m_labsRow lab_row = m_labs.getLabByLabid(labid)[0];
            lbl_name1.Text = lab_row.contact_person;
            lbl_name2.Text = lab_row.key_acc_person;
            lbl_lab_name.Text = lab_row.labname;
            lbl_lab_add.Text = lab_row.address;
            lbl_lab_city.Text = lab_row.city;
            lbl_email1.Text = lab_row.email1;
            lbl_mbl1.Text = lab_row.mbl1;
            lbl_phn1.Text = lab_row.phn1;

            //5 - Auto Print Pop-up
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Print", "javascript:window.print();", true);
        }

    }
    
    protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            ((Label)(e.Item.FindControl("lbl_reqref1"))).Text = req_reference;
            ((Label)(e.Item.FindControl("lbl_reqref2"))).Text = req_reference;
            ((Label)(e.Item.FindControl("lbl_reqref3"))).Text = req_reference;
        }
    }
}