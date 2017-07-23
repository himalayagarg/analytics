<%@ Page Title="Analytics (V1.0)" Language="C#" MasterPageFile="~/UI/admin/admin.master" AutoEventWireup="true" CodeFile="projects.aspx.cs" 
    Inherits="UI_admin_projects" MaintainScrollPositionOnPostback="true"%>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

<asp:Panel ID="pnl_manage" runat="server" class="mypanel">
    <table style="width: 100%;" cellpadding="0">
        <tr>
            <td class="mylabel" height="28px" width="120px">
                Add/ Manage</td>
            <td class="mylabel" height="28px" width="250px">
            </td>
            <td width="120px">

            </td>
            <td>
                <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
                </asp:ToolkitScriptManager>
            </td>
        </tr>
        <tr>
            <td>
                Project Name</td>
            <td>
                <asp:TextBox ID="tb_prj_name" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfv_prj_name" runat="server" 
                    ErrorMessage="*" ControlToValidate="tb_prj_name" ValidationGroup="project" 
                    CssClass="failureNotification"></asp:RequiredFieldValidator>
            </td>
            <td>
                Project Type</td>
            <td >
                <asp:DropDownList ID="ddl_prj_type" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                Project Category</td>
            <td>
                <asp:DropDownList ID="ddl_prj_category" runat="server">
                </asp:DropDownList>
            </td>
            <td>
                Project Brand</td>
             <td >
                 <asp:DropDownList ID="ddl_prj_brand" runat="server">
                 </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                Start Date</td>
            <td>
                <asp:TextBox ID="tb_prj_start" runat="server"></asp:TextBox>                           
                <asp:CalendarExtender ID="tb_prj_start_CalendarExtender" runat="server" Format="dd/MM/yyyy" 
                    Enabled="True" TargetControlID="tb_prj_start">
                </asp:CalendarExtender>
                <asp:CompareValidator ID="CompareValidator1" runat="server" 
                    ControlToValidate="tb_prj_start" CssClass="failureNotification" Display="Dynamic" 
                    ErrorMessage="Invalid Date" Operator="DataTypeCheck" Type="Date" ValidationGroup="project" />
            </td>
            <td>
                End Date</td>
            <td>
                <asp:TextBox ID="tb_prj_end" runat="server"></asp:TextBox>
                <asp:CalendarExtender ID="tb_prj_end_CalendarExtender" runat="server" Format="dd/MM/yyyy" 
                    TargetControlID="tb_prj_end" Enabled="True" />
                <asp:CompareValidator ID="CompareValidator2" runat="server" 
                    ControlToValidate="tb_prj_end" CssClass="failureNotification" Display="Dynamic" 
                    ErrorMessage="Invalid Date" Operator="DataTypeCheck" Type="Date" ValidationGroup="project" />
            </td>
        </tr>
        <tr>
            <td>
                Budget(INR)</td>
            <td>
                <asp:TextBox ID="tb_prj_budget" runat="server"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                    ControlToValidate="tb_prj_budget" CssClass="failureNotification" 
                    ErrorMessage="Invalid Amount" ValidationExpression="\d+\.{0,1}\d*" 
                    ValidationGroup="project">
                </asp:RegularExpressionValidator>
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
                    onclick="btn_add_Click" Text="Add" ValidationGroup="project" Visible="False" />
                <asp:Button ID="btn_update" runat="server" Font-Bold="True" 
                    onclick="btn_update_Click" Text="Update" ValidationGroup="project" 
                    Visible="False" />
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
                Search Project(s)</td>
            <td>

                <asp:TextBox ID="tb_projectname_search" runat="server"></asp:TextBox>
                &nbsp;&nbsp;
                <asp:Button ID="btn_search" runat="server" CausesValidation="False" 
                    onclick="btn_search_Click" Text="Search" />

            </td>
            <td>

            </td>
        </tr>
    </table>
</asp:Panel>

<asp:Panel ID="pnl_prj" runat="server" ScrollBars="Both" Height="340px" 
        class="mypanel">    
    <asp:GridView ID="gv_projects" runat="server" AutoGenerateColumns="False" 
        CellPadding="2" CssClass="mygrid" DataKeyNames="projectid" 
        RowStyle-HorizontalAlign="Left" Width="100%" AllowSorting="True" 
        onrowdatabound="gv_projects_RowDataBound" onsorting="gv_projects_Sorting">
        <RowStyle  HorizontalAlign="Left" />
        <HeaderStyle CssClass="mygridheader" />
        <Columns>
            <asp:TemplateField HeaderText="S.No">
                <ItemTemplate>
                    <%# Container.DataItemIndex + 1 %>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="25px" />
            </asp:TemplateField>
            <asp:BoundField DataField="projectid" SortExpression="projectid" HeaderText="ProjectID" />            
            <asp:BoundField DataField="projectname" SortExpression="projectname" HeaderText="Project Name" />
            <asp:BoundField DataField="projecttype" SortExpression="projecttype" HeaderText="Project Type" />
            <asp:BoundField DataField="projectcategory" SortExpression="projectcategory" HeaderText="Category" />
            <asp:BoundField DataField="projectbrand" SortExpression="projectbrand" HeaderText="Brand" />
            <asp:BoundField DataField="budget" SortExpression="budget" HeaderText="Budget" />            
            <asp:BoundField DataField="createdate" SortExpression="createdate" HeaderText="Start Date" DataFormatString="{0:dd/MM/yyyy}" />
            <asp:BoundField DataField="completiondate" SortExpression="completiondate" HeaderText="End Date" DataFormatString="{0:dd/MM/yyyy}" />
            <asp:BoundField DataField="isactive" SortExpression="isactive" HeaderText="Active" />
            <asp:TemplateField HeaderText="Action" ItemStyle-Width="100px">
                <ItemTemplate>
                    <asp:ImageButton ID="imgbtn_Edit" runat="server"
                        CausesValidation="false" ImageUrl="~/image/edit.png" AlternateText="Edit" 
                        ToolTip="Edit Project" onclick="imgbtn_Edit_Click" />
                    &nbsp;&nbsp;&nbsp;
                    <asp:ImageButton ID="imgbtn_Delete" runat="server"
                        CausesValidation="false" ImageUrl="~/image/dustbin_icon.jpg" AlternateText="Delete" 
                        ToolTip="Delete Project" onclick="imgbtn_Delete_Click" />
                    <asp:ConfirmButtonExtender ID="cbe_delete" runat="server" 
                            ConfirmText="Are you sure you want to delete the Project?" 
                            TargetControlID="imgbtn_Delete" />
                    &nbsp;&nbsp;&nbsp;
                    <asp:ImageButton ID="imgbtn_View" runat="server"
                        CausesValidation="false" ImageUrl="~/image/view.png" AlternateText="View" 
                        ToolTip="View Requests" onclick="imgbtn_View_Click" BorderStyle="Solid" BorderWidth="1px" BorderColor="Red" />
                </ItemTemplate>                    
            </asp:TemplateField>
        </Columns>
        <EmptyDataTemplate>
            <div class="mygridempty">
                No Project Record Found. You can Add/ Manage Projects from above.
            </div>
        </EmptyDataTemplate>
    </asp:GridView>
</asp:Panel>

</asp:Content>

