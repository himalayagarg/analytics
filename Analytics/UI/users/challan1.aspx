<%@ Page Language="C#" AutoEventWireup="true" CodeFile="challan1.aspx.cs" Inherits="UI_users_challan1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Analytics (V1.0)</title>
</head>
<body>
    <form id="form1" runat="server" style="font-family: calibri; font-size: 14px;">
    <div>
        <table style="width: 100%;">
            <tr>
                <td>
                </td>
                <td align="right">
                    <img alt="" src="../../image/logo-gsk-label.png" /></td>
            </tr>
            <tr>
                <td>
                </td>
                <td align="left" width="200px" style="font-size: 11px">
                    <asp:Label ID="lbl_name1" runat="server" Font-Bold="True" />
                    <asp:Label ID="lbl_limited" runat="server" Font-Bold="True" />
                    <asp:Label ID="lbl_gsk" runat="server" />
                    <asp:HyperLink ID="hl_website" runat="server" Target="_blank"/>
                </td>
            </tr>
        </table>
    </div>
    <div style="height: 30px">
        
    </div>
    <div>        
        <table style="width:100%;">
            <tr>
                <td>
                    <asp:Label ID="lbl_date" runat="server" Font-Size="12px" />
                </td>
            </tr>
            <tr>
                <td align="center" 
                    
                    style="padding-top: 15px; padding-bottom: 15px; font-weight: bold; font-size: 16px;" >
                    TO WHOMSOEVER IT MAY CONCERN</td>
            </tr>
            <tr>
                <td>
                    This is to certify that we are sending one Envelop/ Box containing Development 
                    Sample(s).
                </td>
            </tr>
            <tr>
                <td style="padding-top: 15px; padding-bottom: 15px; font-weight: bold;">
                    <asp:Label ID="lbl_lab" runat="server" />
                </td>
            </tr>
            <tr>
                <td style="padding-top: 10px; padding-bottom: 10px;">
                    This sample is only for Analysis Purposes and does not have any commercial 
                    value. The above sample is non-toxic, non-hazardous & non-inflammable.
                </td>
            </tr>
            <tr>
                <td>
                    In case of any clarification, please feel free to contact undersigned.
                </td>
            </tr>
        </table>        
    </div>
    <div style="height: 40px">

    </div>
    <div>        
        <table style="width:100%;">
            <tr>
                <td style="font-weight: bold">
                    AUTHORISED SIGNATORY</td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="padding-top: 15px; padding-bottom: 15px; font-weight: bold;">
                    <asp:Label ID="lbl_sender" runat="server" />                    
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td width="150px" style="font-size: 10px; color: #666666; padding-top: 20px;">
                    <strong>Registered Office</strong><br />
                    Patiala Road<br />
                    Nabha (Punjab)<br />
                    147201
                </td>
            </tr>            
        </table>        
    </div>
    </form>
</body>
</html>
