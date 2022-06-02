<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RpteConsolidadoSarlaft.ascx.cs"
    Inherits="ListasSarlaft.UserControls.RpteConsolidadoSarlaft" %>
<%--<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>--%>
<%--<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>--%>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=14.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
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
<asp:SqlDataSource ID="SqlDataSourceConsolidado" runat="server" ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
    SelectCommand="SP_SarlaftConsolidado" SelectCommandType="StoredProcedure">
    <SelectParameters>
        <asp:ControlParameter ControlID="Label8" Name="FechaIni" PropertyName="Text" Type="String"
            DefaultValue="2013-01-01 00:00:00:000" />
        <asp:ControlParameter ControlID="Label9" Name="FechaFin" PropertyName="Text" Type="String"
            DefaultValue="2013-12-31 23:59:59:998" />
    </SelectParameters>
</asp:SqlDataSource>
<script type="text/javascript">
    function ChangeCalendarView(sender, args) {
        sender._switchMode("months", true);
    }

    function onCalendarHidden(sender) {
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
                    cal._blur.post(true);
                    cal.raiseDateSelectionChanged();
                }
                else {
                    var cal2 = $find("Calendar2");
                    cal2._visibleDate = target.date;
                    cal2.set_selectedDate(target.date);
                    cal2._blur.post(true);
                    cal2.raiseDateSelectionChanged();
                }
                break;
        }
    }
</script>
<script type="text/javascript">
    function popUp(pagina) {
        hiddden = open(pagina, 'NewWindow', 'top=0,left=0,width=1280,height=1024,status=yes,resizable=yes,scrollbars=yes');
    }
</script>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <table align="center">
            <tr align="center" bgcolor="#333399">
                <td>
                    <asp:Label ID="Label61" runat="server" ForeColor="White" Text="Reporte Consolidado Operaciones Inusuales"
                        Font-Bold="False" Font-Names="Calibri" Font-Size="Large"></asp:Label>
                </td>
            </tr>
            <tr>
            <td>
            <br />
            </td>
            </tr>
            <tr align="left" runat="server" id="TbFechas">
                <td>
                    <table align="center">
                        <tr>
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label16" runat="server" Text="Fecha desde" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox7" runat="server" Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="true" TargetControlID="TextBox7"
                                    Format="MMMM-yyyy" OnClientShown="onCalendarShown" BehaviorID="Calendar1" OnClientHidden="onCalendarHidden"
                                    DefaultView="Months"></asp:CalendarExtender>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBox7"
                                    InitialValue="" ValidationGroup="VerRpte" Font-Bold="true" ForeColor="Red">*</asp:RequiredFieldValidator>
                            </td>
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label17" runat="server" Text="Fecha hasta" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox10" runat="server" Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="true" TargetControlID="TextBox10"
                                    Format="MMMM-yyyy" OnClientShown="onCalendarShown" BehaviorID="Calendar2" OnClientHidden="onCalendarHidden"
                                    DefaultView="Months"></asp:CalendarExtender>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBox10"
                                    InitialValue="" ValidationGroup="VerRpte" Font-Bold="true" ForeColor="Red">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <asp:Label ID="Label8" runat="server" Visible="false"></asp:Label>
                                <asp:Label ID="Label9" runat="server" Visible="false"></asp:Label>
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
                                    ToolTip="Consultar" ValidationGroup="VerRpte" OnClick="ImageButton1_Click"
                                    Style="width: 32px" />
                            </td>
                            <td>
                                <asp:ImageButton ID="ImageButton5" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                    ToolTip="Cancelar" OnClick="ImageButton5_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt"
                        InteractiveDeviceInfos="(Colección)" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt"
                        Width="900px">
                        <LocalReport ReportPath="Reportes\SarlaftConsolidado.rdlc">
                            <DataSources>
                                <rsweb:ReportDataSource DataSourceId="SqlDataSourceConsolidado" Name="DS_Consolidado" />
                            </DataSources>
                        </LocalReport>
                    </rsweb:ReportViewer>
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
    </Triggers>
</asp:UpdatePanel>
