using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class UI_admin_settings : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ((MenuItem)((Menu)Master.FindControl("Menu1")).FindItem("Settings")).Selected = true;

            load_backup();
            load_service();
            load_prj_type();
            load_prj_category();
            load_prj_brand();
            load_req_anatype();
            load_sample_stcond();
            load_test_ref();
        }
        else
        {
            
        }
    }    

    private void load_backup()
    {
        //email-id
        DataTable dt = other.getDropdownsbyType("backupemail");
        if (dt.Rows.Count > 0)
        {
            tb_emailid.Text = dt.Rows[0]["value"].ToString();
        }

        //email-name
        DataTable dt2 = other.getDropdownsbyType("backupname");
        if (dt2.Rows.Count > 0)
        {
            tb_emailname.Text = dt2.Rows[0]["value"].ToString();
        }
    }

    private void load_service()
    {
        //service-status
        DataTable dt = other.getDropdownsbyType("servicestatus");
        if (dt.Rows.Count > 0)
        {
            rbl_service_status.SelectedValue = dt.Rows[0]["value"].ToString();
        }

        //service-msg
        DataTable dt2 = other.getDropdownsbyType("serviceuptime");
        if (dt2.Rows.Count > 0)
        {
            tb_service_msg.Text = dt2.Rows[0]["value"].ToString();
        }
    }

    private void load_prj_type()
    {
        DataTable dt = other.getAllDropdownsbyType("typeproject");
        gv_prj_type.DataSource = dt;
        gv_prj_type.DataBind();
    }

    private void load_prj_category()
    {
        DataTable dt = other.getAllDropdownsbyType("categoryproject");
        gv_prj_category.DataSource = dt;
        gv_prj_category.DataBind();
    }

    private void load_prj_brand()
    {
        DataTable dt = other.getAllDropdownsbyType("brandproject");
        gv_prj_brand.DataSource = dt;
        gv_prj_brand.DataBind();
    }

    private void load_req_anatype()
    {
        DataTable dt = other.getAllDropdownsbyType("typeanalysis");
        gv_req_analtype.DataSource = dt;
        gv_req_analtype.DataBind();
    }

    private void load_sample_stcond()
    {
        DataTable dt = other.getAllDropdownsbyType("storagecondition");
        gv_sample_stcond.DataSource = dt;
        gv_sample_stcond.DataBind();
    }

    private void load_test_ref()
    {
        DataTable dt = other.getAllDropdownsbyType("reference");
        gv_test_ref.DataSource = dt;
        gv_test_ref.DataBind();
    }
    //////////////////////////////////    button clicks     //////////////////////////////


    protected void btn_backup_Click(object sender, EventArgs e)
    {
        //email-id
        DataTable dt = other.getDropdownsbyType("backupemail");
        if (dt.Rows.Count > 0)
        {
            DataRow dr = dt.Rows[0];
            dr["value"]=tb_emailid.Text;
            other.update(dr);
        }

        //email-name
        DataTable dt2 = other.getDropdownsbyType("backupname");
        if (dt2.Rows.Count > 0)
        {
            DataRow dr2 = dt2.Rows[0];
            dr2["value"] = tb_emailname.Text;
            other.update(dr2);
        }
        lbl_message.Visible = true;
    }
    protected void btn_service_Click(object sender, EventArgs e)
    {
        //service-status
        DataTable dt = other.getDropdownsbyType("servicestatus");
        if (dt.Rows.Count > 0)
        {
            DataRow dr = dt.Rows[0];
            dr["value"] = rbl_service_status.SelectedValue;
            other.update(dr);
        }

        //service-msg
        DataTable dt2 = other.getDropdownsbyType("serviceuptime");
        if (dt2.Rows.Count > 0)
        {
            DataRow dr2 = dt2.Rows[0];
            dr2["value"] = tb_service_msg.Text;
            other.update(dr2);
        }
        lbl_message.Visible = true;
    }    
    protected void gv_prj_type_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {       
        GridViewRow gv_row = (GridViewRow)((e.CommandSource as ImageButton).NamingContainer);        

        if (e.CommandName == "Editing")
        {
            gv_prj_type.EditIndex = gv_row.RowIndex;
            DataTable dt = other.getAllDropdownsbyType("typeproject");
            gv_prj_type.DataSource = dt;
            gv_prj_type.DataBind();
        }
        else if (e.CommandName == "Deleting")
        {
            long dd_id = Convert.ToInt64(gv_prj_type.DataKeys[gv_row.RowIndex].Value);
            other.delete(dd_id);
            DataTable dt = other.getAllDropdownsbyType("typeproject");
            gv_prj_type.DataSource = dt;
            gv_prj_type.DataBind();
        }
        else if (e.CommandName == "Updating")
        {
            long dd_id = Convert.ToInt64(gv_prj_type.DataKeys[gv_row.RowIndex].Value);
            ds_analytics.dropdownsDataTable dd_dt = other.getDDbyID(dd_id);
            ds_analytics.dropdownsRow dd_row = dd_dt[0];
            
            dd_row.type = "typeproject";
            dd_row.value = ((TextBox)gv_row.FindControl("tb_type_edit")).Text;
            dd_row.isactive = ((CheckBox)gv_row.FindControl("CheckBox1")).Checked;

            other.update(dd_row);
            gv_prj_type.EditIndex = -1;
            DataTable dt = other.getAllDropdownsbyType("typeproject");
            gv_prj_type.DataSource = dt;
            gv_prj_type.DataBind();
        }
        else if (e.CommandName == "Cancelling")
        {
            gv_prj_type.EditIndex = -1;
            DataTable dt = other.getAllDropdownsbyType("typeproject");
            gv_prj_type.DataSource = dt;
            gv_prj_type.DataBind();
        }
        else if (e.CommandName == "Adding")
        {
            string value = ((TextBox)gv_row.FindControl("tb_type_add")).Text;
            bool isactive = ((CheckBox)gv_row.FindControl("CheckBox1")).Checked;
            other.insert("typeproject", value, isactive);
            DataTable dt = other.getAllDropdownsbyType("typeproject");
            gv_prj_type.DataSource = dt;
            gv_prj_type.DataBind();
        }
    }
    protected void gv_prj_category_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow gv_row = (GridViewRow)((e.CommandSource as ImageButton).NamingContainer);

        if (e.CommandName == "Editing")
        {
            gv_prj_category.EditIndex = gv_row.RowIndex;
            DataTable dt = other.getAllDropdownsbyType("categoryproject");
            gv_prj_category.DataSource = dt;
            gv_prj_category.DataBind();
        }
        else if (e.CommandName == "Deleting")
        {
            long dd_id = Convert.ToInt64(gv_prj_category.DataKeys[gv_row.RowIndex].Value);
            other.delete(dd_id);
            DataTable dt = other.getAllDropdownsbyType("categoryproject");
            gv_prj_category.DataSource = dt;
            gv_prj_category.DataBind();
        }
        else if (e.CommandName == "Updating")
        {
            long dd_id = Convert.ToInt64(gv_prj_category.DataKeys[gv_row.RowIndex].Value);
            ds_analytics.dropdownsDataTable dd_dt = other.getDDbyID(dd_id);
            ds_analytics.dropdownsRow dd_row = dd_dt[0];

            dd_row.type = "categoryproject";
            dd_row.value = ((TextBox)gv_row.FindControl("tb_value")).Text;
            dd_row.isactive = ((CheckBox)gv_row.FindControl("CheckBox1")).Checked;

            other.update(dd_row);
            gv_prj_category.EditIndex = -1;
            DataTable dt = other.getAllDropdownsbyType("categoryproject");
            gv_prj_category.DataSource = dt;
            gv_prj_category.DataBind();
        }
        else if (e.CommandName == "Cancelling")
        {
            gv_prj_category.EditIndex = -1;
            DataTable dt = other.getAllDropdownsbyType("categoryproject");
            gv_prj_category.DataSource = dt;
            gv_prj_category.DataBind();
        }
        else if (e.CommandName == "Adding")
        {
            string value = ((TextBox)gv_row.FindControl("tb_value")).Text;
            bool isactive = ((CheckBox)gv_row.FindControl("CheckBox1")).Checked;
            other.insert("categoryproject", value, isactive);
            DataTable dt = other.getAllDropdownsbyType("categoryproject");
            gv_prj_category.DataSource = dt;
            gv_prj_category.DataBind();
        }
    }
    protected void gv_prj_brand_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow gv_row = (GridViewRow)((e.CommandSource as ImageButton).NamingContainer);

        if (e.CommandName == "Editing")
        {
            gv_prj_brand.EditIndex = gv_row.RowIndex;
            DataTable dt = other.getAllDropdownsbyType("brandproject");
            gv_prj_brand.DataSource = dt;
            gv_prj_brand.DataBind();
        }
        else if (e.CommandName == "Deleting")
        {
            long dd_id = Convert.ToInt64(gv_prj_brand.DataKeys[gv_row.RowIndex].Value);
            other.delete(dd_id);
            DataTable dt = other.getAllDropdownsbyType("brandproject");
            gv_prj_brand.DataSource = dt;
            gv_prj_brand.DataBind();
        }
        else if (e.CommandName == "Updating")
        {
            long dd_id = Convert.ToInt64(gv_prj_brand.DataKeys[gv_row.RowIndex].Value);
            ds_analytics.dropdownsDataTable dd_dt = other.getDDbyID(dd_id);
            ds_analytics.dropdownsRow dd_row = dd_dt[0];

            dd_row.type = "brandproject";
            dd_row.value = ((TextBox)gv_row.FindControl("tb_value")).Text;
            dd_row.isactive = ((CheckBox)gv_row.FindControl("CheckBox1")).Checked;

            other.update(dd_row);
            gv_prj_brand.EditIndex = -1;
            DataTable dt = other.getAllDropdownsbyType("brandproject");
            gv_prj_brand.DataSource = dt;
            gv_prj_brand.DataBind();
        }
        else if (e.CommandName == "Cancelling")
        {
            gv_prj_brand.EditIndex = -1;
            DataTable dt = other.getAllDropdownsbyType("brandproject");
            gv_prj_brand.DataSource = dt;
            gv_prj_brand.DataBind();
        }
        else if (e.CommandName == "Adding")
        {
            string value = ((TextBox)gv_row.FindControl("tb_value")).Text;
            bool isactive = ((CheckBox)gv_row.FindControl("CheckBox1")).Checked;
            other.insert("brandproject", value, isactive);
            DataTable dt = other.getAllDropdownsbyType("brandproject");
            gv_prj_brand.DataSource = dt;
            gv_prj_brand.DataBind();
        }
    }
    protected void gv_req_analtype_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow gv_row = (GridViewRow)((e.CommandSource as ImageButton).NamingContainer);

        if (e.CommandName == "Editing")
        {
            gv_req_analtype.EditIndex = gv_row.RowIndex;
            DataTable dt = other.getAllDropdownsbyType("typeanalysis");
            gv_req_analtype.DataSource = dt;
            gv_req_analtype.DataBind();
        }
        else if (e.CommandName == "Deleting")
        {
            long dd_id = Convert.ToInt64(gv_req_analtype.DataKeys[gv_row.RowIndex].Value);
            other.delete(dd_id);
            DataTable dt = other.getAllDropdownsbyType("typeanalysis");
            gv_req_analtype.DataSource = dt;
            gv_req_analtype.DataBind();
        }
        else if (e.CommandName == "Updating")
        {
            long dd_id = Convert.ToInt64(gv_req_analtype.DataKeys[gv_row.RowIndex].Value);
            ds_analytics.dropdownsDataTable dd_dt = other.getDDbyID(dd_id);
            ds_analytics.dropdownsRow dd_row = dd_dt[0];

            dd_row.type = "typeanalysis";
            dd_row.value = ((TextBox)gv_row.FindControl("tb_value")).Text;
            dd_row.isactive = ((CheckBox)gv_row.FindControl("CheckBox1")).Checked;

            other.update(dd_row);
            gv_req_analtype.EditIndex = -1;
            DataTable dt = other.getAllDropdownsbyType("typeanalysis");
            gv_req_analtype.DataSource = dt;
            gv_req_analtype.DataBind();
        }
        else if (e.CommandName == "Cancelling")
        {
            gv_req_analtype.EditIndex = -1;
            DataTable dt = other.getAllDropdownsbyType("typeanalysis");
            gv_req_analtype.DataSource = dt;
            gv_req_analtype.DataBind();
        }
        else if (e.CommandName == "Adding")
        {
            string value = ((TextBox)gv_row.FindControl("tb_value")).Text;
            bool isactive = ((CheckBox)gv_row.FindControl("CheckBox1")).Checked;
            other.insert("typeanalysis", value, isactive);
            DataTable dt = other.getAllDropdownsbyType("typeanalysis");
            gv_req_analtype.DataSource = dt;
            gv_req_analtype.DataBind();
        }
    }
    protected void gv_sample_stcond_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow gv_row = (GridViewRow)((e.CommandSource as ImageButton).NamingContainer);

        if (e.CommandName == "Editing")
        {
            gv_sample_stcond.EditIndex = gv_row.RowIndex;
            DataTable dt = other.getAllDropdownsbyType("storagecondition");
            gv_sample_stcond.DataSource = dt;
            gv_sample_stcond.DataBind();
        }
        else if (e.CommandName == "Deleting")
        {
            long dd_id = Convert.ToInt64(gv_sample_stcond.DataKeys[gv_row.RowIndex].Value);
            other.delete(dd_id);
            DataTable dt = other.getAllDropdownsbyType("storagecondition");
            gv_sample_stcond.DataSource = dt;
            gv_sample_stcond.DataBind();
        }
        else if (e.CommandName == "Updating")
        {
            long dd_id = Convert.ToInt64(gv_sample_stcond.DataKeys[gv_row.RowIndex].Value);
            ds_analytics.dropdownsDataTable dd_dt = other.getDDbyID(dd_id);
            ds_analytics.dropdownsRow dd_row = dd_dt[0];

            dd_row.type = "storagecondition";
            dd_row.value = ((TextBox)gv_row.FindControl("tb_value")).Text;
            dd_row.isactive = ((CheckBox)gv_row.FindControl("CheckBox1")).Checked;

            other.update(dd_row);
            gv_sample_stcond.EditIndex = -1;
            DataTable dt = other.getAllDropdownsbyType("storagecondition");
            gv_sample_stcond.DataSource = dt;
            gv_sample_stcond.DataBind();
        }
        else if (e.CommandName == "Cancelling")
        {
            gv_sample_stcond.EditIndex = -1;
            DataTable dt = other.getAllDropdownsbyType("storagecondition");
            gv_sample_stcond.DataSource = dt;
            gv_sample_stcond.DataBind();
        }
        else if (e.CommandName == "Adding")
        {
            string value = ((TextBox)gv_row.FindControl("tb_value")).Text;
            bool isactive = ((CheckBox)gv_row.FindControl("CheckBox1")).Checked;
            other.insert("storagecondition", value, isactive);
            DataTable dt = other.getAllDropdownsbyType("storagecondition");
            gv_sample_stcond.DataSource = dt;
            gv_sample_stcond.DataBind();
        }
    }
    protected void gv_test_ref_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow gv_row = (GridViewRow)((e.CommandSource as ImageButton).NamingContainer);

        if (e.CommandName == "Editing")
        {
            gv_test_ref.EditIndex = gv_row.RowIndex;
            DataTable dt = other.getAllDropdownsbyType("reference");
            gv_test_ref.DataSource = dt;
            gv_test_ref.DataBind();
        }
        else if (e.CommandName == "Deleting")
        {
            long dd_id = Convert.ToInt64(gv_test_ref.DataKeys[gv_row.RowIndex].Value);
            other.delete(dd_id);
            DataTable dt = other.getAllDropdownsbyType("reference");
            gv_test_ref.DataSource = dt;
            gv_test_ref.DataBind();
        }
        else if (e.CommandName == "Updating")
        {
            long dd_id = Convert.ToInt64(gv_test_ref.DataKeys[gv_row.RowIndex].Value);
            ds_analytics.dropdownsDataTable dd_dt = other.getDDbyID(dd_id);
            ds_analytics.dropdownsRow dd_row = dd_dt[0];

            dd_row.type = "reference";
            dd_row.value = ((TextBox)gv_row.FindControl("tb_value")).Text;
            dd_row.isactive = ((CheckBox)gv_row.FindControl("CheckBox1")).Checked;

            other.update(dd_row);
            gv_test_ref.EditIndex = -1;
            DataTable dt = other.getAllDropdownsbyType("reference");
            gv_test_ref.DataSource = dt;
            gv_test_ref.DataBind();
        }
        else if (e.CommandName == "Cancelling")
        {
            gv_test_ref.EditIndex = -1;
            DataTable dt = other.getAllDropdownsbyType("reference");
            gv_test_ref.DataSource = dt;
            gv_test_ref.DataBind();
        }
        else if (e.CommandName == "Adding")
        {
            string value = ((TextBox)gv_row.FindControl("tb_value")).Text;
            bool isactive = ((CheckBox)gv_row.FindControl("CheckBox1")).Checked;
            other.insert("reference", value, isactive);
            DataTable dt = other.getAllDropdownsbyType("reference");
            gv_test_ref.DataSource = dt;
            gv_test_ref.DataBind();
        }
    }
    protected void Timer1_Tick(object sender, EventArgs e)
    {
        lbl_message.Visible = false;
    }
}