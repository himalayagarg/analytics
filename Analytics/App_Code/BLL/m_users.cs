using System;
using System.Data;
using ds_analyticsTableAdapters;
using System.Collections;
using System.Data.SqlClient;

/// <summary>
/// Summary description for m_users
/// </summary>
public class m_users
{
	public m_users()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    private static m_usersTableAdapter _m_users = null;
    public static m_usersTableAdapter Adapter
    {
        get
        {
            if (_m_users == null)
                _m_users = new m_usersTableAdapter();

            return _m_users;
        }
    }

    public static ds_analytics.m_usersDataTable getAllUsers()
    {
        return Adapter.GetData();
    }

    public static int update(ds_analytics.m_usersRow user_row)
    {
        return Adapter.Update(user_row);
    }

    public static bool insert(string mudid, string fullname, string email, string mblno)
    {
        if (fullname == "")
        {
            throw new Exception("Name can not be null.");
        }

        int rowsAffected = Adapter.Insert(mudid, fullname, email, mblno, true, false, DateTime.Now);
        return rowsAffected == 1;
    }

    public static int delete(string userid)
    {
        return Adapter.Delete(userid);
    }

    public static ds_analytics.m_usersDataTable getAllUsersByName(string username)
    {
        return Adapter.GetAllUsersByName(username);
    }

    public static ds_analytics.m_usersDataTable getAllActiceReceivers()
    {
        ds_analytics.m_usersDataTable users_dt = Adapter.GetAllActiveReceivers();
        if (users_dt.Rows.Count > 0)
        {
            DataRow[] dr_superuser = users_dt.Select("fullname='superuser'");
            foreach (DataRow dr in dr_superuser)
            {
                dr.Delete();
            }
        }

        return users_dt;
    }

    public static ds_analytics.m_usersDataTable getUserByUserid(string userid)
    {
        return Adapter.GetUserByUserId(userid);
    }

    public static string getFullnameByuserid(string userid)
    {
        DataTable userdt = Adapter.GetUserByUserId(userid);
        return userdt.Rows[0]["fullname"].ToString();
    }

    #region AccessRights

    /// <summary>
    /// Return the access all rights of employee by joining the tables "m_user_rights" and "module_submodule"
    /// </summary>    
    public static DataTable getAccessRightsByUserid(string userid)
    {        
        string connString = System.Configuration.ConfigurationManager.ConnectionStrings["analyticsConnectionString"].ConnectionString;
        SqlConnection con = new SqlConnection(connString);
        DataTable dt = new DataTable();
        try
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Prc_GetAccessRightsByUserid", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@userid", userid);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            con.Close();
            return dt;
        }
        catch
        {
            con.Close();
            return dt;
        }        
    }

    /// <summary>
    /// Returns List which contains all unique modules that user have right to access (e.g. "Employee", "Admin")
    /// </summary>    
    public static ArrayList getModulesByUserid(string userid)
    {
        ArrayList arr_List_roles = new ArrayList();

        DataTable dt_rights = getAccessRightsByUserid(userid);
        foreach (DataRow dr in dt_rights.Rows)
        {
            if (!arr_List_roles.Contains(dr["module_name"].ToString()))
            {
                arr_List_roles.Add(dr["module_name"].ToString());
            }
        }
        return arr_List_roles;
    }

    /// <summary>
    /// Returns List which contains all unique sub-modules that user have right to access
    /// </summary>    
    public static ArrayList getSubModulesByUserid(string userid)
    {
        ArrayList arr_List_roles = new ArrayList();

        DataTable dt_rights = getAccessRightsByUserid(userid);
        foreach (DataRow dr in dt_rights.Rows)
        {
            if (!arr_List_roles.Contains(dr["submodule_name"].ToString()))
            {
                arr_List_roles.Add(dr["submodule_name"].ToString());
            }
        }
        return arr_List_roles;
    }

    #endregion   


    #region WebServices_Local

    /// <summary>
    /// Function-1 that uses web-service to authenticate user login
    /// </summary>
    /// <returns>Return True if login success from the Web-Service otherwise false</returns>
    public static bool authenticate(string mudid, string pswrd)
    {
        try
        {
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    /// <summary>
    /// Function-2 that uses web-service to GetUser DataSet
    /// </summary>      
    public static bool AddUser(string mudid, ref string msg)
    {
        string fullname = string.Empty;
        string email = string.Empty;
        string mblno = string.Empty;
        try
        {
            //call GetUser webservice here which return DatSet. Set above three fields from the dataset. Uncomment following lines
            //1. set displayname from web-service as fullname e.g. fullname = dataset.Tables[0].Rows[0].displayname;
            //2. set email also e.g. email = dataset.Tables[0].Rows[0].email;
            //3. set mblno also e.g. mblno = dataset.Tables[0].Rows[0].mblno;

            return m_users.insert(mudid, fullname, email, mblno);
        }
        catch (Exception ex)
        {
            msg = ex.Message;
            return false;
        }
    }

    #endregion


    //#region WebServices_Server

    ///// <summary>
    ///// Function-1 that uses web-service to authenticate user login
    ///// </summary>
    ///// <returns>Return True if login success from the Web-Service otherwise false</returns>
    //public static bool authenticate(string mudid, string pswrd)
    //{
    //    try
    //    {
    //        bool validate;
    //        analytics.SEDServices sed = new analytics.SEDServices();
    //        validate = sed.Authenticate(mudid, pswrd);
    //        return validate;
    //    }
    //    catch (Exception ex)
    //    {
    //        return false;
    //    }
    //}

    ///// <summary>
    ///// Function-2 that uses web-service to GetUser DataSet
    ///// </summary>      
    //public static bool AddUser(string mudid, ref string msg)
    //{
    //    string fullname = string.Empty;
    //    string email = string.Empty;
    //    string mblno = string.Empty;
    //    try
    //    {
    //        DataSet ds = new DataSet();
    //        //call GetUser webservice here which return DatSet. Set above three fields from the dataset. Uncomment following lines
    //        analytics.SEDServices sed = new analytics.SEDServices();
    //        ds = sed.GetUser(mudid.Trim());
    //        //1. set displayname from web-service as fullname e.g. 
    //        if (ds.Tables[0].Rows.Count > 15)
    //        {
    //            fullname = ds.Tables[0].Rows[15].ItemArray[1].ToString();
    //        }
    //        else
    //        {
    //            throw new ArgumentNullException("Name can not be Empty.");
    //        }
    //        //2. set email also e.g. 
    //        if (ds.Tables[0].Rows.Count > 8)
    //        {
    //            email = ds.Tables[0].Rows[8].ItemArray[1].ToString();
    //        }
    //        //3. set mblno also e.g. 
    //        if (ds.Tables[0].Rows.Count > 40)
    //        {
    //            mblno = ds.Tables[0].Rows[40].ItemArray[1].ToString();
    //        }
    //        ds.Dispose();
    //        return m_users.insert(mudid, fullname, email, mblno);
    //    }
    //    catch (Exception ex)
    //    {
    //        msg = ex.Message;
    //        return false;
    //    }
    //}

    //#endregion
}