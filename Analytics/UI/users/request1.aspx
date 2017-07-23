<%@ Page Title="Analytics (V1.0)" Language="C#" MasterPageFile="~/UI/users/users.master" AutoEventWireup="true" CodeFile="request1.aspx.cs" 
    Inherits="UI_users_request1" MaintainScrollPositionOnPostback="true"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<script type = "text/javascript" >
    function check_number()
    {
        var reply = true;
        var no_samples = document.getElementById("<%=tb_samples.ClientID%>").value;
        if (no_samples > 24) 
        {
            alert('Maximum 24 samples supported.');
            reply = false;
        }
        return reply;
    }
</script>
    <div align="left" style="margin-left: 150px; margin-top: 60px;">
        <asp:Panel ID="Panel1" runat="server" CssClass="mypanel" Width="550px">        
        <table style="width: 100%;">
            <tr>
                <td align="right" colspan="2" height="40px">
                </td>
            </tr>
            <tr>
                <td align="right">                 
                    Enter number of Samples :                 
                </td>
                <td>                 
                    <asp:TextBox ID="tb_samples" runat="server" TabIndex="1"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tb_samples" 
                        ErrorMessage="*" CssClass="failureNotification" ValidationGroup="request">
                    </asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="tb_samples"
                        ErrorMessage="Number" ValidationExpression="^\d+$" 
                        CssClass="failureNotification" ValidationGroup="request" Display="Dynamic"></asp:RegularExpressionValidator>                 
                </td>
            </tr>            
            <tr>
                <td align="right">                 
                    Enter number of Tests :                 
                </td>
                <td>                 
                    <asp:TextBox ID="tb_tests" runat="server" TabIndex="2"></asp:TextBox>                 
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tb_tests" 
                        ErrorMessage="*" CssClass="failureNotification" ValidationGroup="request">
                    </asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="tb_tests"
                        ErrorMessage="Number" ValidationExpression="^\d+$" 
                        CssClass="failureNotification" ValidationGroup="request" Display="Dynamic"></asp:RegularExpressionValidator>
                </td>
            </tr>            
            <tr>
                <td align="right" colspan="2" height="40px">                 
                    &nbsp;</td>
            </tr>            
            <tr>
                <td align="right" width="280px">
                 
                    <asp:Button ID="btn_cancel" runat="server" CausesValidation="False" 
                        PostBackUrl="~/UI/users/home.aspx" TabIndex="4" Text="Cancel" 
                        UseSubmitBehavior="False" />
                </td>
                <td align="left">                 
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;                 
                    <asp:Button ID="btn_ok" runat="server" Text="OK" Width="50px" 
                        onclick="btn_ok_Click" ValidationGroup="request" 
                        OnClientClick="if(!check_number()) return false;" TabIndex="3" />                 
                </td>
            </tr>            
            <tr>
                <td align="right" colspan="2" width="300px">
                    &nbsp;</td>
            </tr>
        </table>
        </asp:Panel>
    </div>
</asp:Content>

