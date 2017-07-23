<%@ Page Title="" Language="C#" MasterPageFile="~/UI/admin/admin.master" AutoEventWireup="true" CodeFile="mis_reqlife.aspx.cs" 
    Inherits="UI_admin_mis_reqlife" MaintainScrollPositionOnPostback="true" EnableEventValidation="false"%>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

<asp:Panel ID="pnl_filter" runat="server" class="mypanel">    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddl_searchby" />            
            <asp:PostBackTrigger ControlID="btn_search"/>
            <asp:PostBackTrigger ControlID="imgbtn_excel"/>
        </Triggers>
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td width="5px">
                        <asp:ScriptManager ID="ScriptManager1" runat="server">
                        </asp:ScriptManager>                        
                    </td>
                    <td width="120px">                    
                        <asp:DropDownList ID="ddl_searchby" runat="server" AutoPostBack="True" 
                            onselectedindexchanged="ddl_searchby_SelectedIndexChanged">
                            <asp:ListItem Text="-Search By-" Value="-Search By-" Enabled="true" />
                            <asp:ListItem Text="Request No." Value="reqid"/>
                            <asp:ListItem Text="Project Name" Value="projectid"/>
                            <asp:ListItem Text="Request Type" Value="reqtype"/>
                            <asp:ListItem Text="Request From" Value="reqfrom"/>
                            <asp:ListItem Text="Request To" Value="responsible"/>
                            <asp:ListItem Text="Status" Value="statusid"/>
                            <asp:ListItem Text="Request Date" Value="reqdate"/>
                        </asp:DropDownList>                    
                    </td>
                    <td width="300px">
                        <asp:TextBox ID="TextBox1" runat="server" Visible="false" AutoCompleteType="Disabled"/>
                        <asp:RequiredFieldValidator ID="rfv1" runat="server" ErrorMessage="*" ControlToValidate="TextBox1" Display="Static" />                        
                        <asp:DropDownList ID="DropDownList1" runat="server" Visible="False">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Button ID="btn_search" runat="server" Text="Search" Visible="False" 
                            onclick="btn_search_Click" />                    
                    </td>
                    <td align="right">
                        Download<asp:ImageButton ID="imgbtn_excel" runat="server" AlternateText="Excel" 
                            CausesValidation="False" ImageUrl="~/image/excelicon.jpg" 
                            onclick="imgbtn_excel_Click" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>    
</asp:Panel>

<asp:Panel ID="pnl_report" runat="server" class="mypanel" ScrollBars="Both" Height="390px">
    <asp:GridView ID="gv_reqs" runat="server" AutoGenerateColumns="False" CssClass="mygrid" 
        DataKeyNames="reqid" RowStyle-HorizontalAlign="Left" Width="100%" 
        AllowSorting="True" onrowdatabound="gv_reqs_RowDataBound" 
        onsorting="gv_reqs_Sorting" AllowPaging="True" PageSize="18"
        onpageindexchanging="gv_reqs_PageIndexChanging" 
        EnableModelValidation="True">
        <RowStyle  HorizontalAlign="Left" />
        <HeaderStyle CssClass="mygridheader" />        
        <PagerSettings Mode="NumericFirstLast" />
        <PagerStyle Font-Bold="True" Font-Size="12px" BackColor="#EAEAEA" Height="12px" />
        <Columns>            
            <asp:TemplateField HeaderText="S.No">
                <ItemTemplate>
                    <%# Container.DataItemIndex + 1 %>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="25px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Req.No." SortExpression="reqid">
                <ItemTemplate>
                    <asp:HyperLink ID="hl_reqno" runat="server" Text='<%#Bind("reqid") %>' Target="_blank" />
                </ItemTemplate>                
            </asp:TemplateField>            
            <asp:BoundField DataField="projectid" SortExpression="projectid" HeaderText="Project Name" />
            <asp:BoundField DataField="reqtype" SortExpression="reqtype" HeaderText="Req.Type" />
            <asp:BoundField DataField="reqfrom" SortExpression="reqfrom" HeaderText="From" />
            <asp:BoundField DataField="responsible" SortExpression="responsible" HeaderText="To" />
            <asp:BoundField DataField="statusid" SortExpression="statusid" HeaderText="Status" />
            <asp:BoundField DataField="reqdate" SortExpression="reqdate" HeaderText="Req.Date" DataFormatString="{0:dd/MM/yyyy}"/>
            <asp:BoundField DataField="approvedate" SortExpression="approvedate" HeaderText="Approve Date" DataFormatString="{0:dd/MM/yyyy}"/>
            <asp:BoundField DataField="labdate" SortExpression="labdate" HeaderText="Lab Date" DataFormatString="{0:dd/MM/yyyy}"/>
            <asp:BoundField DataField="resultdate" SortExpression="resultdate" HeaderText="Result Date" DataFormatString="{0:dd/MM/yyyy}"/>
        </Columns>
        <EmptyDataTemplate>
            <div class="mygridempty">
                No Record Found.
            </div>
        </EmptyDataTemplate>
    </asp:GridView>
</asp:Panel>

</asp:Content>

