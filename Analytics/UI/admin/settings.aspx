<%@ Page Title="Analytics (V1.0)" Language="C#" MasterPageFile="~/UI/admin/admin.master" AutoEventWireup="true" CodeFile="settings.aspx.cs" 
    Inherits="UI_admin_settings" MaintainScrollPositionOnPostback="true"%>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <div>
        <table width="100%">
            <tr>
                <td>
                    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
                    </asp:ToolkitScriptManager>
                </td>
                <td align="right">
                    <asp:UpdatePanel ID="UpdatePanel9" runat="server">                    
                        <ContentTemplate>
                            <asp:Label ID="lbl_message" runat="server" 
                            Text="Changes Saved" Visible="False" CssClass="pwd4" />
                            <asp:Timer ID="Timer1" runat="server" Interval="5000" ontick="Timer1_Tick">
                            </asp:Timer>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>                
    </div>
    <div>
        <table style="width: 100%;" cellpadding="6">
            <tr>
                <td valign="top" width="33%">
                <asp:Panel ID="PnlTitle2" runat="server" CssClass="collapsePanelHeader">
                    <asp:Image ID="Image2" runat="server" ImageUrl="~/image/expand_blue.jpg" ></asp:Image>
                            Backup-Email
                </asp:Panel>
                <asp:Panel ID="PnlContent2" runat="server" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <Triggers>
                            <asp:PostBackTrigger ControlID="btn_backup"/>
                        </Triggers>
                        <ContentTemplate>                            
                            <table width="100%">
                                <tr>
                                    <td>
                                        Backup-Email:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tb_emailid" runat="server" Width="90%"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfv_bck1" runat="server" Display="Dynamic"
                                        ErrorMessage="required" ControlToValidate="tb_emailid" ValidationGroup="backup" CssClass="failureNotification" />
                                        <asp:RegularExpressionValidator ID="rev_bck1" runat="server" ControlToValidate="tb_emailid"
                                        ErrorMessage="Invalid email" CssClass="failureNotification" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
                                        ValidationGroup="backup" />                                    
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Backup-Name:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tb_emailname" runat="server" Width="90%"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfv_bck2" runat="server" 
                                        ErrorMessage="required" ControlToValidate="tb_emailname" ValidationGroup="backup" CssClass="failureNotification" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>                                        
                                    </td>
                                    <td>
                                        <asp:Button ID="btn_backup" ValidationGroup="backup" runat="server" Text="Save" 
                                        onclick="btn_backup_Click"/>
                                    </td>
                                </tr>
                           </table>                                                                                                                                
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:Panel>                    
                <asp:CollapsiblePanelExtender ID="cpe_bckup" runat="server"
                    TargetControlID="PnlContent2" ExpandControlID="PnlTitle2" CollapseControlID="PnlTitle2"                    
                    ImageControlID="Image2" ExpandedImage="~/image/collapse_blue.jpg" CollapsedImage="~/image/expand_blue.jpg"
                    Collapsed="True">
                </asp:CollapsiblePanelExtender>                 
                </td>
                <td valign="top" width="33%">
                <asp:Panel ID="PnlTitle1" runat="server" CssClass="collapsePanelHeader">
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/image/expand_blue.jpg" ></asp:Image>
                            Software Maintenance
                </asp:Panel>
                <asp:Panel ID="PnlContent1" runat="server" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" >                        
                        <Triggers>
                            <asp:PostBackTrigger ControlID="btn_service"/>
                        </Triggers>
                        <ContentTemplate>                            
                            <table width="100%" title="Mark 'True' when site is under maintenance">
                                <tr>
                                    <td>
                                        Service Status
                                    </td>
                                    <td>
                                        <asp:RadioButtonList ID="rbl_service_status" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Text="True" Value="True"></asp:ListItem>
                                            <asp:ListItem Text="False" Value="False"></asp:ListItem>
                                        </asp:RadioButtonList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                        ErrorMessage="select status" ControlToValidate="rbl_service_status" ValidationGroup="service" CssClass="failureNotification" />
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        Service Message</td>
                                    <td valign="top">
                                        <asp:TextBox ID="tb_service_msg" runat="server" Width="90%" Rows="2" 
                                            TextMode="MultiLine" Font-Size="12px" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                        ErrorMessage="required" ControlToValidate="tb_service_msg" ValidationGroup="service" CssClass="failureNotification" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>                                        
                                        
                                    </td>
                                    <td>
                                        <asp:Button ID="btn_service" ValidationGroup="service" runat="server" Text="Save" 
                                        onclick="btn_service_Click"/>
                                    </td>
                                </tr>
                           </table>                                                                                                                                
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:Panel>                    
                <asp:CollapsiblePanelExtender ID="CollapsiblePanelExtender1" runat="server"
                    TargetControlID="PnlContent1" ExpandControlID="PnlTitle1" CollapseControlID="PnlTitle1"
                    ImageControlID="Image1" ExpandedImage="~/image/collapse_blue.jpg" CollapsedImage="~/image/expand_blue.jpg"
                    Collapsed="True">
                </asp:CollapsiblePanelExtender>                 
                </td>
                <td valign="top">
                   
                </td>
            </tr>                        
        </table>
    </div>
    <br />
    <div>
        <table width="100%" cellpadding="6">
            <tr>
                <td width="33%" valign="top">
                <asp:Panel ID="PnlTitle3" runat="server" CssClass="collapsePanelHeader">
                    <asp:Image ID="Image3" runat="server" ImageUrl="~/image/expand_blue.jpg" ></asp:Image>
                            Project-Type</asp:Panel>
                <asp:Panel ID="PnlContent3" runat="server" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px">
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                        <Triggers>                            
                            <asp:AsyncPostBackTrigger ControlID="gv_prj_type" EventName="RowCommand"/>                            
                        </Triggers>
                        <ContentTemplate>
                            <table width="100%" cellpadding="4px">
                                <tr>
                                    <td>
                                        <asp:GridView ID="gv_prj_type" runat="server" AutoGenerateColumns="False" 
                                            CellPadding="2" CssClass="mygrid" DataKeyNames="dd_id" 
                                            RowStyle-HorizontalAlign="Left" Width="100%" ShowFooter="True" 
                                            onrowcommand="gv_prj_type_RowCommand" >
                                            <RowStyle  HorizontalAlign="Left" />
                                            <HeaderStyle CssClass="mygridheader" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No">
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" Width="25px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Project Types">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1" runat="server" Text='<%#Eval("value") %>' />                                                        
                                                    </ItemTemplate>
                                                    <EditItemTemplate>                                                        
                                                        <asp:TextBox ID="tb_type_edit" runat="server" Text='<%#Bind("value") %>'/>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                                            ControlToValidate="tb_type_edit" ErrorMessage="*" ValidationGroup="typeedit" />
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:TextBox ID="tb_type_add" runat="server" />
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                                            ControlToValidate="tb_type_add" ErrorMessage="*" ValidationGroup="typeadd" />
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Active">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%#Eval("isactive") %>' Enabled="false"/>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%#Bind("isactive") %>'/>
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%#Bind("isactive") %>'/>
                                                    </FooterTemplate>                                                    
                                                </asp:TemplateField>                                                                                                
                                                <asp:TemplateField HeaderText="Action" ItemStyle-Width="80px">                                                    
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="type_Edit" runat="server" CausesValidation="false" ImageUrl="~/image/edit.png" AlternateText="Edit" 
                                                    ToolTip="Edit" CommandName="Editing"/>
                                                    &nbsp;&nbsp;
                                                    <asp:ImageButton ID="type_Delete" runat="server" ImageUrl="~/image/dustbin_icon.jpg" AlternateText="Delete" 
                                                    ToolTip="Delete" CommandName="Deleting"/>
                                                    <asp:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" TargetControlID="type_Delete" ConfirmText="Are you sure you want to delete?">
                                                    </asp:ConfirmButtonExtender>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:ImageButton ID="type_Update" runat="server" ImageUrl="~/image/tick.jpg" AlternateText="Update" 
                                                    ToolTip="Update" ValidationGroup="typeedit" CommandName="Updating"/>
                                                    &nbsp;&nbsp;
                                                    <asp:ImageButton ID="type_Cancel" runat="server" ImageUrl="~/image/cross.jpg" AlternateText="Cancel" 
                                                    ToolTip="Cancel" CommandName="Cancelling"/>
                                                </EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:ImageButton ID="type_Add" runat="server" ImageUrl="~/image/add.jpg" AlternateText="Add" 
                                                    ToolTip="Add" ValidationGroup="typeadd" CommandName="Adding"/>
                                                </FooterTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>                                
                            </table>                                                        
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:Panel>                    
                <asp:CollapsiblePanelExtender ID="CollapsiblePanelExtender2" runat="server"
                    TargetControlID="PnlContent3" ExpandControlID="PnlTitle3" CollapseControlID="PnlTitle3"                    
                    ImageControlID="Image3" ExpandedImage="~/image/collapse_blue.jpg" CollapsedImage="~/image/expand_blue.jpg"
                    Collapsed="True">
                </asp:CollapsiblePanelExtender>
                </td>
                <td width="33%" valign="top">
                <asp:Panel ID="PnlTitle4" runat="server" CssClass="collapsePanelHeader">
                    <asp:Image ID="Image4" runat="server" ImageUrl="~/image/expand_blue.jpg" ></asp:Image>
                            Project-Category
                </asp:Panel>
                <asp:Panel ID="PnlContent4" runat="server" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px">
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                        <Triggers>                            
                            <asp:AsyncPostBackTrigger ControlID="gv_prj_category" EventName="RowCommand"/>                            
                        </Triggers>
                        <ContentTemplate>
                            <table width="100%" cellpadding="4px">
                                <tr>
                                    <td>
                                        <asp:GridView ID="gv_prj_category" runat="server" AutoGenerateColumns="False" 
                                            CellPadding="2" CssClass="mygrid" DataKeyNames="dd_id" 
                                            RowStyle-HorizontalAlign="Left" Width="100%" ShowFooter="True" 
                                            onrowcommand="gv_prj_category_RowCommand" >
                                            <RowStyle  HorizontalAlign="Left" />
                                            <HeaderStyle CssClass="mygridheader" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No">
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" Width="25px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Project Category">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1" runat="server" Text='<%#Eval("value") %>' />                                                        
                                                    </ItemTemplate>
                                                    <EditItemTemplate>                                                        
                                                        <asp:TextBox ID="tb_value" runat="server" Text='<%#Bind("value") %>'/>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                                            ControlToValidate="tb_value" ErrorMessage="*" ValidationGroup="typeedit" />
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:TextBox ID="tb_value" runat="server" />
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                                            ControlToValidate="tb_value" ErrorMessage="*" ValidationGroup="categoryadd" />
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Active">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%#Eval("isactive") %>' Enabled="false"/>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%#Bind("isactive") %>'/>
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%#Bind("isactive") %>'/>
                                                    </FooterTemplate>                                                    
                                                </asp:TemplateField>                                                                                                
                                                <asp:TemplateField HeaderText="Action" ItemStyle-Width="80px">                                                    
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="type_Edit" runat="server" CausesValidation="false" ImageUrl="~/image/edit.png" AlternateText="Edit" 
                                                    ToolTip="Edit" CommandName="Editing"/>
                                                    &nbsp;&nbsp;
                                                    <asp:ImageButton ID="type_Delete" runat="server" ImageUrl="~/image/dustbin_icon.jpg" AlternateText="Delete" 
                                                    ToolTip="Delete" CommandName="Deleting"/>
                                                    <asp:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" TargetControlID="type_Delete" ConfirmText="Are you sure you want to delete?">
                                                    </asp:ConfirmButtonExtender>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:ImageButton ID="type_Update" runat="server" ImageUrl="~/image/tick.jpg" AlternateText="Update" 
                                                    ToolTip="Update" ValidationGroup="typeedit" CommandName="Updating"/>
                                                    &nbsp;&nbsp;
                                                    <asp:ImageButton ID="type_Cancel" runat="server" ImageUrl="~/image/cross.jpg" AlternateText="Cancel" 
                                                    ToolTip="Cancel" CommandName="Cancelling"/>
                                                </EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:ImageButton ID="category_Add" runat="server" ImageUrl="~/image/add.jpg" AlternateText="Add" 
                                                    ToolTip="Add" ValidationGroup="categoryadd" CommandName="Adding"/>
                                                </FooterTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>                                
                            </table>                                                        
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:Panel>                    
                <asp:CollapsiblePanelExtender ID="CollapsiblePanelExtender3" runat="server"
                    TargetControlID="PnlContent4" ExpandControlID="PnlTitle4" CollapseControlID="PnlTitle4"                    
                    ImageControlID="Image4" ExpandedImage="~/image/collapse_blue.jpg" CollapsedImage="~/image/expand_blue.jpg"
                    Collapsed="True">
                </asp:CollapsiblePanelExtender>
                </td>
                <td width="33%" valign="top">
                <asp:Panel ID="PnlTitle5" runat="server" CssClass="collapsePanelHeader">
                    <asp:Image ID="Image5" runat="server" ImageUrl="~/image/expand_blue.jpg" ></asp:Image>
                            Project-Brand
                </asp:Panel>
                <asp:Panel ID="PnlContent5" runat="server" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px">
                    <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                        <Triggers>                            
                            <asp:AsyncPostBackTrigger ControlID="gv_prj_brand" EventName="RowCommand"/>                            
                        </Triggers>
                        <ContentTemplate>
                            <table width="100%" cellpadding="4px">
                                <tr>
                                    <td>
                                        <asp:GridView ID="gv_prj_brand" runat="server" AutoGenerateColumns="False" 
                                            CellPadding="2" CssClass="mygrid" DataKeyNames="dd_id" 
                                            RowStyle-HorizontalAlign="Left" Width="100%" ShowFooter="True" 
                                            onrowcommand="gv_prj_brand_RowCommand" >
                                            <RowStyle  HorizontalAlign="Left" />
                                            <HeaderStyle CssClass="mygridheader" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No">
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" Width="25px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Project Brand">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1" runat="server" Text='<%#Eval("value") %>' />                                                        
                                                    </ItemTemplate>
                                                    <EditItemTemplate>                                                        
                                                        <asp:TextBox ID="tb_value" runat="server" Text='<%#Bind("value") %>'/>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                                            ControlToValidate="tb_value" ErrorMessage="*" ValidationGroup="typeedit" />
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:TextBox ID="tb_value" runat="server" />
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                                            ControlToValidate="tb_value" ErrorMessage="*" ValidationGroup="brandadd" />
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Active">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%#Eval("isactive") %>' Enabled="false"/>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%#Bind("isactive") %>'/>
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%#Bind("isactive") %>'/>
                                                    </FooterTemplate>                                                    
                                                </asp:TemplateField>                                                                                                
                                                <asp:TemplateField HeaderText="Action" ItemStyle-Width="80px">                                                    
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="type_Edit" runat="server" CausesValidation="false" ImageUrl="~/image/edit.png" AlternateText="Edit" 
                                                    ToolTip="Edit" CommandName="Editing"/>
                                                    &nbsp;&nbsp;
                                                    <asp:ImageButton ID="type_Delete" runat="server" ImageUrl="~/image/dustbin_icon.jpg" AlternateText="Delete" 
                                                    ToolTip="Delete" CommandName="Deleting"/>
                                                    <asp:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" TargetControlID="type_Delete" ConfirmText="Are you sure you want to delete?">
                                                    </asp:ConfirmButtonExtender>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:ImageButton ID="type_Update" runat="server" ImageUrl="~/image/tick.jpg" AlternateText="Update" 
                                                    ToolTip="Update" ValidationGroup="typeedit" CommandName="Updating"/>
                                                    &nbsp;&nbsp;
                                                    <asp:ImageButton ID="type_Cancel" runat="server" ImageUrl="~/image/cross.jpg" AlternateText="Cancel" 
                                                    ToolTip="Cancel" CommandName="Cancelling"/>
                                                </EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:ImageButton ID="category_Add" runat="server" ImageUrl="~/image/add.jpg" AlternateText="Add" 
                                                    ToolTip="Add" ValidationGroup="brandadd" CommandName="Adding"/>
                                                </FooterTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>                                
                            </table>                                                        
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:Panel>                    
                <asp:CollapsiblePanelExtender ID="CollapsiblePanelExtender4" runat="server"
                    TargetControlID="PnlContent5" ExpandControlID="PnlTitle5" CollapseControlID="PnlTitle5"                    
                    ImageControlID="Image5" ExpandedImage="~/image/collapse_blue.jpg" CollapsedImage="~/image/expand_blue.jpg"
                    Collapsed="True">
                </asp:CollapsiblePanelExtender>
                </td>
            </tr>
        </table>
    </div>
    <br />
    <div>
    <table width="100%" cellpadding="6">
        <tr>
            <td width="33%" valign="top">                
                <asp:Panel ID="PnlTitle6" runat="server" CssClass="collapsePanelHeader">
                    <asp:Image ID="Image6" runat="server" ImageUrl="~/image/expand_blue.jpg" ></asp:Image>
                        Request-Analysis Types
                </asp:Panel>
                <asp:Panel ID="PnlContent6" runat="server" BorderColor="Gray" 
                    BorderStyle="Solid" BorderWidth="1px">
                    <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                        <Triggers>                            
                            <asp:AsyncPostBackTrigger ControlID="gv_req_analtype" EventName="RowCommand"/>                            
                        </Triggers>
                        <ContentTemplate>
                            <table width="100%" cellpadding="4px">
                                <tr>
                                    <td>
                                        <asp:GridView ID="gv_req_analtype" runat="server" AutoGenerateColumns="False" 
                                            CellPadding="2" CssClass="mygrid" DataKeyNames="dd_id" 
                                            RowStyle-HorizontalAlign="Left" Width="100%" ShowFooter="True" 
                                            onrowcommand="gv_req_analtype_RowCommand" >
                                            <RowStyle  HorizontalAlign="Left" />
                                            <HeaderStyle CssClass="mygridheader" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No">
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" Width="25px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Analysis Types">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label2" runat="server" Text='<%#Eval("value") %>' />                                                        
                                                    </ItemTemplate>
                                                    <EditItemTemplate>                                                        
                                                        <asp:TextBox ID="tb_value" runat="server" Text='<%#Bind("value") %>'/>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                                                            ControlToValidate="tb_value" ErrorMessage="*" 
                                                            ValidationGroup="typeedit" />
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:TextBox ID="tb_value" runat="server" />
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                                                            ControlToValidate="tb_value" ErrorMessage="*" 
                                                            ValidationGroup="reqanatype_add" />
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Active">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%#Eval("isactive") %>' 
                                                            Enabled="false"/>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%#Bind("isactive") %>'/>
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%#Bind("isactive") %>'/>
                                                    </FooterTemplate>                                                    
                                                </asp:TemplateField>                                                                                                
                                                <asp:TemplateField HeaderText="Action" ItemStyle-Width="80px">                                                    
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="type_Edit" runat="server" CausesValidation="false" 
                                                        ImageUrl="~/image/edit.png" AlternateText="Edit" 
                                                    ToolTip="Edit" CommandName="Editing"/>
                                                    &nbsp;&nbsp;
                                                    <asp:ImageButton ID="type_Delete" runat="server" 
                                                        ImageUrl="~/image/dustbin_icon.jpg" AlternateText="Delete" 
                                                    ToolTip="Delete" CommandName="Deleting"/>
                                                    <asp:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" 
                                                        TargetControlID="type_Delete" ConfirmText="Are you sure you want to delete?">
                                                    </asp:ConfirmButtonExtender>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:ImageButton ID="type_Update" runat="server" ImageUrl="~/image/tick.jpg" AlternateText="Update" 
                                                    ToolTip="Update" ValidationGroup="typeedit" CommandName="Updating"/>
                                                    &nbsp;&nbsp;
                                                    <asp:ImageButton ID="type_Cancel" runat="server" ImageUrl="~/image/cross.jpg" AlternateText="Cancel" 
                                                    ToolTip="Cancel" CommandName="Cancelling"/>
                                                </EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:ImageButton ID="type_Add" runat="server" ImageUrl="~/image/add.jpg" AlternateText="Add" 
                                                    ToolTip="Add" ValidationGroup="reqanatype_add" CommandName="Adding"/>
                                                </FooterTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>                                
                            </table>                                                        
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:Panel>                    
                <asp:CollapsiblePanelExtender ID="PnlContent6_CollapsiblePanelExtender" runat="server"
                    TargetControlID="PnlContent6" ExpandControlID="PnlTitle6" CollapseControlID="PnlTitle6"
                    ImageControlID="Image6" ExpandedImage="~/image/collapse_blue.jpg" CollapsedImage="~/image/expand_blue.jpg"
                    Collapsed="True">
                </asp:CollapsiblePanelExtender>                
            </td>
            <td width="33%" valign="top">
                <asp:Panel ID="PnlTitle7" runat="server" CssClass="collapsePanelHeader">
                    <asp:Image ID="Image7" runat="server" ImageUrl="~/image/expand_blue.jpg" ></asp:Image>
                        Sample-Storage Conditions
                </asp:Panel>
                <asp:Panel ID="PnlContent7" runat="server" BorderColor="Gray" 
                    BorderStyle="Solid" BorderWidth="1px">
                    <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional">
                        <Triggers>                            
                            <asp:AsyncPostBackTrigger ControlID="gv_sample_stcond" EventName="RowCommand"/>                            
                        </Triggers>
                        <ContentTemplate>
                            <table width="100%" cellpadding="4px">
                                <tr>
                                    <td>
                                        <asp:GridView ID="gv_sample_stcond" runat="server" AutoGenerateColumns="False" 
                                            CellPadding="2" CssClass="mygrid" DataKeyNames="dd_id" 
                                            RowStyle-HorizontalAlign="Left" Width="100%" ShowFooter="True" 
                                            onrowcommand="gv_sample_stcond_RowCommand" >
                                            <RowStyle  HorizontalAlign="Left" />
                                            <HeaderStyle CssClass="mygridheader" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No">
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" Width="25px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Storage Condition">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label2" runat="server" Text='<%#Eval("value") %>' />                                                        
                                                    </ItemTemplate>
                                                    <EditItemTemplate>                                                        
                                                        <asp:TextBox ID="tb_value" runat="server" Text='<%#Bind("value") %>'/>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                                                            ControlToValidate="tb_value" ErrorMessage="*" 
                                                            ValidationGroup="typeedit" />
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:TextBox ID="tb_value" runat="server" />
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                                                            ControlToValidate="tb_value" ErrorMessage="*" 
                                                            ValidationGroup="sample_stcond_add" />
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Active">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%#Eval("isactive") %>' 
                                                            Enabled="false"/>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%#Bind("isactive") %>'/>
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%#Bind("isactive") %>'/>
                                                    </FooterTemplate>                                                    
                                                </asp:TemplateField>                                                                                                
                                                <asp:TemplateField HeaderText="Action" ItemStyle-Width="80px">                                                    
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="type_Edit" runat="server" CausesValidation="false" 
                                                        ImageUrl="~/image/edit.png" AlternateText="Edit" 
                                                    ToolTip="Edit" CommandName="Editing"/>
                                                    &nbsp;&nbsp;
                                                    <asp:ImageButton ID="type_Delete" runat="server" 
                                                        ImageUrl="~/image/dustbin_icon.jpg" AlternateText="Delete" 
                                                    ToolTip="Delete" CommandName="Deleting"/>
                                                    <asp:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" 
                                                        TargetControlID="type_Delete" ConfirmText="Are you sure you want to delete?">
                                                    </asp:ConfirmButtonExtender>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:ImageButton ID="type_Update" runat="server" ImageUrl="~/image/tick.jpg" AlternateText="Update" 
                                                    ToolTip="Update" ValidationGroup="typeedit" CommandName="Updating"/>
                                                    &nbsp;&nbsp;
                                                    <asp:ImageButton ID="type_Cancel" runat="server" ImageUrl="~/image/cross.jpg" AlternateText="Cancel" 
                                                    ToolTip="Cancel" CommandName="Cancelling"/>
                                                </EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:ImageButton ID="type_Add" runat="server" ImageUrl="~/image/add.jpg" AlternateText="Add" 
                                                    ToolTip="Add" ValidationGroup="sample_stcond_add" CommandName="Adding"/>
                                                </FooterTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>                                
                            </table>                                                        
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:Panel>
                <asp:CollapsiblePanelExtender ID="CollapsiblePanelExtender5" runat="server"
                    TargetControlID="PnlContent7" ExpandControlID="PnlTitle7" CollapseControlID="PnlTitle7"
                    ImageControlID="Image7" ExpandedImage="~/image/collapse_blue.jpg" CollapsedImage="~/image/expand_blue.jpg"
                    Collapsed="True">
                </asp:CollapsiblePanelExtender>
            </td>
            <td width="33%" valign="top">
                <asp:Panel ID="PnlTitle8" runat="server" CssClass="collapsePanelHeader">
                    <asp:Image ID="Image8" runat="server" ImageUrl="~/image/expand_blue.jpg" ></asp:Image>
                        Test-References
                </asp:Panel>
                <asp:Panel ID="PnlContent8" runat="server" BorderColor="Gray" 
                    BorderStyle="Solid" BorderWidth="1px">
                    <asp:UpdatePanel ID="UpdatePanel8" runat="server" UpdateMode="Conditional">
                        <Triggers>                            
                            <asp:AsyncPostBackTrigger ControlID="gv_sample_stcond" EventName="RowCommand"/>                            
                        </Triggers>
                        <ContentTemplate>
                            <table width="100%" cellpadding="4px">
                                <tr>
                                    <td>
                                        <asp:GridView ID="gv_test_ref" runat="server" AutoGenerateColumns="False" 
                                            CellPadding="2" CssClass="mygrid" DataKeyNames="dd_id" 
                                            RowStyle-HorizontalAlign="Left" Width="100%" ShowFooter="True" 
                                            onrowcommand="gv_test_ref_RowCommand" >
                                            <RowStyle  HorizontalAlign="Left" />
                                            <HeaderStyle CssClass="mygridheader" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No">
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" Width="25px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Reference">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label2" runat="server" Text='<%#Eval("value") %>' />                                                        
                                                    </ItemTemplate>
                                                    <EditItemTemplate>                                                        
                                                        <asp:TextBox ID="tb_value" runat="server" Text='<%#Bind("value") %>'/>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                                                            ControlToValidate="tb_value" ErrorMessage="*" 
                                                            ValidationGroup="typeedit" />
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:TextBox ID="tb_value" runat="server" />
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                                                            ControlToValidate="tb_value" ErrorMessage="*" 
                                                            ValidationGroup="test_ref_add" />
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Active">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%#Eval("isactive") %>' 
                                                            Enabled="false"/>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%#Bind("isactive") %>'/>
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%#Bind("isactive") %>'/>
                                                    </FooterTemplate>                                                    
                                                </asp:TemplateField>                                                                                                
                                                <asp:TemplateField HeaderText="Action" ItemStyle-Width="80px">                                                    
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="type_Edit" runat="server" CausesValidation="false" 
                                                        ImageUrl="~/image/edit.png" AlternateText="Edit" 
                                                    ToolTip="Edit" CommandName="Editing"/>
                                                    &nbsp;&nbsp;
                                                    <asp:ImageButton ID="type_Delete" runat="server" 
                                                        ImageUrl="~/image/dustbin_icon.jpg" AlternateText="Delete" 
                                                    ToolTip="Delete" CommandName="Deleting"/>
                                                    <asp:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" 
                                                        TargetControlID="type_Delete" ConfirmText="Are you sure you want to delete?">
                                                    </asp:ConfirmButtonExtender>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:ImageButton ID="type_Update" runat="server" ImageUrl="~/image/tick.jpg" AlternateText="Update" 
                                                    ToolTip="Update" ValidationGroup="typeedit" CommandName="Updating"/>
                                                    &nbsp;&nbsp;
                                                    <asp:ImageButton ID="type_Cancel" runat="server" ImageUrl="~/image/cross.jpg" AlternateText="Cancel" 
                                                    ToolTip="Cancel" CommandName="Cancelling"/>
                                                </EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:ImageButton ID="type_Add" runat="server" ImageUrl="~/image/add.jpg" AlternateText="Add" 
                                                    ToolTip="Add" ValidationGroup="test_ref_add" CommandName="Adding"/>
                                                </FooterTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>                                
                            </table>                                                        
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:Panel>
                <asp:CollapsiblePanelExtender ID="CollapsiblePanelExtender6" runat="server"
                    TargetControlID="PnlContent8" ExpandControlID="PnlTitle8" CollapseControlID="PnlTitle8"
                    ImageControlID="Image8" ExpandedImage="~/image/collapse_blue.jpg" CollapsedImage="~/image/expand_blue.jpg"
                    Collapsed="True">
                </asp:CollapsiblePanelExtender>
            </td>
        </tr>
    </table>
    </div>
</asp:Content>

