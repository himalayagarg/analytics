<%@ Page Title="Analytics (V1.0)" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" 
    CodeFile="customerror.aspx.cs" Inherits="customerror" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
 <table style="width:100%;">
    <tr>
        <td>
            <hr />
        </td>
    </tr>
    <tr>
        <td align="right" style="font-weight: bold">
            <asp:LinkButton ID="LinkButton1" runat="server" ><< Go Back</asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td style="color: #FF0000; font-weight: bold; font-size: x-large" 
            align="left">
            Server Problem. Please try after sometime.</td>
    </tr>
    <tr>
        <td 
            align="left" height="8px">
            &nbsp;</td>
    </tr>
    <tr>
        <td align="left">
            <asp:Label ID="Label1" runat="server" 
                Font-Bold="True" 
                Text="Error on Page : "></asp:Label>
            <asp:Label ID="Label2" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td height="10px">
            &nbsp;</td>
    </tr>
    <tr>
        <td 
            align="left">
            Please try again.</td>
    </tr>
    <tr>
        <td align="right">
            &nbsp;</td>
    </tr>
</table>
</asp:Content>

