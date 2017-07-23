<%@ Page Title="Analytics (V1.0)" Language="C#" MasterPageFile="~/UI/users/users.master" AutoEventWireup="true" CodeFile="reqaction.aspx.cs"
 Inherits="UI_users_reqaction" MaintainScrollPositionOnPostback="true" EnableViewState="true" EnableEventValidation="false"%>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script language="javascript" type="text/javascript">        
        function mouse_move(rindex, cindex, action) 
        {       
            var gv = document.getElementById("<%=gv_ts_lab_selection.ClientID %>");     
            if (action == 'over') 
            {
                gv.rows[rindex + 1].cells[0].style.backgroundColor = '#EEE8AA';
                gv.rows[0].cells[cindex - 1].style.backgroundColor = '#EEE8AA';
            }
            else if (action == 'out') 
            {
                gv.rows[rindex + 1].cells[0].style.backgroundColor = '#F2F2F2';
                gv.rows[0].cells[cindex - 1].style.backgroundColor = '#DCDCDC';
            } 
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <div>
        <asp:ToolkitScriptManager ID="ScriptManager1" runat="server">
        </asp:ToolkitScriptManager>        
    </div>
    <div>
        <asp:Panel ID="pnl_requested" runat="server">
            <table style="width: 100%;">
                <tr>
                    <td align="left" class="mypanel">                        
                        <asp:TextBox ID="tb_comment" runat="server" Rows="3" TextMode="MultiLine" 
                            Width="260px" Font-Size="12px"></asp:TextBox>
                        <asp:TextBoxWatermarkExtender ID="tb_comment_TextBoxWatermarkExtender" 
                            runat="server" TargetControlID="tb_comment" WatermarkCssClass="watermark" 
                            WatermarkText="Write a comment for requester (optional)">
                        </asp:TextBoxWatermarkExtender>
                        &nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btn_approve" runat="server" Font-Bold="True" 
                            ForeColor="#006600" Height="30px" onclick="btn_approve_Click" Text="Approve" 
                            Width="80px" ToolTip="Approve this request" />
                        <asp:ConfirmButtonExtender ID="cbe_approve" runat="server" 
                            ConfirmText="Are you sure you want to Approve this request? After approval the samples can be send to Lab." 
                            TargetControlID="btn_approve" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btn_decline" runat="server" Font-Bold="True" ForeColor="Red" 
                            Height="30px" onclick="btn_decline_Click" Text="Decline" Width="80px" 
                            ToolTip="Decline this request" />
                        <asp:ConfirmButtonExtender ID="cbe_decline" runat="server" 
                            ConfirmText="Are you sure you want to Decline this request to the user to resubmit?" 
                            TargetControlID="btn_decline" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="pnl_declined" runat="server">
            <table style="width: 100%;">
                <tr>
                    <td align="left" class="mypanel">
                        Receiver's Comment<br />
                        <asp:TextBox ID="tb_comment4" runat="server" Font-Size="12px"
                            ReadOnly="True" Rows="3" TextMode="MultiLine" Width="260px"></asp:TextBox>
                        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" 
                            TargetControlID="tb_comment4" WatermarkCssClass="watermark" 
                            WatermarkText="No Comment">
                        </asp:TextBoxWatermarkExtender>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="pnl_sendingtolab" runat="server">
            <table width="100%" cellspacing="5">
                <%--<tr>
                    <td align="left" class="mypanel">                                             
                        Receiver's Comment<br />
                        <asp:TextBox ID="tb_comment_sendingLab" runat="server" ReadOnly="True" Rows="3" TextMode="MultiLine" 
                            Width="260px" Font-Size="12px"></asp:TextBox>
                        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender4" runat="server" 
                            TargetControlID="tb_comment_sendingLab" WatermarkCssClass="watermark" 
                            WatermarkText="No Comment">
                        </asp:TextBoxWatermarkExtender>                        
                    </td>
                </tr>--%>
                <tr>
                    <td align="left" class="mypanel">
                        <asp:Panel ID="pnl_sendinglab_test_sample1" runat="server" 
                            GroupingText="Select Test-Sample for Lab" ScrollBars="Both" >
                            <asp:GridView ID="gv_ts_lab_selection" runat="server" CssClass="mygrid" AutoGenerateColumns="False"
                                DataKeyNames="test_id" EnableViewState="true"
                                onrowdatabound="gv_ts_lab_selection_RowDataBound" onrowcommand="gv_ts_lab_selection_RowCommand">
                                <HeaderStyle CssClass="mygridheader" />
                                <Columns>
                                    <asp:BoundField DataField="test_id" HeaderText="TestId" ReadOnly="True" Visible="False" />
                                    <asp:BoundField DataField="testname" HeaderText="Test↓/Sample→" ReadOnly="True" ItemStyle-BackColor="#EBEBEB"/>
                                </Columns>
                            </asp:GridView>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td class="mypanel" align="left">
                        <asp:Panel ID="pnl_sendinglab_test_sample2" runat="server" 
                            GroupingText="Select Lab and Method Reference" >
                            <asp:GridView ID="gv_labs" runat="server" CssClass="mygrid" DataKeyNames="ts_id"
                                AutoGenerateColumns="False" ShowFooter="True" Width="100%"
                                onrowdatabound="gv_labs_RowDataBound" onrowcommand="gv_labs_RowCommand" >
                                <RowStyle HorizontalAlign="Left" Height="30px" Font-Bold="True" />
                                <HeaderStyle CssClass="mygridheader" />
                                <FooterStyle HorizontalAlign="Left" BackColor="#EBEBEB"/>
                                <EmptyDataRowStyle HorizontalAlign="Left" />
                                <Columns>                                                                         
                                    <asp:TemplateField HeaderText="ts_id" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("ts_id") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle BackColor="#F2F2F2" Width="30px" />
                                        <FooterTemplate>
                                            <asp:Label ID="lbl_foot_tsid" runat="server"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="S.No">
                                        <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" Width="2%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Test">
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sample">
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Lab">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_labid" runat="server" Text='<%# Eval("labid")%>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:ObjectDataSource ID="ods_labs" runat="server" SelectMethod="GetAllActiveLabs" 
                                                TypeName="ds_analyticsTableAdapters.m_labsTableAdapter">                                                                                                                                              
                                            </asp:ObjectDataSource>
                                            <asp:DropDownList ID="dd_labid" runat="server" DataTextField="labname" 
                                                DataValueField="labid" DataSourceID="ods_labs" 
                                                BackColor="#FFFFCC" ondatabound="dd_lab_DataBound">
                                            </asp:DropDownList>
                                        </FooterTemplate>                                        
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Method Reference">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_mr" runat="server" Text='<%# Eval("mth_ref").ToString() %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="tb_mr" runat="server" BackColor="#FFFFCC"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_mr" runat="server" 
                                                ControlToValidate="tb_mr" ErrorMessage="*" CssClass="failureNotification" 
                                                ValidationGroup="LabFooter">
                                            </asp:RequiredFieldValidator>
                                        </FooterTemplate>                                        
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action" ItemStyle-Width="40px">
                                        <ItemTemplate>
                                           <asp:ImageButton ID="imgbtn_Delete" runat="server"
                                                CausesValidation="false" ImageUrl="~/image/dustbin_icon.jpg" 
                                                AlternateText="Delete" ToolTip="Delete" CommandName="del" />
                                        </ItemTemplate>
                                        <FooterTemplate>
                                           <asp:Button Text="Insert" CausesValidation="true" runat="server" 
                                            ID="btnInsert_Footer" ValidationGroup="LabFooter" 
                                                onclick="btnInsert_Footer_Click" ForeColor="#CC0000" />                       
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                     </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>                                                                    
                                    <table style="width:100%;" border="1px" cellspacing="0">
                                        <tr class="mygridheader">
                                            <td>Test</td>
                                            <td>Sample</td>
                                            <td>Lab</td>
                                            <td>Method Reference</td>
                                            <td>Action</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lbl_empty_ts_id" runat="server" Visible="false"></asp:Label>
                                                <asp:Label ID="lbl_empty_test" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_empty_sample" runat="server"></asp:Label>
                                            </td>
                                            <td>                                                
                                                <asp:DropDownList ID="dd_empty_lab" runat="server" DataTextField="labname" 
                                                    DataValueField="labid" BackColor="#FFFFCC" 
                                                    ondatabound="dd_lab_DataBound">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="tb_empty_mr" runat="server" BackColor="#FFFFCC"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfv_empty_mr" runat="server" 
                                                    ControlToValidate="tb_empty_mr" ErrorMessage="*" CssClass="failureNotification" 
                                                    ValidationGroup="LabEmpty">
                                            </asp:RequiredFieldValidator>
                                            </td>
                                            <td>
                                                <asp:Button ID="btn_empty_insert" runat="server" 
                                                    onclick="btn_empty_insert_Click" Text="Insert" ValidationGroup="LabEmpty" 
                                                    ForeColor="#CC0000" />
                                            </td>
                                        </tr>
                                    </table>
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </asp:Panel>
                    </td>
                </tr>                
                <tr>
                    <td align="left" class="mypanel">
                        <asp:Panel ID="pnl_sendtolab" runat="server" 
                            GroupingText="Enter quantity of Sample per Lab" >
                            <table width="100%">
                                <tr>
                                    <td style="width: 60%">
                                        <asp:GridView ID="gv_lab_sample_quantity" runat="server" CssClass="mygrid" 
                                            AutoGenerateColumns="False" Width="90%" DataKeyNames="labid" 
                                            onrowdatabound="gv_lab_sample_quantity_RowDataBound" AllowSorting="True" 
                                            onsorting="gv_lab_sample_quantity_Sorting" >
                                            <RowStyle HorizontalAlign="Left" Height="30px" Font-Bold="True" />
                                            <HeaderStyle CssClass="mygridheader" />
                                            <FooterStyle HorizontalAlign="Left" />
                                            <EmptyDataRowStyle HorizontalAlign="Left" />
                                            <Columns>
                                                <asp:BoundField DataField="labid" SortExpression="labid" HeaderText="LabID" Visible="false"/>
                                                <asp:BoundField DataField="labname" SortExpression="labname" HeaderText="Lab"/>
                                                <asp:BoundField DataField="sampleid" SortExpression="sampleid" HeaderText="Sample Code"/>                                    
                                                <asp:TemplateField HeaderText="Quantity">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="tb_qty" runat="server" BackColor="#FFFFCC"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfv_qty" runat="server" 
                                                            ControlToValidate="tb_qty" ErrorMessage="required" CssClass="failureNotification" 
                                                            ValidationGroup="quantity">
                                                        </asp:RequiredFieldValidator>
                                                    </ItemTemplate>                                        
                                                </asp:TemplateField>                                    
                                            </Columns>                               
                                        </asp:GridView>
                                    </td>
                                    <td style="width: 40%; vertical-align: bottom; text-align: right;">
                                        <asp:TextBox ID="tb_sendlab" runat="server" Rows="3" TextMode="MultiLine" 
                                            Width="240px" Font-Size="12px">
                                        </asp:TextBox>
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:TextBoxWatermarkExtender ID="tb_wme_sendlab" runat="server" 
                                            TargetControlID="tb_sendlab" WatermarkCssClass="watermark" 
                                            WatermarkText="Write a comment (optional)">
                                        </asp:TextBoxWatermarkExtender>
                                        <asp:Button ID="btn_sendlab" runat="server" CausesValidation="True" 
                                            Font-Bold="True" ForeColor="#006600" Height="30px" onclick="btn_sendlab_Click" 
                                            Text="Send to Lab" Width="80px" ToolTip="Send to Lab" ValidationGroup="quantity"/>
                                        <asp:ConfirmButtonExtender ID="btn_sendlab_cbe" runat="server" 
                                            ConfirmText="Are you sure all the labs are selected correctly to send the test samples?" 
                                            TargetControlID="btn_sendlab">
                                        </asp:ConfirmButtonExtender>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="pnl_inlab" runat="server">
            <table width="100%" cellspacing="6">
                <%--<tr align="right">
                    <td align="left" class="mypanel">
                        Comment for Lab<br />
                        <asp:TextBox ID="tb_comment_fromLab" runat="server" Font-Size="12px"
                            ReadOnly="True" Rows="3" TextMode="MultiLine" Width="240px"></asp:TextBox>
                        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server" 
                            TargetControlID="tb_comment_fromLab" WatermarkCssClass="watermark" 
                            WatermarkText="No Comment">
                        </asp:TextBoxWatermarkExtender>
                    </td>
               </tr>--%> 
                <tr>
                    <td class="mypanel">                    
                        <div class="pageTitle">
                            <table cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td align="left">Fill Results</td>
                                    <td  align="right" width="140px">
                                        <asp:HyperLink ID="hl_print_lab" runat="server" Target="_blank">Print Documents</asp:HyperLink>                                        
                                    </td>                                    
                                </tr>
                            </table>                            
                        </div>
                        <div style="margin: 4px">
                        <asp:GridView ID="gv_fill_result" runat="server" AutoGenerateColumns="False" 
                            CssClass="mygrid" DataKeyNames="labresult_id" 
                            RowStyle-HorizontalAlign="Left" Width="100%" CellPadding="2" 
                                onrowdatabound="gv_fill_result_RowDataBound" >
                            <RowStyle Font-Bold="True" Height="30px" HorizontalAlign="Left" />
                            <HeaderStyle CssClass="mygridheader" />
                            <Columns>
                                <asp:BoundField HeaderText="labresult_id" DataField="labresult_id" Visible="false"/>                                
                                <asp:TemplateField HeaderText="S.No">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="25px" />
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Test" DataField="testname" />
                                <asp:TemplateField HeaderText="Sample">
                                    <ItemTemplate>
                                        <%# Eval("sampleid").ToString().Substring(8, 2)%>                                        
                                    </ItemTemplate>                                    
                                    <ItemStyle HorizontalAlign="Center" Width="46px" />
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Lab" DataField="labname" />
                                <asp:BoundField HeaderText="Method Ref." DataField="mth_ref" />
                                <asp:TemplateField HeaderText="Result">
                                    <ItemTemplate>
                                        <asp:TextBox ID="tb_result" runat="server" Text='<%#Bind("result")%>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Publish<br>to Requestor">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="cb_forreport" runat="server" Checked='<%#Eval("isfor_report").ToString()=="True" %>'/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Attachment<br>(max 20MB)">
                                    <ItemTemplate>
                                        <asp:FileUpload ID="fl_up_attach" runat="server" />                                       
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
                        <br />
                        <asp:TextBox ID="tb_remark_result" runat="server" Width="300px" Font-Size="12px"></asp:TextBox>
                        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" 
                            runat="server" TargetControlID="tb_remark_result" 
                            WatermarkCssClass="watermark" WatermarkText="Write a remark (mandatory)">
                        </asp:TextBoxWatermarkExtender>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_rem_res" runat="server" 
                            ControlToValidate="tb_remark_result" ErrorMessage="Remark" CssClass="failureNotification" 
                            ValidationGroup="submit_result"> </asp:RequiredFieldValidator>
                        <br />                        
                        <br />
                        <asp:TextBox ID="tb_commenting_result" runat="server" Rows="4" 
                            TextMode="MultiLine" Width="300px" Font-Size="12px"></asp:TextBox>
                        &nbsp;&nbsp;
                        <asp:TextBoxWatermarkExtender ID="tb_commenting_result_TextBoxWatermarkExtender" 
                            runat="server" TargetControlID="tb_commenting_result" 
                            WatermarkCssClass="watermark" WatermarkText="Write a comment (optional)">
                        </asp:TextBoxWatermarkExtender>
                        <asp:Button ID="btn_submit_result" runat="server" CausesValidation="True" 
                            Font-Bold="True" ForeColor="#006600" Height="30px" 
                            onclick="btn_submit_result_Click" Text="Submit" Width="80px" ValidationGroup="submit_result" />                        
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="pnl_vieweditresult" runat="server" >
            <table width="100%">                
                <tr>
                    <td class="mypanel">
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
                            DataKeyNames="labresult_id" RowStyle-HorizontalAlign="Left" Width="100%" CellPadding="2" 
                            onrowcommand="gv_view_result_RowCommand" 
                            ondatabound="gv_view_result_DataBound" 
                            onrowdatabound="gv_view_result_RowDataBound" >
                            <RowStyle Font-Bold="True" Height="30px" HorizontalAlign="Left" />
                            <EditRowStyle BackColor="#DFEEFF" />
                            <HeaderStyle CssClass="mygridheader" />
                            <Columns>                                
                                <asp:TemplateField HeaderText="labresult_id" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="Label11" runat="server" Text='<%# Eval("labresult_id") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Label ID="Label12" runat="server" Text='<%# Eval("labresult_id") %>'></asp:Label>
                                    </EditItemTemplate>                                    
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="S.No">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="25px" />                                    
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Test">
                                    <ItemTemplate>
                                        <asp:Label ID="Label21" runat="server" Text='<%# Eval("testname") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Label ID="Label22" runat="server" Text='<%# Eval("testname") %>'></asp:Label>
                                    </EditItemTemplate>                                    
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sample">
                                    <ItemTemplate>
                                        <%# Eval("sampleid").ToString().Substring(8, 2)%>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <%# Eval("sampleid").ToString().Substring(8, 2)%>
                                    </EditItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="46px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Lab">
                                    <ItemTemplate>
                                        <asp:Label ID="Label31" runat="server" Text='<%# Eval("labname") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Label ID="Label32" runat="server" Text='<%# Eval("labname") %>'></asp:Label>
                                    </EditItemTemplate>                                    
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Method Ref.">                                    
                                    <ItemTemplate>
                                        <asp:Label ID="Label41" runat="server" Text='<%# Eval("mth_ref") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Label ID="Label42" runat="server" Text='<%# Eval("mth_ref") %>'></asp:Label>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Result">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_result" runat="server" Text='<%#Bind("result")%>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="tb_result" runat="server" Text='<%#Bind("result")%>'></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Publish<br>to Requestor">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="cb_forreport_item" runat="server" Checked='<%#Eval("isfor_report").ToString()=="True" %>' Enabled="false"/>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:CheckBox ID="cb_forreport_edit" runat="server" Checked='<%#Eval("isfor_report").ToString()=="True" %>' Enabled="true"/>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Attachment<br>(max 20MB)">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hlink_attach" runat="server" Text='<%#Bind("attachment_id")%>' />                                        
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" Width="220px"/>
                                    <EditItemTemplate>
                                    <asp:Panel runat="server" ID="pnl_edit_attach" BorderStyle="Solid" BorderWidth="1px" BorderColor="Red">
                                        <table>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="lbl_attach_edit" runat="server" Text='<%#Bind("attachment_id")%>'></asp:Label>
                                            </td>
                                            <td align="right">
                                                <asp:ImageButton ID="imgbtn_Delete" runat="server" CausesValidation="false" CommandName="deleting"
                                                ImageUrl="~/image/dustbin_icon.jpg" AlternateText="Delete" ToolTip="Delete Attachment" />
                                                <asp:ConfirmButtonExtender ID="cbe_ib_del" runat="server"  TargetControlID="imgbtn_Delete" 
                                                    ConfirmText="Are you sure you want to Delete this attachment? After deleting You will be able to attach a new file in place of this.">
                                                </asp:ConfirmButtonExtender>
                                            </td>
                                        </tr>
                                        </table>                                                                                
                                    </asp:Panel>
                                        <asp:FileUpload ID="fl_up_attach" runat="server" />
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgbtn_Edit" runat="server" CausesValidation="false"
                                            ImageUrl="~/image/edit.png" AlternateText="Edit" ToolTip="Edit" 
                                            CommandName="editing" CommandArgument='<%# Container.DataItemIndex %>'/>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" Width="50px"/>
                                    <EditItemTemplate>                                        
                                           <asp:ImageButton ID="imgbtn_Update" runat="server" ImageUrl="~/image/tick.jpg"
                                           AlternateText="Update" ToolTip="Update" CausesValidation="false"
                                           CommandName="updating" CommandArgument='<%# Container.DataItemIndex %>'/>
                                           &nbsp;&nbsp;&nbsp;
                                           <asp:ImageButton ID="imgbtn_Cancel" runat="server" CausesValidation="false"
                                           ImageUrl="~/image/cross.jpg" AlternateText="Cancel" ToolTip="Cancel" 
                                           CommandName="cancelling" />                                     
                                    </EditItemTemplate>
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
                            ReadOnly="True" Width="300px"></asp:TextBox>
                        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender4" runat="server" 
                            TargetControlID="tb_remark_fromResult" WatermarkCssClass="watermark" 
                            WatermarkText="No Remark">
                        </asp:TextBoxWatermarkExtender>
                        <br />
                        <br />
                        Comment<br />
                        <asp:TextBox ID="tb_comment_fromResult" runat="server" Font-Size="12px"
                            ReadOnly="True" Rows="4" TextMode="MultiLine" Width="300px"></asp:TextBox>
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
            <asp:Label ID="lbl_title1" runat="server" Text="Click to Show Details" ></asp:Label>
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
                <asp:TabPanel ID="TabPanel1" runat="server">
                    <HeaderTemplate>
                        <div style="width: 44px; font-weight: bold; color: #000000; font-size: 14px;">
                            1-4
                        </div>
                    </HeaderTemplate>
                    <ContentTemplate>
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                            Width="100%" onrowcreated="GridView_RowCreated" CssClass="mygrid">
                            <HeaderStyle CssClass="mygridheader"/>
                            <Columns>                                
                                <asp:BoundField HeaderText="ID" DataField="propertyid" >
                                <ItemStyle Width="20px" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Property" DataField="propertyname" >
                                <ItemStyle Width="150px" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Sample1" DataField="1" />
                                <asp:BoundField HeaderText="Sample2" DataField="2" />
                                <asp:BoundField HeaderText="Sample3" DataField="3" />
                                <asp:BoundField HeaderText="Sample4" DataField="4" />
                            </Columns>
                        </asp:GridView>
                    </ContentTemplate>
                </asp:TabPanel>                    
                <asp:TabPanel ID="TabPanel2" runat="server" >
                    <HeaderTemplate>
                        <div style="width: 44px; font-weight: bold; color: #000000; font-size: 14px;">
                            5-8
                        </div>
                    </HeaderTemplate>
                    <ContentTemplate>
                        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
                            Width="100%" onrowcreated="GridView_RowCreated" CssClass="mygrid">
                            <HeaderStyle CssClass="mygridheader"/>
                            <Columns>
                                <asp:BoundField HeaderText="ID" DataField="propertyid" ItemStyle-Width="20px" />
                                <asp:BoundField HeaderText="Property" DataField="propertyname" ItemStyle-Width="150px" />
                                <asp:BoundField HeaderText="Sample5" DataField="5" />
                                <asp:BoundField HeaderText="Sample6" DataField="6" />
                                <asp:BoundField HeaderText="Sample7" DataField="7" />
                                <asp:BoundField HeaderText="Sample8" DataField="8" />
                            </Columns>
                        </asp:GridView>
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel ID="TabPanel3" runat="server">
                    <HeaderTemplate>
                        <div style="width: 44px; font-weight: bold; color: #000000; font-size: 14px;">
                            9-12
                        </div>
                    </HeaderTemplate>
                    <ContentTemplate>
                        <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" 
                            Width="100%" onrowcreated="GridView_RowCreated" CssClass="mygrid">
                            <HeaderStyle CssClass="mygridheader"/>
                            <Columns>
                                <asp:BoundField HeaderText="ID" DataField="propertyid" ItemStyle-Width="20px" />
                                <asp:BoundField HeaderText="Property" DataField="propertyname" ItemStyle-Width="150px" />
                                <asp:BoundField HeaderText="Sample9" DataField="9" />
                                <asp:BoundField HeaderText="Sample10" DataField="10" />
                                <asp:BoundField HeaderText="Sample11" DataField="11" />
                                <asp:BoundField HeaderText="Sample12" DataField="12" />
                            </Columns>
                        </asp:GridView>
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel ID="TabPanel4" runat="server">
                    <HeaderTemplate>
                        <div style="width: 44px; font-weight: bold; color: #000000; font-size: 14px;">
                            13-16
                        </div>
                    </HeaderTemplate>
                    <ContentTemplate>
                        <asp:GridView ID="GridView4" runat="server" AutoGenerateColumns="False" 
                            Width="100%" onrowcreated="GridView_RowCreated" CssClass="mygrid">
                            <HeaderStyle CssClass="mygridheader"/>
                            <Columns>
                                <asp:BoundField HeaderText="ID" DataField="propertyid" ItemStyle-Width="20px" />
                                <asp:BoundField HeaderText="Property" DataField="propertyname" ItemStyle-Width="150px" />
                                <asp:BoundField HeaderText="Sample13" DataField="13" />
                                <asp:BoundField HeaderText="Sample14" DataField="14" />
                                <asp:BoundField HeaderText="Sample15" DataField="15" />
                                <asp:BoundField HeaderText="Sample16" DataField="16" />
                            </Columns>
                        </asp:GridView>
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel ID="TabPanel5" runat="server" >
                    <HeaderTemplate>
                        <div style="width: 44px; font-weight: bold; color: #000000; font-size: 14px;">
                            17-20
                        </div>
                    </HeaderTemplate>
                    <ContentTemplate>
                        <asp:GridView ID="GridView5" runat="server" AutoGenerateColumns="False" 
                            Width="100%" onrowcreated="GridView_RowCreated" CssClass="mygrid">
                            <HeaderStyle CssClass="mygridheader"/>
                            <Columns>
                                <asp:BoundField HeaderText="ID" DataField="propertyid" ItemStyle-Width="20px" />
                                <asp:BoundField HeaderText="Property" DataField="propertyname" ItemStyle-Width="150px" />
                                <asp:BoundField HeaderText="Sample17" DataField="17" />
                                <asp:BoundField HeaderText="Sample18" DataField="18" />
                                <asp:BoundField HeaderText="Sample19" DataField="19" />
                                <asp:BoundField HeaderText="Sample20" DataField="20" />
                            </Columns>
                        </asp:GridView>
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel ID="TabPanel6" runat="server" >
                    <HeaderTemplate>
                        <div style="width: 44px; font-weight: bold; color: #000000; font-size: 14px;">
                            21-24
                        </div>
                    </HeaderTemplate>
                    <ContentTemplate>
                        <asp:GridView ID="GridView6" runat="server" AutoGenerateColumns="False" 
                            Width="100%" onrowcreated="GridView_RowCreated" CssClass="mygrid">
                            <HeaderStyle CssClass="mygridheader"/>
                            <Columns>
                                <asp:BoundField HeaderText="ID" DataField="propertyid" ItemStyle-Width="20px" />
                                <asp:BoundField HeaderText="Property" DataField="propertyname" ItemStyle-Width="150px" />
                                <asp:BoundField HeaderText="Sample21" DataField="21" />
                                <asp:BoundField HeaderText="Sample22" DataField="22" />
                                <asp:BoundField HeaderText="Sample23" DataField="23" />
                                <asp:BoundField HeaderText="Sample24" DataField="24" />
                            </Columns>
                        </asp:GridView>
                    </ContentTemplate>
                </asp:TabPanel>
            </asp:TabContainer>
        </asp:Panel>
        <asp:Panel ID="Panel3" runat="server" CssClass="mypanel" 
            GroupingText="Additional Information (1000 characters)">
            <asp:TextBox ID="tb_addinfo" runat="server" TextMode="MultiLine" Rows="4" Width="100%" 
                onmouseover="this.style.backgroundColor='#EAF5FB'" onmouseout="this.style.backgroundColor='white'" ReadOnly="True">
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
                TextLabelID="lbl_title1" CollapsedText="Click to Show Details" ExpandedText="Click to Hide Details"
                ImageControlID="Image1" ExpandedImage="~/image/collapse_blue.jpg" CollapsedImage="~/image/expand_blue.jpg"
                Collapsed="True" SuppressPostBack="true">
        </asp:CollapsiblePanelExtender>
    </div>
</asp:Content>