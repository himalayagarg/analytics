using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class UI_users_print_doc : System.Web.UI.Page
{
    string reqid;
    DataTable dt_sample_test = new DataTable();

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

        if (!IsPostBack)
        {
            //Fetching sample_test_lab data
            DataTable dt_SampleTestLab = labresult.getSampleTestLabByJoining(reqid);
            DataView dv = dt_SampleTestLab.DefaultView;
            string[] columnNames = {"labid","labname","email1","city","contact_person","mbl1"};
            DataTable dt_unique_labs = dv.ToTable(true, columnNames);
            Repeater1.DataSource = dt_unique_labs;
            Repeater1.DataBind();
        }
        else
        {
 
        }
    }
    protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Encryption64 e64 = new Encryption64();
            long labid = Convert.ToInt64(((Label)(e.Item.FindControl("lbl_labid"))).Text);
            ((HyperLink)(e.Item.FindControl("hl_cover_letter"))).NavigateUrl = "~/UI/users/cover_letter1.aspx?reqid=" + e64.Encrypt(reqid) + "&labid=" + e64.Encrypt(labid.ToString());
            ((HyperLink)(e.Item.FindControl("hl_challan"))).NavigateUrl = "~/UI/users/challan1.aspx?reqid=" + e64.Encrypt(reqid) + "&labid=" + e64.Encrypt(labid.ToString());
            ((HyperLink)(e.Item.FindControl("hl_labels"))).NavigateUrl = "~/UI/users/labels1.aspx?reqid=" + e64.Encrypt(reqid) + "&labid=" + e64.Encrypt(labid.ToString());
        }
    }
}