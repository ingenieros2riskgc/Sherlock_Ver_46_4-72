<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ConsultarFormClienteCopidrogas.ascx.cs"
    Inherits="ListasSarlaft.UserControls.ConsultarFormClienteCopidrogas" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
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
                    <asp:Label ID="Label204" runat="server" ForeColor="White" Text="Consultar formulario"
                        Font-Bold="False" Font-Names="Calibri" Font-Size="Large"></asp:Label>
                </td>
            </tr>
            <tr align="left">
                <td>
                    <table>
                        <tr>
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label187" runat="server" Text="Tipo de persona" Font-Names="Calibri"
                                                            Font-Size="Small"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label188" runat="server" Text="Primer apellido" Font-Names="Calibri"
                                                            Font-Size="Small"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label189" runat="server" Text="Segundo apellido" Font-Names="Calibri"
                                                            Font-Size="Small"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label190" runat="server" Text="Nombre" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label191" runat="server" Text="Numero documento" Font-Names="Calibri"
                                                            Font-Size="Small"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label192" runat="server" Text="Año" Font-Names="Calibri" Font-Size="Small" Visible="False"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label193" runat="server" Text="Fecha desde" Font-Names="Calibri" Font-Size="Small" Visible="False"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label194" runat="server" Text="Fecha hasta" Font-Names="Calibri" Font-Size="Small" Visible="False"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label195" runat="server" Text="Rázon social" Visible="False" Font-Names="Calibri"
                                                            Font-Size="Small"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label196" runat="server" Text="NIT" Visible="False" Font-Names="Calibri"
                                                            Font-Size="Small"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:DropDownList ID="DropDownList39" runat="server" Width="105px" AutoPostBack="True"
                                                            Font-Names="Calibri" Font-Size="Small" OnSelectedIndexChanged="DropDownList39_SelectedIndexChanged">
                                                            <asp:ListItem Value="0">Natural</asp:ListItem>
                                                            <asp:ListItem Value="1">Juridica</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TextBox99" runat="server" Width="95px" Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TextBox103" runat="server" Width="105px" Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TextBox105" runat="server" Width="90px" Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TextBox106" runat="server" Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TextBox107" runat="server" Width="100px" Font-Names="Calibri" Font-Size="Small" Visible="False"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TextBox108" runat="server" Width="90px" Font-Names="Calibri" Font-Size="Small" Visible="False"></asp:TextBox>
                                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="true" TargetControlID="TextBox108"
                                                            Format="yyyy-MM-dd"></asp:CalendarExtender>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TextBox109" runat="server" Width="90px" Font-Names="Calibri" Font-Size="Small" Visible="False"></asp:TextBox>
                                                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="true" TargetControlID="TextBox109"
                                                            Format="yyyy-MM-dd"></asp:CalendarExtender>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TextBox110" runat="server" Visible="False" Width="300px" Font-Names="Calibri"
                                                            Font-Size="Small"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TextBox111" runat="server" Visible="False" Width="90px" Font-Names="Calibri"
                                                            Font-Size="Small"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:Button ID="Button4" runat="server" Text="Consultar" OnClick="Button4_Click"
                                                            Font-Names="Calibri" Font-Size="Small" />
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="Button5" runat="server" Text="Limpiar" OnClick="Button5_Click" Font-Names="Calibri"
                                                            Font-Size="Small" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table id="tbConsulta" runat="server" visible="true">
                                    <tr>
                                        <td>
                                            <asp:Button ID="Button7" runat="server" Text="Exportar" OnClick="Button7_Click" Visible="False"
                                                Font-Names="Calibri" Font-Size="Small" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:GridView ID="GridView1" runat="server" CellPadding="4" EnableModelValidation="True"
                                                ForeColor="#333333" GridLines="Vertical" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand"
                                                ShowHeaderWhenEmpty="True" HeaderStyle-CssClass="gridViewHeader" BorderStyle="Solid"
                                                HorizontalAlign="Center" Font-Names="Calibri" Font-Size="Small" AllowPaging="True"
                                                OnPageIndexChanging="GridView1_PageIndexChanging" PageSize="10">
                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                <Columns>
                                                    <asp:BoundField DataField="IdConocimientoCliente" HeaderText="IdConocimientoCliente"
                                                        Visible="False" />
                                                    <asp:BoundField DataField="PrimerApellido" HeaderText="Primer Apellido" />
                                                    <asp:BoundField DataField="SegundoApellido" HeaderText="Segundo Apellido" />
                                                    <asp:BoundField DataField="Nombres" HeaderText="Nombres" />
                                                    <asp:BoundField DataField="TipoDocumento" HeaderText="Tipo Documento" />
                                                    <asp:BoundField DataField="NumeroDocumento" HeaderText="Numero Documento" />
                                                    <asp:BoundField DataField="Ano" HeaderText="Año" />
                                                    <asp:BoundField DataField="FechaRegistro" HeaderText="Fecha Registro" />
                                                    <asp:BoundField DataField="RazonDenominacion" HeaderText="Razon Denominacion" Visible="false" />
                                                    <asp:BoundField DataField="NIT" HeaderText="NIT" Visible="false" />
                                                    <asp:ButtonField ButtonType="Image" Text="Modificar" CommandName="Modificar" ImageUrl="~/Imagenes/Icons/edit.png" />
                                                    <asp:ButtonField ButtonType="Image" Text="Imprimir" CommandName="Imprimir" ImageUrl="~/Imagenes/Icons/print.png" />
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
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr align="left">
                <td>
                    <table id="tbFormulario1" runat="server" visible="false">
                        <tr>
                            <td>
                                <h1 style="font-size: 13px;">
                                    <table id="tbFormulario" border="1px" style="width: 1015px">
                                        <tr>
                                            <td>
                                                <table align="center" width="100%">
                                                    <tr align="center">
                                                        <td>
                                                            <asp:Label ID="Label200" runat="server" Text="SOLICITUD DE INGRESO COMO ASOCIADO"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table align="left" width="100%">
                                                    <tr>
                                                        <td>
                                                            <table>
                                                                <tr align="left">
                                                                    <td>
                                                                        <asp:Label ID="Label1" runat="server" Text="TIPO DE VINCULACIÓN:"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddlTipoVinculacion" runat="server" AutoPostBack="false">
                                                                            <asp:ListItem>---</asp:ListItem>
                                                                            <asp:ListItem>NUEVO</asp:ListItem>
                                                                            <asp:ListItem>ACTUALIZACION</asp:ListItem>
                                                                            <%--
                                                                            <asp:ListItem>PERSONA NATURAL</asp:ListItem>
                                                                            <asp:ListItem>PERSONA JURIDICA</asp:ListItem>
                                                                            <asp:ListItem>REINGRESO</asp:ListItem>
                                                                            <asp:ListItem>TRASLADO</asp:ListItem>
                                                                            <asp:ListItem>SUCESION</asp:ListItem>
                                                                            <asp:ListItem>ACTUALIZACION DE INFORMACION</asp:ListItem>--%>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                                <tr align="left">
                                                                    <td>
                                                                        <asp:Label ID="Label57" runat="server" Text="TIPO DE CLIENTE:"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddlTipoCliente" runat="server" AutoPostBack="false">
                                                                            <asp:ListItem>---</asp:ListItem>
                                                                            <asp:ListItem>ASOCIADO</asp:ListItem>
                                                                            <asp:ListItem>PRE-ASOCIADO</asp:ListItem>
                                                                            <asp:ListItem>EMPLEADO</asp:ListItem>
                                                                            <asp:ListItem>PROVEEDOR</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table width="100%">
                                                    <tr align="center">
                                                        <td style="background-color: #C0C0C0">
                                                            <asp:Label ID="Label201" runat="server" Text="INFORMACION PERSONA NATURAL O REPRESENTANTE LEGAL"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table width="100%">
                                                    <tr>
                                                        <td>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label202" runat="server" Text="PRIMER APELLIDO"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="tbxPerApellido" runat="server" Width="170px" MaxLength="20"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label203" runat="server" Text="SEGUNDO APELLIDO"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="tbxSdoApellido" runat="server" Width="170px" MaxLength="20"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label206" runat="server" Text="NOMBRES"></asp:Label>
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="tbxNombres" runat="server" Width="280px" MaxLength="50"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <table>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="Label207" runat="server" Text="Documento de Identificación:"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="ddlTipoDocumento" runat="server">
                                                                                        <asp:ListItem>---</asp:ListItem>
                                                                                        <asp:ListItem>C.C.</asp:ListItem>
                                                                                        <asp:ListItem>C.E.</asp:ListItem>
                                                                                        <asp:ListItem>R.S.</asp:ListItem>
                                                                                        <asp:ListItem>NUIP</asp:ListItem>
                                                                                        <asp:ListItem>T.I.</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="Label208" runat="server" Text="Número"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tbxNumeroDoc" runat="server" MaxLength="15"></asp:TextBox>
                                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ErrorMessage="Solo números" Font-Bold="false" 
                                                                            Font-Names="Calibri" Font-Size="Small" ForeColor="Red" ControlToValidate="tbxNumeroDoc" 
                                                                            ValidationExpression="^[0-9]*\.?[0-9]+$" ValidationGroup="SaveKnowCliente1"></asp:RegularExpressionValidator>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                    <td>
                                                                        <table>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="Label209" runat="server" Text="Fecha de Expedición"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tbxFechaExp" runat="server" MaxLength="15"></asp:TextBox>
                                                                                    <asp:CalendarExtender ID="CalendarExtender6" runat="server" TargetControlID="tbxFechaExp"
                                                                                        Format="dd-MMM-yyyy"></asp:CalendarExtender>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="Label210" runat="server" Text="Dpto. / Ciudad"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tbxLugarFechaExp" runat="server" MaxLength="20"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                    <td>
                                                                        <table>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="Label211" runat="server" Text="Fecha de Nacimiento"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tbxFechaNmto" runat="server" MaxLength="15"></asp:TextBox>
                                                                                    <asp:CalendarExtender ID="CalendarExtender9" runat="server" TargetControlID="tbxFechaNmto"
                                                                                        Format="dd-MMM-yyyy"></asp:CalendarExtender>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="Label212" runat="server" Text="Dpto. / Ciudad"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tbxLugarFechaNmto" runat="server" MaxLength="20"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table width="100%">
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label213" runat="server" Text="Sexo"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddlSexo" runat="server">
                                                                            <asp:ListItem>---</asp:ListItem>
                                                                            <asp:ListItem>Masculino</asp:ListItem>
                                                                            <asp:ListItem>Femenino</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label214" runat="server" Text="Nacionalidad"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="tbxNacionalidad" runat="server" MaxLength="20" Width="180px"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label215" runat="server" Text="Estado Civil"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddlEstadoCivil" runat="server">
                                                                            <asp:ListItem>---</asp:ListItem>
                                                                            <asp:ListItem>Soltero</asp:ListItem>
                                                                            <asp:ListItem>Casado</asp:ListItem>
                                                                            <asp:ListItem>Union libre</asp:ListItem>
                                                                            <asp:ListItem>Viudo</asp:ListItem>
                                                                            <asp:ListItem>Divorciado</asp:ListItem>
                                                                            <asp:ListItem>Separado</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table width="100%">
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label216" runat="server" Text="Dirección domicilio principal"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="tbxDirDomicilioPpal" runat="server" Width="500px" MaxLength="100"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table width="100%">
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label217" runat="server" Text="Departamento"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="tbxDptoDomPpal" runat="server" MaxLength="20"  Visible="false"></asp:TextBox>
                                                                        <asp:DropDownList ID="DDLDptoDomPpal" runat="server" DataSourceID="SqlDataSourceDepartamentos"
                                                                            DataTextField="NombreDepartamento" DataValueField="IdDepartamento" AutoPostBack="True">
                                                                        </asp:DropDownList>
                                                                        <asp:SqlDataSource ID="SqlDataSourceDepartamentos" runat="server" ConnectionString="<%$ ConnectionStrings:Sherlock_TestConnectionString %>" SelectCommand="SP_VerDepartamentos" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label218" runat="server" Text="Ciudad"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="tbxCiudadDomPpal" runat="server" MaxLength="20" Visible="false"></asp:TextBox>
                                                                        <asp:DropDownList ID="DDLCiudadDomPpal" runat="server" DataSourceID="SqlDataSourceCiudades"
                                                                            DataTextField="NombreCiudad" DataValueField="IdCiudad">
                                                                        </asp:DropDownList>
                                                                        <asp:SqlDataSource ID="SqlDataSourceCiudades" runat="server" ConnectionString="<%$ ConnectionStrings:Sherlock_TestConnectionString %>" SelectCommand="SP_VerCiudades" SelectCommandType="StoredProcedure">
                                                                            <SelectParameters>
                                                                                <asp:ControlParameter ControlID="DDLDptoDomPpal" DefaultValue="1" Name="IdDepartamento"
                                                                                    PropertyName="SelectedValue" Type="Int32" />
                                                                            </SelectParameters>
                                                                        </asp:SqlDataSource>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label219" runat="server" Text="Barrio"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="tbxBarrioDomPpal" runat="server" MaxLength="20"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label220" runat="server" Text="Estrato"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="tbxEstratoDomPpal" runat="server" MaxLength="20" Visible="False"></asp:TextBox>
                                                                        <asp:DropDownList ID="DDLtbxEstratoDomPpal" runat="server">
                                                                <asp:ListItem>---</asp:ListItem>
                                                                <asp:ListItem>1</asp:ListItem>
                                                                <asp:ListItem>2</asp:ListItem>
                                                                <asp:ListItem>3</asp:ListItem>
                                                                <asp:ListItem>4</asp:ListItem>
                                                                <asp:ListItem>5</asp:ListItem>
                                                                <asp:ListItem>6</asp:ListItem>
                                                            </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label221" runat="server" Text="Teléfono"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="tbxTelefonoInfoPer" runat="server" MaxLength="10"></asp:TextBox>
                                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Solo números" Font-Bold="false" 
                                                                            Font-Names="Calibri" Font-Size="Small" ForeColor="Red" ControlToValidate="tbxTelefonoInfoPer" 
                                                                            ValidationExpression="^[0-9]*\.?[0-9]+$" ValidationGroupSave="SaveKnowCliente1"></asp:RegularExpressionValidator>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label222" runat="server" Text="Número de Celular"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="tbxTelCelInfoPer" runat="server" MaxLength="10"></asp:TextBox>
                                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Solo números" Font-Bold="false" 
                                                                            Font-Names="Calibri" Font-Size="Small" ForeColor="Red" ControlToValidate="tbxTelCelInfoPer" 
                                                                            ValidationExpression="^[0-9]*\.?[0-9]+$" ValidationGroup="SaveKnowCliente1"></asp:RegularExpressionValidator>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label223" runat="server" Text="Correo Eléctronico"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="tbxEmailInfoPer" runat="server" MaxLength="150" Width="450px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label224" runat="server" Text="Administra Recursos Públicos?"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddlAdmRecPublicos" runat="server">
                                                                            <asp:ListItem>---</asp:ListItem>
                                                                            <asp:ListItem>SI</asp:ListItem>
                                                                            <asp:ListItem>NO</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label225" runat="server" Text="Ejerce algún grado de poder público?"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddlGrPoderPublico" runat="server">
                                                                            <asp:ListItem>---</asp:ListItem>
                                                                            <asp:ListItem>SI</asp:ListItem>
                                                                            <asp:ListItem>NO</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label226" runat="server" Text="Goza de reconocimiento público general?"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddlReconPubGral" runat="server">
                                                                            <asp:ListItem>---</asp:ListItem>
                                                                            <asp:ListItem>SI</asp:ListItem>
                                                                            <asp:ListItem>NO</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label227" runat="server" Text="ACTIVIDAD ECONÓMICA"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="tbxActEcoInfoPer" runat="server" Width="300px" MaxLength="80"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label228" runat="server" Text="CIIU"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="tbxCIIUInfoPer" runat="server" MaxLength="10"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label229" runat="server" Text="Ingresos Mensuales $"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="tbxIngMenInfoPer" runat="server" Width="330px" MaxLength="100"></asp:TextBox>
                                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" ErrorMessage="Solo números" Font-Bold="false" 
                                                                            Font-Names="Calibri" Font-Size="Small" ForeColor="Red" ControlToValidate="tbxIngMenInfoPer" 
                                                                            ValidationExpression="^[0-9]*\.?[0-9]+$" ValidationGroupSave="SaveKnowCliente1"></asp:RegularExpressionValidator>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label230" runat="server" Text="Total Activos $"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="tbxTotActivosInfoPer" runat="server" Width="330px" MaxLength="100"></asp:TextBox>
                                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server" ErrorMessage="Solo números" Font-Bold="false" 
                                                                            Font-Names="Calibri" Font-Size="Small" ForeColor="Red" ControlToValidate="tbxTotActivosInfoPer" 
                                                                            ValidationExpression="^[0-9]*\.?[0-9]+$" ValidationGroupSave="SaveKnowCliente1"></asp:RegularExpressionValidator>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label231" runat="server" Text="Egresos Mensuales $"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="tbxEgrMenInfoPer" runat="server" Width="330px" MaxLength="100"></asp:TextBox>
                                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator11" runat="server" ErrorMessage="Solo números" Font-Bold="false" 
                                                                            Font-Names="Calibri" Font-Size="Small" ForeColor="Red" ControlToValidate="tbxEgrMenInfoPer" 
                                                                            ValidationExpression="^[0-9]*\.?[0-9]+$" ValidationGroupSave="SaveKnowCliente1"></asp:RegularExpressionValidator>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label232" runat="server" Text="Total Pasivos $"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="tbxTotPasivosInfoPer" runat="server" Width="330px" MaxLength="100"></asp:TextBox>
                                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator12" runat="server" ErrorMessage="Solo números" Font-Bold="false" 
                                                                            Font-Names="Calibri" Font-Size="Small" ForeColor="Red" ControlToValidate="tbxTotPasivosInfoPer" 
                                                                            ValidationExpression="^[0-9]*\.?[0-9]+$" ValidationGroupSave="SaveKnowCliente1"></asp:RegularExpressionValidator>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label233" runat="server" Text="Otros Ingresos Mensuales $"></asp:Label>
                                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator13" runat="server" ErrorMessage="Solo números" Font-Bold="false" 
                                                                            Font-Names="Calibri" Font-Size="Small" ForeColor="Red" ControlToValidate="tbxIngMenInfoPer" 
                                                                            ValidationExpression="^[0-9]*\.?[0-9]+$" ValidationGroupSave="SaveKnowCliente1"></asp:RegularExpressionValidator>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="tbxOtrosIngInfoPer" runat="server" Width="330px" MaxLength="100"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label234" runat="server" Text="Concepto Otros Ingresos:"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="tbxConceptoOtrosIngInfoPer" runat="server" Width="330px" MaxLength="100"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label235" runat="server" Text="Tipo de Inmueble"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddlTipoInmueble" runat="server" OnSelectedIndexChanged="ddlTipoInmueble_SelectedIndexChanged"
                                                                            AutoPostBack="true">
                                                                            <asp:ListItem>---</asp:ListItem>
                                                                            <asp:ListItem>Casa</asp:ListItem>
                                                                            <asp:ListItem>CasaLote</asp:ListItem>
                                                                            <asp:ListItem>Apartamento</asp:ListItem>
                                                                            <asp:ListItem>Habitación</asp:ListItem>
                                                                            <asp:ListItem>Finca</asp:ListItem>
                                                                            <asp:ListItem>Otro</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblOtroTipoInmueble" runat="server" Text="Cual?"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="tbxOtroTipoInmueble" runat="server" Width="330px" MaxLength="100"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label236" runat="server" Text="Tipo de Vivienda"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddlTipoVivienda" runat="server" OnSelectedIndexChanged="ddlTipoVivienda_SelectedIndexChanged"
                                                                            AutoPostBack="true">
                                                                            <asp:ListItem>---</asp:ListItem>
                                                                            <asp:ListItem>Propia</asp:ListItem>
                                                                            <asp:ListItem>Arriendamiento</asp:ListItem>
                                                                            <asp:ListItem>Familiar</asp:ListItem>
                                                                            <asp:ListItem>Herencia</asp:ListItem>
                                                                            <asp:ListItem>Otro</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblOtroTipoVivienda" runat="server" Text="Cual?"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="tbxOtroTipoVivienda" runat="server" Width="330px" MaxLength="100"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label237" runat="server" Text="Si su vivienda es propia, ¿Cuándo la adquirió?"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="tbxViviendaPropia" runat="server" Width="130px" MaxLength="100"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label238" runat="server" Text="¿Posee crédito hipotecario?"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddlCredHipotecario" runat="server" AutoPostBack="true">
                                                                            <asp:ListItem>---</asp:ListItem>
                                                                            <asp:ListItem>NO</asp:ListItem>
                                                                            <asp:ListItem>SI</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label239" runat="server" Text="Valor de la cuota mensual:"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="tbxCuotaMensual" runat="server" Width="130px" MaxLength="100"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label240" runat="server" Text="Entidad financiera:"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="tbxEntidadFinanciera" runat="server" Width="130px" MaxLength="100"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table width="100%">
                                                                <tr align="center">
                                                                    <td style="background-color: #C0C0C0">
                                                                        <asp:Label ID="Label241" runat="server" Text="INFORMACION PERSONA JURÍDICA"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table width="100%">
                                                                <tr>
                                                                    <td>
                                                                        <table>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="Label242" runat="server" Text="RAZÓN O DENOMINACIÓN SOCIAL"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tbxRazonSocial" runat="server" Width="400px" MaxLength="80"></asp:TextBox>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="Label243" runat="server" Text="NIT"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tbxNIT" runat="server" MaxLength="15"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <table>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="Label244" runat="server" Text="Tipo de persona jurídica:"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="ddlTipoPerJuridica" runat="server" OnSelectedIndexChanged="ddlTipoPerJuridica_SelectedIndexChanged"
                                                                                        AutoPostBack="true">
                                                                                        <asp:ListItem>---</asp:ListItem>
                                                                                        <asp:ListItem>Empresa unipersonal</asp:ListItem>
                                                                                        <asp:ListItem>Fundación</asp:ListItem>
                                                                                        <asp:ListItem>Corporacion civil</asp:ListItem>
                                                                                        <asp:ListItem>Cooperativa</asp:ListItem>
                                                                                        <asp:ListItem>Fondo de empleados</asp:ListItem>
                                                                                        <asp:ListItem>Otro</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="lblOtroPerJuridica" runat="server" Text="Cual?"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tbxOtroPerJuridica" runat="server" Visible="false" Width="150px"
                                                                                        MaxLength="140"></asp:TextBox>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="Label245" runat="server" Text="Sociedad comercial:"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="ddlSociedadComercial" runat="server">
                                                                                        <asp:ListItem>---</asp:ListItem>
                                                                                        <asp:ListItem>LTDA</asp:ListItem>
                                                                                        <asp:ListItem>S.A.</asp:ListItem>
                                                                                        <asp:ListItem>SCS</asp:ListItem>
                                                                                        <asp:ListItem>SCA</asp:ListItem>
                                                                                        <asp:ListItem>S.A.S.</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <table>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="Label246" runat="server" Text="ACTIVIDAD ECONÓMICA:"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tbxActivEconomInfoJur" runat="server" Width="190px" MaxLength="80"></asp:TextBox>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="Label247" runat="server" Text="CIIU"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tbxCIIUInfoJur" runat="server" MaxLength="10"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <table>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="Label248" runat="server" Text="Capital o Aporte Social"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tbxCapitalInfoJur" runat="server" Width="180px" MaxLength="150"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <table>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="Label249" runat="server" Text="Fecha de constitución"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tbxFechaConstitucionInfoJur" runat="server" MaxLength="15"></asp:TextBox>
                                                                                    <asp:CalendarExtender ID="CalendarExtender10" runat="server" TargetControlID="tbxFechaConstitucionInfoJur"
                                                                                        Format="dd-MMM-yyyy"></asp:CalendarExtender>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="Label250" runat="server" Text="Documento de constitución"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tbxDocConstitucionInfoJur" runat="server" Width="150px" MaxLength="140"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <table>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="Label197" runat="server" Text="Fecha de registro"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tbxFechaRegInfoJur" runat="server" MaxLength="15"></asp:TextBox>
                                                                                    <asp:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="tbxFechaRegInfoJur"
                                                                                        Format="dd-MMM-yyyy"></asp:CalendarExtender>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="Label198" runat="server" Text="Matrícula mercatil"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tbxMatriMerInfoJur" runat="server" Width="180px" MaxLength="170"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <table>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="Label194F" runat="server" Text="Teléfono"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tbxTelefonoInfoJur" runat="server" MaxLength="10"></asp:TextBox>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="Label196F" runat="server" Text="Correo eléctronico"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tbxEmailInfoJur" runat="server" Width="300px" MaxLength="300"></asp:TextBox>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="Label199" runat="server" Width="130px" Text="Registro SUPERSOLIDARIA(*)"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tbxRegSuperSolInfoJur" runat="server" Width="230px" MaxLength="220"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <table>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="Label52" runat="server" Text="Representante legal principal"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tbxRepLegalPpalInfoJur" runat="server" MaxLength="250" Width="280px"></asp:TextBox>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="Label53" runat="server" Text="Tipo Documento"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="ddlTipoDocRepLegalPpalInfoJur" runat="server">
                                                                                        <asp:ListItem>---</asp:ListItem>
                                                                                        <asp:ListItem>C.C.</asp:ListItem>
                                                                                        <asp:ListItem>C.E.</asp:ListItem>
                                                                                        <asp:ListItem>PAS</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="Label54" runat="server" Text="Número documento"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tbxNroDocRepLegalPpalInfoJur" runat="server" MaxLength="15"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <table>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="Label200F" runat="server" Text="Representante legal"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tbxRepLegal1InfoJur" runat="server" MaxLength="250" Width="280px"></asp:TextBox>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="Label201F" runat="server" Text="Tipo Documento"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="ddlTipoDocRepLegal1InfoJur" runat="server">
                                                                                        <asp:ListItem>---</asp:ListItem>
                                                                                        <asp:ListItem>C.C.</asp:ListItem>
                                                                                        <asp:ListItem>C.E.</asp:ListItem>
                                                                                        <asp:ListItem>PAS</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="Label202F" runat="server" Text="Número documento"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tbxNroDocRepLegal1InfoJur" runat="server" MaxLength="15"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <table>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="Label203F" runat="server" Text="Representante legal"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tbxRepLegal2InfoJur" runat="server" MaxLength="250" Width="280px"></asp:TextBox>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="Label206F" runat="server" Text="Tipo Documento"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="ddlTipoDocRepLegal2InfoJur" runat="server">
                                                                                        <asp:ListItem>---</asp:ListItem>
                                                                                        <asp:ListItem>C.C.</asp:ListItem>
                                                                                        <asp:ListItem>C.E.</asp:ListItem>
                                                                                        <asp:ListItem>PAS</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="Label207F" runat="server" Text="Número documento"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tbxNroDocRepLegal2InfoJur" runat="server" MaxLength="15"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <table>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="Label208F" runat="server" Text="Representante legal"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tbxRepLegal3InfoJur" runat="server" MaxLength="250" Width="280px"></asp:TextBox>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="Label209F" runat="server" Text="Tipo Documento"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="ddlTipoDocRepLegal3InfoJur" runat="server">
                                                                                        <asp:ListItem>---</asp:ListItem>
                                                                                        <asp:ListItem>C.C.</asp:ListItem>
                                                                                        <asp:ListItem>C.E.</asp:ListItem>
                                                                                        <asp:ListItem>PAS</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="Label210F" runat="server" Text="Número documento"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tbxNroDocRepLegal3InfoJur" runat="server" MaxLength="15"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <table>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="Label211F" runat="server" Text="Representante legal"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tbxRepLegal4InfoJur" runat="server" MaxLength="250" Width="280px"></asp:TextBox>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="Label212F" runat="server" Text="Tipo Documento"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="ddlTipoDocRepLegal4InfoJur" runat="server">
                                                                                        <asp:ListItem>---</asp:ListItem>
                                                                                        <asp:ListItem>C.C.</asp:ListItem>
                                                                                        <asp:ListItem>C.E.</asp:ListItem>
                                                                                        <asp:ListItem>PAS</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="Label213F" runat="server" Text="Número documento"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tbxNroDocRepLegal4InfoJur" runat="server" MaxLength="15"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <table>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="Label55" runat="server" Text="Teléfono fijo / Número Celular"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tbxTelefonoRepLegalInfoJur" runat="server" MaxLength="50"></asp:TextBox>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="Label214F" runat="server" Text="Departamento / Ciudad / Municipio"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tbxDepCiuMunRepLegalInfoJur" runat="server" MaxLength="100"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <table>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="Label56" runat="server" Text="Representante legal suplente"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tbxRepLegalSuplInfoJur" runat="server" MaxLength="250" Width="280px"></asp:TextBox>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="Label215F" runat="server" Text="Tipo Documento"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="ddlTipoDocRepLegalSuplInfoJur" runat="server">
                                                                                        <asp:ListItem>---</asp:ListItem>
                                                                                        <asp:ListItem>C.C.</asp:ListItem>
                                                                                        <asp:ListItem>C.E.</asp:ListItem>
                                                                                        <asp:ListItem>PAS</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="Label216F" runat="server" Text="Número documento"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tbxNroDocRepLegalSuplInfoJur" runat="server" MaxLength="15"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <table>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="Label217F" runat="server" Text="Teléfono fijo / Número Celular"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tbxTelefonoRepLegalSuplInfoJur" runat="server" MaxLength="50"></asp:TextBox>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="Label218F" runat="server" Text="Departamento / Ciudad / Municipio"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tbxDepCiuMunRepLegalSuplInfoJur" runat="server" MaxLength="100"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <table>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="Label58" runat="server" Text="Tipo Empresa"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="ddlTipoEmpresaInfoJur" runat="server">
                                                                                        <asp:ListItem>---</asp:ListItem>
                                                                                        <asp:ListItem>Pública</asp:ListItem>
                                                                                        <asp:ListItem>Privada</asp:ListItem>
                                                                                        <asp:ListItem>Mixta</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="Label219F" runat="server" Text="Descripcion actividad económica principal"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tbxActEconPpalInfoJur" runat="server" MaxLength="300" Width="300px"></asp:TextBox>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="Label220F" runat="server" Text="CIIU"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tbxCIIUEmpInfoJur" runat="server" MaxLength="10"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr style="background-color: #DDDDDD">
                                                                    <td>
                                                                        <table>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="Label71" runat="server" Text="IDENTIFICACION DE LOS ACCIONISTAS O ASOCIADOS QUE TENGAN DIRECTA O INDIRECTAMENTE MAS DEL 5% DEL CAPITAL SOCIAL, APORTE O PARTICIPACION (EN CASO DE REQUERIR MAS ESPACIO DEBE ANEXARSE LA RELACION):"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <table>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="Label72" runat="server" Text="RAZON SOCIAL O NOMBRE COMPLETO"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tbxRazonSocialAccionistas1" runat="server" Width="230px" MaxLength="80"></asp:TextBox>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="Label73" runat="server" Text="TIPO DE IDENTIFICACIÓN"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="ddlTipoDocRazonSocialAccionistas1" runat="server">
                                                                                        <asp:ListItem>---</asp:ListItem>
                                                                                        <asp:ListItem>C.C.</asp:ListItem>
                                                                                        <asp:ListItem>C.E.</asp:ListItem>
                                                                                        <asp:ListItem>T.I.</asp:ListItem>
                                                                                        <asp:ListItem>NIT</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="Label74" runat="server" Text="NÚMERO"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tbxNroDocRazonSocialAccionistas1" runat="server" MaxLength="15"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="Label75" runat="server" Text="RAZON SOCIAL O NOMBRE COMPLETO"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tbxRazonSocialAccionistas2" runat="server" Width="230px" MaxLength="80"></asp:TextBox>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="Label76" runat="server" Text="TIPO DE IDENTIFICACIÓN"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="ddlTipoDocRazonSocialAccionistas2" runat="server">
                                                                                        <asp:ListItem>---</asp:ListItem>
                                                                                        <asp:ListItem>C.C.</asp:ListItem>
                                                                                        <asp:ListItem>C.E.</asp:ListItem>
                                                                                        <asp:ListItem>T.I.</asp:ListItem>
                                                                                        <asp:ListItem>NIT</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="Label77" runat="server" Text="NÚMERO"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tbxNroDocRazonSocialAccionistas2" runat="server" MaxLength="15"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="Label78" runat="server" Text="RAZON SOCIAL O NOMBRE COMPLETO"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tbxRazonSocialAccionistas3" runat="server" Width="230px" MaxLength="80"></asp:TextBox>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="Label79" runat="server" Text="TIPO DE IDENTIFICACIÓN"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="ddlTipoDocRazonSocialAccionistas3" runat="server">
                                                                                        <asp:ListItem>---</asp:ListItem>
                                                                                        <asp:ListItem>C.C.</asp:ListItem>
                                                                                        <asp:ListItem>C.E.</asp:ListItem>
                                                                                        <asp:ListItem>T.I.</asp:ListItem>
                                                                                        <asp:ListItem>NIT</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="Label80" runat="server" Text="NÚMERO"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tbxNroDocRazonSocialAccionistas3" runat="server" MaxLength="15"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="Label81" runat="server" Text="RAZON SOCIAL O NOMBRE COMPLETO"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tbxRazonSocialAccionistas4" runat="server" Width="230px" MaxLength="80"></asp:TextBox>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="Label82" runat="server" Text="TIPO DE IDENTIFICACIÓN"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="ddlTipoDocRazonSocialAccionistas4" runat="server">
                                                                                        <asp:ListItem>---</asp:ListItem>
                                                                                        <asp:ListItem>C.C.</asp:ListItem>
                                                                                        <asp:ListItem>C.E.</asp:ListItem>
                                                                                        <asp:ListItem>T.I.</asp:ListItem>
                                                                                        <asp:ListItem>NIT</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="Label83" runat="server" Text="NÚMERO"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tbxNroDocRazonSocialAccionistas4" runat="server" MaxLength="15"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="Label84" runat="server" Text="RAZON SOCIAL O NOMBRE COMPLETO"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tbxRazonSocialAccionistas5" runat="server" Width="230px" MaxLength="80"></asp:TextBox>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="Label85" runat="server" Text="TIPO DE IDENTIFICACIÓN"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="ddlTipoDocRazonSocialAccionistas5" runat="server">
                                                                                        <asp:ListItem>---</asp:ListItem>
                                                                                        <asp:ListItem>C.C.</asp:ListItem>
                                                                                        <asp:ListItem>C.E.</asp:ListItem>
                                                                                        <asp:ListItem>T.I.</asp:ListItem>
                                                                                        <asp:ListItem>NIT</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="Label86" runat="server" Text="NÚMERO"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tbxNroDocRazonSocialAccionistas5" runat="server" MaxLength="15"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <table>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="Label87" runat="server" Text="Ingresos Mensuales $"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tbxIngMenAccionistas" runat="server" Width="330px" MaxLength="100"></asp:TextBox>
                                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="Solo números" Font-Bold="false" 
                                                                            Font-Names="Calibri" Font-Size="Small" ForeColor="Red" ControlToValidate="tbxIngMenAccionistas" 
                                                                            ValidationExpression="^[0-9]*\.?[0-9]+$" ValidationGroup="SaveKnowCliente1"></asp:RegularExpressionValidator>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="Label88" runat="server" Text="Total Activos $"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tbxTotActivosAccionistas" runat="server" Width="330px" MaxLength="100"></asp:TextBox>
                                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ErrorMessage="Solo números" Font-Bold="false" 
                                                                            Font-Names="Calibri" Font-Size="Small" ForeColor="Red" ControlToValidate="tbxTotActivosAccionistas" 
                                                                            ValidationExpression="^[0-9]*\.?[0-9]+$" ValidationGroup="SaveKnowCliente1"></asp:RegularExpressionValidator>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="Label89" runat="server" Text="Egresos Mensuales $"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tbxEgrMenAccionistas" runat="server" Width="330px" MaxLength="100"></asp:TextBox>
                                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ErrorMessage="Solo números" Font-Bold="false" 
                                                                            Font-Names="Calibri" Font-Size="Small" ForeColor="Red" ControlToValidate="tbxEgrMenAccionistas" 
                                                                            ValidationExpression="^[0-9]*\.?[0-9]+$" ValidationGroup="SaveKnowCliente1"></asp:RegularExpressionValidator>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="Label90" runat="server" Text="Total Pasivos $"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tbxTotPasivosAccionistas" runat="server" Width="330px" MaxLength="100"></asp:TextBox>
                                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ErrorMessage="Solo números" Font-Bold="false" 
                                                                            Font-Names="Calibri" Font-Size="Small" ForeColor="Red" ControlToValidate="tbxTotPasivosAccionistas" 
                                                                            ValidationExpression="^[0-9]*\.?[0-9]+$" ValidationGroup="SaveKnowCliente1"></asp:RegularExpressionValidator>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="Label91" runat="server" Text="Otros Ingresos Mensuales $"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tbxOtrosIngMenAccionistas" runat="server" Width="330px" MaxLength="100"></asp:TextBox>
                                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ErrorMessage="Solo números" Font-Bold="false" 
                                                                            Font-Names="Calibri" Font-Size="Small" ForeColor="Red" ControlToValidate="tbxOtrosIngMenAccionistas" 
                                                                            ValidationExpression="^[0-9]*\.?[0-9]+$" ValidationGroup="SaveKnowCliente1"></asp:RegularExpressionValidator>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="Label92" runat="server" Text="Concepto Otros Ingresos:"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tbxConceptOtrosIngMenAccionistas" runat="server" Width="330px" MaxLength="200"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table width="100%">
                                                                <tr>
                                                                    <td>
                                                                        <table>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="Label94" runat="server" Text="Realiza operaciones en moneda extranjera?"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="ddlOpMonedaExt" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlOpMonedaExt_SelectedIndexChanged">
                                                                                        <asp:ListItem>---</asp:ListItem>
                                                                                        <asp:ListItem>NO</asp:ListItem>
                                                                                        <asp:ListItem>SI</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="lblCualOpMonedaExt" runat="server" Text="Cuales:" Visible="False"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tbxCualOpMonedaExt" runat="server" Visible="False" MaxLength="300"
                                                                                        Width="300"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label36" runat="server" Text="Posee cuentas en moneda extranjera?"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddlCuentasMonedaExt" runat="server">
                                                                            <asp:ListItem>---</asp:ListItem>
                                                                            <asp:ListItem>NO</asp:ListItem>
                                                                            <asp:ListItem>SI</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblNroCuentaMonedaExt" runat="server" Text="No. Cuenta"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="tbxNroCuentaMonedaExt" runat="server" MaxLength="30"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label99" runat="server" Text="Banco"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="tbxBancoMonedaExt" runat="server" MaxLength="30"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label101" runat="server" Text="Ciudad"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="tbxCiudadMonedaExt" runat="server" MaxLength="30"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label102" runat="server" Text="País"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="tbxPaisMonedaExt" runat="server" MaxLength="30"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label100" runat="server" Text="Moneda"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="tbxMonMonedaExt" runat="server" MaxLength="30"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label93" runat="server" Text="* Espacio Exclusivo para las entidades de economía solidaria."></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table width="100%">
                                                                <tr align="center">
                                                                    <td style="background-color: #C0C0C0">
                                                                        <asp:Label ID="Label35" runat="server" Text="IDENTIFICACION DE LAS DROGUERIAS PROPIEDAD DEL ASPIRANTE"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label24" runat="server" Text="NOMBRE DE LA DROGUERIA PRINCIPAL:"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="tbxNombreDrogueriaPpal" runat="server" Width="300px" MaxLength="80"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label25" runat="server" Text="NIT"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="tbxNITDrogueriaPpal" runat="server" Width="160px" MaxLength="20"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table width="100%">
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label28" runat="server" Text="Dirección:"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="tbxDirDrogueriaPpal" runat="server" Width="300px" MaxLength="80"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label29" runat="server" Text="Dpto:"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="tbxDptoDrogueriaPpal" runat="server" Width="160px" MaxLength="20" Visible="False"></asp:TextBox>
                                                                        <asp:DropDownList ID="DDLDptoDrogueriaPpal" runat="server" DataSourceID="SqlDataSourceDepartamentos"
                                                                            DataTextField="NombreDepartamento" DataValueField="IdDepartamento" AutoPostBack="True">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table width="100%">
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label30" runat="server" Text="Ciudad"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="tbxCiudadDrogueriaPpal" runat="server" Width="200px" MaxLength="20" Visible="False"></asp:TextBox>
                                                                        <asp:DropDownList ID="DDLCiudadDrogueriaPpal" runat="server" DataSourceID="SqlDataSourceCiudades1"
                                                                            DataTextField="NombreCiudad" DataValueField="IdCiudad">
                                                                        </asp:DropDownList>
                                                                        <asp:SqlDataSource ID="SqlDataSourceCiudades1" runat="server" ConnectionString="<%$ ConnectionStrings:Sherlock_TestConnectionString %>" SelectCommand="SP_VerCiudades" SelectCommandType="StoredProcedure">
                                                                            <SelectParameters>
                                                                                <asp:ControlParameter ControlID="DDLDptoDrogueriaPpal" DefaultValue="1" Name="IdDepartamento"
                                                                                    PropertyName="SelectedValue" Type="Int32" />
                                                                            </SelectParameters>
                                                                        </asp:SqlDataSource>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label95" runat="server" Text="Barrio"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="tbxBarrioDrogueriaPpal" runat="server" Width="200px" MaxLength="20"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label96" runat="server" Text="Teléfono"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="tbxTelefonoDrogueriaPpal" runat="server" Width="200px" MaxLength="20"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label31" runat="server" Text="NOMBRE DE LA DROGUERIA 2:"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="tbxNombreDrogueria2" runat="server" Width="300px" MaxLength="80"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table width="100%">
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label32" runat="server" Text="Dirección:"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="tbxDirDrogueria2" runat="server" Width="300px" MaxLength="80"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label33" runat="server" Text="Dpto:"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="tbxDptoDrogueria2" runat="server" Width="160px" MaxLength="20" Visible="False"></asp:TextBox>
                                                                        <asp:DropDownList ID="DDLDptoDrogueria2" runat="server" DataSourceID="SqlDataSourceDepartamentos"
                                                                            DataTextField="NombreDepartamento" DataValueField="IdDepartamento" AutoPostBack="True">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table width="100%">
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label34" runat="server" Text="Ciudad"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="tbxCiudadDrogueria2" runat="server" Width="200px" MaxLength="20" Visible="False"></asp:TextBox>
                                                                        <asp:DropDownList ID="DDLCiudadDrogueria2" runat="server" DataSourceID="SqlDataSourceCiudades2"
                                                                            DataTextField="NombreCiudad" DataValueField="IdCiudad">
                                                                        </asp:DropDownList>
                                                                        <asp:SqlDataSource ID="SqlDataSourceCiudades2" runat="server" ConnectionString="<%$ ConnectionStrings:Sherlock_TestConnectionString %>" SelectCommand="SP_VerCiudades" SelectCommandType="StoredProcedure">
                                                                            <SelectParameters>
                                                                                <asp:ControlParameter ControlID="DDLDptoDrogueria2" DefaultValue="1" Name="IdDepartamento"
                                                                                    PropertyName="SelectedValue" Type="Int32" />
                                                                            </SelectParameters>
                                                                        </asp:SqlDataSource>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label98" runat="server" Text="Barrio"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="tbxBarrioDrogueria2" runat="server" Width="200px" MaxLength="20"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label103" runat="server" Text="Teléfono"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="tbxTelefonoDrogueria2" runat="server" Width="200px" MaxLength="20"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label104" runat="server" Text="NOMBRE DE LA DROGUERIA 3:"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="tbxNombreDrogueria3" runat="server" Width="300px" MaxLength="80"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table width="100%">
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label221F" runat="server" Text="Dirección:"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="tbxDirDrogueria3" runat="server" Width="300px" MaxLength="80"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label222F" runat="server" Text="Dpto:"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="tbxDptoDrogueria3" runat="server" Width="160px" MaxLength="20" Visible="False"></asp:TextBox>
                                                                        <asp:DropDownList ID="DDLDptoDrogueria3" runat="server" DataSourceID="SqlDataSourceDepartamentos"
                                                                            DataTextField="NombreDepartamento" DataValueField="IdDepartamento" AutoPostBack="True">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table width="100%">
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label223F" runat="server" Text="Ciudad"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="tbxCiudadDrogueria3" runat="server" Width="200px" MaxLength="20" Visible="False"></asp:TextBox>
                                                                        <asp:DropDownList ID="DDLCiudadDrogueria3" runat="server" DataSourceID="SqlDataSourceCiudades3"
                                                                            DataTextField="NombreCiudad" DataValueField="IdCiudad">
                                                                        </asp:DropDownList>
                                                                        <asp:SqlDataSource ID="SqlDataSourceCiudades3" runat="server" ConnectionString="<%$ ConnectionStrings:Sherlock_TestConnectionString %>" SelectCommand="SP_VerCiudades" SelectCommandType="StoredProcedure">
                                                                            <SelectParameters>
                                                                                <asp:ControlParameter ControlID="DDLDptoDrogueria3" DefaultValue="1" Name="IdDepartamento"
                                                                                    PropertyName="SelectedValue" Type="Int32" />
                                                                            </SelectParameters>
                                                                        </asp:SqlDataSource>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label224F" runat="server" Text="Barrio"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="tbxBarrioDrogueria3" runat="server" Width="200px" MaxLength="20"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label225F" runat="server" Text="Teléfono"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="tbxTelefonoDrogueria3" runat="server" Width="200px" MaxLength="20"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="6">
                                                                        <asp:Label ID="Label226F" runat="server" Text="En caso de no ser suficiente el espacio, favor anexar Hoja con la misma informacion aqui solicitada."></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table width="100%">
                                                                <tr align="center">
                                                                    <td style="background-color: #C0C0C0">
                                                                        <asp:Label ID="Label42" runat="server" Text="AUTORIZACION"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label227F" runat="server" Text="Autorizo a que se me desvincule de la cooperativa y se me retire del registro social de esta, en el caso de infracción de cualquiera de los numerales contenidos en este documento, eximiendo a la entidad de toda responsabilidad que se derive por información errónea, falsa o inexacta que yo hubiere proporcionado en este documento o de la violación del mismo."></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table width="100%">
                                                                <tr align="center">
                                                                    <td style="background-color: #C0C0C0">
                                                                        <asp:Label ID="Label228F" runat="server" Text="COMPORTAMIENTO ÉTICO Y MANEJO ADECUADO DE LA DROGUERÍA"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label229F" runat="server" Text="Declaro conocer que el incumplimiento a una de las disposiciones contenidas en el codigo de etica y conducta de la cooperativa social y comercial que la cooperativa tiene establecidos, es causal de exclusion o de suspension total de derechos. Me comprometo tambien a mantener mi(s) drogueria(s) con una adecuada, correcta e higienica presentacion y atender todos los requerimientos legales para el funcionamiento de la misma. Para facilitar la comprobacion del correcto cumplimiento de lo expuesto faculto desde ya y de forma permanente a COPIDROGAS para que practique visitas de inspeccion y vigilancia a mi(s) establecimiento(s), donde podran examinar con detenimiento el inventario del negocio, las facturas de adquisicion de los medicamentos y mercancias, el servicio de inyectologia si lo poseo y todas las condiciones fisicas de las instalaciones internas, asi como de los demas documentos y comprobantes de la operacion."></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table width="100%">
                                                                <tr align="center">
                                                                    <td style="background-color: #C0C0C0">
                                                                        <asp:Label ID="Label230F" runat="server" Text="SOMETIMIENTO AL ESTATUTO Y LOS REGLAMENTOS"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label231F" runat="server" Text="Me comprometo a dar estricto cumplimiento a las normas establecidas en los Estatutos y Reglamentos definidos por COPIDROGAS, realizando especial atencion a mis derechos deberes y sanciones relativas al cumplimiento del mismo. Manifiesto conocer los deberes y derechos de los asociados y en consecuencia me obligo a cumplir estrictamente los Estatutos, Codigo de Etica y demas reglamentos de la cooperativa, asi mismo a suscribir y pagar los aportes sociales individuales, obligaciones economicas consagradas en el  Estatuto, Reglamentos y determinaciones de la Asamble General y el Consejo de Administracion. Igualmente me comprometo a recibir la formacion basica Cooperativa en los terminos y condiciones que programe la cooperativa.  Desde ya me comprometo a responder y pagarle a la coopertiva los perjucios economicos y morales que se puedan derivar del deprestigio que sufra COPIDROGAS a sus asociados por causa o con ocasión de hechos irregulares, delictuosos, o por comportamientos antieticos e inapropiados que se puedan llegar a presentar en mi(s) drogueria(s)."></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table>
                                                                <tr>
                                                                    <td class="style4">
                                                                        <asp:Label ID="Label59" runat="server" Text="Aportes sociales por pagar inicialmente $"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="tbxAportesXPagar" runat="server" Width="80px" MaxLength="10"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label60" runat="server" Text="Valor apertura de cuenta de cuenta Copicredito $"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="tbxVlrAperturaCtaCopicredito" runat="server" Width="80px" MaxLength="10"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label61" runat="server" Text="Compromiso de ahorro mensual $"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="tbxCompromisoAhorroMen" runat="server" Width="80px" MaxLength="10"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label62" runat="server" Text="Cuota de admisión $"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="tbxCuotaAdmision" runat="server" Width="80px" MaxLength="10"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Lbel57" runat="server" Text="Cuota de afiliación asocoldro $"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="tbxCuotaAfilAsocoldro" runat="server" Width="80px" MaxLength="10"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label232F" runat="server" Text="TOTAL $"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="tbxTotalCompromisos" runat="server" Width="80px" MaxLength="10"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label146" runat="server" Text="Atentamente"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td></td>
                                                    </tr>
                                                    <tr>
                                                        <td></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label63" runat="server" Text="________________________"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label64" runat="server" Text="Firma del aspirante"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td align="center" style="width: 20%;">
                                                            <asp:Panel ID="Panel2" runat="server" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"
                                                                Height="93px" Width="92px">
                                                            </asp:Panel>
                                                        </td>
                                                        <td>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:TextBox ID="tbxFechaEntregaForm" runat="server" MaxLength="15"></asp:TextBox>
                                                                        <asp:CalendarExtender ID="CalendarExtender7" runat="server" TargetControlID="tbxFechaEntregaForm"
                                                                            Format="dd-MMM-yyyy"></asp:CalendarExtender>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label65" runat="server" Text="Fecha de entrega de formulario"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table width="100%">
                                                    <tr align="center" style="background-color: #C0C0C0">
                                                        <td>
                                                            <asp:Label ID="Label66" runat="server" Text="PARA USO EXCLUSIVO DE COPIDROGAS"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr align="center" style="background-color: #C0C0C0">
                                                        <td>
                                                            <asp:Label ID="Label118" runat="server" Text="COMITE DE ADMISIONES"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table width="100%">
                                                    <tr>
                                                        <td>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label107" runat="server" Text="Acto No."></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="tbxActoNoComAdm" runat="server" MaxLength="20"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label105" runat="server" Text="Fecha:"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="tbxFechaActoComAdm" runat="server" MaxLength="15"></asp:TextBox>
                                                                        <asp:CalendarExtender ID="CalendarExtender8" runat="server" TargetControlID="tbxFechaActoComAdm"
                                                                            Format="dd-MMM-yyyy"></asp:CalendarExtender>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label109" runat="server" Text="Favorable:"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="tbxFavorableComAdm" runat="server" MaxLength="20"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label106" runat="server" Text="Aplazado:"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="tbxAplazadoComAdm" runat="server" MaxLength="20"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label108" runat="server" Text="Desfavorable:"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="tbxDesfavorableComAdm" runat="server" MaxLength="20"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label110" runat="server" Text="Visita efectuada por:"></asp:Label>
                                                                    </td>
                                                                    <td colspan="4">
                                                                        <asp:TextBox ID="tbxVisitaEfectuadaPorComAdm" runat="server" MaxLength="400" Width="400"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label111" runat="server" Text="Observaciones"></asp:Label>
                                                                    </td>
                                                                    <td colspan="5">
                                                                        <asp:TextBox ID="tbxObservacionesComAdm" runat="server" MaxLength="700" Width="700"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label112" runat="server" Text="________________________"></asp:Label>
                                                                    </td>
                                                                    <td></td>
                                                                    <td></td>
                                                                    <td></td>
                                                                    <td>
                                                                        <asp:Label ID="Label113" runat="server" Text="________________________"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center">
                                                                        <asp:Label ID="Label114" runat="server" Text="Firma Coordinador"></asp:Label>
                                                                    </td>
                                                                    <td></td>
                                                                    <td></td>
                                                                    <td></td>
                                                                    <td align="center">
                                                                        <asp:Label ID="Label115" runat="server" Text="Firma secretario"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label116" runat="server" Text="Nombre:"></asp:Label>
                                                                        <asp:TextBox ID="tbxNombreCoordinador1ComAdm" runat="server" MaxLength="120" Width="120"></asp:TextBox>
                                                                    </td>
                                                                    <td></td>
                                                                    <td></td>
                                                                    <td></td>
                                                                    <td>
                                                                        <asp:Label ID="Label117" runat="server" Text="Nombre:"></asp:Label>
                                                                        <asp:TextBox ID="tbxNombreSecretario1ComAdm" runat="server" MaxLength="120" Width="120"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table width="100%">
                                                    <tr align="center" style="background-color: #C0C0C0">
                                                        <td>
                                                            <asp:Label ID="Label121" runat="server" Text="CONSEJO DE ADMINISTRACION"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table width="100%">
                                                    <tr>
                                                        <td>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label122" runat="server" Text="Acto No."></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="tbxActoNoConAdm" runat="server" MaxLength="20"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label123" runat="server" Text="Fecha:"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="tbxFechaActoConAdm" runat="server" MaxLength="15"></asp:TextBox>
                                                                        <asp:CalendarExtender ID="CalendarExtender9F" runat="server" TargetControlID="tbxFechaActoConAdm"
                                                                            Format="dd-MMM-yyyy"></asp:CalendarExtender>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label124" runat="server" Text="Favorable:"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="tbxFavorableConAdm" runat="server" MaxLength="20"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label128" runat="server" Text="Aplazado:"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="tbxAplazadoConAdm" runat="server" MaxLength="20"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label129" runat="server" Text="Desfavorable:"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="tbxDesfavorableConAdm" runat="server" MaxLength="20"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label130" runat="server" Text="Visita efectuada por:"></asp:Label>
                                                                    </td>
                                                                    <td colspan="4">
                                                                        <asp:TextBox ID="tbxVisitaEfectuadaPorConAdm" runat="server" MaxLength="400" Width="400"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label131" runat="server" Text="Observaciones"></asp:Label>
                                                                    </td>
                                                                    <td colspan="5">
                                                                        <asp:TextBox ID="tbxObservacionesConAdm" runat="server" MaxLength="700" Width="700"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label132" runat="server" Text="________________________"></asp:Label>
                                                                    </td>
                                                                    <td></td>
                                                                    <td></td>
                                                                    <td></td>
                                                                    <td>
                                                                        <asp:Label ID="Label133" runat="server" Text="________________________"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center">
                                                                        <asp:Label ID="Label134" runat="server" Text="Firma Coordinador"></asp:Label>
                                                                    </td>
                                                                    <td></td>
                                                                    <td></td>
                                                                    <td></td>
                                                                    <td align="center">
                                                                        <asp:Label ID="Label135" runat="server" Text="Firma secretario"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label138" runat="server" Text="Nombre:"></asp:Label>
                                                                        <asp:TextBox ID="tbxNombreCoordinador1ConAdm" runat="server" MaxLength="120" Width="120"></asp:TextBox>
                                                                    </td>
                                                                    <td></td>
                                                                    <td></td>
                                                                    <td></td>
                                                                    <td>
                                                                        <asp:Label ID="Label139" runat="server" Text="Nombre:"></asp:Label>
                                                                        <asp:TextBox ID="tbxNombreSecretario1ConAdm" runat="server" MaxLength="120" Width="120"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table width="100%">
                                                    <tr align="center" style="background-color: #C0C0C0">
                                                        <td>
                                                            <asp:Label ID="Label119" runat="server" Text="INFORMACION DE LA ENTREVISTA"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table width="100%">
                                                    <tr>
                                                        <td valign="top" style="width: 80%;">
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label148" runat="server" Text="Verificación de la entrevista 1"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label140" runat="server" Text="Fecha de verificación:"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="tbxFechaVerEntrevista1" runat="server" MaxLength="15"></asp:TextBox>
                                                                        <asp:CalendarExtender ID="CalendarExtender10F" runat="server" TargetControlID="tbxFechaVerEntrevista1"
                                                                            Format="dd-MMM-yyyy"></asp:CalendarExtender>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label141" runat="server" Text="Observaciones:"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="tbxObsEntrevista1" runat="server" MaxLength="700" Width="700"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label142" runat="server" Text="Nombre y cargo de quien verifica:"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="tbxVerificaEntrevista1" runat="server" MaxLength="700" Width="700"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table width="100%">
                                                    <tr>
                                                        <td valign="top" style="width: 80%;">
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label143" runat="server" Text="Verificación de la entrevista 2"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label144" runat="server" Text="Fecha de verificación:"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="tbxFechaVerEntrevista2" runat="server" MaxLength="15"></asp:TextBox>
                                                                        <asp:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="tbxFechaVerEntrevista2"
                                                                            Format="dd-MMM-yyyy"></asp:CalendarExtender>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label145" runat="server" Text="Observaciones:"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="tbxObsEntrevista2" runat="server" MaxLength="700" Width="700"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label149" runat="server" Text="Nombre y cargo de quien verifica:"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="tbxVerificaEntrevista2" runat="server" MaxLength="700" Width="700"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table width="100%">
                                                    <tr>
                                                        <td>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label120" runat="server" Text="Validación de firma"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddlValidaFirma" runat="server" AutoPostBack="false">
                                                                            <asp:ListItem>---</asp:ListItem>
                                                                            <asp:ListItem>NO</asp:ListItem>
                                                                            <asp:ListItem>SI</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label125" runat="server" Text="Validación de huella"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddlValidaHuella" runat="server" AutoPostBack="false">
                                                                            <asp:ListItem>---</asp:ListItem>
                                                                            <asp:ListItem>NO</asp:ListItem>
                                                                            <asp:ListItem>SI</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label126" runat="server" Text="Validación de entrevista"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddlValidaEntrevista" runat="server" AutoPostBack="false">
                                                                            <asp:ListItem>---</asp:ListItem>
                                                                            <asp:ListItem>NO</asp:ListItem>
                                                                            <asp:ListItem>SI</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </h1>
                                <br />
                                <h1>
                                    <table align="center">
                                        <tr align="center">
                                            <td>
                                                <table>
                                                    <tr align="left">
                                                        <td>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <table>
                                                                            <tr align="center">
                                                                                <td bgcolor="#BBBBBB">
                                                                                    <asp:Label ID="Label186A" runat="server" Text="Documentos adjuntos" Font-Names="Calibri"
                                                                                        Font-Size="Small"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <table>
                                                                                        <tr align="center">
                                                                                            <td colspan="3">
                                                                                                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                                                                                    ForeColor="#333333" GridLines="Vertical" ShowHeaderWhenEmpty="True" HeaderStyle-CssClass="gridViewHeader"
                                                                                                    BorderStyle="Solid" HorizontalAlign="Center" Font-Names="Calibri" Font-Size="Small"
                                                                                                    OnRowCommand="GridView2_RowCommand">
                                                                                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                                                                    <Columns>
                                                                                                        <asp:BoundField DataField="IdArchivo" HeaderText="IdArchivo" Visible="false" />
                                                                                                        <asp:BoundField DataField="NombreUsuario" HeaderText="Nombre Usuario" />
                                                                                                        <asp:BoundField DataField="FechaRegistro" HeaderText="Fecha Registro" />
                                                                                                        <asp:BoundField DataField="UrlArchivo" HeaderText="Nombre Archivo" />
                                                                                                        <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Icons/downloads.png" Text="Descargar"
                                                                                                            CommandName="Descargar" />
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
                                                                                        <tr align="center">
                                                                                            <td bgcolor="#BBBBBB">
                                                                                                <asp:Label ID="Label199A" runat="server" Text="Adjuntar documento .pdf:" Font-Names="Calibri"
                                                                                                    Font-Size="Small"></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:FileUpload ID="FileUpload1" runat="server" Font-Names="Calibri" Font-Size="Small" />
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Imagenes/Icons/Add.png"
                                                                                                    ToolTip="Adjuntar" OnClick="ImageButton1_Click" />
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
                                            </td>
                                        </tr>
                                    </table>
                                </h1>
                                <br />
                                <table align="center">
                                    <tr>
                                        <td>
                                            <asp:Button ID="Button1" runat="server" Text="Modificar" OnClick="Button1_Click"
                                                Visible="False" Font-Names="Calibri" Font-Size="Small" ValidationGroup="SaveKnowCliente1"/>
                                        </td>
                                        <td>
                                            <asp:Button ID="Button2" runat="server" Text="Imprimir" Visible="false" OnClick="Button2_Click"
                                                Font-Names="Calibri" Font-Size="Small" />
                                        </td>
                                        <td>
                                            <asp:Button ID="Button3" runat="server" Text="Continuar" OnClick="Button3_Click"
                                                Visible="False" Font-Names="Calibri" Font-Size="Small" />
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
    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="Button1" />
        <asp:PostBackTrigger ControlID="Button2" />
        <asp:PostBackTrigger ControlID="Button3" />
        <asp:PostBackTrigger ControlID="Button4" />
        <asp:PostBackTrigger ControlID="Button5" />
        <asp:PostBackTrigger ControlID="Button7" />
        <asp:PostBackTrigger ControlID="ImageButton1" />
        <asp:PostBackTrigger ControlID="GridView1" />
        <asp:PostBackTrigger ControlID="GridView2" />
    </Triggers>
</asp:UpdatePanel>
