<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Parametrizacion.ascx.cs"
    Inherits="ListasSarlaft.UserControls.ConfigEstructura.Parametrizacion" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControls/Sitio/OkMessageBox.ascx" TagPrefix="uc" TagName="OkMessageBox" %>
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
</style>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <uc:OkMessageBox ID="omb" runat="server" />
        <asp:Panel ID="pnlMsgBox" runat="server" Width="400px" Style="display: none;" BorderColor="#575757"
            BackColor="#FFFFFF" BorderStyle="Solid">
            <table width="100%">
                <tr class="topHandle" style="background-color: #5D7B9D">
                    <td colspan="2" align="center" runat="server" id="tdCaption">&nbsp;
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
        <asp:ModalPopupExtender ID="mpeMsgBox" runat="server" TargetControlID="btndummy"
            PopupControlID="pnlMsgBox" OkControlID="btnAceptar" BackgroundCssClass="FondoAplicacion"
            Enabled="True" DropShadow="true">
        </asp:ModalPopupExtender>
        <asp:Button ID="btndummy" runat="server" Text="Button" Style="display: none" />
        <asp:Label ID="lblOk" runat="server" Text="Label" Visible="False"></asp:Label>
        <asp:Label ID="lblCancelar" runat="server" Text="Label" Visible="False"></asp:Label>
        <table align="center" bgcolor="#EEEEEE">
            <tr align="center" bgcolor="#333399">
                <td>
                    <asp:Label ID="Label61" runat="server" ForeColor="White" Text="Configuración Categorías"
                        Font-Bold="False" Font-Names="Calibri" Font-Size="Large"></asp:Label>
                </td>
            </tr>
            <tr align="left">
                <td>
                    <table>
                        <tr>
                            <td>
                                <asp:Panel ID="Panel1" runat="server">
                                    <asp:GridView ID="gvParametrizacion" runat="server" CellPadding="4" EnableModelValidation="True"
                                        ForeColor="#333333" GridLines="Vertical" AutoGenerateColumns="False" AllowPaging="false"
                                        OnRowCommand="gvParametrizacion_RowCommand" ShowHeaderWhenEmpty="True" HeaderStyle-CssClass="gridViewHeader"
                                        BorderStyle="Solid" HorizontalAlign="Center" Font-Names="Calibri" Font-Size="Small">
                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                        <Columns>
                                            <asp:BoundField DataField="StrIdUsuario" HeaderText="IdUsuario" Visible="False" />
                                            <asp:BoundField DataField="StrIdCategoria" HeaderText="IdParametros" Visible="False" />
                                            <asp:BoundField DataField="StrNombreCategoria" HeaderText="Nombre Categoría" />
                                            <asp:BoundField DataField="StrCodigoCategoria" HeaderText="Código Categoría" />
                                            <asp:BoundField DataField="StrIdVariable" HeaderText="IdVariable" Visible="False" />
                                            <asp:BoundField DataField="StrNombreVariable" HeaderText="Nombre Variable" />
                                            <asp:BoundField DataField="StrCalificacionCategoria" HeaderText="Calificación Categoría" />
                                            <asp:BoundField DataField="BooEsFormula" HeaderText="Condición" Visible="False" />
                                            <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Icons/edit.png" Text="Modificar"
                                                CommandName="Modificar" />
                                            <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Icons/delete.png" Text="Eliminar"
                                                CommandName="Eliminar" />                                            
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
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr align="right">
                            <td>
                                <asp:ImageButton ID="ibtnAgregar" runat="server" ImageUrl="~/Imagenes/Icons/Add.png"
                                    ToolTip="Agregar" OnClick="ibtnAgregar_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr align="center">
                <td>
                    <table id="updateUser" runat="server" visible="false">
                        <tr align="left">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label3" runat="server" Text="Código Categoría" Font-Names="Calibri"
                                    Font-Size="Small"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="tbCodParametro" runat="server" MaxLength="25" Font-Names="Calibri"
                                    Font-Size="Small" Width="200px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="updateUser1"
                                    ControlToValidate="tbCodParametro" Display="Dynamic" Font-Names="Calibri" Font-Size="Small"
                                    ForeColor="Red">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr align="left">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label5" runat="server" Text="Nombre Categoría" Font-Names="Calibri"
                                    Font-Size="Small"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="tbNombreParametro" runat="server" MaxLength="25" Font-Names="Calibri"
                                    Font-Size="Small" Width="200px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="updateUser1"
                                    ControlToValidate="tbNombreParametro" Display="Dynamic" Font-Names="Calibri"
                                    Font-Size="Small" ForeColor="Red">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr align="left">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label1" runat="server" Text="Nombre Variable" Font-Names="Calibri"
                                    Font-Size="Small"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlTipoParametro" runat="server" Width="150px" Font-Names="Calibri"
                                    Font-Size="Small">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr align="left">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label2" runat="server" Text="Calificación Categoría" Font-Names="Calibri"
                                    Font-Size="Small"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="tbCalificacion" runat="server" MaxLength="15" Font-Names="Calibri"
                                    Font-Size="Small" Width="200px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="updateUser1"
                                    ControlToValidate="tbCalificacion" Display="Dynamic" Font-Names="Calibri" Font-Size="Small"
                                    ForeColor="Red">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr align="left">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label4" runat="server" Text="Requiere Condición:" Font-Names="Calibri"
                                    Font-Size="Small"></asp:Label>
                            </td>
                            <td>
                                <asp:CheckBox ID="chbCondicion" runat="server" AutoPostBack="true" OnCheckedChanged="chbCondicion_CheckedChanged" />
                            </td>
                        </tr>
                        <tr align="left" id="trCondicion">
                            <td colspan="2">
                                <table>
                                    <tr>
                                        <td id="TdOperador" runat="server" valign="top">
                                            <asp:GridView ID="gvOperador" runat="server" CellPadding="4" ForeColor="#333333"
                                                GridLines="Vertical" AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanging="gvOperador_PageIndexChanging"
                                                OnRowCommand="gvOperador_RowCommand" ShowHeaderWhenEmpty="True" BorderStyle="Solid"
                                                HorizontalAlign="Center" Font-Names="Calibri" Font-Size="Small">
                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                <Columns>
                                                    <asp:BoundField DataField="StrIdOperador" HeaderText="IdOperador" Visible="False" />
                                                    <asp:BoundField DataField="StrNombreOperador" HeaderText="Nombre Operador" />
                                                    <asp:BoundField DataField="StrIdentificadorOperador" HeaderText="Identificador" />
                                                    <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Icons/select.png" Text="SelecOperador"
                                                        CommandName="SelecOperador" />
                                                </Columns>
                                                <EditRowStyle BackColor="#999999" />
                                                <FooterStyle BackColor="White" Font-Bold="True" ForeColor="White" />
                                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" CssClass="gridViewHeader" />
                                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Left" />
                                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                            </asp:GridView>
                                        </td>
                                        <td id="Td1" runat="server"></td>
                                        <td id="TdOtro" runat="server" valign="top" align="left">
                                            <table>
                                                <tr id="trOtroValor" align="left">
                                                    <td>
                                                        <asp:Label ID="lblOtroValor" runat="server" Text="Otro Valor:" Font-Names="Calibri"
                                                            Font-Size="Small"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="tbxOtroValor" runat="server" Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:ImageButton ID="ibtnSelecOtroValor" runat="server" ToolTip="Seleccionar" OnClick="ibtnSelecOtroValor_Click"
                                                            ImageUrl="~/Imagenes/Icons/select.png" />
                                                    </td>
                                                </tr>
                                                <tr align="left">
                                                    <td colspan="3">
                                                        <table id="TblRango">
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblDesde" runat="server" Text="Desde:" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="tbxDesde" runat="server" Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblHasta" runat="server" Text="Hasta:" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="tbxHasta" runat="server" Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:ImageButton ID="ibtnRango" runat="server" ImageUrl="~/Imagenes/Icons/select.png"
                                                                        OnClick="ibtnRango_Click" ToolTip="Seleccionar" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr align="center">
                            <td colspan="2">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:ImageButton ID="ibtnGuardar" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                ToolTip="Guardar" Visible="false" OnClick="ibtnGuardar_Click" ValidationGroup="saveUser1" />
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="ibtnGuardarUpd" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                ToolTip="Modificar" Visible="false" OnClick="ibtnGuardarUpd_Click" ValidationGroup="updateUser1" />
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="ibtnCancelUpd" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                                ToolTip="Cancelar" OnClick="ibtnCancelUpd_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>
<%--The fnClick javascript function will force the btnOk button to post back. 
(to actually achieve this we need to add a little piece of code in 
the Page_Load method of the user control.--%>
<script type="text/javascript">
    function fnClick(sender, e) {
        __doPostBack(sender, e);
    }
</script>
