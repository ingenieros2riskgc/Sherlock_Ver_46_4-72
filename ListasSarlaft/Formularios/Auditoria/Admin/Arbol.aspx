<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Arbol.aspx.cs" Inherits="ListasSarlaft.Formularios.Auditoria.Admin.Arbol" %>
<%@ Import Namespace="System.Web.Configuration" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Data.SqlClient" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">



<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<style type="text/css">
    .style1
    {
        width: 123px;
    }
</style>

<body>
    <form id="form1" runat="server">
    <div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
                <table>
                    <tr>
                        <td>
                            <asp:Panel ID="Panel1" runat="server" BorderStyle="Inset" BorderWidth="1px" Width="600px">
                                <asp:TreeView ID="TreeView1" ExpandDepth="0" OnTreeNodePopulate="TreeView1_SelectedNodeChanged"
                                    runat="server" Font-Names="Calibri" Font-Size="Small" LineImagesFolder="~/TreeLineImages"
                                    ForeColor="Black" ShowLines="True" OnSelectedNodeChanged="TreeView1_SelectedNodeChanged1">
                                    <SelectedNodeStyle BackColor="#FFFF99" BorderColor="#66CCFF" BorderStyle="Inset" />
                                </asp:TreeView>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table align="center">
                                <tr>
                                    <td>
                                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Imagenes/Icons/edit.png" />
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Imagenes/Icons/delete.png" />
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Imagenes/Icons/Add.png" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table width="100%" border="1" cellspacing="0" cellpadding="2" bordercolor="White">
                                <tr>
                                    <td align="left" bgcolor="#BBBBBB">
                                        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBox1" runat="server" Width="360px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" bgcolor="#BBBBBB">
                                        <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBox2" runat="server" Width="360px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
