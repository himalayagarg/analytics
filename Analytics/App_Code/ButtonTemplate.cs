using System;
using System.Web.UI;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for ButtonTemplate
/// </summary>
public class ButtonTemplate : ITemplate, INamingContainer
{    
    private string _id;
    private string _text;
    private string _bindfield;

    public ButtonTemplate(string ID, string text, string bindfield)
	{        
        _id = ID;
        _text = text;
        _bindfield = bindfield;
	}

    public void InstantiateIn(Control container)
    {
        Button btn = new Button();
        btn.ID = _id;
        btn.Text = _text;
        btn.DataBinding += new EventHandler(btn_DataBinding);        
        container.Controls.Add(btn);
    }

    void btn_DataBinding(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        GridViewRow gvr = (GridViewRow)btn.NamingContainer;
        string bindValue = DataBinder.Eval(gvr.DataItem, _bindfield).ToString();
        if (bindValue.ToUpper() == "TRUE")
            btn.Visible = true;
        else
            btn.Visible  = false;
    }   
}