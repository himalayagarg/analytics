using System;
using System.Data;
using ds_analyticsTableAdapters;

/// <summary>
/// Summary description for projects
/// </summary>
public class projects
{
	public projects()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    private static projectsTableAdapter _projects = null;
    public static projectsTableAdapter Adapter
    {
        get
        {
            if (_projects == null)
                _projects = new projectsTableAdapter();

            return _projects;
        }
    }

    public static ds_analytics.projectsDataTable getAllProjects()
    {
        return Adapter.GetData();
    }

    public static ds_analytics.projectsDataTable getAllActiceProjects()
    {
        return Adapter.GetAllActivePojects();
    }

    public static DataTable getAllActiveProjectsNotCompleted()
    {
        //Request can be made against those projects whose completion date is either NULL or not past

        ds_analytics.projectsDataTable prj_dt = getAllActiceProjects();
        DataTable prj_dt_notcompleted = prj_dt.Clone();
        foreach (ds_analytics.projectsRow prj_row in prj_dt.Rows)
        {
            if(prj_row.IscompletiondateNull() || DateTime.Compare(prj_row.completiondate,DateTime.Now)>=0)
            {
                prj_dt_notcompleted.ImportRow(prj_row);
            }
        }
        return prj_dt_notcompleted;
    }

    public static ds_analytics.projectsDataTable getAllProjectsByName(string projectname)
    {
        return Adapter.GetAllProjectsByName(projectname);
    }

    public static ds_analytics.projectsDataTable getProjectByPrjid(long projectid)
    {
        return Adapter.GetProjectByProjID(projectid);
    }

    public static string getProjectNameByProjectID(long projectid)
    {
        ds_analytics.projectsDataTable prj_dt = getProjectByPrjid(projectid);
        ds_analytics.projectsRow prj_row = prj_dt[0];
        return prj_row.projectname;
    }    

    public static int insert(ds_analytics.projectsRow prj_row)
    {
        return  Adapter.Insert(prj_row.projectname,
                prj_row.projecttype,
                prj_row.projectcategory,
                prj_row.projectbrand,
                prj_row.budget,
                prj_row.createdate,
                prj_row.completiondate,
                prj_row.isactive);
    }

    public static int delete(long prj_id)
    {
        return Adapter.Delete(prj_id);
    }

    public static int update(ds_analytics.projectsRow prj_row)
    {
        return Adapter.Update(prj_row);
    }
}