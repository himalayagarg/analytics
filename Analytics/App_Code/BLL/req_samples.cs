using System;
using System.Data;
using ds_analyticsTableAdapters;

/// <summary>
/// Summary description for req_samples
/// </summary>
public class req_samples
{
	public req_samples()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    private static req_samplesTableAdapter _req_samples = null;
    public static req_samplesTableAdapter Adapter
    {
        get
        {
            if (_req_samples == null)
                _req_samples = new req_samplesTableAdapter();

            return _req_samples;
        }
    }

    public static bool insert_Req_Samples(string sampleid, string reqid, int? sampleno, bool? isactive)
    {
        int rowsAffected = Adapter.Prc_InsertReq_Samples(sampleid, reqid, sampleno, isactive);
        return rowsAffected == 1;
    }

    public static ds_analytics.req_samplesDataTable getSamplesByReqid(string reqid)
    {
        return Adapter.GetSamplesByReqid(reqid);
    }

    public static int countSamplesByReqId(string reqid)
    {
        return Convert.ToInt32(Adapter.Prc_CountSamplesByReqId(reqid));
    }
}