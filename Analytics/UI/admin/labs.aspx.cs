using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class UI_admin_labs : System.Web.UI.Page
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
        if (!IsPostBack)
        {
            ((MenuItem)((Menu)Master.FindControl("Menu1")).FindItem("Labs")).Selected = true;
            ViewState["SortOrder"] = " ASC";

            ds_analytics.m_labsDataTable lab_dt = m_labs.getAllLabs();
            gv_labs.DataSource = lab_dt;
            gv_labs.DataBind();
            ViewState["dtStored"] = lab_dt;

            btn_add.Visible = true;
        }
        else
        {
 
        }
    }

    protected void gv_labs_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.CssClass = "row";
            if (e.Row.Cells[16].Text == "True")
            {
                e.Row.Cells[16].Text = "Active";
            }
            else
            {
                e.Row.Cells[16].Text = "Non-Active";
            }
        }
    }

    protected void gv_labs_Sorting(object sender, GridViewSortEventArgs e)
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

    protected void btn_search_Click(object sender, EventArgs e)
    {
        ds_analytics.m_labsDataTable lab_dt = m_labs.getAllLabsByName(tb_labname_search.Text);
        gv_labs.DataSource = lab_dt;
        gv_labs.DataBind();

        ViewState["dtStored"] = lab_dt;
    }

    protected void btn_add_Click(object sender, EventArgs e)
    {
        try
        {
            ds_analytics.m_labsDataTable lab_dt = new ds_analytics.m_labsDataTable();
            ds_analytics.m_labsRow lab_row = lab_dt.Newm_labsRow();

            lab_row.labname = tb_labname.Text;
            lab_row.address = tb_address.Text;
            lab_row.city = tb_city.Text;
            lab_row.labtype = tb_type.Text;
            lab_row.auditstatus = tb_auditstatus.Text;
            lab_row.fax = tb_fax.Text;
            lab_row.contact_person = tb_cp1.Text;
            lab_row.key_acc_person = tb_cp2.Text;
            lab_row.email1 = tb_email1.Text;
            lab_row.email2 = tb_email2.Text;
            lab_row.mbl1 = tb_mbl1.Text;
            lab_row.mbl2 = tb_mbl2.Text;
            lab_row.phn1 = tb_phn1.Text;
            lab_row.phn2 = tb_phn2.Text;
            lab_row.isactive = cb_isactive.Checked;

            lab_row = m_labs.GetLabRowWithNull(lab_row);
            m_labs.insert(lab_row);

            string url = "labs.aspx";
            ClientScript.RegisterStartupScript(this.GetType(), "callfunction", "alert('Record Added Successfully.');window.location.href = '" + url + "';", true);
        }
        catch(Exception ex)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "error", "<script>alert('Error in adding record.');</script>");
        }
    }

    protected void imgbtn_Edit_Click(object sender, ImageClickEventArgs e)
    {       
        GridViewRow gv_row = (GridViewRow)((sender as ImageButton).NamingContainer);
        long labid = Convert.ToInt64(gv_labs.DataKeys[gv_row.RowIndex].Value);

        ds_analytics.m_labsDataTable thislab_dt = m_labs.getLabByLabid(labid);
        if (thislab_dt.Rows.Count > 0)
        {
            ds_analytics.m_labsRow lab_row = thislab_dt[0];

            tb_labname.Text = lab_row.labname;
            tb_address.Text = lab_row.address;
            tb_city.Text = lab_row.city;
            tb_type.Text = lab_row.labtype;
            tb_auditstatus.Text = lab_row.auditstatus;
            tb_fax.Text = lab_row.fax;
            tb_cp1.Text = lab_row.contact_person;
            tb_cp2.Text = lab_row.key_acc_person;
            tb_email1.Text = lab_row.email1;
            tb_email2.Text = lab_row.email2;
            tb_mbl1.Text = lab_row.mbl1;
            tb_mbl2.Text = lab_row.mbl2;
            tb_phn1.Text = lab_row.phn1;
            tb_phn2.Text = lab_row.phn2;
            cb_isactive.Checked = lab_row.isactive;

            ViewState["lab_row"] = lab_row;

            btn_add.Visible = false;
            btn_update.Visible = true;
            btn_cancel.Visible = true;
        }
        else
        {
            ClientScript.RegisterStartupScript(this.GetType(), "error", "<script>alert('Error in getting Lab details.');</script>");
        }
    }
        
    protected void btn_update_Click(object sender, EventArgs e)
    {
        ds_analytics.m_labsRow lab_row = ((ds_analytics.m_labsRow)(ViewState["lab_row"]));
        lab_row.labname = tb_labname.Text;
        lab_row.address = tb_address.Text;
        lab_row.city = tb_city.Text;
        lab_row.labtype = tb_type.Text;
        lab_row.auditstatus = tb_auditstatus.Text;
        lab_row.fax = tb_fax.Text;
        lab_row.contact_person = tb_cp1.Text;
        lab_row.key_acc_person = tb_cp2.Text;
        lab_row.email1 = tb_email1.Text;
        lab_row.email2 = tb_email2.Text;
        lab_row.mbl1 = tb_mbl1.Text;
        lab_row.mbl2 = tb_mbl2.Text;
        lab_row.phn1 = tb_phn1.Text;
        lab_row.phn2 = tb_phn2.Text;
        lab_row.isactive = cb_isactive.Checked;

        m_labs.update(m_labs.GetLabRowWithNull(lab_row));

        ds_analytics.m_labsDataTable lab_dt = m_labs.getAllLabs();
        gv_labs.DataSource = lab_dt;
        gv_labs.DataBind();
        ViewState["dtStored"] = lab_dt;

        Reset();
    }
    
    protected void btn_cancel_Click(object sender, EventArgs e)
    {
        Reset();
    }

    private void Reset()
    {
        tb_labname.Text = "";
        tb_address.Text = "";
        tb_city.Text = "";
        tb_type.Text = "";
        tb_auditstatus.Text = "";
        tb_fax.Text = "";
        tb_cp1.Text = "";
        tb_cp2.Text = "";
        tb_email1.Text = "";
        tb_email2.Text = "";
        tb_mbl1.Text = "";
        tb_mbl2.Text = "";
        tb_phn1.Text = "";
        tb_phn2.Text = "";
        cb_isactive.Checked = false;

        btn_add.Visible = true;
        btn_update.Visible = false;
        btn_cancel.Visible = false;
    }
}