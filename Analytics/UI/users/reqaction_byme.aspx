<%@ Page Title="Analytics (V1.0)" Language="C#" MasterPageFile="~/UI/users/users.master" AutoEventWireup="true" CodeFile="reqaction_byme.aspx.cs" 
Inherits="UI_users_reqaction_byme" MaintainScrollPositionOnPostback="true" EnableViewState="true" EnableEventValidation="false"%>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
    
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <div>
        <asp:ToolkitScriptManager ID="ScriptManager1" runat="server">
        </asp:ToolkitScriptManager>        
    </div>
    <div>
        <asp:Panel ID="pnl_declined" runat="server">
            <table style="width: 100%;">
                <tr>
                    <td align="left" class="mypanel">
                        Receiver's Comment
                        <br />
                        <asp:TextBox ID="tb_comment4" runat="server" Font-Size="12px" ReadOnly="True" 
                            Rows="3" TextMode="MultiLine" Width="240px" />
                        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" 
                            TargetControlID="tb_comment4" WatermarkCssClass="watermark" 
                            WatermarkText="No Comment" />                        
                    </td>
                    <td align="right" class="mypanel" valign="bottom">
                        
                        <asp:Button ID="btn_cancel_request" runat="server" CausesValidation="False" 
                            Font-Bold="True" Height="28px" onclick="btn_cancel_request_Click" 
                            Text="Cancel Request" Width="100px" />
                        <asp:ConfirmButtonExtender ID="cbe_cancel" runat="server" 
                            ConfirmText="Are you sure you want to cancel this request? Cancelled request will be deleted." 
                            TargetControlID="btn_cancel_request" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btn_edit_request" runat="server" CausesValidation="False" 
                            Font-Bold="True" onclick="btn_edit_request_Click" Text="Edit Request" 
                            Width="100px" Height="28px" />                        
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="pnl_sendingtolab" runat="server">
            <table width="100%">
                <tr>
                    <td align="left" class="mypanel">
                        Receiver's Comment
                        <br />
                        <asp:TextBox ID="tb_comment_sendingLab" runat="server" Font-Size="12px" 
                            ReadOnly="True" Rows="3" TextMode="MultiLine" Width="240px" />
                        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender4" runat="server" 
                            TargetControlID="tb_comment_sendingLab" WatermarkCssClass="watermark" 
                            WatermarkText="No Comment" />                        
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="pnl_inlab" runat="server" >
            <%--<table width="100%">
                <tr>
                    <td align="left" class="mypanel">
                        Comment for Lab
                        <br />
                        <asp:TextBox ID="tb_comment_fromLab" runat="server" Font-Size="12px" 
                            ReadOnly="True" Rows="3" TextMode="MultiLine" Width="240px"></asp:TextBox>
                        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server" 
                            TargetControlID="tb_comment_fromLab" WatermarkCssClass="watermark" 
                            WatermarkText="No Comment">
                        </asp:TextBoxWatermarkExtender>
                    </td>
               </tr>                                
            </table>--%>
        </asp:Panel>
        <asp:Panel ID="pnl_vieweditresult" runat="server">
            <table width="100%">                 
                <tr>
                    <td> 
                    <div class="pageTitle">
                        <table style="width: 100%;" cellpadding="0" cellspacing="0">
                            <tr>
                                <td align="left">
                                    Lab Result    
                                </td>
                                <td align="right">                                    
                                    Result Date:
                                    <asp:Label ID="lbl_resultdate" runat="server"></asp:Label>                                    
                                </td>                                
                            </tr>                            
                        </table>                        
                    </div>                       
                    <div style="margin: 4px">
                        <asp:GridView ID="gv_view_result" runat="server" AutoGenerateColumns="False" CssClass="mygrid" 
                            DataKeyNames="labresult_id" RowStyle-HorizontalAlign="Left" Width="100%" 
                            CellPadding="2" onrowdatabound="gv_view_result_RowDataBound" >
                            <RowStyle Font-Bold="True" Height="30px" HorizontalAlign="Left" />                            
                            <HeaderStyle CssClass="mygridheader" />
                            <Columns>
                                <asp:BoundField DataField="labresult_id" HeaderText="labresult_id" Visible="false"/>
                                <asp:TemplateField HeaderText="S.No">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="25px" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="testname" HeaderText="Test"/>
                                <asp:TemplateField HeaderText="Sample">
                                    <ItemTemplate>
                                        <%# Eval("sampleid").ToString().Substring(8, 2)%>
                                    </ItemTemplate>                                    
                                    <ItemStyle HorizontalAlign="Center" Width="46px" />
                                </asp:TemplateField>
                                <%--<asp:BoundField DataField="labname" HeaderText="Lab"/>--%>
                                <asp:BoundField DataField="mth_ref" HeaderText="Method Ref."/>
                                <asp:BoundField DataField="result" HeaderText="Result"/>
                                <asp:TemplateField HeaderText="Attachment<br>(max 20MB)">
                                    <ItemTemplate>                                        
                                        <asp:HyperLink ID="hlink_attach" runat="server" Text='<%#Bind("attachment_id")%>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" Width="220px"/>                                    
                                </asp:TemplateField>                                
                            </Columns>
                        </asp:GridView>
                    </div>
                    </td>
               </tr>
               <tr>
                    <td align="left" class="mypanel">
                        Remark<br />
                        <asp:TextBox ID="tb_remark_fromResult" runat="server" Font-Size="12px" 
                            Width="300px"></asp:TextBox>
                        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" 
                            TargetControlID="tb_remark_fromResult" WatermarkCssClass="watermark" 
                            WatermarkText="No Remark">
                        </asp:TextBoxWatermarkExtender>
                        <br />
                        Comment
                        <br />
                        <asp:TextBox ID="tb_comment_fromResult" runat="server" 
                            ReadOnly="True" Rows="4" TextMode="MultiLine" Width="300px" 
                            Font-Size="12px"></asp:TextBox>
                        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" 
                            TargetControlID="tb_comment_fromResult" WatermarkCssClass="watermark" 
                            WatermarkText="No Comment">
                        </asp:TextBoxWatermarkExtender>
                    </td>
               </tr>
            </table>
        </asp:Panel>
    </div>
    <br />
    <div>
        <asp:Panel ID="PnlTitle1" runat="server" CssClass="collapsePanelHeader">
            <asp:Image ID="Image1" runat="server" ImageUrl="~/image/expand_blue.jpg" ></asp:Image>                    
            <asp:Label ID="lbl_title1" runat="server" Text="Show Details" ></asp:Label>
            <asp:Label ID="lbl_requestor" runat="server" ></asp:Label>
        </asp:Panel>        
        <asp:Panel ID="PnlContent1" runat="server" CssClass="collapsePanel">        
        <div align="right">
            <asp:HyperLink ID="hl_print_request" runat="server" Target="_blank">Print Request</asp:HyperLink>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:HyperLink ID="hl_pdf_request" runat="server" Target="_blank">Save Request to PDF</asp:HyperLink>
        </div>
        <asp:Panel ID="Panel1" runat="server" CssClass="mypanel">
            <table style="width:100%;" cellpadding="0">
                <tr>
                    <td align="left">
                        Project Name : &nbsp;&nbsp;                        
                        <asp:Label ID="lbl_project" runat="server" CssClass="mylabel"></asp:Label>
                    </td>
                    <td align="left">
                        Project type: <asp:Label ID="lbl_prj_type" runat="server" CssClass="mylabel"></asp:Label>
                    </td>
                    <td align="left">
                        Type Of Analysis :&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;                        
                        <asp:Label ID="lbl_typeanalysis" runat="server" CssClass="mylabel"></asp:Label>
                    </td>
                    <td align="left">
                        Request No :&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Label ID="lbl_requestid" runat="server" CssClass="mylabel"></asp:Label>
                    </td>
                    <td align="right" class="status" valign="top">
                        <asp:Label ID="lbl_status" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="left">                        
                        Project Category: <asp:Label ID="lbl_prj_category" runat="server" CssClass="mylabel"></asp:Label>
                    </td>
                    <td align="left">
                        Project Brand: <asp:Label ID="lbl_prj_brand" runat="server" CssClass="mylabel"></asp:Label>
                    </td>
                    <td align="left">
                        AS Proj Lead/ Designate :&nbsp; &nbsp;&nbsp;                        
                        <asp:Label ID="lbl_lead" runat="server" CssClass="mylabel"></asp:Label>
                    </td>
                    <td align="left">
                        Request Type :&nbsp; &nbsp;                        
                        <asp:Label ID="lbl_typerequest" runat="server" CssClass="mylabel"></asp:Label>
                    </td>
                    <td align="left">
                        &nbsp;</td>
                </tr>                               
            </table>
        </asp:Panel>
        <asp:Panel ID="Panel2" runat="server" CssClass="mypanel" GroupingText="Samples">
            <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Width="100%">
                <asp:TabPanel ID="TabPanel1" runat="server"><HeaderTemplate><div style="width: 44px; font-weight: bold; color: #000000; font-size: 14px;">1-4 </div></HeaderTemplate><ContentTemplate><asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                            Width="100%" onrowcreated="GridView_RowCreated" CssClass="mygrid"><HeaderStyle CssClass="mygridheader"/><Columns><asp:BoundField HeaderText="ID" DataField="propertyid" ><ItemStyle Width="20px" /></asp:BoundField><asp:BoundField HeaderText="Property" DataField="propertyname" ><ItemStyle Width="150px" /></asp:BoundField><asp:BoundField HeaderText="Sample1" DataField="1" /><asp:BoundField HeaderText="Sample2" DataField="2" /><asp:BoundField HeaderText="Sample3" DataField="3" /><asp:BoundField HeaderText="Sample4" DataField="4" /></Columns></asp:GridView></ContentTemplate></asp:TabPanel>                    
                <asp:TabPanel ID="TabPanel2" runat="server" ><HeaderTemplate><div style="width: 44px; font-weight: bold; color: #000000; font-size: 14px;">5-8 </div></HeaderTemplate><ContentTemplate><asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
                            Width="100%" onrowcreated="GridView_RowCreated" CssClass="mygrid"><HeaderStyle CssClass="mygridheader"/><Columns><asp:BoundField HeaderText="ID" DataField="propertyid" ItemStyle-Width="20px" /><asp:BoundField HeaderText="Property" DataField="propertyname" ItemStyle-Width="150px" /><asp:BoundField HeaderText="Sample5" DataField="5" /><asp:BoundField HeaderText="Sample6" DataField="6" /><asp:BoundField HeaderText="Sample7" DataField="7" /><asp:BoundField HeaderText="Sample8" DataField="8" /></Columns></asp:GridView></ContentTemplate></asp:TabPanel>
                <asp:TabPanel ID="TabPanel3" runat="server"><HeaderTemplate><div style="width: 44px; font-weight: bold; color: #000000; font-size: 14px;">9-12 </div></HeaderTemplate><ContentTemplate><asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" 
                            Width="100%" onrowcreated="GridView_RowCreated" CssClass="mygrid"><HeaderStyle CssClass="mygridheader"/><Columns><asp:BoundField HeaderText="ID" DataField="propertyid" ItemStyle-Width="20px" /><asp:BoundField HeaderText="Property" DataField="propertyname" ItemStyle-Width="150px" /><asp:BoundField HeaderText="Sample9" DataField="9" /><asp:BoundField HeaderText="Sample10" DataField="10" /><asp:BoundField HeaderText="Sample11" DataField="11" /><asp:BoundField HeaderText="Sample12" DataField="12" /></Columns></asp:GridView></ContentTemplate></asp:TabPanel>
                <asp:TabPanel ID="TabPanel4" runat="server"><HeaderTemplate><div style="width: 44px; font-weight: bold; color: #000000; font-size: 14px;">13-16 </div></HeaderTemplate><ContentTemplate><asp:GridView ID="GridView4" runat="server" AutoGenerateColumns="False" 
                            Width="100%" onrowcreated="GridView_RowCreated" CssClass="mygrid"><HeaderStyle CssClass="mygridheader"/><Columns><asp:BoundField HeaderText="ID" DataField="propertyid" ItemStyle-Width="20px" /><asp:BoundField HeaderText="Property" DataField="propertyname" ItemStyle-Width="150px" /><asp:BoundField HeaderText="Sample13" DataField="13" /><asp:BoundField HeaderText="Sample14" DataField="14" /><asp:BoundField HeaderText="Sample15" DataField="15" /><asp:BoundField HeaderText="Sample16" DataField="16" /></Columns></asp:GridView></ContentTemplate></asp:TabPanel>
                <asp:TabPanel ID="TabPanel5" runat="server" ><HeaderTemplate><div style="width: 44px; font-weight: bold; color: #000000; font-size: 14px;">17-20 </div></HeaderTemplate><ContentTemplate><asp:GridView ID="GridView5" runat="server" AutoGenerateColumns="False" 
                            Width="100%" onrowcreated="GridView_RowCreated" CssClass="mygrid"><HeaderStyle CssClass="mygridheader"/><Columns><asp:BoundField HeaderText="ID" DataField="propertyid" ItemStyle-Width="20px" /><asp:BoundField HeaderText="Property" DataField="propertyname" ItemStyle-Width="150px" /><asp:BoundField HeaderText="Sample17" DataField="17" /><asp:BoundField HeaderText="Sample18" DataField="18" /><asp:BoundField HeaderText="Sample19" DataField="19" /><asp:BoundField HeaderText="Sample20" DataField="20" /></Columns></asp:GridView></ContentTemplate></asp:TabPanel>
                <asp:TabPanel ID="TabPanel6" runat="server" ><HeaderTemplate><div style="width: 44px; font-weight: bold; color: #000000; font-size: 14px;">21-24 </div></HeaderTemplate><ContentTemplate><asp:GridView ID="GridView6" runat="server" AutoGenerateColumns="False" 
                            Width="100%" onrowcreated="GridView_RowCreated" CssClass="mygrid"><HeaderStyle CssClass="mygridheader"/><Columns><asp:BoundField HeaderText="ID" DataField="propertyid" ItemStyle-Width="20px" /><asp:BoundField HeaderText="Property" DataField="propertyname" ItemStyle-Width="150px" /><asp:BoundField HeaderText="Sample21" DataField="21" /><asp:BoundField HeaderText="Sample22" DataField="22" /><asp:BoundField HeaderText="Sample23" DataField="23" /><asp:BoundField HeaderText="Sample24" DataField="24" /></Columns></asp:GridView></ContentTemplate></asp:TabPanel>
            </asp:TabContainer>
        </asp:Panel>
        <asp:Panel ID="Panel3" runat="server" CssClass="mypanel" 
            GroupingText="Additional Information (1000 characters)">
            <asp:TextBox ID="tb_addinfo" runat="server" TextMode="MultiLine" Rows="4" Width="100%" 
                onmouseover="this.style.backgroundColor='#EAF5FB'" onmouseout="this.style.backgroundColor='white'" ReadOnly="True">
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            </asp:TextBox>
        </asp:Panel>
        <asp:Panel ID="Panel4" runat="server" CssClass="mypanel" GroupingText="Tests">
            <asp:GridView ID="gv_tests" runat="server" AutoGenerateColumns="False" DataKeyNames="test_id"
                Width="100%" onrowcreated="gv_tests_RowCreated" CssClass="mygrid">
            <HeaderStyle CssClass="mygridheader"/>
                <Columns>
                    <asp:BoundField DataField="test_id" HeaderText="ID" Visible="false"/>                    
                    <asp:BoundField DataField="testname" HeaderText="Test Parameter" />
                    <asp:BoundField DataField="reference" HeaderText="Reference" />
                    <asp:BoundField DataField="standard" HeaderText="Standard/Limit" />
                    <asp:BoundField DataField="unit" HeaderText="Unit" />
                </Columns>
            </asp:GridView>                        
        </asp:Panel>
        <asp:Panel ID="Panel5" runat="server" CssClass="mypanel" GroupingText="Test-Samples">
            <asp:GridView ID="gv_test_sample" runat="server" Width="100%" CssClass="mygrid" 
                DataKeyNames="Test" EnableViewState="true">
                <HeaderStyle CssClass="mygridheader"/>                
                <Columns>                
                </Columns>
            </asp:GridView>
        </asp:Panel>
        </asp:Panel>
        <asp:CollapsiblePanelExtender ID="CollapsiblePanelExtender1" runat="server"
                TargetControlID="PnlContent1" ExpandControlID="PnlTitle1" CollapseControlID="PnlTitle1"
                TextLabelID="lbl_title1" CollapsedText="Show Details" ExpandedText="Hide Details"
                ImageControlID="Image1" ExpandedImage="~/image/collapse_blue.jpg" CollapsedImage="~/image/expand_blue.jpg"
                Collapsed="True" SuppressPostBack="true">
        </asp:CollapsiblePanelExtender>
    </div>
</asp:Content>

