using System;
using System.Data;
using ds_analyticsTableAdapters;

/// <summary>
/// Summary description for lab_sample_quantity
/// </summary>
public class lab_sample_quantity
{
	public lab_sample_quantity()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    private static lab_sample_quantityTableAdapter _lab_sample_quantity = null;
    public static lab_sample_quantityTableAdapter Adapter
    {
        get
        {
            if (_lab_sample_quantity == null)
                _lab_sample_quantity = new lab_sample_quantityTableAdapter();

            return _lab_sample_quantity;
        }
    }

    ///<summary>
    ///Insert quantity per Lab per Sample
    ///</summary>
    public static bool insert_LabSampleQuantity(long labid, string sampleid, string sample_quantity)
    {
        int rowsAffected = Adapter.Prc_Insert_LabSample_Quantity(labid, sampleid, sample_quantity);
        return rowsAffected == 1;
    }

    ///<summary>
    ///delete all quantity in the request (using table join)
    ///</summary>
    public static void deleteAllQuantitiesInRequest(string reqid)
    {
        Adapter.Prc_DeleteAllQuantitiesInRequest(reqid);        
    }

    ///<summary>
    ///Get scalar quantity value by labid, sampleid
    ///</summary>
    public static string getQuantityBySampleIDLabID(long labid, string sampleid)
    {
        string sample_quantity = "";
        Adapter.Prc_GetQuantityBySampleIDLabID(labid, sampleid, ref sample_quantity);
        return sample_quantity;
    }
}