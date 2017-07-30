<%@ Page Language="C#" AutoEventWireup="true" CodeFile="print_request.aspx.cs" Inherits="UI_users_print_request" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Print PDF</title>
    <link href="~/Styles/Print.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">

        <table id="tbl_Request" class="mytable" cellpadding="1" cellspacing="0">
            <tbody>
                <tr class="mytableheader">
                    <th>
                        <asp:Label ID="lbl_requestor" runat="server" Font-Bold="true" />
                    </th>
                </tr>
                <tr>
                    <td>
                        <table style="width: 100%;" cellpadding="2">
                            <tr>
                                <td align="left">Project Name:
                            <br />
                                    <asp:Label ID="lbl_project" runat="server" CssClass="mylabel"></asp:Label>
                                </td>
                                <td align="left">Project type:
                            <br />
                                    <asp:Label ID="lbl_prj_type" runat="server" CssClass="mylabel"></asp:Label>
                                </td>
                                <td align="left">Type Of Analysis:
                            <br />
                                    <asp:Label ID="lbl_typeanalysis" runat="server" CssClass="mylabel"></asp:Label>
                                </td>
                                <td align="left">Request No:
                            <br />
                                    <asp:Label ID="lbl_requestid" runat="server" CssClass="mylabel"></asp:Label>
                                </td>
                                <td align="left">Request Status: 
                            <br />
                                    <asp:Label ID="lbl_status" runat="server" CssClass="status"></asp:Label>
                                </td>
                            </tr>
                            <tr style="border-top: 1px solid #000;">
                                <td align="left">Project Category:
                            <br />
                                    <asp:Label ID="lbl_prj_category" runat="server" CssClass="mylabel"></asp:Label>
                                </td>
                                <td align="left">Project Brand:
                            <br />
                                    <asp:Label ID="lbl_prj_brand" runat="server" CssClass="mylabel"></asp:Label>
                                </td>
                                <td align="left">AS Proj Lead/ Designate:
                            <br />
                                    <asp:Label ID="lbl_lead" runat="server" CssClass="mylabel"></asp:Label>
                                </td>
                                <td align="left">Request Type:
                            <br />
                                    <asp:Label ID="lbl_typerequest" runat="server" CssClass="mylabel"></asp:Label>
                                </td>
                                <td align="left"></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </tbody>
        </table>

        <table id="tbl_Samples" class="mytable" cellpadding="1" cellspacing="0">
            <tbody>
                <tr class="mytableheader">
                    <th>Samples</th>
                </tr>
                <tr>
                    <td>
                        <asp:Panel ID="pnl_Samples" runat="server">
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
                                Width="100%" CssClass="mygrid margin-bottom-10" EnableModelValidation="True">
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
                                <HeaderStyle CssClass="mygridheader" />
                            </asp:GridView>

                            <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False"
                                CssClass="mygrid margin-bottom-10" Width="100%">
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

                            <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False"
                                CssClass="mygrid margin-bottom-10" Width="100%">
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

                            <asp:GridView ID="GridView4" runat="server" AutoGenerateColumns="False"
                                CssClass="mygrid margin-bottom-10" Width="100%">
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

                            <asp:GridView ID="GridView5" runat="server" AutoGenerateColumns="False"
                                CssClass="mygrid margin-bottom-10" Width="100%">
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

                            <asp:GridView ID="GridView6" runat="server" AutoGenerateColumns="False"
                                CssClass="mygrid margin-bottom-10" Width="100%">
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
                    </td>
                </tr>
            </tbody>
        </table>

        <asp:Panel ID="pnl_LabResults" runat="server" Visible="false">
            <table id="tbl_LabResults" class="mytable" cellpadding="1" cellspacing="0">
                <tbody>
                    <tr class="mytableheader">
                        <th>Lab Results</th>
                        <th class="right">Result Date:
                            <asp:Label ID="lbl_resultdate" runat="server"></asp:Label>
                        </th>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:GridView ID="gv_view_result" runat="server" AutoGenerateColumns="False" CssClass="mygrid"
                                DataKeyNames="labresult_id" Width="100%" CellPadding="2">
                                <HeaderStyle CssClass="mygridheader" />
                                <Columns>
                                    <asp:BoundField DataField="labresult_id" HeaderText="ID" Visible="false" />
                                    <asp:TemplateField HeaderText="S.No">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="25px" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="testname" HeaderText="Test" />
                                    <asp:TemplateField HeaderText="Sample">
                                        <ItemTemplate>
                                            <%# Eval("sampleid").ToString().Substring(8, 2)%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="46px" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="labname" HeaderText="Lab" />
                                    <asp:BoundField DataField="mth_ref" HeaderText="Method Ref." />
                                    <asp:BoundField DataField="result" HeaderText="Result" />
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2"><b>Remark:</b><br />
                            <asp:Label ID="lbl_remark_fromResult" runat="server"></asp:Label>
                        </td>
                    </tr>
                </tbody>
            </table>
        </asp:Panel>

        <table id="tbl_Tests" class="mytable" cellpadding="1" cellspacing="0">
            <tbody>
                <tr class="mytableheader">
                    <th>Tests</th>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="gv_tests" runat="server" AutoGenerateColumns="False" DataKeyNames="test_id"
                            Width="100%" CssClass="mygrid">
                            <HeaderStyle CssClass="mygridheader" />
                            <Columns>
                                <asp:BoundField DataField="test_id" HeaderText="ID" Visible="false" />
                                <asp:BoundField DataField="testname" HeaderText="Test Parameter" />
                                <asp:BoundField DataField="reference" HeaderText="Reference" />
                                <asp:BoundField DataField="standard" HeaderText="Standard/Limit" />
                                <asp:BoundField DataField="unit" HeaderText="Unit" />
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </tbody>
        </table>

        <table id="tbl_TestSamples" class="mytable" cellpadding="1" cellspacing="0">
            <tbody>
                <tr class="mytableheader">
                    <th>Test Samples</th>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="gv_test_sample" runat="server" Width="100%" CssClass="mygrid"
                            DataKeyNames="Test" EnableViewState="true">
                            <HeaderStyle CssClass="mygridheader" />
                            <Columns>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </tbody>
        </table>
    </form>
</body>
</html>
