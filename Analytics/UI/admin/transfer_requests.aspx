<%@ Page Title="Analytics (V1.0)" Language="C#" MasterPageFile="~/UI/admin/admin.master"
    AutoEventWireup="true" CodeFile="transfer_requests.aspx.cs" Inherits="UI_admin_transfer_requests"
    MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">    
        <table style="width: 100%;" align="left">
            <tr height="60px">
                <td width="400px">
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td align="right">
                    Select MUD ID to transfer requests from:
                </td>
                <td>
                    <asp:DropDownList ID="ddl_userFrom" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="right">
                    Select MUD ID to transfer requests to:
                </td>
                <td>
                    <asp:DropDownList ID="ddl_userTo" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>                        
            <tr>
                <td align="right">
                    Select Requests to be transferred:
                </td>
                <td>
                                        
                </td>
            </tr>
            <tr height="40px">
                <td align="right">
                                    
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Button ID="btn_transfer" runat="server" CausesValidation="False" Text="Transfer" />
                </td>
                <td>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btn_cancel" runat="server" CausesValidation="False" Text="Cancel" />
                </td>
            </tr>
        </table>    
</asp:Content>
