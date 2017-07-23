<%@ Page Title="Analytics (V1.0)" Language="C#" MasterPageFile="~/UI/admin/admin.master" AutoEventWireup="true" CodeFile="labs.aspx.cs" 
    Inherits="UI_admin_labs" MaintainScrollPositionOnPostback="true"%>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">   
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

<asp:Panel ID="pnl_manage" runat="server" class="mypanel">
    <table style="width: 100%;" cellpadding="0">
        <tr>
            <td class="mylabel" height="28px" width="130px">
                Add/ Manage</td>
            <td class="mylabel" height="28px" width="250px">
            </td>
            <td width="150px">

            </td>
            <td>
                <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
                </asp:ToolkitScriptManager>
            </td>
        </tr>
        <tr>
            <td>
                Lab Name</td>
            <td>
                <asp:TextBox ID="tb_labname" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfv_labname" runat="server" 
                    ErrorMessage="*" ControlToValidate="tb_labname" ValidationGroup="lab" 
                    CssClass="failureNotification"></asp:RequiredFieldValidator>
            </td>
            <td>
                Address</td>
            <td rowspan="2">
                <asp:TextBox ID="tb_address" runat="server" Font-Size="13px" Rows="2" 
                    TextMode="MultiLine" Width="220px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                City</td>
            <td>
                <asp:TextBox ID="tb_city" runat="server"></asp:TextBox>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                Type</td>
            <td>
                <asp:TextBox ID="tb_type" runat="server"></asp:TextBox>
            </td>
            <td>
                Fax</td>
            <td>
                <asp:TextBox ID="tb_fax" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Laboratory Manager</td>
            <td>
                <asp:TextBox ID="tb_cp1" runat="server"></asp:TextBox>
            </td>
            <td>
                Key Accounts Manager</td>
            <td>
                <asp:TextBox ID="tb_cp2" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Email 1</td>
            <td>
                <asp:TextBox ID="tb_email1" runat="server"></asp:TextBox>
                <asp:RegularExpressionValidator ID="rev_email1" runat="server" ControlToValidate="tb_email1"
                    ErrorMessage="Invalid" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
                    ValidationGroup="lab" CssClass="failureNotification" />
            </td>
            <td>
                Email 2</td>
            <td>
                <asp:TextBox ID="tb_email2" runat="server"></asp:TextBox>
                <asp:RegularExpressionValidator ID="rev_email2" runat="server" ControlToValidate="tb_email2"
                    ErrorMessage="Invalid" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
                    ValidationGroup="lab" CssClass="failureNotification"/>
            </td>
        </tr>
        <tr>
            <td>
                Mobile 1</td>
            <td>
                <asp:TextBox ID="tb_mbl1" runat="server"></asp:TextBox>
                <asp:RegularExpressionValidator ID="rev" runat="server" ControlToValidate="tb_mbl1" ErrorMessage="Invalid"
                    ValidationExpression=".{10}.*" ValidationGroup="lab" CssClass="failureNotification"/>
            </td>
            <td>
                Mobile 2</td>
            <td>
                <asp:TextBox ID="tb_mbl2" runat="server"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="tb_mbl2" ErrorMessage="Invalid"
                    ValidationExpression=".{10}.*" ValidationGroup="lab" CssClass="failureNotification"/>
            </td>
        </tr>
        <tr>
            <td>
                Phone 1</td>
            <td>
                <asp:TextBox ID="tb_phn1" runat="server"></asp:TextBox>
            </td>
            <td>
                Phone 2</td>
            <td>
                <asp:TextBox ID="tb_phn2" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Audit Status</td>
            <td>
                <asp:TextBox ID="tb_auditstatus" runat="server"></asp:TextBox>
            </td>
            <td>
                Active</td>
            <td>
                <asp:CheckBox ID="cb_isactive" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
            </td>
            <td>
                <asp:Button ID="btn_add" runat="server" Font-Bold="True" 
                    onclick="btn_add_Click" Text="Add" Visible="False" ValidationGroup="lab" />
                <asp:Button ID="btn_update" runat="server" Font-Bold="True" 
                    onclick="btn_update_Click" Text="Update" Visible="False" 
                    ValidationGroup="lab" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btn_cancel" runat="server" CausesValidation="False" 
                    Font-Bold="True" onclick="btn_cancel_Click" Text="Cancel" Visible="False" />
            </td>
        </tr>
        </table>
