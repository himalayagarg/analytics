//using System.Net.Mail;
using System.Data.SqlClient;
using System.Web;
using System.Web.Mail;

/// <summary>
/// Summary description for PMSEmails
/// </summary>
public class analyticsmail
{
    public analyticsmail()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public bool sendmails(string[] to, string[] cc, string[] bcc, string from, string sub, string body, string pAttachmentPath)
    {
        bool flag = false;
        MailMessage mm = new MailMessage();
        //MailAddress ma = new MailAddress(from);
        string ToMail = "";
        string CCMail = "";
        string BccMail = "";
        mm.From = from;
        foreach (string mailto in to)
        {
            if (mailto != "" && mailto != null)
            {
                ToMail = ToMail + mailto.ToString() + ";";
                //mm.To.Add(mailto);
            }
        }
        if (ToMail != "")
        {
            mm.To = ToMail;
        }
        foreach (string mailcc in cc)
        {
            if (mailcc != "" && mailcc != null)
            {
                CCMail = CCMail + mailcc.ToString() + ";";
                //mm.CC.Add(mailcc);
            }
        }
        if (CCMail != "")
        {
            mm.Cc = CCMail;
        }
        foreach (string mailbcc in bcc)
        {
            if (mailbcc != "" && mailbcc != null)
            {
                BccMail = BccMail + mailbcc.ToString() + ";";
                //mm.Bcc.Add(mailbcc);
            }
        }
        if (BccMail != "")
        {
            mm.Bcc = BccMail;
        }        
        mm.Subject = sub;
        mm.Body = body;   
	    mm.BodyFormat = MailFormat.Html;     
        //mm.IsBodyHtml = true;        
        if (pAttachmentPath.Trim() != "" )
        {
            MailAttachment MyAttachment = new MailAttachment(pAttachmentPath);
            //Attachment MyAttachment = new Attachment(pAttachmentPath);
            mm.Attachments.Add(MyAttachment);            
            mm.Priority = MailPriority.High;
        }
        flag = Send(mm);
        return flag;
    }
    private bool Send(MailMessage msg)
    {
        bool flag = false;
        try
        {            
            //SmtpClient smtp = new SmtpClient();
        //SmtpMail.SmtpServer = "rtpsxsmq01.corpnet2.com";
            SmtpMail.SmtpServer = "172.16.34.31";
            //smtp.Host = "rtpsxsmq01.corpnet2.com";
            //smtp.Port = 25;
            //smtp.EnableSsl = false;
            //smtp.Credentials = new System.Net.NetworkCredential("noreply.rnd@gsk.com", "noreply.rnd");
            SmtpMail.Send(msg);
            flag = true;
        }
        catch (System.Exception err)
        {
            string mail_to1 = null, mail_to2 = null, mail_cc1 = null, mail_cc2 = null, mail_bcc1 = null, mail_bcc2 = null;

            //Adding to
            if (msg.To != "") { mail_to1 = msg.To; }
            //if (msg.To.Count>0) { mail_to1 = msg.To[0].Address; } else { mail_to1 = null; }
            //if (msg.To.Count>1) { mail_to2 = msg.To[1].Address; } else { mail_to2 = null; }
            //Adding cc
            if (msg.Cc != "") { mail_cc1 = msg.Cc; }
            //if (msg.CC.Count>0) { mail_cc1 = msg.CC[0].Address; } else { mail_cc1 = null; }
            //if (msg.CC.Count>1) { mail_cc2 = msg.CC[1].Address; } else { mail_cc2 = null; }
            //Adding to
            if (msg.Bcc != "") { mail_bcc1 = msg.Bcc; }
            //if (msg.Bcc.Count>0) { mail_bcc1 = msg.Bcc[0].Address; } else { mail_bcc1 = null; }
            //if (msg.Bcc.Count>1) { mail_bcc2 = msg.Bcc[1].Address; } else { mail_bcc2 = null; }

            string from = msg.From.ToString();
            string sub = msg.Subject;
            string body = msg.Body;                        
            analyticsmail_log.insert(mail_to1, mail_to2, mail_cc1, mail_cc2, mail_bcc1, mail_bcc2, from, sub, body, false);
        }
        return flag;
    }    
}
