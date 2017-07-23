<%@ Page Title="Analytics (V1.0)" Language="C#" MasterPageFile="~/UI/admin/admin.master" AutoEventWireup="true" CodeFile="prj_requests.aspx.cs" 
    Inherits="UI_admin_prj_requests" MaintainScrollPositionOnPostback="true"%>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<asp:Panel ID="pnl_prj" runat="server" class="mypanel">
    <div>
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
    </div>
    <table style="width: 100%;" cellpadding="0">        
        <tr>
            <td width="120px">
                Project Name</td>
            <td width="210px">
                <asp:Label ID="lbl_name" runat="server" CssClass="mylabel"></asp:Label>
            </td>
            <td width="120px">
                Project Type</td>
            <td>
                <asp:Label ID="lbl_type" runat="server" CssClass="mylabel"></asp:Label>
            </td>
            <td align="right">
                <asp:HyperLink ID="hl_back" runat="server" 
                    NavigateUrl="~/UI/admin/projects.aspx"><< Back</asp:HyperLink>
            </td>
        </tr>
        <tr>
            <td>                
                Project Category</td>
            <td>                
                <asp:Label ID="lbl_category" runat="server" CssClass="mylabel"></asp:Label>
                
            </td>
            <td>
                
                Project Brand</td>
            <td>                
                <asp:Label ID="lbl_brand" runat="server" CssClass="mylabel"></asp:Label>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>                
                Start Date</td>
            <td>                
                <asp:Label ID="lbl_start" runat="server" CssClass="mylabel"></asp:Label>                
            </td>
            <td>                
                End Date</td>
            <td>                
                <asp:Label ID="lbl_end" runat="server" CssClass="mylabel"></asp:Label>                
            </td>
            <td>
                &nbsp;</td>
        </tr>
         <tr>
            <td>                
                Budget</td>
            <td>                
                <asp:Label ID="lbl_budget" runat="server" CssClass="mylabel"></asp:Label>
            </td>
            <td>
                
                Active</td>
            <td>
                
                <asp:CheckBox ID="cb_active" runat="server" Enabled="False" />
                
            </td>
             <td>
                 &nbsp;</td>
        </tr>
    </table>
</asp:Panel>

<asp:Panel ID="pnl_requests" runat="server" class="mypanel">
    <strong>Project Requests</strong>
    <asp:GridView ID="gv_requests" runat="server" AutoGenerateColumns="False" 
        CellPadding="2" CssClass="mygrid" DataKeyNames="reqid" 
        RowStyle-HorizontalAlign="Left" Width="100%" AllowSorting="True" 
        onrowdatabound="gv_requests_RowDataBound" onsorting="gv_requests_Sorting" >
        <RowStyle  HorizontalAlign="Left" />
        <HeaderStyle CssClass="mygridheader" />
        <Columns>            
            <asp:TemplateField HeaderText="S.No">
                <ItemTemplate>
                    <%# Container.DataItemIndex + 1 %>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="25px" />
            </asp:TemplateField>
            <asp:BoundField DataField="reqid" SortExpression="reqid" HeaderText="Req. Id" />
            <asp:BoundField DataField="analysistype" SortExpression="analysistype" HeaderText="Analysis Type" />
            <asp:BoundField DataField="reqtype" SortExpression="reqtype" HeaderText="Req. Type" />
            <asp:BoundField DataField="reqcategory" SortExpression="reqcategory" HeaderText="Req. Category" Visible="false"/>
            <asp:BoundField DataField="reqfrom" SortExpression="reqfrom" HeaderText="From" />            
            <asp:BoundField DataField="responsible" SortExpression="responsible" HeaderText="To"/>
            <asp:BoundField DataField="reqdate" SortExpression="reqdate" HeaderText="Req.Date" DataFormatString="{0:dd/MM/yyyy}" />
            <asp:BoundField DataField="statusid" SortExpression="statusid" HeaderText="Status"/>            
            <asp:TemplateField HeaderText="Action">
                <ItemTemplate>
                    <asp:ImageButton ID="imgbtn_View" runat="server"
                        CausesValidation="false" ImageUrl="~/image/view.png" AlternateText="View" 
                        ToolTip="View Request Detail" />
                    &nbsp;&nbsp;
                    <asp:ImageButton ID="imgbtn_responsible" runat="server"
                        CausesValidation="false" ImageUrl="~/image/employee.jpg" AlternateText="Request To" 
                        ToolTip="Change Request To (Lead/ Designate)" onclick="imgbtn_responsible_Click" />
                    &nbsp;&nbsp;                    
                </ItemTemplate>                    
            </asp:TemplateField>
        </Columns>
        <EmptyDataTemplate>
            <div class="mygridempty">
                No Request Found under this Project.
            </div>
        </EmptyDataTemplate>
    </asp:GridView>
</asp:Panel>

<asp:Panel ID="panel_chng_resp" runat="server" Width="400px" CssClass="mypanel" DefaultButton="btnSave">
<asp:UpdatePanel ID="updatepanel_change_resp" runat="server" UpdateMode="Conditional">                
    <ContentTemplate>                            
    <asp:ModalPopupExtender ID="pnl_MPExt" runat="server" 
        TargetControlID="LinkButtonTarget" PopupControlID="panel_chng_resp" Drag="true"
        CancelControlID="btnCancel" BackgroundCssClass="modalBackground" PopupDragHandleControlID="panel_chng_resp">
    </asp:ModalPopupExtender>
    <div>
        <table style="width: 100%;">
            <tr>
                <td>
                    RequestID :
                    <asp:Label ID="lbl_reqid" runat="server" CssClass="mylabel"></asp:Label>
                </td>
                <td>
                    From :
                    <asp:Label ID="lbl_reqfrom" runat="server" CssClass="mylabel"></asp:Label>
                </td>
                <td>
                    To :
                    <asp:Label ID="lbl_reqto" runat="server" CssClass="mylabel"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="3" height="20px">
                </td>
            </tr>
            <tr>
                <td align="center" colspan="3">                  
                    Change Request To :
                    <asp:DropDownList ID="ddl_chng_reqto" runat="server">
                    </asp:DropDownList>                  
                </td>
            </tr>
            <tr>
                <td align="center" colspan="3" height="20px">
                    &nbsp;</td>
            </tr>
        </table>
    </div>
    <div align="center" style="background-color: #C0C0C0; ">
        <asp:Button ID="btnSave" runat="server" Text="Save" CausesValidation="true" onclick="btnSave_Click"/>  
        &nbsp;&nbsp;&nbsp;&nbsp;
        <asp:LinkButton ID="LinkButtonTarget" runat="server" ForeColor="#C0C0C0">.</asp:LinkButton>
        &nbsp;&nbsp;&nbsp;&nbsp;                               
        <asp:Button ID="btnCancel" runat="server" Text="Cancel" 
            CausesValidation="false" onclick="btnCancel_Click"/>        
    </div> 
    </ContentTemplate>
</asp:UpdatePanel>
</asp:Panel>

</asp:Content>

