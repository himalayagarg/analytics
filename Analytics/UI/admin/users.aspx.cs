using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class UI_admin_users : System.Web.UI.Page
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
            ((MenuItem)((Menu)Master.FindControl("Menu1")).FindItem("Users")).Selected = true;
            ViewState["SortOrder"] = " ASC";

            ds_analytics.m_usersDataTable users_dt = m_users.getAllUsers();
            bind_gv_users(users_dt);
            ViewState["dtStored"] = users_dt;

            CreateTreeView();
        }
        else
        {

        }
    }

    private void CreateTreeView()
    {
        DataTable dt_mod = module_submodule.getAllDistinctModules();
        foreach (DataRow dr in dt_mod.Rows)
        {
            TreeNode node_parent = new TreeNode(dr["module_name"].ToString(),dr["module_key"].ToString());
            TreeView1.Nodes.Add(node_parent);   //Module as Parent
            DataTable dt_submod = module_submodule.getAllDistinctSubModulesByModules(dr["module_name"].ToString());
            foreach (DataRow dr_sum in dt_submod.Rows)
            {
                TreeNode node_child = new TreeNode(dr_sum["submodule_name"].ToString(), dr_sum["submodule_key"].ToString());
                node_child.ShowCheckBox = true;
                node_parent.ChildNodes.Add(node_child); //SubModule as Child
            }
        }
    }    
    protected void btn_update_Click(object sender, EventArgs e)
    {
        ds_analytics.m_usersRow users_row = ((ds_analytics.m_usersRow)(ViewState["user_row"]));
        //1 User Update
        users_row.fullname = tb_username.Text;
        if (tb_email.Text == "") users_row.SetemailNull(); else users_row.email = tb_email.Text;
        if (tb_mblno.Text == "") users_row.SetmblnoNull(); else users_row.mblno = tb_mblno.Text;
        users_row.isactive = cb_isactive.Checked;
        m_users.update(users_row);

        //2 User_Rights Update        
        m_users_rights.delete(users_row.userid);
        foreach (TreeNode node_parent in TreeView1.Nodes)
        {
            foreach (TreeNode node_child in node_parent.ChildNodes)
            {
                if(node_child.Checked == true)
                {
                    m_users_rights.insert(users_row.userid, Convert.ToInt32(node_child.Value));             
                }
            }            
        }

        //Rebind GridView
        ds_analytics.m_usersDataTable users_dt = m_users.getAllUsers();
        bind_gv_users(users_dt);
        ViewState["dtStored"] = users_dt;

        //Reset Fields
        Reset();
        ClientScript.RegisterStartupScript(this.GetType(), "callfunction", "alert('User Updated.');", true);
    }
    protected void btn_cancel_Click(object sender, EventArgs e)
    {
        Reset();
    }

    protected void btn_search_Click(object sender, EventArgs e)
    {
        ds_analytics.m_usersDataTable users_dt = m_users.getAllUsersByName(tb_username_search.Text);
        bind_gv_users(users_dt);

        ViewState["dtStored"] = users_dt;
    }
    protected void btn_add_Click(object sender, EventArgs e)
    {
        DataTable dt_user = m_users.getUserByUserid(tb_add_username.Text);
        if (dt_user.Rows.Count > 0)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "callfunction", "alert('This ID already exist.');", true);
        }
        else
        {
            string msg = "Problem in adding user.";    //msg contains the exception genrated by the function below.
            bool user_added = m_users.AddUser(tb_add_username.Text, ref msg);
            if (user_added)
            {
                //Rebind GridView
                ds_analytics.m_usersDataTable users_dt = m_users.getAllUsers();
                bind_gv_users(users_dt);
                ViewState["dtStored"] = users_dt;

                ClientScript.RegisterStartupScript(this.GetType(), "callfunction", "alert('User Added Successfully.');", true);
                mpopup_1.Show();                
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "callfunction", "alert('"+msg+"');", true);
            }
        }
    }

    protected void btn_modalok_Click(object sender, EventArgs e)
    {
        ImageButton btn_edit_grid = ((ImageButton)gv_users.Rows[0].FindControl("imgbtn_Edit"));
        imgbtn_Edit_Click(btn_edit_grid, null);        
    }

    protected void imgbtn_Edit_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gv_row = (GridViewRow)((sender as ImageButton).NamingContainer);
        string userid = Convert.ToString(gv_users.DataKeys[gv_row.RowIndex].Value);

        ds_analytics.m_usersDataTable thisuser_dt = m_users.getUserByUserid(userid);
        if (thisuser_dt.Rows.Count > 0)
        {
            ds_analytics.m_usersRow user_row = thisuser_dt[0];
            lbl_userid.Text = user_row.userid;
            if (user_row.IsfullnameNull()) tb_username.Text = ""; else tb_username.Text = user_row.fullname;
            if (user_row.IsemailNull()) tb_email.Text = ""; else tb_email.Text = user_row.email;
            if (user_row.IsmblnoNull()) tb_mblno.Text = ""; else tb_mblno.Text = user_row.mblno;
            cb_isactive.Checked = user_row.isactive;

            foreach (TreeNode node_parent in TreeView1.Nodes)
            {
                foreach (TreeNode node_child in node_parent.ChildNodes)
                {
                    node_child.Checked = false;
                }
            }

            DataTable dt_rights = m_users.getAccessRightsByUserid(user_row.userid);
            foreach (DataRow dr in dt_rights.Rows)
            {
                TreeView1.FindNode(dr["module_key"].ToString() + TreeView1.PathSeparator + dr["submodule_key"].ToString()).Checked = true;
            }
            
            ViewState["user_row"] = user_row;
            
            btn_update.Visible = true;
            btn_cancel.Visible = true;
        }
        else
        {
            ClientScript.RegisterStartupScript(this.GetType(), "error", "<script>alert('Error in reading user details.');</script>");
        }        
    }

    protected void imgbtn_Delete_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gv_row = (GridViewRow)((sender as ImageButton).NamingContainer);
        string userid = Convert.ToString(gv_users.DataKeys[gv_row.RowIndex].Value);
        int reqby = requests.getAllRequestsbyReqfrom(userid).Rows.Count;
        int reqto = requests.getAllRequestsbyResponsible(userid).Rows.Count;
        if (reqby == 0 && reqto == 0)
        {
            m_users.delete(userid);
            ds_analytics.m_usersDataTable users_dt = m_users.getAllUsers();
            bind_gv_users(users_dt);
            ViewState["dtStored"] = users_dt;
            ClientScript.RegisterStartupScript(this.GetType(), "error", "<script>alert('User deleted Successfully.');</script>");
        }
        else
        {
            ClientScript.RegisterStartupScript(this.GetType(), "error", "<script>alert('User cannot be deleted as some requests exists related to the user.');</script>");
        }
    }

    protected void gv_users_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.CssClass = "row";
            if (e.Row.Cells[5].Text == "True")
            {
                e.Row.Cells[5].Text = "Active";
            }
            else
            {
                e.Row.Cells[5].Text = "Non-Active";
            }
        }
    }
    protected void gv_users_Sorting(object sender, GridViewSortEventArgs e)
    {
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
        bind_gv_users(dt);
        ViewState["dtStored"] = dt;
    }

    private void bind_gv_users(DataTable dt)
    {
        //Delete superuser row temporarily from the DataTable
        DataRow[] dr_superuser = dt.Select("fullname='superuser'");
        foreach (DataRow dr in dr_superuser)
        {
            dr.Delete();
        }

        //Bind
        gv_users.DataSource = dt;
        gv_users.DataBind();
    }

    private void Reset()
    {
        lbl_userid.Text = "";
        tb_username.Text = "";
        tb_email.Text = "";
        tb_mblno.Text = "";
        cb_isactive.Checked = false;

        foreach (TreeNode node_parent in TreeView1.Nodes)
        {
            foreach (TreeNode node_child in node_parent.ChildNodes)
            {
                node_child.Checked = false;
            }
        }
        
        btn_update.Visible = false;
        btn_cancel.Visible = false;
    }    
}