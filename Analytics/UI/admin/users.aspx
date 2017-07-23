<%@ Page Title="Analytics (V1.0)" Language="C#" MasterPageFile="~/UI/admin/admin.master" AutoEventWireup="true" CodeFile="users.aspx.cs" 
    Inherits="UI_admin_users" MaintainScrollPositionOnPostback="true"%>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script type="text/javascript">
    
</script>
    <style type="text/css">
        .style1
        {
            color: #282828;
            font-weight: bold;
            height: 28px;
        }
        .style2
        {
            height: 28px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

<asp:Panel ID="pnl_manage" runat="server" class="mypanel">
    <table style="width: 100%;" cellpadding="0">
        <tr>
            <td class="style1" width="120px">
                Edit Users</td>
            <td class="style1" width="250px">
            </td>
            <td width="120px" class="style2">

            </td>
            <td class="style2">
                <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
                </asp:ToolkitScriptManager>
            </td>
        </tr>
        <tr>
            <td>
                User ID (MUD ID)</td>
            <td>
                <asp:Label ID="lbl_userid" runat="server" Font-Bold="True"></asp:Label>
            </td>
            <td>
                Access Rights</td>
            <td rowspan="6" >                
                <asp:TreeView ID="TreeView1" runat="server" BorderColor="#333333" 
                    BorderStyle="Solid" BorderWidth="1px" ShowExpandCollapse="False" Width="180px">
                    <RootNodeStyle Font-Bold="True" />
                </asp:TreeView>
            </td>
        </tr>
        <tr>
            <td>
                User Name</td>
            <td>
                <asp:TextBox ID="tb_username" runat="server" Width="160px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator_username" runat="server" 
                    ControlToValidate="tb_username" CssClass="failureNotification" ErrorMessage="*" 
                    ValidationGroup="edit_user">
                </asp:RequiredFieldValidator>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                Email</td>
            <td>                
                <asp:TextBox ID="tb_email" runat="server" Width="200px"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                    ControlToValidate="tb_email" CssClass="failureNotification" 
                    ErrorMessage="Invalid email" 
                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
                    ValidationGroup="edit_user">
                </asp:RegularExpressionValidator>                
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                Contact No.
            </td>
            <td>
                <asp:TextBox ID="tb_mblno" runat="server" Width="200px"></asp:TextBox>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td valign="top">
                Active</td>
            <td valign="top">
                <asp:CheckBox ID="cb_isactive" runat="server" />
            </td>
            <td valign="top">
                &nbsp;</td>
        </tr>
        <tr>
            <td valign="top">
                &nbsp;</td>
            <td valign="top">
                &nbsp;</td>
            <td valign="top">
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                <asp:Button ID="btn_update" runat="server" Font-Bold="True" onclick="btn_update_Click" 
                    Text="Update" Visible="False" ValidationGroup="edit_user" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btn_cancel" runat="server" CausesValidation="False" 
                    Font-Bold="True" onclick="btn_cancel_Click" Text="Cancel" Visible="False" />
            </td>
        </tr>
        </table>
</asp:Panel>

<asp:Panel ID="pnl_search" runat="server" class="mypanel">
    <table style="width: 100%;">
        <tr>
            <td class="mylabel" height="28px" width="100px">
                Search User(s)</td>
            <td>

                <asp:TextBox ID="tb_username_search" runat="server"></asp:TextBox>
                &nbsp;&nbsp;
                <asp:Button ID="btn_search" runat="server" CausesValidation="False" 
                    onclick="btn_search_Click" Text="Search" />

            </td>
            <td>
                <asp:TextBox ID="tb_add_username" runat="server" Width="140px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator_adduser" runat="server" 
                    ControlToValidate="tb_add_username" ErrorMessage="*" CssClass="failureNotification" 
                    ValidationGroup="add_user" />
                <asp:TextBoxWatermarkExtender ID="wm_add_username" runat="server" 
                    TargetControlID="tb_add_username" WatermarkCssClass="watermark" WatermarkText="Add User (Write MUD ID)">
                </asp:TextBoxWatermarkExtender>
                &nbsp;&nbsp;
                <asp:Button ID="btn_add" runat="server" Font-Bold="True" 
                    onclick="btn_add_Click" Text="Add" ValidationGroup="add_user" />
            </td>
        </tr>
    </table>
</asp:Panel>

<asp:Panel ID="pnl_users" runat="server" ScrollBars="Both" Height="340px" class="mypanel">
    <asp:GridView ID="gv_users" runat="server" AutoGenerateColumns="False" 
        CellPadding="2" CssClass="mygrid" DataKeyNames="userid" 
        RowStyle-HorizontalAlign="Left" Width="100%" AllowSorting="True" 
        onrowdatabound="gv_users_RowDataBound" onsorting="gv_users_Sorting">
        <RowStyle  HorizontalAlign="Left" />
        <HeaderStyle CssClass="mygridheader" />
        <Columns>            
            <asp:TemplateField HeaderText="S.No">
                <ItemTemplate>
                    <%# Container.DataItemIndex + 1 %>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="25px" />
            </asp:TemplateField>
            <asp:BoundField DataField="userid" SortExpression="userid" HeaderText="User ID" />
            <asp:BoundField DataField="fullname" SortExpression="fullname" HeaderText="Full Name" />
            <asp:BoundField DataField="email" SortExpression="email" HeaderText="Email" />
            <asp:BoundField DataField="mblno" SortExpression="mblno" HeaderText="Contact No." />
            <asp:BoundField DataField="isactive" SortExpression="isactive" HeaderText="Active" />
            <asp:TemplateField HeaderText="Action">
                <ItemTemplate>
                    <asp:ImageButton ID="imgbtn_Edit" runat="server"
                        CausesValidation="false" ImageUrl="~/image/edit.png" AlternateText="Edit" 
                        ToolTip="Edit User" onclick="imgbtn_Edit_Click" />
                    &nbsp;&nbsp;&nbsp;
                    <asp:ImageButton ID="imgbtn_Delete" runat="server"
                        CausesValidation="false" ImageUrl="~/image/dustbin_icon.jpg" AlternateText="Delete" 
                        ToolTip="Delete User" onclick="imgbtn_Delete_Click" />
                    <asp:ConfirmButtonExtender ID="cbe_delete" runat="server" 
                            ConfirmText="Are you sure you want to delete the user?" 
                            TargetControlID="imgbtn_Delete" />
                </ItemTemplate>                    
            </asp:TemplateField>
        </Columns>
        <EmptyDataTemplate>
            <div class="mygridempty">
                No User Found. You can Add/ Manage Users from above.
            </div>
        </EmptyDataTemplate>
    </asp:GridView>
</asp:Panel>
<div align="center">
    <asp:Panel ID="pnl_popup" runat="server" Width="440px" 
        DefaultButton="btn_modalok" BorderStyle="Solid" BorderWidth="1px">
        <table style="width: 100%;" cellpadding="0" cellspacing="0">
            <tr>
                <td align="center" style="padding: 20px; background-color: #FFFFFF;">
                    <asp:Label ID="lbl_popup" runat="server" Text="Add Rights Now?"/>
                </td>
            </tr>
            <tr>
                <td align="center" style="padding: 8px; background-color: #F2F2F2;">                    
                    <asp:Button ID="btn_modalok" runat="server" Text="Yes" 
                        onclick="btn_modalok_Click" Width="50px" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btn_modalcancel" runat="server" Text="No" Width="50px" />
                </td>
            </tr>
        </table>
        <asp:ModalPopupExtender ID="mpopup_1" runat="server" TargetControlID="lbl_popup" DropShadow="true" 
            PopupControlID="pnl_popup" CancelControlID="btn_modalcancel" BackgroundCssClass="modalBackground" 
            Drag="true" PopupDragHandleControlID="pnl_popup">
        </asp:ModalPopupExtender>
    </asp:Panel>
</div>
</asp:Content>

