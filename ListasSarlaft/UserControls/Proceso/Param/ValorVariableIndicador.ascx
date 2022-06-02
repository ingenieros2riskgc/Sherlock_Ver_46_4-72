
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ValorVariableIndicador.ascx.cs" Inherits="ListasSarlaft.UserControls.Proceso.ValorVariableIndicador" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControls/Sitio/OkMessageBox.ascx" TagPrefix="uc" TagName="OkMessageBox" %>

<style type="text/css">
    .ajax__html_editor_extender_texteditor {
        background-color: White;
        font-size: small;
        font-weight: lighter;
        font-family: Calibri;
        font-style: normal;
    }

    .gridViewHeader a:link {
        text-decoration: none;
    }

    .style1 {
        width: 100%;
    }

    .popup {
        border: Silver 1px solid;
        color: #060F40;
        background: #ffffff;
    }

    .Apariencia {
    }

    .centerTable {
        margin-left: auto;
        margin-right: auto;
    }

    .centertdtr {
        text-align: center;
    }

    .center333399 {
        text-align: center;
        background: #333399;
    }

    .centerEEEEEE {
        text-align: center;
        background: #EEEEEE;
    }

    .righttdtr {
        text-align: right;
    }

    .right5D7B9D {
        text-align: right;
        background: #5D7B9D;
    }

    .lefttdtr {
        text-align: left;
    }

    .leftBBBBBB {
        text-align: left;
        background: #BBBBBB;
    }

    .Tablewidth {
        width: 100%;
    }

    .TablaEspecial {
        width: 100%;
        border: hidden;
        border: 0;
        vertical-align: middle;
    }

    .centerMiddle {
        text-align: center;
        vertical-align: middle;
    }

    .LeftMiddle {
        text-align: left;
        vertical-align: middle;
    }

    .Toptdtr {
        vertical-align: top;
    }
