using System;
using System.Data;
using ds_analyticsTableAdapters;

/// <summary>
/// Summary description for req_tests
/// </summary>
public class req_tests
{
	public req_tests()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    private static req_testsTableAdapter _req_tests = null;
    public static req_testsTableAdapter Adapter
    {
        get
        {
            if (_req_tests == null)
                _req_tests = new req_testsTableAdapter();

            return _req_tests;
        }
    }

    public static long? insert_Req_Tests(string reqid, string testname, string reference, string standard, string unit)
    {
        //Prc_InsertReq_Tests sql query using a scalar Insert Query which returns the scope identity column of the test row inserted        
        long? test_id = new long();
        test_id = Convert.ToInt64(Adapter.Prc_InsertReq_Tests(reqid, testname, reference, standard, unit));
        return test_id;
    }


    public static int update_Req_Tests(string testname, string reference, string standard, string unit, long test_id)
    {
        return Adapter.Prc_UpdateReq_Tests(testname, reference, standard, unit, test_id);
    }

    public static ds_analytics.req_testsDataTable getTestsbyReqid(string reqid)
    {
        return Adapter.GetTestsByreqid(reqid);
    }

    public static ds_analytics.req_testsDataTable getTestbyTestid(long test_id)
    {
        return Adapter.GetTestByTestID(test_id);
    }

    public static int countTestsByReqId(string reqid)
    {
        return Convert.ToInt32(Adapter.Prc_CountTestsByReqId(reqid));
    }
}