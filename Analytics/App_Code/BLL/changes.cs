using System;
using System.Data;
using ds_analyticsTableAdapters;

/// <summary>
/// Summary description for messages
/// </summary>
public class changes
{
    public changes()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    private static changesTableAdapter _changes = null;
    public static changesTableAdapter Adapter
    {
        get
        {
            if (_changes == null)
                _changes = new changesTableAdapter();

            return _changes;
        }
    }
}