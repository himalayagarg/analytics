<%@ Page Title="Analytics (V1.0)" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Default.aspx.cs"
Inherits="_Default" MaintainScrollPositionOnPostback="true"%>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <div>        
        <table cellpadding="0" cellspacing="0">
            <tr>
                <td align="center" rowspan="1" height="100px" width="40%" >
                </td>
                <td align="center" rowspan="1">
                    <asp:ToolkitScriptManager ID="ScriptManager1" runat="server">
                    </asp:ToolkitScriptManager>
                    <asp:Label ID="lbl_logout" runat="server" ForeColor="#009900" 
                        Text="You have successfully logged out." Visible="False"></asp:Label>
                </td>
                <td align="center" rowspan="1" width="30%">
                </td>
            </tr>
            <tr>
                <td align="center">
                </td>
                <td align="left" valign="top" >
                    <asp:Panel ID="pnl_login" runat="server" CssClass="roundpanel">                    
                    <table cellpadding="0" cellspacing="0" 
                            width="100%">
                        <tr>
                            <td align="left" valign="top">
                                <table style="color: #3C5780">
                                    <tr>
                                        <td align="right" style="font-weight: bold; " valign="top">
                                            <asp:Label ID="lbl_uid" runat="server" Text="MUD-ID" />
                                            &nbsp;&nbsp;&nbsp;
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtbx_uid" runat="server" TabIndex="1" Width="150px"></asp:TextBox>
                                            <br />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                                ControlToValidate="txtbx_uid" ErrorMessage="MUD-ID cannot be blank" 
                                                ForeColor="Black"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="font-weight: bold" valign="top">
                                            <asp:Label ID="lbl_pass" runat="server" Text="Password" />
                                            &nbsp;&nbsp;&nbsp;
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtbx_pass" runat="server" TabIndex="2" TextMode="Password" 
                                                Width="150px"></asp:TextBox>
                                            <br />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                                ControlToValidate="txtbx_pass" ErrorMessage="Password cannot be blank" 
                                                ForeColor="Black"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td align="center" height="10">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td align="center">
                                            <asp:Button ID="btn_Login" runat="server" onclick="btn_Login_Click" 
                                                TabIndex="3" Text="Login" Font-Bold="True"/>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    </asp:Panel>                
                </td>
                <td align="center">
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td align="center" colspan="1" height="40px">
                    <asp:Literal ID="lbl_inputerr" runat="server" 
                        Text="Please provide correct User ID and Password" Visible="False"></asp:Literal>
                </td>
                <td align="center">
                    &nbsp;</td>
            </tr>
            </table>
        
    </div>
</asp:Content>

