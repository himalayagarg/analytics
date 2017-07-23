<%@ Page Title="Analytics (V1.0)" Language="C#" MasterPageFile="~/UI/users/users.master" AutoEventWireup="true" 
    CodeFile="process_request1.aspx.cs" Inherits="UI_users_process_request1" MaintainScrollPositionOnPostback="true" EnableViewState="true" EnableEventValidation="false"%>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script type = "text/javascript" >
    function count(text, limit) 
    {
        var countbox = document.getElementById('<%=count_addinfo.ClientID%>');
        if (text.value.length > limit) 
        {
            text.value = text.value.substring(0, limit);
            countbox.style.backgroundColor = "red";
        }
        else 
        {
            countbox.value = limit - text.value.length;
            countbox.style.backgroundColor = "white";
        }
    }

    function validate_StabilityDate(tb_m, tb_s) 
    {
        var tb_mkg = document.getElementById(tb_m);
        var tb_stability = document.getElementById(tb_s);
        var dt_mkg = new Date(tb_mkg.value);
        var dt_stability = new Date(tb_stability.value);
        if (dt_stability < dt_mkg) 
        {
            alert('Stability Initiation Date must be greater than or equal to Manufacturing Date. Please correct.');
            tb_stability.value = '';
        }
    }

    function ValidatePage() 
    {
        flag = true;
        if (typeof (Page_ClientValidate) == 'function') 
        {
            Page_ClientValidate();
        }

        if (!Page_IsValid) 
        {
            alert('All the * marked fields are mandatory.\nPlease fill all the mandatory fields for all Samples and all Tests .');
            flag = false;
            Page_BlockSubmit = false;
        }
        else 
        {
            flag = confirm('Are you sure you have filled the form completely? Click OK to confirm or CANCEL to edit this form.');
        }
        return flag;
    }

    function chng_testname(tb_test, rwindex) 
    {       
        var gv = document.getElementById("<%= gv_test_sample.ClientID %>");
        var t = gv.rows[rwindex].cells[1].textContent;
        if (t == null) 
        {
            gv.rows[rwindex].cells[1].innerText = tb_test.value; //<IE9
        }
        else 
        {
            gv.rows[rwindex].cells[1].textContent = tb_test.value; //Mozilla
        }
    }

    function chkall(rwindex, chkall) 
    {
        var cb = document.getElementById(chkall.id);
        var gv = document.getElementById("<%= gv_test_sample.ClientID %>");
        for (i = 3; i < gv.rows[rwindex].cells.length; i++) 
        {
            var cell = gv.rows[rwindex].cells[i];
            for (j = 0; j < cell.childNodes.length; j++) 
            {
                if (cell.childNodes[j].type == "checkbox") 
                {
                    cell.childNodes[j].checked = cb.checked;
                }
            }
        }
    }

    function preventBack() 
    {
        window.history.forward();
    }
    setTimeout("preventBack()", 0);
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<div>
    <asp:ToolkitScriptManager ID="ScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:ObjectDataSource ID="ods_TestReference" runat="server" SelectMethod="GetDropDownsByType" 
        TypeName="ds_analyticsTableAdapters.dropdownsTableAdapter">
        <SelectParameters>
            <asp:Parameter DefaultValue="reference" Name="type" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
