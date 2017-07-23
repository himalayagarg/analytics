using System;
using System.Data;
using System.Data.SqlClient;
using ds_analyticsTableAdapters;

/// <summary>
/// Summary description for sample_pvalue
/// </summary>
public class sample_pvalue
{
	public sample_pvalue()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    private static sample_pvalueTableAdapter _sample_pvalue = null;
    public static sample_pvalueTableAdapter Adapter
    {
        get
        {
            if (_sample_pvalue == null)
                _sample_pvalue = new sample_pvalueTableAdapter();

            return _sample_pvalue;
        }        
    }

    public static bool insertSample_Pvalue(string sampleid, int? propertyid, string pvalue)
    {
        int rowsAffected = Adapter.Prc_InsertSample_Pvalue(sampleid, propertyid, pvalue);
        return rowsAffected == 1;
    }

    public static int updateSample_Pvalue(string sampleid, int propertyid, string pvalue)
    {
        return Adapter.Prc_UpdateSample_Pvalue(sampleid, propertyid, pvalue);
    }

    public static DataTable getPvaluesBySampleid(string sampleid)
    {
        string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["analyticsConnectionString"].ToString();
        SqlConnection con = new SqlConnection(conStr);
        con.Open();
        SqlDataAdapter da = new SqlDataAdapter("Prc_GetPvaluesBySampleID", con);
        da.SelectCommand.CommandType = CommandType.StoredProcedure;        
        da.SelectCommand.Parameters.AddWithValue("@sampleid", sampleid);
        DataTable dt = new DataTable();
        da.Fill(dt);
        con.Close();
        return dt;
    }

    public static string getPvalueBySampleIDandPropertyID(string sampleid, int propertyid)
    {
        // we can never change the propertyid of any property now, can only change the propertyname
        string pvalue = "";
        try
        {
            DataTable dt_prop_value = sample_pvalue.getPvaluesBySampleid(sampleid);
            DataRow[] dr_property = dt_prop_value.Select("propertyid=" + propertyid);
            DataRow dr = dr_property[0];
            pvalue = dr["pvalue"].ToString();
        }
        catch (Exception ex)
        {

        }        
        return pvalue;        
    }
}