</asp:Panel>

<asp:Panel ID="pnl_search" runat="server" CssClass="mypanel">
    <table width="100%">
        <tr>
            <td class="mylabel" height="28px" width="80px">
                Search Lab(s)</td>
            <td>            
                <asp:TextBox ID="tb_labname_search" runat="server" />
                &nbsp;&nbsp;&nbsp;
                <asp:Button ID="btn_search" runat="server" onclick="btn_search_Click" 
                    Text="Search" CausesValidation="False" />
            </td>
        </tr>       
    </table>
</asp:Panel>

<asp:Panel ID="pnl_labs" runat="server" ScrollBars="Both" Height="360px" class="mypanel">    
    <asp:GridView ID="gv_labs" runat="server" AutoGenerateColumns="False" 
        CssClass="mygrid" DataKeyNames="labid" 
        onrowdatabound="gv_labs_RowDataBound" RowStyle-HorizontalAlign="Left" 
        Width="100%" onsorting="gv_labs_Sorting" AllowSorting="True">
        <RowStyle  HorizontalAlign="Left" />
        <HeaderStyle CssClass="mygridheader" />        
        <Columns>
            <asp:BoundField DataField="labid" HeaderText="labid" Visible="false" />
            <asp:TemplateField HeaderText="S.No">
                <ItemTemplate>
                    <%# Container.DataItemIndex + 1 %>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="25px" />
            </asp:TemplateField>
            <asp:BoundField DataField="labname" SortExpression="labname" HeaderText="Lab Name" />
            <asp:BoundField DataField="address" SortExpression="address" HeaderText="Address" />
            <asp:BoundField DataField="city" SortExpression="city" HeaderText="City" />
            <asp:BoundField DataField="labtype" SortExpression="labtype" HeaderText="Type" />
            <asp:BoundField DataField="contact_person" SortExpression="contact_person" HeaderText="Laboratory<br/>Manager" HtmlEncode="False" />
            <asp:BoundField DataField="key_acc_person" SortExpression="key_acc_person" HeaderText="Key Accounts<br/>Manager" HtmlEncode="False" />
            <asp:BoundField DataField="mbl1" SortExpression="mbl1" HeaderText="Mobile 1" />
            <asp:BoundField DataField="mbl2" SortExpression="mbl2" HeaderText="Mobile 2" />
            <asp:BoundField DataField="email1" SortExpression="email1" HeaderText="Email 1" />
            <asp:BoundField DataField="email2" SortExpression="email2" HeaderText="Email 2" />
            <asp:BoundField DataField="phn1" SortExpression="phn1" HeaderText="Phn 1" />
            <asp:BoundField DataField="phn2" SortExpression="phn2" HeaderText="Phn 2" />
            <asp:BoundField DataField="fax" SortExpression="fax" HeaderText="Fax" />
            <asp:BoundField DataField="auditstatus" SortExpression="auditstatus" HeaderText="Audit Status" />
            <asp:BoundField DataField="isactive" SortExpression="isactive" HeaderText="Active" />
            <asp:TemplateField HeaderText="Edit">
                <ItemTemplate>
                    <asp:ImageButton ID="imgbtn_Edit" runat="server"
                        CausesValidation="false" ImageUrl="~/image/edit.png" AlternateText="Edit" 
                        ToolTip="Edit" onclick="imgbtn_Edit_Click" />                    
                </ItemTemplate>                    
            </asp:TemplateField>
        </Columns>
        <EmptyDataTemplate>
            <div class="mygridempty">
                No Lab Record Found. You can Add/ Manage labs from above.
            </div>
        </EmptyDataTemplate>
    </asp:GridView>
</asp:Panel>

</asp:Content>