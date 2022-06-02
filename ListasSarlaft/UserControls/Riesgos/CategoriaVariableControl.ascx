<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CategoriaVariableControl.ascx.cs" Inherits="ListasSarlaft.UserControls.Riesgos.CategoriaVariableControl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Src="~/UserControls/Sitio/OkMessageBox.ascx" TagPrefix="uc" TagName="OkMessageBox" %>
<link href="../../../Styles/AdminSitio.css" rel="stylesheet" type="text/css" />
<style type="text/css">
    div.ajax__calendar_days table tr td {
        padding-right: 0px;
    }

    div.ajax__calendar_body {
        width: 225px;
    }

    div.ajax__calendar_container {
        width: 225px;
    }
    .no-visible {
        display:none
    }
</style>
<asp:UpdatePanel ID="CVCbody" runat="server">
    <ContentTemplate>
        <uc:OkMessageBox ID="omb" runat="server" />
        <asp:Panel ID="pnlMsgBox" runat="server" CssClass="modalPopup" Style="display: none;">
            <table class="Tablewidth">
                <tr class="topHandle">
                    <td class="centertdtr" colspan="2" runat="server" id="tdCaption">
                        <asp:Label ID="lblCaption" runat="server" Text="Atención" Font-Bold="False" Font-Names="Tahoma" Font-Size="11px"></asp:Label>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td style="width: 60px" class="centerMiddle">
                        <asp:Image ID="imgInfo" runat="server" ImageUrl="~/Imagenes/Icons/icontexto-webdev-about.png" />
                    </td>
                    <td class="LeftMiddle">
                        <asp:Label ID="lblMsgBox" runat="server" Font-Bold="False" Font-Names="Tahoma" Font-Size="11px"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="righttdtr" colspan="2" align="center">
                        <asp:Button ID="btnModificarEstado" runat="server" Text="Ok" OnClick="btnModificarEstado_Click" />
                        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" OnClientClick="$find('mypopup').hide(); return false;" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:ModalPopupExtender ID="mpeMsgBox" runat="server" PopupControlID="pnlMsgBox" BehaviorID="mypopup"
            Enabled="True" TargetControlID="btndummy" BackgroundCssClass="modalBackground" DropShadow="true">
        </asp:ModalPopupExtender>
        <asp:Button ID="btndummy" runat="server" Text="Button" Style="display: none" />
        <asp:Label ID="lblOk" runat="server" Text="Label" Visible="False"></asp:Label>
        <asp:Label ID="lblCancelar" runat="server" Text="Label" Visible="False"></asp:Label>
        <div class="TituloLabel" id="HeadCVC" runat="server">
            <asp:Label ID="Ltitulo" runat="server" ForeColor="White" Text="Categorías de las Variables de Calificación Controles" Font-Bold="False"
                Font-Names="Calibri" Font-Size="Large"></asp:Label>
        </div>
        <div id="BodyGridCVC" class="ColumnStyle" runat="server">
            <table class="tabla" align="center" width="100%">
                <tr align="center">
                    <td>
                        <asp:GridView ID="GVcategoriasVariablesControl" runat="server" CellPadding="4"
                            ForeColor="#333333" AllowPaging="False" AllowSorting="True" AutoGenerateColumns="False"
                            ShowHeaderWhenEmpty="True" DataKeyNames="strUsuario,dtFechaRegistro" 
                            HeaderStyle-CssClass="gridViewHeader" BorderStyle="Solid" GridLines="Vertical"
                            CssClass="Apariencia" Font-Bold="False" OnRowCommand="GVcategoriasVariablesControl_RowCommand" OnPreRender="GVcategoriasVariablesControl_PreRender" OnPageIndexChanging="GVcategoriasVariablesControl_PageIndexChanging">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:BoundField DataField="intIdCategoriaVariableControl" HeaderText="Código" SortExpression="intId" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="no-visible" HeaderStyle-CssClass="no-visible"/>
                                <asp:TemplateField HeaderText="Descripción Categoría" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 150px">
                                            <asp:Label ID="strDescripcionCategoria" runat="server" Text='<% # Bind("strDescripcionCategoria")%>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                    <HeaderStyle Wrap="false" HorizontalAlign="center" />
                                    <ItemStyle Wrap="false" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="intPesoCategoria" HeaderText="Calificación Categoría" ReadOnly="True" SortExpression="intPesoCategoria" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="dtFechaRegistro" HeaderText="fechaRegistro" ReadOnly="True" Visible="false" SortExpression="dtFechaRegistro" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="intIdUsuario" HeaderText="IdUsuario" ReadOnly="True" Visible="false" SortExpression="intIdUsuario" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="strUsuario" HeaderText="Usuario" ReadOnly="True" Visible="false" SortExpression="strUsuario" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="booActivo" HeaderText="Activo" ReadOnly="True" Visible="false" SortExpression="booActivo" ItemStyle-HorizontalAlign="Center" />
                                <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Icons/select.png" Text="Seleccionar" HeaderText="Seleccionar" CommandName="Seleccionar" ItemStyle-HorizontalAlign="Center" />
                                <asp:TemplateField HeaderText="(In)Activar" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 150px" align="center">
                                            <asp:Label ID="booActivo" runat="server" Visible="false" Text='<% # Bind("booActivo")%>'></asp:Label>
                                            <asp:ImageButton runat="server" ID="ImgBtnInact" ImageUrl="~/Imagenes/Icons/switch-on-icon.png" CommandName="Activar" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                        </div>
                                    </ItemTemplate>
                                    <HeaderStyle Wrap="false" HorizontalAlign="center" />
                                    <ItemStyle Wrap="false" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblIdVariable" Text='<%# String.Format("{0}", Eval("IdVariable")).Trim()%>' Visible="false"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EditRowStyle BackColor="#999999" />
                            <FooterStyle BackColor="White" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:ImageButton ID="btnInsertarNuevo" runat="server" CausesValidation="False" CommandName="Insert"
                            ImageUrl="~/Imagenes/Icons/Add.png" Text="Insert" OnClick="btnInsertarNuevo_Click" ToolTip="Insertar" />
                    </td>
                </tr>
            </table>
        </div>
        <div id="BodyFormCVC" class="ColumnStyle" runat="server" visible="false">
            <div id="form" class="TableContains">
                <table class="tabla" align="center" width="80%">
                    <tr class="no-visible">
                        <td class="RowsText">
                            <asp:Label ID="Lcodigo" runat="server" Text="Código:" CssClass="Apariencia"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtId" runat="server" Enabled="False"
                                CssClass="Apariencia" Width="300px"></asp:TextBox>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="RowsText">
                            <asp:Label ID="LDescripcionCategoria" runat="server" Text="Descripción Categoría:" CssClass="Apariencia"></asp:Label></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtDescripcionCategoria" CssClass="Apariencia" Width="300px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="rfvDescripcionCategoria" runat="server" ControlToValidate="txtDescripcionCategoria"
                                ErrorMessage="Debe ingresar la Descripción de la Categoría." ToolTip="Debe ingresar la Descripción de la Categoría."
                                ValidationGroup="vgCategoriaVariable" CausesValidation="true" ForeColor="Red">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="RowsText">
                            <asp:Label ID="lblPesoCategoria" runat="server" Text="Calificación Categoría:" CssClass="Apariencia"></asp:Label></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtPesoCategoria" CssClass="Apariencia" Width="300px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="rfvPesoCategoria" runat="server" ControlToValidate="txtPesoCategoria"
                                ErrorMessage="Debe ingresar el Peso de la Categoría." ToolTip="Debe ingresar el Peso de la Categoría."
                                ValidationGroup="vgCategoriaVariable" ForeColor="Red" CausesValidation="true">*</asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revPesoCategoria" runat="server" ControlToValidate="txtPesoCategoria"
                                ValidationExpression="^[0-9]{0,10}$" ForeColor="Red" ErrorMessage="Solo números Enteros" ToolTip="Solo números Enteros"
                                ValidationGroup="vgCategoriaVariable">*</asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="RowsText">
                            <asp:Label ID="Lusuario" runat="server" Text="Usuario Creación:" CssClass="Apariencia" Width="300px"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="tbxUsuarioCreacion" runat="server" Width="300px" CssClass="Apariencia"
                                Enabled="False"></asp:TextBox>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="RowsText">
                            <asp:Label ID="LfechaCreacion" runat="server" Text="Fecha de Creación:" CssClass="Apariencia"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtFecha" runat="server" Width="300px" CssClass="Apariencia"
                                Enabled="False"></asp:TextBox>
                        </td>
                        <td></td>
                    </tr>
                    <tr class="RowsText">
                        <td class="Apariencia">Variable</td>
                        <td><asp:DropDownList ID="ddlVariable" runat="server" CssClass="Apariencia" Width="300px">
                            </asp:DropDownList></td>
                        <td>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlVariable" InitialValue="0" Text="*" Display="Dynamic" ForeColor="Red" ValidationGroup="vgCategoriaVariable">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:ImageButton ID="IBinsertGVC" runat="server" CausesValidation="true" CommandName="Insert"
                                ImageUrl="~/Imagenes/Icons/guardar.png" Text="Insert" ValidationGroup="vgCategoriaVariable" ToolTip="Insertar" Visible="false" OnClick="IBinsertGVC_Click" />
                            <asp:ImageButton ID="IBupdateGVC" runat="server" CausesValidation="true" CommandName="Update"
                                ImageUrl="~/Imagenes/Icons/guardar.png" Text="Update" ValidationGroup="vgCategoriaVariable" ToolTip="Actualizar" Visible="false" OnClick="IBupdateGVC_Click" />
                            <asp:ImageButton ID="btnImgCancelar" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                OnClick="btnImgCancelar_Click" ToolTip="Cancelar" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
