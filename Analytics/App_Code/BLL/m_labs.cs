using System;
using System.Data;
using ds_analyticsTableAdapters;

/// <summary>
/// Summary description for m_labs
/// </summary>
public class m_labs
{
	public m_labs()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    private static m_labsTableAdapter _m_labs = null;
    public static m_labsTableAdapter Adapter
    {
        get
        {
            if (_m_labs == null)
                _m_labs = new m_labsTableAdapter();

            return _m_labs;
        }
    }

    public static ds_analytics.m_labsDataTable getAllLabs()
    {
        return Adapter.GetData();
    }

    public static ds_analytics.m_labsDataTable getAllActiveLabs()
    {
        return Adapter.GetAllActiveLabs();
    }

    public static ds_analytics.m_labsDataTable getAllLabsByName(string labname)
    {
        return Adapter.GetAllLabsByName(labname);
    }

    public static ds_analytics.m_labsDataTable getLabByLabid(long labid)
    {
        return Adapter.GetLabByLabID(labid);
    }

    public static int insert(ds_analytics.m_labsRow lab_row)
    {
        return Adapter.Insert(lab_row.labname,
            lab_row.address,
            lab_row.city,
            lab_row.labtype,
            lab_row.auditstatus,
            lab_row.contact_person,
            lab_row.key_acc_person,
            lab_row.mbl1,
            lab_row.mbl2,
            lab_row.email1,
            lab_row.email2,
            lab_row.phn1,
            lab_row.phn2,
            lab_row.fax,
            lab_row.isactive
            );
    }

    public static int update(ds_analytics.m_labsRow lab_row)
    {
        return Adapter.Update(lab_row);
    }

    public static ds_analytics.m_labsRow GetLabRowWithNull(ds_analytics.m_labsRow lab_row)
    {
        if (lab_row.labname == null || lab_row.labname == "") lab_row.SetlabnameNull();
        if (lab_row.address == null || lab_row.address == "") lab_row.SetaddressNull();
        if (lab_row.city == null || lab_row.city == "") lab_row.SetcityNull();
        if (lab_row.labtype == null || lab_row.labtype == "") lab_row.SetlabtypeNull();
        if (lab_row.auditstatus == null || lab_row.auditstatus == "") lab_row.SetauditstatusNull();
        if (lab_row.fax == null || lab_row.fax == "") lab_row.SetfaxNull();
        if (lab_row.contact_person == null || lab_row.contact_person == "") lab_row.Setcontact_personNull();
        if (lab_row.key_acc_person == null || lab_row.key_acc_person == "") lab_row.Setkey_acc_personNull();
        if (lab_row.email1 == null || lab_row.email1 == "") lab_row.Setemail1Null();
        if (lab_row.email2 == null || lab_row.email2 == "") lab_row.Setemail2Null();
        if (lab_row.mbl1 == "" || lab_row.mbl1 == "") lab_row.Setmbl1Null();
        if (lab_row.mbl2 == "" || lab_row.mbl2 == "") lab_row.Setmbl2Null();
        if (lab_row.phn1 == null || lab_row.phn1 == "") lab_row.Setphn1Null();
        if (lab_row.phn2 == null || lab_row.phn2 == "") lab_row.Setphn2Null();

        return lab_row;
    }
}