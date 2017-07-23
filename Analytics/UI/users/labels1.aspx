<%@ Page Language="C#" AutoEventWireup="true" CodeFile="labels1.aspx.cs" Inherits="UI_users_labels1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Analytics (V1.0)</title>
</head>
<body>
    <form id="form1" runat="server" style="font-family: calibri">
    <div style="margin: 10px">
        <br />
        <asp:Repeater ID="Repeater1" runat="server" 
            onitemdatabound="Repeater1_ItemDataBound">
            <ItemTemplate>
                <table width="60%" >
                    <tr>
                        <td>
                            <asp:Label ID="lbl_reqref1" runat="server" Font-Bold="True" Font-Size="16px"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lbl_reqref2" runat="server" Font-Bold="True" Font-Size="16px"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lbl_reqref3" runat="server" Font-Bold="True" Font-Size="16px"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lbl_sample_code1" runat="server"  Text='<%#Bind("sampleid")%>' Font-Bold="True" Font-Size="16px"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lbl_sample_code2" runat="server" Text='<%#Bind("sampleid")%>' Font-Bold="True" Font-Size="16px"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lbl_sample_code3" runat="server" Text='<%#Bind("sampleid")%>' Font-Bold="True" Font-Size="16px"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lbl_sample_quantity1" runat="server" Text='<%#Bind("sample_quantity")%>' Font-Bold="True" Font-Size="16px"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lbl_sample_quantity2" runat="server" Text='<%#Bind("sample_quantity")%>' Font-Bold="True" Font-Size="16px"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lbl_sample_quantity3" runat="server" Text='<%#Bind("sample_quantity")%>' Font-Bold="True" Font-Size="16px"></asp:Label>
                        </td>
                    </tr>
                </table>
                <br />
            </ItemTemplate>
        </asp:Repeater>
        <br />
        <div style="font-size: 16px ; font-weight:bold">
            To,<br />
            <asp:Label ID="lbl_name1" runat="server" Text="Label"></asp:Label>/ <asp:Label ID="lbl_name2" runat="server" Text="Label"></asp:Label><br />
            <asp:Label ID="lbl_lab_name" runat="server" Text="Label"></asp:Label><br />
            <asp:Label ID="lbl_lab_add" runat="server" Text="Label"></asp:Label><br />
            <asp:Label ID="lbl_lab_city" runat="server" Text="Label"></asp:Label><br />
            Email: <asp:Label ID="lbl_email1" runat="server" Text="Label"></asp:Label><br />
            Mbl: <asp:Label ID="lbl_mbl1" runat="server" Text="Label"></asp:Label><br />
            Phn: <asp:Label ID="lbl_phn1" runat="server" Text="Label"></asp:Label><br />
        </div>
    </div>
    </form>
</body>
</html>
