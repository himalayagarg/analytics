using System;
using System.Data;
using ds_analyticsTableAdapters;

/// <summary>
/// Summary description for test_samples
/// </summary>
public class test_samples
{
	public test_samples()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    private static test_samplesTableAdapter _test_samples = null;
    public static test_samplesTableAdapter Adapter
    {
        get
        {
            if (_test_samples == null)
                _test_samples = new test_samplesTableAdapter();

            return _test_samples;
        }
    }

    public static bool insertTest_Sample(long? test_id, string sampleid, bool? isselected)
    {
        int rowsAffected = Adapter.Prc_InsertTest_Sample(test_id, sampleid, isselected);
        return rowsAffected == 1;
    }

    public static int updateTest_Sample(long? test_id, string sampleid, bool? isselected)
    {
        return Adapter.Prc_UpdateTest_Samples(test_id, sampleid, isselected);
    }

    public static ds_analytics.test_samplesDataTable getTest_SamplesByTestid(long test_id)
    {
        return Adapter.GetTest_SamplesByTestId(test_id);
    }

    public static ds_analytics.test_samplesDataTable getTestSampleBy_TestidandSampleid(long test_id, string sampleid)
    {
        return Adapter.GetTestSampleBy_TestIDandSampleID(test_id, sampleid);
    }
    public static ds_analytics.test_samplesDataTable getTestSampleBy_ts_id(long ts_id)
    {
        return Adapter.GetTestSampleBy_ts_id(ts_id);
    }
}