</style>

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <uc:OkMessageBox ID="omb" runat="server" />
        <asp:Panel ID="pnlMsgBox" runat="server" Width="400px" CssClass="modalPopup" Style="display: none;">
            <table width="100%">
                <tr class="topHandle">
                    <td colspan="2" align="center" runat="server" id="tdCaption">
                        <asp:Label ID="lblCaption" runat="server" Text="Atención" Font-Bold="False" Font-Names="Tahoma"
                            Font-Size="11px"></asp:Label><br />
                    </td>
                </tr>
                <tr>
                    <td  valign="middle" align="center">
                        <asp:Label ID="lblMsgBox" runat="server" Font-Bold="False" Font-Names="Tahoma" Font-Size="11px"
                            Text="Digite el año que desea consultar"
                            ></asp:Label>
                    </td>
                    <td valign="middle" align="left">
                        <asp:TextBox ID="txtPeridoFiltro" runat="server" Width="50px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvPeriodoFiltro" runat="server" ControlToValidate="txtPeridoFiltro"
                                    ErrorMessage="Debe ingresar el año." ToolTip="Debe ingresar el año."
                                    ValidationGroup="pIndicador" ForeColor="Red" InitialValue="">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td align="right" colspan="2">
                        <asp:Button ID="btnImgok" ValidationGroup="pIndicador" runat="server" Text="Ok" OnClick="btnImgok_Click" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:ModalPopupExtender ID="mpeMsgBox" runat="server" PopupControlID="pnlMsgBox"
            BehaviorID="mypopup" Enabled="True" TargetControlID="btndummy" BackgroundCssClass="modalBackground"
            DropShadow="true">
        </asp:ModalPopupExtender>
        <asp:Button ID="btndummy" runat="server" Text="Button" Style="display: none" />
        <table align="center" width="80%">
            <tr id="FilaTituloPpal" class="center333399">
                <td>
                    <asp:Label ID="Label13" runat="server" ForeColor="White" Text="Gestionar valor variable de indicador" Font-Bold="False"
                        Font-Names="Calibri" Font-Size="Large"></asp:Label>
                </td>
            </tr>
            <tr id="FilaProcesos" runat="server">
                <td style="background: #EEEEEE">
                    <table>
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
                            </td>
                        </tr>
                        <tr id="Macroproceso">
                            <td class="leftBBBBBB">
                                <asp:Label ID="Label22" runat="server" Text="Macroproceso:" CssClass="Apariencia"></asp:Label></td>
                            <td>
                                <asp:DropDownList ID="ddlMacroproceso" runat="server" Width="300px"
                                    CssClass="Apariencia" AutoPostBack="True" DataTextField="Nombre" DataValueField="IdMacroproceso"
                                    OnSelectedIndexChanged="ddlMacroproceso_SelectedIndexChanged">
                                    <asp:ListItem Text="" Value="0"></asp:ListItem>
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
                                    ValidationGroup="iIndicador" ForeColor="Red">*</asp:RequiredFieldValidator></td>
                        </tr>
                        <tr id="Subproceso" runat="server" visible="false">
                            <td class="leftBBBBBB">
                                <asp:Label ID="Label8" runat="server" Text="Subproceso:" CssClass="Apariencia"></asp:Label></td>
                            <td>
                                <asp:DropDownList ID="ddlSubproceso" runat="server" Width="300px" OnSelectedIndexChanged="ddlSubproceso_SelectedIndexChanged"
                                    CssClass="Apariencia" AutoPostBack="True" DataTextField="Nombre" DataValueField="IdProceso">
                                </asp:DropDownList><%--<asp:RequiredFieldValidator ID="rfvSubproceso" runat="server" ControlToValidate="ddlSubproceso"
                                    ErrorMessage="Debe ingresar el Subproceso." ToolTip="Debe ingresar el Subproceso." Enabled="False"
                                    ValidationGroup="iIndicador" ForeColor="Red" InitialValue="0">*</asp:RequiredFieldValidator>--%></td>
                        </tr>
                        <tr id="trPeriodoAnual" runat="server" visible="false">
                            <td class="leftBBBBBB">
                                <asp:Label ID="periodo" runat="server" Text="Año:" CssClass="Apariencia"></asp:Label></td>
                            <td>
                                </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr id="FilaBotonBuscar" runat="server">
                <td align="center" style="background: #EEEEEE">
                    <asp:ImageButton ID="btnBuscarIndicador" runat="server" OnClick="btnBuscarIndicador_Click"
                        ImageUrl="~/Imagenes/Icons/Apps-Search-And-Replace-icon.png" ValidationGroup="iIndicador" ToolTip="Buscar" />
                    <asp:ImageButton ID="IBclear" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                        ToolTip="Cancelar" OnClick="IBclear_Click" />
                </td>
            </tr>
            <tr id="FilaTituloIndicador" runat="server" class="center333399">
                <td>
                    <asp:Label ID="Label1" runat="server" ForeColor="White" Text="Indicadores" Font-Bold="False"
                        Font-Names="Calibri" Font-Size="Large"></asp:Label>
                </td>
            </tr>
            <tr id="FilaGridIndicador" style="background: #EEEEEE" runat="server" visible="false">
                <td style="background: #EEEEEE" align="center">
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
                                        <asp:TemplateField HeaderText="Nombre Indicador" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 300px">
                                                    <asp:Label ID="NombreIndicador" runat="server" Text='<% # Bind("strNombreIndicador")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" Width="300" HorizontalAlign="center" />
                                            <ItemStyle Wrap="false" Width="300" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Descripción Indicador" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 300px">
                                                    <asp:Label ID="DescripcionIndicador" runat="server" Text='<% # Bind("strDescripcion")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" Width="300" HorizontalAlign="center" />
                                            <ItemStyle Wrap="false" Width="300" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="intIdPeriodicidad" HeaderText="Id Periodicidad" SortExpression="intIdPeriodicidad" HtmlEncodeFormatString="True" HtmlEncode="False" Visible="False" />
                                        <asp:TemplateField HeaderText="Periodicidad" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 100px">
                                                    <asp:Label ID="NombrePeriodo" runat="server" Text='<% # Bind("strNombrePeriodicidad")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" Width="100" HorizontalAlign="center" />
                                            <ItemStyle Wrap="false" Width="100" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="intMeta" HeaderText="Meta" SortExpression="intMeta" HtmlEncodeFormatString="True" HtmlEncode="False" Visible="False" />
                                        <asp:TemplateField HeaderText="Estado" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" Visible="False">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chbActivo" runat="server" Checked='<% # Bind("booEstado")%>' Enabled="false" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="intIdObjetivoCalidad" HeaderText="Id Objetivo Calidad" SortExpression="intIdObjetivoCalidad" Visible="False" />
                                        <asp:BoundField DataField="strDescObjetivo" HeaderText="Desc. Objetivo" SortExpression="strDescObjetivo" Visible="False" />
                                        <asp:BoundField DataField="intIdProcesoIndicador" HeaderText="Id ProcesoIndicador" SortExpression="intIdProcesoIndicador" Visible="False" />
                                        <asp:BoundField DataField="intIdUsuario" HeaderText="IdUsuario" SortExpression="intIdUsuario" Visible="False" />
                                        <asp:BoundField DataField="strNombreUsuario" HeaderText="Usuario" SortExpression="strNombreUsuario" Visible="False" />
                                        <asp:BoundField DataField="dtFechaRegistro" HeaderText="FechaRegistro" ReadOnly="True" SortExpression="dtFechaRegistro" ItemStyle-HorizontalAlign="Center" Visible="False" />
                                        <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Icons/select.png" Text="Seleccionar" HeaderText="Variables" CommandName="Variables" ItemStyle-HorizontalAlign="Center" />
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
                    </table>
                </td>
            </tr>
            <tr id="BotonRegresarFiltros" runat="server" class="centertdtr" visible="false">
                <td style="background: #EEEEEE">
                    <asp:ImageButton ID="btnRegresarFiltros" runat="server" CausesValidation="False" CommandName="Back"
                        ImageUrl="~/Imagenes/Icons/undo-icon32x32.png" Text="Regresar a Filtros" OnClick="btnRegresarFiltros_Click" ToolTip="Regresar a Filtros" />
                </td>
            </tr>
            <tr id="FilaTituloVariables" runat="server" class="center333399">
                <td>
                    <asp:Label ID="Label2" runat="server" ForeColor="White" Text="Variables" Font-Bold="False"
                        Font-Names="Calibri" Font-Size="Large"></asp:Label>
                </td>
            </tr>
            <tr id="FilaGridVariables" runat="server">
                <td style="background: #EEEEEE" align="center">
                    <table class="tabla" style="width: 100%">
                        <tr>
                            <td align="center">
                                <asp:GridView ID="GridView3" runat="server" CellPadding="4"
                                    ForeColor="#333333" AllowSorting="True" AutoGenerateColumns="false"
                                    ShowHeaderWhenEmpty="True" OnRowCommand="GridView3_RowCommand" BorderStyle="Solid" GridLines="Vertical"
                                    CssClass="Apariencia" Font-Bold="False" AllowPaging="True" OnPageIndexChanging="GridView3_PageIndexChanging">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:BoundField DataField="intId" HeaderText="Código" SortExpression="intId" ItemStyle-HorizontalAlign="Center" />
                                        <asp:TemplateField HeaderText="Descripción">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 200px">
                                                    <asp:Label ID="Descripcion" runat="server" Text='<% # Bind("strDescripcion")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="False" Width="200px" HorizontalAlign="Center" />
                                            <ItemStyle Wrap="False" Width="200px" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="strFormato" HeaderText="Formato" SortExpression="strFormato" ItemStyle-HorizontalAlign="Center" />
                                        <asp:TemplateField HeaderText="Estado" Visible="false">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chbActivo" runat="server" Checked='<% # Bind("booEstado")%>' Enabled="false" />
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="intIdUsuario" HeaderText="IdUsuario" SortExpression="intIdUsuario" Visible="False" />
                                        <asp:BoundField DataField="strNombreUsuario" HeaderText="Usuario Creación" SortExpression="strNombreUsuario" Visible="False" />
                                        <asp:BoundField DataField="intIdDetalleVariable" HeaderText="IdDetalle" ItemStyle-CssClass="no-visible" HeaderStyle-CssClass="no-visible" />
                                        <asp:BoundField DataField="decValor" HeaderText="Valor" />
                                        <asp:BoundField DataField="intIdDetallePeriodo" HeaderText="Id Periodo" Visible="False" />
                                        <asp:BoundField DataField="strNombreDetPeriodo" HeaderText="Periodo" />
                                        <asp:BoundField DataField="strPeriodoAnual" HeaderText="Período Anual" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="intIdIndicador" HeaderText="Id. Indicador" Visible="False" />
                                        <asp:BoundField DataField="dtFechaRegistro" HeaderText="Año de Creación" SortExpression="dtFechaRegistro" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" Visible="false" />
                                        <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Icons/select.png" Text="Asignar" HeaderText="Asignar Valor" CommandName="Asignar" ItemStyle-HorizontalAlign="Center" Visible="false" />
                                        <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Icons/edit.png" Text="Editar" HeaderText="Editar Valor" CommandName="Editar" ItemStyle-HorizontalAlign="Center"/>
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
                                <asp:GridView ID="gvVariablesClean" runat="server" CellPadding="4" Visible="false"
                                    ForeColor="#333333" AllowSorting="True" AutoGenerateColumns="false"
                                    ShowHeaderWhenEmpty="True" OnRowCommand="gvVariablesClean_RowCommand" BorderStyle="Solid" GridLines="Vertical"
                                    CssClass="Apariencia" Font-Bold="False" AllowPaging="True" OnPageIndexChanging="gvVariablesClean_PageIndexChanging">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:BoundField DataField="intId" HeaderText="Código" SortExpression="intId" ItemStyle-HorizontalAlign="Center" />
                                        <asp:TemplateField HeaderText="Descripción">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 200px">
                                                    <asp:Label ID="Descripcion" runat="server" Text='<% # Bind("strDescripcion")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="False" Width="200px" HorizontalAlign="Center" />
                                            <ItemStyle Wrap="False" Width="200px" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="strFormato" HeaderText="Formato" SortExpression="strFormato" ItemStyle-HorizontalAlign="Center" />
                                        <asp:TemplateField HeaderText="Estado" Visible="false">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chbActivo" runat="server" Checked='<% # Bind("booEstado")%>' Enabled="false" />
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="intIdUsuario" HeaderText="IdUsuario" SortExpression="intIdUsuario" Visible="False" />
                                        <asp:BoundField DataField="strNombreUsuario" HeaderText="Usuario Creación" SortExpression="strNombreUsuario" Visible="False" />
                                        <asp:BoundField DataField="intIdDetalleVariable" HeaderText="IdDetalle" ItemStyle-CssClass="no-visible" HeaderStyle-CssClass="no-visible" />
                                        <asp:BoundField DataField="decValor" HeaderText="Valor" />
                                        <asp:BoundField DataField="intIdDetallePeriodo" HeaderText="Id Periodo" Visible="False" />
                                        <asp:BoundField DataField="strNombreDetPeriodo" HeaderText="Periodo" />
                                        <asp:BoundField DataField="strPeriodoAnual" HeaderText="Período Anual" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="intIdIndicador" HeaderText="Id. Indicador" Visible="False" />
                                        <asp:BoundField DataField="dtFechaRegistro" HeaderText="Año de Creación" SortExpression="dtFechaRegistro" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" Visible="false" />
                                        <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Icons/select.png" Text="Asignar" HeaderText="Asignar Valor" CommandName="Asignar" ItemStyle-HorizontalAlign="Center" />
                                        <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Icons/edit.png" Text="Editar" HeaderText="Editar Valor" CommandName="Editar" ItemStyle-HorizontalAlign="Center" Visible="false"/>
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
                        <tr>
                            <td class="righttdtr">
                                <asp:ImageButton ID="btnInsertarNuevo" runat="server" CausesValidation="False" CommandName="Insert"
                                    ImageUrl="~/Imagenes/Icons/Add.png" Text="Insert" OnClick="btnInsertarNuevo_Click" ToolTip="Insertar" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr id="FilaTituloAsignar" runat="server" class="center333399">
                <td>
                    <asp:Label ID="Label3" runat="server" ForeColor="White" Text="Asignar Variables" Font-Bold="False"
                        Font-Names="Calibri" Font-Size="Large"></asp:Label>
                </td>
            </tr>
            <tr id="FilaDetalleAsignar" runat="server">
                <td style="background: #EEEEEE" align="center">
                    <table class="tabla" style="width: 100%; text-align:center" >
                        <tr id="Codigo" runat="server" visible="false">
                            <td class="leftBBBBBB">
                                <asp:Label ID="Label4" runat="server" Text="Código:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td class="lefttdtr">
                                <asp:TextBox ID="tbxId" runat="server" Enabled="False" Width="100px" CssClass="Apariencia"></asp:TextBox>
                                <asp:TextBox ID="tbxIdIndicador" runat="server" Enabled="False" Width="100px" CssClass="Apariencia"></asp:TextBox>
                                <asp:TextBox ID="tbxIdDetalle" runat="server" Enabled="False" Width="100px" CssClass="Apariencia"></asp:TextBox>
                                <asp:TextBox ID="tbxIdPeriodicidad" runat="server" Enabled="False" Width="100px" CssClass="Apariencia"></asp:TextBox>
                            </td>
                        </tr>
                        <tr id="Variable">
                            <td class="leftBBBBBB">
                                <asp:Label ID="Label5" runat="server" Text="Variable:" CssClass="Apariencia"></asp:Label></td>
                            <td class="lefttdtr">
                                <asp:DropDownList ID="ddlVariable" runat="server" CssClass="Apariencia" Width="300px" AutoPostBack="True"
                                    ></asp:DropDownList>
                                <asp:TextBox ID="tbxDescVariable" runat="server" Enabled="False" Width="300px" CssClass="Apariencia" Visible="false"></asp:TextBox></td>
                        </tr>
                        <tr id="VlrVariable" runat="server">
                            <td id="Td3" class="leftBBBBBB" runat="server">
                                <asp:Label ID="Label10" runat="server" Text="Valor:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td id="Td4" runat="server" class="lefttdtr">
                                <asp:TextBox ID="tbxVlrVariable" runat="server" Width="100px" CssClass="Apariencia" MaxLength="60"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvValorVar" runat="server" ControlToValidate="tbxVlrVariable"
                                    ErrorMessage="Debe ingresar el valor de la variable." ToolTip="Debe ingresar el valor de la variable."
                                    ValidationGroup="iValor" ForeColor="Red">*</asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1"
                                    ControlToValidate="tbxVlrVariable" runat="server"
                                    ErrorMessage="Sólo se permiten números."
                                    ValidationExpression="\d+" ValidationGroup="iValor" ForeColor="Red" >

                                </asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr id="PeriodoVariable" runat="server">
                            <td id="Td1" class="leftBBBBBB" runat="server">
                                <asp:Label ID="Label9" runat="server" Text="Periodo" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td id="Td2" runat="server" class="lefttdtr">
                                <asp:DropDownList ID="ddlDetPeriodo" runat="server" Width="300px"
                                    CssClass="Apariencia" AutoPostBack="False" DataTextField="Nombre" DataValueField="Id">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvPeriodoVariable" runat="server" ControlToValidate="ddlDetPeriodo"
                                    ErrorMessage="Debe ingresar el periodo." ToolTip="Debe ingresar el periodo."
                                    ValidationGroup="iValor" ForeColor="Red" InitialValue="0">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>



                        <!-- Cesar -->
                        <tr align="center">
                            <td class="leftBBBBBB" runat="server">
                                <asp:Label ID="PeriodoAnual" runat="server" BackColor="Silver" CssClass="Apariencia" Text="Período anual:" Width="300px"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlPeriodoAnual" runat="server" CssClass="Apariencia" Width="300px" AutoPostBack="True"
                                    OnSelectedIndexChanged="ddlPeriodoAnual_SelectedIndexChanged" Visible="false"></asp:DropDownList>
                                <asp:TextBox ID="txtperiodoanual" runat="server" Width="100px" CssClass="Apariencia" MaxLength="4"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revPeriodonaual" runat="server" 
                                    ControlToValidate="txtperiodoanual" ErrorMessage="Obligatorio solo números" ForeColor="Red" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <!-- Cesar -->



                        <tr id="BotonesVar" runat="server" align="center">
                            <td id="Td9" colspan="2" runat="server">
                                <asp:ImageButton ID="btnGuardarVar" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                    OnClick="btnGuardarVar_Click" ToolTip="Guardar" ValidationGroup="iValor" Visible="False" />
                                      <asp:ImageButton ID="btnActualizarVar" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                    Style="text-align: right" OnClick="btnActualizarVar_Click" ValidationGroup="iValor"
                                    ToolTip="Guardar" Visible="False" />
                                <asp:ImageButton ID="btnCancelarVar" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                    OnClick="btnCancelarVar_Click" ToolTip="Cancelar" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr id="BotonRegresarIndicador" runat="server" class="centertdtr" visible="false">
                <td style="background: #EEEEEE">
                    <asp:ImageButton ID="btnRegresarIndicador" runat="server" CausesValidation="False" CommandName="Back"
                        ImageUrl="~/Imagenes/Icons/undo-icon32x32.png" Text="Regresar a Indicadores" OnClick="btnRegresarIndicador_Click" ToolTip="Regresar a Indicadores" />
                </td>
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>
<%--The fnClick javascript function will force the btnOk button to post back. 
(to actually achieve this we need to add a little piece of code in the Page_Load method of the user control.--%>
<script type="text/javascript">
    function fnClick(sender, e) {
        __doPostBack(sender, e);
    }
</script>