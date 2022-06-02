<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Reporte.ascx.cs" Inherits="ListasSarlaft.UserControls.Riesgos.Reporte" %>
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
        height: 74px;
    }
</style>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <Triggers>
        <asp:PostBackTrigger ControlID="Button7" />
<asp:PostBackTrigger ControlID="Button6"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="Button1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="Button2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="Button3"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="Button4"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="Button5"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="Button8"></asp:PostBackTrigger>
    </Triggers>
    <ContentTemplate>
        <table align="center" bgcolor="#EEEEEE">
            <tr align="center" bgcolor="#333399">
                <td>
                    <asp:Label ID="Label140" runat="server" Font-Bold="False" Font-Names="Calibri" Font-Size="Large"
                        ForeColor="White" Text="Reporte"></asp:Label>
                </td>
            </tr>
            <tr align="center">
                <td>
                    <table>
                        <tr align="left">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label1" runat="server" Text="Tipo de reporte" Font-Names="Calibri"
                                    Font-Size="Small"></asp:Label>
                            </td>
                            <td bgcolor="#EEEEEE">
                                <asp:DropDownList ID="DropDownList1" runat="server" Width="200px" Font-Names="Calibri"
                                    Font-Size="Small" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                                    <asp:ListItem Value="---">---</asp:ListItem>
                                    <asp:ListItem Value="1">Riesgos</asp:ListItem>
                                    <asp:ListItem Value="2">Riesgos vs controles</asp:ListItem>
                                    <asp:ListItem Value="3">Riesgos vs eventos</asp:ListItem>
                                    <asp:ListItem Value="4">Riesgos vs planes de acción</asp:ListItem>
                                    <asp:ListItem Value="5">Cambios a los controles</asp:ListItem>
                                    <asp:ListItem Value="6">Cambios a los Riesgos</asp:ListItem>
                                    <asp:ListItem Value="7">Controles</asp:ListItem>
                                    <asp:ListItem Value="8">Causas sin Controles</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="DropDownList1"
                                    InitialValue="---" ForeColor="Red" ValidationGroup="consultar">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr align="left">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label153" runat="server" Text="Cadena de valor" Font-Names="Calibri"
                                    Font-Size="Small"></asp:Label>
                            </td>
                            <td bgcolor="#EEEEEE">
                                <asp:DropDownList ID="DropDownList52" runat="server" Width="200px" Font-Names="Calibri"
                                    Font-Size="Small" AutoPostBack="True" OnSelectedIndexChanged="DropDownList52_SelectedIndexChanged">
                                    <asp:ListItem Value="---">---</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr align="left">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label154" runat="server" Text="Macroproceso" Font-Names="Calibri"
                                    Font-Size="Small"></asp:Label>
                            </td>
                            <td bgcolor="#EEEEEE">
                                <asp:DropDownList ID="DropDownList53" runat="server" Width="200px" Font-Names="Calibri"
                                    Font-Size="Small" AutoPostBack="True" OnSelectedIndexChanged="DropDownList53_SelectedIndexChanged">
                                    <asp:ListItem Value="---">---</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr align="left">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label155" runat="server" Text="Proceso" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                            <td bgcolor="#EEEEEE">
                                <asp:DropDownList ID="DropDownList54" runat="server" Width="200px" Font-Names="Calibri"
                                    Font-Size="Small">
                                    <asp:ListItem Value="---">---</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr align="left">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label157" runat="server" Text="Riesgos globales" Font-Names="Calibri"
                                    Font-Size="Small"></asp:Label>
                            </td>
                            <td bgcolor="#EEEEEE">
                                <asp:DropDownList ID="DropDownList56" runat="server" Width="455px" Font-Names="Calibri"
                                    Font-Size="Small" AutoPostBack="True" OnSelectedIndexChanged="DropDownList56_SelectedIndexChanged">
                                    <asp:ListItem Value="---">---</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr align="left">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label158" runat="server" Text="Clasificación general" Font-Names="Calibri"
                                    Font-Size="Small"></asp:Label>
                            </td>
                            <td bgcolor="#EEEEEE">
                                <asp:DropDownList ID="DropDownList57" runat="server" Width="455px" Font-Names="Calibri"
                                    Font-Size="Small">
                                    <asp:ListItem Value="---">---</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr align="left">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label2" runat="server" Text="Riesgo inherente" Font-Names="Calibri"
                                    Font-Size="Small"></asp:Label>
                            </td>
                            <td bgcolor="#EEEEEE">
                                <asp:DropDownList ID="DropDownList2" runat="server" Width="200px" Font-Names="Calibri"
                                    Font-Size="Small">
                                    <asp:ListItem Value="---">---</asp:ListItem>
                                    <asp:ListItem Value="Extremo">Extremo</asp:ListItem>
                                    <asp:ListItem Value="Alto">Alto</asp:ListItem>
                                    <asp:ListItem Value="Moderado">Moderado</asp:ListItem>
                                    <asp:ListItem Value="Bajo">Bajo</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr align="left">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label3" runat="server" Text="Riesgo residual" Font-Names="Calibri"
                                    Font-Size="Small"></asp:Label>
                            </td>
                            <td bgcolor="#EEEEEE">
                                <asp:DropDownList ID="DropDownList3" runat="server" Width="200px" Font-Names="Calibri"
                                    Font-Size="Small">
                                    <asp:ListItem Value="---">---</asp:ListItem>
                                    <asp:ListItem Value="Extremo">Extremo</asp:ListItem>
                                    <asp:ListItem Value="Alto">Alto</asp:ListItem>
                                    <asp:ListItem Value="Moderado">Moderado</asp:ListItem>
                                    <asp:ListItem Value="Bajo">Bajo</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr align="left">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Larea" runat="server" Text="Área" Font-Names="Calibri"
                                    Font-Size="Small"></asp:Label>
                            </td>
                            <td bgcolor="#EEEEEE">
                                <asp:DropDownList ID="DDLareas" runat="server" Width="200px" Font-Names="Calibri"
                                    Font-Size="Small">
                                    <asp:ListItem Value="---">---</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr align="left">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label4" runat="server" Text="Empresa" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                            <td bgcolor="#EEEEEE">
                                <asp:DropDownList ID="DropDownList4" runat="server" Width="200px" Font-Names="Calibri"
                                    Font-Size="Small">
                                    <asp:ListItem Value="---">---</asp:ListItem>
                                    <asp:ListItem Value="1">Vida</asp:ListItem>
                                    <asp:ListItem Value="2">Generales</asp:ListItem>
                                    <asp:ListItem Value="3">Ambas</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr align="left">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label5" runat="server" Text="Fecha Inicial Modificación (INCLUSIVE)"
                                    Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                            <td bgcolor="#EEEEEE">
                                <asp:TextBox ID="TxtFechaIni" runat="server" Width="100px" Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
                                <asp:CalendarExtender ID="TxtFechaIni_CalendarExtender" runat="server" Format="yyyy-MM-dd"
                                    Enabled="True" TargetControlID="TxtFechaIni">
                                </asp:CalendarExtender>
                            </td>
                        </tr>
                        <tr align="left">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label6" runat="server" Text="Fecha Final Modificación (INCLUSIVE)"
                                    Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                            <td bgcolor="#EEEEEE">
                                <asp:TextBox ID="TxtFechaFin" runat="server" Width="100px" Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
                                <asp:CalendarExtender ID="txtFechaFin_CalendarExtender" runat="server" Format="yyyy-MM-dd"
                                    Enabled="True" TargetControlID="TxtFechaFin">
                                </asp:CalendarExtender>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr align="center">
                <td>
                    <table>
                        <tr>
                            <td>
                                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Imagenes/Icons/lupa.png"
                                    ToolTip="Consultar" ValidationGroup="consultar" OnClick="ImageButton1_Click" />
                            </td>
                            <td>
                                <asp:ImageButton ID="ImageButton5" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                    ToolTip="Cancelar" OnClick="ImageButton5_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr align="center" id="ReporteRiesgos" runat="server" visible="false">
                <td>
                    <table>
                        <tr align="left">
                            <td>
                                <asp:Button ID="Button6" runat="server" Text="Exportar" ToolTip="Exportar" Font-Names="Calibri"
                                    Font-Size="Small" OnClick="Button6_Click" />
                            </td>
                        </tr>
                        <tr align="center">
                            <td>
                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                    ForeColor="#333333" GridLines="Vertical" ShowHeaderWhenEmpty="True" HeaderStyle-CssClass="gridViewHeader"
                                    BorderStyle="Solid" HorizontalAlign="Center" Font-Names="Calibri" Font-Size="Small"
                                    AllowPaging="True" OnPageIndexChanging="GridView1_PageIndexChanging">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:BoundField HeaderText="Codigo Riesgo" DataField="CodigoRiesgo" />
                                        <asp:BoundField HeaderText="Riesgo" DataField="NombreRiesgo" />
                                        <asp:BoundField HeaderText="Fecha Registro" DataField="FechaRegistroRiesgo" />
                                        <asp:BoundField HeaderText="Clasificacion Riesgo" DataField="ClasificacionRiesgo" />
                                        <asp:BoundField HeaderText="Macroproceso" DataField="Macroproceso" />
                                        <asp:BoundField HeaderText="Proceso" DataField="Proceso" />
                                        <asp:BoundField HeaderText="Riesgo Inherente" DataField="RiesgoInherente" />
                                        <asp:BoundField HeaderText="Riesgo Residual" DataField="RiesgoResidual" />
                                        <asp:BoundField HeaderText="Nombre Area" DataField="NombreArea" />
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
            <tr align="center" id="ReporteRiesgosControles" runat="server" visible="false">
                <td>
                    <table>
                        <tr align="left">
                            <td>
                                <asp:Button ID="Button1" runat="server" Text="Exportar" ToolTip="Exportar" Font-Names="Calibri"
                                    Font-Size="Small" OnClick="Button1_Click" />
                            </td>
                        </tr>
                        <tr align="center">
                            <td>
                                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                    ForeColor="#333333" GridLines="Vertical" ShowHeaderWhenEmpty="True" HeaderStyle-CssClass="gridViewHeader"
                                    BorderStyle="Solid" HorizontalAlign="Center" Font-Names="Calibri" Font-Size="Small"
                                    AllowPaging="True" OnPageIndexChanging="GridView2_PageIndexChanging">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:BoundField HeaderText="Codigo Riesgo" DataField="CodigoRiesgo" />
                                        <asp:BoundField HeaderText="Riesgo" DataField="NombreRiesgo" />
                                        <asp:BoundField HeaderText="Fecha Registro" DataField="FechaRegistroRiesgo" />
                                        <asp:BoundField HeaderText="Clasificacion Riesgo" DataField="ClasificacionRiesgo" />
                                        <asp:BoundField HeaderText="Macroproceso" DataField="Macroproceso" />
                                        <asp:BoundField HeaderText="Riesgo Inherente" DataField="RiesgoInherente" />
                                        <asp:BoundField HeaderText="Riesgo Residual" DataField="RiesgoResidual" />
                                        <asp:BoundField HeaderText="Codigo Control" DataField="CodigoControl" />
                                        <asp:BoundField HeaderText="Control" DataField="NombreControl" />
                                        <asp:BoundField HeaderText="Fecha Registro" DataField="FechaRegistroControl" />
                                        <asp:BoundField HeaderText="Calificación" DataField="NombreEscala" />
                                        <asp:BoundField HeaderText="Nombre Area" DataField="NombreArea" />
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
            <tr align="center" id="ReporteRiesgosEventos" runat="server" visible="false">
                <td>
                    <table>
                        <tr align="left">
                            <td>
                                <asp:Button ID="Button2" runat="server" Text="Exportar" ToolTip="Exportar" Font-Names="Calibri"
                                    Font-Size="Small" OnClick="Button2_Click" />
                            </td>
                        </tr>
                        <tr align="center">
                            <td>
                                <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                    ForeColor="#333333" GridLines="Vertical" ShowHeaderWhenEmpty="True" HeaderStyle-CssClass="gridViewHeader"
                                    BorderStyle="Solid" HorizontalAlign="Center" Font-Names="Calibri" Font-Size="Small"
                                    AllowPaging="True" OnPageIndexChanging="GridView3_PageIndexChanging">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:BoundField HeaderText="Codigo Riesgo" DataField="CodigoRiesgo" />
                                        <asp:BoundField HeaderText="Riesgo" DataField="NombreRiesgo" />
                                        <asp:BoundField HeaderText="Fecha Registro" DataField="FechaRegistroRiesgo" />
                                        <asp:BoundField HeaderText="Clasificacion Riesgo" DataField="ClasificacionRiesgo" />
                                        <asp:BoundField HeaderText="Macroproceso" DataField="Macroproceso" />
                                        <asp:BoundField HeaderText="Riesgo Inherente" DataField="RiesgoInherente" />
                                        <asp:BoundField HeaderText="Riesgo Residual" DataField="RiesgoResidual" />
                                        <asp:BoundField HeaderText="Codigo Evento" DataField="CodigoEvento" />
                                        <asp:BoundField HeaderText="Descripción" DataField="DescripcionEvento" />
                                        <asp:BoundField HeaderText="Fecha Registro" DataField="FechaRegistroEvento" />
                                        <asp:BoundField HeaderText="Nombre Area" DataField="NombreArea" />
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
            <tr align="center" id="ReporteRiesgosPlanesAccion" runat="server" visible="false">
                <td>
                    <table>
                        <tr align="left">
                            <td>
                                <asp:Button ID="Button3" runat="server" Text="Exportar" ToolTip="Exportar" Font-Names="Calibri"
                                    Font-Size="Small" OnClick="Button3_Click" />
                            </td>
                        </tr>
                        <tr align="center">
                            <td>
                                <asp:GridView ID="GridView4" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                    ForeColor="#333333" GridLines="Vertical" ShowHeaderWhenEmpty="True" HeaderStyle-CssClass="gridViewHeader"
                                    BorderStyle="Solid" HorizontalAlign="Center" Font-Names="Calibri" Font-Size="Small"
                                    AllowPaging="True" OnPageIndexChanging="GridView4_PageIndexChanging">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:BoundField HeaderText="Codigo Riesgo" DataField="CodigoRiesgo" />
                                        <asp:BoundField HeaderText="Riesgo" DataField="NombreRiesgo" />
                                        <asp:BoundField HeaderText="Fecha Registro" DataField="FechaRegistroRiesgo" />
                                        <asp:BoundField HeaderText="Clasificacion Riesgo" DataField="ClasificacionRiesgo" />
                                        <asp:BoundField HeaderText="Macroproceso" DataField="Macroproceso" />
                                        <asp:BoundField HeaderText="Riesgo Inherente" DataField="RiesgoInherente" />
                                        <asp:BoundField HeaderText="Riesgo Residual" DataField="RiesgoResidual" />
                                        <asp:BoundField HeaderText="Descripción Plan Acción" DataField="DescripcionAccion" />
                                        <asp:BoundField HeaderText="Tipo Recurso" DataField="NombreTipoRecursoPlanAccion" />
                                        <asp:BoundField HeaderText="Estado" DataField="NombreEstadoPlanAccion" />
                                        <asp:BoundField HeaderText="Nombre Area" DataField="NombreArea" />
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
            <tr align="center" id="ReporteModControles" runat="server" visible="false">
                <td>
                    <table>
                        <tr align="left">
                            <td>
                                <asp:Button ID="Button4" runat="server" Text="Exportar" ToolTip="Exportar" Font-Names="Calibri"
                                    Font-Size="Small" OnClick="Button4_Click" />
                            </td>
                        </tr>
                        <tr align="center">
                            <td>
                                <asp:GridView ID="GridView5" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                    ForeColor="#333333" GridLines="Vertical" ShowHeaderWhenEmpty="True" HeaderStyle-CssClass="gridViewHeader"
                                    BorderStyle="Solid" HorizontalAlign="Center" Font-Names="Calibri" Font-Size="Small"
                                    AllowPaging="True" OnPageIndexChanging="GridView5_PageIndexChanging">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:BoundField HeaderText="Codigo Control" DataField="CodigoControl" />
                                        <asp:BoundField HeaderText="Nombre Control" DataField="NombreControl" />
                                        <asp:BoundField HeaderText="Responsable" DataField="ResponsableControl" />
                                        <asp:BoundField HeaderText="Fecha Registro" DataField="FechaRegistroControl" />
                                        <asp:BoundField HeaderText="Periodicidad" DataField="NombrePeriodicidad" />
                                        <asp:BoundField HeaderText="Nombre Test" DataField="NombreTest" />
                                        
                                        <asp:BoundField HeaderText="Variable" DataField="NombreVariable" />
                                        <asp:BoundField HeaderText="Categorpía" DataField="NombreCategoria" />
                                        <asp:BoundField HeaderText="Escala" DataField="NombreEscala" />
                                        <asp:BoundField HeaderText="Mitiga" DataField="NombreMitiga" />
                                        <asp:BoundField HeaderText="Fecha Modificacion" DataField="FechaModificacion" />
                                        <asp:BoundField HeaderText="Usuario Cambio" DataField="NombreUsuarioCambio" />
                                        <asp:BoundField HeaderText="Justificacion" DataField="JustificacionCambio" />
                                        
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
            <tr align="center" id="ReporteModRiesgos" runat="server" visible="false">
                <td>
                    <table>
                        <tr align="left">
                            <td>
                                <asp:Button ID="Button5" runat="server" Text="Exportar" ToolTip="Exportar" Font-Names="Calibri"
                                    Font-Size="Small" OnClick="Button5_Click" />
                            </td>
                        </tr>
                        <tr align="center">
                            <td>
                                <asp:GridView ID="GridView6" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                    ForeColor="#333333" GridLines="Vertical" ShowHeaderWhenEmpty="True" HeaderStyle-CssClass="gridViewHeader"
                                    BorderStyle="Solid" HorizontalAlign="Center" Font-Names="Calibri" Font-Size="Small"
                                    AllowPaging="True" OnPageIndexChanging="GridView6_PageIndexChanging">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:BoundField HeaderText="Codigo Riesgo" DataField="CodigoRiesgo" />
                                        <asp:BoundField HeaderText="Nombre Riesgo" DataField="NombreRiesgo" />
                                        <asp:BoundField HeaderText="Responsable" DataField="ResponsableRiesgo" />
                                        <asp:BoundField HeaderText="Fecha Registro" DataField="FechaRegistroRiesgo" />
                                        <asp:BoundField HeaderText="Clasificacion" DataField="ClasificacionRiesgo" />
                                        <asp:BoundField HeaderText="Clasificacion General" DataField="ClasificacionGeneralRiesgo" />
                                        <asp:BoundField HeaderText="Clasificacion Particular" DataField="ClasificacionParticularRiesgo" />
                                        <asp:BoundField HeaderText="Tipo Evento" DataField="TipoEvento" />
                                        <asp:BoundField HeaderText="Causas" DataField="Causas" />
                                        <asp:BoundField HeaderText="Consecuencias" DataField="Consecuencias" />
                                        <asp:BoundField HeaderText="Fecha Modificacion" DataField="FechaModificacion" />
                                        <asp:BoundField HeaderText="Usuario Cambio" DataField="NombreUsuarioCambio" />
                                        <asp:BoundField HeaderText="Justificacion" DataField="JustificacionCambio" />
                                        <asp:BoundField HeaderText="Nombre Area" DataField="NombreArea" />
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
            <tr align="center" id="ReporteControles" runat="server" visible="false">
                <td>
                    <table>
                        <tr align="left">
                            <td>
                                <asp:Button ID="Button7" runat="server" Text="Exportar" ToolTip="Exportar" Font-Names="Calibri"
                                    Font-Size="Small" OnClick="Button7_Click" />
                            </td>
                        </tr>
                        <tr align="center">
                            <td>
                                <asp:GridView ID="GridView7" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                    ForeColor="#333333" GridLines="Vertical" ShowHeaderWhenEmpty="True" HeaderStyle-CssClass="gridViewHeader"
                                    BorderStyle="Solid" HorizontalAlign="Center" Font-Names="Calibri" Font-Size="Small"
                                    AllowPaging="True" OnPageIndexChanging="GridView7_PageIndexChanging">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:BoundField HeaderText="Codigo Control" DataField="CodigoControl" />
                                        <asp:BoundField HeaderText="Control" DataField="NombreControl" />
                                        <asp:BoundField HeaderText="Fecha Registro" DataField="FechaRegistroControl" />
                                        <asp:BoundField HeaderText="Calificación" DataField="NombreEscala" />
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
            <tr align="center" id="ReporteCausasSinControl" runat="server" visible="false">
                <td>
                    <table>
                        <tr align="left">
                            <td>
                                <asp:Button ID="Button8" runat="server" Text="Exportar" ToolTip="Exportar" Font-Names="Calibri"
                                    Font-Size="Small" OnClick="Button8_Click" />
                            </td>
                        </tr>
                        <tr align="center">
                            <td>
                                <asp:GridView ID="GVcausasControl" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                    ForeColor="#333333" GridLines="Vertical" ShowHeaderWhenEmpty="True" HeaderStyle-CssClass="gridViewHeader"
                                    BorderStyle="Solid" HorizontalAlign="Center" Font-Names="Calibri" Font-Size="Small"
                                    AllowPaging="True" OnPageIndexChanging="GVcausasControl_PageIndexChanging" >
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:BoundField HeaderText="Causas Sin Control" DataField="NombreCausas" />
                                        <asp:BoundField HeaderText="Código Riesgo" DataField="CodigoRiesgo" />
                                        <asp:BoundField HeaderText="Riesgo" DataField="NombreRiesgo" />
                                        <asp:BoundField HeaderText="Descripción Riesgo" DataField="Descripcion" />
                                        <asp:BoundField HeaderText="Riesgo Inherente" DataField="RiesgoInherente" />
                                        <asp:BoundField HeaderText="Riesgo Residual" DataField="RiesgoResidual" />
                                        <asp:BoundField HeaderText="Código Evento" DataField="CodigoEvento" />
                                        <asp:BoundField HeaderText="Descripción Evento" DataField="DescripcionEvento" />
                                        <asp:BoundField HeaderText="Nombre Área" DataField="NombreArea" />
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
        <asp:PostBackTrigger ControlID="Button6" />
        <asp:PostBackTrigger ControlID="Button1" />
        <asp:PostBackTrigger ControlID="Button2" />
        <asp:PostBackTrigger ControlID="Button3" />
        <asp:PostBackTrigger ControlID="Button4" />
        <asp:PostBackTrigger ControlID="Button5" />
        <asp:PostBackTrigger ControlID="Button8" />
    </Triggers>
</asp:UpdatePanel>
