<%@ Page Title="Analytics (V1.0)" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="sitemaintain.aspx.cs" 
    Inherits="sitemaintain" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <div style="padding: 5px; background-color: #F0F0F0" align="center" >
        <div align="center" style="margin: 20px; background-color: #F9F9F9;">            
            <div style="height: 40px; width: 70%;" align="left">
            </div>
            <div style="background-color: #FFFFFF; width: 70%;" >
                <table style="width: 100%;">
                    <tr>
                        <td align="left">
                            &nbsp;</td>
                        <td align="left"                             
                            
                            
                            style="background-image: url('image/GSK-login-logo.jpg'); background-repeat: no-repeat; background-position: center top; height: 270px;" 
                            width="310px">
                        </td>
                        <td align="left">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td align="center">
                            &nbsp;</td>
                        <td align="left">
                            <asp:Label ID="lbl_text" runat="server" Font-Bold="True" Font-Size="18px" />
                        </td>
                        <td align="center">
                            &nbsp;</td>
                    </tr>
                    </table>
            </div>
            <div style="height: 40px; width: 70%;">
            </div>
        </div>
        <div align="right" style="margin: 20px; background-color: #F9F9F9;">
            <asp:TextBox ID="txtbx_pass" runat="server" />
            <asp:Button ID="btn_ok" runat="server" Text="Ok" onclick="btn_ok_Click" />
        </div>
    </div>
</asp:Content>

