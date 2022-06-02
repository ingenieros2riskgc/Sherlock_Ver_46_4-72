<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Indicador.ascx.cs" Inherits="ListasSarlaft.UserControls.Proceso.Indicador" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
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

    .Apariencia
    {
    }

    .centerTable
    {
        margin-left: auto;
        margin-right: auto;
    }

    .centertdtr
    {
        text-align: center;
    }


    .righttdtr
    {
        text-align: right;
    }

    .right5D7B9D
    {
        text-align: right;
        background: #5D7B9D;
    }

    .lefttdtr
    {
        text-align: left;
    }

    .leftBBBBBB
    {
        text-align: left;
        background: #BBBBBB;
    }

    .Tablewidth
    {
        width: 100%;
    }

    .TablaEspecial
    {
        width: 100%;
        border: hidden;
        border: 0;
        vertical-align: middle;
    }

    .centerMiddle
    {
        text-align: center;
        vertical-align: middle;
    }

    .LeftMiddle
    {
        text-align: left;
        vertical-align: middle;
    }

    .Toptdtr
    {
        vertical-align: top;
    }
</style>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <uc:OkMessageBox ID="omb" runat="server" />
        <asp:Panel ID="pnlMsgBox" runat="server" Width="400px" CssClass="modalPopup" Style="display: none;">
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
                    <td class="righttdtr" colspan="2">
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

        <asp:Panel ID="pnlObjetivo" runat="server" CssClass="popup" Width="400px" Style="display: none">
            <table class="TablaEspecial">
                <tr class="right5D7B9D">
                    <td>
                        <asp:ImageButton ID="ImageButton8" runat="server" ImageUrl="~/Imagenes/Icons/dialog-close2.png"
                            OnClientClick="$find('popupObjetivo').hidePopup(); return false;" />
                    </td>
                </tr>
                <tr class="centertdtr">
                    <td>
                        <asp:GridView ID="GridView2" runat="server" Font-Names="Calibri" CellPadding="4"
                            Font-Size="Small" HeaderStyle-CssClass="gridViewHeader" AutoGenerateColumns="False"
                            BorderStyle="Solid" AllowPaging="True" AllowSorting="True" GridLines="Vertical"
                            ShowHeaderWhenEmpty="True" ForeColor="#333333" Font-Bold="False" OnRowCommand="GridView2_RowCommand">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:BoundField DataField="intId" HeaderText="Código" ReadOnly="True" SortExpression="intId" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="no-visible" HeaderStyle-CssClass="no-visible" />
                                <asp:TemplateField HeaderText="Descripción" HeaderStyle-HorizontalAlign="center">
                                    <ItemTemplate>
                                        <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 100px">
                                            <asp:Label ID="Descripcion" runat="server" Text='<% # Bind("strDescripcion")%>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                    <HeaderStyle Wrap="false" Width="100" />
                                    <ItemStyle Wrap="false" Width="100" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Estado" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" Visible="false">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chbActivo" runat="server" Checked='<% # Bind("booEstado")%>' Enabled="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="intIdUsuario" HeaderText="IdUsuario" SortExpression="intIdUsuario" Visible="False" />
                                <asp:BoundField DataField="strNombreUsuario" HeaderText="Usuario Creación" SortExpression="strNombreUsuario" Visible="False" />
                                <asp:BoundField DataField="dtFechaRegistro" HeaderText="Fecha Creación" SortExpression="dtFechaRegistro"
                                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" Visible="False" />
                                <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Icons/select.png" Text="Seleccionar"
                                    HeaderText="Seleccionar" CommandName="Seleccionar" ItemStyle-HorizontalAlign="Center" />
                            </Columns>
                            <EditRowStyle BackColor="#999999" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#5D7B9D" CssClass="gridViewHeader" ForeColor="White" Font-Bold="True" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                        </asp:GridView>
                    </td>
                </tr>
                <tr class="centertdtr">
                    <td>
                        <asp:Button ID="Button1" runat="server" Text="Aceptar" CssClass="Apariencia" OnClientClick="$find('popupObjetivo').hidePopup(); return false;" />
                    </td>
                </tr>
            </table>
        </asp:Panel>

        <table class="centerTable" style="width: 100%">
            <tr class="centertdtr" style="background: #333399">
                <td>
                    <asp:Label ID="Label13" runat="server" ForeColor="White" Text="Indicador" Font-Bold="False"
                        Font-Names="Calibri" Font-Size="Large"></asp:Label>
                    <asp:Label ID="LidIndicador" runat="server" ForeColor="White" Text="" Font-Bold="False"
                        Font-Names="Calibri" Font-Size="Large" Visible="false"></asp:Label>
                </td>
            </tr>
            <tr id="filaGrid" style="background: #EEEEEE" runat="server" visible="true">
                <td style="background: #EEEEEE" align="center">
                    <br />
                    <table>
                        <tr>
                            <td>
                                <asp:GridView ID="GridView1" runat="server" CellPadding="4"
                                    ForeColor="#333333" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
                                    ShowHeaderWhenEmpty="True" OnRowCommand="GridView1_RowCommand"
                                    HeaderStyle-CssClass="gridViewHeader" BorderStyle="Solid" GridLines="Vertical"
                                    CssClass="Apariencia" Font-Bold="False" OnPageIndexChanging="GridView1_PageIndexChanging">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:BoundField DataField="intId" HeaderText="Código" SortExpression="intId" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="no-visible" HeaderStyle-CssClass="no-visible" />
                                        <asp:TemplateField HeaderText="Nombre Indicador" ItemStyle-HorizontalAlign="center">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 150px">
                                                    <asp:Label ID="NombreIndicador" runat="server" Text='<% # Bind("strNombreIndicador")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" Width="150px" HorizontalAlign="center" />
                                            <ItemStyle Wrap="false" Width="150px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Descripción" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 300px">
                                                    <asp:Label ID="Descripcion" runat="server" Text='<% # Bind("strDescripcion")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" Width="300" HorizontalAlign="center" />
                                            <ItemStyle Wrap="false" Width="300" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Proceso" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 300px">
                                                    <asp:Label ID="proceso" runat="server" Text='<% # Bind("strProceso")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" Width="300" HorizontalAlign="center" />
                                            <ItemStyle Wrap="false" Width="300" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="intIdPeriodicidad" HeaderText="Id Periodicidad" SortExpression="intIdPeriodicidad" HtmlEncodeFormatString="True" HtmlEncode="False" Visible="False" />
                                        <asp:BoundField DataField="intMeta" HeaderText="Meta" SortExpression="intMeta" HtmlEncodeFormatString="True" HtmlEncode="False" Visible="False" />
                                        <asp:TemplateField HeaderText="Estado" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chbActivo" runat="server" Checked='<% # Bind("booEstado")%>' Enabled="false" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="intIdObjetivoCalidad" HeaderText="Id Objetivo Calidad" SortExpression="intIdObjetivoCalidad" Visible="False" />
                                        <asp:BoundField DataField="strDescObjetivo" HeaderText="Desc. Objetivo" SortExpression="strDescObjetivo" Visible="False" />
                                        <asp:BoundField DataField="intIdProcesoIndicador" HeaderText="Id ProcesoIndicador" SortExpression="intIdProcesoIndicador" Visible="False" />
                                        <asp:BoundField DataField="intIdUsuario" HeaderText="IdUsuario" SortExpression="intIdUsuario" Visible="False" />
                                        <asp:BoundField DataField="strNombreUsuario" HeaderText="Usuario" SortExpression="strNombreUsuario" Visible="False" />
                                        <asp:BoundField DataField="dtFechaRegistro" HeaderText="FechaRegistro" ReadOnly="True" SortExpression="dtFechaRegistro" ItemStyle-HorizontalAlign="Center" />
                                        <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Icons/edit.png" Text="Modificar" HeaderText="Modificar" CommandName="Modificar" ItemStyle-HorizontalAlign="Center" />
                                        <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Icons/switch-on-icon.png" Text="(In)Activar" HeaderText="(In)Activar" CommandName="Activar" ItemStyle-HorizontalAlign="Center" />
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
                            <td class="righttdtr">
                                <asp:ImageButton ID="btnInsertarNuevo" runat="server" CausesValidation="False" CommandName="Insert"
                                    ImageUrl="~/Imagenes/Icons/Add.png" Text="Insert" OnClick="btnInsertarNuevo_Click" ToolTip="Insertar" />
                            </td>
                        </tr>
                    </table>
                    <br />
                </td>
            </tr>
            <tr id="filaDetalle" runat="server" visible="false">
                <td style="background: #EEEEEE" align="center">
                    <ajax:TabContainer ID="TabDetalles" runat="server" ActiveTabIndex="0" Font-Names="Calibri" Font-Size="Small" Width="800px">
                        <ajax:TabPanel ID="tpGeneral" runat="server" HeaderText="General" Font-Names="Calibri" Font-Size="Small">
                            <HeaderTemplate>General</HeaderTemplate>
                            <ContentTemplate>
                                <table class="tabla" style="width: 100%">
                                    <tr id="Codigo">
                                        <td class="leftBBBBBB">
                                            <asp:Label ID="Label1" runat="server" Text="Código:" CssClass="Apariencia"></asp:Label></td>
                                        <td>
                                            <asp:TextBox ID="tbxId" runat="server" Enabled="False" Width="100px" CssClass="Apariencia"></asp:TextBox></td>
                                    </tr>

                                    <tr id="CadenaValor">
                                        <td class="leftBBBBBB">
                                            <asp:Label ID="Label19" runat="server" Text="Cadena de Valor:" CssClass="Apariencia"></asp:Label></td>
                                        <td>
                                            <asp:DropDownList ID="ddlCadenaValor" runat="server" Width="300px"
                                                CssClass="Apariencia" AutoPostBack="True"
                                                DataTextField="NombreCadenaValor" DataValueField="IdCadenaValor"
                                                OnSelectedIndexChanged="ddlCadenaValor_SelectedIndexChanged">
                                            </asp:DropDownList><asp:RequiredFieldValidator ID="rfvCadenaValor" runat="server" ControlToValidate="ddlCadenaValor"
                                                ErrorMessage="Debe ingresar la cadena de valor." ToolTip="Debe ingresar la cadena de valor."
                                                ValidationGroup="iIndicador" ForeColor="Red" InitialValue="0">*</asp:RequiredFieldValidator>
                                            <asp:TextBox ID="tbxProcIndica" runat="server" Width="90px" MaxLength="20" CssClass="Apariencia" Visible="false"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr id="Macroproceso">
                                        <td class="leftBBBBBB">
                                            <asp:Label ID="Label22" runat="server" Text="Macroproceso:" CssClass="Apariencia"></asp:Label></td>
                                        <td>
                                            <asp:DropDownList ID="ddlMacroproceso" runat="server" Width="300px"
                                                CssClass="Apariencia" AutoPostBack="True" DataTextField="Nombre" DataValueField="IdMacroproceso"
                                                OnSelectedIndexChanged="ddlMacroproceso_SelectedIndexChanged">
                                            </asp:DropDownList><asp:RequiredFieldValidator ID="rfvMacroProceso" runat="server" ControlToValidate="ddlMacroproceso"
                                                ErrorMessage="Debe ingresar el Macroproceso." ToolTip="Debe ingresar el Macroproceso."
                                                ValidationGroup="iIndicador" ForeColor="Red" InitialValue="0">*</asp:RequiredFieldValidator></td>
                                    </tr>
                                    <tr id="Proceso" runat="server" visible="false">
                                        <td class="leftBBBBBB">
                                            <asp:Label ID="Label7" runat="server" Text="Proceso:" CssClass="Apariencia"></asp:Label></td>
                                        <td>
                                            <asp:DropDownList ID="ddlProceso" runat="server" Width="300px" OnSelectedIndexChanged="ddlProceso_SelectedIndexChanged"
                                                CssClass="Apariencia" AutoPostBack="True" DataTextField="Nombre" DataValueField="IdProceso">
                                            </asp:DropDownList><asp:RequiredFieldValidator ID="rfvProceso" runat="server" ControlToValidate="ddlProceso"
                                                ErrorMessage="Debe ingresar el Proceso." ToolTip="Debe ingresar el Proceso." Enabled="False"
                                                ValidationGroup="iIndicador1" ForeColor="Red" InitialValue="0">*</asp:RequiredFieldValidator></td>
                                    </tr>
                                    <tr id="Subproceso" runat="server" visible="false">
                                        <td class="leftBBBBBB">
                                            <asp:Label ID="Label8" runat="server" Text="Subproceso:" CssClass="Apariencia"></asp:Label></td>
                                        <td>
                                            <asp:DropDownList ID="ddlSubproceso" runat="server" Width="300px" OnSelectedIndexChanged="ddlSubproceso_SelectedIndexChanged"
                                                CssClass="Apariencia" AutoPostBack="True" DataTextField="Nombre" DataValueField="IdProceso">
                                            </asp:DropDownList><asp:RequiredFieldValidator ID="rfvSubproceso" runat="server" ControlToValidate="ddlSubproceso"
                                                ErrorMessage="Debe ingresar el Subproceso." ToolTip="Debe ingresar el Subproceso." Enabled="False"
                                                ValidationGroup="iIndicador1" ForeColor="Red" InitialValue="0">*</asp:RequiredFieldValidator></td>
                                    </tr>
                                    <tr id="NombreIndicador">
                                        <td class="leftBBBBBB">
                                            <asp:Label ID="LnombreIndicador" runat="server" Text="Nombre Indicador:" CssClass="Apariencia"></asp:Label>

                                        </td>
                                        <td>
                                            <asp:TextBox ID="TXnombreIndicador" runat="server" Width="600px" CssClass="Apariencia" Height="42px"
                                                Columns="100" Rows="4" TextMode="SingleLine" Font-Size="10pt" Font-Bold="False"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TXnombreIndicador"
                                                    ErrorMessage="Debe ingresar el nombre del Indicador." ToolTip="Debe ingresar el nombre del Indicador."
                                                    ValidationGroup="iIndicador" ForeColor="Red">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr id="Descripcion">
                                        <td class="leftBBBBBB">
                                            <asp:Label ID="Label5" runat="server" Text="Descripción:" CssClass="Apariencia"></asp:Label></td>
                                        <td>
                                            <asp:TextBox ID="tbxDescripcion" runat="server" Width="600px" CssClass="Apariencia" Height="42px"
                                                Columns="100" Rows="4" TextMode="MultiLine" Font-Size="10pt" Font-Bold="False"></asp:TextBox><asp:RequiredFieldValidator ID="rfvDesc" runat="server" ControlToValidate="tbxDescripcion"
                                                    ErrorMessage="Debe ingresar la Descripción." ToolTip="Debe ingresar la Descripción."
                                                    ValidationGroup="iIndicador" ForeColor="Red">*</asp:RequiredFieldValidator></td>
                                    </tr>
                                    <tr id="Objetivo">
                                        <td class="leftBBBBBB">
                                            <asp:Label ID="Label6" runat="server" Text="Objetivo:" CssClass="Apariencia"></asp:Label></td>
                                        <td>
                                            <asp:TextBox ID="tbxObjetivo" runat="server" CssClass="Apariencia" Enabled="False" Height="42px"
                                                Width="600px" Columns="100" Rows="4" TextMode="MultiLine" Font-Size="10pt" Font-Bold="False" MaxLength="250"></asp:TextBox><asp:Label ID="lblIdObjetivo" runat="server" Text="Label" Visible="False"></asp:Label><asp:ImageButton ID="btnObjetivo" runat="server" ImageUrl="~/Imagenes/Icons/Apps-Search-And-Replace-icon.png"
                                                    OnClientClick="return false;" /><asp:PopupControlExtender ID="popupObjetivo" runat="server" BehaviorID="popupObjetivo"
                                                        ExtenderControlID="" PopupControlID="pnlObjetivo" Position="Bottom" TargetControlID="btnObjetivo">
                                                    </asp:PopupControlExtender>
                                            <asp:RequiredFieldValidator ID="rfvObjetivo" runat="server" ControlToValidate="tbxObjetivo"
                                                ErrorMessage="Debe ingresar el Objetivo." ToolTip="Debe ingresar el Objetivo."
                                                ValidationGroup="iIndicador" ForeColor="Red">*</asp:RequiredFieldValidator></td>
                                    </tr>
                                    <tr id="Periodicidad">
                                        <td class="leftBBBBBB">
                                            <asp:Label ID="Label2" runat="server" Text="Periodicidad:" CssClass="Apariencia"></asp:Label></td>
                                        <td>
                                            <asp:DropDownList ID="ddlPeriodo" runat="server" Width="100px"
                                                CssClass="Apariencia" DataTextField="Nombre" DataValueField="Id">
                                            </asp:DropDownList><asp:RequiredFieldValidator ID="rfvPeriodicidad" runat="server" ControlToValidate="ddlPeriodo"
                                                ErrorMessage="Debe ingresar la periodicidad." ToolTip="Debe ingresar la periodicidad."
                                                ValidationGroup="iIndicador" ForeColor="Red" InitialValue="0">*</asp:RequiredFieldValidator></td>
                                    </tr>
                                    <tr id="Meta">
                                        <td class="leftBBBBBB">
                                            <asp:Label ID="Label11" runat="server" Text="Meta:" CssClass="Apariencia"></asp:Label></td>
                                        <td>
                                            <asp:TextBox ID="tbxMeta" runat="server" Width="90px" MaxLength="50" CssClass="Apariencia"></asp:TextBox>
                                            <asp:Label ID="Label9" runat="server" Text=" %  (Rango de 0% a 100%)" CssClass="Apariencia"></asp:Label>
                                            <asp:RequiredFieldValidator ID="rfvMeta" runat="server" ControlToValidate="tbxMeta"
                                                ErrorMessage="Debe ingresar la meta." ToolTip="Debe ingresar la meta."
                                                ValidationGroup="iIndicador" ForeColor="Red">*</asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="revMeta" runat="server" ControlToValidate="tbxMeta"
                                                ValidationExpression="^[0-9]+([.,][0-9]{1,3})?$" ForeColor="Red" ErrorMessage="Solo números" ToolTip="Solo números"
                                                ValidationGroup="iIndicador">*</asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                    <tr id="Estado">
                                        <td class="leftBBBBBB">
                                            <asp:Label ID="Label12" runat="server" Text="Estado:" CssClass="Apariencia"></asp:Label></td>
                                        <td>
                                            <asp:CheckBox ID="ChBEstado" runat="server" Checked="True" Enabled="false"/></td>
                                    </tr>
                                    <tr id="UsuarioCreacion">
                                        <td class="leftBBBBBB">
                                            <asp:Label ID="Label3" runat="server" Text="Usuario creación:" CssClass="Apariencia"></asp:Label></td>
                                        <td>
                                            <asp:TextBox ID="tbxUsuario" runat="server" Width="100px" CssClass="Apariencia"
                                                Enabled="False"></asp:TextBox></td>
                                    </tr>
                                    <tr id="FechaCreacion">
                                        <td class="leftBBBBBB">
                                            <asp:Label ID="Label4" runat="server" Text="Fecha de Creación:" CssClass="Apariencia"></asp:Label></td>
                                        <td>
                                            <asp:TextBox ID="tbxFecha" runat="server" Width="100px" CssClass="Apariencia"
                                                Enabled="False"></asp:TextBox></td>
                                    </tr>
                                    <tr class="centerMiddle" id="BotonSiguienteGral">
                                        <td colspan="2">
                                            <asp:ImageButton ID="btnSiguienteGral" runat="server" ImageUrl="~/Imagenes/Icons/Button-Next-icon.png"
                                                OnClick="btnSiguienteGral_Click" ToolTip="Siguiente" ValidationGroup="iIndicador" />
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </ajax:TabPanel>

                        <ajax:TabPanel ID="tpVariables" runat="server" HeaderText="Variables" Font-Names="Calibri" Font-Size="Small">
                            <HeaderTemplate>Variables</HeaderTemplate>
                            <ContentTemplate>
                                <table class="tabla" style="width: 100%">
                                    <tr>
                                        <td>
                                            <asp:GridView ID="GridView3" runat="server" CellPadding="4"
                                                ForeColor="#333333" AllowSorting="True" AutoGenerateColumns="False"
                                                ShowHeaderWhenEmpty="True" OnRowCommand="GridView3_RowCommand" BorderStyle="Solid" GridLines="Vertical"
                                                CssClass="Apariencia" Font-Bold="False" AllowPaging="True" OnPageIndexChanging="GridView3_PageIndexChanging">
                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                <Columns>
                                                    <asp:BoundField DataField="intId" HeaderText="Código" SortExpression="intId" >
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="Descripción">
                                                        <ItemTemplate>
                                                            <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 100px">
                                                                <asp:Label ID="Descripcion" runat="server" Text='<% # Bind("strDescripcion")%>'></asp:Label>
                                                            </div>
                                                        </ItemTemplate>
                                                        <HeaderStyle Wrap="False" Width="100px" HorizontalAlign="Center" />
                                                        <ItemStyle Wrap="False" Width="100px" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="strFormato" HeaderText="Formato" SortExpression="strFormato" >
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="Estado">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chbActivo" runat="server" Checked='<% # Bind("booEstado")%>' Enabled="false" />
                                                        </ItemTemplate>
                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="intIdUsuario" HeaderText="IdUsuario" SortExpression="intIdUsuario" Visible="False" />
                                                    <asp:BoundField DataField="strNombreUsuario" HeaderText="Usuario Creación" SortExpression="strNombreUsuario" Visible="False" />
                                                    <asp:BoundField DataField="dtFechaRegistro" HeaderText="Fecha de Creación" SortExpression="dtFechaRegistro" >
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Icons/edit.png" Text="Modificar" HeaderText="Modificar" CommandName="Modificar" >
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    </asp:ButtonField>
                                                    <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Icons/switch-on-icon.png" Text="(In)Activar" HeaderText="(In)Activar" CommandName="Activar" >
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    </asp:ButtonField>
                                                </Columns>
                                                <EditRowStyle BackColor="#999999" />
                                                <FooterStyle BackColor="White" Font-Bold="true" ForeColor="White" />
                                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" CssClass="gridViewHeader" />
                                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                            </asp:GridView>
                                            <asp:ImageButton ID="btnNuevaVar" runat="server" CausesValidation="False" CommandName="Insert"
                                                ImageUrl="~/Imagenes/Icons/Add.png" Text="Insert" OnClick="btnNuevaVar_Click" ToolTip="Insertar" /></td>
                                        <td>
                                            <table id="TblDetallesVar" class="tabla" width="100%" runat="server" visible="False">
                                                <tr id="CodigoVar" runat="server">
                                                    <td id="Td1" align="left" bgcolor="#BBBBBB" runat="server">
                                                        <asp:Label ID="Label30" runat="server" Text="Código:" CssClass="Apariencia"></asp:Label></td>
                                                    <td id="Td2" runat="server">
                                                        <asp:TextBox ID="tbxCodigoVar" runat="server" Width="100px" CssClass="Apariencia" MaxLength="60" Enabled="False"></asp:TextBox></td>
                                                </tr>
                                                <tr id="DescripcionVar" runat="server">
                                                    <td id="Td3" class="leftBBBBBB" runat="server">
                                                        <asp:Label ID="Label10" runat="server" Text="Descripción:" CssClass="Apariencia"></asp:Label></td>
                                                    <td id="Td4" runat="server">
                                                        <asp:TextBox ID="tbxDescripcionVar" runat="server" Width="100px" CssClass="Apariencia" MaxLength="60"></asp:TextBox><asp:RequiredFieldValidator ID="rfvDescripcionVar" runat="server" ControlToValidate="tbxDescripcionVar"
                                                            ErrorMessage="Debe ingresar la descripción." ToolTip="Debe ingresar la descripción."
                                                            ValidationGroup="iIndicadorVar" ForeColor="Red">*</asp:RequiredFieldValidator></td>
                                                </tr>
                                                <tr id="FormatoVar" runat="server">
                                                    <td id="Td5" class="leftBBBBBB" runat="server">
                                                        <asp:Label ID="Label14" runat="server" Text="Formato:" CssClass="Apariencia"></asp:Label></td>
                                                    <td id="Td6" runat="server">
                                                        <asp:DropDownList ID="ddlFormato" runat="server" Width="100px"
                                                            CssClass="Apariencia" DataTextField="NombreFormato" DataValueField="IdFormato">
                                                        </asp:DropDownList><asp:RequiredFieldValidator ID="rfvFormatoVar" runat="server" ControlToValidate="ddlFormato"
                                                            ErrorMessage="Debe ingresar el formato de la variable." ToolTip="Debe ingresar el formato de la variable."
                                                            ValidationGroup="iIndicadorVar" ForeColor="Red" InitialValue="0">*</asp:RequiredFieldValidator></td>
                                                </tr>
                                                <tr id="EstadoVar" runat="server">
                                                    <td id="Td7" class="leftBBBBBB" runat="server">
                                                        <asp:Label ID="Label31" runat="server" Text="Estado:" CssClass="Apariencia"></asp:Label></td>
                                                    <td id="Td8" runat="server">
                                                        <asp:CheckBox ID="chbEstadoVar" runat="server" Checked="True" /></td>
                                                </tr>
                                                <tr align="center" id="BotonesVar" runat="server">
                                                    <td id="Td9" colspan="2" runat="server">
                                                        <asp:ImageButton ID="btnGuardarVar" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                            OnClick="btnGuardarVar_Click" ToolTip="Siguiente" ValidationGroup="iIndicadorVar" Visible="False" /><asp:ImageButton ID="btnActualizarVar" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                                Style="text-align: right" OnClick="btnActualizarVar_Click" ValidationGroup="iIndicadorVar"
                                                                ToolTip="Guardar" Visible="False" /><asp:ImageButton ID="btnCancelarVar" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                                                    OnClick="btnCancelarVar_Click" ToolTip="Cancelar" /></td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr id="BotonesSigVar" class="centertdtr">
                                        <td colspan="2">
                                            <asp:ImageButton ID="btnPrevioVar" runat="server" ImageUrl="~/Imagenes/Icons/Button-Previous-icon.png"
                                                OnClick="btnPrevioVar_Click" ToolTip="Anterior" /><asp:ImageButton ID="btnSiguienteVar" runat="server" ImageUrl="~/Imagenes/Icons/Button-Next-icon.png"
                                                    OnClick="btnSiguienteVar_Click" ToolTip="Siguiente" /></td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </ajax:TabPanel>

                        <ajax:TabPanel ID="tpFormula" runat="server" HeaderText="Fórmula" Font-Names="Calibri" Font-Size="Small">
                            <HeaderTemplate>Fórmula</HeaderTemplate>
                            <ContentTemplate>
                                <table class="tabla" style="width: 100%">
                                    <tr>
                                        <td id="ZonaVariables" align="center" class="Toptdtr">
                                            <table>
                                                <tr>
                                                    <td class="leftBBBBBB">
                                                        <asp:Label ID="Label34" runat="server" Text="Variables:"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:GridView ID="GridView4" runat="server" CellPadding="4"
                                                            ForeColor="#333333" AllowSorting="True" AutoGenerateColumns="False"
                                                            ShowHeaderWhenEmpty="True" OnRowCommand="GridView4_RowCommand" BorderStyle="Solid" GridLines="Vertical"
                                                            CssClass="Apariencia" Font-Bold="False" AllowPaging="True" OnPageIndexChanging="GridView4_PageIndexChanging">
                                                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                            <Columns>
                                                                <asp:BoundField DataField="intId" HeaderText="Código" SortExpression="intId" >
                                                                <ItemStyle HorizontalAlign="Center" />
                                                                </asp:BoundField>
                                                                <asp:TemplateField HeaderText="Descripción">
                                                                    <ItemTemplate>
                                                                        <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 100px">
                                                                            <asp:Label ID="Descripcion" runat="server" Text='<% # Bind("strDescripcion")%>'></asp:Label>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle Wrap="False" Width="100px" HorizontalAlign="Center" />
                                                                    <ItemStyle Wrap="False" Width="100px" />
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="strFormato" HeaderText="Formato" SortExpression="strFormato" >
                                                                <ItemStyle HorizontalAlign="Center" />
                                                                </asp:BoundField>
                                                                <asp:TemplateField HeaderText="Estado" Visible="False">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chbActivo" runat="server" Checked='<% # Bind("booEstado")%>' Enabled="false" />
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="intIdUsuario" HeaderText="IdUsuario" SortExpression="intIdUsuario" Visible="False" />
                                                                <asp:BoundField DataField="strNombreUsuario" HeaderText="Usuario Creación" SortExpression="strNombreUsuario" Visible="False" />
                                                                <asp:BoundField DataField="dtFechaRegistro" HeaderText="Fecha de Creación" SortExpression="dtFechaRegistro" Visible="False" >
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Center" />
                                                                </asp:BoundField>
                                                                <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Icons/select.png" Text="Seleccionar" HeaderText="Seleccionar"
                                                                    CommandName="Seleccionar" >
                                                                <ItemStyle HorizontalAlign="Center" />
                                                                </asp:ButtonField>
                                                            </Columns>
                                                            <EditRowStyle BackColor="#999999" />
                                                            <FooterStyle BackColor="White" Font-Bold="True" ForeColor="White" />
                                                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" CssClass="gridViewHeader" />
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
                                            </table>
                                        </td>
                                        <td id="ZonaOperaciones" class="Toptdtr">
                                            <table>
                                                <tr>
                                                    <td align="center">
                                                        <table id="tblCalculadora" runat="server" class="centertdtr">
                                                            <tr id="FilaOperadores1" runat="server">
                                                                <td id="OperadorMas" runat="server">
                                                                    <asp:Button ID="btnOperMas" runat="server" Text="+" Width="30px" Font-Names="Calibri"
                                                                        Font-Size="Small" OnClick="btnOperMas_Click" /></td>
                                                                <td id="OperadorMenos" runat="server">
                                                                    <asp:Button ID="btnMenos" runat="server" Text="-" Width="30px" Font-Names="Calibri"
                                                                        Font-Size="Small" OnClick="btnMenos_Click" /></td>
                                                            </tr>
                                                            <tr id="FilaOperadores2" runat="server">
                                                                <td id="OperadorMultiplica" runat="server">
                                                                    <asp:Button ID="btnOperPor" runat="server" Text="x" Width="30px" Font-Names="Calibri"
                                                                        Font-Size="Small" OnClick="btnOperPor_Click" /></td>
                                                                <td id="OperadorDividir" runat="server">
                                                                    <asp:Button ID="btnOperDividir" runat="server" Text="/" Width="30px" Font-Names="Calibri"
                                                                        Font-Size="Small" OnClick="btnOperDividir_Click" /></td>
                                                            </tr>
                                                            <tr id="FilaOperadores4" runat="server">
                                                                <td id="OperadorPorcentaje" runat="server">
                                                                    <asp:Button ID="btnOperPorcentaje" runat="server" Text="%" Width="30px" Font-Names="Calibri"
                                                                        Font-Size="Small" OnClick="btnOperPorcentaje_Click" /></td>
                                                                <td id="OperadorBorrar" runat="server">
                                                                    <asp:Button ID="btnOperBorrar" runat="server" Text="C" Width="30px" Font-Names="Calibri"
                                                                        Font-Size="Small" OnClick="btnOperBorrar_Click" Style="font-weight: 700; color: #FF0000" /></td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="leftBBBBBB">
                                                        <asp:Label ID="Label33" runat="server" Text="Campo de Números:"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="tbxCampoTexto" runat="server" MaxLength="6"></asp:TextBox>
                                                        <asp:RegularExpressionValidator ID="revCampoTexto" runat="server" ControlToValidate="tbxCampoTexto"
                                                            ValidationExpression="^\d+$" ForeColor="Red" ErrorMessage="Solo números" ToolTip="Solo números"
                                                            ValidationGroup="iNumero">*</asp:RegularExpressionValidator>
                                                        <asp:ImageButton ID="btnSelectCampoTexto" runat="server" ImageUrl="~/Imagenes/Icons/select.png"
                                                            OnClick="btnSelectCampoTexto_Click" ToolTip="Seleccionar Campo Texto" ValidationGroup="iNumero" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="ZonaFormula" class="Toptdtr">
                                            <table>
                                                <tr>
                                                    <td class="leftBBBBBB">
                                                        <asp:Label ID="Label32" runat="server" Text="Fórmula:"></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="tbxCampoFormula" runat="server" Enabled="False" MaxLength="250"
                                                            TextMode="MultiLine" Height="81px" Width="325px"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvFormula" runat="server" ControlToValidate="tbxCampoFormula"
                                                            ErrorMessage="Debe ingresar la Fórmula." ToolTip="Debe ingresar la Fórmula."
                                                            ValidationGroup="iFormula" ForeColor="Red">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                    </tr>
                                    <tr class="centertdtr" id="BotonesSigForm">
                                        <td colspan="3">
                                            <asp:ImageButton ID="btnPrevioForm" runat="server" ImageUrl="~/Imagenes/Icons/Button-Previous-icon.png"
                                                OnClick="btnPrevioForm_Click" ToolTip="Anterior" />
                                            <asp:ImageButton ID="btnSiguienteForm" runat="server" ImageUrl="~/Imagenes/Icons/Button-Next-icon.png"
                                                OnClick="btnSiguienteForm_Click" ToolTip="Siguiente" ValidationGroup="iFormula" /></td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </ajax:TabPanel>

                        <ajax:TabPanel ID="tpCumplimiento" runat="server" HeaderText="Cumplimiento" Font-Names="Calibri" Font-Size="Small">
                            <HeaderTemplate>Cumplimiento</HeaderTemplate>
                            <ContentTemplate>
                                <table class="tabla" style="width: 100%">
                                    
                                    <tr id="ValoresRangos">
                                        <td class="lefttdtr">
                                            <table>
                                                <tr id="RangosUp">
                                                    <td>
                                                        <asp:Image ID="Image1" runat="server" ImageUrl="~/Imagenes/Aplicacion/Arriba.png" Width="20px" /></td>
                                                    <td class="leftBBBBBB">
                                                        <asp:Label ID="Label16" runat="server" Text="Mínimo:" CssClass="Apariencia"></asp:Label></td>
                                                    <td>
                                                        <asp:TextBox ID="tbxRangoMinUp" runat="server" Width="40px" CssClass="Apariencia" MaxLength="6"></asp:TextBox>
                                                        <asp:Label ID="Label24" runat="server" Text=" % (Rango de 0% a 100%)" CssClass="Apariencia"></asp:Label>
                                                        <asp:RequiredFieldValidator ID="rfvRangoMinUp" runat="server" ControlToValidate="tbxRangoMinUp"
                                                            ErrorMessage="Debe ingresar el Rango Mínimo." ToolTip="Debe ingresar el Rango Mínimo."
                                                            ValidationGroup="iIndicadorCum" ForeColor="Red">*</asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="revRangoMinUp" runat="server" ControlToValidate="tbxRangoMinUp"
                                                            ValidationExpression="^[0-9]+([.,][0-9]{1,3})?$" ForeColor="Red" ErrorMessage="Solo números enteros" ToolTip="Solo números enteros"
                                                            ValidationGroup="iIndicadorCum">*</asp:RegularExpressionValidator>
                                                    </td>
                                                    <td class="leftBBBBBB">
                                                        <asp:Label ID="Label17" runat="server" Text="Máximo:" CssClass="Apariencia"></asp:Label></td>
                                                    <td>
                                                        <asp:TextBox ID="tbxRangoMaxUp" runat="server" Width="40px" CssClass="Apariencia" MaxLength="6"></asp:TextBox>
                                                        <asp:Label ID="Label25" runat="server" Text=" % (Rango de 0% a 100%)" CssClass="Apariencia"></asp:Label>
                                                        <asp:RequiredFieldValidator ID="rfvRangoMaxUp" runat="server" ControlToValidate="tbxRangoMaxUp"
                                                            ErrorMessage="Debe ingresar el Rango Máximo." ToolTip="Debe ingresar el Rango Máximo."
                                                            ValidationGroup="iIndicadorCum" ForeColor="Red">*</asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="revRangoMaxUp" runat="server" 
                                                            ControlToValidate="tbxRangoMaxUp"
                                                            ValidationExpression="^[0-9]+([.,][0-9]{1,3})?$" 
                                                            ForeColor="Red" 
                                                            ErrorMessage="Solo números enteros" 
                                                            ToolTip="Solo números enteros"
                                                            ValidationGroup="iIndicadorCum">*</asp:RegularExpressionValidator>
                                                        
                                                        
                                                        
                                                    </td>
                                                    <td class="leftBBBBBB">
                                                        <asp:Label ID="Label37" runat="server" Text="Cumplimiento:" CssClass="Apariencia"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="tbxCumpUp" runat="server" Width="100px" CssClass="Apariencia" MaxLength="20"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvCumplimiento" runat="server" ControlToValidate="tbxCumpUp"
                                                            ErrorMessage="Debe ingresar el Cumplimiento." ToolTip="Debe ingresar el Cumplimiento."
                                                            ValidationGroup="iIndicadorCum" ForeColor="Red">*</asp:RequiredFieldValidator>
                                                        <asp:TextBox ID="tbxIdCumpUp" runat="server" Width="100px" CssClass="Apariencia" MaxLength="5" Visible="false"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr id="RangoInterM">
                                                    <td>
                                                        <asp:Image ID="Image2" runat="server" ImageUrl="~/Imagenes/Aplicacion/Igual.png" Width="20px" /></td>
                                                    <td class="leftBBBBBB">
                                                        <asp:Label ID="Label18" runat="server" Text="Mínimo:" CssClass="Apariencia"></asp:Label></td>
                                                    <td>
                                                        <asp:TextBox ID="tbxRangoMinOdd" runat="server" Width="40px" CssClass="Apariencia" MaxLength="6"></asp:TextBox>
                                                        <asp:Label ID="Label26" runat="server" Text=" % (Rango de 0% a 100%)" CssClass="Apariencia"></asp:Label>
                                                        <asp:RequiredFieldValidator ID="rfvRangoMinOdd" runat="server" ControlToValidate="tbxRangoMinOdd"
                                                            ErrorMessage="Debe ingresar el Rango Mínimo." ToolTip="Debe ingresar el Rango Mínimo."
                                                            ValidationGroup="iIndicadorCum" ForeColor="Red">*</asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="revRangoMinOdd" runat="server" ControlToValidate="tbxRangoMinOdd"
                                                            ValidationExpression="^[0-9]+([.,][0-9]{1,3})?$" ForeColor="Red" ErrorMessage="Solo números enteros" ToolTip="Solo números enteros"
                                                            ValidationGroup="iIndicadorCum">*</asp:RegularExpressionValidator>
                                                    </td>
                                                    <td class="leftBBBBBB">
                                                        <asp:Label ID="Label20" runat="server" Text="Máximo:" CssClass="Apariencia"></asp:Label></td>
                                                    <td>
                                                        <asp:TextBox ID="tbxRangoMaxOdd" runat="server" Width="40px" CssClass="Apariencia" MaxLength="6"></asp:TextBox>
                                                        <asp:Label ID="Label27" runat="server" Text=" % (Rango de 0% a 100%)" CssClass="Apariencia"></asp:Label>
                                                        <asp:RequiredFieldValidator ID="rfvRangoMaxOdd" runat="server" ControlToValidate="tbxRangoMaxOdd"
                                                            ErrorMessage="Debe ingresar el Rango Máximo." ToolTip="Debe ingresar el Rango Máximo."
                                                            ValidationGroup="iIndicadorCum" ForeColor="Red">*</asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="revRangoMaxOdd" runat="server" ControlToValidate="tbxRangoMaxOdd"
                                                            ValidationExpression="^[0-9]+([.,][0-9]{1,3})?$" ForeColor="Red" ErrorMessage="Solo números enteros" ToolTip="Solo números enteros"
                                                            ValidationGroup="iIndicadorCum">*</asp:RegularExpressionValidator>
                                                        
                                                        
                                                    </td>
                                                    <td class="leftBBBBBB">
                                                        <asp:Label ID="Label36" runat="server" Text="Cumplimiento:" CssClass="Apariencia"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="tbxCumpOdd" runat="server" Width="100px" CssClass="Apariencia" MaxLength="20"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbxCumpOdd"
                                                            ErrorMessage="Debe ingresar el Cumplimiento." ToolTip="Debe ingresar el Cumplimiento."
                                                            ValidationGroup="iIndicadorCum" ForeColor="Red">*</asp:RequiredFieldValidator>
                                                        <asp:TextBox ID="tbxIdCumpOdd" runat="server" Width="100px" CssClass="Apariencia" MaxLength="5" Visible="false"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr id="RangoDown">
                                                    <td>
                                                        <asp:Image ID="Image3" runat="server" ImageUrl="~/Imagenes/Aplicacion/Abajo.png" Width="20px" /></td>
                                                    <td class="leftBBBBBB">
                                                        <asp:Label ID="Label21" runat="server" Text="Mínimo:" CssClass="Apariencia"></asp:Label></td>
                                                    <td>
                                                        <asp:TextBox ID="tbxRangoMinDown" runat="server" Width="40px" CssClass="Apariencia" MaxLength="6"></asp:TextBox>
                                                        <asp:Label ID="Label28" runat="server" Text=" % (Rango de 0% a 100%)" CssClass="Apariencia"></asp:Label>
                                                        <asp:RequiredFieldValidator ID="rfvRangoMinDown" runat="server" ControlToValidate="tbxRangoMinDown"
                                                            ErrorMessage="Debe ingresar el Rango Mínimo." ToolTip="Debe ingresar el Rango Mínimo."
                                                            ValidationGroup="iIndicadorCum" ForeColor="Red">*</asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="revRangoMinDown" runat="server" ControlToValidate="tbxRangoMinDown"
                                                            ValidationExpression="^[0-9]+([.,][0-9]{1,3})?$" ForeColor="Red" ErrorMessage="Solo números enteros" ToolTip="Solo números enteros"
                                                            ValidationGroup="iIndicadorCum">*</asp:RegularExpressionValidator>
                                                    </td>
                                                    <td class="leftBBBBBB">
                                                        <asp:Label ID="Label23" runat="server" Text="Máximo:" CssClass="Apariencia"></asp:Label></td>
                                                    <td>
                                                        <asp:TextBox ID="tbxRangoMaxDown" runat="server" Width="40px" CssClass="Apariencia" MaxLength="6"></asp:TextBox>
                                                        <asp:Label ID="Label29" runat="server" Text=" % (Rango de 0% a 100%)" CssClass="Apariencia"></asp:Label>
                                                        <asp:RequiredFieldValidator ID="rfvRangoMaxDown" runat="server" ControlToValidate="tbxRangoMaxDown"
                                                            ErrorMessage="Debe ingresar el Rango Máximo." ToolTip="Debe ingresar el Rango Máximo."
                                                            ValidationGroup="iIndicadorCum" ForeColor="Red">*</asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="revRangoMaxDown" runat="server" ControlToValidate="tbxRangoMaxDown"
                                                            ValidationExpression="^[0-9]+([.,][0-9]{1,3})?$" ForeColor="Red" ErrorMessage="Solo números " ToolTip="Solo números enteros"
                                                            ValidationGroup="iIndicadorCum">*</asp:RegularExpressionValidator>
                                                        
                                                    </td>
                                                    <td class="leftBBBBBB">
                                                        <asp:Label ID="Label38" runat="server" Text="Cumplimiento:" CssClass="Apariencia"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="tbxCumpDown" runat="server" Width="100px" CssClass="Apariencia" MaxLength="20"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tbxCumpDown"
                                                            ErrorMessage="Debe ingresar el Cumplimiento." ToolTip="Debe ingresar el Cumplimiento."
                                                            ValidationGroup="iIndicadorCum" ForeColor="Red">*</asp:RequiredFieldValidator>
                                                         <asp:TextBox ID="tbxIdCumpDown" runat="server" Width="100px" CssClass="Apariencia" MaxLength="5" Visible="false"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr class="centertdtr" id="BotonCumplimiento">
                                        <td>
                                            <asp:ImageButton ID="btnPrevioCump" runat="server" ImageUrl="~/Imagenes/Icons/Button-Previous-icon.png"
                                                OnClick="btnPrevioCump_Click" ToolTip="Anterior" /><asp:ImageButton ID="btnSiguienteCump" runat="server" ImageUrl="~/Imagenes/Icons/Button-Next-icon.png"
                                                    OnClick="btnSiguienteCump_Click" ToolTip="Siguiente" ValidationGroup="iIndicadorCum" /></td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </ajax:TabPanel>

                    </ajax:TabContainer>
                </td>
            </tr>
            <tr id="Botones" align="center" runat="server">
                <td>
                    <table class="tablaSinBordes">
                        <tr>
                            <td>
                                <asp:ImageButton ID="btnImgInsertar" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                    Visible="False" OnClick="btnImgInsertar_Click" ToolTip="Guardar" ValidationGroup="iIndicadorw" />
                            </td>
                            <td>
                                <asp:ImageButton ID="btnImgActualizar" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                    Style="text-align: right" OnClick="btnImgActualizar_Click" ValidationGroup="iIndicadorw"
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
