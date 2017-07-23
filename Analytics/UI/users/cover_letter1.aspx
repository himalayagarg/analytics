<%@ Page Language="C#" AutoEventWireup="true" CodeFile="cover_letter1.aspx.cs" Inherits="UI_users_cover_letter1" %>

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
                <td style="font-size: 12px">
                    Date&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lbl_date" runat="server" />
                </td>
            </tr>
            <tr>
                <td style="font-size: 12px">
                    Ref. No.&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lbl_refno" runat="server" />
                </td>
            </tr>
            <tr>
                <td style="padding-top: 15px; padding-bottom: 15px; font-weight: bold;">
                    <asp:Label ID="lbl_lab" runat="server" />
                </td>
            </tr>
            <tr>
                <td style="padding-top: 10px; padding-bottom: 10px;">
                    Dear
                    <asp:Label ID="lbl_labmanager" runat="server"></asp:Label>
                    ,<br />
                    <br />
                    Please find submitted herewith the following samples for testing parameters as 
                    mentioned below:</td>
            </tr>
            <tr>
                <td style="padding-top: 10px; padding-bottom: 10px;">
                    <asp:GridView ID="gv_samples" runat="server" AutoGenerateColumns="False" DataKeyNames="sampleid" 
                        RowStyle-HorizontalAlign="Left" Width="90%" Font-Size="12px" 
                        onrowdatabound="gv_samples_RowDataBound">
                        <RowStyle HorizontalAlign="Left" />
                        <HeaderStyle Font-Bold="True" HorizontalAlign="Left" />
                        <Columns>                            
                            <asp:TemplateField HeaderText="S.No.">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="25px" />
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="Product" />
                            <asp:BoundField HeaderText="Batch No." />
                            <asp:BoundField HeaderText="Qty" />
                            <asp:BoundField DataField="Sampleid" HeaderText="Code No(s)" />
                            <asp:BoundField HeaderText="Testing</br>Parameters" HtmlEncode="False" />
                            <asp:BoundField HeaderText="Method</br>Reference" HtmlEncode="False" />
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>        
    </div>    
    <div>        
        <table style="width:100%;">
            <tr>
                <td>
                    Best Regards,</td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="padding-top: 15px; padding-bottom: 15px; font-weight: bold;">
                    <asp:Label ID="lbl_sender" runat="server" />
                    <br />
                    <asp:Label ID="lbl_name0" runat="server" Font-Bold="True" />
                    <asp:Label ID="lbl_limited0" runat="server" Font-Bold="True" />
                    <asp:Label ID="lbl_gsk0" runat="server" />
                    <asp:HyperLink ID="hl_website0" runat="server" Target="_blank" />
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
