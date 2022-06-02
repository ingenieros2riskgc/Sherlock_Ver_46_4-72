<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ParGruposTrabajo.ascx.cs"
    Inherits="ListasSarlaft.UserControls.Riesgos.ParGruposTrabajo" %>
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

    .style1
    {
        height: 74px;
    }
</style>
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
</style>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <table align="center" bgcolor="#EEEEEE">
            <tr align="center" bgcolor="#333399">
                <td>
                    <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Calibri" Font-Size="Large"
                        ForeColor="White" Text="Grupos de Trabajo"></asp:Label>
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
                                    AllowPaging="False" OnPageIndexChanging="GridView1_PageIndexChanging" OnRowCommand="GridView1_RowCommand">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:BoundField HeaderText="Código Grupo" DataField="IdGrupoTrabajo" >
                                        <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Nombre Grupo" DataField="Nombre" />
                                        <asp:BoundField HeaderText="Estado" DataField="Estado" />
                                        <asp:BoundField HeaderText="Fecha Registro" DataField="FechaRegistro" />
                                        <asp:BoundField HeaderText="Usuario" DataField="Usuario" />
                                        <asp:TemplateField ShowHeader="False" HeaderText="Acción">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnImgRecursos" runat="server" CausesValidation="False" CommandName="Recursos"
                                                    ImageUrl="~/Imagenes/Icons/group_edit.png" Text="Recursos" ToolTip="Recursos"
                                                    CommandArgument="<%# Container.DataItemIndex %>" />
                                                <asp:ImageButton ID="btnImgModificar" runat="server" CausesValidation="False" CommandName="Modificar"
                                                    ImageUrl="~/Imagenes/Icons/edit.png" Text="Editar" ToolTip="Editar"
                                                    CommandArgument="<%# Container.DataItemIndex %>" />
                                                <asp:ImageButton ID="btnImgEliminar" runat="server" CausesValidation="False" CommandName="Borrar"
                                                    ImageUrl="~/Imagenes/Icons/delete.png" Text="Eliminar" ToolTip="Eliminar"
                                                    CommandArgument="<%# Container.DataItemIndex %>" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
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
                        <tr align="right">
                            <td>
                                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Imagenes/Icons/Add.png"
                                    ToolTip="Insertar" OnClick="ImageButton1_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr align="center" id="trCampos" runat="server" visible="false">
                <td>
                    <table>
                        <tr align="left">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label3" runat="server" Text="Código Grupo:" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                            <td bgcolor="#EEEEEE">
                                <asp:TextBox ID="TxbCodigo" runat="server" Width="150px" Font-Names="Calibri" Font-Size="Small" MaxLength="50" Enabled="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr align="left">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label2" runat="server" Text="Nombre Grupo:" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                            <td bgcolor="#EEEEEE">
                                <asp:TextBox ID="TxbNombre" runat="server" Width="450px" Font-Names="Calibri" Font-Size="Small" MaxLength="50"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="ReqFiValNombre" runat="server" ControlToValidate="TxbNombre"
                                    InitialValue="" ForeColor="Red" ValidationGroup="validarCampos">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr align="left">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label4" runat="server" Text="Estado:" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                            <td bgcolor="#EEEEEE">
                                <asp:DropDownList ID="DdlEstado" runat="server" Font-Names="Calibri" Font-Size="Small">
                                    <asp:ListItem Value="0" Text="Inactivo"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="Activo" Selected="True"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <%--<tr align="left">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label5" runat="server" Text="Fecha:" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                            <td bgcolor="#EEEEEE">
                                <asp:TextBox ID="TxbFecha" runat="server" Width="150px" Font-Names="Calibri" Font-Size="Small" MaxLength="50" Enabled="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr align="left">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label6" runat="server" Text="Usuario:" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                            <td bgcolor="#EEEEEE">
                                <asp:TextBox ID="TxbUsuario" runat="server" Width="150px" Font-Names="Calibri" Font-Size="Small" MaxLength="50" Enabled="false"></asp:TextBox>
                            </td>
                        </tr>--%>
                        <tr align="center">
                            <td colspan="2">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                ToolTip="Guardar" Visible="false" ValidationGroup="validarCampos" OnClick="ImageButton2_Click" />
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                ToolTip="Guardar" Visible="false" ValidationGroup="validarCampos" OnClick="ImageButton3_Click" />
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
            <tr align="center" id="trRecursos" runat="server" visible="false">
                <td>
                    <table>
                        <tr align="center" bgcolor="#333399">
                            <td>
                                <asp:Label ID="Label7" runat="server" Font-Bold="False" Font-Names="Calibri" Font-Size="Medium"
                                    ForeColor="White" Text="Recursos de Grupo de Trabajo"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table>
                                    <tr align="center">
                                        <td>
                                            <asp:GridView ID="GVRecursos" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                                ForeColor="#333333" GridLines="Vertical" ShowHeaderWhenEmpty="True" HeaderStyle-CssClass="gridViewHeader"
                                                BorderStyle="Solid" HorizontalAlign="Justify" Font-Names="Calibri" Font-Size="Small"
                                                AllowPaging="True" OnPageIndexChanging="GVRecursos_PageIndexChanging" OnRowCommand="GVRecursos_RowCommand">
                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                <Columns>
                                                    <asp:BoundField HeaderText="Código Grupo" DataField="IdGrupoTrabajo" >
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="Nombre Recurso" DataField="Nombre" />
                                                    <asp:BoundField HeaderText="Correo Electrónico" DataField="Correo" />
                                                    <asp:BoundField HeaderText="Estado" DataField="Estado" />
                                                    <asp:BoundField HeaderText="Fecha Registro" DataField="FechaRegistro" />
                                                    <asp:BoundField HeaderText="Usuario" DataField="Usuario" />
                                                    <asp:TemplateField ShowHeader="False" HeaderText="Acción">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="btnImgModificar" runat="server" CausesValidation="False" CommandName="ModificarRecurso"
                                                                ImageUrl="~/Imagenes/Icons/edit.png" Text="Editar" ToolTip="Editar"
                                                                CommandArgument="<%# Container.DataItemIndex %>" />
                                                            <asp:ImageButton ID="btnImgEliminar" runat="server" CausesValidation="False" CommandName="BorrarRecurso"
                                                                ImageUrl="~/Imagenes/Icons/delete.png" Text="Eliminar" ToolTip="Eliminar"
                                                                CommandArgument="<%# Container.DataItemIndex %>" />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
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
                                    <tr align="right">
                                        <td>
                                            <asp:ImageButton ID="ImageButton9" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                                ToolTip="Cancelar" OnClick="ImageButton9_Click" />
                                        
                                            <asp:ImageButton ID="ImageButton8" runat="server" ImageUrl="~/Imagenes/Icons/Add.png"
                                                ToolTip="Insertar" OnClick="ImageButton8_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr align="center" id="TrRecurosNuevos" runat="server" visible="false">
                            <td>
                                <table>
                                    <tr align="center">
                                        <td colspan="2">
                                            <table>
                                                <tr align="left">
                                                    <td>
                                                        <asp:Label ID="Label9" runat="server" Font-Names="Calibri"
                                                            Font-Size="Small" Text="Jerarquía Organizacional"></asp:Label>
                                                        <asp:ImageButton ID="imgDependencia2" runat="server" ImageUrl="~/Imagenes/Icons/Organization-Chart.png"
                                                            OnClientClick="return false;" />
                                                        <asp:PopupControlExtender ID="popupDependencia2" runat="server" DynamicServicePath=""
                                                            Enabled="True" ExtenderControlID="" TargetControlID="imgDependencia2" BehaviorID="popup2"
                                                            PopupControlID="pnlDependencia2" OffsetY="-200">
                                                        </asp:PopupControlExtender>
                                                        <asp:Panel ID="pnlDependencia2" runat="server" CssClass="popup" Width="400px" Style="display: none">
                                                            <table width="100%" border="1" cellspacing="0" cellpadding="2" bordercolor="White">
                                                                <tr align="right" bgcolor="#5D7B9D">
                                                                    <td>
                                                                        <asp:ImageButton ID="btnClosepp2" runat="server" ImageUrl="~/Imagenes/Icons/dialog-close2.png"
                                                                            OnClientClick="$find('popup2').hidePopup(); return false;" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:TreeView ID="TreeViewJerarquiaOrg" ExpandDepth="3" runat="server" Font-Names="Calibri"
                                                                            Font-Size="Small" LineImagesFolder="~/TreeLineImages" ForeColor="Black" ShowLines="True"
                                                                            AutoGenerateDataBindings="False" OnSelectedNodeChanged="TreeViewJerarquiaOrg_SelectedNodeChanged">
                                                                            <SelectedNodeStyle BackColor="Silver" BorderColor="#66CCFF" BorderStyle="Inset" />
                                                                        </asp:TreeView>
                                                                    </td>
                                                                </tr>
                                                                <tr align="center">
                                                                    <td>
                                                                        <asp:Button ID="BtnOk2" runat="server" Text="Aceptar" CssClass="Apariencia" OnClientClick="$find('popup2').hidePopup(); return false;" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                    <td></td>
                                                    <td>
                                                        <asp:Label ID="Label10" runat="server" Font-Names="Calibri"
                                                            Font-Size="Small" Text="Recursos Parametrizados"></asp:Label>
                                                        <asp:ImageButton ID="imgDependencia3" runat="server" ImageUrl="~/Imagenes/Icons/workflow (24).png"
                                                            OnClientClick="return false;" />
                                                        <asp:PopupControlExtender ID="popupDependencia3" runat="server" DynamicServicePath=""
                                                            Enabled="True" ExtenderControlID="" TargetControlID="imgDependencia3" BehaviorID="popup3"
                                                            PopupControlID="pnlDependencia3" OffsetY="-200">
                                                        </asp:PopupControlExtender>
                                                        <asp:Panel ID="pnlDependencia3" runat="server" CssClass="popup" Width="400px" Style="display: none">
                                                            <table width="100%" border="1" cellspacing="0" cellpadding="2" bordercolor="White">
                                                                <tr align="right" bgcolor="#5D7B9D">
                                                                    <td>
                                                                        <asp:ImageButton ID="ImageButton10" runat="server" ImageUrl="~/Imagenes/Icons/dialog-close2.png"
                                                                            OnClientClick="$find('popup3').hidePopup(); return false;" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:TreeView ID="TreeViewTablaParam" ExpandDepth="3" runat="server" Font-Names="Calibri"
                                                                            Font-Size="Small" LineImagesFolder="~/TreeLineImages" ForeColor="Black" ShowLines="True"
                                                                            AutoGenerateDataBindings="False" OnSelectedNodeChanged="TreeViewTablaParam_SelectedNodeChanged">
                                                                            <SelectedNodeStyle BackColor="Silver" BorderColor="#66CCFF" BorderStyle="Inset" />
                                                                        </asp:TreeView>
                                                                    </td>
                                                                </tr>
                                                                <tr align="center">
                                                                    <td>
                                                                        <asp:Button ID="Button1" runat="server" Text="Aceptar" CssClass="Apariencia" OnClientClick="$find('popup3').hidePopup(); return false;" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>

                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label5" runat="server" Text="Nombre:" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                        </td>
                                        <td bgcolor="#EEEEEE">
                                            <asp:TextBox ID="TxbNombreRecurso" runat="server" Width="300px" Font-Names="Calibri" Font-Size="Small" MaxLength="50"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequFiValRecurso" runat="server" ControlToValidate="TxbNombreRecurso"
                                                InitialValue="" ForeColor="Red" ValidationGroup="validarCamposRecursos">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr align="left">
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label6" runat="server" Text="Correo electrónico:" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                        </td>
                                        <td bgcolor="#EEEEEE">
                                            <asp:TextBox ID="TxbCorreo" runat="server" Width="300px" Font-Names="Calibri" Font-Size="Small" MaxLength="50"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequFiValCorreo" runat="server" ControlToValidate="TxbCorreo"
                                                InitialValue="" ForeColor="Red" ValidationGroup="validarCamposRecursos">*</asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="validateEmail" runat="server" ErrorMessage="Correo inválido"
                                                Font-Names="Calibri" Font-Size="Small" ForeColor="Red" ControlToValidate="TxbCorreo" ValidationGroup="validarCamposRecursos"
                                                ValidationExpression="^(\s*;?\s*[0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})+\s*$" />
                                        </td>
                                    </tr>
                                    <tr align="left">
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label8" runat="server" Text="Estado:" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                        </td>
                                        <td bgcolor="#EEEEEE">
                                            <asp:DropDownList ID="DdlEstadoRecurso" runat="server" Font-Names="Calibri" Font-Size="Small">
                                                <asp:ListItem Value="0" Text="Inactivo"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="Activo" Selected="True"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <%--<tr align="left">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label5" runat="server" Text="Fecha:" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                            <td bgcolor="#EEEEEE">
                                <asp:TextBox ID="TxbFecha" runat="server" Width="150px" Font-Names="Calibri" Font-Size="Small" MaxLength="50" Enabled="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr align="left">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label6" runat="server" Text="Usuario:" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                            <td bgcolor="#EEEEEE">
                                <asp:TextBox ID="TxbUsuario" runat="server" Width="150px" Font-Names="Calibri" Font-Size="Small" MaxLength="50" Enabled="false"></asp:TextBox>
                            </td>
                        </tr>--%>
                                    <tr align="center">
                                        <td colspan="2">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:ImageButton ID="ImageButton5" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                            ToolTip="Guardar" Visible="false" ValidationGroup="validarCamposRecursos" OnClick="ImageButton5_Click" />
                                                    </td>
                                                    <td>
                                                        <asp:ImageButton ID="ImageButton6" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                            ToolTip="Guardar" Visible="false" ValidationGroup="validarCamposRecursos" OnClick="ImageButton6_Click" />
                                                    </td>
                                                    <td>
                                                        <asp:ImageButton ID="ImageButton7" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                                            ToolTip="Cancelar" OnClick="ImageButton7_Click" />
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
        <asp:ModalPopupExtender ID="mpeMsgBoxOkNo" runat="server" TargetControlID="btndummyOkNo"
            PopupControlID="pnlMsgBoxOkNo" OkControlID="btnCancelarOkNo" BackgroundCssClass="FondoAplicacion"
            Enabled="True" DropShadow="true">
        </asp:ModalPopupExtender>
        <asp:Button ID="btndummyOkNo" runat="server" Text="Button" Style="display: none" />
        <asp:Label ID="lbldummyOkNo" runat="server" Text="" Visible="False"></asp:Label>
        <asp:Panel ID="pnlMsgBoxOkNo" runat="server" Width="400px" Style="display: none;"
            BorderColor="#575757" BackColor="#FFFFFF" BorderStyle="Solid">
            <table width="100%">
                <tr class="topHandle" style="background-color: #5D7B9D">
                    <td colspan="2" align="center" runat="server" id="tdCaptionOkNo">&nbsp;
                        <asp:Label ID="lblCaptionOkNo" runat="server" Text="Atención" Font-Names="Calibri"
                            Font-Size="Small"></asp:Label><br />
                    </td>
                </tr>
                <tr>
                    <td style="width: 60px" valign="middle" align="center">
                        <asp:Image ID="imgInfoOkNo" runat="server" ImageUrl="~/Imagenes/Icons/icontexto-webdev-about.png" />
                    </td>
                    <td valign="middle" align="left">
                        <asp:Label ID="lblMsgBoxOkNo" runat="server" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="right" colspan="2">
                        <asp:Button ID="btnAceptarOkNo" runat="server" Text="Ok" Font-Names="Calibri" Font-Size="Small"
                            OnClick="btnAceptarOkNo_Click" />
                        <asp:Button ID="btnCancelarOkNo" runat="server" Text="Cancelar" Font-Names="Calibri"
                            Font-Size="Small" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>
