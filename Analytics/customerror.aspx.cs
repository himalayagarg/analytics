using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class customerror : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string prevurl = Request.UrlReferrer.ToString();
            LinkButton1.PostBackUrl = prevurl;
        }
        string pre_url = Request.QueryString["aspxerrorpath"].ToString();
        Label2.Text = pre_url;

        //HttpContext ctx = HttpContext.Current;
        //Exception exception = ctx.Server.GetLastError();
        //Label1.Text = ctx.Request.Url.ToString();
        //Label2.Text = exception.Message;
        //ctx.Server.ClearError();
    }
}