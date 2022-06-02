<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FormularioConocimientoCliente.ascx.cs" Inherits="ListasSarlaft.UserControls.Sarlaft.FormularioConocimientoCliente" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControls/Sitio/OkMessageBox.ascx" TagPrefix="uc" TagName="OkMessageBox" %>
<style type="text/css">
    .gridViewHeader a:link {
        text-decoration: none;
    }

    .FondoAplicacion {
        background-color: Gray;
        filter: alpha(opacity=80);
        opacity: 0.8;
    }

    .style1 {
        width: 332px;
    }

    .style2 {
        width: 194px;
    }

    .style3 {
        width: 191px;
    }

    .style4 {
        width: 111px;
    }

    .style5 {
        width: 112px;
    }

    .style6 {
        height: 19px;
    }

    .style7 {
        width: 753px;
    }

    .style8 {
        width: 391px;
    }

    .auto-style2 {
        width: 100%;
        border-style: solid;
        border-width: 1px;
    }

    .auto-style4 {
        width: 787px;
    }

    .auto-style5 {
        text-align: center;
    }

    .auto-style6 {
        height: 73px;
    }

    .auto-style7 {
        height: 67px;
    }

    .auto-style8 {
        height: 29px;
    }

    .auto-style9 {
        text-align: center;
        background-color: #000000;
    }

    .auto-style10 {
        color: #FFFFFF;
    }
</style>
<script type = "text/javascript">
    function Confirm() {
        var confirm_value = document.createElement("INPUT");
        confirm_value.type = "hidden";
        confirm_value.name = "confirm_value";
        if (confirm("¿Esta seguro que desea aprobar el formulario?")) {
            confirm_value.value = "Si";
        } else {
            confirm_value.value = "No";
        }
        document.forms[0].appendChild(confirm_value);
    }
    function Confirm2() {
        var confirm_value = document.createElement("INPUT");
        confirm_value.type = "hidden";
        confirm_value.name = "confirm_value2";
        if (confirm("¿Esta seguro que desea rechazar el formulario?")) {
            confirm_value.value = "Si";
        } else {
            confirm_value.value = "No";
        }
        document.forms[0].appendChild(confirm_value);
    }
    </script>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <uc:OkMessageBox ID="omb" runat="server" />
            <table align="center" bgcolor="#EEEEEE" style="width:80%">
            <tr align="center" bgcolor="#333399">
                <td>
                    <asp:Label ID="Ltitulo" runat="server" ForeColor="White" Text="Formulario Conocimiento Cliente"
                        Font-Bold="False" Font-Names="Calibri" Font-Size="Large"></asp:Label>
                </td>
            </tr>
                </table>
                <table align="center" width="50%">
                    <tr>
                        <td align="center">
                            <asp:Label ID="LtipoPersona" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Tipo de Persona:"></asp:Label>
                        </td>
                        <td align="center">
                            <asp:DropDownList ID="DDLtipoPersona" runat="server" Width="300px">
                                <asp:ListItem Text="" Value="0"></asp:ListItem>
                                <asp:ListItem Text="Persona Juridica" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Persona Natural" Value="2"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="rfvTipoF" runat="server" ControlToValidate="DDLtipoPersona"
                                                ErrorMessage="Debe seleccionar un formulario." ToolTip="Debe seleccionar un formulario."
                                                ValidationGroup="FCC" ForeColor="Red" InitialValue="0">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr align="center">
                        <td align="center">
                            <asp:Label ID="LIdentificacion" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Documento Identificación:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TXidentificacion" runat="server" Font-Names="Calibri" Font-Size="Small" Width="300px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="rfvId" runat="server" ControlToValidate="TXidentificacion"
                                    ErrorMessage="Debe ingresar el Numero de Identificacion." ToolTip="Debe ingresar el Numero de Identificacion."
                                    ValidationGroup="FCC" ForeColor="Red">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2">
                            <asp:ImageButton ID="btnSearchCarac" runat="server" CausesValidation="true" ValidationGroup="FCC" CommandName="Search" ImageUrl="~/Imagenes/Icons/Apps-Search-And-Replace-icon.png" OnClick="btnSearchCarac_Click" Text="Search" ToolTip="Search" />
                        </td>
                        
                    </tr>
                </table>
        <table align="center" width="80%" runat="server" id="TBgridCliente">
            <tr>
                <td>
                    <asp:GridView ID="GVformularioCliente" runat="server" CellPadding="4"
                                    ForeColor="#333333" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
                                    ShowHeaderWhenEmpty="True" DataKeyNames="IdConocimientoCliente,IdUsuario,Usuario"
                                    HeaderStyle-CssClass="gridViewHeader" BorderStyle="Solid" GridLines="Vertical"
                                    CssClass="Apariencia" Font-Bold="False" OnRowCommand="GVformularioCliente_RowCommand">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:BoundField DataField="IdRegistro" HeaderText="Código" SortExpression="IdRegistro" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="NumeroDocumento" HeaderText="Numero Documento" SortExpression="NumeroDocumento" ItemStyle-HorizontalAlign="Center" />
                                        <asp:TemplateField HeaderText="Primer Apellido" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 150px">
                                                    <asp:Label ID="PNPrimerApellido" runat="server" Text='<% # Bind("PrimerApellido")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" HorizontalAlign="center" />
                                            <ItemStyle Wrap="false"  />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Segundo Apellido" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 150px">
                                                    <asp:Label ID="PNSegunApellido" runat="server" Text='<% # Bind("SegundoApellido")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false"  HorizontalAlign="center" />
                                            <ItemStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Nombre Cliente" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 150px">
                                                    <asp:Label ID="PNNombres" runat="server" Text='<% # Bind("Nombres")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false"  HorizontalAlign="center" />
                                            <ItemStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Estado" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 150px">
                                                    <asp:Label ID="estado" runat="server" Text='<% # Bind("estado")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false"  HorizontalAlign="center" />
                                            <ItemStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="FechaRegistro" HeaderText="Fecha Registro" ReadOnly="True" SortExpression="FechaRegistro" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="IdUsuario" HeaderText="IdUsuario" ReadOnly="True" Visible="false" SortExpression="IdUsuario" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="Usuario" HeaderText="Usuario" ReadOnly="True" Visible="false" SortExpression="Usuario" ItemStyle-HorizontalAlign="Center" />
                                        <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Icons/select.png" Text="Seleccionar" HeaderText="Seleccionar" CommandName="Seleccionar" ItemStyle-HorizontalAlign="Center" />
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
        <table runat="server" id="TBFormularioNatural" visible="false" align="center">
                <tr>
                    <td>
                        <h1 style="font-size: 13px;">
                           <!--Copiar desde aqui para Consultar-->
                            <div id="PartesFormulario" visible="true">
                                <tr id="Encabezado" runat="server" visible="false">
                                    <td>
                                        <table align="center" width="100%">
                                            <tr align="center">
                                                <td>
                                                    <asp:Label ID="lblTituloFormulario" runat="server" Text="FORMULARIO CONOCIMIENTO DEL CLIENTE"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr align="center">
                                                <td>
                                                    <asp:Label ID="lblSubTituloFormulario" runat="server" Text="WILLIS SEGUROS"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr align="center">
                                                <td>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblFechaFormulario" runat="server" Text="Fecha de diligenciamiento"></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="tbxFechaFormulario" runat="server" ToolTip="DD/MM/AAAA" MaxLength="15"></asp:TextBox>
                                                                <asp:CalendarExtender ID="ceFechaDiligenciamiento" runat="server" TargetControlID="tbxFechaFormulario"
                                                                    Format="dd-MMM-yyyy"></asp:CalendarExtender>
                                                                <asp:RequiredFieldValidator ID="rfvFechaFormulario" runat="server"
                                                                    ControlToValidate="tbxFechaFormulario"
                                                                    ValidationGroup="vdgWillis"
                                                                    ErrorMessage="*"
                                                                    ForeColor="Red"> </asp:RequiredFieldValidator>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblCiudad" runat="server" Text="Ciudad"></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="tbxCiudad" runat="server" MaxLength="25"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblSucursal" runat="server" Text="Sucursal"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlSucursal" runat="server" AutoPostBack="false">
                                                                    <asp:ListItem Value="">---</asp:ListItem>
                                                                    <asp:ListItem Value="MEDELLIN">MEDELLIN</asp:ListItem>
                                                                    <asp:ListItem Value="CALI">CALI</asp:ListItem>
                                                                    <asp:ListItem Value="BARRANQUILLA">BARRANQUILLA</asp:ListItem>
                                                                    <asp:ListItem Value="CARTAGENA">CARTAGENA</asp:ListItem>
                                                                    <asp:ListItem Value="BOGOTA">BOGOTA</asp:ListItem>
                                                                </asp:DropDownList><asp:RequiredFieldValidator ID="rfvSucursal"
                                                                    ControlToValidate="ddlSucursal"
                                                                    ValidationGroup="vdgWillis"
                                                                    ErrorMessage="*"
                                                                    ForeColor="Red"
                                                                    InitialValue="" runat="server"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblTipoSolicitud" runat="server" Text="Tipo de Solicitud"></asp:Label></td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlTipoSolicitud" runat="server" AutoPostBack="false">
                                                                    <asp:ListItem Value="">---</asp:ListItem>
                                                                    <asp:ListItem Value="ACTUALIZACIÓN">ACTUALIZACIÓN</asp:ListItem>
                                                                    <asp:ListItem Value="RENOVACIÓN">RENOVACIÓN</asp:ListItem>
                                                                    <asp:ListItem Value="VINCULACIÓN">VINCULACIÓN</asp:ListItem>
                                                                </asp:DropDownList><asp:RequiredFieldValidator ID="rfvTipoSolicitud"
                                                                    ControlToValidate="ddlTipoSolicitud"
                                                                    ValidationGroup="vdgWillis"
                                                                    ErrorMessage="*"
                                                                    ForeColor="Red"
                                                                    InitialValue="" runat="server"></asp:RequiredFieldValidator></td>
                                                            <td></td>
                                                            <td></td>
                                                            <td></td>
                                                            <td></td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr id="InfoClaseVinculacionNota" visible="false" runat="server">
                                    <td>
                                        <table width="100%">
                                            <tr valign="middle">
                                                <td style="background-color: #DDDDDD; text-align: center;">
                                                    <asp:Label ID="lblCVN" runat="server" Text=""></asp:Label></td>
                                            </tr>
                                        </table>

                                    </td>
                                </tr>
                                <tr id="InfoClaseVinculacion" visible="false" runat="server">
                                    <td>
                                        <table>
                                            <tr align="left">
                                                <td>
                                                    <asp:Label ID="lblClaseVinculacion" runat="server" Text="CLASE DE VINCULACIÓN:"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlClaseVinculacion" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlClaseVinculacion_SelectedIndexChanged">
                                                        <asp:ListItem Value="">---</asp:ListItem>
                                                        <asp:ListItem Value="TOMADOR">TOMADOR</asp:ListItem>
                                                        <asp:ListItem Value="ASEGURADO">ASEGURADO</asp:ListItem>
                                                        <asp:ListItem Value="BENEFICIARIO">BENEFICIARIO</asp:ListItem>
                                                        <asp:ListItem Value="AFIANZADO">AFIANZADO</asp:ListItem>
                                                        <asp:ListItem Value="PROVEEDOR">PROVEEDOR</asp:ListItem>
                                                        <asp:ListItem Value="INTERMEDIARIO">INTERMEDIARIO</asp:ListItem>
                                                        <asp:ListItem Value="OTRO">OTRO</asp:ListItem>
                                                    </asp:DropDownList><asp:RequiredFieldValidator ID="rfvClaseVinculacion"
                                                        ControlToValidate="ddlClaseVinculacion"
                                                        ValidationGroup="vdgWillis"
                                                        ErrorMessage="*"
                                                        ForeColor="Red"
                                                        InitialValue="" runat="server"></asp:RequiredFieldValidator>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblOtraVinculacion" runat="server" Text="OTRO"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="tbxOtraClaseVinculacion" runat="server"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblTipoCliente" runat="server" Text="Residencia de la Sociedad" Visible="false"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlTipoCliente" runat="server" AutoPostBack="false" Visible="false">
                                                        <asp:ListItem Value="">---</asp:ListItem>
                                                        <asp:ListItem Value="EXTRANJERA">EXTRANJERA</asp:ListItem>
                                                        <asp:ListItem Value="NACIONAL">NACIONAL</asp:ListItem>
                                                    </asp:DropDownList><asp:RequiredFieldValidator ID="rfvTipoCliente"
                                                        ControlToValidate="ddlTipoCliente"
                                                        ValidationGroup="vdgWillis"
                                                        ErrorMessage="*"
                                                        ForeColor="Red"
                                                        InitialValue="" runat="server"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr id="infoNota2" visible="false" runat="server">
                                    <td>
                                        <table width="100%">
                                            <tr valign="middle">
                                                <td style="background-color: #DDDDDD;">
                                                    <asp:Label ID="lblN2" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr id="infoVinculos" visible="false" runat="server">
                                    <td>
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblTomadorAsegurado" runat="server" Text="TOMADOR-ASEGURADO"></asp:Label></td>
                                                <td>
                                                    <asp:DropDownList ID="ddlTomadorAsegurado" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlTomadorAsegurado_SelectedIndexChanged">
                                                        <asp:ListItem Value="">---</asp:ListItem>
                                                        <asp:ListItem Value="FAMILIAR">FAMILIAR</asp:ListItem>
                                                        <asp:ListItem Value="COMERCIAL">COMERCIAL</asp:ListItem>
                                                        <asp:ListItem Value="LABORAL">LABORAL</asp:ListItem>
                                                        <asp:ListItem Value="OTRA">OTRA</asp:ListItem>
                                                    </asp:DropDownList><asp:RequiredFieldValidator ID="rfvTomadorAsegurado"
                                                        ControlToValidate="ddlTomadorAsegurado"
                                                        ValidationGroup="vdgWillis"
                                                        ErrorMessage="*"
                                                        ForeColor="Red"
                                                        InitialValue="" runat="server"></asp:RequiredFieldValidator></td>
                                                <td>
                                                    <asp:Label ID="lblOtraTomadorAsegurado" runat="server" Text="CUAL?"></asp:Label></td>
                                                <td>
                                                    <asp:TextBox ID="tbxOtraTomadorAsegurado" runat="server"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblTomadorBeneficiario" runat="server" Text="TOMADOR-BENEFICIARIO"></asp:Label></td>
                                                <td>
                                                    <asp:DropDownList ID="ddlTomadorBeneficiario" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlTomadorBeneficiario_SelectedIndexChanged">
                                                        <asp:ListItem Value="">---</asp:ListItem>
                                                        <asp:ListItem Value="FAMILIAR">FAMILIAR</asp:ListItem>
                                                        <asp:ListItem Value="COMERCIAL">COMERCIAL</asp:ListItem>
                                                        <asp:ListItem Value="LABORAL">LABORAL</asp:ListItem>
                                                        <asp:ListItem Value="OTRA">OTRA</asp:ListItem>
                                                    </asp:DropDownList><asp:RequiredFieldValidator ID="rfvTomadorBeneficiario"
                                                        ControlToValidate="ddlTomadorBeneficiario"
                                                        ValidationGroup="vdgWillis"
                                                        ErrorMessage="*"
                                                        ForeColor="Red"
                                                        InitialValue="" runat="server"></asp:RequiredFieldValidator></td>
                                                <td>
                                                    <asp:Label ID="lblOtraTomadorBeneficiario" runat="server" Text="CUAL?"></asp:Label></td>
                                                <td>
                                                    <asp:TextBox ID="tbxOtraTomadorBeneficiario" runat="server"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblAseguradoBeneficiario" runat="server" Text="ASEGURADO-BENEFICIARIO"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlAseguradoBeneficiario" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlAseguradoBeneficiario_SelectedIndexChanged">
                                                        <asp:ListItem Value="">---</asp:ListItem>
                                                        <asp:ListItem Value="FAMILIAR">FAMILIAR</asp:ListItem>
                                                        <asp:ListItem Value="COMERCIAL">COMERCIAL</asp:ListItem>
                                                        <asp:ListItem Value="LABORAL">LABORAL</asp:ListItem>
                                                        <asp:ListItem Value="OTRA">OTRA</asp:ListItem>
                                                    </asp:DropDownList><asp:RequiredFieldValidator ID="rfvAseguradoBeneficiario"
                                                        ControlToValidate="ddlAseguradoBeneficiario"
                                                        ValidationGroup="vdgWillis"
                                                        ErrorMessage="*"
                                                        ForeColor="Red" InitialValue="" runat="server"></asp:RequiredFieldValidator>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblOtraAseguradoBeneficiario" runat="server" Text="CUAL?"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="tbxOtraAseguradoBeneficiario" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr id="infoTituloPN" visible="false" runat="server">
                                    <td>
                                        <table width="100%">
                                            <tr align="center">
                                                <td style="background-color: #C0C0C0">
                                                    <asp:Label ID="Label14" runat="server" Text="1. INFORMACIÓN BÁSICA"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr id="infoPN" visible="false" runat="server">
                                    <td>
                                        <table>
                                            <tr>
                                                <td>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblPNPrimerApellido" runat="server" Text="PRIMER APELLIDO"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="tbxPNPrimerApellido" runat="server" Width="170px" MaxLength="20"></asp:TextBox><asp:RequiredFieldValidator ID="rfvNPrimerApellido" runat="server"
                                                                    ControlToValidate="tbxPNPrimerApellido"
                                                                    ValidationGroup="vdgWillis"
                                                                    ErrorMessage="*"
                                                                    ForeColor="Red"> </asp:RequiredFieldValidator>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblPNSegunApellido" runat="server" Text="SEGUNDO APELLIDO"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="tbxPNSegunApellido" runat="server" Width="170px" MaxLength="20"></asp:TextBox>
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
                                                                <asp:Label ID="lblPNNombres" runat="server" Text="NOMBRES"></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="tbxPNNombres" runat="server" Width="200px" MaxLength="45"></asp:TextBox><asp:RequiredFieldValidator ID="rfvPNNombres" runat="server"
                                                                    ControlToValidate="tbxPNNombres"
                                                                    ValidationGroup="vdgWillis"
                                                                    ErrorMessage="*"
                                                                    ForeColor="Red"> </asp:RequiredFieldValidator>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblPNTipoDocumento" runat="server" Text="TIPO DOCUMENTO"></asp:Label></td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlPNTipoDocumento" runat="server">
                                                                    <asp:ListItem Value="">---</asp:ListItem>
                                                                    <asp:ListItem Value="C.C.">C.C.</asp:ListItem>
                                                                    <asp:ListItem Value="C.E.">C.E.</asp:ListItem>
                                                                    <asp:ListItem Value="NIT">NIT</asp:ListItem>
                                                                    <asp:ListItem Value="NUIP">NUIP</asp:ListItem>
                                                                    <asp:ListItem Value="PASAPORTE">PASAPORTE</asp:ListItem>
                                                                    <asp:ListItem Value="T.I.">T.I.</asp:ListItem>
                                                                </asp:DropDownList><asp:RequiredFieldValidator ID="rfvPNTipoDocumento"
                                                                    ControlToValidate="ddlPNTipoDocumento"
                                                                    ValidationGroup="vdgWillis"
                                                                    ErrorMessage="*"
                                                                    ForeColor="Red"
                                                                    InitialValue="" runat="server"></asp:RequiredFieldValidator>
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
                                                                <asp:Label ID="lblPNNumeroDocumento" runat="server" Text="NÚMERO"></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="tbxPNNumeroDocumento" runat="server" MaxLength="15"></asp:TextBox><asp:RequiredFieldValidator ID="rfvPNNumeroDocumento" runat="server"
                                                                    ControlToValidate="tbxPNNumeroDocumento"
                                                                    ValidationGroup="vdgWillis"
                                                                    ErrorMessage="*"
                                                                    ForeColor="Red"> </asp:RequiredFieldValidator></td>
                                                            <td>
                                                                <asp:Label ID="lblFechaExpedicionPN" runat="server" Text="FECHA DE EXPEDICIÓN"></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="tbxPNFechaExpedicion" runat="server" MaxLength="15"></asp:TextBox>
                                                                <asp:CalendarExtender ID="cePNFechaExpedicion" runat="server" TargetControlID="tbxPNFechaExpedicion"
                                                                    Format="dd-MMM-yyyy"></asp:CalendarExtender>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblPNLugar" runat="server" Text="LUGAR DE EXPEDICIÓN"></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="tbxPNLugar" runat="server" MaxLength="20"></asp:TextBox></td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblPNFechaNacimiento" runat="server" Text="FECHA DE NACIMIENTO"></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="tbxPNFechaNacimiento" runat="server" MaxLength="15"></asp:TextBox>
                                                                <asp:CalendarExtender ID="cePNFechaNacimiento" runat="server" TargetControlID="tbxPNFechaNacimiento"
                                                                    Format="dd-MMM-yyyy"></asp:CalendarExtender>
                                                                <asp:RequiredFieldValidator ID="rfvPNFechaNacimiento" runat="server"
                                                                    ControlToValidate="tbxPNFechaNacimiento"
                                                                    ValidationGroup="vdgWillis"
                                                                    ErrorMessage="*"
                                                                    ForeColor="Red"> </asp:RequiredFieldValidator>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblPNLugarNmto" runat="server" Text="LUGAR DE NACIMIENTO"></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="tbxPNLugarNmto" runat="server" MaxLength="15" Width="120px"></asp:TextBox><asp:RequiredFieldValidator ID="rfvPNLugarNmto" runat="server"
                                                                    ControlToValidate="tbxPNLugarNmto"
                                                                    ValidationGroup="vdgWillis"
                                                                    ErrorMessage="*"
                                                                    ForeColor="Red"> </asp:RequiredFieldValidator>
                                                                <%--            <asp:AutoCompleteExtender ID="txtBoxLugarNacimiento_AutoCompleteExtender"
                                                                    runat="server"
                                                                    DelimiterCharacters=""
                                                                    Enabled="True"
                                                                    ServicePath="AutoComplete.asmx"
                                                                    TargetControlID="txtBoxLugarNacimiento"
                                                                    CompletionSetCount="20"
                                                                    MinimumPrefixLength="1"
                                                                    ServiceMethod="GetCompletionList">
                                                                </asp:AutoCompleteExtender>--%></td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblNacionalidadPN1" runat="server" Text="NACIONALIDAD" Width="120px"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="tbxPNNacionalidad" runat="server" MaxLength="20" Width="150px"></asp:TextBox></td>
                                                            <td>
                                                                <asp:Label ID="LblPNCorreoE" runat="server" Text="EMAIL"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="tbxPNCorreoElectronico" runat="server" MaxLength="100" Width="350px"></asp:TextBox></td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblDireccionResidencia" runat="server" Text="DIRECCION (Residencia)"></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="tbxPNDireccionResidencia" runat="server" Width="300px" MaxLength="100"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                                                                    ControlToValidate="tbxPNDireccionResidencia"
                                                                    ValidationGroup="vdgWillis2"
                                                                    ErrorMessage="*"
                                                                    ForeColor="Red"> </asp:RequiredFieldValidator></td>
                                                            <td>
                                                                <asp:Label ID="lblPNDpto" runat="server" Text="DEPARTAMENTO"></asp:Label></td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlPNDpto" runat="server" DataSourceID="sdsPNDpto"
                                                                    DataTextField="NombreDepartamento" DataValueField="IdDepartamento" AutoPostBack="True">
                                                                </asp:DropDownList>
                                                                <asp:SqlDataSource ID="sdsPNDpto" runat="server" ConnectionString="<%$ ConnectionStrings:Sherlock_TestConnectionString %>" SelectCommand="SP_VerDepartamentos" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
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
                                                                <asp:Label ID="lblCiudadResidencia" runat="server" Text="CIUDAD"></asp:Label></td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlPNCiudad1" runat="server" DataSourceID="SqlDataSourceCiudades"
                                                                    DataTextField="NombreCiudad" DataValueField="IdCiudad" Width="250px">
                                                                </asp:DropDownList>
                                                                <asp:SqlDataSource ID="SqlDataSourceCiudades" runat="server" ConnectionString="<%$ ConnectionStrings:Sherlock_TestConnectionString %>" SelectCommand="SP_VerCiudades" SelectCommandType="StoredProcedure">
                                                                    <SelectParameters>
                                                                        <asp:ControlParameter ControlID="ddlPNDpto" DefaultValue="1" Name="IdDepartamento"
                                                                            PropertyName="SelectedValue" Type="Int32" />
                                                                    </SelectParameters>
                                                                </asp:SqlDataSource>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblPNTelefono1" runat="server" Text="TELEFONO (Casa)"></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="tbxPNTelefono1" runat="server" MaxLength="10"></asp:TextBox><asp:RequiredFieldValidator ID="rfvPNTelefono1" runat="server"
                                                                    ControlToValidate="tbxPNTelefono1"
                                                                    ValidationGroup="vdgWillis2"
                                                                    ErrorMessage="*"
                                                                    ForeColor="Red"></asp:RequiredFieldValidator><asp:FilteredTextBoxExtender ID="ftePNTelefono1" runat="server"
                                                                        TargetControlID="tbxPNTelefono1"
                                                                        FilterType="Numbers,Custom"
                                                                        ValidChars=""></asp:FilteredTextBoxExtender>
                                                                <asp:RegularExpressionValidator ID="revPNTelefono1Num" runat="server" ErrorMessage="Solo números" Font-Bold="false"
                                                                    Font-Names="Calibri" Font-Size="Small" ForeColor="Red" ControlToValidate="tbxPNTelefono1" ValidationExpression="^[0-9]*\.?[0-9]+$" ValidationGroup="SaveKnowCliente"></asp:RegularExpressionValidator></td>
                                                            <td>
                                                                <asp:Label ID="lblPNCelular" runat="server" Text="CELULAR"></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="tbxPNCelular" runat="server" MaxLength="10"></asp:TextBox><asp:FilteredTextBoxExtender ID="ftxtbxPNCelular" runat="server"
                                                                    TargetControlID="tbxPNCelular"
                                                                    FilterType="Numbers,Custom"
                                                                    ValidChars=""></asp:FilteredTextBoxExtender>
                                                                <asp:RegularExpressionValidator ID="revPNCelular" runat="server" ErrorMessage="Solo números" Font-Bold="false"
                                                                    Font-Names="Calibri" Font-Size="Small" ForeColor="Red" ControlToValidate="tbxPNCelular" ValidationExpression="^[0-9]*\.?[0-9]+$" ValidationGroup="SaveKnowCliente"></asp:RegularExpressionValidator></td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label2" runat="server" Text="CIIU (Cód.)"></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="tbxPNCIIU" runat="server" Text="" Enabled="False"></asp:TextBox>
                                                                <asp:DropDownList ID="DDLcodCIIU" runat="server" DataSourceID="sqlDSPNciiuCod"
                                                                    DataTextField="CIIU" DataValueField="CIIU" AutoPostBack="true"  >
                                                                </asp:DropDownList><asp:RequiredFieldValidator ID="rfvCodCiiu"
                                                                    ControlToValidate="DDLcodCIIU"
                                                                    ValidationGroup="vdgWillis"
                                                                    ErrorMessage="*"
                                                                    ForeColor="Red"
                                                                    InitialValue="0" runat="server" />
                                                                <asp:SqlDataSource ID="sqlDSPNciiuCod" runat="server" ConnectionString="<%$ ConnectionStrings:Sherlock_TestConnectionString %>" SelectCommand="Parametrizacion.spCodCIIU" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                                                            </td>
                                                            
                                                            <td>
                                                                <asp:Label ID="lblPNCIIUDescripcion" runat="server" Text="CIIU"></asp:Label></td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlPNCIIUDescripcion" runat="server" DataSourceID="sdsPNCIIUDescripcion"
                                                                    DataTextField="Descripcion" DataValueField="CIIU" AutoPostBack="true" OnSelectedIndexChanged="ddlPNCIIUDescripcion_SelectedIndexChanged">
                                                                </asp:DropDownList><asp:RequiredFieldValidator ID="rfvPNCIIUDescripcion"
                                                                    ControlToValidate="ddlPNCIIUDescripcion"
                                                                    ValidationGroup="vdgWillis"
                                                                    ErrorMessage="*"
                                                                    ForeColor="Red"
                                                                    InitialValue="0" runat="server" />
                                                                <asp:SqlDataSource ID="sdsPNCIIUDescripcion" runat="server" ConnectionString="<%$ ConnectionStrings:Sherlock_TestConnectionString %>" SelectCommand="Parametrizacion.spCIIU" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblPNActividadEconomica" runat="server" Text="ACTIVIDAD PRINCIPAL"></asp:Label></td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlPNActividadEconomica" runat="server">
                                                                    <asp:ListItem Value="">---</asp:ListItem>
                                                                    <asp:ListItem Value="ASALARIADO">ASALARIADO</asp:ListItem>
                                                                    <asp:ListItem Value="COMERCIANTE">COMERCIANTE</asp:ListItem>
                                                                    <asp:ListItem Value="EMPLEADO PUBLICO">EMPLEADO PUBLICO</asp:ListItem>
                                                                    <asp:ListItem Value="ESTUDIANTE">ESTUDIANTE</asp:ListItem>
                                                                    <asp:ListItem Value="HOGAR">HOGAR</asp:ListItem>
                                                                    <asp:ListItem Value="INDEPENDIENTE">INDEPENDIENTE</asp:ListItem>
                                                                    <asp:ListItem Value="INVERSIONISTA">INVERSIONISTA</asp:ListItem>
                                                                    <asp:ListItem Value="PENSIONADO">PENSIONADO</asp:ListItem>
                                                                    <asp:ListItem Value="RENTISTA">RENTISTA</asp:ListItem>
                                                                    <asp:ListItem Value="SOCIO">SOCIO</asp:ListItem>
                                                                </asp:DropDownList><asp:RequiredFieldValidator ID="rfvPNActividadEconomica"
                                                                    ControlToValidate="ddlPNActividadEconomica"
                                                                    ValidationGroup="vdgWillis"
                                                                    ErrorMessage="*"
                                                                    ForeColor="Red"
                                                                    InitialValue="" runat="server"></asp:RequiredFieldValidator></td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblPNSector1" runat="server" Text="SECTOR"></asp:Label></td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlPNSector1" runat="server">
                                                                    <asp:ListItem Value="">---</asp:ListItem>
                                                                    <asp:ListItem Value="AGROPECUARIO">AGROPECUARIO</asp:ListItem>
                                                                    <asp:ListItem Value="COMERCIO">COMERCIO</asp:ListItem>
                                                                    <asp:ListItem Value="CONSTRUCCION">CONSTRUCCION</asp:ListItem>
                                                                    <asp:ListItem Value="FINANCIERO">FINANCIERO</asp:ListItem>
                                                                    <asp:ListItem Value="INDUSTRIAL">INDUSTRIAL</asp:ListItem>
                                                                    <asp:ListItem Value="MINERO">MINERO Y ENERGETICO</asp:ListItem>
                                                                    <asp:ListItem Value="SERVICIOS">SERVICIOS</asp:ListItem>
                                                                    <asp:ListItem Value="SOLIDARIO">SOLIDARIO</asp:ListItem>
                                                                    <asp:ListItem Value="TRANSPORTE">TRANSPORTE</asp:ListItem>
                                                                </asp:DropDownList><asp:RequiredFieldValidator ID="rfvPNSector1"
                                                                    ControlToValidate="ddlPNSector1"
                                                                    ValidationGroup="vdgWillis2"
                                                                    ErrorMessage="*"
                                                                    ForeColor="Red"
                                                                    InitialValue="" runat="server"></asp:RequiredFieldValidator></td>
                                                            <td>
                                                                <asp:Label ID="lblPNOcupacionOficio" runat="server" Text="OCUPACIÓN"></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="tbxPNOcupacionOficio" runat="server" MaxLength="20" Width="150px"></asp:TextBox><asp:RequiredFieldValidator ID="rfvPNOcupacionOficio" runat="server"
                                                                    ControlToValidate="tbxPNOcupacionOficio"
                                                                    ValidationGroup="vdgWillis2"
                                                                    ErrorMessage="*"
                                                                    ForeColor="Red"> </asp:RequiredFieldValidator></td>
                                                            <td>
                                                                <asp:Label ID="lblPNCargo" runat="server" Text="CARGO"></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="tbxPNCargo" runat="server" Width="200px" MaxLength="20"></asp:TextBox></td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table>
                                                        <tr>
                                                            <td>
