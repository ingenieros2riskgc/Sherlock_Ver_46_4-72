<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Roles.ascx.cs" Inherits="ListasSarlaft.UserControls.AdminUsers.Roles" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<script type="text/javascript">
    function fnClick(sender, e) {
        __doPostBack(sender, e);
    }
</script>
<style type="text/css">
    .gridViewHeader a:link
    {
        text-decoration: none;
    }
    .FondoAplicacion
    {
        background-color: Gray;
        filter: alpha(opacity=80);
        opacity: 0.8;
    }
    .scrollingControlContainer
    {
        overflow-x: hidden;
        overflow-y: scroll;
    }
    .scrollingCheckBoxList
    {
        border: 1px #808080 solid;
        margin: 10px 10px 10px 10px;
        height: 200px;
    }
    .popup
    {
        border: Silver 1px solid;
        color: #060F40;
        background: #ffffff;
    }
    .LetraNormal
    {
        font-size: small;
        font-family:Calibri;
        text-align:center;
        color:White;
        font-weight:bold;
    }
</style>
<table align="center" bgcolor="#EEEEEE">
    <tr align="center" bgcolor="#333399">
        <td>
            <asp:Label ID="Label111" runat="server" Font-Bold="False" Font-Names="Calibri" Font-Size="Large"
                ForeColor="White" Text="Roles"></asp:Label>
        </td>
    </tr>
    <tr align="center">
        <td>
            <table>
                <tr align="center">
                    <td>
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4"
                            ForeColor="#333333" GridLines="Vertical" ShowHeaderWhenEmpty="True" HeaderStyle-CssClass="gridViewHeader"
                            BorderStyle="Solid" HorizontalAlign="Justify" Font-Names="Calibri" Font-Size="Small"
                            OnRowCommand="GridView1_RowCommand" AllowPaging="True" OnPageIndexChanging="GridView1_PageIndexChanging">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:BoundField HeaderText="Nombre del rol" DataField="NombreRol" />
                                <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Icons/edit.png" Text="Modificar"
                                    CommandName="Modificar" />
                                <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Icons/edit-find-replace.png"
                                    Text="Permisos" CommandName="Permisos" />
                            </Columns>
                            <EditRowStyle BackColor="#999999" />
                            <FooterStyle BackColor="White" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Left" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                        </asp:GridView>
                    </td>
                </tr>
                <tr align="right">
                    <td>
                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Imagenes/Icons/Add.png"
                            ToolTip="Agregar" OnClick="ImageButton1_Click" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr align="center" id="trCampos" runat="server" visible="false">
        <td>
            <table>
                <tr align="center">
                    <td bgcolor="#BBBBBB">
                        <asp:Label ID="Label2" runat="server" Text="Rol" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                    </td>
                    <td bgcolor="#EEEEEE">
                        <asp:TextBox ID="TextBox1" runat="server" Width="450px" Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBox1"
                            InitialValue="" ForeColor="Red" ValidationGroup="validarCampos">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr align="center">
                    <td colspan="2">
                        <table>
                            <tr>
                                <td>
                                    <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                        ToolTip="Agregar" Visible="false" ValidationGroup="validarCampos" OnClick="ImageButton2_Click" />
                                </td>
                                <td>
                                    <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                        ToolTip="Modificar" Visible="false" ValidationGroup="validarCampos" OnClick="ImageButton3_Click" />
                                </td>
                                <td>
                                    <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                        ToolTip="Cancelar" OnClick="ImageButton4_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr align="center" id="trPermisos" runat="server" visible="false">
        <td>
            <table>
                <tr align="center" bgcolor="#333399">
                    <td>
                        <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Calibri" Font-Size="Large"
                            ForeColor="White" Text="Permisos"></asp:Label>
                    </td>
                </tr>
                <%--<tr>
                    <td align="right">
                        <table runat="server">
                            <tr align="right">
                                <td colspan="4" >
                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Imagenes/Icons/SelectAll.png" />
                                </td>
                            </tr>
                            <tr align="center">
                                <td>
                                    <asp:CheckBox ID="CheckBoxConsultar" runat="server" />
                                </td>
                                <td>
                                    <asp:CheckBox ID="CheckBoxAgregar" runat="server" />
                                </td>
                                <td>
                                    <asp:CheckBox ID="CheckBoxActualizar" runat="server" />
                                </td>
                                <td>
                                    <asp:CheckBox ID="CheckBoxBorrar" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>--%>
                <tr align="center">
                    <td>
                        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" CellPadding="4"
                            ForeColor="#333333" GridLines="Vertical" ShowHeaderWhenEmpty="True" HeaderStyle-CssClass="gridViewHeader"
                            BorderStyle="Solid" HorizontalAlign="Justify" Font-Names="Calibri" Font-Size="Small">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:BoundField HeaderText="Modulo" DataField="NombreModulo" />
                                <asp:BoundField HeaderText="Sub-Modulo" DataField="NombreSubModulo" />
                                <asp:BoundField HeaderText="Formulario" DataField="NombreFormulario" />
                                <asp:TemplateField HeaderText="Consultar">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBox1" runat="server" Checked='<% # Bind("Consultar") %>' />
                                    </ItemTemplate>
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Agregar">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBox2" runat="server" Checked='<% # Bind("Agregar") %>' />
                                    </ItemTemplate>
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Actualizar">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBox3" runat="server" Checked='<% # Bind("Actualizar") %>' />
                                    </ItemTemplate>
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Borrar">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBox4" runat="server" Checked='<% # Bind("Borrar") %>' />
                                    </ItemTemplate>
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>
                            <EditRowStyle BackColor="#999999" />
                            <FooterStyle BackColor="White" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Left" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                        </asp:GridView>
                    </td>
                </tr>
                <tr align="center">
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <asp:ImageButton ID="ImageButton5" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                        ToolTip="Modificar" OnClick="ImageButton5_Click" />
                                </td>
                                <td>
                                    <asp:ImageButton ID="ImageButton6" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                        ToolTip="Cancelar" OnClick="ImageButton6_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
<asp:ModalPopupExtender ID="mpeMsgBox" runat="server" TargetControlID="btndummy"
    PopupControlID="pnlMsgBox" OkControlID="btnAceptar" BackgroundCssClass="FondoAplicacion"
    Enabled="True" DropShadow="true">
</asp:ModalPopupExtender>
<asp:Button ID="btndummy" runat="server" Text="Button" Style="display: none" />
<asp:Panel ID="pnlMsgBox" runat="server" Width="400px" Style="display: none;" BorderColor="#575757"
    BackColor="#FFFFFF" BorderStyle="Solid">
    <table width="100%">
        <tr class="topHandle" style="background-color: #5D7B9D">
            <td colspan="2" align="center" runat="server" id="tdCaption">
                &nbsp;
                <asp:Label ID="lblCaption" runat="server" Text="Atención" Font-Names="Calibri" Font-Size="Small"></asp:Label><br />
            </td>
        </tr>
        <tr>
            <td style="width: 60px" valign="middle" align="center">
                <asp:Image ID="imgInfo" runat="server" ImageUrl="~/Imagenes/Icons/icontexto-webdev-about.png" />
            </td>
            <td valign="middle" align="left">
                <asp:Label ID="lblMsgBox" runat="server" Font-Names="Calibri" Font-Size="Small"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="right" colspan="2">
                <asp:Button ID="btnAceptar" runat="server" Text="Ok" Font-Names="Calibri" Font-Size="Small" />
            </td>
        </tr>
    </table>
</asp:Panel>
