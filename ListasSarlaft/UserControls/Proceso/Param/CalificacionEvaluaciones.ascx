<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CalificacionEvaluaciones.ascx.cs" Inherits="ListasSarlaft.UserControls.Proceso.CalificacionEvaluaciones" %>
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
                    <asp:Label ID="Label13" runat="server" ForeColor="White" Text="Gestionar Calificación de Evaluaciones" Font-Bold="False"
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
                                        <asp:BoundField HeaderText="Código" DataField="intId" ReadOnly="True" SortExpression="intId" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="no-visible" HeaderStyle-CssClass="no-visible" />
                                        <asp:BoundField HeaderText="Código Conf. Evaluación" DataField="intIdConfiguracionEvaluacion" ItemStyle-HorizontalAlign="Center" Visible="false" />
                                        <asp:BoundField HeaderText="Código Evaluación" DataField="intIdEvaluacion" ItemStyle-HorizontalAlign="Center" Visible="false" />
                                        <asp:TemplateField HeaderText="Nombre Evaluación" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 120px">
                                                    <asp:Label ID="NombreEval" runat="server" Text='<% # Bind("strNombreEvaluacion")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" Width="120" />
                                            <ItemStyle Wrap="false" Width="120" />
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Valor Mínimo" DataField="intValorMinimo" Visible="False" />
                                        <asp:BoundField HeaderText="Valor Máximo" DataField="intValorMaximo" Visible="False" />
                                        <asp:TemplateField HeaderText="Descripción Calificación" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 150px">
                                                    <asp:Label ID="NombreDesc" runat="server" Text='<% # Bind("strDescripcion")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" Width="150" />
                                            <ItemStyle Wrap="false" Width="150" />
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Id. Usuario" DataField="intIdUsuario" Visible="False" />
                                        <asp:BoundField HeaderText="Usuario Creación" DataField="strNombreUsuario" Visible="False" />
                                        <asp:BoundField HeaderText="Fecha de Creación" DataField="dtFechaRegistro" SortExpression="dtFechaRegistro"
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
                                <asp:ImageButton ID="btnNuevo" runat="server" CausesValidation="False" CommandName="Insert"
                                    ImageUrl="~/Imagenes/Icons/Add.png" Text="Insert" OnClick="btnNuevo_Click" ToolTip="Insertar" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr id="filaTituloCalificacion" class="center333399" visible="false" runat="server">
                <td>
                    <asp:Label ID="Label7" runat="server" ForeColor="White" Text="Configurar Calificación" Font-Bold="False" Font-Names="Calibri" Font-Size="Large"></asp:Label>
                </td>
            </tr>
            <tr id="filaDetalleCalificacion" runat="server" class="lefttdtr" visible="false">
                <td style="background: #EEEEEE">
                    <table class="tabla" width="100%">
                        <tr id="CodigoCalificacion">
                            <td class="leftBBBBBB">
                                <asp:Label ID="Label1" runat="server" Text="Código:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="tbxId" runat="server" Enabled="False" Width="100px" CssClass="Apariencia"></asp:TextBox>
                                <asp:TextBox ID="tbxIdConfCalifica" runat="server" Enabled="False" Width="100px" CssClass="Apariencia" Visible="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr id="NombreEvaluacion">
                            <td class="leftBBBBBB">
                                <asp:Label ID="Label6" runat="server" Text="Nombre Evaluación:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlEvaluacion" runat="server" Width="300px" CssClass="Apariencia" DataTextField="NombreEvaluacion" DataValueField="Id">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvEvaluacion" runat="server" ControlToValidate="ddlEvaluacion"
                                    ErrorMessage="Debe ingresar la evaluación." ToolTip="Debe ingresar la evaluación."
                                    ValidationGroup="iCalificacion" ForeColor="Red" InitialValue="0">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr id="Descripcion">
                            <td class="leftBBBBBB">
                                <asp:Label ID="Label3" runat="server" Text="Descripción:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="tbxDescripcion" runat="server" CssClass="Apariencia" Font-Size="10pt" Font-Bold="False" MaxLength="250" Width="504px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvDescripcion" runat="server" ControlToValidate="tbxDescripcion"
                                    ErrorMessage="Debe ingresar la Descripción." ToolTip="Debe ingresar la Descripción."
                                    ValidationGroup="iCalificacion" ForeColor="Red">*</asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator runat="server" ID="revDescripcion" Display="Dynamic" ForeColor="Red"
                                    ControlToValidate="tbxDescripcion" ValidationExpression="^[\s\S]{0,250}$" ValidationGroup="iCalificacion"
                                    ErrorMessage="La longitud máxima es 250 caracteres" ToolTip="La longitud máxima es 250 caracteres">*</asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr id="ValorMinimo">
                            <td class="leftBBBBBB">
                                <asp:Label ID="Label2" runat="server" Text="Valor Mínimo:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="tbxValorMinimo" runat="server" CssClass="Apariencia" Font-Size="10pt" Font-Bold="False" MaxLength="10" Width="100px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvValorMinimo" runat="server" ControlToValidate="tbxValorMinimo"
                                    ErrorMessage="Debe ingresar el Valor Mínimo." ToolTip="Debe ingresar el Valor Mínimo."
                                    ValidationGroup="iCalificacion" ForeColor="Red">*</asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator runat="server" ID="revValorMinimo" Display="Dynamic" ForeColor="Red"
                                    ControlToValidate="tbxValorMinimo" ValidationExpression="^[0-9]+([.,][0-9]{1,3})?$" ValidationGroup="iCalificacion"
                                    ErrorMessage="Ingresar solamente números enteros" ToolTip="Ingresar solamente números enteros">*</asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr id="ValorMaximo">
                            <td class="leftBBBBBB">
                                <asp:Label ID="Label8" runat="server" Text="Valor Máximo:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="tbxValorMaximo" runat="server" CssClass="Apariencia" Font-Size="10pt" Font-Bold="False" MaxLength="10" Width="100px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvValorMaximo" runat="server" ControlToValidate="tbxValorMaximo"
                                    ErrorMessage="Debe ingresar el Valor Máximo." ToolTip="Debe ingresar el Valor Máximo."
                                    ValidationGroup="iCalificacion" ForeColor="Red">*</asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator runat="server" ID="revValorMaximo" Display="Dynamic" ForeColor="Red"
                                    ControlToValidate="tbxValorMaximo" ValidationExpression="^[0-9]+([.,][0-9]{1,3})?$" ValidationGroup="iCalificacion"
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
                                            <asp:ImageButton ID="btnInsertarCal" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                Visible="False" OnClick="btnInsertarCal_Click" ToolTip="Guardar" ValidationGroup="iCalificacion" />
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="btnActualizarCal" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png" Visible="False"
                                                Style="text-align: right" OnClick="btnActualizarCal_Click" ToolTip="Guardar" ValidationGroup="iCalificacion" />
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="btnCancelarCal" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png" Visible="False"
                                                OnClick="btnCancelarCal_Click" ToolTip="Cancelar" />
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
(to actually achieve this we need to add a little piece of code in the Page_Load method of the user control.--%>
<script type="text/javascript">
    function fnClick(sender, e) {
        __doPostBack(sender, e);
    }
</script>