<asp:Label ID="LTipoactividadPN" runat="server" Text="TIPO DE ACTIVIDAD"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlTipoActividadPN" runat="server" DataSourceID="sqlDSPNtipoactividad"
                                                                    DataTextField="Nombre" DataValueField="IdTipoActividad" AutoPostBack="true" >
                                                                </asp:DropDownList>
                                                                <asp:SqlDataSource ID="sqlDSPNtipoactividad" runat="server" ConnectionString="<%$ ConnectionStrings:Sherlock_TestConnectionString %>" SelectCommand="Parametrizacion.spTipoActividadPN" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                                                            </td>
                                                            <td id="tdOtroTipoActividadPN" runat="server" visible="false">
                                                                <asp:Label ID="LOtroTipoActividadON" runat="server" Text="¿Cual?:"></asp:Label>
                                                            </td>
                                                            <td id="tdValorOtroTipoActividadPN" runat="server" visible="false">
<asp:TextBox ID="TXvalorOtroTipoActividadPN" runat="server" Width="200px" MaxLength="20"></asp:TextBox>
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
                                                                <asp:Label ID="Label28" runat="server" Text="EMPRESA DONDE TRABAJA"></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="tbxPNEmpresaTrabajo" runat="server" MaxLength="80" Width="300px"></asp:TextBox></td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblPNDireccionEmpresa" runat="server" Text="DIRECCION (OfIcina)"></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="tbxPNDireccionEmpresa" runat="server" MaxLength="100" Width="250px"></asp:TextBox></td>
                                                            <td>
                                                                <asp:Label ID="lblPNDptoEmpresa" runat="server" Text="DEPARTAMENTO"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlPNDptoEmpresa" runat="server" DataSourceID="sdsPNDptoEmpresa"
                                                                    DataTextField="NombreDepartamento" DataValueField="IdDepartamento" AutoPostBack="True">
                                                                </asp:DropDownList>
                                                                <asp:SqlDataSource ID="sdsPNDptoEmpresa" runat="server" ConnectionString="<%$ ConnectionStrings:Sherlock_TestConnectionString %>" SelectCommand="SP_VerDepartamentos" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
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
                                                                <asp:Label ID="lblPNCiudadEmpresa" runat="server" Text="CIUDAD"></asp:Label></td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlPNCiudadEmpresa" runat="server" DataSourceID="sdsPNCiudadEmpresa"
                                                                    DataTextField="NombreCiudad" DataValueField="IdCiudad" Width="250px">
                                                                </asp:DropDownList>
                                                                <asp:SqlDataSource ID="sdsPNCiudadEmpresa" runat="server" ConnectionString="<%$ ConnectionStrings:Sherlock_TestConnectionString %>" SelectCommand="SP_VerCiudades" SelectCommandType="StoredProcedure">
                                                                    <SelectParameters>
                                                                        <asp:ControlParameter ControlID="ddlPNDptoEmpresa" DefaultValue="1" Name="IdDepartamento"
                                                                            PropertyName="SelectedValue" Type="Int32" />
                                                                    </SelectParameters>
                                                                </asp:SqlDataSource>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label33" runat="server" Text="TELEFONO (Oficina)"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="tbxPNTelefonoEmpresa" runat="server" MaxLength="10"></asp:TextBox><asp:FilteredTextBoxExtender ID="ftbPNTelefonoEmpresa" runat="server"
                                                                    TargetControlID="tbxPNTelefonoEmpresa"
                                                                    FilterType="Numbers,Custom"
                                                                    ValidChars=""></asp:FilteredTextBoxExtender>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Solo números" Font-Bold="false"
                                                                    Font-Names="Calibri" Font-Size="Small" ForeColor="Red" ControlToValidate="tbxPNTelefonoEmpresa" ValidationExpression="^[0-9]*\.?[0-9]+$" ValidationGroup="SaveKnowCliente"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label9" runat="server" Text="ACTIVIDAD SECUNDARIA"></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="tbxPNOtraActEconomica" runat="server" MaxLength="80"></asp:TextBox>
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
                                                                <asp:Label ID="lblPNSector2" runat="server" Text="SECTOR"></asp:Label></td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlPNSector2" runat="server">
                                                                    <asp:ListItem Value="">---</asp:ListItem>
                                                                    <asp:ListItem Value="AGROPECUARIO">AGROPECUARIO</asp:ListItem>
                                                                    <asp:ListItem Value="COMERCIO">COMERCIO</asp:ListItem>
                                                                    <asp:ListItem Value="CONSTRUCCION">CONSTRUCCION</asp:ListItem>
                                                                    <asp:ListItem Value="FINANCIERO">FINANCIERO</asp:ListItem>
                                                                    <asp:ListItem Value="INDUSTRIAL">INDUSTRIAL</asp:ListItem>
                                                                    <asp:ListItem Value="MINERO">MINERO Y ENERGETICO</asp:ListItem>
                                                                    <asp:ListItem Value="SERVICIOS">SERVICIOS</asp:ListItem>
                                                                    <asp:ListItem Value="SOLIDARIO">SOLIDARIO</asp:ListItem>
                                                                    <asp:ListItem Value="TRANSPORTE">TRANSPORTE</asp:ListItem>
                                                                </asp:DropDownList></td>
                                                            <td>
                                                                <asp:Label ID="lblPNDireccion" runat="server" Text="DIRECCION"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="tbxPNDireccion" runat="server" MaxLength="100" Width="250px"></asp:TextBox>
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
                                                                <asp:Label ID="lblPNDpto2" runat="server" Text="DEPARTAMENTO"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlPNDpto2" runat="server" DataSourceID="sdsPNDpto2"
                                                                    DataTextField="NombreDepartamento" DataValueField="IdDepartamento" AutoPostBack="True">
                                                                </asp:DropDownList>
                                                                <asp:SqlDataSource ID="sdsPNDpto2" runat="server" ConnectionString="<%$ ConnectionStrings:Sherlock_TestConnectionString %>" SelectCommand="SP_VerDepartamentos" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblPNCiudad2" runat="server" Text="CIUDAD"></asp:Label></td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlPNCiudad2" runat="server" DataSourceID="sdsPNCiudad2"
                                                                    DataTextField="NombreCiudad" DataValueField="IdCiudad" Width="250px">
                                                                </asp:DropDownList>
                                                                <asp:SqlDataSource ID="sdsPNCiudad2" runat="server" ConnectionString="<%$ ConnectionStrings:Sherlock_TestConnectionString %>" SelectCommand="SP_VerCiudades" SelectCommandType="StoredProcedure">
                                                                    <SelectParameters>
                                                                        <asp:ControlParameter ControlID="ddlPNDpto2" DefaultValue="1" Name="IdDepartamento"
                                                                            PropertyName="SelectedValue" Type="Int32" />
                                                                    </SelectParameters>
                                                                </asp:SqlDataSource>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblPNTelefono2" runat="server" Text="TELEFONO"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="tbxPNTelefono2" runat="server" MaxLength="10"></asp:TextBox><asp:FilteredTextBoxExtender ID="ftbPNTelefono2" runat="server"
                                                                    TargetControlID="tbxPNTelefono2"
                                                                    FilterType="Numbers,Custom"
                                                                    ValidChars=""></asp:FilteredTextBoxExtender>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ErrorMessage="Solo números" Font-Bold="false"
                                                                    Font-Names="Calibri" Font-Size="Small" ForeColor="Red" ControlToValidate="tbxPNTelefono2" ValidationExpression="^[0-9]*\.?[0-9]+$" ValidationGroup="SaveKnowCliente"></asp:RegularExpressionValidator>
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
                                                                <asp:Label ID="lblPNServicio" runat="server" Text="QUE TIPO DE ACTIVIDAD Y/O SERVICIO COMERCIALIZA? (Independientes o comerciantes)"></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td style="margin-left: 40px">
                                                                <asp:TextBox ID="tbxPNServicio" runat="server" MaxLength="100" Width="505px"></asp:TextBox></td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblPNIngresosMensuales" runat="server" Text="INGRESOS MENSUALES (Pesos)"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="tbxPNIngresosMensuales" runat="server" MaxLength="20" Width="160px" AutoPostBack="True" OnTextChanged="tbxPNIngresosMensuales_TextChanged"></asp:TextBox><asp:RequiredFieldValidator ID="revPNIngresosMensuales" runat="server"
                                                                    ControlToValidate="tbxPNIngresosMensuales"
                                                                    ValidationGroup="vdgWillis"
                                                                    ErrorMessage="*"
                                                                    ForeColor="Red"> </asp:RequiredFieldValidator><asp:FilteredTextBoxExtender ID="ftbPNIngresosMensuales" runat="server"
                                                                        TargetControlID="tbxPNIngresosMensuales"
                                                                        FilterType="Numbers,Custom"
                                                                        ValidChars="$€,."></asp:FilteredTextBoxExtender>
                                                                <asp:RegularExpressionValidator ID="revPNIngresosMensualesNum" runat="server" ErrorMessage="Solo números" Font-Bold="false"
                                                                    Font-Names="Calibri" Font-Size="Small" ForeColor="Red" ControlToValidate="tbxPNIngresosMensuales" ValidationExpression="^[0-9]*\.?[0-9]+$" ValidationGroup="SaveKnowCliente"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblPNEgresoMensuales" runat="server" Text="EGRESO MENSUALES (Pesos)"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="tbxPNEgresoMensuales" runat="server" MaxLength="20" Width="160px" AutoPostBack="True" OnTextChanged="tbxPNEgresoMensuales_TextChanged"></asp:TextBox><asp:RequiredFieldValidator ID="rfvPNEgresoMensuales" runat="server"
                                                                    ControlToValidate="tbxPNEgresoMensuales"
                                                                    ValidationGroup="vdgWillis"
                                                                    ErrorMessage="*"
                                                                    ForeColor="Red"> </asp:RequiredFieldValidator><asp:FilteredTextBoxExtender ID="ftbPNEgresoMensuales" runat="server"
                                                                        TargetControlID="tbxPNEgresoMensuales"
                                                                        FilterType="Numbers,Custom"
                                                                        ValidChars="$€,."></asp:FilteredTextBoxExtender>
                                                                <asp:RegularExpressionValidator ID="fvpPNEgresoMensualesNum" runat="server" ErrorMessage="Solo números" Font-Bold="false"
                                                                    Font-Names="Calibri" Font-Size="Small" ForeColor="Red" ControlToValidate="tbxPNEgresoMensuales" ValidationExpression="^[0-9]*\.?[0-9]+$" ValidationGroup="SaveKnowCliente"></asp:RegularExpressionValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblPNActivos" runat="server" Text="ACTIVOS (Pesos)"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="tbxPNActivos" runat="server" MaxLength="20" Width="160px" AutoPostBack="True" OnTextChanged="tbxPNActivos_TextChanged"></asp:TextBox><asp:RequiredFieldValidator ID="rfvPNActivos" runat="server"
                                                                    ControlToValidate="tbxPNActivos"
                                                                    ValidationGroup="vdgWillis"
                                                                    ErrorMessage="*"
                                                                    ForeColor="Red"> </asp:RequiredFieldValidator><asp:FilteredTextBoxExtender ID="ftbPNActivos" runat="server"
                                                                        TargetControlID="tbxPNActivos"
                                                                        FilterType="Numbers,Custom"
                                                                        ValidChars="$€,."></asp:FilteredTextBoxExtender>
                                                                <asp:RegularExpressionValidator ID="revPNActivosNum" runat="server" ErrorMessage="Solo números" Font-Bold="false"
                                                                    Font-Names="Calibri" Font-Size="Small" ForeColor="Red" ControlToValidate="tbxPNActivos" ValidationExpression="^[0-9]*\.?[0-9]+$" ValidationGroup="SaveKnowCliente"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblPNPasivos" runat="server" Text="PASIVOS (Pesos)"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="tbxPNPasivos" runat="server" MaxLength="20" Width="160px" AutoPostBack="True" OnTextChanged="tbxPNPasivos_TextChanged"></asp:TextBox><asp:RequiredFieldValidator ID="revPNPasivos" runat="server"
                                                                    ControlToValidate="tbxPNPasivos"
                                                                    ValidationGroup="vdgWillis"
                                                                    ErrorMessage="*"
                                                                    ForeColor="Red"> </asp:RequiredFieldValidator><asp:FilteredTextBoxExtender ID="ftbPNPasivos" runat="server"
                                                                        TargetControlID="tbxPNPasivos"
                                                                        FilterType="Numbers,Custom"
                                                                        ValidChars="$€,."></asp:FilteredTextBoxExtender>
                                                                <asp:RegularExpressionValidator ID="revPNPasivosNum" runat="server" ErrorMessage="Solo números" Font-Bold="false"
                                                                    Font-Names="Calibri" Font-Size="Small" ForeColor="Red" ControlToValidate="tbxPNPasivos" ValidationExpression="^[0-9]*\.?[0-9]+$" ValidationGroup="SaveKnowCliente"></asp:RegularExpressionValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblPNPatrimonio" runat="server" Text="PATRIMONIO (Pesos)"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="tbxPNPatrimonio" runat="server" MaxLength="20" Width="160px" AutoPostBack="True" OnTextChanged="tbxPNPatrimonio_TextChanged"></asp:TextBox><asp:RequiredFieldValidator ID="revPNPatrimonio" runat="server"
                                                                    ControlToValidate="tbxPNPatrimonio"
                                                                    ValidationGroup="vdgWillis"
                                                                    ErrorMessage="*"
                                                                    ForeColor="Red"> </asp:RequiredFieldValidator><asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server"
                                                                        TargetControlID="tbxPNTelefono1"
                                                                        FilterType="Numbers,Custom"
                                                                        ValidChars="$€,."></asp:FilteredTextBoxExtender>
                                                                <asp:RegularExpressionValidator ID="revPNPatrimonioNum" runat="server" ErrorMessage="Solo números" Font-Bold="false"
                                                                    Font-Names="Calibri" Font-Size="Small" ForeColor="Red" ControlToValidate="tbxPNPatrimonio" ValidationExpression="^[0-9]*\.?[0-9]+$" ValidationGroup="SaveKnowCliente"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblPNOtrosIngresos" runat="server" Text="OTROS INGRESOS (Pesos)"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="tbxPNOtrosIngresos" runat="server" MaxLength="20" Width="160px" AutoPostBack="True" OnTextChanged="tbxPNOtrosIngresos_TextChanged"></asp:TextBox><asp:RequiredFieldValidator ID="rfvPNOtrosIngresos" runat="server"
                                                                    ControlToValidate="tbxPNOtrosIngresos"
                                                                    ValidationGroup="vdgWillis2"
                                                                    ErrorMessage="*"
                                                                    ForeColor="Red"> </asp:RequiredFieldValidator><asp:FilteredTextBoxExtender ID="ftbPNOtrosIngresos" runat="server"
                                                                        TargetControlID="tbxPNOtrosIngresos"
                                                                        FilterType="Numbers,Custom"
                                                                        ValidChars="$€,."></asp:FilteredTextBoxExtender>
                                                                <asp:RegularExpressionValidator ID="revPNOtrosIngresosNum" runat="server" ErrorMessage="Solo números" Font-Bold="false"
                                                                    Font-Names="Calibri" Font-Size="Small" ForeColor="Red" ControlToValidate="tbxPNOtrosIngresos" ValidationExpression="^[0-9]*\.?[0-9]+$" ValidationGroup="SaveKnowCliente"></asp:RegularExpressionValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblConceptoOtrosIngresos" runat="server" Text="CONCEPTO OTROS INGRESOS"></asp:Label>
                                                            </td>
                                                            <td colspan="3">
                                                                <asp:TextBox ID="tbxPNConceptoOtrosIngresos" runat="server" MaxLength="100" Width="455px"></asp:TextBox>
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
                                                                <asp:Label ID="lblPNPregunta1" runat="server" Text="POR SU CARGO O ACTIVIDAD MANEJA RECURSOS PUBLICOS?"></asp:Label>
                                                            </td>

                                                            <td>
                                                                <asp:DropDownList ID="ddlPNPregunta1" runat="server">
                                                                    <asp:ListItem Value="">---</asp:ListItem>
                                                                    <asp:ListItem Value="SI">SI</asp:ListItem>
                                                                    <asp:ListItem Value="NO">NO</asp:ListItem>
                                                                </asp:DropDownList><asp:RequiredFieldValidator ID="rfvPNPregunta1"
                                                                    ControlToValidate="ddlPNPregunta1"
                                                                    ValidationGroup="vdgWillis"
                                                                    ErrorMessage="*"
                                                                    ForeColor="Red"
                                                                    InitialValue="" runat="server"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblPNPregunta2" runat="server" Text="POR SU CARGO O ACTIVIDAD EJERCE ALGUN GRADO DE PODER PUBLICO?"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlPNPregunta2" runat="server">
                                                                    <asp:ListItem Value="">---</asp:ListItem>
                                                                    <asp:ListItem Value="SI">SI</asp:ListItem>
                                                                    <asp:ListItem Value="NO">NO</asp:ListItem>
                                                                </asp:DropDownList><asp:RequiredFieldValidator ID="frvPNPregunta2"
                                                                    ControlToValidate="ddlPNPregunta2"
                                                                    ValidationGroup="vdgWillis"
                                                                    ErrorMessage="*"
                                                                    ForeColor="Red"
                                                                    InitialValue="" runat="server"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblPNPregunta3" runat="server" Text="POR SU ACTIVIDAD U OFICIO, GOZA USTED DE RECONOCIMIENTO PUBLICO GENERAL?"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlPNPregunta3" runat="server">
                                                                    <asp:ListItem Value="">---</asp:ListItem>
                                                                    <asp:ListItem Value="SI">SI</asp:ListItem>
                                                                    <asp:ListItem Value="NO">NO</asp:ListItem>
                                                                </asp:DropDownList><asp:RequiredFieldValidator ID="frvPNPregunta3"
                                                                    ControlToValidate="ddlPNPregunta3"
                                                                    ValidationGroup="vdgWillis"
                                                                    ErrorMessage="*"
                                                                    ForeColor="Red"
                                                                    InitialValue="" runat="server"></asp:RequiredFieldValidator>
                                                            </td>

                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblPNPregunta4" runat="server" Text="EXISTE ALGÚN VINCULO ENTRE USTED Y UNA PERSONA CONSIDERADA PUBLICAMENTE EXPUESTA?"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlPNPregunta4" runat="server" AutoPostBack="True" >
                                                                    <asp:ListItem Value="">---</asp:ListItem>
                                                                    <asp:ListItem Value="SI">SI</asp:ListItem>
                                                                    <asp:ListItem Value="NO">NO</asp:ListItem>
                                                                </asp:DropDownList><asp:RequiredFieldValidator ID="rfvPNPregunta4"
                                                                    ControlToValidate="ddlPNPregunta4"
                                                                    ValidationGroup="vdgWillis"
                                                                    ErrorMessage="*"
                                                                    ForeColor="Red"
                                                                    InitialValue="" runat="server"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>

                                                    </table>
                                                </td>
                                            </tr>
                                            <tr runat="server" id="TrVinculoPPExpuestaPN" visible="false">
                                                <td>
                                                    <table>
                                                        <tr>
                                                            <td><asp:Label ID="LprimerapellidpPNPPE" runat="server" Text="PRIMER APELLIDO:"></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="TXprimerApellidoPNPPE" runat="server" MaxLength="80" Width="160px"></asp:TextBox>
                                                            </td>
                                                            <td><asp:Label ID="LsegundoApellidoPNPPE" runat="server" Text="SEGUNDO APELLIDO:"></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="TXsegundApellidoPNPPE" runat="server" MaxLength="80" Width="160px"></asp:TextBox>
                                                            </td>
                                                            <td><asp:Label ID="LnombresPNPPE" runat="server" Text="NOMBRES:"></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="TXnombrePNPPE" runat="server" MaxLength="80" Width="160px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td><asp:Label ID="LocupacionPNPPE" runat="server" Text="OCUPACION:"></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="TXocupacionPNPPE" runat="server" MaxLength="80" Width="160px"></asp:TextBox>
                                                            </td>
                                                            <td><asp:Label ID="LcargoPNPPE" runat="server" Text="CARGO:"></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="TXcargoPNPPE" runat="server" MaxLength="80" Width="160px"></asp:TextBox>
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
                                                                <asp:Label ID="lblPNEspecificacionPreguntas" runat="server" Text="INDIQUE"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="tbxPNEspecificacionPreguntas" runat="server" MaxLength="100" Width="320px"></asp:TextBox><asp:RequiredFieldValidator ID="rfvPNEspecificacionPreguntas" runat="server"
                                                                    ControlToValidate="tbxPNEspecificacionPreguntas"
                                                                    ValidationGroup="vdgWillis"
                                                                    ErrorMessage="*"
                                                                    ForeColor="Red">
                                                                </asp:RequiredFieldValidator>
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
                                                                <asp:Label ID="lblPNPregunta5" runat="server" Text="ES USTED SUJETO DE OBLIGACIONES TRIBUTARIAS EN OTRO PAÍS O GRUPO DE PAISES?"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlPNPregunta5" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlPNPregunta5_SelectedIndexChanged">
                                                                    <asp:ListItem Value="">---</asp:ListItem>
                                                                    <asp:ListItem Value="SI">SI</asp:ListItem>
                                                                    <asp:ListItem Value="NO">NO</asp:ListItem>
                                                                </asp:DropDownList><asp:RequiredFieldValidator ID="rfvPNPregunta5"
                                                                    ControlToValidate="ddlPNPregunta5"
                                                                    ValidationGroup="vdgWillis"
                                                                    ErrorMessage="*"
                                                                    ForeColor="Red"
                                                                    InitialValue="" runat="server"></asp:RequiredFieldValidator>
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
                                                                <asp:Label ID="lblPNEspecificacionPreguntas2" runat="server" Text="INDIQUE"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="tbxPNEspecificacionPreguntas2" runat="server" MaxLength="100" Width="320px"></asp:TextBox><asp:RequiredFieldValidator ID="rfvPNEspecificacionPreguntas2" runat="server"
                                                                    ControlToValidate="tbxPNEspecificacionPreguntas2"
                                                                    ValidationGroup="vdgWillis"
                                                                    ErrorMessage="*"
                                                                    ForeColor="Red">
                                                                </asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr id="infoTituloPJ" visible="false" runat="server">
                                    <td>
                                        <table width="100%">
                                            <tr align="center">
                                                <td style="background-color: #C0C0C0">
                                                    <asp:Label ID="Label49" runat="server" Text="1. INFORMACIÓN BASICA"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr id="infoPJ" visible="false" runat="server">
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <table witdh="100%">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblPJRazonDenominacion" runat="server" Text="RAZÓN O DENOMINACIÓN SOCIAL"></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="tbxPJRazonDenominacion" runat="server" Width="400px" MaxLength="80"></asp:TextBox><asp:RequiredFieldValidator ID="frvPJRazonDenominacion" runat="server"
                                                                    ControlToValidate="tbxPJRazonDenominacion"
                                                                    ValidationGroup="vdgWillis"
                                                                    ErrorMessage="*"
                                                                    ForeColor="Red">
                                                                </asp:RequiredFieldValidator>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblPJNIT" runat="server" Text="NIT"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="tbxPJNIT" runat="server" MaxLength="15"></asp:TextBox><asp:RequiredFieldValidator ID="rfvPJNIT" runat="server"
                                                                    ControlToValidate="tbxPJNIT"
                                                                    ValidationGroup="vdgWillis"
                                                                    ErrorMessage="*"
                                                                    ForeColor="Red"></asp:RequiredFieldValidator>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblPJDV" runat="server" Text="DV"></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="tbxPJDV" runat="server" MaxLength="1"></asp:TextBox>
                                                                <asp:FilteredTextBoxExtender ID="ftbPJDV" runat="server"
                                                                    TargetControlID="tbxPJDV"
                                                                    FilterType="Numbers,Custom"
                                                                    ValidChars=""></asp:FilteredTextBoxExtender>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table witdh="100%">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lPJtipoDocumentoPJempresa" runat="server" Text="TIPO DOCUMENTO"></asp:Label></td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlTipoDocumentoPJempresa" runat="server">
                                                                    <asp:ListItem Value="">---</asp:ListItem>
                                                                    <asp:ListItem Value="C.C.">C.C.</asp:ListItem>
                                                                    <asp:ListItem Value="C.E.">C.E.</asp:ListItem>
                                                                    <asp:ListItem Value="NIT">NIT</asp:ListItem>
                                                                    <asp:ListItem Value="NUIP">NUIP</asp:ListItem>
                                                                    <asp:ListItem Value="PASAPORTE">PASAPORTE</asp:ListItem>
                                                                    <asp:ListItem Value="T.I.">T.I.</asp:ListItem>
                                                                    <asp:ListItem Value="OTROS SIN">OTROS SIN</asp:ListItem>
                                                                </asp:DropDownList></td>
                                                            <td>
                                                                <asp:Label ID="lblPJDireccionOficina" runat="server" Text="OFICINA PRINCIPAL: DIRECCIÓN"></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="tbxPJDireccionOficina" runat="server" Width="280px" MaxLength="150"></asp:TextBox><asp:RequiredFieldValidator ID="rfvPJDireccionOficina" runat="server"
                                                                    ControlToValidate="tbxPJDireccionOficina"
                                                                    ValidationGroup="vdgWillis"
                                                                    ErrorMessage="*"
                                                                    ForeColor="Red">
                                                                </asp:RequiredFieldValidator>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblPJTipoEmpresa" runat="server" Text="TIPO DE EMPRESA"></asp:Label></td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlPJTipoEmpresa" runat="server">
                                                                    <asp:ListItem Value="">---</asp:ListItem>
                                                                    <asp:ListItem Value="MIXTA">MIXTA</asp:ListItem>
                                                                    <asp:ListItem Value="OFICINA DE REPRESENTACION">OFICINA DE REPRESENTACION</asp:ListItem>
                                                                    <asp:ListItem Value="PRIVADA">PRIVADA</asp:ListItem>
                                                                    <asp:ListItem Value="PÚBLICA">PÚBLICA</asp:ListItem>
                                                                    <asp:ListItem Value="SIN ANIMO DE LUCRO">SIN ANIMO DE LUCRO</asp:ListItem>
                                                                    <asp:ListItem Value="SOCIEDAD EXTRAJERA">SOCIEDAD EXTRAJERA</asp:ListItem>
                                                                </asp:DropDownList><asp:RequiredFieldValidator ID="rfvPJTipoEmpresa"
                                                                    ControlToValidate="ddlPJTipoEmpresa"
                                                                    ValidationGroup="vdgWillis"
                                                                    ErrorMessage="*"
                                                                    ForeColor="Red"
                                                                    InitialValue="" runat="server">
                                                                </asp:RequiredFieldValidator></td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table witdh="100%">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblPJCodCIIU2" runat="server" Text="CIIU"></asp:Label></td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlPJCodCIIU2" runat="server" DataSourceID="sdsPJCodCIIU2"
                                                                    DataTextField="Descripcion" DataValueField="CIIU" AutoPostBack="true" OnSelectedIndexChanged="ddlPJCodCIIU2_SelectedIndexChanged">
                                                                </asp:DropDownList><asp:RequiredFieldValidator ID="rfvPJCodCIIU2"
                                                                    ControlToValidate="ddlPJCodCIIU2"
                                                                    ValidationGroup="vdgWillis"
                                                                    ErrorMessage="*"
                                                                    ForeColor="Red"
                                                                    InitialValue="0" runat="server" />
                                                                <asp:SqlDataSource ID="sdsPJCodCIIU2" runat="server" ConnectionString="<%$ ConnectionStrings:Sherlock_TestConnectionString %>" SelectCommand="Parametrizacion.spCIIU" SelectCommandType="StoredProcedure"></asp:SqlDataSource>

                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblPJCIIU" runat="server" Text="CIIU (Cód.)"></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="tbxPJCIIU" runat="server" MaxLength="10" Enabled="false"></asp:TextBox>
                                                                <asp:DropDownList ID="DDLPJcodCiiu" runat="server" DataSourceID="sqlDSPJciiuCod"
                                                                    DataTextField="CIIU" DataValueField="CIIU" AutoPostBack="true"  >
                                                                </asp:DropDownList><asp:RequiredFieldValidator ID="rfvCodCiiuPJ"
                                                                    ControlToValidate="DDLPJcodCiiu"
                                                                    ValidationGroup="vdgWillis"
                                                                    ErrorMessage="*"
                                                                    ForeColor="Red"
                                                                    InitialValue="0" runat="server" />
                                                                <asp:SqlDataSource ID="sqlDSPJciiuCod" runat="server" ConnectionString="<%$ ConnectionStrings:Sherlock_TestConnectionString %>" SelectCommand="Parametrizacion.spCodCIIU" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                                                            
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblPJActividadEconomica" runat="server" Text="SECTOR"></asp:Label></td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlPJActividadEconomica" runat="server">
                                                                    <asp:ListItem Value="">---</asp:ListItem>
                                                                    <asp:ListItem Value="AGROPECUARIO">AGROPECUARIO</asp:ListItem>
                                                                    <asp:ListItem Value="COMERCIO">COMERCIO</asp:ListItem>
                                                                    <asp:ListItem Value="CONSTRUCCION">CONSTRUCCION</asp:ListItem>
                                                                    <asp:ListItem Value="FINANCIERO">FINANCIERO</asp:ListItem>
                                                                    <asp:ListItem Value="INDUSTRIAL">INDUSTRIAL</asp:ListItem>
                                                                    <asp:ListItem Value="MINERO Y ENERGETICO">MINERO Y ENERGETICO</asp:ListItem>
                                                                    <asp:ListItem Value="SERVICIOS">SERVICIOS</asp:ListItem>
                                                                    <asp:ListItem Value="SOLIDARIO">SOLIDARIO</asp:ListItem>
                                                                    <asp:ListItem Value="TRANSPORTE">TRANSPORTE</asp:ListItem>

                                                                </asp:DropDownList><asp:RequiredFieldValidator ID="rfvPJActividadEconomica"
                                                                    ControlToValidate="ddlPJActividadEconomica"
                                                                    ValidationGroup="vdgWillis2"
                                                                    ErrorMessage="*"
                                                                    ForeColor="Red"
                                                                    InitialValue="" runat="server">
                                                                </asp:RequiredFieldValidator></td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblPJDpto" runat="server" Text="DEPARTAMENTO"></asp:Label></td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlPJDpto" runat="server" DataSourceID="sdsPJDpto"
                                                                    DataTextField="NombreDepartamento" DataValueField="IdDepartamento" AutoPostBack="True">
                                                                </asp:DropDownList>
                                                                <asp:SqlDataSource ID="sdsPJDpto" runat="server" ConnectionString="<%$ ConnectionStrings:Sherlock_TestConnectionString %>" SelectCommand="SP_VerDepartamentos" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblPJCiudad1" runat="server" Text="CIUDAD"></asp:Label></td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlPJCiudad1" runat="server" DataSourceID="sdsPJCiudad1"
                                                                    DataTextField="NombreCiudad" DataValueField="IdCiudad" Width="250px">
                                                                </asp:DropDownList>
                                                                <asp:SqlDataSource ID="sdsPJCiudad1" runat="server"
                                                                    ConnectionString="<%$ ConnectionStrings:Sherlock_TestConnectionString %>" SelectCommand="SP_VerCiudades" SelectCommandType="StoredProcedure">
                                                                    <SelectParameters>
                                                                        <asp:ControlParameter ControlID="ddlPJDpto" DefaultValue="1" Name="IdDepartamento"
                                                                            PropertyName="SelectedValue" Type="Int32" />
                                                                    </SelectParameters>
                                                                </asp:SqlDataSource>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblPJTelefono1" runat="server" Text="TELÉFONO"></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="tbxPJTelefono1" runat="server" Width="70px" MaxLength="10"></asp:TextBox><asp:FilteredTextBoxExtender ID="ftbPJTelefono1" runat="server"
                                                                    TargetControlID="tbxPJTelefono1"
                                                                    FilterType="Numbers,Custom"
                                                                    ValidChars=""></asp:FilteredTextBoxExtender>
                                                                <asp:RegularExpressionValidator ID="revPJTelefono1Num" runat="server" ErrorMessage="Solo números" Font-Bold="false"
                                                                    Font-Names="Calibri" Font-Size="Small" ForeColor="Red" ControlToValidate="tbxPJTelefono1" ValidationExpression="^[0-9]*\.?[0-9]+$" ValidationGroup="SaveKnowCliente"></asp:RegularExpressionValidator>
                                                                <asp:RequiredFieldValidator ID="rfvPJTelefono1" runat="server"
                                                                    ControlToValidate="tbxPJTelefono1"
                                                                    ValidationGroup="vdgWillis2"
                                                                    ErrorMessage="*"
                                                                    ForeColor="Red">
                                                                </asp:RequiredFieldValidator></td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblPJCorreoPrincipal" runat="server" Width="60px" Text="E-MAIL"></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="tbxPJCorreoPrincipal" runat="server" Width="280px" MaxLength="100"></asp:TextBox></td>
                                                            <td>
                                                                <asp:Label ID="lblPJDireccionSucursal" runat="server" Width="80px" Text="SUCURSAL O AGENCIA: DIRECCIÓN"></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="tbxPJDireccionSucursal" runat="server" Width="280px" MaxLength="100"></asp:TextBox></td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblPJDpto2" runat="server" Text="DEPARTAMENTO"></asp:Label></td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlPJDpto2" runat="server" DataSourceID="sdsddlPJDpto2"
                                                                    DataTextField="NombreDepartamento" DataValueField="IdDepartamento" AutoPostBack="True">
                                                                </asp:DropDownList>
                                                                <asp:SqlDataSource ID="sdsddlPJDpto2" runat="server" ConnectionString="<%$ ConnectionStrings:Sherlock_TestConnectionString %>" SelectCommand="SP_VerDepartamentos" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblPJCiudad2" runat="server" Text="CIUDAD"></asp:Label></td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlPJCiudad2" runat="server" DataSourceID="sdsPJCiudad2"
                                                                    DataTextField="NombreCiudad" DataValueField="IdCiudad" Width="250px">
                                                                </asp:DropDownList>
                                                                <asp:SqlDataSource ID="sdsPJCiudad2" runat="server" ConnectionString="<%$ ConnectionStrings:Sherlock_TestConnectionString %>" SelectCommand="SP_VerCiudades" SelectCommandType="StoredProcedure">
                                                                    <SelectParameters>
                                                                        <asp:ControlParameter ControlID="ddlPJDpto2" DefaultValue="1" Name="IdDepartamento"
                                                                            PropertyName="SelectedValue" Type="Int32" />
                                                                    </SelectParameters>
                                                                </asp:SqlDataSource>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblPJTelefono2" runat="server" Text="TELÉFONO"></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="tbxPJTelefono2" runat="server" Width="70px" MaxLength="10"></asp:TextBox><asp:FilteredTextBoxExtender ID="ftbPJTelefono2" runat="server"
                                                                    TargetControlID="tbxPJTelefono2"
                                                                    FilterType="Numbers,Custom"
                                                                    ValidChars=""></asp:FilteredTextBoxExtender>
                                                                <asp:RegularExpressionValidator ID="revPJTelefono2Num" runat="server" ErrorMessage="Solo números" Font-Bold="false"
                                                                    Font-Names="Calibri" Font-Size="Small" ForeColor="Red" ControlToValidate="tbxPJTelefono2" ValidationExpression="^[0-9]*\.?[0-9]+$" ValidationGroup="SaveKnowCliente"></asp:RegularExpressionValidator>
                                                                <asp:RequiredFieldValidator ID="rfvPJTelefono2" runat="server"
                                                                    ControlToValidate="tbxPJTelefono2"
                                                                    ValidationGroup="vdgWillis2"
                                                                    ErrorMessage="*"
                                                                    ForeColor="Red">
                                                                </asp:RequiredFieldValidator></td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblPJPrimerApellido" runat="server" Text="REPRESENTANTE LEGAL: PRIMER APELLIDO"></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="tbxPJPrimerApellido" runat="server" MaxLength="20"></asp:TextBox><asp:RequiredFieldValidator ID="rfvPJPrimerApellido" runat="server"
                                                                    ControlToValidate="tbxPJPrimerApellido"
                                                                    ValidationGroup="vdgWillis"
                                                                    ErrorMessage="*"
                                                                    ForeColor="Red">
                                                                </asp:RequiredFieldValidator>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblPJSegundoApellido" runat="server" Text="SEGUNDO APELLIDO"></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="tbxPJSegundoApellido" runat="server" MaxLength="20"></asp:TextBox><asp:RequiredFieldValidator ID="rfvPJSegundoApellido" runat="server"
                                                                    ControlToValidate="tbxPJSegundoApellido"
                                                                    ValidationGroup="vdgWillis"
                                                                    ErrorMessage="*"
                                                                    ForeColor="Red">
                                                                </asp:RequiredFieldValidator>
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
                                                                <asp:Label ID="lblPJNombres" runat="server" Text="NOMBRES"></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="tbxPJNombres" runat="server" MaxLength="45"></asp:TextBox><asp:RequiredFieldValidator ID="rfvPJNombres" runat="server"
                                                                    ControlToValidate="tbxPJNombres"
                                                                    ValidationGroup="vdgWillis"
                                                                    ErrorMessage="*"
                                                                    ForeColor="Red">
                                                                </asp:RequiredFieldValidator>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblPJTipoDocumento" runat="server" Text="TIPO DOCUMENTO"></asp:Label></td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlPJTipoDocumento" runat="server">
                                                                    <asp:ListItem Value="">---</asp:ListItem>
                                                                    <asp:ListItem Value="C.C.">C.C.</asp:ListItem>
                                                                    <asp:ListItem Value="C.E.">C.E.</asp:ListItem>
                                                                    <asp:ListItem Value="NIT">NIT</asp:ListItem>
                                                                    <asp:ListItem Value="NUIP">NUIP</asp:ListItem>
                                                                    <asp:ListItem Value="PASAPORTE">PASAPORTE</asp:ListItem>
                                                                    <asp:ListItem Value="T.I.">T.I.</asp:ListItem>
                                                                    <asp:ListItem Value="OTROS SIN">OTROS SIN</asp:ListItem>
                                                                </asp:DropDownList></td>
                                                            <td>
                                                                <asp:Label ID="lblPJNumeroDocumento" runat="server" Text="NÚMERO"></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="tbxPJNumeroDocumento" runat="server" MaxLength="15"></asp:TextBox><asp:RequiredFieldValidator ID="rfvPJNumeroDocumento" runat="server"
                                                                    ControlToValidate="tbxPJNumeroDocumento"
                                                                    ValidationGroup="vdgWillis"
                                                                    ErrorMessage="*"
                                                                    ForeColor="Red">
                                                                </asp:RequiredFieldValidator>
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
                                                                <asp:Label ID="lblPJFechaExpedicion" runat="server" Text="FECHA DE EXPEDICIÓN"></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="tbxPJFechaExpedicion" runat="server" MaxLength="20" Width="200px"></asp:TextBox>
                                                                <asp:CalendarExtender ID="cePJFechaExpedicion" runat="server" TargetControlID="tbxPJFechaExpedicion"
                                                                    Format="dd-MMM-yyyy"></asp:CalendarExtender>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblPJLugarExpedicion" runat="server" Text="LUGAR DE EXPEDICIÓN"></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="tbxPJLugarExpedicion" runat="server" MaxLength="20" Width="200px"></asp:TextBox></td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblPJFechaConstitucion" runat="server" Text="FECHA DE NACIMIENTO"></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="tbxPJFechaConstitucion" runat="server" MaxLength="20" Width="200px"></asp:TextBox>
                                                                <asp:CalendarExtender ID="cePJFechaConstitucion" runat="server" TargetControlID="tbxPJFechaConstitucion"
                                                                    Format="dd-MMM-yyyy"></asp:CalendarExtender>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblPJLugarNmto" runat="server" Text="LUGAR DE NACIMIENTO"></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="tbxPJLugarNmto" runat="server" MaxLength="20" Width="200px"></asp:TextBox></td>
                                                            <td>
                                                                <asp:Label ID="lblPJNacionalidad1" runat="server" Text="NACIONALIDAD"></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="tbxPJNacionalidad1" runat="server" MaxLength="80" Width="100px"></asp:TextBox></td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblPJPregunta1" runat="server" Text="POR SU CARGO O ACTIVIDAD MANEJA RECURSOS PUBLICOS?"></asp:Label></td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlPJPregunta1" runat="server">
                                                                    <asp:ListItem Value="">---</asp:ListItem>
                                                                    <asp:ListItem Value="SI">SI</asp:ListItem>
                                                                    <asp:ListItem Value="NO">NO</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvPJPregunta1"
                                                                    ControlToValidate="ddlPJPregunta1"
                                                                    ValidationGroup="vdgWillis"
                                                                    ErrorMessage="*"
                                                                    ForeColor="Red"
                                                                    InitialValue="" runat="server"></asp:RequiredFieldValidator></td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblPJPregunta2" runat="server" Text="POR SU CARGO O ACTIVIDAD EJERCE ALGUN GRADO DE PODER PUBLICO?"></asp:Label></td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlPJPregunta2" runat="server">
                                                                    <asp:ListItem Value="">---</asp:ListItem>
                                                                    <asp:ListItem Value="SI">SI</asp:ListItem>
                                                                    <asp:ListItem Value="NO">NO</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="frvPJPregunta2"
                                                                    ControlToValidate="ddlPJPregunta2"
                                                                    ValidationGroup="vdgWillis"
                                                                    ErrorMessage="*"
                                                                    ForeColor="Red"
                                                                    InitialValue="" runat="server"></asp:RequiredFieldValidator></td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblPJPregunta5" runat="server" Text="¿POR SU ACTIVIDAD U OFICIO, GOZA USTED DE RECONOCIMIENTO PUBLICO GENERAL?"></asp:Label></td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlPJPregunta5" runat="server">
                                                                    <asp:ListItem Value="">---</asp:ListItem>
                                                                    <asp:ListItem Value="SI">SI</asp:ListItem>
                                                                    <asp:ListItem Value="NO">NO</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvPJPregunta5"
                                                                    ControlToValidate="ddlPJPregunta5"
                                                                    ValidationGroup="vdgWillis"
                                                                    ErrorMessage="*"
                                                                    ForeColor="Red"
                                                                    InitialValue="" runat="server"></asp:RequiredFieldValidator></td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblPJPregunta3" runat="server" Text="¿POSEE PARTICIPACIÓN SUPERIOR AL 5%?"></asp:Label></td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlPJPregunta3" runat="server">
                                                                    <asp:ListItem Value="">---</asp:ListItem>
                                                                    <asp:ListItem Value="SI">SI</asp:ListItem>
                                                                    <asp:ListItem Value="NO">NO</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="frvPJPregunta3"
                                                                    ControlToValidate="ddlPJPregunta3"
                                                                    ValidationGroup="vdgWillis"
                                                                    ErrorMessage="*"
                                                                    ForeColor="Red"
                                                                    InitialValue="" runat="server"></asp:RequiredFieldValidator></td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblPJPregunta4" runat="server" Text="EXISTE ALGÚN VINCULO ENTRE USTED Y UNA PERSONA CONSIDERADA PUBLICAMENTE EXPUESTA?"></asp:Label></td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlPJPregunta4" runat="server" AutoPostBack="true" >
                                                                    <asp:ListItem Value="">---</asp:ListItem>
                                                                    <asp:ListItem Value="SI">SI</asp:ListItem>
                                                                    <asp:ListItem Value="NO">NO</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvPJPregunta4"
                                                                    ControlToValidate="ddlPJPregunta4"
                                                                    ValidationGroup="vdgWillis"
                                                                    ErrorMessage="*"
                                                                    ForeColor="Red"
                                                                    InitialValue="" runat="server"></asp:RequiredFieldValidator></td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr runat="server" id="TrVinculoPPExpuestaPJ" visible="false">
                                                <td>
                                                    <table>
                                                        <tr>
                                                            <td><asp:Label ID="LPrimerApellidoPPE" runat="server" Text="PRIMER APELLIDO:"></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="TXprimerApellidoPPE" runat="server" MaxLength="80" Width="160px"></asp:TextBox>
                                                            </td>
                                                            <td><asp:Label ID="LSegundoApellidPPE" runat="server" Text="SEGUNDO APELLIDO:"></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="TXsegundoApellidoPPE" runat="server" MaxLength="80" Width="160px"></asp:TextBox>
                                                            </td>
                                                            <td><asp:Label ID="LnombresPPE" runat="server" Text="NOMBRES:"></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="TXnombresPPE" runat="server" MaxLength="80" Width="160px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td><asp:Label ID="LOcupacionPPE" runat="server" Text="OCUPACION:"></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="TXocipacionPPE" runat="server" MaxLength="80" Width="160px"></asp:TextBox>
                                                            </td>
                                                            <td><asp:Label ID="LcargoPPE" runat="server" Text="CARGO:"></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="TXcargoPPE" runat="server" MaxLength="80" Width="160px"></asp:TextBox>
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
                                                                <asp:Label ID="lblPJEspecificacionPreguntas" runat="server" Text="INDIQUE"></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="tbxPJEspecificacionPreguntas" runat="server" MaxLength="80" Width="160px"></asp:TextBox></td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr style="background-color: #DDDDDD">
                                                <td>
                                                    <asp:Label ID="Label71" runat="server" Text="IDENTIFICACIÓN DE LOS ACCIONISTAS O ASOCIADOS QUE TENGAN DIRECTA O INDIRECTAMENTE MAS DEL 5% DEL CAPITAL SOCIAL, APORTE O PARTICIPACIÓN (EN CASO DE REQUERIR MAS ESPACIO DEBE ANEXARSE LA RELACIÓN)"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table>
                                                        <tr>
                                                            <td class="auto-style9">
                                                                <asp:Label ID="Label73" runat="server" Text="TIPO ID" CssClass="auto-style10"></asp:Label></td>
                                                            <td class="auto-style9">
                                                                <asp:Label ID="Label74" runat="server" Text="NUMERO ID" CssClass="auto-style10"></asp:Label></td>
                                                            <td class="auto-style9">
                                                                <asp:Label ID="Label72" runat="server" Text="NOMBRE" CssClass="auto-style10"></asp:Label></td>
                                                            <td class="auto-style9">
                                                                <asp:Label ID="Label5" runat="server" Text="% Participación" CssClass="auto-style10"></asp:Label></td>
                                                            <td class="auto-style9">
                                                                <asp:Label ID="Label" runat="server" Text="¿ POR SU ACTIVIDAD O CARGO, ADMINISTRA RECURSOS PUBLICOS?" CssClass="auto-style10"></asp:Label></td>
                                                            <td class="auto-style9">
                                                                <asp:Label ID="Label3" runat="server" Text="¿POR SU CARGO O ACTIVIDAD, EJERCE ALGUN GRADO DE PODER PUBLICO?" CssClass="auto-style10"></asp:Label></td>
                                                            <td class="auto-style9">
                                                                <asp:Label ID="Label4" runat="server" Text="POR SU ACTIVIDAD U OFICIO,¿GOZA USTED DE RECONOCIMIENTO PUBLICO?" CssClass="auto-style10"></asp:Label></td>
                                                            <td class="auto-style9">
                                                                <asp:Label ID="Label6" runat="server" Text="¿ESTA USTED OBLIGADO A DECLARACIÓN TRIBUTARIA EN OTRO PAIS O GRUPO DE PAISES?. INDIQUE CUAL (ES)" CssClass="auto-style10"></asp:Label></td>
                                                        </tr>
                                                        <tr valign="top">
                                                            <td>
                                                                <asp:DropDownList ID="ddlPJTipoDocRepLegalPpal" runat="server">
                                                                    <asp:ListItem Value="">---</asp:ListItem>
                                                                    <asp:ListItem Value="C.C.">C.C.</asp:ListItem>
                                                                    <asp:ListItem Value="C.E.">C.E.</asp:ListItem>
                                                                    <asp:ListItem Value="NIT">NIT</asp:ListItem>
                                                                    <asp:ListItem Value="NUIP">NUIP</asp:ListItem>
                                                                    <asp:ListItem Value="T.I.">T.I.</asp:ListItem>
                                                                    <asp:ListItem Value="PASAPORTE">PASAPORTE</asp:ListItem>
                                                                    <asp:ListItem Value="PASE DIPLOMATICO">PASE DIPLOMATICO</asp:ListItem>
                                                                </asp:DropDownList><asp:RequiredFieldValidator ID="rfvPJTipoDocRepLegalPpal"
                                                                    ControlToValidate="ddlPJTipoDocRepLegalPpal"
                                                                    ValidationGroup="vdgWillis"
                                                                    ErrorMessage="*"
                                                                    ForeColor="Red"
                                                                    InitialValue="" runat="server">
                                                                </asp:RequiredFieldValidator></td>
                                                            <td>
                                                                <asp:TextBox ID="tbxPJDocumentoRepLegalPpal" runat="server" MaxLength="15"></asp:TextBox><asp:RequiredFieldValidator ID="rfvPJDocumentoRepLegalPpal" runat="server"
                                                                    ControlToValidate="tbxPJDocumentoRepLegalPpal"
                                                                    ValidationGroup="vdgWillis"
                                                                    ErrorMessage="*"
                                                                    ForeColor="Red">
                                                                </asp:RequiredFieldValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="tbxPJNombresRepLegalPpal" runat="server" Width="230px" MaxLength="80"></asp:TextBox><asp:RequiredFieldValidator ID="rfvPJNombresRepLegalPpal" runat="server"
                                                                    ControlToValidate="tbxPJNombresRepLegalPpal"
                                                                    ValidationGroup="vdgWillis"
                                                                    ErrorMessage="*"
                                                                    ForeColor="Red">
                                                                </asp:RequiredFieldValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="tbxPJParticipacion1" runat="server" MaxLength="10" Width="100px"></asp:TextBox><asp:RequiredFieldValidator ID="rfvPJParticipacion1" runat="server"
                                                                    ControlToValidate="tbxPJParticipacion1"
                                                                    ValidationGroup="vdgWillis"
                                                                    ErrorMessage="*"
                                                                    ForeColor="Red">
                                                                </asp:RequiredFieldValidator><asp:FilteredTextBoxExtender ID="ftbPJParticipacion1" runat="server"
                                                                    TargetControlID="tbxPJParticipacion1"
                                                                    FilterType="Numbers,Custom"
                                                                    ValidChars=""></asp:FilteredTextBoxExtender>
                                                                <asp:RegularExpressionValidator ID="tbxPJParticipacion1Num" runat="server" ErrorMessage="Solo números" Font-Bold="false"
                                                                    Font-Names="Calibri" Font-Size="Small" ForeColor="Red" ControlToValidate="tbxPJParticipacion1" ValidationExpression="^[0-9]*\.?[0-9]+$" ValidationGroup="SaveKnowCliente"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td class="auto-style5">
                                                                <asp:DropDownList ID="ddlPJPreguntaRepPpalLegal1" runat="server">
                                                                    <asp:ListItem Value="">---</asp:ListItem>
                                                                    <asp:ListItem Value="SI">SI</asp:ListItem>
                                                                    <asp:ListItem Value="NO">NO</asp:ListItem>
                                                                </asp:DropDownList><asp:RequiredFieldValidator ID="rfvPJPreguntaRepPpalLegal1"
                                                                    ControlToValidate="ddlPJPreguntaRepPpalLegal1"
                                                                    ValidationGroup="vdgWillis"
                                                                    ErrorMessage="*"
                                                                    ForeColor="Red"
                                                                    InitialValue="" runat="server">
                                                                </asp:RequiredFieldValidator></td>
                                                            <td class="auto-style5">
                                                                <asp:DropDownList ID="ddlPJPreguntaRepPpalLegal2" runat="server">
                                                                    <asp:ListItem Value="">---</asp:ListItem>
                                                                    <asp:ListItem Value="SI">SI</asp:ListItem>
                                                                    <asp:ListItem Value="NO">NO</asp:ListItem>
                                                                </asp:DropDownList><asp:RequiredFieldValidator ID="rfvPJPreguntaRepPpalLegal2"
                                                                    ControlToValidate="ddlPJPreguntaRepPpalLegal2"
                                                                    ValidationGroup="vdgWillis"
                                                                    ErrorMessage="*"
                                                                    ForeColor="Red"
                                                                    InitialValue="" runat="server">
                                                                </asp:RequiredFieldValidator></td>
                                                            <td class="auto-style5">
                                                                <asp:DropDownList ID="ddlPJPreguntaRepPpalLegal3" runat="server">
                                                                    <asp:ListItem Value="">---</asp:ListItem>
                                                                    <asp:ListItem Value="SI">SI</asp:ListItem>
                                                                    <asp:ListItem Value="NO">NO</asp:ListItem>
                                                                </asp:DropDownList><asp:RequiredFieldValidator ID="rfvPJPreguntaRepPpalLegal3"
                                                                    ControlToValidate="ddlPJPreguntaRepPpalLegal3"
                                                                    ValidationGroup="vdgWillis"
                                                                    ErrorMessage="*"
                                                                    ForeColor="Red"
                                                                    InitialValue="" runat="server">
                                                                </asp:RequiredFieldValidator></td>
                                                            <td class="auto-style5">
                                                                <asp:DropDownList ID="ddlPJPreguntaRepPpalLegal4" runat="server">
                                                                    <asp:ListItem Value="">---</asp:ListItem>
                                                                    <asp:ListItem Value="SI">SI</asp:ListItem>
                                                                    <asp:ListItem Value="NO">NO</asp:ListItem>
                                                                </asp:DropDownList><asp:RequiredFieldValidator ID="rfvPJPreguntaRepPpalLegal4"
                                                                    ControlToValidate="ddlPJPreguntaRepPpalLegal4"
                                                                    ValidationGroup="vdgWillis"
                                                                    ErrorMessage="*"
                                                                    ForeColor="Red"
                                                                    InitialValue="" runat="server">
                                                                </asp:RequiredFieldValidator></td>
                                                        </tr>
                                                        <tr valign="top">
                                                            <td>
                                                                <asp:DropDownList ID="ddlPJTipoDocRepLegal1" runat="server">
                                                                    <asp:ListItem Value="">---</asp:ListItem>
                                                                    <asp:ListItem Value="C.C.">C.C.</asp:ListItem>
                                                                    <asp:ListItem Value="C.E.">C.E.</asp:ListItem>
                                                                    <asp:ListItem Value="NIT">NIT</asp:ListItem>
                                                                    <asp:ListItem Value="NUIP">NUIP</asp:ListItem>
                                                                    <asp:ListItem Value="T.I.">T.I.</asp:ListItem>
                                                                    <asp:ListItem Value="PASAPORTE">PASAPORTE</asp:ListItem>
                                                                    <asp:ListItem Value="PASE DIPLOMATICO">PASE DIPLOMATICO</asp:ListItem>
                                                                </asp:DropDownList><asp:RequiredFieldValidator ID="rfvPJTipoDocRepLegal1"
                                                                    ControlToValidate="ddlPJTipoDocRepLegal1"
                                                                    ValidationGroup="vdgWillis"
                                                                    Enabled="false"
                                                                    ErrorMessage="*"
                                                                    ForeColor="Red"
                                                                    InitialValue="" runat="server">
                                                                </asp:RequiredFieldValidator></td>
                                                            <td>
                                                                <asp:TextBox ID="tbxPJDocumentoRepLegal1" runat="server" MaxLength="15"></asp:TextBox></td>
                                                            <td>
                                                                <asp:TextBox ID="tbxPJNombresRepLegal1" runat="server" Width="230px" MaxLength="80"></asp:TextBox></td>
                                                            <td>
                                                                <asp:TextBox ID="tbxPJParticipacion2" runat="server" MaxLength="10" Width="100px"></asp:TextBox><asp:FilteredTextBoxExtender ID="ftbPJParticipacion2" runat="server"
                                                                    TargetControlID="tbxPJParticipacion2"
                                                                    FilterType="Numbers,Custom"
                                                                    ValidChars=""></asp:FilteredTextBoxExtender>
                                                            </td>

                                                            <td class="auto-style5">
                                                                <asp:DropDownList ID="ddlPJPreguntaRep1Legal1" runat="server">
                                                                    <asp:ListItem Value="">---</asp:ListItem>
                                                                    <asp:ListItem Value="SI">SI</asp:ListItem>
                                                                    <asp:ListItem Value="NO">NO</asp:ListItem>
                                                                </asp:DropDownList><asp:RequiredFieldValidator ID="rfvPJPreguntaRep1Legal1"
                                                                    ControlToValidate="ddlPJPreguntaRep1Legal1"
                                                                    ValidationGroup="vdgWillis"
                                                                    Enabled="false"
                                                                    ErrorMessage="*"
                                                                    ForeColor="Red"
                                                                    InitialValue="" runat="server">
                                                                </asp:RequiredFieldValidator></td>
                                                            <td class="auto-style5">
                                                                <asp:DropDownList ID="ddlPJPreguntaRep1Legal2" runat="server">
                                                                    <asp:ListItem Value="">---</asp:ListItem>
                                                                    <asp:ListItem Value="SI">SI</asp:ListItem>
                                                                    <asp:ListItem Value="NO">NO</asp:ListItem>
                                                                </asp:DropDownList><asp:RequiredFieldValidator ID="rfvPJPreguntaRep1Legal2"
                                                                    ControlToValidate="ddlPJPreguntaRep1Legal2"
                                                                    ValidationGroup="vdgWillis"
                                                                    Enabled="false"
                                                                    ErrorMessage="*"
                                                                    ForeColor="Red"
                                                                    InitialValue="" runat="server">
                                                                </asp:RequiredFieldValidator></td>
                                                            <td class="auto-style5">
                                                                <asp:DropDownList ID="ddlPJPreguntaRep1Legal3" runat="server">
                                                                    <asp:ListItem Value="">---</asp:ListItem>
                                                                    <asp:ListItem Value="SI">SI</asp:ListItem>
                                                                    <asp:ListItem Value="NO">NO</asp:ListItem>
                                                                </asp:DropDownList><asp:RequiredFieldValidator ID="rfvPJPreguntaRep1Legal3"
                                                                    ControlToValidate="ddlPJPreguntaRep1Legal3"
                                                                    ValidationGroup="vdgWillis"
                                                                    Enabled="false"
                                                                    ErrorMessage="*"
                                                                    ForeColor="Red"
                                                                    InitialValue="" runat="server">
                                                                </asp:RequiredFieldValidator></td>
                                                            <td class="auto-style5">
                                                                <asp:DropDownList ID="ddlPJPreguntaRep1Legal4" runat="server">
                                                                    <asp:ListItem Value="">---</asp:ListItem>
                                                                    <asp:ListItem Value="SI">SI</asp:ListItem>
                                                                    <asp:ListItem Value="NO">NO</asp:ListItem>
                                                                </asp:DropDownList><asp:RequiredFieldValidator ID="rfvPJPreguntaRep1Legal4"
                                                                    ControlToValidate="ddlPJPreguntaRep1Legal4"
                                                                    ValidationGroup="vdgWillis"
                                                                    Enabled="false"
                                                                    ErrorMessage="*"
                                                                    ForeColor="Red"
                                                                    InitialValue="" runat="server">
                                                                </asp:RequiredFieldValidator></td>
                                                        </tr>
                                                        <tr valign="top">
                                                            <td>
                                                                <asp:DropDownList ID="ddlPJTipoDocRepLegal2" runat="server">
                                                                    <asp:ListItem Value="">---</asp:ListItem>
                                                                    <asp:ListItem Value="C.C.">C.C.</asp:ListItem>
                                                                    <asp:ListItem Value="C.E.">C.E.</asp:ListItem>
                                                                    <asp:ListItem Value="NIT">NIT</asp:ListItem>
                                                                    <asp:ListItem Value="NUIP">NUIP</asp:ListItem>
                                                                    <asp:ListItem Value="T.I.">T.I.</asp:ListItem>
                                                                    <asp:ListItem Value="PASAPORTE">PASAPORTE</asp:ListItem>
                                                                    <asp:ListItem Value="PASE DIPLOMATICO">PASE DIPLOMATICO</asp:ListItem>
                                                                </asp:DropDownList><asp:RequiredFieldValidator ID="rfvPJTipoDocRepLegal2"
                                                                    ControlToValidate="ddlPJTipoDocRepLegal2"
                                                                    ValidationGroup="vdgWillis"
                                                                    Enabled="false"
                                                                    ErrorMessage="*"
                                                                    ForeColor="Red"
                                                                    InitialValue="" runat="server">
                                                                </asp:RequiredFieldValidator></td>
                                                            <td>
                                                                <asp:TextBox ID="tbxPJDocumentoRepLegal2" runat="server" MaxLength="15"></asp:TextBox></td>
                                                            <td>
                                                                <asp:TextBox ID="tbxPJNombresRepLegal2" runat="server" Width="230px" MaxLength="80"></asp:TextBox></td>
                                                            <td>
                                                                <asp:TextBox ID="tbxPJParticipacion3" runat="server" MaxLength="10" Width="100px"></asp:TextBox><asp:FilteredTextBoxExtender ID="ftbPJParticipacion3" runat="server"
                                                                    TargetControlID="tbxPJParticipacion3"
                                                                    FilterType="Numbers,Custom"
                                                                    ValidChars=""></asp:FilteredTextBoxExtender>
                                                            </td>
                                                            <td class="auto-style5">
                                                                <asp:DropDownList ID="ddlPJPreguntaRep2Legal1" runat="server">
                                                                    <asp:ListItem Value="">---</asp:ListItem>
                                                                    <asp:ListItem Value="SI">SI</asp:ListItem>
                                                                    <asp:ListItem Value="NO">NO</asp:ListItem>
                                                                </asp:DropDownList><asp:RequiredFieldValidator ID="rfvPJPreguntaRep2Legal1"
                                                                    ControlToValidate="ddlPJPreguntaRep2Legal1"
                                                                    ValidationGroup="vdgWillis"
                                                                    Enabled="false"
                                                                    ErrorMessage="*"
                                                                    ForeColor="Red"
                                                                    InitialValue="" runat="server">
                                                                </asp:RequiredFieldValidator></td>
                                                            <td class="auto-style5">
                                                                <asp:DropDownList ID="ddlPJPreguntaRep2Legal2" runat="server">
                                                                    <asp:ListItem Value="">---</asp:ListItem>
                                                                    <asp:ListItem Value="SI">SI</asp:ListItem>
                                                                    <asp:ListItem Value="NO">NO</asp:ListItem>
                                                                </asp:DropDownList><asp:RequiredFieldValidator ID="rfvPJPreguntaRep2Legal2"
                                                                    ControlToValidate="ddlPJPreguntaRep2Legal2"
                                                                    ValidationGroup="vdgWillis"
                                                                    Enabled="false"
                                                                    ErrorMessage="*"
                                                                    ForeColor="Red"
                                                                    InitialValue="" runat="server">
                                                                </asp:RequiredFieldValidator></td>
                                                            <td class="auto-style5">
                                                                <asp:DropDownList ID="ddlPJPreguntaRep2Legal3" runat="server">
                                                                    <asp:ListItem Value="">---</asp:ListItem>
                                                                    <asp:ListItem Value="SI">SI</asp:ListItem>
                                                                    <asp:ListItem Value="NO">NO</asp:ListItem>
                                                                </asp:DropDownList><asp:RequiredFieldValidator ID="rfvPJPreguntaRep2Legal3"
                                                                    ControlToValidate="ddlPJPreguntaRep2Legal3"
                                                                    ValidationGroup="vdgWillis"
                                                                    Enabled="false"
                                                                    ErrorMessage="*"
                                                                    ForeColor="Red"
                                                                    InitialValue="" runat="server">
                                                                </asp:RequiredFieldValidator></td>
                                                            <td class="auto-style5">
                                                                <asp:DropDownList ID="ddlPJPreguntaRep2Legal4" runat="server">
                                                                    <asp:ListItem Value="">---</asp:ListItem>
                                                                    <asp:ListItem Value="SI">SI</asp:ListItem>
                                                                    <asp:ListItem Value="NO">NO</asp:ListItem>
                                                                </asp:DropDownList><asp:RequiredFieldValidator ID="rfvPJPreguntaRep2Legal4"
                                                                    ControlToValidate="ddlPJPreguntaRep2Legal4"
                                                                    ValidationGroup="vdgWillis"
                                                                    Enabled="false"
                                                                    ErrorMessage="*"
                                                                    ForeColor="Red"
                                                                    InitialValue="" runat="server">
                                                                </asp:RequiredFieldValidator></td>
                                                        </tr>
                                                        <tr valign="top">
                                                            <td>
                                                                <asp:DropDownList ID="ddlPJTipoDocRepLegal3" runat="server">
                                                                    <asp:ListItem Value="">---</asp:ListItem>
                                                                    <asp:ListItem Value="C.C.">C.C.</asp:ListItem>
                                                                    <asp:ListItem Value="C.E.">C.E.</asp:ListItem>
                                                                    <asp:ListItem Value="NIT">NIT</asp:ListItem>
                                                                    <asp:ListItem Value="NUIP">NUIP</asp:ListItem>
                                                                    <asp:ListItem Value="T.I.">T.I.</asp:ListItem>
                                                                    <asp:ListItem Value="PASAPORTE">PASAPORTE</asp:ListItem>
                                                                    <asp:ListItem Value="PASE DIPLOMATICO">PASE DIPLOMATICO</asp:ListItem>
                                                                </asp:DropDownList><asp:RequiredFieldValidator ID="rfvPJTipoDocRepLegal3"
                                                                    ControlToValidate="ddlPJTipoDocRepLegal3"
                                                                    ValidationGroup="vdgWillis"
                                                                    Enabled="false"
                                                                    ErrorMessage="*"
                                                                    ForeColor="Red"
                                                                    InitialValue="" runat="server">
                                                                </asp:RequiredFieldValidator></td>
                                                            <td>
                                                                <asp:TextBox ID="tbxPJDocumentoRepLegal3" runat="server" MaxLength="15"></asp:TextBox></td>
                                                            <td>
                                                                <asp:TextBox ID="tbxPJNombresRepLegal3" runat="server" Width="230px" MaxLength="80"></asp:TextBox></td>
                                                            <td>
                                                                <asp:TextBox ID="tbxPJParticipacion4" runat="server" MaxLength="10" Width="100px"></asp:TextBox><asp:FilteredTextBoxExtender ID="ftbPJParticipacion4" runat="server"
                                                                    TargetControlID="tbxPJParticipacion4"
                                                                    FilterType="Numbers,Custom"
                                                                    ValidChars=""></asp:FilteredTextBoxExtender>
                                                            </td>
                                                            <td class="auto-style5">
                                                                <asp:DropDownList ID="ddlPJPreguntaRep3Legal1" runat="server">
                                                                    <asp:ListItem Value="">---</asp:ListItem>
                                                                    <asp:ListItem Value="SI">SI</asp:ListItem>
                                                                    <asp:ListItem Value="NO">NO</asp:ListItem>
                                                                </asp:DropDownList><asp:RequiredFieldValidator ID="rfvPJPreguntaRep3Legal1"
                                                                    ControlToValidate="ddlPJPreguntaRep3Legal1"
                                                                    ValidationGroup="vdgWillis"
                                                                    Enabled="false"
                                                                    ErrorMessage="*"
                                                                    ForeColor="Red"
                                                                    InitialValue="" runat="server">
                                                                </asp:RequiredFieldValidator></td>
                                                            <td class="auto-style5">
                                                                <asp:DropDownList ID="ddlPJPreguntaRep3Legal2" runat="server">
                                                                    <asp:ListItem Value="">---</asp:ListItem>
                                                                    <asp:ListItem Value="SI">SI</asp:ListItem>
                                                                    <asp:ListItem Value="NO">NO</asp:ListItem>
                                                                </asp:DropDownList><asp:RequiredFieldValidator ID="rfvPJPreguntaRep3Legal2"
                                                                    ControlToValidate="ddlPJPreguntaRep3Legal2"
                                                                    ValidationGroup="vdgWillis"
                                                                    Enabled="false"
                                                                    ErrorMessage="*"
                                                                    ForeColor="Red"
                                                                    InitialValue="" runat="server">
                                                                </asp:RequiredFieldValidator></td>
                                                            <td class="auto-style5">
                                                                <asp:DropDownList ID="ddlPJPreguntaRep3Legal3" runat="server">
                                                                    <asp:ListItem Value="">---</asp:ListItem>
                                                                    <asp:ListItem Value="SI">SI</asp:ListItem>
                                                                    <asp:ListItem Value="NO">NO</asp:ListItem>
                                                                </asp:DropDownList><asp:RequiredFieldValidator ID="rfvPJPreguntaRep3Legal3"
                                                                    ControlToValidate="ddlPJPreguntaRep3Legal3"
                                                                    ValidationGroup="vdgWillis"
                                                                    Enabled="false"
                                                                    ErrorMessage="*"
                                                                    ForeColor="Red"
                                                                    InitialValue="" runat="server">
                                                                </asp:RequiredFieldValidator></td>
                                                            <td class="auto-style5">
                                                                <asp:DropDownList ID="ddlPJPreguntaRep3Legal4" runat="server">
                                                                    <asp:ListItem Value="">---</asp:ListItem>
                                                                    <asp:ListItem Value="SI">SI</asp:ListItem>
                                                                    <asp:ListItem Value="NO">NO</asp:ListItem>
                                                                </asp:DropDownList><asp:RequiredFieldValidator ID="rfvPJPreguntaRep3Legal4"
                                                                    ControlToValidate="ddlPJPreguntaRep3Legal4"
                                                                    ValidationGroup="vdgWillis"
                                                                    Enabled="false"
                                                                    ErrorMessage="*"
                                                                    ForeColor="Red"
                                                                    InitialValue="" runat="server">
                                                                </asp:RequiredFieldValidator></td>
                                                        </tr>
                                                        <tr valign="top">
                                                            <td>
                                                                <asp:DropDownList ID="ddlPJTipoDocRepLegal4" runat="server">
                                                                    <asp:ListItem Value="">---</asp:ListItem>
                                                                    <asp:ListItem Value="C.C.">C.C.</asp:ListItem>
                                                                    <asp:ListItem Value="C.E.">C.E.</asp:ListItem>
                                                                    <asp:ListItem Value="NIT">NIT</asp:ListItem>
                                                                    <asp:ListItem Value="NUIP">NUIP</asp:ListItem>
                                                                    <asp:ListItem Value="T.I.">T.I.</asp:ListItem>
                                                                    <asp:ListItem Value="PASAPORTE">PASAPORTE</asp:ListItem>
                                                                    <asp:ListItem Value="PASE DIPLOMATICO">PASE DIPLOMATICO</asp:ListItem>
                                                                </asp:DropDownList><asp:RequiredFieldValidator ID="rfvPJTipoDocRepLegal4"
                                                                    ControlToValidate="ddlPJTipoDocRepLegal4"
                                                                    ValidationGroup="vdgWillis"
                                                                    Enabled="false"
                                                                    ErrorMessage="*"
                                                                    ForeColor="Red"
                                                                    InitialValue="" runat="server">
                                                                </asp:RequiredFieldValidator></td>
                                                            <td>
                                                                <asp:TextBox ID="tbxPJDocumentoRepLegal4" runat="server" MaxLength="15"></asp:TextBox></td>
                                                            <td>
                                                                <asp:TextBox ID="tbxPJNombresRepLegal4" runat="server" Width="230px" MaxLength="80"></asp:TextBox></td>
                                                            <td>
                                                                <asp:TextBox ID="tbxPJParticipacion5" runat="server" MaxLength="10" Width="100px"></asp:TextBox></td>
                                                            <td class="auto-style5">
                                                                <asp:DropDownList ID="ddlPJPreguntaRep4Legal1" runat="server">
                                                                    <asp:ListItem Value="">---</asp:ListItem>
                                                                    <asp:ListItem Value="SI">SI</asp:ListItem>
                                                                    <asp:ListItem Value="NO">NO</asp:ListItem>
                                                                </asp:DropDownList><asp:RequiredFieldValidator ID="rfvPJPreguntaRep4Legal1"
                                                                    ControlToValidate="ddlPJPreguntaRep4Legal1"
                                                                    ValidationGroup="vdgWillis"
                                                                    Enabled="false"
                                                                    ErrorMessage="*"
                                                                    ForeColor="Red"
                                                                    InitialValue="" runat="server">
                                                                </asp:RequiredFieldValidator></td>
                                                            <td class="auto-style5">
                                                                <asp:DropDownList ID="ddlPJPreguntaRep4Legal2" runat="server">
                                                                    <asp:ListItem Value="">---</asp:ListItem>
                                                                    <asp:ListItem Value="SI">SI</asp:ListItem>
                                                                    <asp:ListItem Value="NO">NO</asp:ListItem>
                                                                </asp:DropDownList><asp:RequiredFieldValidator ID="rfvPJPreguntaRep4Legal2"
                                                                    ControlToValidate="ddlPJPreguntaRep4Legal2"
                                                                    ValidationGroup="vdgWillis"
                                                                    Enabled="false"
                                                                    ErrorMessage="*"
                                                                    ForeColor="Red"
                                                                    InitialValue="" runat="server">
                                                                </asp:RequiredFieldValidator></td>
                                                            <td class="auto-style5">
                                                                <asp:DropDownList ID="ddlPJPreguntaRep4Legal3" runat="server">
                                                                    <asp:ListItem Value="">---</asp:ListItem>
                                                                    <asp:ListItem Value="SI">SI</asp:ListItem>
                                                                    <asp:ListItem Value="NO">NO</asp:ListItem>
                                                                </asp:DropDownList><asp:RequiredFieldValidator ID="rfvPJPreguntaRep4Legal3"
                                                                    ControlToValidate="ddlPJPreguntaRep4Legal3"
                                                                    ValidationGroup="vdgWillis"
                                                                    Enabled="false"
                                                                    ErrorMessage="*"
                                                                    ForeColor="Red"
                                                                    InitialValue="" runat="server">
                                                                </asp:RequiredFieldValidator></td>
                                                            <td class="auto-style5">
                                                                <asp:DropDownList ID="ddlPJPreguntaRep4Legal4" runat="server">
                                                                    <asp:ListItem Value="">---</asp:ListItem>
                                                                    <asp:ListItem Value="SI">SI</asp:ListItem>
                                                                    <asp:ListItem Value="NO">NO</asp:ListItem>
                                                                </asp:DropDownList><asp:RequiredFieldValidator ID="rfvPJPreguntaRep4Legal4"
                                                                    ControlToValidate="ddlPJPreguntaRep4Legal4"
                                                                    ValidationGroup="vdgWillis"
                                                                    Enabled="false"
                                                                    ErrorMessage="*"
                                                                    ForeColor="Red"
                                                                    InitialValue="" runat="server">
                                                                </asp:RequiredFieldValidator></td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <!-- eeor-->
                                            <tr>
                                                <td>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblPJIngresosMensuales" runat="server" Text="INGRESOS MENSUALES (Pesos)"></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="tbxPJIngresosMensuales" runat="server" Width="160px" MaxLength="100" AutoPostBack="True" OnTextChanged="tbxPJIngresosMensuales_TextChanged"></asp:TextBox><asp:RequiredFieldValidator ID="rfvPJIngresosMensuales" runat="server"
                                                                    ControlToValidate="tbxPJIngresosMensuales"
                                                                    ValidationGroup="vdgWillis"
                                                                    ErrorMessage="*"
                                                                    ForeColor="Red">
                                                                </asp:RequiredFieldValidator><asp:FilteredTextBoxExtender ID="ftbPJIngresosMensuales" runat="server"
                                                                    TargetControlID="tbxPJIngresosMensuales"
                                                                    FilterType="Numbers,Custom"
                                                                    ValidChars="$€,."></asp:FilteredTextBoxExtender>
                                                                <asp:RegularExpressionValidator ID="revPJIngresosMensualesNum" runat="server" ErrorMessage="Solo números" Font-Bold="false"
                                                                    Font-Names="Calibri" Font-Size="Small" ForeColor="Red" ControlToValidate="tbxPJIngresosMensuales" ValidationExpression="^[0-9]*\.?[0-9]+$" ValidationGroup="SaveKnowCliente"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblPJEgresoMensuales" runat="server" Text="EGRESOS MENSUALES (pesos)"></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="tbxPJEgresoMensuales" runat="server" Width="160px" MaxLength="100" Text="" AutoPostBack="True" OnTextChanged="tbxPJEgresoMensuales_TextChanged"></asp:TextBox><asp:RequiredFieldValidator ID="rfvPJEgresoMensuales" runat="server"
                                                                    ControlToValidate="tbxPJEgresoMensuales"
                                                                    ValidationGroup="vdgWillis"
                                                                    ErrorMessage="*"
                                                                    ForeColor="Red">
                                                                </asp:RequiredFieldValidator><asp:FilteredTextBoxExtender ID="ftbPJEgresoMensuales" runat="server"
                                                                    TargetControlID="tbxPJEgresoMensuales"
                                                                    FilterType="Numbers,Custom"
                                                                    ValidChars="$€,."></asp:FilteredTextBoxExtender>
                                                                <asp:RegularExpressionValidator ID="revPJEgresoMensualesNum" runat="server" ErrorMessage="Solo números" Font-Bold="false"
                                                                    Font-Names="Calibri" Font-Size="Small" ForeColor="Red" ControlToValidate="tbxPJEgresoMensuales" ValidationExpression="^[0-9]*\.?[0-9]+$" ValidationGroup="SaveKnowCliente"></asp:RegularExpressionValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label1" runat="server" Text="ACTIVOS (Pesos)"></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="tbxPJActivos" runat="server" Width="160px" MaxLength="100" AutoPostBack="True" OnTextChanged="tbxPJActivos_TextChanged"></asp:TextBox><asp:RequiredFieldValidator ID="rfvPJActivos" runat="server"
                                                                    ControlToValidate="tbxPJActivos"
                                                                    ValidationGroup="vdgWillis"
                                                                    ErrorMessage="*"
                                                                    ForeColor="Red">
                                                                </asp:RequiredFieldValidator><asp:FilteredTextBoxExtender ID="ftbPJActivos" runat="server"
                                                                    TargetControlID="tbxPJActivos"
                                                                    FilterType="Numbers,Custom"
                                                                    ValidChars="$€,."></asp:FilteredTextBoxExtender>
                                                                <asp:RegularExpressionValidator ID="revPJActivosNum" runat="server" ErrorMessage="Solo números" Font-Bold="false"
                                                                    Font-Names="Calibri" Font-Size="Small" ForeColor="Red" ControlToValidate="tbxPJActivos" ValidationExpression="^[0-9]*\.?[0-9]+$" ValidationGroup="SaveKnowCliente"></asp:RegularExpressionValidator></td>
                                                            <td>
                                                                <asp:Label ID="lblPJPasivos" runat="server" Text="PASIVOS (Pesos)"></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="tbxPJPasivos" runat="server" Width="160px" MaxLength="100" AutoPostBack="True" OnTextChanged="tbxPJPasivos_TextChanged"></asp:TextBox><asp:RequiredFieldValidator ID="rfvPJPasivos" runat="server"
                                                                    ControlToValidate="tbxPJPasivos"
                                                                    ValidationGroup="vdgWillis"
                                                                    ErrorMessage="*"
                                                                    ForeColor="Red">
                                                                </asp:RequiredFieldValidator><asp:FilteredTextBoxExtender ID="ftbPJPasivos" runat="server"
                                                                    TargetControlID="tbxPJPasivos"
                                                                    FilterType="Numbers,Custom"
                                                                    ValidChars="$€,."></asp:FilteredTextBoxExtender>
                                                                <asp:RegularExpressionValidator ID="revPJPasivosNum" runat="server" ErrorMessage="Solo números" Font-Bold="false"
                                                                    Font-Names="Calibri" Font-Size="Small" ForeColor="Red" ControlToValidate="tbxPJPasivos" ValidationExpression="^[0-9]*\.?[0-9]+$" ValidationGroup="SaveKnowCliente"></asp:RegularExpressionValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblPJOtrosIngresos" runat="server" Text="OTROS INGRESOS MENSUALES (Pesos)"></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="tbxPJOtrosIngresos" runat="server" Width="160px" MaxLength="100" AutoPostBack="True" OnTextChanged="tbxPJOtrosIngresos_TextChanged"></asp:TextBox><asp:RequiredFieldValidator ID="rfvPJOtrosIngresos" runat="server"
                                                                    ControlToValidate="tbxPJOtrosIngresos"
                                                                    ValidationGroup="vdgWillis2"
                                                                    ErrorMessage="*"
                                                                    ForeColor="Red">
                                                                </asp:RequiredFieldValidator>
                                                                <asp:FilteredTextBoxExtender ID="ftbPJOtrosIngresos" runat="server"
                                                                    TargetControlID="tbxPJOtrosIngresos"
                                                                    FilterType="Numbers,Custom"
                                                                    ValidChars="$€,."></asp:FilteredTextBoxExtender>
                                                                <asp:RegularExpressionValidator ID="revPJOtrosIngresos" runat="server" ErrorMessage="Solo números" Font-Bold="false"
                                                                    Font-Names="Calibri" Font-Size="Small" ForeColor="Red" ControlToValidate="tbxPJOtrosIngresos" ValidationExpression="^[0-9]*\.?[0-9]+$" ValidationGroup="SaveKnowCliente"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblPJConceptoOtrosIngresos" runat="server" Text="CONCEPTO"></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="tbxPJConceptoOtrosIngresos" runat="server" Width="330px" MaxLength="100"></asp:TextBox></td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblPJPregunta6" runat="server" Text="¿Está la persona jurídica obligada a tributar en un país diferente a Colombia?"></asp:Label></td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlPJPregunta6" runat="server">
                                                                    <asp:ListItem Value="">---</asp:ListItem>
                                                                    <asp:ListItem Value="SI">SI</asp:ListItem>
                                                                    <asp:ListItem Value="NO">NO</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvPJPregunta6"
                                                                    ControlToValidate="ddlPJPregunta6"
                                                                    ValidationGroup="vdgWillis"
                                                                    ErrorMessage="*"
                                                                    ForeColor="Red"
                                                                    InitialValue="" runat="server"></asp:RequiredFieldValidator></td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblFiscal" runat="server" Text="En caso de afirmativo, de cual (es) e indique el (los) número(s) de identificación tributaria en dicha(s) jurisdicción(es)"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblPJPais1" runat="server" Text="PAÍS"></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="tbxPJPais1" runat="server" MaxLength="20"></asp:TextBox></td>
                                                            <td>
                                                                <asp:Label ID="lblPJNIT1" runat="server" Text="NIT"></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="tbxPJNIT1" runat="server" MaxLength="20"></asp:TextBox></td>
                                                            <td>
                                                                <asp:Label ID="lblPJPais2" runat="server" Text="PAÍS"></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="tbxPJPais2" runat="server" MaxLength="20"></asp:TextBox></td>
                                                            <td>
                                                                <asp:Label ID="lblPJNIT2" runat="server" Text="NIT"></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="tbxPJNIT2" runat="server" MaxLength="20"></asp:TextBox></td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblPJDireccionFiscal1" runat="server" Text="Dirección de Residencia Fiscal"></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="tbxPJDireccionFiscal1" runat="server" MaxLength="50"></asp:TextBox></td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblPJPais3" runat="server" Text="PAÍS"></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="tbxPJPais3" runat="server" MaxLength="20"></asp:TextBox></td>
                                                            <td>
                                                                <asp:Label ID="lblPJNIT3" runat="server" Text="NIT"></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="tbxPJNIT3" runat="server" MaxLength="20"></asp:TextBox></td>
                                                            <td>
                                                                <asp:Label ID="lblPJPais4" runat="server" Text="PAÍS"></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="tbxPJPais4" runat="server" MaxLength="20"></asp:TextBox></td>
                                                            <td>
                                                                <asp:Label ID="lblPJNIT4" runat="server" Text="NIT"></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="tbxPJNIT4" runat="server" MaxLength="20"></asp:TextBox></td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblPJDireccionFiscal2" runat="server" Text="Dirección de Residencia Fiscal"></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="tbxPJDireccionFiscal2" runat="server" MaxLength="50"></asp:TextBox></td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblTexto" runat="server" Text="Si usted indicó ser residente fiscal o ciudadano de los Estados Unidos de América, diligencie las preguntas que se encuentran a continuación"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblPJPregunta7" runat="server" Text="Es la persona jurídica una sociedad de personas o sociedad constituida en EE.UU o de conformidad a la legislación estadounidense o de cualquiera de sus Estados"></asp:Label></td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlPJPregunta7" runat="server">
                                                                    <asp:ListItem Value="">---</asp:ListItem>
                                                                    <asp:ListItem Value="SI">SI</asp:ListItem>
                                                                    <asp:ListItem Value="NO">NO</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvPJPregunta7"
                                                                    ControlToValidate="ddlPJPregunta7"
                                                                    ValidationGroup="vdgWillis"
                                                                    ErrorMessage="*"
                                                                    ForeColor="Red"
                                                                    InitialValue="" runat="server"></asp:RequiredFieldValidator></td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblPJPregunta8" runat="server" Text="Posee un número telefónico en los Estados Unidos"></asp:Label></td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlPJPregunta8" runat="server">
                                                                    <asp:ListItem Value="">---</asp:ListItem>
                                                                    <asp:ListItem Value="SI">SI</asp:ListItem>
                                                                    <asp:ListItem Value="NO">NO</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvPJPregunta8"
                                                                    ControlToValidate="ddlPJPregunta8"
                                                                    ValidationGroup="vdgWillis"
                                                                    ErrorMessage="*"
                                                                    ForeColor="Red"
                                                                    InitialValue="" runat="server"></asp:RequiredFieldValidator></td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblPJPregunta9" runat="server" Text="Cuenta con una dirección de domicilio en los Estados Unidos (residencia, correspondencia o casillero postal/P.O Box)"></asp:Label></td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlPJPregunta9" runat="server">
                                                                    <asp:ListItem Value="">---</asp:ListItem>
                                                                    <asp:ListItem Value="SI">SI</asp:ListItem>
                                                                    <asp:ListItem Value="NO">NO</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvPJPregunta9"
                                                                    ControlToValidate="ddlPJPregunta9"
                                                                    ValidationGroup="vdgWillis"
                                                                    ErrorMessage="*"
                                                                    ForeColor="Red"
                                                                    InitialValue="" runat="server"></asp:RequiredFieldValidator></td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblPJPregunta10" runat="server" Text="La persona jurídica ha otorgado poder o firma autorizada a una persona con dirección en Estados Unidos, o se ha designado una dirección “al cuidado de” o una dirección de “ recepción de correspondencia”"></asp:Label></td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlPJPregunta10" runat="server">
                                                                    <asp:ListItem Value="">---</asp:ListItem>
                                                                    <asp:ListItem Value="SI">SI</asp:ListItem>
                                                                    <asp:ListItem Value="NO">NO</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvPJPregunta10"
                                                                    ControlToValidate="ddlPJPregunta10"
                                                                    ValidationGroup="vdgWillis"
                                                                    ErrorMessage="*"
                                                                    ForeColor="Red"
                                                                    InitialValue="" runat="server"></asp:RequiredFieldValidator></td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr id="infoTituloDeclaracionFondos" visible="false" runat="server">
                                    <td>
                                        <table width="100%">
                                            <tr align="center" style="background-color: #C0C0C0">
                                                <td>
                                                    <asp:Label ID="Label30" runat="server" Text="2. DECLARACIÓN DE ORIGEN DE FONDOS"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr id="infoDeclaracionFondos" visible="false" runat="server">
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblDF_1" runat="server" Text=""></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblDF_2" runat="server" Text=""></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblDF_3" runat="server" Text=""></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblDF_4" runat="server" Text=""></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblDF_5" runat="server" Text=""></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblDF_6" runat="server" Text=""></asp:Label></td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblOrigenFondos" runat="server" Text="Origen de Fondos:" Width="260px"></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="tbxOrigenFondos" runat="server" MaxLength="500" Width="550px"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvOrigenFondos" runat="server"
                                                                    ControlToValidate="tbxOrigenFondos"
                                                                    ValidationGroup="vdgWillis"
                                                                    ErrorMessage="*"
                                                                    ForeColor="Red"></asp:RequiredFieldValidator></td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr id="infoDeclaracionFondosPJ" visible="false" runat="server">
                                    <td>
                                        <table>
                                            <tr>
                                                <td>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblPJCotizaBolsa" runat="server" Text="¿La persona jurídica esta listada y cotiza en bolsa?"></asp:Label></td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlPJCotizaBolsa" runat="server">
                                                                    <asp:ListItem Value="">---</asp:ListItem>
                                                                    <asp:ListItem Value="SI">SI</asp:ListItem>
                                                                    <asp:ListItem Value="NO">NO</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvPJCotizaBolsa"
                                                                    ControlToValidate="ddlPJCotizaBolsa"
                                                                    ValidationGroup="vdgWillis"
                                                                    ErrorMessage="*"
                                                                    ForeColor="Red"
                                                                    InitialValue="" runat="server"></asp:RequiredFieldValidator></td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblPJEstatal" runat="server" Text="¿La persona jurídica es una entidad estatal o del orden gubernamental?"></asp:Label></td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlPJEstatal" runat="server">
                                                                    <asp:ListItem Value="">---</asp:ListItem>
                                                                    <asp:ListItem Value="SI">SI</asp:ListItem>
                                                                    <asp:ListItem Value="NO">NO</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvPJEstatal"
                                                                    ControlToValidate="ddlPJEstatal"
                                                                    ValidationGroup="vdgWillis"
                                                                    ErrorMessage="*"
                                                                    ForeColor="Red"
                                                                    InitialValue="" runat="server"></asp:RequiredFieldValidator></td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblPJSinAnimoLucro" runat="server" Text="¿Es la persona jurídica una entidad sin ánimo de lucro?"></asp:Label></td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlPJSinAnimoLucro" runat="server">
                                                                    <asp:ListItem Value="">---</asp:ListItem>
                                                                    <asp:ListItem Value="SI">SI</asp:ListItem>
                                                                    <asp:ListItem Value="NO">NO</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvPJSinAnimoLucro"
                                                                    ControlToValidate="ddlPJSinAnimoLucro"
                                                                    ValidationGroup="vdgWillis"
                                                                    ErrorMessage="*"
                                                                    ForeColor="Red"
                                                                    InitialValue="" runat="server"></asp:RequiredFieldValidator></td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label17" runat="server" Text="Si ninguna de las anteriores respuestas es en sentido positivo, responda la siguiente pregunta:"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblPJSubsidiaria" runat="server" Text="¿La persona jurídica actúa en calidad de subsidiaria, subordinada o filial de una compañía matriz domiciliada en un país diferente a Colombia?"></asp:Label></td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlPJSubsidiaria" runat="server">
                                                                    <asp:ListItem Value="">---</asp:ListItem>
                                                                    <asp:ListItem Value="SI">SI</asp:ListItem>
                                                                    <asp:ListItem Value="NO">NO</asp:ListItem>
                                                                </asp:DropDownList></td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label274" runat="server" Text="En caso afirmativo, responda las siguientes preguntas:"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblPJSocMatriz" runat="server" Text="Nombre de la sociedad Matriz"></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="tbxPJSocMatriz" runat="server" MaxLength="50"></asp:TextBox></td>
                                                            <td>
                                                                <asp:Label ID="lblPJSocMatrizIdenTrib" runat="server" Text="Número de Identificación tributaria"></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="tbxPJSocMatrizIdenTrib" runat="server" MaxLength="50"></asp:TextBox></td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblPJSocMatrizJurisdiccion" runat="server" Text="Jurisdicción/País"></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="tbxPJSocMatrizJurisdiccion" runat="server" MaxLength="50"></asp:TextBox></td>
                                                            <td>
                                                                <asp:Label ID="lblPJSocMatrizDireccion" runat="server" Text="Dirección"></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="tbxPJSocMatrizDireccion" runat="server" MaxLength="50"></asp:TextBox></td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblPJSocMatrizCiudad" runat="server" Text="Ciudad"></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="tbxPJSocMatrizCiudad" runat="server" MaxLength="50"></asp:TextBox></td>
                                                            <td>
                                                                <asp:Label ID="lblPJSocMatrizTelefono" runat="server" Text="Teléfono"></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="tbxPJSocMatrizTelefono" runat="server" MaxLength="50"></asp:TextBox></td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>

                                        </table>
                                    </td>
                                </tr>
                                <tr id="infoTituloPF" visible="false" runat="server">
                                    <td>
                                        <table width="100%">
                                            <tr align="center" style="background-color: #C0C0C0">
                                                <td>
                                                    <asp:Label ID="Label93" runat="server" Text="3. ACTIVIDAD EN OPERACIONES INTERNACIONALES"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr id="infoPF1" visible="false" runat="server">
                                    <td>
                                        <table>
                                            <tr>
                                                <td>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblTransacMonedaExtra" runat="server" Text="REALIZA TRANSACCIONES EN MONEDA EXTRANJERA?"></asp:Label></td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlTransacMonedaExtra" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlTransacMonedaExtra_SelectedIndexChanged">
                                                                    <asp:ListItem Value="">---</asp:ListItem>
                                                                    <asp:ListItem Value="NO">NO</asp:ListItem>
                                                                    <asp:ListItem Value="SI">SI</asp:ListItem>
                                                                </asp:DropDownList><asp:RequiredFieldValidator ID="rfvTransacMonedaExtra"
                                                                    ControlToValidate="ddlTransacMonedaExtra"
                                                                    ValidationGroup="vdgWillis"
                                                                    ErrorMessage="*"
                                                                    ForeColor="Red"
                                                                    InitialValue="" runat="server"></asp:RequiredFieldValidator>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblTipoTransaccion" runat="server" Text="¿CUAL?" Visible="false"></asp:Label></td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlTipoTransaccion" runat="server" AutoPostBack="False" Visible="false">
                                                                    <asp:ListItem Value="">---</asp:ListItem>
                                                                    <asp:ListItem Value="EXPORTACIONES">EXPORTACIONES</asp:ListItem>
                                                                    <asp:ListItem Value="IMPORTACIONES">IMPORTACIONES</asp:ListItem>
                                                                    <asp:ListItem Value="INVERSIONES">INVERSIONES</asp:ListItem>
                                                                    <asp:ListItem Value="OTRAS">OTRAS</asp:ListItem>
                                                                    <asp:ListItem Value="PRESTAMO">PRESTAMO</asp:ListItem>
                                                                    <asp:ListItem Value="TRANSFERENCIAS">TRANSFERENCIAS</asp:ListItem>
                                                                </asp:DropDownList><asp:RequiredFieldValidator ID="rfvTipoTransaccion"
                                                                    ControlToValidate="ddlTipoTransaccion"
                                                                    ValidationGroup="vdgWillis"
                                                                    ErrorMessage="**"
                                                                    ForeColor="Red"
                                                                    InitialValue="" runat="server"></asp:RequiredFieldValidator></td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblOtroTipoTransaccion" runat="server" Text="INDIQUE OTRAS OPERACIONES"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="tbxOtroTipoTransaccion" runat="server" MaxLength="80"></asp:TextBox>
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
                                                                <asp:Label ID="lblPFProdExterior" runat="server" Text="TIENE PRODUCTOS FINACIEROS EN EL EXTERIOR?"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlPFProdExterior" runat="server" AutoPostBack="False">
                                                                    <asp:ListItem Value="">---</asp:ListItem>
                                                                    <asp:ListItem Value="NO">NO</asp:ListItem>
                                                                    <asp:ListItem Value="SI">SI</asp:ListItem>
                                                                </asp:DropDownList><asp:RequiredFieldValidator ID="rfvPFProdExterior"
                                                                    ControlToValidate="ddlPFProdExterior"
                                                                    ValidationGroup="vdgWillis"
                                                                    ErrorMessage="*"
                                                                    ForeColor="Red"
                                                                    InitialValue="" runat="server"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label20" runat="server" Text="POSEE CUENTAS EN MONEDA EXTRANJERA?"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlPFCtaMonedaExtra" runat="server" AutoPostBack="False">
                                                                    <asp:ListItem Value="">---</asp:ListItem>
                                                                    <asp:ListItem Value="NO">NO</asp:ListItem>
                                                                    <asp:ListItem Value="SI">SI</asp:ListItem>
                                                                </asp:DropDownList><asp:RequiredFieldValidator ID="rfvPFCtaMonedaExtra"
                                                                    ControlToValidate="ddlPFCtaMonedaExtra"
                                                                    ValidationGroup="vdgWillis"
                                                                    ErrorMessage="*"
                                                                    ForeColor="Red"
                                                                    InitialValue="" runat="server"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table>
                                                        <tr valign="top">
                                                            <td class="auto-style5">
                                                                <asp:Label ID="lblPFTipoProducto" runat="server" Text="TIPO DE PRODUCTO"></asp:Label>
                                                            </td>
                                                            <td class="auto-style5">
                                                                <asp:Label ID="lblPFNumeroProducto" runat="server" Text="N° ID DEL PRODUCTO"></asp:Label>
                                                            </td>
                                                            <td class="auto-style5">
                                                                <asp:Label ID="lblPFEntidad" runat="server" Text="ENTIDAD"></asp:Label>
                                                            </td>
                                                            <td class="auto-style5">
                                                                <asp:Label ID="lblPFMonto" runat="server" Text="MONTO"></asp:Label>
                                                            </td>
                                                            <td class="auto-style5">
                                                                <asp:Label ID="lblPFCiudad" runat="server" Text="CIUDAD"></asp:Label>
                                                            </td>
                                                            <td class="auto-style5">
                                                                <asp:Label ID="lblPFPais" runat="server" Text="PAIS"></asp:Label>
                                                            </td>
                                                            <td class="auto-style5">
                                                                <asp:Label ID="lblPFMoneda" runat="server" Text="MONEDA"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr valign="top">
                                                            <td>
                                                                <asp:TextBox ID="tbxPFTipoProducto1" runat="server" MaxLength="50"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="tbxPFNumeroProducto1" runat="server" MaxLength="50"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="tbxPFEntidad1" runat="server" MaxLength="80"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="tbxPFMonto1" runat="server" MaxLength="100"></asp:TextBox><asp:FilteredTextBoxExtender ID="ftbPFMonto1" runat="server"
                                                                    TargetControlID="tbxPFMonto1"
                                                                    FilterType="Numbers,Custom"
                                                                    ValidChars=""></asp:FilteredTextBoxExtender>
                                                                <asp:RegularExpressionValidator ID="revPFMonto1" runat="server" ErrorMessage="Solo números" Font-Bold="false"
                                                                    Font-Names="Calibri" Font-Size="Small" ForeColor="Red" ControlToValidate="tbxPFMonto1" ValidationExpression="^[0-9]*\.?[0-9]+$" ValidationGroup="SaveKnowCliente"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="tbxPFCiudad1" runat="server" MaxLength="20"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="tbxPFPais1" runat="server" MaxLength="20"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="tbxPFMoneda1" runat="server" MaxLength="20"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr valign="top">
                                                            <td>
                                                                <asp:TextBox ID="tbxPFTipoProducto2" runat="server" MaxLength="50"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="tbxPFNumeroProducto2" runat="server" MaxLength="50"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="tbxPFEntidad2" runat="server" MaxLength="80"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="tbxPFMonto2" runat="server" MaxLength="100"></asp:TextBox><asp:FilteredTextBoxExtender ID="ftbPFMonto2" runat="server"
                                                                    TargetControlID="tbxPFMonto2"
                                                                    FilterType="Numbers,Custom"
                                                                    ValidChars=""></asp:FilteredTextBoxExtender>
                                                                <asp:RegularExpressionValidator ID="revPFMonto2" runat="server" ErrorMessage="Solo números" Font-Bold="false"
                                                                    Font-Names="Calibri" Font-Size="Small" ForeColor="Red" ControlToValidate="tbxPFMonto2" ValidationExpression="^[0-9]*\.?[0-9]+$" ValidationGroup="SaveKnowCliente"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="tbxPFCiudad2" runat="server" MaxLength="20"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="tbxPFPais2" runat="server" MaxLength="20"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="tbxPFMoneda2" runat="server" MaxLength="20"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr valign="top">
                                                            <td>
                                                                <asp:TextBox ID="tbxPFTipoProducto3" runat="server" MaxLength="50"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="tbxPFNumeroProducto3" runat="server" MaxLength="50"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="tbxPFEntidad3" runat="server" MaxLength="80"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="tbxPFMonto3" runat="server" MaxLength="100"></asp:TextBox><asp:FilteredTextBoxExtender ID="ftbPFMonto3" runat="server"
                                                                    TargetControlID="tbxPFMonto3"
                                                                    FilterType="Numbers,Custom"
                                                                    ValidChars=""></asp:FilteredTextBoxExtender>
                                                                <asp:RegularExpressionValidator ID="revPFMonto3" runat="server" ErrorMessage="Solo números" Font-Bold="false"
                                                                    Font-Names="Calibri" Font-Size="Small" ForeColor="Red" ControlToValidate="tbxPFMonto3" ValidationExpression="^[0-9]*\.?[0-9]+$" ValidationGroup="SaveKnowCliente"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="tbxPFCiudad3" runat="server" MaxLength="20"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="tbxPFPais3" runat="server" MaxLength="20"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="tbxPFMoneda3" runat="server" MaxLength="20"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>

                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblPF_1" runat="server" Text=""></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblPF_2" runat="server" Text=""></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblPF_3" runat="server" Text=""></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblPF_4" runat="server" Text=""></asp:Label></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr id="infoTituloSeguros" visible="false" runat="server">
                                    <td>
                                        <table width="100%">
                                            <tr align="center" style="background-color: #C0C0C0">
                                                <td>
                                                    <asp:Label ID="Label105" runat="server" Text="4. INFORMACIÓN SOBRE RECLAMACIONES EN SEGUROS"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr id="infoSeguros1" visible="false" runat="server">
                                    <td>
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label106" runat="server"
                                                        Text="¿HA PRESENTADO RECLAMOS O HA RECIBIDO INDEMNIZACIONES EN SEGUROS EN LOS DOS ULTIMOS AÑOS?"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlReclamaciones" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlReclamaciones_SelectedIndexChanged">
                                                        <asp:ListItem Value="">---</asp:ListItem>
                                                        <asp:ListItem Value="NO">NO</asp:ListItem>
                                                        <asp:ListItem Value="SI">SI</asp:ListItem>
                                                    </asp:DropDownList><asp:RequiredFieldValidator ID="rfvReclamaciones"
                                                        ControlToValidate="ddlReclamaciones"
                                                        ValidationGroup="vdgWillis"
                                                        ErrorMessage="*"
                                                        ForeColor="Red"
                                                        InitialValue="" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr id="infoSeguros2" visible="false" runat="server">
                                    <td>
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label107" runat="server" Text="AÑO"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label108" runat="server" Text="RAMO"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label109" runat="server" Text="COMPAÑÍA"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label110" runat="server" Text="VALOR"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label16" runat="server" Text="RESULTADO"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="tbxSeguroAno1" runat="server" MaxLength="4"></asp:TextBox><asp:RequiredFieldValidator ID="rfvSeguroAno1" runat="server"
                                                        ControlToValidate="tbxSeguroAno1"
                                                        ValidationGroup="vdgWillis"
                                                        ErrorMessage="*"
                                                        ForeColor="Red">
                                                    </asp:RequiredFieldValidator><asp:FilteredTextBoxExtender ID="fteSeguroAno1" runat="server"
                                                        TargetControlID="tbxSeguroAno1"
                                                        FilterType="Numbers,Custom"
                                                        ValidChars=""></asp:FilteredTextBoxExtender>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="tbxSeguroRamo1" runat="server" MaxLength="15"></asp:TextBox><asp:RequiredFieldValidator ID="rfvSeguroRamo1" runat="server"
                                                        ControlToValidate="tbxSeguroRamo1"
                                                        ValidationGroup="vdgWillis"
                                                        ErrorMessage="*"
                                                        ForeColor="Red">
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="tbxSeguroCompania1" runat="server" MaxLength="80"></asp:TextBox><asp:RequiredFieldValidator ID="rfvSeguroCompania1" runat="server"
                                                        ControlToValidate="tbxSeguroCompania1"
                                                        ValidationGroup="vdgWillis"
                                                        ErrorMessage="*"
                                                        ForeColor="Red">
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="tbxSeguroValor1" runat="server" MaxLength="18"></asp:TextBox><asp:RequiredFieldValidator ID="rfvSeguroValor1" runat="server"
                                                        ControlToValidate="tbxSeguroValor1"
                                                        ValidationGroup="vdgWillis"
                                                        ErrorMessage="*"
                                                        ForeColor="Red">
                                                    </asp:RequiredFieldValidator><asp:FilteredTextBoxExtender ID="fteSeguroValor1" runat="server"
                                                        TargetControlID="tbxSeguroValor1"
                                                        FilterType="Numbers,Custom"
                                                        ValidChars=""></asp:FilteredTextBoxExtender>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlSeguroTipo1" runat="server">
                                                        <asp:ListItem Value="">---</asp:ListItem>
                                                        <asp:ListItem Value="RECLAM.">RECLAM.</asp:ListItem>
                                                        <asp:ListItem Value="INDEMNIZ.">INDEMNIZ.</asp:ListItem>
                                                    </asp:DropDownList><asp:RequiredFieldValidator ID="rfvSeguroTipo1"
                                                        ControlToValidate="ddlSeguroTipo1"
                                                        ValidationGroup="vdgWillis"
                                                        ErrorMessage="*"
                                                        ForeColor="Red"
                                                        InitialValue="" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="tbxSeguroAno2" runat="server" MaxLength="4"></asp:TextBox><asp:FilteredTextBoxExtender ID="fteSeguroAno2" runat="server"
                                                        TargetControlID="tbxSeguroAno2"
                                                        FilterType="Numbers,Custom"
                                                        ValidChars=""></asp:FilteredTextBoxExtender>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="tbxSeguroRamo2" runat="server" MaxLength="15"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="tbxSeguroCompania2" runat="server" MaxLength="80"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="tbxSeguroValor2" runat="server" MaxLength="18"></asp:TextBox><asp:FilteredTextBoxExtender ID="fteSeguroValor2" runat="server"
                                                        TargetControlID="tbxSeguroValor2"
                                                        FilterType="Numbers,Custom"
                                                        ValidChars=""></asp:FilteredTextBoxExtender>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlSeguroTipo2" runat="server">
                                                        <asp:ListItem Value="">---</asp:ListItem>
                                                        <asp:ListItem Value="RECLAM.">RECLAM.</asp:ListItem>
                                                        <asp:ListItem Value="INDEMNIZ.">INDEMNIZ.</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr id="infoAutorizacion" visible="false" runat="server">
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblAuto_1" runat="server" Text=""></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblAuto_2" runat="server" Text=""></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblAuto_3" runat="server" Text=""></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblAuto_4" runat="server" Text=""></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblAuto_5" runat="server" Text=""></asp:Label></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr id="infoTituloClausula" visible="false" runat="server">
                                    <td>
                                        <table width="100%">
                                            <tr align="center" style="background-color: #C0C0C0">
                                                <td>
                                                    <asp:Label ID="Label118" runat="server" Text="5. CLÁUSULA DE AUTORIZACIÓN"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr id="infoClausula" valign="top" visible="false" runat="server">
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblClau_1" runat="server" Text=""></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblClau_2" runat="server" Text=""></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblClau_3" runat="server" Text=""></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblClau_4" runat="server" Text=""></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblClau_5" runat="server" Text=""></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblClau_6" runat="server" Text=""></asp:Label></td>
                                            </tr>
                                            <!-- La tabla -->
                                            <tr>
                                                <td>
                                                    <table class="auto-style2" border="1">
                                                        <tr>
                                                            <td colspan="3" style="text-align: center">
                                                                <asp:Label ID="Label78" runat="server"
                                                                    Text="AUTORIZACION"></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3" style="text-align: center">
                                                                <asp:Label ID="Label217" runat="server" Text="Para efectos de la presente autorización, entiéndase por LA ASEGURADORA, la(s) sociedad(s) del siguiente listado"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="auto-style4">
                                                                <asp:Label ID="Label218" runat="server" Text="NOMBRE"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label219" runat="server" Text="DIRECCIÓN"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label220" runat="server" Text="TELÉFONO"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="auto-style4">
                                                                <asp:Label ID="Label80" runat="server" Text="ACE Seguros S.A."></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label81" runat="server" Text="Calle 72 No. 10-51"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label82" runat="server" Text="(1)3190300"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="auto-style4">
                                                                <asp:Label ID="Label83" runat="server" Text="AIG Seguros Colombia S.A."></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label84" runat="server" Text="Calle 78 No. 9-57"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label85" runat="server" Text="(1)3138700"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="auto-style4">
                                                                <asp:Label ID="Label86" runat="server" Text="Allianz Seguros de Vida S.A; Allianz Seguros S.A."></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label111" runat="server" Text="Calle 100 No. 9ª-45"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label112" runat="server" Text="(1)5600600"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="auto-style4">
                                                                <asp:Label ID="Label113" runat="server" Text="Aseguradora Solidaria de Colombia LTDA. Entidad Cooperativa"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label114" runat="server" Text="Carrera 13 A No. 29-24"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label115" runat="server" Text="(1)6464330"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="auto-style4">
                                                                <asp:Label ID="Label116" runat="server" Text="BBVA Seguros Colombia; BBVA Seguros de Vida Colombia"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label120" runat="server" Text="Carrera 15 No. 95 65 Piso 6"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label125" runat="server" Text="(1)2191100"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="auto-style4">
                                                                <asp:Label ID="Label126" runat="server" Text="Cardif Colombia Seguros Generales S.A."></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label127" runat="server" Text="Calle 113 No. 7-80"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label129" runat="server" Text="(1)7444040"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="auto-style4">
                                                                <asp:Label ID="Label130" runat="server" Text="Cardinal Compañía de Seguros S.A."></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label131" runat="server" Text="Calle 98 No.21-50"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label132" runat="server" Text="(1)7039052"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="auto-style4">
                                                                <asp:Label ID="Label133" runat="server" Text="Chubb de Colombia Compañía de Seguros S.A."></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label134" runat="server" Text="Av. Calle 26 No. 59-51"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label135" runat="server" Text="(1)3266210"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="auto-style4">
                                                                <asp:Label ID="Label136" runat="server" Text="Compañía Aseguradora de Fianzas S.A."></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label137" runat="server" Text="Calle 82 No. 11-37"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label138" runat="server" Text="(1)6444690"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="auto-style4">
                                                                <asp:Label ID="Label139" runat="server" Text="Compañía de Seguros Bolívar S.A; Seguros Comerciales Bolívar S.A."></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label140" runat="server" Text="Av. Dorado No. 68B-31"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label141" runat="server" Text="(1)3410077"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="auto-style4">
                                                                <asp:Label ID="Label142" runat="server" Text="Compañía de Seguros de Vida Aurora S.A."></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label143" runat="server" Text="Carrera 7 No. 74-21"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label144" runat="server" Text="(1)3454980"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="auto-style4">
                                                                <asp:Label ID="Label145" runat="server" Text="Cóndor S.A. Compañía de Seguros Generales"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label147" runat="server" Text="Carrera 7 No. 74-21"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label148" runat="server" Text="(1)3454980"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="auto-style4">
                                                                <asp:Label ID="Label159" runat="server" Text="Compañía de Seguros de Vida Colmena S.A."></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label160" runat="server" Text="Av, Calle 69C-03"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label163" runat="server" Text="(1)3241111"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="auto-style4">
                                                                <asp:Label ID="Label168" runat="server" Text="Compañía Mundial de Seguros S.A."></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label169" runat="server" Text="Calle 33 No.6B-24"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label170" runat="server" Text="(1)2855600"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="auto-style4">
                                                                <asp:Label ID="Label171" runat="server" Text="Compañía Mundial de Seguros S.A.  "></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label172" runat="server" Text="Calle 33 No.6B-24"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label173" runat="server" Text="(1)2855600"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="auto-style4">
                                                                <asp:Label ID="Label174" runat="server" Text="Generali Colombia Vida Compañía de Seguros S.A; Generali Colombia- Seguros Generales S.A."></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label175" runat="server" Text="Carrera 7 No, 72-13"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label176" runat="server" Text="(1)3468888"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="auto-style4">
                                                                <asp:Label ID="Label177" runat="server" Text="Global Seguros de Vida S.A."></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label178" runat="server" Text="Carrera 9 No, 74-62"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label179" runat="server" Text="(1)3139200"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="auto-style4">
                                                                <asp:Label ID="Label180" runat="server" Text="La Equidad Seguros de Vida Organismos Cooperativo; La Equidad Seguros Generales Organismos Cooperativos"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label181" runat="server" Text="Carrera 9 A No. 99-07"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label182" runat="server" Text="(1)5922929"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="auto-style4">
                                                                <asp:Label ID="Label183" runat="server" Text="La Previsora S.A. Compañía de Seguros"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label184" runat="server" Text="Calle 57 No.9-07"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label185" runat="server" Text="(1)3485757"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="auto-style4">
                                                                <asp:Label ID="Label7" runat="server" Text="Liberty Seguros de Vida S.A.; Liberty Seguros S.A."></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label8" runat="server" Text="Calle 72 No.10-07"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label10" runat="server" Text="(1)3103300"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="auto-style4">
                                                                <asp:Label ID="Label198" runat="server" Text="Mapfre Colombia Vida Seguros S.A.; Mapfre Seguros de Crédito S.A.; Mapfre Seguros Generales de Colombia S.A."></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label199" runat="server" Text="Carrera 14 No. 96-34"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label200" runat="server" Text="(1)6503300"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="auto-style4">
                                                                <asp:Label ID="Label201" runat="server" Text="Metlife Colombia Seguros de Vida S.A."></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label202" runat="server" Text="Carrera 7 No.99-53"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label203" runat="server" Text="(1)6388240"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="auto-style4">
                                                                <asp:Label ID="Label205" runat="server" Text="Pan American Life de Colombia Compañía de Seguros S.A."></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label206" runat="server" Text="Carrera 7 No. 75-09"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label221" runat="server" Text="(1)3267400"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="auto-style4">
                                                                <asp:Label ID="Label222" runat="server" Text="Positiva Compañía de Seguros S.A."></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label223" runat="server" Text="Av. Carrera 45 No. 94-72"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label224" runat="server" Text="(1)6502200"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="auto-style4">
                                                                <asp:Label ID="Label225" runat="server" Text="QBE Seguros S.A."></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label226" runat="server" Text="Carrera 7 No. 76-35"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label227" runat="server" Text="(1)3190730"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="auto-style4">
                                                                <asp:Label ID="Label228" runat="server" Text="Royal & Sun Allince Seguros (Colombia) S.A."></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label229" runat="server" Text="Avenida 19 No. 104-37"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label230" runat="server" Text="(1)4881000"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="auto-style4">
                                                                <asp:Label ID="Label231" runat="server" Text="Segurexpo de Colombia S.A."></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label232" runat="server" Text="Calle 72 No. 6-44"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label233" runat="server" Text="(1)3266969"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="auto-style4">
                                                                <asp:Label ID="Label234" runat="server" Text="Seguros Alfa S.A; Seguros de Vida Alfa S.A."></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label235" runat="server" Text="Av Calle 24 A No. 59-42"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label236" runat="server" Text="(1)3446770"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="auto-style4">
                                                                <asp:Label ID="Label237" runat="server" Text="Seguros de Riesgo Laborales Suramericana S.A.; Seguros de Vida Suramericana S.A.;
    Seguros Generales Suramericana S.A.
    "></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label238" runat="server" Text="Carrera 64B No.49ª-30 Medellín- Colombia"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label239" runat="server" Text="
    (4)2602100
    "></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="auto-style4">
                                                                <asp:Label ID="Label240" runat="server" Text="Seguros del Estado S.A.; Seguros de Vida del Estado S.A."></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label241" runat="server" Text="Carrera 11 No. 90-20"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label242" runat="server" Text="(1)6019330"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="auto-style4">
                                                                <asp:Label ID="Label243" runat="server" Text="Old Mutual Holding de Colombia S.A."></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label244" runat="server" Text="Av. 19 No. 109ª-30"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label245" runat="server" Text="(1)6584300"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="auto-style4">
                                                                <asp:Label ID="Label246" runat="server" Text="AXA Colpatria Seguros S.A.; AXA Colpatria Seguros de Vida S.A.; AXA ColpatriaCapitalizadora S.A."></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label247" runat="server" Text="Carrera 7 No.24-89"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label248" runat="server" Text="(1)3364677"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="auto-style4">
                                                                <asp:Label ID="Label249" runat="server" Text="Nacional de Seguros S.A."></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label250" runat="server" Text="Carrera 14 No. 89-48 Of 401"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label251" runat="server" Text="(1)7463219"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="auto-style4">
                                                                <asp:Label ID="Label252" runat="server" Text="Coface Colombia Seguros de Crédito S.A."></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label253" runat="server" Text="Carrera 15 No. 91-30 Of 601"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label254" runat="server" Text="(1)6231631"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <!-- Termina la tabla -->
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblClau_7" runat="server" Text=""></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblClau_8" runat="server" Text=""></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblClau_9" runat="server" Text=""></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblClau_10" runat="server" Text=""></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblClau_11" runat="server" Text=""></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblClau_12" runat="server" Text=""></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblClau_13" runat="server" Text=""></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblClau_14" runat="server" Text=""></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblClau_15" runat="server" Text=""></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblClau_16" runat="server" Text=""></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblClau_17" runat="server" Text=""></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblClau_18" runat="server" Text=""></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblClau_19" runat="server" Text=""></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblClau_20" runat="server" Text=""></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblClau_21" runat="server" Text=""></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblClau_22" runat="server" Text=""></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblClau_23" runat="server" Text=""></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblClau_24" runat="server" Text=""></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblClau_25" runat="server" Text=""></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblClau_26" runat="server" Text=""></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblClau_27" runat="server" Text=""></asp:Label></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr id="InfoFirmaHuella1" visible="false" runat="server">
                                    <td>
                                        <table width="100%">
                                            <tr align="center" style="background-color: #C0C0C0">
                                                <td>
                                                    <asp:Label ID="Label146" runat="server" Text="6. FIRMA Y HUELLA"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr id="InfoFirmaHuella2" visible="false" runat="server">
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td align="center" style="width: 80%;" valign="top">
                                                    <table>
                                                        <tr>
                                                            <td class="style7">COMO CONSTANCIA DE HABER LEIDO, ENTENDIDO Y ACEPTADO LO ANTERIOR, DECLARO QUE LA INFORMACIÓN QUE HE SUMINISTRADO ES EXACTA EN TODAS SUS PARTES Y FIRMO EL PRESENTE DOCUMENTO</td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    <table width="670" height="242" border="1">
                                                        <tr>
                                                            <td height="192">&nbsp;</td>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td height="42">
                                                                <asp:Label ID="Label149" runat="server" Text="FIRMA CLIENTE O APODERADO"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label273" runat="server" Text="FIRMA CLIENTE O APODERADO"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr id="infoEntrevista" visible="false" runat="server">
                                    <td>
                                        <table id="Table1" runat="server" width="100%">
                                            <tr>
                                                <td valign="top">
                                                    <table border="1px" width="100%">
                                                        <tr align="center">
                                                            <td style="background-color: #C0C0C0">
                                                                <asp:Label ID="Label166" runat="server" Text="7. INFORMACION ENTREVISTA"></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <table>
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:Label ID="lblLugarEntrevista" runat="server" Text="Lugar de la Entrevista"></asp:Label></td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="tbxLugarEntrevista" runat="server" MaxLength="20" Width="166px"></asp:TextBox><asp:RequiredFieldValidator ID="rfvLugarEntrevista" runat="server"
                                                                                            ControlToValidate="tbxLugarEntrevista"
                                                                                            ValidationGroup="vdgWillis"
                                                                                            ErrorMessage="*"
                                                                                            ForeColor="Red">
                                                                                        </asp:RequiredFieldValidator>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:Label ID="lblFechaEntrevist" runat="server" Text="Fecha de la Entrevista:"></asp:Label></td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="tbxFechaEntrevista" runat="server" MaxLength="15"></asp:TextBox>
                                                                                        <asp:CalendarExtender ID="ceFechaEntrevista" runat="server" Format="dd-MMM-yyyy" TargetControlID="tbxFechaEntrevista" />
                                                                                        <asp:RequiredFieldValidator ID="rfvFechaEntrevista" runat="server"
                                                                                            ControlToValidate="tbxFechaEntrevista"
                                                                                            ValidationGroup="vdgWillis"
                                                                                            ErrorMessage="*"
                                                                                            ForeColor="Red">
                                                                                        </asp:RequiredFieldValidator>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:Label ID="lblHoraEntrevista" runat="server" Text="Hora de entrevista"></asp:Label></td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="tbxHoraEntrevista" runat="server" MaxLength="15"></asp:TextBox></td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="height: 43px">
                                                                            <table>
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:Label ID="lblObservaciones1" runat="server" Text="Observaciones"></asp:Label></td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="tbxObservaciones1" runat="server" Height="100px" MaxLength="200" Width="366" TextMode="MultiLine"></asp:TextBox></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="auto-style8">
                                                                                        <asp:Label ID="lblNombreIntermediario" runat="server" Text="Nombre del intermediario" Width="320px"></asp:Label></td>
                                                                                    <td class="auto-style8">
                                                                                        <asp:TextBox ID="tbxNombreIntermediario" runat="server" MaxLength="80" Width="166px"></asp:TextBox><asp:RequiredFieldValidator ID="rfvNombreIntermediario" runat="server"
                                                                                            ControlToValidate="tbxNombreIntermediario"
                                                                                            ValidationGroup="vdgWillis"
                                                                                            ErrorMessage="*"
                                                                                            ForeColor="Red">
                                                                                        </asp:RequiredFieldValidator></td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="height: 43px">
                                                                            <table>
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:Label ID="lblNombreResponsable" runat="server" Text="Nombre del Asesor" Width="320px"></asp:Label></td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="tbxNombreResponsable" runat="server" MaxLength="80" Width="166px"></asp:TextBox>
                                                                                        <asp:RequiredFieldValidator ID="rfvNombreResponsable" runat="server"
                                                                                            ControlToValidate="tbxNombreResponsable"
                                                                                            ValidationGroup="vdgWillis"
                                                                                            ErrorMessage="*"
                                                                                            ForeColor="Red">
                                                                                        </asp:RequiredFieldValidator>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:Label ID="lblResultado" runat="server" Text="Resultado de la entrevista"></asp:Label></td>
                                                                                    <td>
                                                                                        <asp:DropDownList ID="ddlResultado" runat="server">
                                                                                            <asp:ListItem Value="">---</asp:ListItem>
                                                                                            <asp:ListItem Value="Aprobado">Aprobado</asp:ListItem>
                                                                                            <asp:ListItem Value="Rechazado">Rechazado</asp:ListItem>
                                                                                        </asp:DropDownList><asp:RequiredFieldValidator ID="rfvResultado"
                                                                                            ControlToValidate="ddlResultado"
                                                                                            ValidationGroup="vdgWillis"
                                                                                            ErrorMessage="*"
                                                                                            ForeColor="Red" InitialValue="" runat="server"></asp:RequiredFieldValidator></td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td valign="top">
                                                    <table border="1px" width="100%">
                                                        <tr align="center">
                                                            <td style="background-color: #C0C0C0">
                                                                <asp:Label ID="Label167" runat="server" Text="8. VERIFICACION DE LA INFORMACION"></asp:Label>
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
                                                                                        <asp:Label ID="Label25" runat="server" Text="Fecha de verificación:"></asp:Label></td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="tbxFechaVerificacion" runat="server" MaxLength="15"></asp:TextBox>
                                                                                        <asp:CalendarExtender ID="ceFechaVerificacion" runat="server" Format="dd-MMM-yyyy" TargetControlID="tbxFechaVerificacion" />
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
                                                                                        <asp:Label ID="Label26" runat="server" Text="Hora de Confirmación"></asp:Label></td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="tbxHoraVerificacion" runat="server" MaxLength="15"></asp:TextBox></td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="Label24" runat="server" Text="Nombre y Cargo de Quien verifica"></asp:Label></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:TextBox ID="tbxNombreVerifica" runat="server" MaxLength="80"></asp:TextBox></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="auto-style6">
                                                                            <table>
                                                                                <tr>
                                                                                    <td class="auto-style7">
                                                                                        <asp:Label ID="Label29" runat="server" Text="Firma"></asp:Label></td>
                                                                                    <td class="auto-style7">___________________________________</td>
                                                                                </tr>
                                                                                <table>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:Label ID="Label27" runat="server" Text="Observaciones"></asp:Label></td>
                                                                                        <td>
                                                                                            <asp:TextBox ID="tbxObservaciones2" runat="server" Height="100px" MaxLength="200" Width="366" TextMode="MultiLine"></asp:TextBox></td>
                                                                                    </tr>
                                                                                </table>

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
                            </div>
                            <!--Hasta aqui para Consultar-->
                            <h1></h1>
                            <br />
                            <h1>
                                <table id="tbDocumentos" runat="server" align="center" visible="false">
                                    <tr align="center">
                                        <td>
                                            <table id="Table2" runat="server">
                                                <tr align="left">
                                                    <td>
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <table>
                                                                        <tr align="center">
                                                                            <td bgcolor="#BBBBBB">
                                                                                <asp:Label ID="Label186" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Documentos adjuntos"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <table>
                                                                                    <tr align="center">
                                                                                        <td>
                                                                                            <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" BorderStyle="Solid" CellPadding="4" Font-Names="Calibri" Font-Size="Small" ForeColor="#333333" GridLines="Vertical" HeaderStyle-CssClass="gridViewHeader" HorizontalAlign="Center" OnRowCommand="GridView2_RowCommand" ShowHeaderWhenEmpty="True">
                                                                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                                                                <Columns>
                                                                                                    <asp:BoundField DataField="IdArchivo" HeaderText="IdArchivo" Visible="false" />
                                                                                                    <asp:BoundField DataField="NombreUsuario" HeaderText="Nombre Usuario" />
                                                                                                    <asp:BoundField DataField="FechaRegistro" HeaderText="Fecha Registro" />
                                                                                                    <asp:BoundField DataField="UrlArchivo" HeaderText="Nombre Archivo" />
                                                                                                    <asp:ButtonField ButtonType="Image" CommandName="Descargar" ImageUrl="~/Imagenes/Icons/downloads.png" Text="Descargar" />
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
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </h1>
                            <br />
                            <table align="center" style="width:50%" align="center">
                                <tr>
                                    <td colspan="2" align="center" bgcolor="#333399">
                                        <asp:Label ID="LtextAproRec" runat="server" ForeColor="White" Font-Bold="False" Font-Names="Calibri" Font-Size="Large" Text="Selecciona la opcion que deseas realizar:"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:Button ID="btnConfirm" runat="server" OnClick = "OnConfirm" Text = "Aprobar" OnClientClick = "Confirm()"/>
                                    </td>
                                    <td align="center">
                                        <asp:Button ID="btnRechazar" runat="server" OnClick = "OnConfirm" Text = "Rechazar" OnClientClick = "Confirm2()"/>
                                    </td>
                                </tr>
                            </table>
                            
                            
                            
                            <h1></h1>
                            
                            
                            
                            <h1></h1>
                            
                            
                            
                            <h1></h1>
                            
                            
                            
                            <h1></h1>
                            
                            
                            
                            <h1></h1>
                            
                            
                            
                            <h1></h1>
                            
                            
                            
                            <h1></h1>
                            
                            
                            
                            <h1></h1>
                            
                            
                            
                            <h1></h1>
                            
                            
                            
                            <h1></h1>
                            
                            
                            
                        </h1>
                    </td>
                </tr>
        </table>
        </ContentTemplate>
    </asp:UpdatePanel>