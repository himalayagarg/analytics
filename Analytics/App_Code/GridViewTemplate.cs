using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for GridViewTemplate 
/// </summary>
public class GridViewTemplate : ITemplate
{
    private DataControlRowType templateType;
    private string columnName;

    public GridViewTemplate(DataControlRowType type, string colname)
    {
        templateType = type;
        columnName = colname;
    }

    public void InstantiateIn(System.Web.UI.Control container)
    {
        // Create the content for the different row types.
        switch (templateType)
        {
            case DataControlRowType.Header:
                
                Literal lc = new Literal();
                lc.Text = "<b>" + columnName + "</b>";
                
                container.Controls.Add(lc);
                break;
            case DataControlRowType.DataRow:
                // Create the controls to put in a data row
                // section and set their properties.
                TextBox tb = new TextBox();                                

                // To support data binding, register the event-handling methods
                // to perform the data binding. Each control needs its own event
                // handler.
                

                // Add the controls to the Controls collection
                // of the container.
                container.Controls.Add(tb);                
                break;            

            default:
                // Insert code to handle unexpected values.
                break;
        }
    }    
}