using System;
using System.Data;
using ds_analyticsTableAdapters;

/// <summary>
/// Summary description for labresult
/// </summary>
public class labresult
{
	public labresult()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    private static labresultTableAdapter _labresult = null;
    public static labresultTableAdapter Adapter
    {
        get
        {
            if (_labresult == null)
                _labresult = new labresultTableAdapter();

            return _labresult;
        }
    }

    ///<summary>
    ///Insert new labid for a ts_id
    ///</summary>
    public static bool insertLabResult(long ts_id, long labid, string mth_ref)
    {
        int rowsAffected = Adapter.Prc_InsertLabResult(ts_id, labid, mth_ref);
        return rowsAffected == 1;
    }

    ///<summary>
    ///Get LabResult Table Data Formatted By Join
    ///</summary>
    public static DataTable getLabResult_ByJoining(string reqid)
    {
        return Adapter.GetLabResult_ByJoining(reqid);
    }

    ///<summary>
    ///Get SampleTestLab Table Data Formatted By Join
    ///</summary>
    public static DataTable getSampleTestLabByJoining(string reqid)
    {
        return Adapter.GetSampleTestLabByJoining(reqid);
    }

    ///<summary>
    ///Get SampleTestResult Table Data By SampleID and LabID, Formatted By Join
    ///</summary>
    public static DataTable getSampleTestResultBySampleIDLabID(string sampleid, long labid)
    {
        return Adapter.GetSampleTestResultBySampleIDLabID(sampleid, labid);
    }

    ///<summary>
    ///Get Distinct Samples In a Request To a Lab
    ///</summary>
    public static DataTable getDistinctSamplesInaReqToaLab(string reqid, long labid)
    {
        return Adapter.GetDistinctSamplesInaReqToaLab(reqid, labid);
    }

    ///<summary>
    ///Update Results in labresult table
    ///</summary>
    public static int updateLabResult(string result, bool isfor_report, string attachment_id, long labresult_id)
    {
        return Adapter.Prc_UpdateLabResult(result, isfor_report, attachment_id, labresult_id);
    }
}