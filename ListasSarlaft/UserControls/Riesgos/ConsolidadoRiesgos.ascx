<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ConsolidadoRiesgos.ascx.cs"
    Inherits="ListasSarlaft.UserControls.Riesgos.ConsolidadoRiesgos" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<link href="../../../Styles/AdminSitio.css" rel="stylesheet" type="text/css" />
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
<script type="text/javascript">
    function ChangeCalendarView(sender, args) {
        sender._switchMode("months", true);
    }

    function onCalendarHidden(sender) {
        //        var cal = $find("Calendar1");

        //        if (cal._monthsBody) {
        //            for (var i = 0; i < cal._monthsBody.rows.length; i++) {
        //                var row = cal._monthsBody.rows[i];
        //                for (var j = 0; j < row.cells.length; j++) {
        //                    Sys.UI.DomEvent.removeHandler(row.cells[j].firstChild, "click", call);
        //                }
        //            }
        //        }

        if (sender._monthsBody) {
            for (var i = 0; i < sender._monthsBody.rows.length; i++) {
                var row = sender._monthsBody.rows[i];
                for (var j = 0; j < row.cells.length; j++) {
                    Sys.UI.DomEvent.removeHandler(row.cells[j].firstChild, "click", call);
                }
            }
        }

    }

    function onCalendarShown(sender) {

        //        var cal = $find("Calendar1");

        //        cal._switchMode("months", true);

        //        if (cal._monthsBody) {
        //            for (var i = 0; i < cal._monthsBody.rows.length; i++) {
        //                var row = cal._monthsBody.rows[i];
        //                for (var j = 0; j < row.cells.length; j++) {
        //                    Sys.UI.DomEvent.addHandler(row.cells[j].firstChild, "click", call);
        //                }
        //            }
        //        }

        sender._switchMode("months", true);

        if (sender._monthsBody) {
            for (var i = 0; i < sender._monthsBody.rows.length; i++) {
                var row = sender._monthsBody.rows[i];
                for (var j = 0; j < row.cells.length; j++) {
                    Sys.UI.DomEvent.addHandler(row.cells[j].firstChild, "click", call);
                }
            }
        }

    }

    function call(sender) {
        var target = sender.target;
        switch (target.mode) {
            case "month":
                var strId = sender.target.id;
                if (strId.indexOf("Calendar1") != -1) {
                    var cal = $find("Calendar1");
                    cal._visibleDate = target.date;
                    cal.set_selectedDate(target.date);
                    ////cal._switchMonth(target.date);
                    cal._blur.post(true);
                    cal.raiseDateSelectionChanged();
                }
                else {
                    var cal2 = $find("Calendar2");
                    cal2._visibleDate = target.date;
                    cal2.set_selectedDate(target.date);
                    ////cal2._switchMonth(target.date);
                    cal2._blur.post(true);
                    cal2.raiseDateSelectionChanged();
                }
                break;
        }
    }
