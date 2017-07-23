<%@ Page Title="Analytics (V1.0)" Language="C#" MasterPageFile="~/UI/users/users.master" AutoEventWireup="true" CodeFile="home.aspx.cs" 
    Inherits="UI_users_home" MaintainScrollPositionOnPostback="true" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
    function transferLink(url) 
    {
        var str = window.location.href;
        window.location.replace(str.replace("home.aspx", url));
    }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">    
    <table style="width:100%;" cellpadding="0" cellspacing="0">
        <tr>
            <td width="70%">                
                <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
                </asp:ToolkitScriptManager>
                <asp:Panel ID="Panel1" runat="server" CssClass="mypanel">
                <div align="right">
                    <table style="width: 100%;" class="pageTitle" cellpadding="0" cellspacing="0">
                        <tr>
                            <td align="left">
                                Home
                            </td>
                            <td>
                                <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                                    ConnectionString="<%$ ConnectionStrings:analyticsConnectionString %>" 
                                    SelectCommand="SELECT DISTINCT * FROM [m_status] ORDER BY [statusid]">
                                </asp:SqlDataSource>
                            </td>
                            <td align="right">
                                Filter by Request Status
                                <asp:DropDownList ID="ddl_status" runat="server" AutoPostBack="True" 
                                    DataSourceID="SqlDataSource1" DataTextField="statustext" 
                                    DataValueField="statusid" AppendDataBoundItems="True" 
                                    onselectedindexchanged="ddl_status_SelectedIndexChanged">
                                    <asp:ListItem Value="*">All</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                    
                </div>
                <div align="left">
                <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" 
                        Width="100%" >
                    <asp:TabPanel ID="TabPanel1" runat="server">
                    <HeaderTemplate>
                            <div style="font-weight: bold; color: #000000; font-size: 14px; ">
                                Requests To Me
                            </div>
                    </HeaderTemplate>
                    <ContentTemplate>
                        <asp:Panel ID="Panel2" runat="server" ScrollBars="Vertical" Height="400px">                    
                        <asp:GridView ID="gv_req_to_me" runat="server" AutoGenerateColumns="False" 
                            Width="100%" CssClass="mygrid" 
                            AllowSorting="True" onrowdatabound="gv_req_to_me_RowDataBound"
                            DataKeyNames="reqid" onsorting="gv_req_to_me_Sorting" CellPadding="2" 
                                EnableModelValidation="True" GridLines="Horizontal">
                            <HeaderStyle CssClass="mygridheader"/>
                            <EmptyDataTemplate>
                                <div style="height: 370px">
                                
                                </div>
                            </EmptyDataTemplate>
                            <Columns>                                
                                <asp:BoundField DataField="reqid" HeaderText="Req.No" SortExpression="reqid" />
                                <asp:BoundField DataField="analysistype" HeaderText="Analysis Type" SortExpression="analysistype" />
                                <asp:BoundField DataField="reqtype" HeaderText="Request Type" 
                                    SortExpression="reqtype" Visible="False"/>
                                <asp:BoundField DataField="reqfrom" HeaderText="From" SortExpression="reqfrom" />
                                <asp:BoundField DataField="reqdate" HeaderText="Request Date" SortExpression="reqdate" />
                                <asp:BoundField DataField="statusid" HeaderText="Status" SortExpression="statusid" />
                            </Columns>
                        </asp:GridView>
                        </asp:Panel>
                    </ContentTemplate>
                    </asp:TabPanel>
                    <asp:TabPanel ID="TabPanel2" runat="server">
                    <HeaderTemplate>
                            <div style="font-weight: bold; color: #000000; font-size: 14px; ">
                                Requests By Me
                            </div>
                    </HeaderTemplate>
                    <ContentTemplate>
                        <asp:Panel ID="Panel3" runat="server" ScrollBars="Vertical" Height="400px">                    
                        <asp:GridView ID="gv_req_by_me" runat="server" AutoGenerateColumns="False" 
                            Width="100%" CssClass="mygrid" OnRowCreated="gv_req_by_me_RowCreated"
                            AllowSorting="True" onrowdatabound="gv_req_by_me_RowDataBound" 
                            DataKeyNames="reqid"  CellPadding="2" OnSorting="gv_req_by_me_Sorting" 
                                EnableModelValidation="True" GridLines="Horizontal">
                            <HeaderStyle CssClass="mygridheader"/>
                            <EmptyDataTemplate>
                                <div style="height: 370px">
                                
                                </div>
                            </EmptyDataTemplate>
                            <Columns>                                                              
                                <asp:BoundField DataField="reqid" HeaderText="Req.No" SortExpression="reqid" />
                                <asp:BoundField DataField="analysistype" HeaderText="Analysis Type" SortExpression="analysistype" />
                                <asp:BoundField DataField="reqtype" HeaderText="Request Type" 
                                    SortExpression="reqtype" Visible="False"/>
                                <asp:BoundField DataField="responsible" HeaderText="To" SortExpression="responsible" />
                                <asp:BoundField DataField="reqdate" HeaderText="Request Date" SortExpression="reqdate" />
                                <asp:BoundField DataField="statusid" HeaderText="Status" SortExpression="statusid" />
                                <asp:TemplateField >
                                    <ItemTemplate>
                                        <asp:HoverMenuExtender ID="ahm_1" runat="Server" PopupControlID="PopupMenu" 
                                            PopupPosition="Right" OffsetX="-20" OffsetY="0" PopDelay="50" 
                                            HoverDelay="0" />
                                        <asp:Panel ID="PopupMenu" runat="server" CssClass="hoverPanel" >                                            
                                            <asp:HyperLink ID="hl_copy" runat="server" Text="Copy Request" Height="24px" ToolTip="Copy this request to make a new request"/>
                                        </asp:Panel>
                                    </ItemTemplate>                                                                        
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        </asp:Panel>
                    </ContentTemplate>
                    </asp:TabPanel>
                </asp:TabContainer>
                </div>
                </asp:Panel>
            </td>
            <td width="30%" align="left" valign="top">
                <asp:Panel ID="pnl_count" runat="server">
                    <asp:GridView ID="gv_count" runat="server" AutoGenerateColumns="False" CellPadding="2" 
                        CssClass="mygrid" DataKeyNames="statusid" Width="100%" ShowFooter="True" ondatabound="gv_count_DataBound" >
                        <HeaderStyle CssClass="mygridheader" />
                        <FooterStyle CssClass="mygridfooter" Font-Bold="True" BackColor="WhiteSmoke" />
                        <Columns>
                            <asp:BoundField DataField="statusid" HeaderText="ID" Visible="False" />
                            <asp:BoundField DataField="statustext" HeaderText="Status" />
                            <asp:BoundField DataField="count_tome" HeaderText="Requests to me" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="count_byme" HeaderText="Requests by me" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center" />
                        </Columns>
                    </asp:GridView>
                </asp:Panel>
            </td>
        </tr>
        </table>
</asp:Content>

