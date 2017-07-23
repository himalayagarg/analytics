using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ds_analyticsTableAdapters;

/// <summary>
/// Summary description for mail_error
/// </summary>
public class analyticsmail_log
{
    private static mails_logTableAdapter _mails_logAdapter = null;
    protected static mails_logTableAdapter Adapter
    {
        get
        {
            if (_mails_logAdapter == null)
            { _mails_logAdapter = new mails_logTableAdapter(); }

            return _mails_logAdapter;
        }
    }

    public analyticsmail_log()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    /// <summary>
    /// Return mails by sent_status (is_sent)
    /// </summary>
    public static ds_analytics.mails_logDataTable getmail_logByIssent(bool is_sent)
    {
        return Adapter.Getmail_logByIssent(is_sent);
    }

    /// <summary>
    /// Delete any mail from the mail_error table
    /// </summary>
    public static bool delete(long mail_id)
    {
        int roweffected = Adapter.Delete(mail_id);

        return roweffected == 1;
    }

    /// <summary>
    /// Insert a row in table
    /// </summary>
    public static bool insert(string mail_to1, string mail_to2, string mail_cc1, string mail_cc2, string mail_bcc1, string mail_bcc2, string mail_from, string mail_sub, string mail_body, bool is_sent)
    {
        int roweffected = Adapter.Insert(mail_to1, mail_to2, mail_cc1, mail_cc2, mail_bcc1, mail_bcc2, mail_from, mail_sub, mail_body, DateTime.Now, is_sent);

        return roweffected == 1;
    }

}