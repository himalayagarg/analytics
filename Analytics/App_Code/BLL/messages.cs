using System;
using System.Data;
using ds_analyticsTableAdapters;

/// <summary>
/// Summary description for messages
/// </summary>
public class messages
{
	public messages()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    private static messagesTableAdapter _messages = null;
    public static messagesTableAdapter Adapter
    {
        get
        {
            if (_messages == null)
                _messages = new messagesTableAdapter();

            return _messages;
        }
    }
}