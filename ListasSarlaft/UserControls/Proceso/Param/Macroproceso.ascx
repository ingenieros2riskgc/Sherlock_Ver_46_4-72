<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Macroproceso.ascx.cs" Inherits="ListasSarlaft.UserControls.Proceso.Macroproceso" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControls/Sitio/OkMessageBox.ascx" TagPrefix="uc" TagName="OkMessageBox" %>
<style type="text/css">
    .ajax__html_editor_extender_texteditor
    {
        background-color: White;
        font-size: small;
        font-weight: lighter;
        font-family: Calibri;
        font-style: normal;
    }

    .gridViewHeader a:link
    {
        text-decoration: none;
    }

    .style1
    {
        width: 100%;
    }

    .popup
    {
        border: Silver 1px solid;
        color: #060F40;
        background: #ffffff;
    }

    .auto-style1
    {
        width: 120px;
    }

</style>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <uc:OkMessageBox ID="omb" runat="server" />
        <asp:Panel ID="pnlMsgBox" runat="server" Width="400px" CssClass="modalPopup" Style="display: none;">
            <table width="100%">
                <tr class="topHandle">
                    <td colspan="2" align="center" runat="server" id="tdCaption">
                        <asp:Label ID="lblCaption" runat="server" Text="Atención"
                            Font-Bold="False" Font-Names="Tahoma" Font-Size="11px"></asp:Label><br />
                    </td>
                </tr>
                <tr>
                    <td style="width: 60px" valign="middle" align="center">
                        <asp:Image ID="imgInfo" runat="server"
                            ImageUrl="~/Imagenes/Icons/icontexto-webdev-about.png" />
                    </td>
                    <td valign="middle" align="left">
                        <asp:Label ID="lblMsgBox" runat="server" Font-Bold="False" Font-Names="Tahoma"
                            Font-Size="11px"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="right" colspan="2">
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
        <table align="center" width="80%">
            <tr align="center" bgcolor="#333399">
                <td>
                    <asp:Label ID="Label13" runat="server" ForeColor="White" Text="Macroproceso" Font-Bold="False"
                        Font-Names="Calibri" Font-Size="Large"></asp:Label>
                </td>
            </tr>
            <tr align="center" bgcolor="#EEEEEE" id="filaGrid" runat="server" visible="true">
                <td bgcolor="#EEEEEE">
                    <br />
                    <table>
                        <tr>
                            <td>
                                <asp:GridView ID="GridView1" runat="server" CellPadding="4"
                                    ForeColor="#333333" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
                                    DataKeyNames="intId,strDescripcion,strObjetivo,intCargoResponsable"
                                    ShowHeaderWhenEmpty="True" OnRowCommand="GridView1_RowCommand"
                                    HeaderStyle-CssClass="gridViewHeader" BorderStyle="Solid" GridLines="Vertical"
                                    CssClass="Apariencia" Font-Bold="False" OnPageIndexChanging="GridView1_PageIndexChanging1" PageSize="7">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:BoundField DataField="intId" HeaderText="Código" SortExpression="intId" ItemStyle-CssClass="no-visible" HeaderStyle-CssClass="no-visible" />
                                        <asp:TemplateField HeaderText="Nombre" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="center">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 100px">
                                                    <asp:Label ID="Nombre" runat="server" Text='<% # Bind("strNombreMacroproceso")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" Width="100" />
                                            <ItemStyle Wrap="false" Width="100" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="intIdCadenaDeValor" HeaderText="IdCadenaDeValor" SortExpression="intIdCadenaDeValor" Visible="False" />
                                        <asp:TemplateField HeaderText="Cadena de Valor" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="center">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 100px">
                                                    <asp:Label ID="CadenaValor" runat="server" Text='<% # Bind("strNombreCadenaValor")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" Width="100" />
                                            <ItemStyle Wrap="false" Width="100" />
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="strDescripcion" HeaderText="Descripción" SortExpression="strDescripcion" HtmlEncode="False" HtmlEncodeFormatString="True" Visible="False" />
                                        <asp:BoundField DataField="strObjetivo" HeaderText="Objetivo" SortExpression="strObjetivo" HtmlEncode="False" HtmlEncodeFormatString="True" Visible="False" />
                                        <asp:TemplateField HeaderText="Estado">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chbActivo" runat="server" Checked='<% # Bind("booEstado")%>' Enabled="false" />
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="intIdUsuario" HeaderText="IdUsuario" SortExpression="intIdUsuario" Visible="False" />
                                        <asp:BoundField DataField="strNombreUsuario" HeaderText="Usuario Creación" SortExpression="strNombreUsuario" Visible="False" />
                                        <asp:BoundField DataField="intCargoResponsable" HeaderText="Id CargoResponsable" SortExpression="intCargoResponsable" Visible="False" />
                                        <asp:BoundField DataField="strNombreResponsable" HeaderText="Cargo Responsable" SortExpression="strNombreResponsable" Visible="False" />
                                        <asp:BoundField DataField="dtFechaRegistro" HeaderText="Fecha de Creación" SortExpression="dtFechaRegistro" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                        <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Icons/edit.png" Text="Modificar" HeaderText="Modificar" CommandName="Modificar" ItemStyle-HorizontalAlign="Center" />
                                        <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Icons/switch-on-icon.png" Text="(In)Activar" HeaderText="(In)Activar" CommandName="Activar" ItemStyle-HorizontalAlign="Center" />
                                        <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Icons/select.png" Text="Ver" HeaderText="Ver" CommandName="Ver" ItemStyle-HorizontalAlign="Center" />
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
                            <td align="right">
                                <asp:ImageButton ID="imgBtnInsertar" runat="server" CausesValidation="False" CommandName="Insert"
                                    ImageUrl="~/Imagenes/Icons/Add.png" Text="Insert"
                                    OnClick="imgBtnInsertar_Click" ToolTip="Insertar" />
                            </td>
                        </tr>
                    </table>
                    <br />
                </td>
            </tr>
            <tr align="left" id="filaDetalle" runat="server" visible="false">
                <td bgcolor="#EEEEEE">
                    <table class="tabla" width="100%">
                        <tr>
                            <td align="left" bgcolor="#BBBBBB">
                                <asp:Label ID="Label1" runat="server" Text="Código:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtId" runat="server" Enabled="False" Width="70px"
                                    CssClass="Apariencia"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" bgcolor="#BBBBBB">
                                <asp:Label ID="Label8" runat="server" Text="Cadena de Valor:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlCadenaValor" runat="server" Width="300px" CssClass="Apariencia"
                                    DataTextField="NombreCadenaValor" DataValueField="IdCadenaValor">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvCadenaValor" runat="server" ControlToValidate="ddlCadenaValor"
                                    ErrorMessage="Debe ingresar la Cadena de valor." ToolTip="Debe ingresar la Cadena de valor."
                                    ValidationGroup="iMacroProceso" ForeColor="Red" InitialValue="0">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" bgcolor="#BBBBBB">
                                <asp:Label ID="Label6" runat="server" Text="Nombre:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtNombre" runat="server" Width="300px" CssClass="Apariencia"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvNombre" runat="server" ControlToValidate="txtNombre"
                                    ErrorMessage="Debe ingresar el Nombre." ToolTip="Debe ingresar el Nombre."
                                    ValidationGroup="iMacroProceso" ForeColor="Red">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" bgcolor="#BBBBBB">
                                <asp:Label ID="Label5" runat="server" Text="Descripción:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TxtDescripcion" runat="server" Width="800px" CssClass="Apariencia"
                                    Columns="100" Rows="6" TextMode="MultiLine" Font-Size="10pt" Font-Bold="False" Height="42px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvDescripcion" runat="server" ControlToValidate="TxtDescripcion"
                                    ErrorMessage="Debe ingresar la Descripción." ToolTip="Debe ingresar la Descripción."
                                    ValidationGroup="iMacroProceso" ForeColor="Red">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" bgcolor="#BBBBBB">
                                <asp:Label ID="Label2" runat="server" Text="Objetivo:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtObjetivo" runat="server" Width="800px" CssClass="Apariencia" Height="42px"
                                    Columns="100" Rows="6" TextMode="MultiLine" Font-Size="10pt" Font-Bold="False"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvObjetivo" runat="server" ControlToValidate="txtObjetivo"
                                    ErrorMessage="Debe ingresar el Objetivo." ToolTip="Debe ingresar el Objetivo."
                                    ValidationGroup="iMacroProceso" ForeColor="Red">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" bgcolor="#BBBBBB">
                                <asp:Label ID="Label3" runat="server" Text="Responsable:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="tbxResponsable" runat="server" Width="212px" CssClass="Apariencia"></asp:TextBox>
                                <asp:Label ID="lblIdDependencia4" runat="server" Visible="False" Font-Names="Calibri"
                                    Font-Size="Small"></asp:Label>
                                <asp:ImageButton ID="imgDependencia4" runat="server" ImageUrl="~/Imagenes/Icons/Organization-Chart.png"
                                    OnClientClick="return false;" />
                                <asp:PopupControlExtender ID="popupDependencia4" runat="server" TargetControlID="imgDependencia4" BehaviorID="popup4"
                                    PopupControlID="pnlDependencia4" OffsetY="-200">
                                </asp:PopupControlExtender>
                                <asp:Panel ID="pnlDependencia4" runat="server" CssClass="popup" Width="400px" Style="display: none">
                                    <table width="100%" border="1" cellspacing="0" cellpadding="2" bordercolor="White">
                                        <tr align="right" bgcolor="#5D7B9D">
                                            <td>
                                                <asp:ImageButton ID="btnClosepp4" runat="server" ImageUrl="~/Imagenes/Icons/dialog-close2.png"
                                                    OnClientClick="$find('popup4').hidePopup(); return false;" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TreeView ID="TreeView4" ExpandDepth="3" runat="server" Font-Names="Calibri"
                                                    Font-Size="Small" LineImagesFolder="~/TreeLineImages" ForeColor="Black" ShowLines="True"
                                                    AutoGenerateDataBindings="False" OnSelectedNodeChanged="TreeView4_SelectedNodeChanged">
                                                    <SelectedNodeStyle BackColor="Silver" BorderColor="#66CCFF" BorderStyle="Inset" />
                                                </asp:TreeView>
                                            </td>
                                        </tr>
                                        <tr align="center">
                                            <td>
                                                <asp:Button ID="BtnOk4" runat="server" Text="Aceptar" CssClass="Apariencia" OnClientClick="$find('popup4').hidePopup(); return false;" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:RequiredFieldValidator ID="rfvCargoResponsable" runat="server" ControlToValidate="tbxResponsable"
                                    ErrorMessage="Debe ingresar el Cargo Responsable." ToolTip="Debe ingresar el Cargo Responsable."
                                    ValidationGroup="iMacroProceso" ForeColor="Red">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" bgcolor="#BBBBBB">
                                <asp:Label ID="Label7" runat="server" Text="Estado:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:CheckBox ID="ChBEstado" runat="server" Checked="true" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" bgcolor="#BBBBBB">
                                <asp:Label ID="Label9" runat="server" Text="Usuario Creación:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="tbxUsuarioCreacion" runat="server" Width="100px" CssClass="Apariencia"
                                    Enabled="False"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" bgcolor="#BBBBBB">
                                <asp:Label ID="Label4" runat="server" Text="Fecha de Creación:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtFecha" runat="server" Width="100px" CssClass="Apariencia"
                                    Enabled="False"></asp:TextBox>
                            </td>
                        </tr>
                        <tr id="FilaBotones" align="center">
                            <td colspan="2">
                                <table class="tablaSinBordes">
                                    <tr>
                                        <td>
                                            <asp:ImageButton ID="btnImgInsertar" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                Visible="False" OnClick="btnImgInsertar_Click" ToolTip="Guardar" ValidationGroup="iMacroProceso" />
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="btnImgActualizar" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                Style="text-align: right" OnClick="btnImgActualizar_Click" ValidationGroup="iMacroProceso"
                                                ToolTip="Guardar" />
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="btnImgCancelar" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                                OnClick="btnImgCancelar_Click" ToolTip="Cancelar" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr align="left" id="filaConsulta" runat="server" visible="false">
                <td>
                    <table id="TablaConsulta" runat="server" align="center" width="80%">
                        <tr id="Titulo" align="center" bgcolor="#333399" runat="server">
                            <td>
                                <asp:Label ID="Label10" runat="server" ForeColor="White" Text="Consultar" Font-Bold="False"
                                    Font-Names="Calibri" Font-Size="Large"></asp:Label>
                            </td>
                        </tr>
                        <tr align="left" id="Cabecera" runat="server">
                            <td bgcolor="#EEEEEE">
                                <table class="tabla" runat="server">
                                    <tr>
                                        <td align="left" bgcolor="#BBBBBB">
                                            <asp:Label ID="lblTitCV" runat="server" Text="Cadena Valor:" CssClass="Apariencia"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="tbxCadenaValor" runat="server" Width="200px" CssClass="Apariencia" Enabled="False"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" bgcolor="#BBBBBB">
                                            <asp:Label ID="Label11" runat="server" Text="Macroproceso:" CssClass="Apariencia"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="tbxMacroproceso" runat="server" Width="200px" CssClass="Apariencia" Enabled="False"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr id="Proceso" runat="server">
                                        <td align="left" bgcolor="#BBBBBB">
                                            <asp:Label ID="lblProceso" runat="server" Text="Proceso:" CssClass="Apariencia"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="tbxProceso" runat="server" Width="200px" CssClass="Apariencia" Enabled="False"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr id="SubProceso" runat="server">
                                        <td align="left" bgcolor="#BBBBBB">
                                            <asp:Label ID="lblSubproceso" runat="server" Text="Sub-Proceso:" CssClass="Apariencia"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="tbxSubProceso" runat="server" Width="200px" CssClass="Apariencia" Enabled="False"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" bgcolor="#BBBBBB">
                                            <asp:Label ID="Label15" runat="server" Text="Descripción:" CssClass="Apariencia"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="tbxDescripcion" runat="server" Width="200px" CssClass="Apariencia" Enabled="False"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" bgcolor="#BBBBBB">
                                            <asp:Label ID="Label16" runat="server" Text="Objetivo:" CssClass="Apariencia"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="tbxObjetivo" runat="server" Width="200px" CssClass="Apariencia" Enabled="False"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" bgcolor="#BBBBBB">
                                            <asp:Label ID="Label17" runat="server" Text="Cargo:" CssClass="Apariencia"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="tbxCargo" runat="server" Width="200px" CssClass="Apariencia" Enabled="False"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr align="left" id="Detalles" runat="server">
                            <td bgcolor="#EEEEEE">
                                <table class="tabla">
                                    <tr>
                                        <td>
                                            <asp:TabContainer ID="TabDetalles" runat="server" ActiveTabIndex="0" Font-Names="Calibri"
                                                Font-Size="Small" Width="800px">

                                                <asp:TabPanel ID="tpEntrada" runat="server" HeaderText="Entradas" Font-Names="Calibri" Font-Size="Small">
                                                    <HeaderTemplate>Entradas</HeaderTemplate>
                                                    <ContentTemplate>
                                                        <table>
                                                            <tr align="center">
                                                                <td>
                                                                    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" BorderStyle="Solid"
                                                                        CellPadding="4" Font-Names="Calibri" Font-Size="Small" ForeColor="#333333" GridLines="Vertical"
                                                                        HorizontalAlign="Center" ShowHeaderWhenEmpty="True" AllowPaging="True" OnPageIndexChanging="GridView2_PageIndexChanging">
                                                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775"></AlternatingRowStyle>
                                                                        <Columns>
                                                                            <asp:BoundField DataField="intId" HeaderText="Código" ReadOnly="True" SortExpression="intId" >
                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                            </asp:BoundField>
                                                                            <asp:TemplateField HeaderText="Descripción">
                                                                                <ItemTemplate>
                                                                                    <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 300px">
                                                                                        <asp:Label ID="Descripcion" runat="server" Text='<% # Bind("strDescripcion")%>'></asp:Label>
                                                                                    </div>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle Wrap="False" Width="300px" HorizontalAlign="Center" />
                                                                                <ItemStyle Wrap="False" Width="300px" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Estado" Visible="False">
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="chbActivo" runat="server" Checked='<% # Bind("booEstado")%>' Enabled="false" />
                                                                                </ItemTemplate>
                                                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                            </asp:TemplateField>
                                                                            <asp:BoundField DataField="strProveedor" HeaderText="Proveedor" SortExpression="strProveedor" />
                                                                            <asp:BoundField DataField="intIdUsuario" HeaderText="Región" SortExpression="intIdUsuario"
                                                                                Visible="False" />
                                                                            <asp:BoundField DataField="strNombreUsuario" HeaderText="Usuario Creación" SortExpression="strNombreUsuario"
                                                                                Visible="False" />
                                                                            <asp:BoundField DataField="dtFechaRegistro" HeaderText="Fecha de Creación" SortExpression="dtFechaRegistro" >
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                            </asp:BoundField>
                                                                        </Columns>
                                                                        <EditRowStyle BackColor="#999999" />
                                                                        <FooterStyle BackColor="White" Font-Bold="True" ForeColor="White"></FooterStyle>
                                                                        <HeaderStyle BackColor="#5D7B9D" CssClass="gridViewHeader" Font-Bold="True" ForeColor="White"></HeaderStyle>
                                                                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center"></PagerStyle>
                                                                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Left"></RowStyle>
                                                                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333"></SelectedRowStyle>
                                                                        <SortedAscendingCellStyle BackColor="#E9E7E2"></SortedAscendingCellStyle>
                                                                        <SortedAscendingHeaderStyle BackColor="#506C8C"></SortedAscendingHeaderStyle>
                                                                        <SortedDescendingCellStyle BackColor="#FFFDF8"></SortedDescendingCellStyle>
                                                                        <SortedDescendingHeaderStyle BackColor="#6F8DAE"></SortedDescendingHeaderStyle>
                                                                    </asp:GridView>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ContentTemplate>
                                                </asp:TabPanel>

                                                <asp:TabPanel ID="tpActividades" runat="server" HeaderText="Actividades" Font-Names="Calibri" Font-Size="Small">
                                                    <HeaderTemplate>Actividades</HeaderTemplate>
                                                    <ContentTemplate>
                                                        <table>
                                                            <tr align="center">
                                                                <td>
                                                                    <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" BorderStyle="Solid"
                                                                        CellPadding="4" Font-Names="Calibri" Font-Size="Small" ForeColor="#333333" GridLines="Vertical"
                                                                        HorizontalAlign="Center" ShowHeaderWhenEmpty="True">
                                                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775"></AlternatingRowStyle>
                                                                        <Columns>
                                                                            <asp:BoundField DataField="intId" HeaderText="Código" ReadOnly="True" SortExpression="intId" ItemStyle-HorizontalAlign="Center" />
                                                                            <asp:TemplateField HeaderText="Descripción">
                                                                                <ItemTemplate>
                                                                                    <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 300px">
                                                                                        <asp:Label ID="Descripcion" runat="server" Text='<% # Bind("strDescripcion")%>'></asp:Label>
                                                                                    </div>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle Wrap="false" Width="300" HorizontalAlign="center" />
                                                                                <ItemStyle Wrap="false" Width="300" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Estado" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="chbActivo" runat="server" Checked='<% # Bind("booEstado")%>' Enabled="false" />
                                                                                </ItemTemplate>
                                                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                            </asp:TemplateField>
                                                                            <asp:BoundField DataField="intCargoResponsable" HeaderText="IdCargo" SortExpression="intCargoResponsable"
                                                                                Visible="False" />
                                                                            <asp:BoundField DataField="strNombreCargoResponsable" HeaderText="Cargo" SortExpression="strNombreCargoResponsable" />
                                                                            <asp:BoundField DataField="intIdUsuario" HeaderText="Región" SortExpression="intIdUsuario" Visible="False" />
                                                                            <asp:BoundField DataField="strNombreUsuario" HeaderText="Usuario Creación" SortExpression="strNombreUsuario" Visible="False" />
                                                                            <asp:BoundField DataField="dtFechaRegistro" HeaderText="Fecha de Creación" SortExpression="dtFechaRegistro" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                                                        </Columns>
                                                                        <EditRowStyle BackColor="#999999" />
                                                                        <FooterStyle BackColor="White" Font-Bold="True" ForeColor="White"></FooterStyle>
                                                                        <HeaderStyle BackColor="#5D7B9D" CssClass="gridViewHeader" Font-Bold="True" ForeColor="White"></HeaderStyle>
                                                                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center"></PagerStyle>
                                                                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Left"></RowStyle>
                                                                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333"></SelectedRowStyle>
                                                                        <SortedAscendingCellStyle BackColor="#E9E7E2"></SortedAscendingCellStyle>
                                                                        <SortedAscendingHeaderStyle BackColor="#506C8C"></SortedAscendingHeaderStyle>
                                                                        <SortedDescendingCellStyle BackColor="#FFFDF8"></SortedDescendingCellStyle>
                                                                        <SortedDescendingHeaderStyle BackColor="#6F8DAE"></SortedDescendingHeaderStyle>
                                                                    </asp:GridView>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ContentTemplate>
                                                </asp:TabPanel>

                                                <asp:TabPanel ID="tpSalidas" runat="server" HeaderText="Salidas" Font-Names="Calibri" Font-Size="Small">
                                                    <HeaderTemplate>
                                                        Salidas
                                                    </HeaderTemplate>
                                                    <ContentTemplate>
                                                        <table>
                                                            <tr align="center">
                                                                <td>
                                                                    <asp:GridView ID="GridView4" runat="server" AutoGenerateColumns="False" BorderStyle="Solid"
                                                                        CellPadding="4" Font-Names="Calibri" Font-Size="Small" ForeColor="#333333" GridLines="Vertical"
                                                                        HorizontalAlign="Center" ShowHeaderWhenEmpty="True">
                                                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775"></AlternatingRowStyle>
                                                                        <Columns>
                                                                            <asp:BoundField DataField="intId" HeaderText="Código" ReadOnly="True" SortExpression="intId" ItemStyle-HorizontalAlign="Center" />
                                                                            <asp:TemplateField HeaderText="Descripción">
                                                                                <ItemTemplate>
                                                                                    <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 300px">
                                                                                        <asp:Label ID="Descripcion" runat="server" Text='<% # Bind("strDescripcion")%>'></asp:Label>
                                                                                    </div>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle Wrap="false" Width="300" HorizontalAlign="center" />
                                                                                <ItemStyle Wrap="false" Width="300" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Estado" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="chbActivo" runat="server" Checked='<% # Bind("booEstado")%>' Enabled="false" />
                                                                                </ItemTemplate>
                                                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                            </asp:TemplateField>
                                                                            <asp:BoundField DataField="strCliente" HeaderText="Cliente" SortExpression="strCliente" />
                                                                            <asp:BoundField DataField="intIdUsuario" HeaderText="Región" SortExpression="intIdUsuario" Visible="False" />
                                                                            <asp:BoundField DataField="strNombreUsuario" HeaderText="Usuario Creación" SortExpression="strNombreUsuario" Visible="False" />
                                                                            <asp:BoundField DataField="dtFechaRegistro" HeaderText="Fecha de Creación" SortExpression="dtFechaRegistro" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                                                        </Columns>
                                                                        <EditRowStyle BackColor="#999999" />
                                                                        <FooterStyle BackColor="White" Font-Bold="True" ForeColor="White"></FooterStyle>
                                                                        <HeaderStyle BackColor="#5D7B9D" CssClass="gridViewHeader" Font-Bold="True" ForeColor="White"></HeaderStyle>
                                                                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center"></PagerStyle>
                                                                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Left"></RowStyle>
                                                                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333"></SelectedRowStyle>
                                                                        <SortedAscendingCellStyle BackColor="#E9E7E2"></SortedAscendingCellStyle>
                                                                        <SortedAscendingHeaderStyle BackColor="#506C8C"></SortedAscendingHeaderStyle>
                                                                        <SortedDescendingCellStyle BackColor="#FFFDF8"></SortedDescendingCellStyle>
                                                                        <SortedDescendingHeaderStyle BackColor="#6F8DAE"></SortedDescendingHeaderStyle>
                                                                    </asp:GridView>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ContentTemplate>
                                                </asp:TabPanel>

                                                <asp:TabPanel ID="tpIndicadores" runat="server" HeaderText="Indicadores" Font-Names="Calibri" Font-Size="Small">
                                                    <HeaderTemplate>
                                                        Indicadores
                                                    </HeaderTemplate>
                                                    <ContentTemplate>
                                                        <table>
                                                            <tr align="center">
                                                                <td>
                                                                    <asp:GridView ID="GridView5" runat="server" AutoGenerateColumns="False" BorderStyle="Solid"
                                                                        CellPadding="4" Font-Names="Calibri" Font-Size="Small" ForeColor="#333333" GridLines="Vertical"
                                                                        HorizontalAlign="Center" ShowHeaderWhenEmpty="True">
                                                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775"></AlternatingRowStyle>
                                                                        <Columns>
                                                                            <asp:BoundField DataField="intId" HeaderText="Código" SortExpression="intId" ItemStyle-HorizontalAlign="Center" />
                                                                            <asp:TemplateField HeaderText="Descripción" ItemStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 100px">
                                                                                        <asp:Label ID="Descripcion" runat="server" Text='<% # Bind("strDescripcion")%>'></asp:Label>
                                                                                    </div>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle Wrap="false" Width="100" HorizontalAlign="center" />
                                                                                <ItemStyle Wrap="false" Width="100" />
                                                                            </asp:TemplateField>
                                                                            <asp:BoundField DataField="intIdPeriodicidad" HeaderText="Id Periodicidad" SortExpression="intIdPeriodicidad" HtmlEncodeFormatString="True" HtmlEncode="False" Visible="False" />
                                                                            <asp:BoundField DataField="strNombrePeriodicidad" HeaderText="Periodicidad" SortExpression="strNombrePeriodicidad" HtmlEncodeFormatString="True" HtmlEncode="False" Visible="True" />
                                                                            <asp:BoundField DataField="intMeta" HeaderText="Meta" SortExpression="intMeta" HtmlEncodeFormatString="True" HtmlEncode="False" Visible="False" />
                                                                            <asp:TemplateField HeaderText="Estado" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="chbActivo" runat="server" Checked='<% # Bind("booEstado")%>' Enabled="false" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:BoundField DataField="intIdCalificacion" HeaderText="Id Calificacion" SortExpression="intIdCalificacion" Visible="False" />
                                                                            <asp:BoundField DataField="intIdObjetivoCalidad" HeaderText="Id Objetivo Calidad" SortExpression="intIdObjetivoCalidad" Visible="False" />
                                                                            <asp:BoundField DataField="strDescObjetivo" HeaderText="Desc. Objetivo" SortExpression="strDescObjetivo" Visible="False" />
                                                                            <asp:BoundField DataField="intIdProcesoIndicador" HeaderText="Id ProcesoIndicador" SortExpression="intIdProcesoIndicador" Visible="False" />
                                                                            <asp:BoundField DataField="intIdUsuario" HeaderText="IdUsuario" SortExpression="intIdUsuario" Visible="False" />
                                                                            <asp:BoundField DataField="strNombreUsuario" HeaderText="Usuario" SortExpression="strNombreUsuario" Visible="False" />
                                                                            <asp:BoundField DataField="dtFechaRegistro" HeaderText="FechaRegistro" ReadOnly="True" SortExpression="dtFechaRegistro" ItemStyle-HorizontalAlign="Center" />
                                                                        </Columns>
                                                                        <EditRowStyle BackColor="#999999" />
                                                                        <FooterStyle BackColor="White" Font-Bold="True" ForeColor="White"></FooterStyle>
                                                                        <HeaderStyle BackColor="#5D7B9D" CssClass="gridViewHeader" Font-Bold="True" ForeColor="White"></HeaderStyle>
                                                                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center"></PagerStyle>
                                                                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Left"></RowStyle>
                                                                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333"></SelectedRowStyle>
                                                                        <SortedAscendingCellStyle BackColor="#E9E7E2"></SortedAscendingCellStyle>
                                                                        <SortedAscendingHeaderStyle BackColor="#506C8C"></SortedAscendingHeaderStyle>
                                                                        <SortedDescendingCellStyle BackColor="#FFFDF8"></SortedDescendingCellStyle>
                                                                        <SortedDescendingHeaderStyle BackColor="#6F8DAE"></SortedDescendingHeaderStyle>
                                                                    </asp:GridView>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ContentTemplate>
                                                </asp:TabPanel>

                                                <asp:TabPanel ID="tpRiesgos" runat="server" HeaderText="Riesgos" Font-Names="Calibri" Font-Size="Small">
                                                    <HeaderTemplate>Riesgos</HeaderTemplate>
                                                    <ContentTemplate>
                                                        <table>
                                                            <tr align="center">
                                                                <td>
                                                                    <asp:GridView ID="GridView6" runat="server" AutoGenerateColumns="False" BorderStyle="Solid"
                                                                        CellPadding="4" Font-Names="Calibri" Font-Size="Small" ForeColor="#333333" GridLines="Vertical"
                                                                        HorizontalAlign="Center" ShowHeaderWhenEmpty="True">
                                                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775"></AlternatingRowStyle>
                                                                        <Columns>
                                                                            <asp:BoundField DataField="intIdRiesgo" HeaderText="IdRiesgo" ReadOnly="True" SortExpression="intIdRiesgo" Visible="false" />
                                                                            <asp:BoundField DataField="strCodigo" HeaderText="Código" ReadOnly="True" SortExpression="strCodigo" ItemStyle-HorizontalAlign="Center" />
                                                                            <asp:BoundField DataField="strNombreRiesgo" HeaderText="Nombre Riesgo" ReadOnly="True" SortExpression="strNombreRiesgo" ItemStyle-HorizontalAlign="Center" />
                                                                            <asp:TemplateField HeaderText="Descripción" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 300px">
                                                                                        <asp:Label ID="Descripcion" runat="server" Text='<% # Bind("strDescripcion")%>'></asp:Label>
                                                                                    </div>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle Wrap="false" Width="300" HorizontalAlign="center" />
                                                                                <ItemStyle Wrap="false" Width="300" />
                                                                            </asp:TemplateField>
                                                                            <asp:BoundField DataField="intIdRegion" HeaderText="Id Region" SortExpression="intIdRegion" Visible="false" />
                                                                            <asp:BoundField DataField="intIdPais" HeaderText="Id Pais" SortExpression="intIdPais" Visible="false" />
                                                                            <asp:BoundField DataField="intIdDepartamento" HeaderText="Id Departamento" SortExpression="intIdDepartamento" Visible="false" />
                                                                            <asp:BoundField DataField="intIdCiudad" HeaderText="Id Ciudad" SortExpression="intIdCiudad" Visible="false" />
                                                                            <asp:BoundField DataField="intIdOficinaSucursal" HeaderText="Id OficinaSucursal" SortExpression="intIdOficinaSucursal" Visible="false" />
                                                                            <asp:BoundField DataField="intIdCadenaValor" HeaderText="Id CadenaValor" SortExpression="intIdCadenaValor" Visible="false" />
                                                                            <asp:BoundField DataField="intIdMacroproceso" HeaderText="Id Macroproceso" SortExpression="intIdMacroproceso" Visible="false" />
                                                                            <asp:BoundField DataField="intIdProceso" HeaderText="Id Proceso" SortExpression="intIdProceso" Visible="false" />
                                                                            <asp:BoundField DataField="intIdSubProceso" HeaderText="Id SubProceso" SortExpression="intIdSubProceso" Visible="false" />
                                                                            <asp:BoundField DataField="intIdActividad" HeaderText="Id Actividad" SortExpression="intIdActividad" Visible="false" />
                                                                            <asp:BoundField DataField="intIdClasificacionRiesgo" HeaderText="Id ClasificacionRiesgo" SortExpression="intIdClasificacionRiesgo" Visible="false" />
                                                                            <asp:BoundField DataField="intIdClasificacionGeneralRiesgo" HeaderText="Id ClasificacionGeneralRiesgo" SortExpression="intIdClasificacionGeneralRiesgo" Visible="false" />
                                                                            <asp:BoundField DataField="intIdClasificacionParticularRiesgo" HeaderText="Id ClasificacionParticularRiesgo" SortExpression="intIdClasificacionParticularRiesgo" Visible="false" />
                                                                            <asp:BoundField DataField="intIdFactorRiesgoOperativo" HeaderText="Id FactorRiesgoOperativo" SortExpression="intIdFactorRiesgoOperativo" Visible="false" />
                                                                            <asp:BoundField DataField="intIdTipoRiesgoOperativo" HeaderText="Id TipoRiesgoOperativo" SortExpression="intIdTipoRiesgoOperativo" Visible="false" />
                                                                            <asp:BoundField DataField="intIdTipoEventoOperativo" HeaderText="Id TipoEventoOperativo" SortExpression="intIdTipoEventoOperativo" Visible="false" />
                                                                            <asp:BoundField DataField="intIdRiesgoAsociadoOperativo" HeaderText="Id RiesgoAsociadoOperativo" SortExpression="intIdRiesgoAsociadoOperativo" Visible="false" />
                                                                            <asp:BoundField DataField="intIdResponsableRiesgo" HeaderText="Id ResponsableRiesgo" SortExpression="intIdResponsableRiesgo" Visible="false" />
                                                                            <asp:BoundField DataField="intIdProbabilidad" HeaderText="Id Probabilidad" SortExpression="intIdProbabilidad" Visible="false" />
                                                                            <asp:BoundField DataField="intIdProbabilidadResidual" HeaderText="Id ProbabilidadResidual" SortExpression="intIdProbabilidadResidual" Visible="false" />
                                                                            <asp:BoundField DataField="intIdImpacto" HeaderText="Id Impacto" SortExpression="intIdImpacto" Visible="false" />
                                                                            <asp:BoundField DataField="intIdImpactoResidual" HeaderText="Id ImpactoResidual" SortExpression="intIdImpactoResidual" Visible="false" />
                                                                            <asp:BoundField DataField="strListaRiesgoAsociadoLA" HeaderText="ListaRiesgoAsociadoLA" SortExpression="strListaRiesgoAsociadoLA" Visible="false" />
                                                                            <asp:BoundField DataField="strListaFactorRiesgoLAFT" HeaderText="ListaFactorRiesgoLAFT" SortExpression="strListaFactorRiesgoLAFT" Visible="false" />
                                                                            <asp:BoundField DataField="strCliente" HeaderText="Cliente" SortExpression="strCliente" Visible="false" />
                                                                            <asp:BoundField DataField="strListaCausas" HeaderText="ListaCausas" SortExpression="strListaCausas" Visible="false" />
                                                                            <asp:BoundField DataField="strListaConsecuencias" HeaderText="ListaConsecuencias" SortExpression="strListaConsecuencias" Visible="false" />
                                                                            <asp:BoundField DataField="strOcurrenciaEventoHasta" HeaderText="OcurrenciaEventoHasta" SortExpression="strOcurrenciaEventoHasta" Visible="false" />
                                                                            <asp:BoundField DataField="strOcurrenciaEventoDesde" HeaderText="OcurrenciaEventoDesde" SortExpression="strOcurrenciaEventoDesde" Visible="false" />
                                                                            <asp:BoundField DataField="strPerdidaEconomicaDesde" HeaderText="PerdidaEconomicaDesde" SortExpression="strPerdidaEconomicaDesde" Visible="false" />
                                                                            <asp:BoundField DataField="strPerdidaEconomicaHasta" HeaderText="PerdidaEconomicaHasta" SortExpression="strPerdidaEconomicaHasta" Visible="false" />
                                                                            <asp:BoundField DataField="strListaTratamiento" HeaderText="ListaTratamiento" SortExpression="strListaTratamiento" Visible="false" />
                                                                            <asp:TemplateField HeaderText="Estado" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="chbActivo" runat="server" Checked='<% # Bind("booAnulado")%>' Enabled="false" />
                                                                                </ItemTemplate>
                                                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                            </asp:TemplateField>
                                                                            <asp:BoundField DataField="intIdUsuario" HeaderText="Id Usuario" SortExpression="intIdUsuario" Visible="False" />
                                                                            <asp:BoundField DataField="dtFechaRegistro" HeaderText="Fecha de Creación" SortExpression="dtFechaRegistro" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                                                        </Columns>
                                                                        <EditRowStyle BackColor="#999999" />
                                                                        <FooterStyle BackColor="White" Font-Bold="True" ForeColor="White"></FooterStyle>
                                                                        <HeaderStyle BackColor="#5D7B9D" CssClass="gridViewHeader" Font-Bold="True" ForeColor="White"></HeaderStyle>
                                                                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center"></PagerStyle>
                                                                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Left"></RowStyle>
                                                                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333"></SelectedRowStyle>
                                                                        <SortedAscendingCellStyle BackColor="#E9E7E2"></SortedAscendingCellStyle>
                                                                        <SortedAscendingHeaderStyle BackColor="#506C8C"></SortedAscendingHeaderStyle>
                                                                        <SortedDescendingCellStyle BackColor="#FFFDF8"></SortedDescendingCellStyle>
                                                                        <SortedDescendingHeaderStyle BackColor="#6F8DAE"></SortedDescendingHeaderStyle>
                                                                    </asp:GridView>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ContentTemplate>
                                                </asp:TabPanel>
                                            </asp:TabContainer>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr align="center">
                            <td>
                                <asp:ImageButton ID="btnCancelarConsultar" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                    OnClick="btnCancelarConsultar_Click" ToolTip="Cancelar" />
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
