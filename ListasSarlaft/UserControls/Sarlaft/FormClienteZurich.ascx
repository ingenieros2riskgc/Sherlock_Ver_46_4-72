<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FormClienteZurich.ascx.cs"
    Inherits="ListasSarlaft.UserControls.FormClienteZurich" %>
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

    .style1
    {
        width: 332px;
    }

    .style2
    {
        width: 194px;
    }

    .style3
    {
        width: 191px;
    }

    .style4
    {
        width: 111px;
    }

    .style5
    {
        width: 112px;
    }

    .style6
    {
        height: 19px;
    }

    .style7
    {
        width: 753px;
    }

    .style8
    {
        width: 391px;
    }
</style>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <table align="center" bgcolor="#EEEEEE">
            <tr align="center" bgcolor="#333399">
                <td>
                    <asp:Label ID="Label204" runat="server" ForeColor="White" Text="Registrar formulario"
                        Font-Bold="False" Font-Names="Calibri" Font-Size="Large"></asp:Label>
                </td>
            </tr>
            <tr align="left">
                <td>
                    <h1 style="font-size: 13px;">
                        <table id="tbFormulario" runat="server" visible="true" border="1" style="width: 1015px">
                            <tr>
                                <td>
                                    <table align="center" width="100%">
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label191" runat="server" Text="Tipo de Formulario:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlTipoFormulario" runat="server"
                                                    AutoPostBack="true" OnSelectedIndexChanged="ddlTipoFormulario_SelectedIndexChanged">
                                                    <asp:ListItem>---</asp:ListItem>
                                                    <asp:ListItem>PERSONA NATURAL</asp:ListItem>
                                                    <asp:ListItem>PERSONA JURÍDICA</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>

                            <tr id="Encabezado" runat="server" visible="false">
                                <td>
                                    <table align="center" width="100%">
                                        <tr align="center">
                                            <td>
                                                <asp:Label ID="Label1" runat="server" Text="FORMULARIO CONOCIMIENTO DEL CLIENTE"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr align="center">
                                            <td>
                                                <asp:Label ID="Label192" runat="server" Text="ZURICH SEGUROS"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr align="center">
                                            <td>
                                                <table>
                                                    <tr>
                                                        <td align="center"></td>
                                                        <td align="center">
                                                            <asp:Label ID="Label4" runat="server" Text="SUCURSAL"></asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="Label193" runat="server" Text="DD/MM/AAAA"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label3" runat="server" Text="FECHA DE DILIGENCIAMIENTO"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="tbxSucursal" runat="server" MaxLength="25"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TextBox1" runat="server" ToolTip="DD/MM/AAAA" MaxLength="15"></asp:TextBox>
                                                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="TextBox1"
                                                                Format="dd-MMM-yyyy"></asp:CalendarExtender>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>

                            <tr id="InfoClaseVinculacion" visible="false">
                                <td>
                                    <table align="left" width="100%">
                                        <tr>
                                            <td>
                                                <table>
                                                    <tr align="left">
                                                        <td>
                                                            <asp:Label ID="Label2" runat="server" Text="CLASE DE VINCULACIÓN:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="false">
                                                                <asp:ListItem>---</asp:ListItem>
                                                                <asp:ListItem>TOMADOR</asp:ListItem>
                                                                <asp:ListItem>ASEGURADO</asp:ListItem>
                                                                <asp:ListItem>BENEFICIARIO</asp:ListItem>
                                                                <asp:ListItem>APODERADO</asp:ListItem>
                                                                <asp:ListItem>AFIANZADO</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>

                            <tr id="infoNota2" visible="false">
                                <td>
                                    <table width="100%">
                                        <tr valign="middle">
                                            <td style="background-color: #DDDDDD;">
                                                <asp:Label ID="Label7" runat="server"
                                                    Text="INDIQUE LOS VÍNCULOS EXISTENTES ENTRE TOMADOR, ASEGURADO, AFIANZADO Y BENEFICIARIO"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>

                            <tr id="infoVinculos" visible="false">
                                <td>
                                    <table align="left" width="100%">
                                        <tr>
                                            <td>
                                                <table>
                                                    <tr align="left">
                                                        <td>
                                                            <asp:Label ID="Label8" runat="server" Text="TOMADOR-ASEGURADO-AFIANZADO"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="false">
                                                                <asp:ListItem>---</asp:ListItem>
                                                                <asp:ListItem>FAMILIAR</asp:ListItem>
                                                                <asp:ListItem>COMERCIAL</asp:ListItem>
                                                                <asp:ListItem>LABORAL</asp:ListItem>
                                                                <asp:ListItem>PERSONAL</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr align="left">
                                                        <td>
                                                            <asp:Label ID="Label10" runat="server" Text="TOMADOR-BENEFICIARIO"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="DropDownList3" runat="server" AutoPostBack="false">
                                                                <asp:ListItem>---</asp:ListItem>
                                                                <asp:ListItem>FAMILIAR</asp:ListItem>
                                                                <asp:ListItem>COMERCIAL</asp:ListItem>
                                                                <asp:ListItem>LABORAL</asp:ListItem>
                                                                <asp:ListItem>PERSONAL</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr align="left">
                                                        <td>
                                                            <asp:Label ID="Label12" runat="server" Text="ASEGURADO-BENEFICIARIO"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="DropDownList4" runat="server" AutoPostBack="False">
                                                                <asp:ListItem>---</asp:ListItem>
                                                                <asp:ListItem>FAMILIAR</asp:ListItem>
                                                                <asp:ListItem>COMERCIAL</asp:ListItem>
                                                                <asp:ListItem>LABORAL</asp:ListItem>
                                                                <asp:ListItem>PERSONAL</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>

                            <tr id="infoNota1" visible="false">
                                <td>
                                    <table width="100%">
                                        <tr valign="middle">
                                            <td style="background-color: #CCCCCC;">
                                                <asp:Label ID="Label5" runat="server"
                                                    Text="NOTA: Para el correcto diligenciamiento de la siguiente información, tenga en cuenta no dejar espaciones en blanco. De no poseer la información, favor registre No Aplica (N.A)"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>

                            <tr id="infoTituloPN" visible="false">
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
                            <tr id="infoPN" visible="false">
                                <td>
                                    <table width="100%">
                                        <tr>
                                            <td>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label15" runat="server" Text="PRIMER APELLIDO"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TextBox6" runat="server" Width="170px" MaxLength="20"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label16" runat="server" Text="SEGUNDO APELLIDO"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TextBox7" runat="server" Width="170px" MaxLength="20"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label17" runat="server" Text="PRIMER NOMBRE"></asp:Label>
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TextBox8" runat="server" Width="200px" MaxLength="45"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label6" runat="server" Text="SEGUNDO NOMBRE"></asp:Label>
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="tbxSdoNombrePN" runat="server" Width="200px" MaxLength="45"></asp:TextBox>
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
                                                            <asp:Label ID="Label18" runat="server" Text="TIPO DOCUMENTO"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="DropDownList5" runat="server">
                                                                <asp:ListItem>---</asp:ListItem>
                                                                <asp:ListItem>C.C.</asp:ListItem>
                                                                <asp:ListItem>C.E.</asp:ListItem>
                                                                <asp:ListItem>T.I.</asp:ListItem>
                                                                <asp:ListItem>R.C.</asp:ListItem>
                                                                <asp:ListItem>PASAPORTE</asp:ListItem>
                                                                <asp:ListItem>OTRO</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label19" runat="server" Text="NÚMERO"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TextBox9" runat="server" MaxLength="15"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label21" runat="server" Text="LUGAR DE EXPEDICIÓN"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TextBox11" runat="server" MaxLength="20"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label20" runat="server" Text="FECHA DE EXPEDICIÓN"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TextBox10" runat="server" MaxLength="15"></asp:TextBox>
                                                            <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="TextBox10"
                                                                Format="dd-MMM-yyyy"></asp:CalendarExtender>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="LblSexo" runat="server" Text="SEXO"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="CboSexo" runat="server">
                                                                <asp:ListItem>---</asp:ListItem>
                                                                <asp:ListItem>MASCULINO</asp:ListItem>
                                                                <asp:ListItem>FEMENINO</asp:ListItem>
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
                                                            <asp:Label ID="Label22" runat="server" Text="FECHA DE NACIMIENTO"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TextBox12" runat="server" MaxLength="15"></asp:TextBox>
                                                            <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="TextBox12"
                                                                Format="dd-MMM-yyyy"></asp:CalendarExtender>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label23" runat="server" Text="NACIONALIDAD 1" Width="120px"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TextBox13" runat="server" MaxLength="20" Width="150px"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label9" runat="server" Text="NACIONALIDAD 2" Width="120px"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="tbxNacionalidadPN2" runat="server" MaxLength="20" Width="150px"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label24" runat="server" Text="OCUPACIÓN / PROFESIÓN"></asp:Label>
                                                            <asp:TextBox ID="tbxProfesionPN" runat="server" MaxLength="20" Width="150px"></asp:TextBox>
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
                                                            <asp:Label ID="Label11" runat="server" Text="ESTADO CIVIL"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlEstadoCivil" runat="server">
                                                                <asp:ListItem>---</asp:ListItem>
                                                                <asp:ListItem>CASADO</asp:ListItem>
                                                                <asp:ListItem>SOLTERO</asp:ListItem>
                                                                <asp:ListItem>SEPARADO</asp:ListItem>
                                                                <asp:ListItem>VIUDO</asp:ListItem>
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
                                                            <asp:Label ID="Label35" runat="server" Text="DIRECCIÓN RESIDENCIA"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TextBox25" runat="server" Width="500px" MaxLength="100"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label36" runat="server" Text="CIUDAD RESIDENCIA"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TextBox26" runat="server" MaxLength="20"></asp:TextBox>
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
                                                            <asp:Label ID="Label37" runat="server" Text="TELÉFONO"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TextBox27" runat="server" MaxLength="10"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label38" runat="server" Text="CELULAR"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TextBox28" runat="server" MaxLength="10"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="LblPNCorreoE" runat="server" Text="EMAIL"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="tbxPNCorreoElectronico" runat="server" MaxLength="100" Width="450px"></asp:TextBox>
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
                                                            <asp:Label ID="Label26" runat="server" Text="ACTIVIDAD ECONÓMICA PRINCIPAL"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlActivEconoPpal" runat="server">
                                                                <asp:ListItem>---</asp:ListItem>
                                                                <asp:ListItem>ASALARIADO</asp:ListItem>
                                                                <asp:ListItem>ESTUDIANTE</asp:ListItem>
                                                                <asp:ListItem>AMA DE CASA</asp:ListItem>
                                                                <asp:ListItem>RENTISTA</asp:ListItem>
                                                                <asp:ListItem>PENSIONADO</asp:ListItem>
                                                                <asp:ListItem>INDEPENDIENTE</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label27" runat="server" Text="SI ES INDEPENDIENTE, ESPECIFIQUE:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="tbxIndependiente" runat="server" MaxLength="10"></asp:TextBox>
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
                                                            <asp:Label ID="Label28" runat="server" Text="NOMBRE DE LA EMPRESA DONDE TRABAJA"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TextBox18" runat="server" Width="300px" MaxLength="80"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label30" runat="server" Text="CARGO"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TextBox20" runat="server" Width="200px" MaxLength="20"></asp:TextBox>
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
                                                            <asp:Label ID="Label31" runat="server" Text="CIUDAD"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TextBox21" runat="server" Width="160px" MaxLength="20"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label32" runat="server" Text="DIRECCIÓN"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TextBox22" runat="server" Width="250px" MaxLength="100"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label33" runat="server" Text="TELEFONO"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TextBox23" runat="server" MaxLength="10"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label34" runat="server" Text="FAX"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TextBox24" runat="server" MaxLength="10"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr align="center">
                                            <td>
                                                <table>
                                                    <tr align="center">
                                                        <td>
                                                            <asp:Label ID="Label39" runat="server" Text="NOTA: Diligenciar la información en pesos" Style="text-align: center"></asp:Label>
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
                                                            <asp:Label ID="Label43" runat="server" Text="INGRESOS MENSUALES $"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="tbxPNIngresosMen" runat="server" Width="160px" MaxLength="20"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label44" runat="server" Text="ACTIVOS $"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TextBox31" runat="server" Width="160px" MaxLength="20"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label45" runat="server" Text="EGRESO MENSUALES $"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="tbxPNEgresosMen" runat="server" Width="160px" MaxLength="20"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label46" runat="server" Text="PASIVOS $"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TextBox33" runat="server" Width="160px" MaxLength="20"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label47" runat="server" Text="OTROS INGRESOS $"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="tbxPNOtrosIngresos" runat="server" Width="160px" MaxLength="20"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label48" runat="server" Text="CONCEPTO OTROS INGRESOS"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TextBox35" runat="server" Width="160px" MaxLength="20"></asp:TextBox>
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
                                                            <asp:Label ID="Label13" runat="server" Text="ES USTED SUJETO DE OBLIGACIONES TRIBUTARIAS EN OTRO PAÍS O GRUPO DE PAISES?"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlPNObligacionPais" runat="server">
                                                                <asp:ListItem>---</asp:ListItem>
                                                                <asp:ListItem>SI</asp:ListItem>
                                                                <asp:ListItem>NO</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label25" runat="server" Text="ESPECIFIQUE EL PAÍS:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="tbxPNObligacionPais" runat="server" Width="160px" MaxLength="20"></asp:TextBox></td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>

                            <tr id="infoTituloPJ" visible="false">
                                <td>
                                    <table width="100%">
                                        <tr align="center">
                                            <td style="background-color: #C0C0C0">
                                                <asp:Label ID="Label49" runat="server" Text="1. PERSONA JURÍDICA"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr id="infoPJ" visible="false">
                                <td>
                                    <table width="100%">
                                        <tr>
                                            <td>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label50" runat="server" Text="RAZÓN O DENOMINACIÓN SOCIAL"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TextBox36" runat="server" Width="400px" MaxLength="80"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label51" runat="server" Text="NIT"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TextBox37" runat="server" MaxLength="15"></asp:TextBox>
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
                                                            <asp:Label ID="Label208" runat="server" Text="TIPO DE SOCIEDAD:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlTipoSociedad" runat="server">
                                                                <asp:ListItem>---</asp:ListItem>
                                                                <asp:ListItem>LTDA</asp:ListItem>
                                                                <asp:ListItem>S.A.</asp:ListItem>
                                                                <asp:ListItem>S.A.S</asp:ListItem>
                                                                <asp:ListItem>SC</asp:ListItem>
                                                                <asp:ListItem>COOP</asp:ListItem>
                                                                <asp:ListItem>E.U.</asp:ListItem>
                                                                <asp:ListItem>E.S.</asp:ListItem>
                                                                <asp:ListItem>OTRA</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label209" runat="server" Text="CUAL:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="tbxTipoSociedad" runat="server" Width="160px" MaxLength="20"></asp:TextBox></td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label52" runat="server" Text="REPRESENTANTE LEGAL: PRIMER APELLIDO"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TextBox38" runat="server" MaxLength="20"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label53" runat="server" Text="SEGUNDO APELLIDO"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TextBox39" runat="server" MaxLength="20"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label54" runat="server" Text="PRIMER NOMBRE"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TextBox40" runat="server" MaxLength="45"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label01" runat="server" Text="SEGUNDO NOMBRE"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="tbxPJSdoNombre" runat="server" MaxLength="45"></asp:TextBox>
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
                                                            <asp:Label ID="Label55" runat="server" Text="TIPO DOCUMENTO"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="DropDownList10" runat="server">
                                                                <asp:ListItem>---</asp:ListItem>
                                                                <asp:ListItem>C.C.</asp:ListItem>
                                                                <asp:ListItem>C.E.</asp:ListItem>
                                                                <asp:ListItem>OTRO</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label210" runat="server" Text="CUAL?"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="tbxPJOtroDoc" runat="server" MaxLength="15"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label56" runat="server" Text="NÚMERO"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TextBox41" runat="server" MaxLength="15"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label58" runat="server" Text="LUGAR Y FECHA DE EXPEDICIÓN"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TextBox43" runat="server" MaxLength="20" Width="200px"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label211" runat="server" Text="NACIONALIDAD 1"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="tbxPJNacionalidad1" runat="server" MaxLength="20" Width="100px"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label212" runat="server" Text="NACIONALIDAD 2"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="tbxPJNacionalidad2" runat="server" MaxLength="20" Width="100px"></asp:TextBox>
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
                                                            <asp:Label ID="Label59" runat="server" Text="DATOS OFICINA PRINCIPAL: DIRECCIÓN" Width="280px"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TextBox44" runat="server" Width="280px" MaxLength="150"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label60" runat="server" Text="CIUDAD"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TextBox45" runat="server" Width="100px" MaxLength="20"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label61" runat="server" Text="TELÉFONO"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TextBox46" runat="server" Width="70px" MaxLength="10"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label62" runat="server" Width="35px" Text="FAX"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TextBox47" runat="server" Width="70px" MaxLength="10"></asp:TextBox>
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
                                                            <asp:Label ID="Lbel57" runat="server" Width="80px" Text="PAG. WEB"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="tbxPJPagWeb" runat="server" Width="280px" MaxLength="10"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label213" runat="server" Width="60px" Text="CORREO"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="tbxPJCorreoPrincipal" runat="server" Width="280px" MaxLength="100"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table>
                                                    <tr>
                                                        <td class="style5">
                                                            <asp:Label ID="Label63" runat="server" Text="DATOS SUCURSAL O AGENCIA: DIRECCION" Width="280px"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TextBox48" runat="server" Width="280px" MaxLength="150"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label64" runat="server" Text="CIUDAD"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TextBox49" runat="server" Width="100px" MaxLength="20"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label65" runat="server" Text="TELÉFONO"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TextBox50" runat="server" Width="70px" MaxLength="10"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label66" runat="server" Width="35px" Text="FAX"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TextBox51" runat="server" Width="70px" MaxLength="10"></asp:TextBox>
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
                                                            <asp:Label ID="Label67" runat="server" Text="TIPO DE EMPRESA:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="DropDownList11" runat="server">
                                                                <asp:ListItem>---</asp:ListItem>
                                                                <asp:ListItem>PÚBLICA</asp:ListItem>
                                                                <asp:ListItem>PRIVADA</asp:ListItem>
                                                                <asp:ListItem>MIXTA</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label68" runat="server" Text="ACTIVIDAD ECONÓMICA:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="DropDownList12" runat="server">
                                                                <asp:ListItem>---</asp:ListItem>
                                                                <asp:ListItem>INDUSTRIAL</asp:ListItem>
                                                                <asp:ListItem>COMERCIAL</asp:ListItem>
                                                                <asp:ListItem>TRANSPORTE</asp:ListItem>
                                                                <asp:ListItem>CONSTRUCCIÓN</asp:ListItem>
                                                                <asp:ListItem>AGROINDUSTRIA</asp:ListItem>
                                                                <asp:ListItem>CIVIL</asp:ListItem>
                                                                <asp:ListItem>FINANCIERO</asp:ListItem>
                                                                <asp:ListItem>SERVICIOS</asp:ListItem>
                                                                <asp:ListItem>SALUD</asp:ListItem>
                                                                <asp:ListItem>OTRA</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label69" runat="server" Text="CUAL:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TextBox52" runat="server" Width="190px" MaxLength="80"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label70" runat="server" Text="CÓDIGO CIIU"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TextBox53" runat="server" MaxLength="10"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="8">
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label214" runat="server" Text="BREVE DESCRIPCIÓN DEL OBJETO SOCIAL:" Width="260"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="tbxPJDescObjSoc" runat="server" MaxLength="500" Width="550px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
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
                                                            <asp:Label ID="Label71" runat="server"
                                                                Text="RELACIONE LOS ACCIONISTAS O ASOCIADOS QUE TENGAN DIRECTA O INDIRECTAMENTE MAS DEL 5% DEL CAPITAL SOCIAL, APORTE O PARTICIPACION (EN CASO DE REQUERIR MAS ESPACIO DEBE ANEXARSE LA RELACION):"></asp:Label>
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
                                                            <asp:Label ID="Label215" runat="server" Text="CONSECUTIVO"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label73" runat="server" Text="TIPO DE IDENTIFICACIÓN"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label74" runat="server" Text="NÚMERO"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label72" runat="server" Text="RAZON SOCIAL O NOMBRE COMPLETO"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ID="tbxPJConsecutivo1" runat="server" Width="100px" MaxLength="5"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="DropDownList13" runat="server">
                                                                <asp:ListItem>---</asp:ListItem>
                                                                <asp:ListItem>C.C.</asp:ListItem>
                                                                <asp:ListItem>C.E.</asp:ListItem>
                                                                <asp:ListItem>T.I.</asp:ListItem>
                                                                <asp:ListItem>NIT</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TextBox56" runat="server" MaxLength="15"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TextBox54" runat="server" Width="230px" MaxLength="80"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ID="tbxPJConsecutivo2" runat="server" Width="100px" MaxLength="5"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="DropDownList14" runat="server">
                                                                <asp:ListItem>---</asp:ListItem>
                                                                <asp:ListItem>C.C.</asp:ListItem>
                                                                <asp:ListItem>C.E.</asp:ListItem>
                                                                <asp:ListItem>T.I.</asp:ListItem>
                                                                <asp:ListItem>NIT</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TextBox57" runat="server" MaxLength="15"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TextBox55" runat="server" Width="230px" MaxLength="80"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ID="tbxPJConsecutivo3" runat="server" Width="100px" MaxLength="5"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="DropDownList15" runat="server">
                                                                <asp:ListItem>---</asp:ListItem>
                                                                <asp:ListItem>C.C.</asp:ListItem>
                                                                <asp:ListItem>C.E.</asp:ListItem>
                                                                <asp:ListItem>T.I.</asp:ListItem>
                                                                <asp:ListItem>NIT</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TextBox59" runat="server" MaxLength="15"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TextBox58" runat="server" Width="230px" MaxLength="80"></asp:TextBox>
                                                        </td>

                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ID="tbxPJConsecutivo4" runat="server" Width="100px" MaxLength="5"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="DropDownList16" runat="server">
                                                                <asp:ListItem>---</asp:ListItem>
                                                                <asp:ListItem>C.C.</asp:ListItem>
                                                                <asp:ListItem>C.E.</asp:ListItem>
                                                                <asp:ListItem>T.I.</asp:ListItem>
                                                                <asp:ListItem>NIT</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TextBox61" runat="server" MaxLength="15"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TextBox60" runat="server" Width="230px" MaxLength="80"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr align="center">
                                            <td>
                                                <table>
                                                    <tr align="center">
                                                        <td>
                                                            <asp:Label ID="Label75" runat="server" Text="NOTA: Diligenciar la información en pesos" Style="text-align: center"></asp:Label>
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
                                                            <asp:Label ID="Label87" runat="server" Text="INGRESOS MENSUALES $"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="tbxPJIngresosMen" runat="server" Width="330px" MaxLength="100"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label88" runat="server" Text="ACTIVOS $"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TextBox65" runat="server" Width="330px" MaxLength="100"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label89" runat="server" Text="EGRESOS MENSUALES $"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="tbxPJEgresosMen" runat="server" Width="330px" MaxLength="100"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label90" runat="server" Text="PASIVOS $"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TextBox67" runat="server" Width="330px" MaxLength="100"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label91" runat="server" Text="OTROS INGRESOS $"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="tbxPJOtroIngresos" runat="server" Width="330px" MaxLength="100"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label92" runat="server" Text="CONCEPTO OTROS INGRESOS"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TextBox69" runat="server" Width="330px" MaxLength="100"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>

                            <tr id="infoTituloPF" visible="false">
                                <td>
                                    <table width="100%">
                                        <tr align="center" style="background-color: #C0C0C0">
                                            <td>
                                                <asp:Label ID="Label93" runat="server" Text="2. ACTIVIDAD EN OPERACIONES INTERNACIONALES"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr id="infoPF1" visible="false">
                                <td>
                                    <table width="100%">
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label94" runat="server" Text="REALIZA TRANSACCIONES EN MONEDA EXTRANJERA?"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="DropDownList18" runat="server" AutoPostBack="False">
                                                    <asp:ListItem>---</asp:ListItem>
                                                    <asp:ListItem>NO</asp:ListItem>
                                                    <asp:ListItem>SI</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label95" runat="server" Text="TIPO DE TRANSACCIONES:" Visible="False"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="DropDownList19" runat="server" AutoPostBack="False" Visible="False">
                                                    <asp:ListItem>---</asp:ListItem>
                                                    <asp:ListItem>IMPORTACIONES</asp:ListItem>
                                                    <asp:ListItem>EXPORTACIONES</asp:ListItem>
                                                    <asp:ListItem>INVERSIONES</asp:ListItem>
                                                    <asp:ListItem>TRANSFERENCIAS</asp:ListItem>
                                                    <asp:ListItem>OTRA</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label96" runat="server" Text="INDIQUE CUAL:" Visible="False"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextBox70" runat="server" MaxLength="80" Visible="False"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label29" runat="server" Text="TIENE PRODUCTOS FINACIEROS EN EL EXTERIOR?"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlPFProdExterior" runat="server" AutoPostBack="False">
                                                    <asp:ListItem>---</asp:ListItem>
                                                    <asp:ListItem>NO</asp:ListItem>
                                                    <asp:ListItem>SI</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                <asp:Label ID="Label97" runat="server" Text="Especifique los productos financieros en el siguiente cuadro:"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>

                                            <td>
                                                <asp:Label ID="Label40" runat="server" Text="TIPO DE PRODUCTO"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label41" runat="server" Text="IDENTIFICACION O NÚMERO DEL PRODUCTO"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label42" runat="server" Text="ENTIDAD"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label194" runat="server" Text="MONTO"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label195" runat="server" Text="CIUDAD"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label196" runat="server" Text="PAIS"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label197" runat="server" Text="MONEDA"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="TextBox71" runat="server" MaxLength="50"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextBox72" runat="server" MaxLength="50"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextBox73" runat="server" MaxLength="80"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextBox74" runat="server" MaxLength="100"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextBox75" runat="server" MaxLength="20"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextBox76" runat="server" MaxLength="20"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextBox77" runat="server" MaxLength="20"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="TextBox78" runat="server" MaxLength="50"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextBox79" runat="server" MaxLength="50"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextBox80" runat="server" MaxLength="80"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextBox81" runat="server" MaxLength="100"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextBox82" runat="server" MaxLength="20"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextBox83" runat="server" MaxLength="20"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextBox84" runat="server" MaxLength="20"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="tbxPFTipoPdto2" runat="server" MaxLength="50"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="tbxPFIdPdto2" runat="server" MaxLength="50"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="tbxPFEntidadPdto2" runat="server" MaxLength="80"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="tbxPFMontoPdto2" runat="server" MaxLength="100"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="tbxPFCiudadPdto2" runat="server" MaxLength="20"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="tbxPFPaisPdto2" runat="server" MaxLength="20"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="tbxPFMonedaPdto2" runat="server" MaxLength="20"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>

                            <tr id="infoTituloDeclaracionFondos" visible="false">
                                <td>
                                    <table width="100%">
                                        <tr align="center" style="background-color: #C0C0C0">
                                            <td>
                                                <asp:Label ID="Label111" runat="server" Text="3. DECLARACIÓN DE ORIGEN Y DESTINO DE FONDOS"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr id="infoDeclaracionFondos" visible="false">
                                <td>
                                    <table width="100%">
                                        <tr>
                                            <td>
                                                <table>
                                                    <tr>
                                                        <td colspan="2">
                                                            <asp:Label ID="Label112" runat="server" Text="Declaro expresamente que:"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label113" runat="server" Text="" Width="585px"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TextBox93" runat="server" MaxLength="100" Width="320px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <asp:Label ID="Label114" runat="server" Text=""></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <asp:Label ID="Label115" runat="server" Text=""></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <asp:Label ID="Label116" runat="server" Text=""></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>

                            <tr id="infoTituloSeguros" visible="false">
                                <td>
                                    <table width="100%">
                                        <tr align="center" style="background-color: #C0C0C0">
                                            <td>
                                                <asp:Label ID="Label105" runat="server" Text="4. INFORMACIÓN SOBRE RECLAMACIONES DE SEGUROS"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr id="infoSeguros1" visible="false">
                                <td>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label106" runat="server"
                                                    Text="RELACIONE A CONTINUACIÓN LAS RECLAMACIONES PRESENTADAS E INDEMNIZACIONES RECIBIDAS SOBRE SEGUROS EN LOS ÚLTIMOS DOS AÑOS"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr id="infoSeguros2" visible="false">
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
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="TextBox85" runat="server" MaxLength="4"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextBox86" runat="server" MaxLength="15"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextBox87" runat="server" MaxLength="80"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextBox88" runat="server" MaxLength="18"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="DropDownList20" runat="server">
                                                    <asp:ListItem>---</asp:ListItem>
                                                    <asp:ListItem>RECLAM.</asp:ListItem>
                                                    <asp:ListItem>INDEMNIZ.</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="TextBox89" runat="server" MaxLength="4"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextBox90" runat="server" MaxLength="15"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextBox91" runat="server" MaxLength="80"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextBox92" runat="server" MaxLength="18"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="DropDownList21" runat="server">
                                                    <asp:ListItem>---</asp:ListItem>
                                                    <asp:ListItem>RECLAM.</asp:ListItem>
                                                    <asp:ListItem>INDEMNIZ.</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>

                            <tr id="infoAutorizacion" visible="false">
                                <td>
                                    <table width="100%">
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label117" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label119" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label121" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label122" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label123" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label124" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label128" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label129" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label130" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label206" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label131" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label132" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label133" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label134" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label135" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label138" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label139" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label140" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label141" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label142" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label143" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label144" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label145" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label148" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label159" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label160" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label163" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label198" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label199" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label200" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label201" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label202" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label203" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label205" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>

                            <tr id="infoTituloDocRequeridos" visible="false">
                                <td>
                                    <table width="100%">
                                        <tr align="center" style="background-color: #C0C0C0">
                                            <td>
                                                <asp:Label ID="Label118" runat="server" Text="5. DOCUMENTOS MÍNIMOS REQUERIDOS"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr id="infoDocRequeridos" valign="top" visible="false">
                                <td>
                                    <table width="100%">
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label98" runat="server"
                                                    Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label99" runat="server"
                                                    Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label103" runat="server"
                                                    Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label104" runat="server"
                                                    Text=""></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>

                            <tr id="InfoFirmaHuella1" visible="false">
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
                            <tr id="InfoFirmaHuella2" visible="false">
                                <td>
                                    <table width="100%">
                                        <tr>
                                            <td align="center" style="width: 80%;" valign="top">
                                                <table>
                                                    <tr>
                                                        <td class="style7">BAJO LA GRAVEDAD DE JURAMENTO DECLARO HABER LEÍDO, ENTIENDO Y ACEPTADO LO SEÑALADO EN EL PRESENTE DOCUMENTO. ASÍ MISMO, DECLARO QUE LA INFORMACIÓN QUE HE SUMINISTRADO ES EXACTA EN TODAS SUS PARTES, FIRMO COMO CONSTANCIA
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td align="center" style="width: 20%;">
                                                <asp:Panel ID="Panel1" runat="server" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" Height="110px" Width="92px">
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center">
                                                <asp:Label ID="Label151" runat="server" Text="__________________________________________________________________________________"></asp:Label>
                                            </td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td align="center">
                                                <asp:Label ID="Label149" runat="server" Text="FIRMA CLIENTE O REPRESENTANTE LEGAL"></asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:Label ID="Label150" runat="server" Text="HUELLA"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>

                            <tr id="infoNota3" visible="false">
                                <td>
                                    <table width="100%">
                                        <tr valign="middle">
                                            <td style="background-color: #DDDDDD;">
                                                <asp:Label ID="Label207" runat="server"
                                                    Text=""></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>

                            <tr id="infoEntrevista" visible="false">
                                <td>
                                    <table id="Table1" runat="server" width="100%">
                                        <tr>
                                            <td valign="top">
                                                <table border="1px" width="100%">
                                                    <tr align="center">
                                                        <td style="background-color: #C0C0C0">
                                                            <asp:Label ID="Label166" runat="server" Text="7. INFORMACION ENTREVISTA"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table>
                                                                <tr>
                                                                    <td style="height: 43px">
                                                                        <table>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="Label152" runat="server" Text="LUGAR DE LA ENTREVISTA"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="TextBox94" runat="server" MaxLength="20" Width="166px"></asp:TextBox>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="Label153" runat="server" Text="FECHA ENTREVISTA"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="TextBox95" runat="server" MaxLength="15"></asp:TextBox>
                                                                                    <asp:CalendarExtender ID="CalendarExtender5" runat="server" Format="dd-MMM-yyyy" TargetControlID="TextBox95" />
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="Label154" runat="server" Text="HORA"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="TextBox96" runat="server" MaxLength="15"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="height: 43px">
                                                                        <table>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="Label157" runat="server" Text="NOMBRE DEL RESPONSABLE DE LA ENTREVISTA (Contacto directo con el cliente)" Width="320px"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="TextBox98" runat="server" MaxLength="80" Width="166px"></asp:TextBox>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="Label155" runat="server" Text="RESULTADO:"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="DropDownList22" runat="server">
                                                                                        <asp:ListItem>---</asp:ListItem>
                                                                                        <asp:ListItem>Aceptado</asp:ListItem>
                                                                                        <asp:ListItem>Rechazado</asp:ListItem>
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
                                                                                <td style="height: 43px">
                                                                                    <asp:Label ID="Label158" runat="server" Text="FIRMA DEL RESPONSABLE DE LA ENTREVISTA"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="height: 90px"></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="Label165" runat="server" Text="________________________________________________"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="Label100" runat="server" Text="C.C. No. "></asp:Label>
                                                                                    <asp:TextBox ID="tbxCCResponsableEntrevista" runat="server" MaxLength="80" Width="166px"></asp:TextBox>
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
                                                                                    <asp:Label ID="Label156" runat="server" Text="OBSERVACIONES"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="TextBox97" runat="server" MaxLength="200" Width="366" Height="100px"></asp:TextBox>
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
                                                                    <td style="height: 43px">
                                                                        <table>
                                                                            <tr>
                                                                                <td></td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="height: 43px">
                                                                        <table>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="Label161" runat="server" Text="NOMBRE FUNCIONARIO QUE VERIFICA CONTENIDO Y SOPORTES DEL FORMULARIO" Width="320px"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="TextBox102" runat="server" MaxLength="80" Width="196px"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <table>
                                                                            <tr>
                                                                                <td style="height: 43px">
                                                                                    <asp:Label ID="Label162" runat="server" Text="FIRMA FUNCIONARIO RESPONSABLE DE VERIFICAR CONTENIDO Y SOPORTES"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="height: 90px"></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="Label164" runat="server" Text="________________________________________________"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="Label101" runat="server" Text="C.C. No. "></asp:Label>
                                                                                    <asp:TextBox ID="tbxCCResponsableVerificaEntrevista" runat="server" MaxLength="80" Width="166px"></asp:TextBox>
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
                                                                                    <asp:Label ID="Label102" runat="server" Text="OBSERVACIONES"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tbxObsResponsableVerificaEntrevista" runat="server" MaxLength="200" Width="366" Height="100px"></asp:TextBox>
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
                </td>
            </tr>
        </table>

        </h1>
        <br />
        <h1>
            <table id="tbDocumentos" runat="server" visible="false" align="center">
                <tr align="center">
                    <td>
                        <table>
                            <tr align="left">
                                <td>
                                    <table>
                                        <tr>
                                            <td colspan="2">
                                                <table>
                                                    <tr align="center">
                                                        <td bgcolor="#BBBBBB">
                                                            <asp:Label ID="Label186" runat="server" Text="Documentos adjuntos" Font-Names="Calibri"
                                                                Font-Size="Small"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table>
                                                                <tr align="center">
                                                                    <td colspan="3">
                                                                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                                                            ForeColor="#333333" GridLines="Vertical" ShowHeaderWhenEmpty="True" HeaderStyle-CssClass="gridViewHeader"
                                                                            BorderStyle="Solid" HorizontalAlign="Center" Font-Names="Calibri" Font-Size="Small"
                                                                            OnRowCommand="GridView1_RowCommand">
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
                                                                        <asp:Label ID="Label187" runat="server" Text="Adjuntar documento .pdf:" Font-Names="Calibri"
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
                    <asp:Button ID="Button5" runat="server" Text="Siguiente" OnClick="Button5_Click"
                        Font-Names="Calibri" Font-Size="Small" Visible="False" />
                </td>
                <td>
                    <asp:Button ID="Button6" runat="server" Text="Anterior" Visible="False" OnClick="Button6_Click"
                        Font-Names="Calibri" Font-Size="Small" />
                </td>
                <td>
                    <asp:Button ID="Button1" runat="server" Text="Guardar" OnClick="Button1_Click" Font-Names="Calibri"
                        Font-Size="Small" />
                </td>
                <td>
                    <asp:Button ID="Button2" runat="server" Text="Imprimir" Visible="false" OnClick="Button2_Click"
                        Font-Names="Calibri" Font-Size="Small" />
                </td>
                <td>
                    <asp:Button ID="Button3" runat="server" Text="Limpiar" OnClick="Button3_Click" Font-Names="Calibri"
                        Font-Size="Small" />
                </td>
                <td>
                    <asp:Button ID="Button4" runat="server" Text="Continuar" Visible="false" OnClick="Button4_Click"
                        Font-Names="Calibri" Font-Size="Small" />
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
        <asp:PostBackTrigger ControlID="Button6" />
        <asp:PostBackTrigger ControlID="ImageButton1" />
        <asp:PostBackTrigger ControlID="GridView1" />
    </Triggers>
</asp:UpdatePanel>
