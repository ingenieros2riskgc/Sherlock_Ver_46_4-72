<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CriterioProveedor.ascx.cs" Inherits="ListasSarlaft.UserControls.Proceso.CriterioProveedor" %>
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

    .center333399
    {
        text-align: center;
        background: #333399;
    }

    .centerEEEEEE
    {
        text-align: center;
        background: #EEEEEE;
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
        <table align="center" width="80%">
            <tr id="trTitulo" class="center333399">
                <td>
                    <asp:Label ID="Label13" runat="server" ForeColor="White" Text="Gestionar criterios evaluación de proveedores" Font-Bold="False"
                        Font-Names="Calibri" Font-Size="Large"></asp:Label>
                </td>
            </tr>
            <tr id="filaGridAspectos" class="centerEEEEEE" runat="server" visible="true">
                <td align="center">
                    <table>
                        <tr>
                            <td>
                                <asp:GridView ID="GridView1" runat="server" CellPadding="4"
                                    ForeColor="#333333" AllowPaging="False" AllowSorting="True" AutoGenerateColumns="False"
                                    DataKeyNames="intId" ShowHeaderWhenEmpty="True" OnRowCommand="GridView1_RowCommand"
                                    HeaderStyle-CssClass="gridViewHeader" BorderStyle="Solid" GridLines="Vertical"
                                    CssClass="Apariencia" Font-Bold="False">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:BoundField DataField="intId" HeaderText="Código" ReadOnly="True" SortExpression="intId" ItemStyle-CssClass="no-visible" HeaderStyle-CssClass="no-visible">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Aspecto " HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 200px">
                                                    <asp:Label ID="NombreAspecto" runat="server" Text='<% # Bind("strAspecto")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" Width="200" />
                                            <ItemStyle Wrap="false" Width="200" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="intValor" HeaderText="Valor" Visible="False" />
                                        <asp:BoundField DataField="intIdUsuario" HeaderText="Región" SortExpression="intIdUsuario" Visible="False" />
                                        <asp:BoundField DataField="strNombreUsuario" HeaderText="Usuario Creación" Visible="False" />
                                        <asp:BoundField DataField="dtFechaRegistro" HeaderText="Fecha de Creación" SortExpression="dtFechaRegistro"
                                            ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                        <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Icons/edit.png" Text="Modificar"
                                            HeaderText="Modificar" CommandName="Modificar" ItemStyle-HorizontalAlign="Center" />
                                        <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Icons/select.png" Text="Criterios"
                                            HeaderText="Criterios" CommandName="Criterios" ItemStyle-HorizontalAlign="Center" />
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
                                <asp:ImageButton ID="btnNuevoAspecto" runat="server" CausesValidation="False" CommandName="Insert"
                                    ImageUrl="~/Imagenes/Icons/Add.png" Text="Insert" OnClick="btnNuevoAspecto_Click" ToolTip="Insertar" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr id="trTituloAspectos" class="center333399" visible="false" runat="server">
                <td>
                    <asp:Label ID="Label7" runat="server" ForeColor="White" Text="Aspectos" Font-Bold="False"
                        Font-Names="Calibri" Font-Size="Large"></asp:Label>
                </td>
            </tr>
            <tr id="filaDetalleAspectos" runat="server" class="lefttdtr" visible="false">
                <td style="background: #EEEEEE">
                    <table class="tabla" width="100%">
                        <tr id="CodigoAspectos">
                            <td class="leftBBBBBB">
                                <asp:Label ID="Label1" runat="server" Text="Código:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="tbxIdAspecto" runat="server" Enabled="False" Width="100px" CssClass="Apariencia"></asp:TextBox>
                            </td>
                        </tr>
                        <tr id="NombreAspecto">
                            <td class="leftBBBBBB">
                                <asp:Label ID="Label5" runat="server" Text="Nombre Aspecto:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtNombreAspecto" runat="server" Width="400px" MaxLength="100" CssClass="Apariencia"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvNombreAspecto" runat="server" ControlToValidate="txtNombreAspecto"
                                    ErrorMessage="Debe ingresar el Nombre del Aspecto." ToolTip="Debe ingresar el Nombre del Aspecto."
                                    ValidationGroup="iAspectos" ForeColor="Red">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr id="ValorAspecto">
                            <td class="leftBBBBBB">
                                <asp:Label ID="Label6" runat="server" Text="Valor:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="tbxValorAspecto" runat="server" Width="100px" MaxLength="7" CssClass="Apariencia"></asp:TextBox>
                                <asp:Label ID="Label25" runat="server" Text=" % (Rango de 0% a 100%)" CssClass="Apariencia"></asp:Label>
                                <asp:RequiredFieldValidator ID="rfvValorAspecto" runat="server" ControlToValidate="tbxValorAspecto"
                                    ErrorMessage="Debe ingresar el Valor del Aspecto." ToolTip="Debe ingresar el Valor del Aspecto."
                                    ValidationGroup="iAspectos" ForeColor="Red">*</asp:RequiredFieldValidator>
                                
                            </td>
                        </tr>
                        <tr id="UsuarioAsp">
                            <td class="leftBBBBBB">
                                <asp:Label ID="Label9" runat="server" Text="Usuario creación:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="tbxUsuarioAspecto" runat="server" Enabled="False" Width="100px" CssClass="Apariencia"></asp:TextBox>
                            </td>
                        </tr>
                        <tr id="FechaAsp">
                            <td class="leftBBBBBB">
                                <asp:Label ID="Label10" runat="server" Text="Fecha creación:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="tbxFechaAspecto" runat="server" Enabled="False" Width="100px" CssClass="Apariencia"></asp:TextBox>
                            </td>
                        </tr>
                        <tr id="BotonesAspectos" align="center">
                            <td colspan="2">
                                <table class="tablaSinBordes">
                                    <tr>
                                        <td>
                                            <asp:ImageButton ID="btnInsertarAspectos" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                Visible="False" OnClick="btnInsertarAspectos_Click" ToolTip="Guardar" ValidationGroup="iAspectos" />
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="btnActualizarAspectos" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                Style="text-align: right" OnClick="btnActualizarAspectos_Click" ToolTip="Guardar" ValidationGroup="iAspectos" />
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="btnCancelarAspectos" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                                OnClick="btnCancelarAspectos_Click" ToolTip="Cancelar" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>

            <tr id="trTituloCriterios" class="center333399" visible="false" runat="server">
                <td>
                    <asp:Label ID="Label8" runat="server" ForeColor="White" Text="Criterios" Font-Bold="False"
                        Font-Names="Calibri" Font-Size="Large"></asp:Label>
                </td>
            </tr>
            <tr id="FilaGridCriterios" class="centerEEEEEE" align="center" runat="server" visible="false">
                <td align="center">
                    <table>
                        <tr class="centerEEEEEE">
                            <td>
                                <asp:GridView ID="GridView2" runat="server" CellPadding="4"
                                    ForeColor="#333333" AllowPaging="False" AllowSorting="True" AutoGenerateColumns="False"
                                    DataKeyNames="intId" ShowHeaderWhenEmpty="True" OnRowCommand="GridView2_RowCommand"
                                    HeaderStyle-CssClass="gridViewHeader" BorderStyle="Solid" GridLines="Vertical"
                                    CssClass="Apariencia" Font-Bold="False">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:BoundField DataField="intId" HeaderText="Código" ReadOnly="True" SortExpression="intId" ItemStyle-HorizontalAlign="Center" />
                                        <asp:TemplateField HeaderText="Descripción" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 200px">
                                                    <asp:Label ID="Descripcion" runat="server" Text='<% # Bind("strDescripcion")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" Width="200" />
                                            <ItemStyle Wrap="false" Width="200" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="intIdAspecto" HeaderText="Ponderacion" Visible="False" />
                                        <asp:BoundField DataField="intIdUsuario" HeaderText="Región" SortExpression="intIdUsuario" Visible="False" />
                                        <asp:BoundField DataField="strNombreUsuario" HeaderText="Usuario Creación" Visible="False" />
                                        <asp:BoundField DataField="dtFechaRegistro" HeaderText="Fecha de Creación" SortExpression="dtFechaRegistro"
                                            ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                        <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Icons/edit.png" Text="Modificar"
                                            HeaderText="Modificar" CommandName="Modificar" ItemStyle-HorizontalAlign="Center" />
                                        <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Icons/select.png" Text="Parámetros"
                                            HeaderText="Parámetros" CommandName="Parametros" ItemStyle-HorizontalAlign="Center" />
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
                                <asp:ImageButton ID="btnNuevoCriterio" runat="server" CausesValidation="False" CommandName="Insert"
                                    ImageUrl="~/Imagenes/Icons/Add.png" Text="Insert" OnClick="btnNuevoCriterio_Click" ToolTip="Insertar" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr id="DetalleCriterios" runat="server" class="lefttdtr" visible="false">
                <td style="background: #EEEEEE">
                    <table class="tabla" width="100%">
                        <tr id="CodigoCriterios">
                            <td class="leftBBBBBB">
                                <asp:Label ID="Label3" runat="server" Text="Código:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="tbxCodigoCriterio" runat="server" Enabled="False" Width="100px" CssClass="Apariencia"></asp:TextBox>
                            </td>
                        </tr>
                        <tr id="DescCriterio">
                            <td class="leftBBBBBB">
                                <asp:Label ID="Label11" runat="server" Text="Descripción:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="tbxDescCriterio" runat="server" Width="400px" CssClass="Apariencia" MaxLength="60"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvDescCriterio" runat="server" ControlToValidate="tbxDescCriterio"
                                    ErrorMessage="Debe ingresar la Descripción del Criterio." ToolTip="Debe ingresar la Descripción del Criterio."
                                    ValidationGroup="iCriterio" ForeColor="Red">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr id="NombreAspectoCriterio">
                            <td class="leftBBBBBB">
                                <asp:Label ID="Label15" runat="server" Text="Nombre Aspecto:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="tbxNombreAspectoCriterio" runat="server" Width="400px" Enabled="False" CssClass="Apariencia"></asp:TextBox>

                            </td>
                        </tr>
                        <tr id="UsuarioCriterio">
                            <td class="leftBBBBBB">
                                <asp:Label ID="Label12" runat="server" Text="Usuario creación:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="tbxUsuarioCriterio" runat="server" Enabled="False" Width="100px" CssClass="Apariencia"></asp:TextBox>
                            </td>
                        </tr>
                        <tr id="FechaCriterio">
                            <td class="leftBBBBBB">
                                <asp:Label ID="Label14" runat="server" Text="Fecha creación:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="tbxFechaCriterio" runat="server" Enabled="False" Width="100px" CssClass="Apariencia"></asp:TextBox>
                            </td>
                        </tr>

                        <tr id="BotonesCriterios" align="center">
                            <td colspan="2">
                                <table class="tablaSinBordes">
                                    <tr>
                                        <td>
                                            <asp:ImageButton ID="btnInsertarCri" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                Visible="False" OnClick="btnInsertarCri_Click" ToolTip="Guardar" ValidationGroup="iCriterio" />
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="btnActualizarCri" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                Style="text-align: right" OnClick="btnActualizarCri_Click" ToolTip="Guardar" ValidationGroup="iCriterio" />
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="btnCancelarCri" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                                OnClick="btnCancelarCri_Click" ToolTip="Cancelar" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr id="BotonRegresarCriterios" runat="server" class="centertdtr" visible="false">
                <td style="background: #EEEEEE">
                    <asp:ImageButton ID="btnRegresarAspectos" runat="server" CausesValidation="False" CommandName="Back"
                        ImageUrl="~/Imagenes/Icons/undo-icon32x32.png" Text="Regresar a Aspectos" OnClick="btnRegresarAspectos_Click" ToolTip="Regresar a Aspectos" />
                </td>
            </tr>

            <tr id="trTituloParametros" class="center333399" visible="false" runat="server">
                <td>
                    <asp:Label ID="Label2" runat="server" ForeColor="White" Text="Parámetros" Font-Bold="False"
                        Font-Names="Calibri" Font-Size="Large"></asp:Label>
                </td>
            </tr>
            <tr id="FilaGridParametros" class="centerEEEEEE" align="center" runat="server" visible="false">
                <td align="center">
                    <table>
                        <tr class="centerEEEEEE">
                            <td>
                                <asp:GridView ID="GridView3" runat="server" CellPadding="4"
                                    ForeColor="#333333" AllowPaging="False" AllowSorting="True" AutoGenerateColumns="False"
                                    DataKeyNames="intId" ShowHeaderWhenEmpty="True" OnRowCommand="GridView3_RowCommand"
                                    HeaderStyle-CssClass="gridViewHeader" BorderStyle="Solid" GridLines="Vertical"
                                    CssClass="Apariencia" Font-Bold="False">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:BoundField DataField="intId" HeaderText="Código" ReadOnly="True" SortExpression="intId" ItemStyle-HorizontalAlign="Center" />
                                        <asp:TemplateField HeaderText="Descripción" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 200px">
                                                    <asp:Label ID="DescripcionParam" runat="server" Text='<% # Bind("strDescripcionParametro")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" Width="200" />
                                            <ItemStyle Wrap="false" Width="200" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="intIdCriterioProveedor" HeaderText="Ponderacion" Visible="False" />
                                        <asp:BoundField DataField="intIdUsuario" HeaderText="Región" SortExpression="intIdUsuario" Visible="False" />
                                        <asp:BoundField DataField="strNombreUsuario" HeaderText="Usuario Creación" Visible="False" />
                                        <asp:BoundField DataField="dtFechaRegistro" HeaderText="Fecha de Creación" SortExpression="dtFechaRegistro"
                                            ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                        <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Icons/edit.png" Text="Modificar"
                                            HeaderText="Modificar" CommandName="Modificar" ItemStyle-HorizontalAlign="Center" />
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
                                <asp:ImageButton ID="btnNuevoParametro" runat="server" CausesValidation="False" CommandName="Insert"
                                    ImageUrl="~/Imagenes/Icons/Add.png" Text="Insert" OnClick="btnNuevoParametro_Click" ToolTip="Insertar" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr id="FilaDetParametros" runat="server" class="lefttdtr" visible="false">
                <td style="background: #EEEEEE">
                    <table class="tabla" width="100%">
                        <tr id="CodigoParametro">
                            <td class="leftBBBBBB">
                                <asp:Label ID="Label4" runat="server" Text="Código:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="tbxIdParametro" runat="server" Enabled="False" Width="100px" CssClass="Apariencia"></asp:TextBox>
                            </td>
                        </tr>
                        <tr id="DescParametro">
                            <td class="leftBBBBBB">
                                <asp:Label ID="Label16" runat="server" Text="Descripción:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="tbxDescParametro" runat="server" Width="400px" MaxLength="200" CssClass="Apariencia"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvDescParametro" runat="server" ControlToValidate="tbxDescParametro"
                                    ErrorMessage="Debe ingresar la descripción del parámetro." ToolTip="Debe ingresar la descripción del parámetro."
                                    ValidationGroup="iParametros" ForeColor="Red">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr id="DescCriterioParam">
                            <td class="leftBBBBBB">
                                <asp:Label ID="Label17" runat="server" Text="Criterio:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="tbxDescCriterioParam" runat="server" Width="400px" CssClass="Apariencia" Enabled="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr id="UsuarioParametro">
                            <td class="leftBBBBBB">
                                <asp:Label ID="Label18" runat="server" Text="Usuario creación:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="tbxUsuarioParametro" runat="server" Enabled="False" Width="100px" CssClass="Apariencia"></asp:TextBox>
                            </td>
                        </tr>
                        <tr id="FechaParametro">
                            <td class="leftBBBBBB">
                                <asp:Label ID="Label19" runat="server" Text="Fecha creación:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="tbxFechaParametro" runat="server" Enabled="False" Width="100px" CssClass="Apariencia"></asp:TextBox>
                            </td>
                        </tr>

                        <tr id="BotonesParametros" align="center">
                            <td colspan="2">
                                <table class="tablaSinBordes">
                                    <tr>
                                        <td>
                                            <asp:ImageButton ID="btnInsertarParametro" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                Visible="False" OnClick="btnInsertarParametro_Click" ToolTip="Guardar" ValidationGroup="iParametros" />
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="btnActualizarParametro" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                Style="text-align: right" OnClick="btnActualizarParametro_Click" ToolTip="Guardar" ValidationGroup="iParametros" />
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="btnCancelarParametro" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                                OnClick="btnCancelarParametro_Click" ToolTip="Cancelar" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr id="BotonRegresarParametros" runat="server" class="centertdtr" visible="false">
                <td style="background: #EEEEEE">
                    <asp:ImageButton ID="btnRegresarCriterios" runat="server" CausesValidation="False" CommandName="Back"
                        ImageUrl="~/Imagenes/Icons/undo-icon32x32.png" Text="Regresar a Criterios" OnClick="btnRegresarCriterios_Click" ToolTip="Regresar a Criterios" />
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
