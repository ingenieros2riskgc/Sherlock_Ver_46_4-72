<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CriteriosDesempeno.ascx.cs" Inherits="ListasSarlaft.UserControls.Proceso.CriteriosDesempeno" %>
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
                    <asp:Label ID="Label13" runat="server" ForeColor="White" Text="Gestionar criterios evaluación de desempeño" Font-Bold="False"
                        Font-Names="Calibri" Font-Size="Large"></asp:Label>
                </td>
            </tr>
            <tr id="filaGridDesempeno" class="centerEEEEEE" runat="server" visible="true">
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
                                        <asp:TemplateField HeaderText="Factor Desempeño" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 120px">
                                                    <asp:Label ID="Factor" runat="server" Text='<% # Bind("strFactoresEvaluacion")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" Width="120" />
                                            <ItemStyle Wrap="false" Width="120" />
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Id. Usuario" DataField="intIdUsuario" Visible="False" />
                                        <asp:BoundField HeaderText="Usuario Creación" DataField="strNombreUsuario" Visible="False" />
                                        <asp:BoundField HeaderText="Fecha de Creación" DataField="dtFechaRegistro" SortExpression="dtFechaRegistro"
                                            ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                        <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Icons/edit.png" Text="Modificar"
                                            HeaderText="Modificar" CommandName="Modificar" ItemStyle-HorizontalAlign="Center" />
                                         <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Icons/select.png" Text="Detalles"
                                            HeaderText="Detalles" CommandName="Detalles" ItemStyle-HorizontalAlign="Center" />
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
            <tr id="trTituloDesempeno" class="center333399" visible="false" runat="server">
                <td>
                    <asp:Label ID="Label7" runat="server" ForeColor="White" Text="Factor de Desempeño" Font-Bold="False"
                        Font-Names="Calibri" Font-Size="Large"></asp:Label>
                </td>
            </tr>
            <tr id="filaDetalleDesempeno" runat="server" class="lefttdtr" visible="false">
                <td style="background: #EEEEEE">
                    <table class="tabla" width="100%">
                        <tr id="CodigoFact">
                            <td class="leftBBBBBB">
                                <asp:Label ID="Label1" runat="server" Text="Código:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="tbxId" runat="server" Enabled="False" Width="100px"
                                    CssClass="Apariencia"></asp:TextBox>
                            </td>
                        </tr>
                        <tr id="NombreFact">
                            <td class="leftBBBBBB">
                                <asp:Label ID="Label6" runat="server" Text="Nombre:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="tbxNombre" runat="server" CssClass="Apariencia" Font-Size="10pt" Font-Bold="False" MaxLength="250" Width="504px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvNombre" runat="server" ControlToValidate="tbxNombre"
                                    ErrorMessage="Debe ingresar el nombre del factor de desempeño." ToolTip="Debe ingresar el nombre del factor de desempeño."
                                    ValidationGroup="iFactor" ForeColor="Red">*</asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator runat="server" ID="revNombre" Display="Dynamic" ForeColor="Red"
                                    ControlToValidate="tbxNombre" ValidationExpression="^[\s\S]{0,250}$" ValidationGroup="iFactor"
                                    ErrorMessage="La longitud máxima es 250 caracteres" ToolTip="La longitud máxima es 250 caracteres">*</asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr id="UsuarioCreacionFact">
                            <td class="leftBBBBBB">
                                <asp:Label ID="Label4" runat="server" Text="Usuario Creación:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="tbxUsuarioCreacion" runat="server" Width="100px" CssClass="Apariencia"
                                    Enabled="False"></asp:TextBox>
                            </td>
                        </tr>
                        <tr id="FechaCreacionFact">
                            <td class="leftBBBBBB">
                                <asp:Label ID="Label5" runat="server" Text="Fecha de Creación:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="tbxFecha" runat="server" Width="100px" CssClass="Apariencia" Enabled="False"></asp:TextBox>
                            </td>
                        </tr>
                        <tr id="BotonesFactor" align="center">
                            <td colspan="2">
                                <table class="tablaSinBordes">
                                    <tr>
                                        <td>
                                            <asp:ImageButton ID="btnInsertarFact" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                Visible="False" OnClick="btnInsertarFact_Click" ToolTip="Guardar" ValidationGroup="iFactor" CausesValidation="true" />
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="btnActualizarFact" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                Style="text-align: right" OnClick="btnActualizarFact_Click" ToolTip="Guardar" ValidationGroup="iFactor" CausesValidation="true"/>
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="btnCancelarFact" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                                OnClick="btnCancelarFact_Click" ToolTip="Cancelar" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr id="trTituloDetallesFact" class="center333399" visible="false" runat="server">
                <td>
                    <asp:Label ID="Label8" runat="server" ForeColor="White" Text="Detalle Factor de desempeño" Font-Bold="False"
                        Font-Names="Calibri" Font-Size="Large"></asp:Label>
                </td>
            </tr>
            <tr id="FilaGridDetallesFact" class="centerEEEEEE" align="center" runat="server" visible="false">
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
                                        <asp:TemplateField HeaderText="Descr. Detalle" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 100px">
                                                    <asp:Label ID="Descripcion" runat="server" Text='<% # Bind("strDescripcion")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" Width="100" />
                                            <ItemStyle Wrap="false" Width="100" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="intIdFactoresDesempeno" HeaderText="Código" ReadOnly="True" ItemStyle-HorizontalAlign="Center" Visible="false" />
                                        <asp:TemplateField HeaderText="Nombre Factor" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 100px">
                                                    <asp:Label ID="NombreFactor" runat="server" Text='<% # Bind("strNombreFactor")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" Width="100" />
                                            <ItemStyle Wrap="false" Width="100" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="intIdCalificacion" HeaderText="Id. Calificación" Visible="False" />
                                        <asp:TemplateField HeaderText="Calificación" HeaderStyle-HorizontalAlign="Center" Visible="false">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 100px">
                                                    <asp:Label ID="NombreCalificacion" runat="server" Text='<% # Bind("strNombreCalificacion")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" Width="100" />
                                            <ItemStyle Wrap="false" Width="100" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="intIdUsuario" HeaderText="Región" SortExpression="intIdUsuario" Visible="False" />
                                        <asp:BoundField DataField="strNombreUsuario" HeaderText="Usuario Creación" Visible="False" />
                                        <asp:BoundField DataField="dtFechaRegistro" HeaderText="Fecha de Creación" SortExpression="dtFechaRegistro"
                                            ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="decCriterioMinimo" HeaderText="Criterio Minimo" SortExpression="decCriterioMinimo"
                                            ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="decCriterioMaximo" HeaderText="Criterio Maximo" SortExpression="decCriterioMaximo"
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
                                <asp:ImageButton ID="btnNuevoDetFactor" runat="server" CausesValidation="False" CommandName="Insert"
                                    ImageUrl="~/Imagenes/Icons/Add.png" Text="Insert" OnClick="btnNuevoDetFactor_Click" ToolTip="Insertar" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr id="FilaDetallesFact" runat="server" class="lefttdtr" visible="false">
                <td style="background: #EEEEEE">
                    <table class="tabla" width="100%">
                        <tr id="CodigoDetFactor">
                            <td class="leftBBBBBB">
                                <asp:Label ID="Label3" runat="server" Text="Código:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="tbxCodigoDetFactor" runat="server" Enabled="False" Width="100px" CssClass="Apariencia"></asp:TextBox>
                            </td>
                        </tr>
                        <tr id="DescripcionDetFactor">
                            <td class="leftBBBBBB">
                                <asp:Label ID="Label9" runat="server" Text="Criterio de Evaluación:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="tbxDescripcionDetFactor" CausesValidation="true" runat="server" CssClass="Apariencia" Height="42px"
                                    Width="504px" Columns="100" Rows="8" TextMode="MultiLine" Font-Size="10pt" Font-Bold="False" MaxLength="250"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvDescripcionDetFactor" runat="server" ControlToValidate="tbxDescripcionDetFactor"
                                    ErrorMessage="Debe ingresar el criterio de la evaluacion de desempeño." ToolTip="Debe ingresar el criterio de la evaluacion de desempeño."
                                    ValidationGroup="iDetFactor" ForeColor="Red">*</asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator runat="server" ID="revDescLen" Display="Dynamic" ForeColor="Red"
                                    ControlToValidate="tbxDescripcionDetFactor" ValidationExpression="^[\s\S]{0,250}$" ValidationGroup="iDetFactor"
                                    ErrorMessage="La longitud máxima es 250 caracteres" ToolTip="La longitud máxima es 250 caracteres">*</asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr id="FactorDetFactor">
                            <td class="leftBBBBBB">
                                <asp:Label ID="Label10" runat="server" Text="Nombre Factor:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="tbxNombreFactorDetFactor" runat="server" Enabled="False" Width="450px" CssClass="Apariencia"></asp:TextBox>
                                <asp:TextBox ID="tbxIdFactorDetFactor" runat="server" Enabled="False" Width="20px" CssClass="Apariencia" Visible="false"></asp:TextBox>
                            </td>
                        </tr>
                        <!--<tr id="CalificacionDetFactor">
                            <td class="leftBBBBBB">
                                <asp:Label ID="Label2" runat="server" Text="Calificación:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlCalificacion" runat="server" Width="300px" CssClass="Apariencia"
                                    DataTextField="NombreCalificacion" DataValueField="Id">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvCalificacion" runat="server" ControlToValidate="ddlCalificacion"
                                    ErrorMessage="Debe ingresar la Calificación." ToolTip="Debe ingresar la Calificación."
                                    ValidationGroup="iDetFactor" ForeColor="Red" InitialValue="0">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>-->
                        <tr id="ValorMinimo">
                            <td class="leftBBBBBB">
                                <asp:Label ID="Label14" runat="server" Text="Criterio Mínimo de Evaluación:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox CausesValidation="true" ID="tbxValorMinimo" runat="server" CssClass="Apariencia" Font-Size="10pt" Font-Bold="False" MaxLength="10" Width="100px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvValorMinimo" runat="server" ControlToValidate="tbxValorMinimo"
                                    ErrorMessage="Debe ingresar el Valor Mínimo." ToolTip="Debe ingresar el Valor Mínimo."
                                    ValidationGroup="iDetFactor" ForeColor="Red">*</asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator runat="server" ID="revValorMinimo" Display="Dynamic" ForeColor="Red"
                                    ControlToValidate="tbxValorMinimo" ValidationExpression="^[0-9]+([.,][0-9]{1,3})?$" ValidationGroup="iDetFactor"
                                    ErrorMessage="Ingresar solamente números enteros" ToolTip="Ingresar solamente números enteros">*</asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr id="ValorMaximo">
                            <td class="leftBBBBBB">
                                <asp:Label ID="Label15" runat="server" Text="Criterio Máximo de Evaluación:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="tbxValorMaximo" CausesValidation="true" runat="server" CssClass="Apariencia" Font-Size="10pt" Font-Bold="False" MaxLength="10" Width="100px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvValorMaximo" runat="server" ControlToValidate="tbxValorMaximo"
                                    ErrorMessage="Debe ingresar el Valor Máximo." ToolTip="Debe ingresar el Valor Máximo."
                                    ValidationGroup="iDetFactor" ForeColor="Red">*</asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator runat="server" ID="revValorMaximo" Display="Dynamic" ForeColor="Red"
                                    ControlToValidate="tbxValorMaximo" ValidationExpression="^[0-9]+([.,][0-9]{1,3})?$" ValidationGroup="iDetFactor"
                                    ErrorMessage="Ingresar solamente números enteros" ToolTip="Ingresar solamente números enteros">*</asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr id="UsuarioCreacionDetFactor">
                            <td class="leftBBBBBB">
                                <asp:Label ID="Label11" runat="server" Text="Usuario Creación:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="tbxUsuarioCreacionDetFactor" runat="server" Width="100px" CssClass="Apariencia" Enabled="False"></asp:TextBox>
                            </td>
                        </tr>
                        <tr id="FechaCreacionDetFactor">
                            <td class="leftBBBBBB">
                                <asp:Label ID="Label12" runat="server" Text="Fecha de Creación:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="tbxFechaDetFactor" runat="server" Width="100px" CssClass="Apariencia" Enabled="False"></asp:TextBox>
                            </td>
                        </tr>
                        <tr id="BotonesDetFactor" align="center">
                            <td colspan="2">
                                <table class="tablaSinBordes">
                                    <tr>
                                        <td>
                                            
                                            <asp:ImageButton ID="btnInsertarDetFactor" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                Visible="False" OnClick="btnInsertarDetFactor_Click" ToolTip="Guardar" ValidationGroup="iDetFactor" CausesValidation="true" style="height: 20px" />
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="btnActualizarDetFactor" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                Style="text-align: right" OnClick="btnActualizarDetFactor_Click" ToolTip="Guardar" ValidationGroup="iDetFactor" CausesValidation="true" />
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="btnCancelarDetFactor" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                                OnClick="btnCancelarDetFactor_Click" ToolTip="Cancelar" />
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
                    <asp:ImageButton ID="btnRegresarFactor" runat="server" CausesValidation="False" CommandName="Back"
                        ImageUrl="~/Imagenes/Icons/undo-icon32x32.png" Text="Regresar a Factores de desempeño" OnClick="btnRegresarFactor_Click" ToolTip="Regresar a Factores de desempeño" />
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

