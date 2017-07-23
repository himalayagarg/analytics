using System;
using System.Data;
using ds_analyticsTableAdapters;

/// <summary>
/// Summary description for requests
/// </summary>
public class requests
{
	public requests()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    private static requestsTableAdapter _requests = null;
    public static requestsTableAdapter Adapter
    {
        get
        {
            if (_requests == null)
                _requests = new requestsTableAdapter();

            return _requests;
        }
    }

    ///<summary>
    ///Insert new request at the time requestor first make it
    ///</summary>
    public static bool insert_request(string reqid, long projectid, string analysistype, string reqtype, string reqfrom, string responsible, string reqto, string designate, int statusid, string req_cmnt)
    {
        int rowsAffected = Adapter.Prc_InsertRequest(reqid, projectid, analysistype, reqtype, reqfrom, responsible, reqto, designate, statusid, req_cmnt);
        return rowsAffected == 1;
    }

    ///<summary>
    ///Get all request record
    ///</summary>
    public static ds_analytics.requestsDataTable getAllRequests()
    {
        return Adapter.GetData();
    }

    ///<summary>
    ///Get request record
    ///</summary>
    public static ds_analytics.requestsDataTable getRequestbyReqid(string reqid)
    {
        return Adapter.GetRequestByReqid(reqid);        
    }

    public static ds_analytics.requestsDataTable getAllRequestsByReqType(string reqtype)
    {
        return Adapter.GetAllRequestsByReqType(reqtype);
    }

    public static ds_analytics.requestsDataTable getAllRequestsByStatusID(int statusid)
    {
        return Adapter.GetAllRequestsByStatusID(statusid);
    }

    public static ds_analytics.requestsDataTable getAllRequestsByReqFromName(string fullname)
    {
        return Adapter.GetAllRequestsByReqFromName(fullname);
    }

    public static ds_analytics.requestsDataTable getAllRequestsByResponsibleName(string fullname)
    {
        return Adapter.GetAllRequestsByResponsibleName(fullname);
    }

    public static ds_analytics.requestsDataTable getAllRequestsByReqDateFromTo(DateTime dt1, DateTime dt2)
    {
        dt1 = dt1.AddHours(-dt1.Hour).AddMinutes(-dt1.Minute).AddSeconds(-dt1.Second);    //removing extra hours, minutes, seconds
        dt2 = dt2.AddHours(-dt2.Hour).AddMinutes(-dt2.Minute).AddSeconds(-dt2.Second);    //removing extra hours, minutes, seconds
        dt2 = dt2.AddDays(1).AddSeconds(-1);
        return Adapter.GetAllRequestsByReqDateFromTo(dt1, dt2);
    }

    ///<summary>
    ///Get request statusid
    ///</summary>
    public static int getStatusIDbyReqid(string reqid)
    {
        ds_analytics.requestsDataTable rdt = Adapter.GetRequestByReqid(reqid);
        return rdt[0].statusid;

    }

    ///<summary>
    ///Update request record
    ///</summary>
    public static bool update_Request(ds_analytics.requestsRow req_row)
    {         
        int rowsAffected = Adapter.Update(req_row);
     
        return rowsAffected == 1;
    }

    ///<summary>
    ///Delete request record
    ///</summary>
    public static bool delete_Request(string reqid)
    {
        int rowsAffected = Adapter.Delete(reqid);

        return rowsAffected == 1;
    }

    ///<summary>
    ///Get all requests by a user
    ///</summary>
    public static ds_analytics.requestsDataTable getAllRequestsbyReqfrom(string reqfrom)
    {
        return Adapter.GetAllRequestsByReqFrom(reqfrom);
    }

    ///<summary>
    ///Get all requests to a user
    ///</summary>
    public static ds_analytics.requestsDataTable getAllRequestsbyResponsible(string responsible)
    {
        return Adapter.GetAllRequestsByResponsible(responsible);
    }

    ///<summary>
    ///Count requests in a project
    ///</summary>
    public static int countReqByProjID(long projectid)
    {
        return Convert.ToInt32(Adapter.Prc_CountReqByProjID(projectid));
    }

    ///<summary>
    ///Count requests by a user on a particular status
    ///</summary>
    public static int countReqByStatusReqfrom(string responsible, int statusid)
    {
        return Convert.ToInt32(Adapter.Prc_CountReqByStatusReqfrom(responsible, statusid));
    }

    ///<summary>
    ///Count requests to a user on a particular status
    ///</summary>
    public static int countReqByStatusResponsible(string responsible, int statusid)
    {
        return Convert.ToInt32(Adapter.Prc_CountReqByStatusResponsible(responsible, statusid));
    }

    ///<summary>
    ///Get all requests by ProjectID
    ///</summary>
    public static ds_analytics.requestsDataTable getAllRequestsByProjID(long prj_id)
    {
        return Adapter.GetAllRequestsByProjID(prj_id);
    }

    ///<summary>
    ///Returns new reqid (add 1 in old if in same month, otherwise send yymm0001)
    ///</summary>    
    public static string get_new_reqid()
    {
        string new_reqid = string.Empty;
        ds_analytics.requestsDataTable rdt = Adapter.GetLastRequestId();
        if (rdt.Rows.Count > 0)
        {
            string reqid_last = rdt[0]["reqid"].ToString();         //12110001

            string old_reqno = reqid_last.Substring(4);                 //0001
            string yearmonth_last = reqid_last.Substring(0, 4);     //1211 (yymm)

            string yearmonth_today = DateTime.Now.Year.ToString().Substring(2) + DateTime.Now.Month.ToString("00"); //yymm
            if (yearmonth_today == yearmonth_last)
            {
                string new_reqno = (Convert.ToInt32(old_reqno) + 1).ToString("0000");     //0002
                new_reqid = yearmonth_today + new_reqno;
            }
            else
            {
                new_reqid = yearmonth_today + "0001";
            }
        }
        else
        {
            new_reqid = DateTime.Now.Year.ToString().Substring(2) + DateTime.Now.Month.ToString("00") + "0001";
        }
        return new_reqid;
    }

}