<%@ Page Title="Analytics (V1.0)" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" 
    CodeFile="print_doc.aspx.cs" Inherits="UI_users_print_doc" MaintainScrollPositionOnPostback="true"%>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<div class="pageTitle">
    Print Documents for the Labs
</div>
<div class="mypanel" style="width: 700px">
    <asp:Repeater ID="Repeater1" runat="server" 
        onitemdatabound="Repeater1_ItemDataBound">
        <ItemTemplate>
            <table width="98%" style="padding: 6px; margin: 5px; border: 1px solid #999999;" cellpadding="0" cellspacing="0">
                <tr>                
                    <td align="left" style="text-align: left; vertical-align: top">
                        <asp:Label ID="lbl_person1" runat="server" CssClass="mylabel" Text='<%#Bind("contact_person")%>'></asp:Label>
                        <br />
                        <asp:Label ID="lbl_labid" runat="server" CssClass="mylabel" Text='<%#Bind("labid")%>' Visible="False"></asp:Label>
                        <asp:Label ID="lbl_labname" runat="server" CssClass="mylabel" Text='<%#Bind("labname")%>'></asp:Label>, 
                        <asp:Label ID="lbl_city" runat="server" CssClass="mylabel" Text='<%#Bind("city")%>'></asp:Label>
                        <br />
                        Phone: <asp:Label ID="lbl_mbl1" runat="server" CssClass="mylabel" Text='<%#Bind("mbl1")%>'></asp:Label>
                        <br />
                        Email: <asp:Label ID="lbl_email1" runat="server" CssClass="mylabel" Text='<%#Bind("email1")%>'></asp:Label>                        
                    </td>
                    <td align="left" width="80px" valign="top">
                        <asp:HyperLink ID="hl_cover_letter" runat="server" Target="_blank">Cover Letter</asp:HyperLink>
                        <br />
                        <asp:HyperLink ID="hl_challan" runat="server" Target="_blank">Challan</asp:HyperLink>
                        <br />
                        <asp:HyperLink ID="hl_labels" runat="server" Target="_blank">Labels</asp:HyperLink>
                    </td>
                </tr>                       
            </table>
            <br />            
        </ItemTemplate>
    </asp:Repeater>
</div>
</asp:Content>

