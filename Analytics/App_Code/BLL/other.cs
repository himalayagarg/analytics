using System;
using System.Data;
using System.Data.SqlClient;
using ds_analyticsTableAdapters;

/// <summary>
/// Summary description for other
/// </summary>
public class other
{
    public other()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    #region dropdowns

    private static dropdownsTableAdapter _dropdowns = null;
    public static dropdownsTableAdapter Adapter_dd
    {
        get 
        {
            if (_dropdowns == null)
              _dropdowns = new dropdownsTableAdapter(); 

            return _dropdowns;
        }        
    }

    public static DataTable getDropdownsbyType(string type)
    {
        return Adapter_dd.GetDropDownsByType(type);
    }

    public static DataTable getAllDropdownsbyType(string type)
    {
        return Adapter_dd.GetAllDropDownsByType(type);
    }

    public static ds_analytics.dropdownsDataTable getDDbyID(long dd_id)
    {
        return Adapter_dd.GetDDbyID(dd_id);
    }

    public static int update(DataRow dd_row)
    {
        return Adapter_dd.Update(dd_row);
    }

    public static bool insert(string type, string value, bool isactive)
    {        
        int rowsAffected = Adapter_dd.Insert(type, value, isactive);
        return rowsAffected == 1;
    }

    public static int delete(long dd_id)
    {
        return Adapter_dd.Delete(dd_id);
    }

    #endregion

    #region m_properties

    private static m_propertiesTableAdapter _mproperties = null;
    public static m_propertiesTableAdapter Adapter_mprop
    {
        get
        {
            if (_mproperties == null)
                _mproperties = new m_propertiesTableAdapter();

            return _mproperties;
        }
    }	    

    public static DataTable getProperties()
    {
        return Adapter_mprop.GetProperties();
    }

    #endregion

    #region m_status

    private static m_statusTableAdapter _m_status = null;
    public static m_statusTableAdapter Adapter_status
    {
        get
        {
            if (_m_status == null)
                _m_status = new m_statusTableAdapter();

            return _m_status;
        }
    }

    public static string getStatustext(int statusid)
    {
       ds_analytics.m_statusDataTable st_dt = Adapter_status.GetStatusByStatusId(statusid);
       ds_analytics.m_statusRow st_row = st_dt[0];
       return st_row.statustext;
    }

    public static ds_analytics.m_statusDataTable getAllStatus()
    {
        return Adapter_status.GetData();
    }

    #endregion

    #region property_mandatory

    private static property_mandatoryTableAdapter _property_mandatory = null;
    public static property_mandatoryTableAdapter Adapter_property_mandatory
    {
        get
        {
            if (_property_mandatory == null)
                _property_mandatory = new property_mandatoryTableAdapter();

            return _property_mandatory;
        }
    }

    public static bool getIsMandatoryPropertyByTypeRequest(string typerequest, int propertyid)
    {
        string con_string = System.Configuration.ConfigurationManager.ConnectionStrings["analyticsConnectionString"].ToString();
        using (SqlConnection con = new SqlConnection(con_string))
        {
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "Prc_GetIsMandatoryPropertyByTypeRequest";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param_mandatory = new SqlParameter("@mandatory", typeof(System.Boolean));
            param_mandatory.Value = true;
            param_mandatory.Direction = ParameterDirection.Output;

            cmd.Parameters.AddWithValue("@typerequest", typerequest);
            cmd.Parameters.AddWithValue("@propertyid", propertyid);
            cmd.Parameters.Add(param_mandatory);

            cmd.ExecuteNonQuery();

            con.Close();
            return Convert.ToBoolean(param_mandatory.Value);
        }
    }

    #endregion
}