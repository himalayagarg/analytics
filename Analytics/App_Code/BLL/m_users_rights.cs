using System;
using System.Data;
using ds_analyticsTableAdapters;
using System.Collections;
using System.Data.SqlClient;

/// <summary>
/// Summary description for m_users_rights
/// </summary>
public class m_users_rights
{
	public m_users_rights()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    private static m_users_rightsTableAdapter _m_users_rights = null;
    public static m_users_rightsTableAdapter Adapter
    {
        get
        {
            if (_m_users_rights == null)
                _m_users_rights = new m_users_rightsTableAdapter();

            return _m_users_rights;
        }
    }

    public static ds_analytics.m_users_rightsDataTable getAllUserRights()
    {
        return Adapter.GetData();
    }

    /// <summary>
    /// Delete all rights of a particular user
    /// </summary>    
    public static int delete(string userid)
    {
        return Adapter.Delete(userid);
    }

    /// <summary>
    /// Insert user right for one submodule at a time
    /// </summary>    
    public static int insert(string userid, int submodule_key)
    {
        return Adapter.Insert(userid, submodule_key);
    }
}