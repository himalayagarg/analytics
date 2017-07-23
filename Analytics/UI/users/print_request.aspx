<%@ Page Language="C#" AutoEventWireup="true" CodeFile="print_request.aspx.cs" Inherits="UI_users_print_request" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="~/Styles/Site.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server" style="background-color: #FFFFFF">
    <div style="font-size: 12px;" >
        <asp:Label ID="lbl_requestor" runat="server" Font-Bold="true"/>
        <asp:Panel ID="Panel1" runat="server" CssClass="mypanel">
            <table style="width:100%;" cellpadding="5" rules="all">
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
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                Width="100%" CssClass="mygrid" 
                EnableModelValidation="True">
                <Columns>
                    <asp:BoundField DataField="propertyid" HeaderText="ID">
                    <ItemStyle Width="20px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="propertyname" HeaderText="Property">
                    <ItemStyle Width="150px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="1" HeaderText="Sample1" />
                    <asp:BoundField DataField="2" HeaderText="Sample2" />
                    <asp:BoundField DataField="3" HeaderText="Sample3" />
                    <asp:BoundField DataField="4" HeaderText="Sample4" />
                </Columns>
                <HeaderStyle CssClass="mygridheader"/>
            </asp:GridView>        
            <br />
            <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
                CssClass="mygrid" Width="100%">
                <HeaderStyle CssClass="mygridheader" />
                <Columns>
                    <asp:BoundField DataField="propertyid" HeaderText="ID" ItemStyle-Width="20px" />
                    <asp:BoundField DataField="propertyname" HeaderText="Property" 
                        ItemStyle-Width="150px" />
                    <asp:BoundField DataField="5" HeaderText="Sample5" />
                    <asp:BoundField DataField="6" HeaderText="Sample6" />
                    <asp:BoundField DataField="7" HeaderText="Sample7" />
                    <asp:BoundField DataField="8" HeaderText="Sample8" />
                </Columns>
            </asp:GridView>
            <br />
            <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" 
                CssClass="mygrid" Width="100%">
                <HeaderStyle CssClass="mygridheader" />
                <Columns>
                    <asp:BoundField DataField="propertyid" HeaderText="ID" ItemStyle-Width="20px" />
                    <asp:BoundField DataField="propertyname" HeaderText="Property" 
                        ItemStyle-Width="150px" />
                    <asp:BoundField DataField="9" HeaderText="Sample9" />
                    <asp:BoundField DataField="10" HeaderText="Sample10" />
                    <asp:BoundField DataField="11" HeaderText="Sample11" />
                    <asp:BoundField DataField="12" HeaderText="Sample12" />
                </Columns>
            </asp:GridView>
            <br />
            <asp:GridView ID="GridView4" runat="server" AutoGenerateColumns="False" 
                CssClass="mygrid" Width="100%">
                <HeaderStyle CssClass="mygridheader" />
                <Columns>
                    <asp:BoundField DataField="propertyid" HeaderText="ID" ItemStyle-Width="20px" />
                    <asp:BoundField DataField="propertyname" HeaderText="Property" 
                        ItemStyle-Width="150px" />
                    <asp:BoundField DataField="13" HeaderText="Sample13" />
                    <asp:BoundField DataField="14" HeaderText="Sample14" />
                    <asp:BoundField DataField="15" HeaderText="Sample15" />
                    <asp:BoundField DataField="16" HeaderText="Sample16" />
                </Columns>
            </asp:GridView>
            <br />
            <asp:GridView ID="GridView5" runat="server" AutoGenerateColumns="False" 
                CssClass="mygrid" Width="100%">
                <HeaderStyle CssClass="mygridheader" />
                <Columns>
                    <asp:BoundField DataField="propertyid" HeaderText="ID" ItemStyle-Width="20px" />
                    <asp:BoundField DataField="propertyname" HeaderText="Property" 
                        ItemStyle-Width="150px" />
                    <asp:BoundField DataField="17" HeaderText="Sample17" />
                    <asp:BoundField DataField="18" HeaderText="Sample18" />
                    <asp:BoundField DataField="19" HeaderText="Sample19" />
                    <asp:BoundField DataField="20" HeaderText="Sample20" />
                </Columns>
            </asp:GridView>
            <br />
            <asp:GridView ID="GridView6" runat="server" AutoGenerateColumns="False" 
                CssClass="mygrid" Width="100%">
                <HeaderStyle CssClass="mygridheader" />
                <Columns>
                    <asp:BoundField DataField="propertyid" HeaderText="ID" ItemStyle-Width="20px" />
                    <asp:BoundField DataField="propertyname" HeaderText="Property" 
                        ItemStyle-Width="150px" />
                    <asp:BoundField DataField="21" HeaderText="Sample21" />
                    <asp:BoundField DataField="22" HeaderText="Sample22" />
                    <asp:BoundField DataField="23" HeaderText="Sample23" />
                    <asp:BoundField DataField="24" HeaderText="Sample24" />
                </Columns>
            </asp:GridView>
        </asp:Panel>
        <asp:Panel ID="Panel3" runat="server" CssClass="mypanel" 
            GroupingText="Additional Information (1000 characters)">
            <asp:TextBox ID="tb_addinfo" runat="server" TextMode="MultiLine" Rows="4" Width="100%" ReadOnly="True"></asp:TextBox>
        </asp:Panel>
        <asp:Panel ID="Panel4" runat="server" CssClass="mypanel" GroupingText="Tests">
            <asp:GridView ID="gv_tests" runat="server" AutoGenerateColumns="False" DataKeyNames="test_id"
                Width="100%" CssClass="mygrid">
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
    </div>
    </form>
</body>
</html>
