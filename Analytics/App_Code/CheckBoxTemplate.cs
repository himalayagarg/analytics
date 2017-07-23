using System;
using System.Web.UI;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for CheckBoxTemplate
/// </summary>
public class CheckBoxTemplate : ITemplate, INamingContainer
{
    private ListItemType _type;
    private string _id;
    private string _bindfield;

    public CheckBoxTemplate(ListItemType type, string ID, string bindfield)
	{
        _type = type;
        _id = ID;
        _bindfield = bindfield;
	}
    public void InstantiateIn(Control container)
    {
        CheckBox chk = new CheckBox();
        chk.ID = _id;
        chk.DataBinding += new EventHandler(chk_DataBinding);
        switch (_type)
        {           
            case ListItemType.Item:
                chk.Enabled = false;
                container.Controls.Add(chk);
                break;
            case ListItemType.EditItem:                            
                chk.Enabled = true;
                container.Controls.Add(chk);
                break;
            default:
                break;
        }
    }

    private void chk_DataBinding(object sender, EventArgs e)
    {
        CheckBox cb = (CheckBox)sender;
        GridViewRow gvr = (GridViewRow)cb.NamingContainer;
        string bindValue = DataBinder.Eval(gvr.DataItem, _bindfield).ToString();
        if (bindValue.ToUpper() == "TRUE")
            cb.Checked = true;
        else
            cb.Checked = false;
    }
}