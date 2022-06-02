<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuestionsIframe.aspx.cs" Inherits="ListasSarlaft.UserControls.Proceso.Param.Iframe.QuestionsIframe" %>
<link href="../../../Styles/AdminSitio.css" rel="stylesheet" type="text/css" />
<%@ Register Src="~/UserControls/Sitio/OkMessageBox.ascx" TagPrefix="uc" TagName="OkMessageBox" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    
    
    <form id="formQuestion" runat="server">
        <uc:OkMessageBox ID="omb" runat="server" />
        <asp:ScriptManager ID="ScriptManager1" runat="server">
                            </asp:ScriptManager>
    <asp:Panel ID="pnlTextBoxes" runat="server">
        <asp:Label ID="IdEncuesta" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="CantQuestion" runat="server" Text="" Visible="false"></asp:Label>
    </asp:Panel>
    <hr />
        <asp:Label ID="OptionsQuestions" runat="server" Visible="false"></asp:Label>
    <asp:Button ID="btnAdd" runat="server" Text="Add New" OnClick="AddTextBox" Visible="false"/>
    <asp:Button ID="btnGet" runat="server" Text="Get Values" OnClick="GetTextBoxValues"  Visible="false"/>
        <div id="DVbuttons" class="ColumnStyle" runat="server">
            <div id="ButtonsContents" class="TableContains">
                <Table class="tabla" align="center" width="80%" id="questionContents" runat="server">
                <tr>
                        <td>
                            <asp:ImageButton ID="IBinsertGVC" runat="server" CausesValidation="true" CommandName="Insert"
                                    ImageUrl="~/Imagenes/Icons/guardar.png" Text="Insert" ValidationGroup="GEvaProveedor" ToolTip="Insertar" Visible="false" OnClick="IBinsertGVC_Click" style="height: 20px"/>
            <asp:ImageButton ID="IBupdateGVC" runat="server" CausesValidation="true" CommandName="Update"
                                    ImageUrl="~/Imagenes/Icons/guardar.png" Text="Update" ValidationGroup="GEvaProveedor" ToolTip="Actualizar" Visible="false" OnClick="IBupdateGVC_Click" Width="20px"/>
                        </td>
                    </tr>
            </table>
                
        </div>
            </div>
    </form>
</body>
</html>
