using System;
using System.Data;
using ds_analyticsTableAdapters;
using System.Collections;
using System.Data.SqlClient;

/// <summary>
/// Summary description for module_submodule
/// </summary>
public class module_submodule
{
	public module_submodule()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    private static module_submoduleTableAdapter _mod_submod = null;
    public static module_submoduleTableAdapter Adapter
    {
        get
        {
            if (_mod_submod == null)
                _mod_submod = new module_submoduleTableAdapter();

            return _mod_submod;
        }
    }

    public static ds_analytics.module_submoduleDataTable getAllModule_Submodule()
    {
        return Adapter.GetData();
    }

    /// <summary>
    /// Return All Modules list in Software(e.g. "Requests", "Admin")
    /// </summary>    
    public static DataTable getAllDistinctModules()
    {        
        DataTable dt_all_mod_submod = getAllModule_Submodule();
        DataView view = new DataView(dt_all_mod_submod);
        DataTable distinctModules = view.ToTable(true, "module_name","module_key");
        return distinctModules;
    }

    /// <summary>
    /// Return All SubModules list by Modules in the Software(e.g. "Requestor" in "Requests")
    /// </summary>    
    public static DataTable getAllDistinctSubModulesByModules(string module_name)
    {
        DataTable dt_all_mod_submod = getAllModule_Submodule();
        DataTable dt_clone = dt_all_mod_submod.Clone();
        DataRow[] dt_filter = dt_all_mod_submod.Select("module_name='"+module_name+"'");
        foreach (DataRow dr in dt_filter)
        {
            dt_clone.ImportRow(dr);
        }
        DataView view = new DataView(dt_clone);        
        DataTable distinctSubModules = view.ToTable(true, "submodule_name", "submodule_key");
        return distinctSubModules;
    }
}