</script>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <table align="center" bgcolor="#EEEEEE">
            <tr align="center" bgcolor="#333399">
                <td>
                    <asp:Label ID="Label23" runat="server" Font-Bold="False" Font-Names="Calibri" Font-Size="Large"
                        ForeColor="White" Text="Mapa de riesgos"></asp:Label>
                </td>
            </tr>
            <tr align="center">
                <td>
                    <table>
                        <tr align="left">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label1" runat="server" Text="Tipo mapa" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                            <td bgcolor="#EEEEEE">
                                <asp:DropDownList ID="DropDownList1" runat="server" Width="200px" Font-Names="Calibri"
                                    Font-Size="Small">
                                    <asp:ListItem Value="---">---</asp:ListItem>
                                    <asp:ListItem Value="1">Riesgo inherente</asp:ListItem>
                                    <asp:ListItem Value="2">Riesgo residual</asp:ListItem>
                                    <asp:ListItem Value="3">Perfil riesgo inherente</asp:ListItem>
                                    <asp:ListItem Value="4">Perfil riesgo residual</asp:ListItem>
                                    <asp:ListItem Value="5">Perfil de Riesgo Residual por Sistema de Riesgo</asp:ListItem>
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
                                    Font-Size="Small" AutoPostBack="true" OnSelectedIndexChanged="DropDownList54_SelectedIndexChanged">
                                    <asp:ListItem Value="---">---</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr align="left">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label44" runat="server" Text="Subproceso" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                            <td bgcolor="#EEEEEE">
                                <asp:DropDownList ID="DropDownList7" runat="server" Width="200px" Font-Names="Calibri"
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
                                    Font-Size="Small" AutoPostBack="True" OnSelectedIndexChanged="DropDownList57_SelectedIndexChanged">
                                    <asp:ListItem Value="---">---</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr align="left">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label159" runat="server" Text="Clasificación particular" Font-Names="Calibri"
                                    Font-Size="Small"></asp:Label>
                            </td>
                            <td bgcolor="#EEEEEE">
                                <asp:DropDownList ID="DropDownList58" runat="server" Width="455px" Font-Names="Calibri"
                                    Font-Size="Small">
                                    <asp:ListItem Value="---">---</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr align="left">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label17" runat="server" Text="Factor de Riesgo LA/FT" Font-Names="Calibri"
                                    Font-Size="Small"></asp:Label>
                            </td>
                            <td bgcolor="#EEEEEE">
                                <asp:DropDownList ID="DropDownList2" runat="server" Width="455px" Font-Names="Calibri"
                                    Font-Size="Small">
                                    <asp:ListItem Value="---">---</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr align="left">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="lblEmpresa" runat="server" Text="Empresa" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                            <td bgcolor="#EEEEEE">
                                <asp:DropDownList ID="ddlEmpresa" runat="server" Width="455px" Font-Names="Calibri"
                                    Font-Size="Small">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr align="left">
                                                                <td bgcolor="#BBBBBB">
                                                                    <asp:Label ID="Label85" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Plan Estratégico:"></asp:Label>
                                                                </td>
                                                                <td bgcolor="#EEEEEE">
                                                                    <asp:DropDownList ID="DDLPlanEstrategico" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                                        Width="400px" AutoPostBack="True" OnSelectedIndexChanged="DDLPlanEstrategico_SelectedIndexChanged">
                                                                        <asp:ListItem Value="---">---</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr align="left">
                                                                <td bgcolor="#BBBBBB">
                                                                    <asp:Label ID="Label185" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Objetivo Estratégico"></asp:Label>
                                                                </td>
                                                                <td bgcolor="#EEEEEE">
                                                                    <asp:DropDownList ID="DDLObjetivoEstrategico" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                                        Width="400px">
                                                                        <asp:ListItem Value="---">---</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                        <tr align="left">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label16" runat="server" Text="Fecha historico" Font-Names="Calibri"
                                    Width="130" Font-Size="Small"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox7" runat="server" Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="true" TargetControlID="TextBox7"
                                    Format="MMMM-yyyy" OnClientShown="onCalendarShown" BehaviorID="Calendar1" OnClientHidden="onCalendarHidden"
                                    DefaultView="Months">
                                </asp:CalendarExtender>
                            </td>
                        </tr>
                        <tr align="left">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label13" runat="server" Text="Areas" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                            <td bgcolor="#EEEEEE">
                                <table runat="server" align="center" id="TblPlaAccion">
                                    <tr>
                                        <td>
                                            <asp:ListBox ID="ListBox1" runat="server" SelectionMode="Multiple" Font-Names="Calibri"
                                                Font-Size="Small"></asp:ListBox>
                                        </td>
                                        <td>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:Button ID="BtnSelectAll" runat="server" Text=">>" Height="20px" Width="30px"
                                                            OnClick="BtnSelectAll_Click" Font-Names="Calibri" Font-Size="Small" ToolTip="Seleccionar todos" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Button ID="BtnSelectOne" runat="server" Text=">" Height="20px" Width="30px"
                                                            OnClick="BtnSelectOne_Click" Font-Names="Calibri" Font-Size="Small" ToolTip="Seleccionar uno" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Button ID="BtnUnSelectAll" runat="server" Text="<<" Height="20px" Width="30px"
                                                            OnClick="BtnUnSelectAll_Click" Font-Names="Calibri" Font-Size="Small" ToolTip="Quitar todos" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Button ID="BtnUnSelectOne" runat="server" Text="<" Height="20px" Width="30px"
                                                            OnClick="BtnUnSelectOne_Click" Font-Names="Calibri" Font-Size="Small" ToolTip="Quitar uno" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td>
                                            <asp:ListBox ID="ListBox2" runat="server" SelectionMode="Multiple" Font-Names="Calibri"
                                                Font-Size="Small" Visible="false"></asp:ListBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr align="center">
                            <td colspan="2">
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
                    </table>
                </td>
            </tr>
            <tr align="center" id="trMapaRiesgos" runat="server" visible="false">
                <td>
                    <table>
                        <tr>
                            <td>
                                <table>
                                    <tr align="center">
                                        <td>
                                            <asp:Label ID="Label24" runat="server" Text="Frecuencia" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Image ID="Image3" runat="server" ImageUrl="~/Imagenes/Aplicacion/probabilidad.png" />
                                        </td>
                                        <td>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:Panel ID="Panel51" runat="server" BorderWidth="1px" BorderColor="#996633" BackColor="Orange"
                                                            Visible="true" Width="60px" Height="60px">
                                                            <table width="100%" height="100%">
                                                                <tr align="center" valign="middle">
                                                                    <td>
                                                                        <asp:LinkButton ID="LBt51" runat="server" Font-Underline="False" ForeColor="Black"
                                                                            Font-Bold="True" Font-Names="Calibri"  CommandArgument="5,1"
                                                                            OnCommand="coordenadaRiesgo"></asp:LinkButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                    <td>
                                                        <asp:Panel ID="Panel52" runat="server" BorderWidth="1px" BorderColor="#996633" BackColor="Orange"
                                                            Visible="true" Width="60px" Height="60px">
                                                            <table width="100%" height="100%">
                                                                <tr align="center" valign="middle">
                                                                    <td>
                                                                        <asp:LinkButton ID="LBt52" runat="server" Font-Underline="false" ForeColor="Black"
                                                                            Font-Bold="true" Font-Names="Calibri"  CommandArgument="5,2"
                                                                            OnCommand="coordenadaRiesgo"></asp:LinkButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                    <td>
                                                        <asp:Panel ID="Panel53" runat="server" BorderWidth="1px" BorderColor="#996633" BackColor="Red"
                                                            Visible="true" Width="60px" Height="60px">
                                                            <table width="100%" height="100%">
                                                                <tr align="center" valign="middle">
                                                                    <td>
                                                                        <asp:LinkButton ID="LBt53" runat="server" Font-Underline="false" ForeColor="Black"
                                                                            Font-Bold="true" Font-Names="Calibri"  CommandArgument="5,3"
                                                                            OnCommand="coordenadaRiesgo"></asp:LinkButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                    <td>
                                                        <asp:Panel ID="Panel54" runat="server" BorderWidth="1px" BorderColor="#996633" BackColor="Red"
                                                            Visible="true" Width="60px" Height="60px">
                                                            <table width="100%" height="100%">
                                                                <tr align="center" valign="middle">
                                                                    <td>
                                                                        <asp:LinkButton ID="LBt54" runat="server" Font-Underline="false" ForeColor="Black"
                                                                            Font-Bold="true" Font-Names="Calibri"  CommandArgument="5,4"
                                                                            OnCommand="coordenadaRiesgo"></asp:LinkButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                    <td>
                                                        <asp:Panel ID="Panel55" runat="server" BorderWidth="1px" BorderColor="#996633" BackColor="Red"
                                                            Visible="true" Width="60px" Height="60px">
                                                            <table width="100%" height="100%">
                                                                <tr align="center" valign="middle">
                                                                    <td>
                                                                        <asp:LinkButton ID="LBt55" runat="server" Font-Underline="false" ForeColor="Black"
                                                                            Font-Bold="true" Font-Names="Calibri"  CommandArgument="5,5"
                                                                            OnCommand="coordenadaRiesgo"></asp:LinkButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Panel ID="Panel41" runat="server" BorderWidth="1px" BorderColor="#996633" BackColor="Yellow"
                                                            Visible="true" Width="60px" Height="60px">
                                                            <table width="100%" height="100%">
                                                                <tr align="center" valign="middle">
                                                                    <td>
                                                                        <asp:LinkButton ID="LBt41" runat="server" Font-Underline="false" ForeColor="Black"
                                                                            Font-Bold="true" Font-Names="Calibri"  CommandArgument="4,1"
                                                                            OnCommand="coordenadaRiesgo"></asp:LinkButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                    <td>
                                                        <asp:Panel ID="Panel42" runat="server" BorderWidth="1px" BorderColor="#996633" BackColor="Orange"
                                                            Visible="true" Width="60px" Height="60px">
                                                            <table width="100%" height="100%">
                                                                <tr align="center" valign="middle">
                                                                    <td>
                                                                        <asp:LinkButton ID="LBt42" runat="server" Font-Underline="false" ForeColor="Black"
                                                                            Font-Bold="true" Font-Names="Calibri"  CommandArgument="4,2"
                                                                            OnCommand="coordenadaRiesgo"></asp:LinkButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                    <td>
                                                        <asp:Panel ID="Panel43" runat="server" BorderWidth="1px" BorderColor="#996633" BackColor="Orange"
                                                            Visible="true" Width="60px" Height="60px">
                                                            <table width="100%" height="100%">
                                                                <tr align="center" valign="middle">
                                                                    <td>
                                                                        <asp:LinkButton ID="LBt43" runat="server" Font-Underline="false" ForeColor="Black"
                                                                            Font-Bold="true" Font-Names="Calibri"  CommandArgument="4,3"
                                                                            OnCommand="coordenadaRiesgo"></asp:LinkButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                    <td>
                                                        <asp:Panel ID="Panel44" runat="server" BorderWidth="1px" BorderColor="#996633" BackColor="Red"
                                                            Visible="true" Width="60px" Height="60px">
                                                            <table width="100%" height="100%">
                                                                <tr align="center" valign="middle">
                                                                    <td>
                                                                        <asp:LinkButton ID="LBt44" runat="server" Font-Underline="false" ForeColor="Black"
                                                                            Font-Bold="true" Font-Names="Calibri"  CommandArgument="4,4"
                                                                            OnCommand="coordenadaRiesgo"></asp:LinkButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                    <td>
                                                        <asp:Panel ID="Panel45" runat="server" BorderWidth="1px" BorderColor="#996633" BackColor="Red"
                                                            Visible="true" Width="60px" Height="60px">
                                                            <table width="100%" height="100%">
                                                                <tr align="center" valign="middle">
                                                                    <td>
                                                                        <asp:LinkButton ID="LBt45" runat="server" Font-Underline="false" ForeColor="Black"
                                                                            Font-Bold="true" Font-Names="Calibri"  CommandArgument="4,5"
                                                                            OnCommand="coordenadaRiesgo"></asp:LinkButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Panel ID="Panel31" runat="server" BorderWidth="1px" BorderColor="#996633" BackColor="Green"
                                                            Visible="true" Width="60px" Height="60px">
                                                            <table width="100%" height="100%">
                                                                <tr align="center" valign="middle">
                                                                    <td>
                                                                        <asp:LinkButton ID="LBt31" runat="server" Font-Underline="false" ForeColor="Black"
                                                                            Font-Bold="true" Font-Names="Calibri"  CommandArgument="3,1"
                                                                            OnCommand="coordenadaRiesgo"></asp:LinkButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                    <td>
                                                        <asp:Panel ID="Panel32" runat="server" BorderWidth="1px" BorderColor="#996633" BackColor="Yellow"
                                                            Visible="true" Width="60px" Height="60px">
                                                            <table width="100%" height="100%">
                                                                <tr align="center" valign="middle">
                                                                    <td>
                                                                        <asp:LinkButton ID="LBt32" runat="server" Font-Underline="false" ForeColor="Black"
                                                                            Font-Bold="true" Font-Names="Calibri"  CommandArgument="3,2"
                                                                            OnCommand="coordenadaRiesgo"></asp:LinkButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                    <td>
                                                        <asp:Panel ID="Panel33" runat="server" BorderWidth="1px" BorderColor="#996633" BackColor="Orange"
                                                            Visible="true" Width="60px" Height="60px">
                                                            <table width="100%" height="100%">
                                                                <tr align="center" valign="middle">
                                                                    <td>
                                                                        <asp:LinkButton ID="LBt33" runat="server" Font-Underline="false" ForeColor="Black"
                                                                            Font-Bold="true" Font-Names="Calibri"  CommandArgument="3,3"
                                                                            OnCommand="coordenadaRiesgo"></asp:LinkButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                    <td>
                                                        <asp:Panel ID="Panel34" runat="server" BorderWidth="1px" BorderColor="#996633" BackColor="Red"
                                                            Visible="true" Width="60px" Height="60px">
                                                            <table width="100%" height="100%">
                                                                <tr align="center" valign="middle">
                                                                    <td>
                                                                        <asp:LinkButton ID="LBt34" runat="server" Font-Underline="false" ForeColor="Black"
                                                                            Font-Bold="true" Font-Names="Calibri"  CommandArgument="3,4"
                                                                            OnCommand="coordenadaRiesgo"></asp:LinkButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                    <td>
                                                        <asp:Panel ID="Panel35" runat="server" BorderWidth="1px" BorderColor="#996633" BackColor="Red"
                                                            Visible="true" Width="60px" Height="60px">
                                                            <table width="100%" height="100%">
                                                                <tr align="center" valign="middle">
                                                                    <td>
                                                                        <asp:LinkButton ID="LBt35" runat="server" Font-Underline="false" ForeColor="Black"
                                                                            Font-Bold="true" Font-Names="Calibri"  CommandArgument="3,5"
                                                                            OnCommand="coordenadaRiesgo"></asp:LinkButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Panel ID="Panel21" runat="server" BorderWidth="1px" BorderColor="#996633" BackColor="Green"
                                                            Visible="true" Width="60px" Height="60px">
                                                            <table width="100%" height="100%">
                                                                <tr align="center" valign="middle">
                                                                    <td>
                                                                        <asp:LinkButton ID="LBt21" runat="server" Font-Underline="false" ForeColor="Black"
                                                                            Font-Bold="true" Font-Names="Calibri"  CommandArgument="2,1"
                                                                            OnCommand="coordenadaRiesgo"></asp:LinkButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                    <td>
                                                        <asp:Panel ID="Panel22" runat="server" BorderWidth="1px" BorderColor="#996633" BackColor="Green"
                                                            Visible="true" Width="60px" Height="60px">
                                                            <table width="100%" height="100%">
                                                                <tr align="center" valign="middle">
                                                                    <td>
                                                                        <asp:LinkButton ID="LBt22" runat="server" Font-Underline="false" ForeColor="Black"
                                                                            Font-Bold="true" Font-Names="Calibri"  CommandArgument="2,2"
                                                                            OnCommand="coordenadaRiesgo"></asp:LinkButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                    <td>
                                                        <asp:Panel ID="Panel23" runat="server" BorderWidth="1px" BorderColor="#996633" BackColor="Yellow"
                                                            Visible="true" Width="60px" Height="60px">
                                                            <table width="100%" height="100%">
                                                                <tr align="center" valign="middle">
                                                                    <td>
                                                                        <asp:LinkButton ID="LBt23" runat="server" Font-Underline="false" ForeColor="Black"
                                                                            Font-Bold="true" Font-Names="Calibri"  CommandArgument="2,3"
                                                                            OnCommand="coordenadaRiesgo"></asp:LinkButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                    <td>
                                                        <asp:Panel ID="Panel24" runat="server" BorderWidth="1px" BorderColor="#996633" BackColor="Orange"
                                                            Visible="true" Width="60px" Height="60px">
                                                            <table width="100%" height="100%">
                                                                <tr align="center" valign="middle">
                                                                    <td>
                                                                        <asp:LinkButton ID="LBt24" runat="server" Font-Underline="false" ForeColor="Black"
                                                                            Font-Bold="true" Font-Names="Calibri"  CommandArgument="2,4"
                                                                            OnCommand="coordenadaRiesgo"></asp:LinkButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                    <td>
                                                        <asp:Panel ID="Panel25" runat="server" BorderWidth="1px" BorderColor="#996633" BackColor="Red"
                                                            Visible="true" Width="60px" Height="60px">
                                                            <table width="100%" height="100%">
                                                                <tr align="center" valign="middle">
                                                                    <td>
                                                                        <asp:LinkButton ID="LBt25" runat="server" Font-Underline="false" ForeColor="Black"
                                                                            Font-Bold="true" Font-Names="Calibri"  CommandArgument="2,5"
                                                                            OnCommand="coordenadaRiesgo"></asp:LinkButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Panel ID="Panel11" runat="server" BorderWidth="1px" BorderColor="#996633" BackColor="Green"
                                                            Visible="true" Width="60px" Height="60px">
                                                            <table width="100%" height="100%">
                                                                <tr align="center" valign="middle">
                                                                    <td>
                                                                        <asp:LinkButton ID="LBt11" runat="server" Font-Underline="false" ForeColor="Black"
                                                                            Font-Bold="true" Font-Names="Calibri"  CommandArgument="1,1"
                                                                            OnCommand="coordenadaRiesgo"></asp:LinkButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                    <td>
                                                        <asp:Panel ID="Panel12" runat="server" BorderWidth="1px" BorderColor="#996633" BackColor="Green"
                                                            Visible="true" Width="60px" Height="60px">
                                                            <table width="100%" height="100%">
                                                                <tr align="center" valign="middle">
                                                                    <td>
                                                                        <asp:LinkButton ID="LBt12" runat="server" Font-Underline="false" ForeColor="Black"
                                                                            Font-Bold="true" Font-Names="Calibri"  CommandArgument="1,2"
                                                                            OnCommand="coordenadaRiesgo"></asp:LinkButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                    <td>
                                                        <asp:Panel ID="Panel13" runat="server" BorderWidth="1px" BorderColor="#996633" BackColor="Yellow"
                                                            Visible="true" Width="60px" Height="60px">
                                                            <table width="100%" height="100%">
                                                                <tr align="center" valign="middle">
                                                                    <td>
                                                                        <asp:LinkButton ID="LBt13" runat="server" Font-Underline="false" ForeColor="Black"
                                                                            Font-Bold="true" Font-Names="Calibri"  CommandArgument="1,3"
                                                                            OnCommand="coordenadaRiesgo"></asp:LinkButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                    <td>
                                                        <asp:Panel ID="Panel14" runat="server" BorderWidth="1px" BorderColor="#996633" BackColor="Orange"
                                                            Visible="true" Width="60px" Height="60px">
                                                            <table width="100%" height="100%">
                                                                <tr align="center" valign="middle">
                                                                    <td>
                                                                        <asp:LinkButton ID="LBt14" runat="server" Font-Underline="false" ForeColor="Black"
                                                                            Font-Bold="true" Font-Names="Calibri"  CommandArgument="1,4"
                                                                            OnCommand="coordenadaRiesgo"></asp:LinkButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                    <td>
                                                        <asp:Panel ID="Panel15" runat="server" BorderWidth="1px" BorderColor="#996633" BackColor="Orange"
                                                            Visible="true" Width="60px" Height="60px">
                                                            <table width="100%" height="100%">
                                                                <tr align="center" valign="middle">
                                                                    <td>
                                                                        <asp:LinkButton ID="LBt15" runat="server" Font-Underline="false" ForeColor="Black"
                                                                            Font-Bold="true" Font-Names="Calibri"  CommandArgument="1,5"
                                                                            OnCommand="coordenadaRiesgo"></asp:LinkButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr align="right">
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:Image ID="Image2" runat="server" ImageUrl="~/Imagenes/Aplicacion/impacto.png" />
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr align="center">
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label7" runat="server" Text="Impacto" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr align="center" id="trRiesgovsSistemaTitulo" runat="server" visible="false">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label18" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Riesgos"></asp:Label>
                            </td>
                        </tr>
            <tr align="center" id="trRiesgovsSistema" runat="server" visible="false">
                <td align="center">
                    <asp:GridView ID="GVriesgovsSistema" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                    ForeColor="#333333" GridLines="Vertical" ShowHeaderWhenEmpty="True" HeaderStyle-CssClass="gridViewHeader"
                                    BorderStyle="Solid" HorizontalAlign="Center" Font-Names="Calibri" Font-Size="Small"
                                     >
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:BoundField HeaderText="Código" DataField="IdClasificacionRiesgo" />
                                        <asp:BoundField HeaderText="Nombre" DataField="NombreClasificacionRiesgo" />
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
            <tr align="center" id="trRiesgos" runat="server" visible="false">
                <td>
                    <table width="100%">
                        <tr align="center">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label2" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Riesgos"></asp:Label>
                            </td>
                        </tr>
                        <tr align="center">
                            <td>
                                <table>
                                    <tr>
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label15" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Severidad:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Panel ID="Panel1" runat="server" Width="80px" Height="50px">
                                                <table style="width: 100%; height: 100%">
                                                    <tr align="center">
                                                        <td>
                                                            <asp:Label ID="Label14" runat="server" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label5" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Frecuencia:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label6" runat="server" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                        </td>
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label3" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Impacto:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label4" runat="server" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr align="center">
                            <td>
                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                    ForeColor="#333333" GridLines="Vertical" ShowHeaderWhenEmpty="True" HeaderStyle-CssClass="gridViewHeader"
                                    BorderStyle="Solid" HorizontalAlign="Center" Font-Names="Calibri" Font-Size="Small"
                                    OnRowCommand="GridView1_RowCommand" AllowPaging="True" OnPageIndexChanging="GridView1_PageIndexChanging">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:BoundField HeaderText="Código" DataField="Codigo" />
                                        <asp:BoundField HeaderText="Nombre" DataField="Nombre" />
                                        <asp:BoundField HeaderText="Riesgo global" DataField="NombreClasificacionRiesgo" />
                                        <asp:BoundField HeaderText="Fecha Registro" DataField="FechaRegistro" />
                                        <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Icons/select.png" Text="Ver detalle"
                                            CommandName="Ver" />
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
                        <tr align="center" id="trDetalleRiesgo" runat="server" visible="false">
                            <td>
                                <table width="100%">
                                    <tr align="center">
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label8" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Detalle riesgo"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr align="center">
                                        <td>
                                            <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Font-Names="Calibri"
                                                Font-Size="Small" Width="500px">
                                                <asp:TabPanel ID="TabPanel1" runat="server" HeaderText="Controles" Font-Names="Calibri"
                                                    Font-Size="Small">
                                                    <ContentTemplate>
                                                        <table>
                                                            <tr align="center">
                                                                <td bgcolor="#BBBBBB">
                                                                    <asp:Label ID="Label9" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Controles Relacionados"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr align="center">
                                                                <td bgcolor="#BBBBBB">
                                                                    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" BorderStyle="Solid"
                                                                        CellPadding="4" Font-Names="Calibri" Font-Size="Small" ForeColor="#333333" GridLines="Vertical"
                                                                        HorizontalAlign="Center" ShowHeaderWhenEmpty="True">
                                                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                                        <Columns>
                                                                            <asp:BoundField DataField="CodigoControl" HeaderText="Código"></asp:BoundField>
                                                                            <asp:BoundField DataField="NombreControl" HeaderText="Nombre"></asp:BoundField>
                                                                            <asp:BoundField DataField="NombreTest" HeaderText="Test"></asp:BoundField>
                                                                        </Columns>
                                                                        <EditRowStyle BackColor="#999999" />
                                                                        <FooterStyle BackColor="White" Font-Bold="True" ForeColor="White" />
                                                                        <HeaderStyle BackColor="#5D7B9D" CssClass="gridViewHeader" Font-Bold="True" ForeColor="White" />
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
                                                    </ContentTemplate>
                                                </asp:TabPanel>
                                                <asp:TabPanel ID="TabPanel2" runat="server" HeaderText="Objetivos" Font-Names="Calibri"
                                                    Font-Size="Small">
                                                    <ContentTemplate>
                                                        <table>
                                                            <tr align="center">
                                                                <td bgcolor="#BBBBBB">
                                                                    <asp:Label ID="Label10" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Objetivos Relacionados"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr align="center">
                                                                <td bgcolor="#BBBBBB">
                                                                    <asp:GridView ID="GridView7" runat="server" AutoGenerateColumns="False" BorderStyle="Solid"
                                                                        CellPadding="4" Font-Names="Calibri" Font-Size="Small" ForeColor="#333333" GridLines="Vertical"
                                                                        HorizontalAlign="Center" ShowHeaderWhenEmpty="True">
                                                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                                        <Columns>
                                                                            <asp:BoundField HeaderText="Nombre del Objetivo" DataField="NombreObjetivos" />
                                                                        </Columns>
                                                                        <EditRowStyle BackColor="#999999" />
                                                                        <FooterStyle BackColor="White" Font-Bold="True" ForeColor="White" />
                                                                        <HeaderStyle BackColor="#5D7B9D" CssClass="gridViewHeader" Font-Bold="True" ForeColor="White" />
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
                                                    </ContentTemplate>
                                                </asp:TabPanel>
                                                <asp:TabPanel ID="TabPanel3" runat="server" HeaderText="Planes de Acción" Font-Names="Calibri"
                                                    Font-Size="Small">
                                                    <ContentTemplate>
                                                        <table>
                                                            <tr align="center">
                                                                <td bgcolor="#BBBBBB">
                                                                    <asp:Label ID="Label11" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Planes de Acción Relacionados"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr align="center">
                                                                <td bgcolor="#BBBBBB">
                                                                    <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" BorderStyle="Solid"
                                                                        CellPadding="4" Font-Names="Calibri" Font-Size="Small" ForeColor="#333333" GridLines="Vertical"
                                                                        HorizontalAlign="Center" ShowHeaderWhenEmpty="True">
                                                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                                        <Columns>
                                                                            <asp:BoundField HeaderText="Estado" DataField="NombreEstadoPlanAccion"></asp:BoundField>
                                                                            <asp:BoundField HeaderText="Fecha Compromiso" DataField="FechaCompromiso"></asp:BoundField>
                                                                            <asp:BoundField HeaderText="Descripción" DataField="DescripcionAccion"></asp:BoundField>
                                                                            <asp:BoundField HeaderText="Responsable" DataField="NombreHijo"></asp:BoundField>
                                                                        </Columns>
                                                                        <EditRowStyle BackColor="#999999" />
                                                                        <FooterStyle BackColor="White" Font-Bold="True" ForeColor="White" />
                                                                        <HeaderStyle BackColor="#5D7B9D" CssClass="gridViewHeader" Font-Bold="True" ForeColor="White" />
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
                                                    </ContentTemplate>
                                                </asp:TabPanel>
                                                <asp:TabPanel ID="TabPanel4" runat="server" HeaderText="Responsables" Font-Names="Calibri"
                                                    Font-Size="Small" Visible="false">
                                                    <ContentTemplate>
                                                        <table>
                                                            <tr align="center">
                                                                <td bgcolor="#BBBBBB">
                                                                    <asp:Label ID="Label12" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Responsables Relacionados"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr align="center">
                                                                <td bgcolor="#BBBBBB">
                                                                    <asp:GridView ID="GridView8" runat="server" AutoGenerateColumns="False" BorderStyle="Solid"
                                                                        CellPadding="4" Font-Names="Calibri" Font-Size="Small" ForeColor="#333333" GridLines="Vertical"
                                                                        HorizontalAlign="Center" ShowHeaderWhenEmpty="True">
                                                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                                        <Columns>
                                                                            <asp:BoundField DataField="Responsable" HeaderText="Responsable"></asp:BoundField>
                                                                        </Columns>
                                                                        <EditRowStyle BackColor="#999999" />
                                                                        <FooterStyle BackColor="White" Font-Bold="True" ForeColor="White" />
                                                                        <HeaderStyle BackColor="#5D7B9D" CssClass="gridViewHeader" Font-Bold="True" ForeColor="White" />
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
                                                    </ContentTemplate>
                                                </asp:TabPanel>
                                            </asp:TabContainer>
                                        </td>
                                    </tr>
                                    <tr align="center">
                                        <td>
                                            <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                                ToolTip="Cancelar" OnClick="ImageButton2_Click" />
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
</asp:UpdatePanel>