</div>
<div>        
    <asp:Panel ID="Panel1" runat="server" CssClass="mypanel">
        <table style="width:100%;" cellpadding="0">
            <tr>
                <td align="left">
                    Request Type&nbsp; &nbsp;
                    <asp:DropDownList ID="ddl_typerequest" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddl_typerequest_SelectedIndexChanged" CausesValidation="false">
                    </asp:DropDownList>
                </td>
                <td align="left" rowspan="2">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                        <table cellpadding="0" cellspacing="0" style="width:100%;">                            
                            <tr>
                                <td>
                                    Project Name &nbsp;&nbsp;
                                    <asp:DropDownList ID="ddl_project" runat="server" AutoPostBack="True" 
                                        onselectedindexchanged="ddl_project_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    Project type: <asp:Label ID="lbl_prj_type" runat="server" CssClass="mylabel"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    Project Category: <asp:Label ID="lbl_prj_category" runat="server" CssClass="mylabel"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    Project Brand: <asp:Label ID="lbl_prj_brand" runat="server" CssClass="mylabel"></asp:Label>
                                </td>
                            </tr>
                        </table>                        
                        </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                <td align="left">
                    Type Of Analysis&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:DropDownList ID="ddl_typeanalysis" runat="server">
                    </asp:DropDownList>
                </td>
                <td align="right" class="status" valign="top">
                    <asp:Label ID="lbl_status" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="left">
                    Request No.&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lbl_requestid" runat="server" CssClass="mylabel"></asp:Label>
                </td>
                
                <td align="left">
                    As Proj Lead/ Designate&nbsp; &nbsp;&nbsp;
                    <asp:DropDownList ID="ddl_lead" runat="server">                            
                    </asp:DropDownList>
                </td>
                <td align="left">
                    &nbsp;</td>
            </tr>                               
        </table>
    </asp:Panel>
    <asp:Panel ID="Panel2" runat="server" CssClass="mypanel" GroupingText="Samples">
        <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" >
            <asp:TabPanel ID="TabPanel1" runat="server">
                <HeaderTemplate>
                    <div style="width: 44px; font-weight: bold; color: #000000; font-size: 14px;">
                        1-4
                    </div>
                </HeaderTemplate>
                <ContentTemplate>
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%" 
                        CssClass="mygrid" EnableModelValidation="True" onrowdatabound="GridView_RowDataBound">
                        <HeaderStyle CssClass="mygridheader"/>
                        <Columns>                                
                            <asp:BoundField HeaderText="ID" DataField="propertyid" >
                            <ItemStyle Width="20px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Property" DataField="propertyname" >
                            <ItemStyle Width="150px" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Sample1">
                                <ItemTemplate>
                                    <asp:TextBox ID="tb1" runat="server" Text='<%#Bind("1") %>'></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                        ControlToValidate="tb1" ErrorMessage="*" CssClass="failureNotification" ValidationGroup="submit">
                                    </asp:RequiredFieldValidator>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sample2">
                                <ItemTemplate>
                                    <asp:TextBox ID="tb2" runat="server" Text='<%#Bind("2") %>'></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                        ControlToValidate="tb2" ErrorMessage="*" CssClass="failureNotification" ValidationGroup="submit">
                                    </asp:RequiredFieldValidator>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sample3">
                                <ItemTemplate>
                                    <asp:TextBox ID="tb3" runat="server" Text='<%#Bind("3") %>'></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                        ControlToValidate="tb3" ErrorMessage="*" CssClass="failureNotification" ValidationGroup="submit">
                                    </asp:RequiredFieldValidator>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sample4">
                                <ItemTemplate>
                                    <asp:TextBox ID="tb4" runat="server" Text='<%#Bind("4") %>'></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                                        ControlToValidate="tb4" ErrorMessage="*" CssClass="failureNotification" ValidationGroup="submit">
                                    </asp:RequiredFieldValidator>
                                </ItemTemplate>
                            </asp:TemplateField>
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
                        Width="100%" CssClass="mygrid" onrowdatabound="GridView_RowDataBound">
                        <HeaderStyle CssClass="mygridheader"/>
                        <Columns>
                            <asp:BoundField HeaderText="ID" DataField="propertyid" ItemStyle-Width="20px" />
                            <asp:BoundField HeaderText="Property" DataField="propertyname" ItemStyle-Width="150px" />
                            <asp:TemplateField HeaderText="Sample5">
                                <ItemTemplate>
                                    <asp:TextBox ID="tb5" runat="server" Text='<%#Bind("5") %>'></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                                        ControlToValidate="tb5" ErrorMessage="*" CssClass="failureNotification" ValidationGroup="submit">
                                    </asp:RequiredFieldValidator>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sample6">
                                <ItemTemplate>
                                    <asp:TextBox ID="tb6" runat="server" Text='<%#Bind("6") %>'></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                                        ControlToValidate="tb6" ErrorMessage="*" CssClass="failureNotification" ValidationGroup="submit">
                                    </asp:RequiredFieldValidator>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sample7">
                                <ItemTemplate>
                                    <asp:TextBox ID="tb7" runat="server" Text='<%#Bind("7") %>'></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
                                        ControlToValidate="tb7" ErrorMessage="*" CssClass="failureNotification" ValidationGroup="submit">
                                    </asp:RequiredFieldValidator>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sample8">
                                <ItemTemplate>
                                    <asp:TextBox ID="tb8" runat="server" Text='<%#Bind("8") %>'></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" 
                                        ControlToValidate="tb8" ErrorMessage="*" CssClass="failureNotification" ValidationGroup="submit">
                                    </asp:RequiredFieldValidator>
                                </ItemTemplate>
                            </asp:TemplateField>
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
                        Width="100%" CssClass="mygrid" onrowdatabound="GridView_RowDataBound">
                        <HeaderStyle CssClass="mygridheader"/>
                        <Columns>
                            <asp:BoundField HeaderText="ID" DataField="propertyid" ItemStyle-Width="20px" />
                            <asp:BoundField HeaderText="Property" DataField="propertyname" ItemStyle-Width="150px" />
                            <asp:TemplateField HeaderText="Sample9">
                                <ItemTemplate>
                                    <asp:TextBox ID="tb9" runat="server" Text='<%#Bind("9") %>'></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" 
                                        ControlToValidate="tb9" ErrorMessage="*" CssClass="failureNotification" ValidationGroup="submit">
                                    </asp:RequiredFieldValidator>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sample10">
                                <ItemTemplate>
                                    <asp:TextBox ID="tb10" runat="server" Text='<%#Bind("10") %>'></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" 
                                        ControlToValidate="tb10" ErrorMessage="*" CssClass="failureNotification" ValidationGroup="submit">
                                    </asp:RequiredFieldValidator>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sample11">
                                <ItemTemplate>
                                    <asp:TextBox ID="tb11" runat="server" Text='<%#Bind("11") %>'></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" 
                                        ControlToValidate="tb11" ErrorMessage="*" CssClass="failureNotification" ValidationGroup="submit">
                                    </asp:RequiredFieldValidator>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sample12">
                                <ItemTemplate>
                                    <asp:TextBox ID="tb12" runat="server" Text='<%#Bind("12") %>'></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" 
                                        ControlToValidate="tb12" ErrorMessage="*" CssClass="failureNotification" ValidationGroup="submit">
                                    </asp:RequiredFieldValidator>
                                </ItemTemplate>
                            </asp:TemplateField>
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
                        Width="100%" CssClass="mygrid" onrowdatabound="GridView_RowDataBound">
                        <HeaderStyle CssClass="mygridheader"/>
                        <Columns>
                            <asp:BoundField HeaderText="ID" DataField="propertyid" ItemStyle-Width="20px" />
                            <asp:BoundField HeaderText="Property" DataField="propertyname" ItemStyle-Width="150px" />
                            <asp:TemplateField HeaderText="Sample13">
                                <ItemTemplate>
                                    <asp:TextBox ID="tb13" runat="server" Text='<%#Bind("13") %>'></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" 
                                        ControlToValidate="tb13" ErrorMessage="*" CssClass="failureNotification" ValidationGroup="submit">
                                    </asp:RequiredFieldValidator>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sample14">
                                <ItemTemplate>
                                    <asp:TextBox ID="tb14" runat="server" Text='<%#Bind("14") %>'></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" 
                                        ControlToValidate="tb14" ErrorMessage="*" CssClass="failureNotification" ValidationGroup="submit">
                                    </asp:RequiredFieldValidator>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sample15">
                                <ItemTemplate>
                                    <asp:TextBox ID="tb15" runat="server" Text='<%#Bind("15") %>'></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" 
                                        ControlToValidate="tb15" ErrorMessage="*" CssClass="failureNotification" ValidationGroup="submit">
                                    </asp:RequiredFieldValidator>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sample16">
                                <ItemTemplate>
                                    <asp:TextBox ID="tb16" runat="server" Text='<%#Bind("16") %>'></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" 
                                        ControlToValidate="tb16" ErrorMessage="*" CssClass="failureNotification" ValidationGroup="submit">
                                    </asp:RequiredFieldValidator>
                                </ItemTemplate>
                            </asp:TemplateField>
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
                        Width="100%" CssClass="mygrid" onrowdatabound="GridView_RowDataBound">
                        <HeaderStyle CssClass="mygridheader"/>
                        <Columns>
                            <asp:BoundField HeaderText="ID" DataField="propertyid" ItemStyle-Width="20px" />
                            <asp:BoundField HeaderText="Property" DataField="propertyname" ItemStyle-Width="150px" />
                            <asp:TemplateField HeaderText="Sample17">
                                <ItemTemplate>
                                    <asp:TextBox ID="tb17" runat="server" Text='<%#Bind("17") %>'></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" 
                                        ControlToValidate="tb17" ErrorMessage="*" CssClass="failureNotification" ValidationGroup="submit">
                                    </asp:RequiredFieldValidator>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sample18">
                                <ItemTemplate>
                                    <asp:TextBox ID="tb18" runat="server" Text='<%#Bind("18") %>'></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" 
                                        ControlToValidate="tb18" ErrorMessage="*" CssClass="failureNotification" ValidationGroup="submit">
                                    </asp:RequiredFieldValidator>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sample19">
                                <ItemTemplate>
                                    <asp:TextBox ID="tb19" runat="server" Text='<%#Bind("19") %>'></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" 
                                        ControlToValidate="tb19" ErrorMessage="*" CssClass="failureNotification" ValidationGroup="submit">
                                    </asp:RequiredFieldValidator>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sample20">
                                <ItemTemplate>
                                    <asp:TextBox ID="tb20" runat="server" Text='<%#Bind("20") %>'></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" 
                                        ControlToValidate="tb20" ErrorMessage="*" CssClass="failureNotification" ValidationGroup="submit">
                                    </asp:RequiredFieldValidator>
                                </ItemTemplate>
                            </asp:TemplateField>
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
                        Width="100%" CssClass="mygrid" onrowdatabound="GridView_RowDataBound">
                        <HeaderStyle CssClass="mygridheader"/>
                        <Columns>
                            <asp:BoundField HeaderText="ID" DataField="propertyid" ItemStyle-Width="20px" />
                            <asp:BoundField HeaderText="Property" DataField="propertyname" ItemStyle-Width="150px" />
                            <asp:TemplateField HeaderText="Sample21">
                                <ItemTemplate>
                                    <asp:TextBox ID="tb21" runat="server" Text='<%#Bind("21") %>'></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" 
                                        ControlToValidate="tb21" ErrorMessage="*" CssClass="failureNotification" ValidationGroup="submit">
                                    </asp:RequiredFieldValidator>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sample22">
                                <ItemTemplate>
                                    <asp:TextBox ID="tb22" runat="server" Text='<%#Bind("22") %>'></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" 
                                        ControlToValidate="tb22" ErrorMessage="*" CssClass="failureNotification" ValidationGroup="submit">
                                    </asp:RequiredFieldValidator>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sample23">
                                <ItemTemplate>
                                    <asp:TextBox ID="tb23" runat="server" Text='<%#Bind("23") %>'></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" 
                                        ControlToValidate="tb23" ErrorMessage="*" CssClass="failureNotification" ValidationGroup="submit">
                                    </asp:RequiredFieldValidator>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sample24">
                                <ItemTemplate>
                                    <asp:TextBox ID="tb24" runat="server" Text='<%#Bind("24") %>'></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" 
                                        ControlToValidate="tb24" ErrorMessage="*" CssClass="failureNotification" ValidationGroup="submit">
                                    </asp:RequiredFieldValidator>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </ContentTemplate>
            </asp:TabPanel>
        </asp:TabContainer>
    </asp:Panel>
    <asp:Panel ID="Panel3" runat="server" CssClass="mypanel" 
        GroupingText="Additional Information (1000 characters)">
        <asp:TextBox ID="tb_addinfo" runat="server" TextMode="MultiLine" Rows="4" 
            Width="100%" onkeyup="count(this,1000)" onmouseover="this.style.backgroundColor='#EAF5FB'" onmouseout="this.style.backgroundColor='white'">
        </asp:TextBox>
        <asp:TextBox ID="count_addinfo" runat="server" MaxLength="4" ReadOnly="True" 
            Text="1000" Width="30px"></asp:TextBox>               
        <span style="font-size: 13px">characters remaining.</span>
    </asp:Panel>
    <asp:Panel ID="Panel4" runat="server" CssClass="mypanel" GroupingText="Tests">
        <asp:GridView ID="gv_tests" runat="server" AutoGenerateColumns="False" 
            Width="100%" CssClass="mygrid" DataKeyNames="test_id"
            EnableModelValidation="True" onrowdatabound="gv_tests_RowDataBound">
        <HeaderStyle CssClass="mygridheader"/>
            <Columns>
                <asp:BoundField DataField="test_id" HeaderText="ID" Visible="false"/>
                <asp:TemplateField HeaderText="Test Parameter">
                    <ItemTemplate>
                        <asp:TextBox ID="tb_testname" runat="server" Text='<%#Bind("testname") %>'></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tb_testname" 
                            ErrorMessage="*" CssClass="failureNotification" ValidationGroup="submit">
                        </asp:RequiredFieldValidator>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Reference">
                    <ItemTemplate>
                        <asp:DropDownList ID="dd_specification" runat="server" DataSourceID="ods_TestReference" 
                            DataTextField="value" DataValueField="value"/>                        
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Standard/Limit">
                    <ItemTemplate>
                        <asp:TextBox ID="tb_standard" runat="server" Text='<%#Bind("standard") %>'></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tb_standard" 
                            ErrorMessage="*" CssClass="failureNotification" ValidationGroup="submit">
                        </asp:RequiredFieldValidator>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Unit">
                    <ItemTemplate>
                        <asp:TextBox ID="tb_unit" runat="server" Text='<%#Bind("unit") %>'></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="tb_unit" 
                            ErrorMessage="*" CssClass="failureNotification" ValidationGroup="submit">
                        </asp:RequiredFieldValidator>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </asp:Panel>
    <asp:Panel ID="Panel5" runat="server" CssClass="mypanel" GroupingText="Select Samples for the Tests">            
            <asp:GridView ID="gv_test_sample" runat="server" Width="100%" AutoGenerateColumns="False" CssClass="mygrid" 
                onrowcreated="gv_test_sample_RowCreated" DataKeyNames="test_id" ondatabound="gv_test_sample_DataBound" 
                EnableViewState="true">
                <HeaderStyle CssClass="mygridheader"/>
                <EditRowStyle CssClass="mygridedit"/>
                <Columns>                                        
                <asp:TemplateField HeaderText="Edit" ItemStyle-Width="50px" ItemStyle-BackColor="#F2F2F2">
                    <ItemTemplate>
                        <asp:ImageButton ID="imgbtn_Edit" runat="server"
                            CausesValidation="false" ImageUrl="~/image/edit.png" AlternateText="Edit" 
                            ToolTip="Edit" onclick="imgbtn_Edit_Click" />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:ImageButton ID="imgbtn_Update" runat="server"
                            ImageUrl="~/image/tick.jpg" AlternateText="Update" 
                            ToolTip="Update" onclick="imgbtn_Update_Click" CausesValidation="False" />&nbsp;&nbsp;&nbsp;
                       <asp:ImageButton ID="imgbtn_Cancel" runat="server"
                            CausesValidation="false" ImageUrl="~/image/cross.jpg" AlternateText="Cancel" 
                            ToolTip="Cancel" onclick="imgbtn_Cancel_Click" />
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="test_id" HeaderText="Id" ReadOnly="True" ItemStyle-Width="30px" 
                    ItemStyle-BackColor="#F2F2F2"   Visible="False" />
                <asp:BoundField DataField="testname" HeaderText="Test↓/Sample→" ReadOnly="True" ItemStyle-Width="80px" 
                    ItemStyle-BackColor="#F2F2F2"   />
                </Columns>
            </asp:GridView>
        </asp:Panel>
    <asp:Panel ID="Panel6" runat="server" CssClass="mypanel" DefaultButton="btn_submit">
        <asp:Button ID="btn_cancel" runat="server" Text="Cancel" Font-Bold="True" 
            Height="28px" Width="120px" CausesValidation="False" 
            UseSubmitBehavior="False" PostBackUrl="~/UI/users/home.aspx" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btn_submit" runat="server" Text="Submit" Font-Bold="True" 
            Height="28px" Width="120px" onclick="btn_submit_Click" 
            onclientclick="return ValidatePage();" ValidationGroup="submit" />
    </asp:Panel>
    </div>
</asp:Content>

