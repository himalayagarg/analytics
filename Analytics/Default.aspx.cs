using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //1. check sitemaintain
        bool sitemaintain = false;
        DataTable dt_service = other.getDropdownsbyType("servicestatus");
        if (dt_service.Rows.Count > 0)
        {
            if (dt_service.Rows[0]["value"].ToString().ToLower() == "true")
            {
                sitemaintain = true;
            }
        }

        if (sitemaintain == true)
        {
            Response.Redirect("~/sitemaintain.aspx");
        }
        else
        {
            txtbx_uid.Focus();
            if (Page.ClientQueryString == "logout")
            {
                lbl_logout.Visible = true;
            }
        }

        //2. check JS
        string JStest = ((HiddenField)Master.FindControl("JStest")).Value;
        if (IsPostBack)
        {
            if (JStest == "no")
            {
                Response.Redirect("~/JS.aspx");
            }
        }
    }
    protected void btn_Login_Click(object sender, EventArgs e)
    {
        string errorText = string.Empty;
        string login_id = Request.QueryString.Get("login");
        string redirect = Request.QueryString.Get("redirect");
        if(txtbx_uid.Text=="superuser")
        {
            DataTable dt_superuser = other.getDropdownsbyType("superuser");
            if (dt_superuser.Rows.Count > 0)
            {
                if (dt_superuser.Rows[0]["value"].ToString() == txtbx_pass.Text)
                {
                    Session["userid"] = txtbx_uid.Text;
                    if (redirect == "" || redirect == null)
                    {
                        //user has came to this page directly, not through mail
                        Response.Redirect("~/UI/admin/home.aspx");
                    }
                    else
                    {
                        //user has came to this page through mail
                        if (login_id == txtbx_uid.Text)
                        {
                            Response.Redirect(redirect);
                        }
                        else
                        {
                            //mail receiver is different from the person doing login
                            errorText = "Link not intended for you.";
                        }
                    }
                }
                else
                {
                    errorText = "Wrong UserID or Password";
                }
            }
            else
            {
                errorText = "Wrong UserID or Password";
            }
        }

        else if (m_users.authenticate(txtbx_uid.Text, txtbx_pass.Text))
        {
            ds_analytics.m_usersDataTable user_dt = m_users.getUserByUserid(txtbx_uid.Text);
            if (user_dt.Rows.Count > 0)
            {
                ds_analytics.m_usersRow user_row = user_dt[0];
                if (user_row.isactive == true)
                {
                    user_row.isloggedin = true;
                    m_users.update(user_row);

                    Session["userid"] = txtbx_uid.Text;
                    if (m_users.getModulesByUserid(txtbx_uid.Text).Contains("Requests"))
                    {
                        if (redirect == "" || redirect == null)
                        {
                            Response.Redirect("~/UI/users/home.aspx");
                        }
                        else
                        {
                            if (login_id == txtbx_uid.Text)
                            {
                                Response.Redirect(redirect);
                            }
                            else
                            {
                                errorText = "Link not intended for you.";
                            }
                        }
                    }
                    else if (m_users.getModulesByUserid(txtbx_uid.Text).Contains("Admin"))
                    {
                        if (redirect == "" || redirect == null)
                        {
                            Response.Redirect("~/UI/admin/home.aspx");
                        }
                        else
                        {
                            if (login_id == txtbx_uid.Text)
                            {
                                Response.Redirect(redirect);
                            }
                            else
                            {
                                errorText = "Link not intended for you.";
                            }
                        }
                    }
                    else
                    {
                        errorText = "Problem in login as the access rights are not properly defined.";
                    }
                }
                else
                {
                    errorText = "User has been made inactive.";
                }
            }
            else
            {
                errorText = "Wrong UserID or Password";
            }
        }
        else
        {
            errorText = "Wrong UserID or Password";
        }
        if (errorText != string.Empty)
        {            
            ClientScript.RegisterStartupScript(this.GetType(), "error", "<script>alert('" + errorText + "');</script>");
        }
    }
}