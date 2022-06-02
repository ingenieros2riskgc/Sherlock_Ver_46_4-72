<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CriterioCompetencias.ascx.cs" Inherits="ListasSarlaft.UserControls.Proceso.CriterioCompetencias" %>
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
                    <asp:Label ID="Label13" runat="server" ForeColor="White" Text="Gestionar criterios evaluación de competencia" Font-Bold="False"
                        Font-Names="Calibri" Font-Size="Large"></asp:Label>
                </td>
            </tr>
            <tr id="filaGridCompetencias" class="centerEEEEEE" runat="server" visible="true">
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
                                        <asp:TemplateField HeaderText="Nombre" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 100px">
                                                    <asp:Label ID="Nombre" runat="server" Text='<% # Bind("strNombre")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" Width="100" />
                                            <ItemStyle Wrap="false" Width="100" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="intPonderacion" HeaderText="Ponderacion" Visible="False" />
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
                                <asp:ImageButton ID="btnInsertarNuevo" runat="server" CausesValidation="False" CommandName="Insert"
                                    ImageUrl="~/Imagenes/Icons/Add.png" Text="Insert" OnClick="btnInsertarNuevo_Click" ToolTip="Insertar" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr id="trTituloCompetencias" class="center333399" visible="false" runat="server">
                <td>
                    <asp:Label ID="Label7" runat="server" ForeColor="White" Text="Competencias" Font-Bold="False"
                        Font-Names="Calibri" Font-Size="Large"></asp:Label>
                </td>
            </tr>
            <tr id="filaDetalleCompetencias" runat="server" class="lefttdtr" visible="false">
                <td style="background: #EEEEEE">
                    <table class="tabla" width="100%">
                        <tr id="CodigoComp">
                            <td class="leftBBBBBB">
                                <asp:Label ID="Label1" runat="server" Text="Código:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="tbxId" runat="server" Enabled="False" Width="100px"
                                    CssClass="Apariencia"></asp:TextBox>
                            </td>
                        </tr>
                        <tr id="NombreComp">
                            <td class="leftBBBBBB">
                                <asp:Label ID="Label6" runat="server" Text="Nombre:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="tbxNombre" runat="server" CssClass="Apariencia" Font-Size="10pt" Font-Bold="False" MaxLength="250" Width="504px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvNombre" runat="server" ControlToValidate="tbxNombre"
                                    ErrorMessage="Debe ingresar el nombre de la competencia." ToolTip="Debe ingresar el nombre de la competencia."
                                    ValidationGroup="iCompetencia" ForeColor="Red">*</asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator runat="server" ID="revNombre" Display="Dynamic" ForeColor="Red"
                                    ControlToValidate="tbxNombre" ValidationExpression="^[\s\S]{0,250}$" ValidationGroup="iCompetencia"
                                    ErrorMessage="La longitud máxima es 250 caracteres" ToolTip="La longitud máxima es 250 caracteres">*</asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr id="Ponderacion">
                            <td class="leftBBBBBB">
                                <asp:Label ID="Label2" runat="server" Text="Ponderación:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="tbxPonderacion" runat="server" CssClass="Apariencia" Font-Size="10pt" Font-Bold="False" MaxLength="10" Width="100px"></asp:TextBox>
                                <asp:Label ID="Label25" runat="server" Text=" % (Rango de 0% a 100%)" CssClass="Apariencia"></asp:Label>
                                <asp:RequiredFieldValidator ID="rfvPonderacion" runat="server" ControlToValidate="tbxPonderacion"
                                    ErrorMessage="Debe ingresar la ponderación de la competencia." ToolTip="Debe ingresar la ponderación de la competencia."
                                    ValidationGroup="iCompetencia" ForeColor="Red">*</asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator runat="server" ID="revPonderacion" Display="Dynamic" ForeColor="Red"
                                    ControlToValidate="tbxPonderacion" ValidationExpression="^[0-9]*$" ValidationGroup="iCompetencia"
                                    ErrorMessage="Ingresar solamente números enteros" ToolTip="Ingresar solamente números enteros">*</asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr id="UsuarioCreacionComp">
                            <td class="leftBBBBBB">
                                <asp:Label ID="Label4" runat="server" Text="Usuario Creación:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="tbxUsuarioCreacion" runat="server" Width="100px" CssClass="Apariencia"
                                    Enabled="False"></asp:TextBox>
                            </td>
                        </tr>
                        <tr id="FechaCreacionComp">
                            <td class="leftBBBBBB">
                                <asp:Label ID="Label5" runat="server" Text="Fecha de Creación:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="tbxFecha" runat="server" Width="100px" CssClass="Apariencia" Enabled="False"></asp:TextBox>
                            </td>
                        </tr>
                        <tr id="BotonesCompetencia" align="center">
                            <td colspan="2">
                                <table class="tablaSinBordes">
                                    <tr>
                                        <td>
                                            <asp:ImageButton ID="btnInsertarComp" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                Visible="False" OnClick="btnInsertarComp_Click" ToolTip="Guardar" ValidationGroup="iCompetencia" />
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="btnActualizarComp" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                Style="text-align: right" OnClick="btnActualizarComp_Click" ToolTip="Guardar" ValidationGroup="iCompetencia" />
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="btnCancelarComp" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                                OnClick="btnCancelarComp_Click" ToolTip="Cancelar" />
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
                    <asp:Label ID="Label8" runat="server" ForeColor="White" Text="Criterios Competencias" Font-Bold="False"
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
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 100px">
                                                    <asp:Label ID="Descripcion" runat="server" Text='<% # Bind("strDescripcion")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" Width="100" />
                                            <ItemStyle Wrap="false" Width="100" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="intIdCompetencia" HeaderText="Ponderacion" Visible="False" />
                                        <asp:TemplateField HeaderText="Competencia" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 100px">
                                                    <asp:Label ID="NombreCompetencia" runat="server" Text='<% # Bind("strNombreCompetencia")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" Width="100" />
                                            <ItemStyle Wrap="false" Width="100" />
                                        </asp:TemplateField>
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
                                <asp:ImageButton ID="btnInsertarNuevoCri" runat="server" CausesValidation="False" CommandName="Insert"
                                    ImageUrl="~/Imagenes/Icons/Add.png" Text="Insert" OnClick="btnInsertarNuevoCri_Click" ToolTip="Insertar" />
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
                        <tr id="DescripcionCriterios">
                            <td class="leftBBBBBB">
                                <asp:Label ID="Label9" runat="server" Text="Descripción:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="tbxDescripcionCri" runat="server" CssClass="Apariencia" Height="42px"
                                    Width="504px" Columns="100" Rows="8" TextMode="MultiLine" Font-Size="10pt" Font-Bold="False" MaxLength="250"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvDescripcion" runat="server" ControlToValidate="tbxDescripcionCri"
                                    ErrorMessage="Debe ingresar la descripción." ToolTip="Debe ingresar la descripción."
                                    ValidationGroup="iCriterio" ForeColor="Red">*</asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator runat="server" ID="revDescLen" Display="Dynamic" ForeColor="Red"
                                    ControlToValidate="tbxDescripcionCri" ValidationExpression="^[\s\S]{0,250}$" ValidationGroup="iCriterio"
                                    ErrorMessage="La longitud máxima es 250 caracteres" ToolTip="La longitud máxima es 250 caracteres">*</asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr id="Competencia">
                            <td class="leftBBBBBB">
                                <asp:Label ID="Label10" runat="server" Text="Competencia:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="tbxCompetenciaCri" runat="server" Enabled="False" Width="100px" CssClass="Apariencia"></asp:TextBox>
                                <asp:TextBox ID="tbxIdCompetenciaCri" runat="server" Enabled="False" Width="100px" CssClass="Apariencia" Visible="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr id="UsuarioCreacionCriterios">
                            <td class="leftBBBBBB">
                                <asp:Label ID="Label11" runat="server" Text="Usuario Creación:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="tbxUsuarioCreacionCri" runat="server" Width="100px" CssClass="Apariencia" Enabled="False"></asp:TextBox>
                            </td>
                        </tr>
                        <tr id="FechaCreacionCriterios">
                            <td class="leftBBBBBB">
                                <asp:Label ID="Label12" runat="server" Text="Fecha de Creación:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="tbxFechaCri" runat="server" Width="100px" CssClass="Apariencia" Enabled="False"></asp:TextBox>
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
            <tr id="BotonRegresar" runat="server" class="centertdtr" visible="false">
                <td style="background: #EEEEEE">
                    <asp:ImageButton ID="btnRegresarCompetencias" runat="server" CausesValidation="False" CommandName="Back"
                        ImageUrl="~/Imagenes/Icons/undo-icon32x32.png" Text="Regresar a Competencias" OnClick="btnRegresarCompetencias_Click" ToolTip="Regresar a Competencias" />
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
