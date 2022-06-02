<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ConsultarFormCliente.ascx.cs"
    Inherits="ListasSarlaft.UserControls.ConsultarFormCliente" %>
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
                                                        <asp:Label ID="Label192" runat="server" Text="Año" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label193" runat="server" Text="Fecha desde" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label194" runat="server" Text="Fecha hasta" Font-Names="Calibri" Font-Size="Small"></asp:Label>
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
                                                            OnSelectedIndexChanged="DropDownList39_SelectedIndexChanged" Font-Names="Calibri"
                                                            Font-Size="Small">
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
                                                        <asp:TextBox ID="TextBox107" runat="server" Width="100px" Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TextBox108" runat="server" Width="90px" Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
                                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="true" TargetControlID="TextBox108"
                                                            Format="yyyy-MM-dd">
                                                        </asp:CalendarExtender>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TextBox109" runat="server" Width="90px" Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
                                                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="true" TargetControlID="TextBox109"
                                                            Format="yyyy-MM-dd">
                                                        </asp:CalendarExtender>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TextBox110" runat="server" Visible="false" Width="90px" Font-Names="Calibri"
                                                            Font-Size="Small"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TextBox111" runat="server" Visible="false" Width="90px" Font-Names="Calibri"
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
                                                        <asp:Label ID="Label201" runat="server" Text="Incluir tipo inusualidad" Font-Names="Calibri"
                                                            Font-Size="Small"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label202" runat="server" Text="Tipo de inusualidad" Font-Names="Calibri"
                                                            Font-Size="Small"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label203" runat="server" Text="Estado" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:DropDownList ID="DropDownList38" runat="server" Font-Names="Calibri" Font-Size="Small">
                                                            <asp:ListItem Value="NO">NO</asp:ListItem>
                                                            <asp:ListItem Value="SI">SI</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="DropDownList41" runat="server" Font-Names="Calibri" Font-Size="Small">
                                                            <asp:ListItem Value="Conve1">Nombre y/o identificación no consistentes con soportes</asp:ListItem>
                                                            <asp:ListItem Value="Conve2">Nombre de representante legal no coincide con cámara de comercio</asp:ListItem>
                                                            <asp:ListItem Value="Conve3">Información representante legal no consistente con documento de identidad</asp:ListItem>
                                                            <asp:ListItem Value="Conve4">Dirección domicilio u oficina no consistente con soporte</asp:ListItem>
                                                            <asp:ListItem Value="Conve5">Actividad económica no consistente con soporte</asp:ListItem>
                                                            <asp:ListItem Value="Conve6">Socios o accionistas no consistentes con soporte</asp:ListItem>
                                                            <asp:ListItem Value="Conve7">Cifras financieras no consistentes con estados financieros</asp:ListItem>
                                                            <asp:ListItem Value="Conve8">Falta verificación de listas</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="DropDownList42" runat="server" Font-Names="Calibri" Font-Size="Small">
                                                            <asp:ListItem Value="NO">NO</asp:ListItem>
                                                            <asp:ListItem Value="SI">SI</asp:ListItem>
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
                                                HorizontalAlign="Center" Font-Names="Calibri" Font-Size="Small" 
                                                AllowPaging="True" onpageindexchanging="GridView1_PageIndexChanging" 
                                                PageSize="10">
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
                    <table id="tbFormulario" runat="server" visible="false">
                        <tr>
                            <td>
                                <h1 style="font-size: 13px;">
                                    <table border="1px" style="width: 1015px">
                                        <tr>
                                            <td>
                                                <table align="center" width="100%">
                                                    <tr align="center">
                                                        <td>
                                                            <asp:Label ID="Label1" runat="server" Text="FORMULARIO CONOCIMIENTO DEL CLIENTE"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr align="center">
                                                        <td>
                                                            <table>
                                                                <tr>
                                                                    <td colspan="2" align="center">
                                                                        <asp:Label ID="Label4" runat="server" Text="Dia/Mes/Año"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label3" runat="server" Text="Fecha"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBox1" runat="server" ToolTip="Dia/Mes/Año" MaxLength="15"></asp:TextBox>
                                                                        <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="TextBox1"
                                                                            Format="dd-MMM-yyyy">
                                                                        </asp:CalendarExtender>
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
                                                    <tr valign="middle">
                                                        <td style="background-color: #CCCCCC;">
                                                            <asp:Label ID="Label5" runat="server" Text="En el evento en que el potencial cliente no cuente con la información solicitada en este formulario, deberá consignar dicha circunstancia en el espacio correspondiente."></asp:Label>
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
                                                                        <asp:Label ID="Label2" runat="server" Text="CLASE DE VINCULACIÓN:"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="DropDownList1" runat="server" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged"
                                                                            AutoPostBack="true">
                                                                            <asp:ListItem>---</asp:ListItem>
                                                                            <asp:ListItem>TOMADOR</asp:ListItem>
                                                                            <asp:ListItem>ASEGURADO</asp:ListItem>
                                                                            <asp:ListItem>BENEFICIARIO</asp:ListItem>
                                                                            <asp:ListItem>AFIANZADO</asp:ListItem>
                                                                            <asp:ListItem>PROVEEDOR</asp:ListItem>
                                                                            <asp:ListItem>INTERMEDIARIO</asp:ListItem>
                                                                            <asp:ListItem>OTRA</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label6" runat="server" Text="CUAL:" Visible="false"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBox2" runat="server" Visible="false" MaxLength="50"></asp:TextBox>
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
                                                    <tr valign="middle">
                                                        <td style="background-color: #DDDDDD;">
                                                            <asp:Label ID="Label7" runat="server" Text="INDIQUE LOS VÍNCULOS EXISTENTES ENTRE TOMADOR, ASEGURADO, AFIANZADO Y BENEFICIARIO: (INDIVIDUALIZACION DEL PRODUCTO)"></asp:Label>
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
                                                                        <asp:Label ID="Label8" runat="server" Text="Tomador-Asegurado"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="DropDownList2" runat="server" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged"
                                                                            AutoPostBack="True">
                                                                            <asp:ListItem>---</asp:ListItem>
                                                                            <asp:ListItem>Familiar</asp:ListItem>
                                                                            <asp:ListItem>Comercial</asp:ListItem>
                                                                            <asp:ListItem>Laboral</asp:ListItem>
                                                                            <asp:ListItem>Ninguna</asp:ListItem>
                                                                            <asp:ListItem>Otra</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label9" runat="server" Text="Cual:" Visible="false"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBox3" runat="server" Visible="false" MaxLength="50"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr align="left">
                                                                    <td>
                                                                        <asp:Label ID="Label10" runat="server" Text="Tomador-Beneficiario"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="DropDownList3" runat="server" OnSelectedIndexChanged="DropDownList3_SelectedIndexChanged"
                                                                            AutoPostBack="True">
                                                                            <asp:ListItem>---</asp:ListItem>
                                                                            <asp:ListItem>Familiar</asp:ListItem>
                                                                            <asp:ListItem>Comercial</asp:ListItem>
                                                                            <asp:ListItem>Laboral</asp:ListItem>
                                                                            <asp:ListItem>Ninguna</asp:ListItem>
                                                                            <asp:ListItem>Otra</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label11" runat="server" Text="Cual:" Visible="false"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBox4" runat="server" Visible="false" MaxLength="50"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr align="left">
                                                                    <td>
                                                                        <asp:Label ID="Label12" runat="server" Text="Asegurado-Beneficiario"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="DropDownList4" runat="server" OnSelectedIndexChanged="DropDownList4_SelectedIndexChanged"
                                                                            AutoPostBack="True">
                                                                            <asp:ListItem>---</asp:ListItem>
                                                                            <asp:ListItem>Familiar</asp:ListItem>
                                                                            <asp:ListItem>Comercial</asp:ListItem>
                                                                            <asp:ListItem>Laboral</asp:ListItem>
                                                                            <asp:ListItem>Ninguna</asp:ListItem>
                                                                            <asp:ListItem>Otra</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label13" runat="server" Text="Cual:" Visible="false"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBox5" runat="server" Visible="false" MaxLength="50"></asp:TextBox>
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
                                                            <asp:Label ID="Label14" runat="server" Text="1. PERSONA NATURAL"></asp:Label>
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
                                                                        <asp:Label ID="Label15" runat="server" Text="PRIMER APELLIDO"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBox6" runat="server" Width="200px" MaxLength="20"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label16" runat="server" Text="SEGUNDO APELLIDO"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBox7" runat="server" Width="200px" MaxLength="20"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label17" runat="server" Text="NOMBRES"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBox8" runat="server" Width="200px" MaxLength="45"></asp:TextBox>
                                                                    </td>
                                                                     <td>
                                                                        <asp:Label ID="LblSexo" runat="server" Text="Sexo"></asp:Label>
                                                                    </td>
                                                                     <td>
                                                                        <asp:DropDownList ID="CboSexo" runat="server" >
                                                                            <asp:ListItem>---</asp:ListItem>
                                                                            <asp:ListItem>Masculino</asp:ListItem>
                                                                            <asp:ListItem>Femenino</asp:ListItem>
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
                                                                        <asp:Label ID="Label18" runat="server" Text="TIPO DOCUMENTO"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="DropDownList5" runat="server">
                                                                            <asp:ListItem>---</asp:ListItem>
                                                                            <asp:ListItem>C.C.</asp:ListItem>
                                                                            <asp:ListItem>C.E.</asp:ListItem>
                                                                            <asp:ListItem>T.I.</asp:ListItem>
                                                                            <asp:ListItem>R.C.</asp:ListItem>
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
                                                                        <asp:Label ID="Label20" runat="server" Text="FECHA DE EXPEDICIÓN"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBox10" runat="server" MaxLength="15"></asp:TextBox>
                                                                        <asp:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="TextBox10"
                                                                            Format="dd-MMM-yyyy">
                                                                        </asp:CalendarExtender>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label21" runat="server" Text="LUGAR"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBox11" runat="server" MaxLength="20"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label22" runat="server" Text="FECHA DE NACIMIENTO"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBox12" runat="server" MaxLength="15"></asp:TextBox>
                                                                        <asp:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="TextBox12"
                                                                            Format="dd-MMM-yyyy">
                                                                        </asp:CalendarExtender>
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
                                                                        <asp:Label ID="Label23" runat="server" Text="NACIONALIDAD"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBox13" runat="server" MaxLength="20"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label24" runat="server" Text="OCUPACIÓN / OFICIO"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="DropDownList6" runat="server" Width="197px">
                                                                            <asp:ListItem>---</asp:ListItem>
                                                                            <asp:ListItem>ACCIONISTA</asp:ListItem>
                                                                            <asp:ListItem>AMA DE CASA</asp:ListItem>
                                                                            <asp:ListItem>ANALISTA</asp:ListItem>
                                                                            <asp:ListItem>ARQUITECTO</asp:ListItem>
                                                                            <asp:ListItem>ASISTENTE</asp:ListItem>
                                                                            <asp:ListItem>CONSULTOR</asp:ListItem>
                                                                            <asp:ListItem>DEPORTISTA</asp:ListItem>
                                                                            <asp:ListItem>DIRECTOR</asp:ListItem>
                                                                            <asp:ListItem>ESTUDIANTE</asp:ListItem>
                                                                            <asp:ListItem>GERENTE</asp:ListItem>
                                                                            <asp:ListItem>INGENIERO</asp:ListItem>
                                                                            <asp:ListItem>JEFE</asp:ListItem>
                                                                            <asp:ListItem>MECANICO</asp:ListItem>
                                                                            <asp:ListItem>OPERARIO</asp:ListItem>
                                                                            <asp:ListItem>OTRO</asp:ListItem>
                                                                            <asp:ListItem>PENSIONADO</asp:ListItem>
                                                                            <asp:ListItem>RENTISTA</asp:ListItem>
                                                                            <asp:ListItem>REPRESENTANTE LEGAL</asp:ListItem>
                                                                            <asp:ListItem>SOCIO</asp:ListItem>
                                                                            <asp:ListItem>SUBGERENTE</asp:ListItem>
                                                                            <asp:ListItem>TECNICO</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label25" runat="server" Text="PROFESIÓN"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="CboProfesion" runat="server" Width="189px">
                                                                        <asp:ListItem>---</asp:ListItem>
                                                                        <asp:ListItem>ABOGADO</asp:ListItem>
                                                                        <asp:ListItem>ADMINISTRADOR DE EMPRESAS</asp:ListItem>
                                                                        <asp:ListItem>ARQUITECTO</asp:ListItem>
                                                                        <asp:ListItem>CARICATURISTA</asp:ListItem>
                                                                        <asp:ListItem>CARPINTERO</asp:ListItem>
                                                                        <asp:ListItem>CIRUJANO PLASTICO</asp:ListItem>
                                                                        <asp:ListItem>CONSTRUCTOR</asp:ListItem>
                                                                        <asp:ListItem>CONSULTOR</asp:ListItem>
                                                                        <asp:ListItem>CONTADOR PUBLICO</asp:ListItem>
                                                                        <asp:ListItem>DECORADOR</asp:ListItem>
                                                                        <asp:ListItem>DEPORTISTA</asp:ListItem>
                                                                        <asp:ListItem>DIBUJANTE</asp:ListItem>
                                                                        <asp:ListItem>DISEÑADOR</asp:ListItem>
                                                                        <asp:ListItem>DISEÑADOR PAGINAS WEB</asp:ListItem>
                                                                        <asp:ListItem>EBANISTA</asp:ListItem>
                                                                        <asp:ListItem>ECONOMISTA</asp:ListItem>
                                                                        <asp:ListItem>ELECTRICISTA</asp:ListItem>
                                                                        <asp:ListItem>FISOTERAPEUTA</asp:ListItem>
                                                                        <asp:ListItem>FOTOGRAFO</asp:ListItem>
                                                                        <asp:ListItem>INGENIERO</asp:ListItem>
                                                                        <asp:ListItem>MEDICO</asp:ListItem>
                                                                        <asp:ListItem>MODISTA</asp:ListItem>
                                                                        <asp:ListItem>MUSICO</asp:ListItem>
                                                                        <asp:ListItem>NOTARIO</asp:ListItem>
                                                                        <asp:ListItem>ODONTOLOGO</asp:ListItem>
                                                                        <asp:ListItem>OFTALMOLOGO</asp:ListItem>
                                                                        <asp:ListItem>OPTOMETRA</asp:ListItem>
                                                                        <asp:ListItem>OTRO</asp:ListItem>
                                                                        <asp:ListItem>PLOMERO</asp:ListItem>
                                                                        <asp:ListItem>PROFESOR</asp:ListItem>
                                                                        <asp:ListItem>PSICOLOGO</asp:ListItem>
                                                                        <asp:ListItem>PUBLICISTA</asp:ListItem>
                                                                        <asp:ListItem>SASTRE</asp:ListItem>
                                                                        <asp:ListItem>SERVICIOS DE INFORMATICA</asp:ListItem>
                                                                        <asp:ListItem>TRADUCTOR</asp:ListItem>
                                                                        <asp:ListItem>TRANSPORTADOR</asp:ListItem>
                                                                        <asp:ListItem>VETERINARIO</asp:ListItem>
                                                            </asp:DropDownList>
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;</td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label26" runat="server" Text="ACTIVIDAD ECONÓMICA"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBox16" runat="server" Width="300px" MaxLength="80"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label27" runat="server" Text="CIIU"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBox17" runat="server" MaxLength="10"></asp:TextBox>
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
                                                                        <asp:Label ID="Label29" runat="server" Text="AREA"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBox19" runat="server" Width="160px" MaxLength="20"></asp:TextBox>
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
                                                                        <asp:Label ID="Label36" runat="server" Text="CIUDAD"></asp:Label>
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
                                                                        <asp:Label ID="Label37" runat="server" Text="TELEFONO"></asp:Label>
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
                                                                        <asp:Label ID="LblPNCorreoE" runat="server" Text="CORREO ELECTRONICO"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="TxtPNCorreoElectronico" runat="server" MaxLength="100" 
                                                                            Width="450px"></asp:TextBox>
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
                                                                        <asp:Label ID="Label39" runat="server" Text="POR SU CARGO O ACTIVIDAD MANEJA RECURSOS PUBLICOS?"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="DropDownList7" runat="server">
                                                                            <asp:ListItem>---</asp:ListItem>
                                                                            <asp:ListItem>SI</asp:ListItem>
                                                                            <asp:ListItem>NO</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label40" runat="server" Text="POR SU CARGO O ACTIVIDAD EJERCE ALGUN GRADO DE PODER PUBLICO?"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="DropDownList8" runat="server">
                                                                            <asp:ListItem>---</asp:ListItem>
                                                                            <asp:ListItem>SI</asp:ListItem>
                                                                            <asp:ListItem>NO</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label41" runat="server" Text="POR SU ACTIVIDAD U OFICIO, GOZA USTED DE RECONOCIMIENTO PUBLICO GENERAL?"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="DropDownList9" runat="server">
                                                                            <asp:ListItem>---</asp:ListItem>
                                                                            <asp:ListItem>SI</asp:ListItem>
                                                                            <asp:ListItem>NO</asp:ListItem>
                                                                        </asp:DropDownList>
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
                                                                        <asp:Label ID="Label42" runat="server" Text="SI ALGUNA DE LAS PREGUNTAS ANTERIORES ES AFIRMATIVA POR FAVOR ESPECIFIQUE:"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBox29" runat="server" Width="700px" MaxLength="200"></asp:TextBox>
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
                                                                        <asp:Label ID="Label43" runat="server" Text="INGRESOS MENSUALES"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="CboIngresos" runat="server" Width="300px">
                                                                            <asp:ListItem>---</asp:ListItem>
                                                                            <asp:ListItem>Menos de 1 Millón</asp:ListItem>
                                                                            <asp:ListItem>1 a 3 Millones</asp:ListItem>
                                                                            <asp:ListItem>3 a 5 Millones</asp:ListItem>
                                                                            <asp:ListItem>5 a 10 Millones</asp:ListItem>
                                                                            <asp:ListItem>10 a 20 Millones</asp:ListItem>
                                                                            <asp:ListItem>Más de 20 Millones</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label44" runat="server" Text="ACTIVOS $"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBox31" runat="server" Width="330px" MaxLength="100"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label45" runat="server" Text="EGRESO MENSUALES"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="CboEgresos" runat="server" Width="300px">
                                                                            <asp:ListItem>---</asp:ListItem>
                                                                            <asp:ListItem>Menos de 1 Millón</asp:ListItem>
                                                                            <asp:ListItem>1 a 3 Millones</asp:ListItem>
                                                                            <asp:ListItem>3 a 5 Millones</asp:ListItem>
                                                                            <asp:ListItem>5 a 10 Millones</asp:ListItem>
                                                                            <asp:ListItem>10 a 20 Millones</asp:ListItem>
                                                                            <asp:ListItem>Más de 20 Millones</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label46" runat="server" Text="PASIVOS $"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBox33" runat="server" Width="330px" MaxLength="100"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label47" runat="server" Text="OTROS INGRESOS"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="CboOtrosIngresos" runat="server" Width="300px">
                                                                            <asp:ListItem>---</asp:ListItem>
                                                                            <asp:ListItem>Menos de 1 Millón</asp:ListItem>
                                                                            <asp:ListItem>1 a 3 Millones</asp:ListItem>
                                                                            <asp:ListItem>3 a 5 Millones</asp:ListItem>
                                                                            <asp:ListItem>5 a 10 Millones</asp:ListItem>
                                                                            <asp:ListItem>10 a 20 Millones</asp:ListItem>
                                                                            <asp:ListItem>Más de 20 Millones</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label48" runat="server" Text="CONCEPTO OTROS INGRESOS"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBox35" runat="server" Width="330px" MaxLength="100"></asp:TextBox>
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
                                                            <asp:Label ID="Label49" runat="server" Text="2. PERSONA JURÍDICA"></asp:Label>
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
                                                                        <asp:Label ID="Label54" runat="server" Text="NOMBRES"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBox40" runat="server" MaxLength="45"></asp:TextBox>
                                                                    </td>
                                                                     <td>
                                                                        <asp:Label ID="LblCboSexoRL" runat="server" Text="SEXO"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                         <asp:DropDownList ID="CboSexoRepLegal" runat="server" >
                                                                            <asp:ListItem>---</asp:ListItem>
                                                                            <asp:ListItem>Masculino</asp:ListItem>
                                                                            <asp:ListItem>Femenino</asp:ListItem>
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
                                                                        <asp:Label ID="Label55" runat="server" Text="TIPO DOCUMENTO"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="DropDownList10" runat="server">
                                                                            <asp:ListItem>---</asp:ListItem>
                                                                            <asp:ListItem>C.C.</asp:ListItem>
                                                                            <asp:ListItem>C.E.</asp:ListItem>
                                                                            <asp:ListItem>PAS</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label56" runat="server" Text="NÚMERO"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBox41" runat="server" MaxLength="15"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label58" runat="server" Text="LUGAR DE EXPEDICIÓN"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBox43" runat="server" MaxLength="20" Width="300px"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;</td>
                                                                    <td>
                                                                        &nbsp;</td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table>
                                                                <tr>
                                                        <td class="style4">
                                                            <asp:Label ID="Label59" runat="server" Text="DATOS OF PPAL DIRECCIÓN"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TextBox44" runat="server" Width="180px" MaxLength="100"></asp:TextBox>
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
                                                        <td>
                                                            <asp:Label ID="Lbel57" runat="server" Width="60px" Text="CORREO"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TxtCorreoPrincipal" runat="server" Width="180px" MaxLength="10"></asp:TextBox>
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
                                                                <asp:Label ID="Label63" runat="server" Text="DATOS SUC DIRECCION"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="TextBox48" runat="server" Width="180px" MaxLength="100"></asp:TextBox>
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
                                                            <td>
                                                                <asp:Label ID="lblCEOf" runat="server" Width="60px" Text="CORREO"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="TxtCorreoSucursal" runat="server" Width="180px" MaxLength="10"></asp:TextBox>
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
                                                                            <asp:ListItem>UNIPERSONAL</asp:ListItem>
                                                                            <asp:ListItem>OTRA</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label68" runat="server" Text="ACTIVIDAD ECONÓMICA:"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="DropDownList12" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList12_SelectedIndexChanged">
                                                                            <asp:ListItem>---</asp:ListItem>
                                                                            <asp:ListItem>INDUSTRIAL</asp:ListItem>
                                                                            <asp:ListItem>COMERCIAL</asp:ListItem>
                                                                            <asp:ListItem>TRANSPORTE</asp:ListItem>
                                                                            <asp:ListItem>CONSTRUCCIÓN</asp:ListItem>
                                                                            <asp:ListItem>AGRÍCOLA</asp:ListItem>
                                                                            <asp:ListItem>CIVIL</asp:ListItem>
                                                                            <asp:ListItem>SERVICIOS FINANCIEROS</asp:ListItem>
                                                                            <asp:ListItem>OTRA</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label69" runat="server" Text="CUAL:" Visible="false"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBox52" runat="server" Visible="false" Width="190px" MaxLength="80"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label70" runat="server" Text="CIIU"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBox53" runat="server" MaxLength="10"></asp:TextBox>
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
                                                                        <asp:TextBox ID="TextBox54" runat="server" Width="230px" MaxLength="80"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label73" runat="server" Text="TIPO DE IDENTIFICACIÓN"></asp:Label>
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
                                                                        <asp:Label ID="Label74" runat="server" Text="NÚMERO"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBox56" runat="server" MaxLength="15"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label75" runat="server" Text="RAZON SOCIAL O NOMBRE COMPLETO"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBox55" runat="server" Width="230px" MaxLength="80"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label76" runat="server" Text="TIPO DE IDENTIFICACIÓN"></asp:Label>
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
                                                                        <asp:Label ID="Label77" runat="server" Text="NÚMERO"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBox57" runat="server" MaxLength="15"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label78" runat="server" Text="RAZON SOCIAL O NOMBRE COMPLETO"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBox58" runat="server" Width="230px" MaxLength="80"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label79" runat="server" Text="TIPO DE IDENTIFICACIÓN"></asp:Label>
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
                                                                        <asp:Label ID="Label80" runat="server" Text="NÚMERO"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBox59" runat="server" MaxLength="15"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label81" runat="server" Text="RAZON SOCIAL O NOMBRE COMPLETO"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBox60" runat="server" Width="230px" MaxLength="80"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label82" runat="server" Text="TIPO DE IDENTIFICACIÓN"></asp:Label>
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
                                                                        <asp:Label ID="Label83" runat="server" Text="NÚMERO"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBox61" runat="server" MaxLength="15"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label84" runat="server" Text="RAZON SOCIAL O NOMBRE COMPLETO"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBox62" runat="server" Width="230px" MaxLength="80"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label85" runat="server" Text="TIPO DE IDENTIFICACIÓN"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="DropDownList17" runat="server">
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
                                                                        <asp:TextBox ID="TextBox63" runat="server" MaxLength="15"></asp:TextBox>
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
                                                                        <asp:Label ID="Label87" runat="server" Text="INGRESOS MENSUALES"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="CboIngresosPJ" runat="server" Width="300px">
                                                                            <asp:ListItem>---</asp:ListItem>
                                                                            <asp:ListItem>Menos de 1 Millón</asp:ListItem>
                                                                            <asp:ListItem>1 a 3 Millones</asp:ListItem>
                                                                            <asp:ListItem>3 a 5 Millones</asp:ListItem>
                                                                            <asp:ListItem>5 a 10 Millones</asp:ListItem>
                                                                            <asp:ListItem>10 a 20 Millones</asp:ListItem>
                                                                            <asp:ListItem>Más de 20 Millones</asp:ListItem>
                                                                        </asp:DropDownList>
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
                                                                        <asp:Label ID="Label89" runat="server" Text="EGRESO MENSUALES"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="CboEgresosPJ" runat="server" Width="300px">
                                                                            <asp:ListItem>---</asp:ListItem>
                                                                            <asp:ListItem>Menos de 1 Millón</asp:ListItem>
                                                                            <asp:ListItem>1 a 3 Millones</asp:ListItem>
                                                                            <asp:ListItem>3 a 5 Millones</asp:ListItem>
                                                                            <asp:ListItem>5 a 10 Millones</asp:ListItem>
                                                                            <asp:ListItem>10 a 20 Millones</asp:ListItem>
                                                                            <asp:ListItem>Más de 20 Millones</asp:ListItem>
                                                                        </asp:DropDownList>
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
                                                                        <asp:Label ID="Label91" runat="server" Text="OTROS INGRESOS"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="CboOtrosIngresosPJ" runat="server" Width="300px">
                                                                            <asp:ListItem>---</asp:ListItem>
                                                                            <asp:ListItem>Menos de 1 Millón</asp:ListItem>
                                                                            <asp:ListItem>1 a 3 Millones</asp:ListItem>
                                                                            <asp:ListItem>3 a 5 Millones</asp:ListItem>
                                                                            <asp:ListItem>5 a 10 Millones</asp:ListItem>
                                                                            <asp:ListItem>10 a 20 Millones</asp:ListItem>
                                                                            <asp:ListItem>Más de 20 Millones</asp:ListItem>
                                                                        </asp:DropDownList>
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
                                        <tr>
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
                                        <tr>
                                            <td>
                                                <table width="100%">
                                                    <tr>
                                                        <td>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label94" runat="server" Text="REALIZA TRANSACCIONES EN MONEDA EXTRANJERA"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="DropDownList18" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList18_SelectedIndexChanged">
                                                                            <asp:ListItem>---</asp:ListItem>
                                                                            <asp:ListItem>NO</asp:ListItem>
                                                                            <asp:ListItem>SI</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label95" runat="server" Text="CUAL:" Visible="False"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="DropDownList19" runat="server" Visible="False" AutoPostBack="True"
                                                                            OnSelectedIndexChanged="DropDownList19_SelectedIndexChanged">
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
                                                                        <asp:TextBox ID="TextBox70" runat="server" Visible="False" MaxLength="80"></asp:TextBox>
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
                                                            <table width="100%">
                                                                <tr align="center" style="background-color: #C0C0C0">
                                                                    <td>
                                                                        <asp:Label ID="Label97" runat="server" Text="PRODUCTOS FINANCIEROS EN EL EXTERIOR"></asp:Label>
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
                                                                        <asp:Label ID="Label98" runat="server" Text="TIPO DE PRODUCTO"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label99" runat="server" Text="IDENTIFICACION O NÚMERO DEL PRODUCTO"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label100" runat="server" Text="ENTIDAD"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label101" runat="server" Text="MONTO"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label102" runat="server" Text="CIUDAD"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label103" runat="server" Text="PAIS"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label104" runat="server" Text="MONEDA"></asp:Label>
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
                                                            <asp:Label ID="Label105" runat="server" Text="4. INFORMACIÓN SOBRE RECLAMACIONES DE SEGUROS"></asp:Label>
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
                                                                        <asp:Label ID="Label106" runat="server" Text="RELACIONE A CONTINUACIÓN LAS RECLAMACIONES PRESENTADAS E INDEMNIZACIONES RECIBIDAS SOBRE SEGUROS EN LOS ÚLTIMOS DOS AÑOS"></asp:Label>
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
                                                                    </td>
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
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table width="100%">
                                                    <tr align="center" style="background-color: #C0C0C0">
                                                        <td>
                                                            <asp:Label ID="Label111" runat="server" Text="5. DECLARACIÓN DE ORIGEN DE FONDOS Y AUTORIZACIÓN CONSULTA CENTRALES DE RIESGO"></asp:Label>
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
                                                                    <td colspan="2">
                                                                        <asp:Label ID="Label112" runat="server" Text="Declaro expresamente que:"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label113" runat="server" Text="a. Los recursos que poseo provienen de las siguientes fuentes (detalle ocupacion, oficio, actividad o negocio):"
                                                                            Width="585px"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBox93" runat="server" Width="320px" MaxLength="100"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2">
                                                                        <asp:Label ID="Label114" runat="server" 
                                                                            Text="b. Tanto mi actividad, profesión u oficio es lícita y la ejerzo dentro del marco legal y los recursos que poseo no provienen de actividades ilícitas de las contempladas en el Código Penal Colombiano."></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2">
                                                                        <asp:Label ID="Label115" runat="server" 
                                                                            Text="c. La información que he suministrado en la solicitud y en este documento es veraz y verificable y me obligo a actualizarla anualmente."></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2">
                                                                        <asp:Label ID="Label116" runat="server" 
                                                                            Text="d. De manera irrevocable autorizo a las compañías de Seguros, Sociedades de Capitalización e Intermediarios de Seguros, con los que mantenga una relación comercial vigente para solicitar, consultar, procesar, suministrar, reportar o divulgar a cualquier entidad válidamente autorizada para manejar o administrar bases de datos, incluidas las entidades gubernamentales, información contenida en este formulario."></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2">
                                                                        <asp:Label ID="Label117" runat="server" 
                                                                            Text="e. Los recursos que se deriven del desarrollo de este contrato no se destinaran a la financiación del terrorismo, grupos terroristas o actividades terroristas."></asp:Label>
                                                                        <br />
                                                                        f. Autorizo de manera permanente e irrevocable a Seguros del Estado S.A. y 
                                                                        Seguros de Vida del Estado S.A. o a quien represente sus derechos para que con 
                                                                        fines estadísticos, de control, supervisión y de información comercial a otras 
                                                                        entidades, procese, reporte, conserve, consulte, suministre o actualice 
                                                                        cualquier información de carácter financiero, comercial, crediticio y personal 
                                                                        desde el momento de la solicitud de seguro o vinculación, a las centrales de 
                                                                        información o bases de datos debidamente constituidas que estime conveniente, en 
                                                                        los términos y durante el tiempo que los sistemas de bases de datos, las normas 
                                                                        y las autoridades lo establezcan<br /> La consecuencia de esta autorización sera 
                                                                        la inclusión de mi información en las mencionadas bases de datos y por tanto las 
                                                                        entidades del sector financiero, asegurador o de cualquier otro sector afiliadas 
                                                                        a dichas centrales conocerán mi comportamiento presente y pasado relacionado con 
                                                                        mis obligaciones financieras, comerciales, crediticias y personales o cualquier 
                                                                        otro dato personal o económico que estime pertinente.</td>
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
                                                            <asp:Label ID="Label118" runat="server" Text="6. DOCUMENTOS REQUERIDOS"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                <td>
                                    <table width="100%">
                                        <tr>
                                            <td colspan="2">
                                                <asp:Label ID="Label119" runat="server" 
                                                    Text="EN TODOS LOS CASOS ES NECESARIO ADJUNTAR FOTOCOPIA DEL DOCUMENTO DE IDENTIFICACIÓN (PARA PERSONAS JURÍDICAS SE DEBE ADJUNTAR EL DEL REPRESENTANTE LEGAL Y ORIGINAL DEL CERTIFICADO DE EXISTENCIA Y REPRESENTACIÓN LEGAL CON VIGENCIA NO SUPERIOR A TRES (3) MESES, EXPEDIDO POR LA CÁMARA DE COMERCIO)"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                &nbsp;</td>
                                        </tr>
                                        <tr valign="top">
                                            <td>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label121" runat="server" Text="a. PERSONA NATURAL"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label122" runat="server" 
                                                                Text="* Constancia de ingresos (Honorarios laborales, Certificado de Ingresos y Retenciones o el documento que corresponda)."></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label123" runat="server" 
                                                                Text="* Declaración de Renta del último período gravable disponible. (Si declara)"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label124" runat="server" 
                                                                Text="* Inventario general de los bienes objeto del seguro salvo cuando se trate de pólizas flotantes o automáticas."></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            &nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label128" runat="server" Text="3. PERSONA JURÍDICA"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label132" runat="server" 
                                                                Text="* Original del Certificado de Existencia y Representación Legal con vigencia no superior a (3) meses, expedido por la Camara de Comercio."></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label133" runat="server" 
                                                                Text="* Fotocopia del Registro Único Tributario (RUT), el cual puede ser obtenido directamente por la entidad vigilada."></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label129" runat="server" 
                                                                Text="* Fotocopia del documento de identificación del Representante Legal."></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label130" runat="server" 
                                                                Text="* Inventario general de los bienes objeto del seguro, salvo cuando se trate de pólizas flotantes o automáticas."></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label131" runat="server" 
                                                                Text="* Declaración de Renta del último período gravable disponible (Si declara) o estados financieros."></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            &nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style6">
                                                            <asp:Label ID="Label134" runat="server" 
                                                                Text="* APODERADO 1. En caso de que el cliente se presente a través de apoderado debe anexar poder debidamente firmado con reconocimiento de texto y firma ante notaria"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            &nbsp;</td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td>
                                                <table runat="server" >
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label135" runat="server" Text="c. INTERMEDIARIOS"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label205" runat="server" 
                                                                Text="* Documentación según políticas vigentes de la compañía"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            &nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label138" runat="server" Text="d. PROVEEDORES"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label139" runat="server" Text="* Original del Certificado de Existencia y Representación Legal expedido por la Camara de Comercio o por la entidad competente (Vigencia no superior a tres (3) meses)."></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label140" runat="server" Text="* Fotocopia del número de Identificación Tributaria NIT o RUT."></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label141" runat="server" Text="* Fotocopia del documento de identidad del Representante Legal."></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label142" runat="server" Text="* Declaración de renta del ultimo periodo gravable disponible (si declara) o estados financieros"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label143" runat="server" Text=" "></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label144" runat="server" Text="e. BENEFICIARIOS"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label145" runat="server" Text="* Documentación según políticas vigentes de la compañía"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
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
                                                            <asp:Label ID="Label146" runat="server" Text="7. FIRMA Y HUELLA"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table width="100%">
                                                    <tr>
                                                        <td align="center" valign="top" style="width: 80%;">
                                                            <table>
                                                               <tr>
                                                                    <td class="style7">
                                                                        COMO CONSTANCIA DE HABER LEÍDO, ENTENDIDO Y ACEPTADO LO ANTERIOR, FIRMO EL 
                                                                        PRESENTE DOCUMENTO<br />
                                                                        <asp:Label ID="Label148" runat="server" 
                                                                            Text="Y ESTARÉ DISPUESTO A LA VERIFICACIÓN DEL MISMO DECLARO QUE LA INFORMACIÓN"></asp:Label>
                                                                        <br />
                                                                        QUE HE SUMINISTRADO ES EXACTA EN TODAS SUS PARTES.</td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td align="center" style="width: 20%;">
                                                            <asp:Panel ID="Panel1" runat="server" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"
                                                                Height="93px" Width="92px">
                                                            </asp:Panel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center">
                                                            <asp:Label ID="Label151" runat="server" Text="__________________________________________________________________________________"></asp:Label>
                                                        </td>
                                                        <td>
                                                        </td>
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
                                        <tr>
                                            <td>
                                                <table width="100%" runat="server" visible="false">
                                                    <tr>
                                                        <td>
                                                            <table width="100%" border="1px">
                                                                <tr align="center">
                                                                    <td style="background-color: #C0C0C0">
                                                                        <asp:Label ID="Label166" runat="server" Text="8. INFORMACION ENTREVISTA"></asp:Label>
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
                                                                                                <asp:Label ID="Label152" runat="server" Text="LUGAR DE LA ENTREVISTA"></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="TextBox94" runat="server" MaxLength="20"></asp:TextBox>
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
                                                                                                <asp:Label ID="Label153" runat="server" Text="FECHA DE LA ENTREVISTA"></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="TextBox95" runat="server" MaxLength="15"></asp:TextBox>
                                                                                                <asp:CalendarExtender ID="CalendarExtender7" runat="server" TargetControlID="TextBox95"
                                                                                                    Format="dd-MMM-yyyy">
                                                                                                </asp:CalendarExtender>
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
                                                                                <td>
                                                                                    <table>
                                                                                        <tr>
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
                                                                                            <td>
                                                                                                <asp:Label ID="Label156" runat="server" Text="OBSERVACIONES"></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="TextBox97" runat="server" MaxLength="200"></asp:TextBox>
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
                                                                                                <asp:Label ID="Label157" runat="server" Text="NOMBRE INTERMEDIARIO Y/O ASESOR RESPONSABLE"></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="TextBox98" runat="server" MaxLength="80"></asp:TextBox>
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
                                                                                                <asp:Label ID="Label158" runat="server" Text="FIRMA INTERMEDIARIO Y/O ASESOR RESPONSABLE"></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="Label165" runat="server" Text="________________________"></asp:Label>
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
                                                            <table width="100%" border="1px">
                                                                <tr align="center">
                                                                    <td style="background-color: #C0C0C0">
                                                                        <asp:Label ID="Label167" runat="server" Text="9. VERIFICACION DE LA INFORMACION"></asp:Label>
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
                                                                                                <asp:Label ID="Label159" runat="server" Text="FECHA VERIFICACION"></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="TextBox100" runat="server" MaxLength="15"></asp:TextBox>
                                                                                                <asp:CalendarExtender ID="CalendarExtender8" runat="server" TargetControlID="TextBox100"
                                                                                                    Format="dd-MMM-yyyy">
                                                                                                </asp:CalendarExtender>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="Label160" runat="server" Text="HORA"></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="TextBox101" runat="server" MaxLength="15"></asp:TextBox>
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
                                                                                                <asp:Label ID="Label161" runat="server" Text="NOMBRE Y CARGO DE QUIEN VERIFICA"></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="TextBox102" runat="server" MaxLength="80"></asp:TextBox>
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
                                                                                                <asp:Label ID="Label162" runat="server" Text="FIRMA"></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="Label164" runat="server" Text="_______________________________"></asp:Label>
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
                                                                                                <asp:Label ID="Label163" runat="server" Text="OBSERVACIONES"></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="TextBox104" runat="server" MaxLength="200"></asp:TextBox>
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
                                    <table align="center">
                                        <tr align="center">
                                            <td>
                                                <table>
                                                    <tr align="center">
                                                        <td bgcolor="#BBBBBB">
                                                            <asp:Label ID="Label177" runat="server" Text="Convenciones inusualidades" Font-Names="Calibri"
                                                                Font-Size="Small"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr align="left">
                                                        <td>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label179" runat="server" Text="Nombre y/o identificación no consistentes con soportes"
                                                                            Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="DropDownList40" runat="server" Font-Names="Calibri" Font-Size="Small">
                                                                            <asp:ListItem>NO</asp:ListItem>
                                                                            <asp:ListItem>SI</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label178" runat="server" Text="Nombre de representante legal no coincide con cámara de comercio"
                                                                            Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="DropDownList31" runat="server" Font-Names="Calibri" Font-Size="Small">
                                                                            <asp:ListItem>NO</asp:ListItem>
                                                                            <asp:ListItem>SI</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label180" runat="server" Text="Información representante legal no consistente con documento de identidad"
                                                                            Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="DropDownList32" runat="server" Font-Names="Calibri" Font-Size="Small">
                                                                            <asp:ListItem>NO</asp:ListItem>
                                                                            <asp:ListItem>SI</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label181" runat="server" Text="Dirección domicilio u oficina no consistente con soporte"
                                                                            Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="DropDownList33" runat="server" Font-Names="Calibri" Font-Size="Small">
                                                                            <asp:ListItem>NO</asp:ListItem>
                                                                            <asp:ListItem>SI</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label182" runat="server" Text="Actividad económica no consistente con soporte"
                                                                            Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="DropDownList34" runat="server" Font-Names="Calibri" Font-Size="Small">
                                                                            <asp:ListItem>NO</asp:ListItem>
                                                                            <asp:ListItem>SI</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label183" runat="server" Text="Socios o accionistas no consistentes con soporte"
                                                                            Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="DropDownList35" runat="server" Font-Names="Calibri" Font-Size="Small">
                                                                            <asp:ListItem>NO</asp:ListItem>
                                                                            <asp:ListItem>SI</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label184" runat="server" Text="Cifras financieras no consistentes con estados financieros"
                                                                            Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="DropDownList36" runat="server" Font-Names="Calibri" Font-Size="Small">
                                                                            <asp:ListItem>NO</asp:ListItem>
                                                                            <asp:ListItem>SI</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label185" runat="server" Text="Falta verificación de listas" Font-Names="Calibri"
                                                                            Font-Size="Small"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="DropDownList37" runat="server" Font-Names="Calibri" Font-Size="Small">
                                                                            <asp:ListItem>NO</asp:ListItem>
                                                                            <asp:ListItem>SI</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                                <tr align="center">
                                                                    <td colspan="2">
                                                                        <asp:Button ID="Button6" runat="server" Text="Agregar a ROI" OnClick="Button6_Click"
                                                                            Font-Names="Calibri" Font-Size="Small" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr align="center">
                                            <td>
                                                <table>
                                                    <tr align="center">
                                                        <td bgcolor="#BBBBBB">
                                                            <asp:Label ID="Label168" runat="server" Text="Lista de documentos que adjunta" Font-Names="Calibri"
                                                                Font-Size="Small"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr align="left">
                                                        <td>
                                                            <table>
                                                                <tr>
                                                                    <td colspan="2" align="center">
                                                                        <asp:Label ID="Label197" runat="server" Text="PERSONA NATURAL" Font-Names="Calibri"
                                                                            Font-Size="Small"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label169" runat="server" Text="Constancia de ingresos" Font-Names="Calibri"
                                                                            Font-Size="Small"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="DropDownList23" runat="server" Font-Names="Calibri" Font-Size="Small">
                                                                            <asp:ListItem>NO</asp:ListItem>
                                                                            <asp:ListItem>SI</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label170" runat="server" Text="Declaración de renta del último período gravable"
                                                                            Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="DropDownList24" runat="server" Font-Names="Calibri" Font-Size="Small">
                                                                            <asp:ListItem>NO</asp:ListItem>
                                                                            <asp:ListItem>SI</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label171" runat="server" Text="Inventario general de los bienes objeto del seguro"
                                                                            Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="DropDownList25" runat="server" Font-Names="Calibri" Font-Size="Small">
                                                                            <asp:ListItem>NO</asp:ListItem>
                                                                            <asp:ListItem>SI</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2" align="center">
                                                                        <asp:Label ID="Label198" runat="server" Text="PERSONA JURIDICA" Font-Names="Calibri"
                                                                            Font-Size="Small"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label172" runat="server" Text="Original del certificado de existencia y representación legal"
                                                                            Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="DropDownList26" runat="server" Font-Names="Calibri" Font-Size="Small">
                                                                            <asp:ListItem>NO</asp:ListItem>
                                                                            <asp:ListItem>SI</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label173" runat="server" Text="Fotocopia del registro único tributario (RUT)"
                                                                            Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="DropDownList27" runat="server" Font-Names="Calibri" Font-Size="Small">
                                                                            <asp:ListItem>NO</asp:ListItem>
                                                                            <asp:ListItem>SI</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label174" runat="server" Text="Fotocopia del documento de identificación del representante legal"
                                                                            Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="DropDownList28" runat="server" Font-Names="Calibri" Font-Size="Small">
                                                                            <asp:ListItem>NO</asp:ListItem>
                                                                            <asp:ListItem>SI</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label175" runat="server" Text="Inventario general de los bienes objeto de seguro"
                                                                            Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="DropDownList29" runat="server" Font-Names="Calibri" Font-Size="Small">
                                                                            <asp:ListItem>NO</asp:ListItem>
                                                                            <asp:ListItem>SI</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label176" runat="server" Text="Declaración de renta del último período gravable disponible"
                                                                            Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="DropDownList30" runat="server" Font-Names="Calibri" Font-Size="Small">
                                                                            <asp:ListItem>NO</asp:ListItem>
                                                                            <asp:ListItem>SI</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                        <td colspan="2" align="center">
                                                            <asp:Label ID="Label57" runat="server" Text="INTERMEDIARIOS" Font-Names="Calibri"
                                                                Font-Size="Small"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label126" runat="server" Text="Documentación según políticas vigentes de la compañía"
                                                                Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="DropDownList43" runat="server" Font-Names="Calibri" Font-Size="Small">
                                                                <asp:ListItem>NO</asp:ListItem>
                                                                <asp:ListItem>SI</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" align="center">
                                                            <asp:Label ID="Label120" runat="server" Text="PROVEEDORES" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label127" runat="server" Text="Original del Certificado de Existencia y Representación Legal expedido por la Cámara de Comercio o por la entidad competente (Vigencia no superior a tres (3) meses)"
                                                                Font-Names="Calibri" Font-Size="Small" Width="370px"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="DropDownList44" runat="server" Font-Names="Calibri" Font-Size="Small">
                                                                <asp:ListItem>NO</asp:ListItem>
                                                                <asp:ListItem>SI</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label136" runat="server" Text="Fotocopia del número de Identificación Tributaria NIT o RUT"
                                                                Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="DropDownList45" runat="server" Font-Names="Calibri" Font-Size="Small">
                                                                <asp:ListItem>NO</asp:ListItem>
                                                                <asp:ListItem>SI</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label137" runat="server" Text="Fotocopia del documento de identidad del Representante Legal"
                                                                Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="DropDownList46" runat="server" Font-Names="Calibri" Font-Size="Small">
                                                                <asp:ListItem>NO</asp:ListItem>
                                                                <asp:ListItem>SI</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label147" runat="server" Text="Declaración de renta del ultimo periodo gravable disponible (si declara) o estados financieros"
                                                                Font-Names="Calibri" Font-Size="Small" Width="370px"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="DropDownList47" runat="server" Font-Names="Calibri" Font-Size="Small">
                                                                <asp:ListItem>NO</asp:ListItem>
                                                                <asp:ListItem>SI</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" align="center">
                                                            <asp:Label ID="Label125" runat="server" Text="BENEFICIARIOS" Font-Names="Calibri"
                                                                Font-Size="Small"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label666" runat="server" Text="Documentación según políticas vigentes de la compañía"
                                                                Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="DropDownList48" runat="server" Font-Names="Calibri" Font-Size="Small">
                                                                <asp:ListItem>NO</asp:ListItem>
                                                                <asp:ListItem>SI</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
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
                                                                                                <asp:Label ID="Label199" runat="server" Text="Adjuntar documento .pdf:" Font-Names="Calibri"
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
                                                Visible="False" Font-Names="Calibri" Font-Size="Small" />
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
                    <td colspan="2" align="center" runat="server" id="tdCaption">
                        &nbsp;
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
        <asp:PostBackTrigger ControlID="Button7" />
        <asp:PostBackTrigger ControlID="ImageButton1" />
        <asp:PostBackTrigger ControlID="GridView1" />
        <asp:PostBackTrigger ControlID="GridView2" />
    </Triggers>
</asp:UpdatePanel>
