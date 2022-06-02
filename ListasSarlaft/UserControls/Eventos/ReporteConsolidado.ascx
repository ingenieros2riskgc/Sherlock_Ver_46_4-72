<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ReporteConsolidado.ascx.cs"
    Inherits="ListasSarlaft.UserControls.Eventos.ReporteConsolidado" %>
<%--<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>--%>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=14.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControls/Sitio/OkMessageBox.ascx" TagPrefix="uc" TagName="OkMessageBox" %>
<style type="text/css">
    .gridViewHeader a:link
    {
        text-decoration: none;
    }

    .style1
    {
        width: 100%;
    }
</style>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <table align="center" bgcolor="#EEEEEE">
            <tr>
                <td>
                    <asp:Label ID="Label140" runat="server" Font-Bold="False" Font-Names="Calibri" Font-Size="Large"
                        ForeColor="White" Text="Reportes Eventos Consolidado"></asp:Label>
                </td>
            </tr>
            <tr align="left">
                <td>
                    <table>
                        <tr align="left" id="Tr1" runat="server">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label5" runat="server" Text="Fecha Inicio" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                            <td bgcolor="#EEEEEE">
                                <asp:TextBox ID="TextBox1" runat="server" Width="195px" Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender4" runat="server" Enabled="True" TargetControlID="TextBox1"
                                    Format="yyyy-MM-dd"></asp:CalendarExtender>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator28" runat="server" ControlToValidate="TextBox1"
                                    ForeColor="Red" ValidationGroup="Addne">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr align="left" id="Tr2" runat="server">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label6" runat="server" Text="Fecha Fin" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                            <td bgcolor="#EEEEEE">
                                <asp:TextBox ID="TextBox2" runat="server" Width="195px" Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" TargetControlID="TextBox2"
                                    Format="yyyy-MM-dd"></asp:CalendarExtender>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBox1"
                                    ForeColor="Red" ValidationGroup="Addne">*</asp:RequiredFieldValidator>
                                <asp:Button ID="Button1" runat="server" Text="Ver..." Font-Names="Calibri" Font-Size="Small"
                                    OnClick="Button1_Click" ValidationGroup="Addne" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr id="RptEventos" runat="server" visible="false">
                <td>
                    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Calibri" Font-Size="Small"
                        InteractiveDeviceInfos="(Colección)" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt"
                        Width="800px" Height="600px">
                        <LocalReport ReportPath="Reportes\RptEventos.rdlc">
                            <DataSources>
                                <rsweb:ReportDataSource DataSourceId="SqlDataSource1" Name="DataSet2" />
                            </DataSources>
                        </LocalReport>
                    </rsweb:ReportViewer>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
                        SelectCommand="SP_Eventos" SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="TextBox1" Name="FechaInicial" PropertyName="Text"
                                Type="String" />
                            <asp:ControlParameter ControlID="TextBox2" Name="FechaFinal" PropertyName="Text"
                                Type="String" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </td>
            </tr>
        </table>
    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="ReportViewer1" />
        <asp:PostBackTrigger ControlID="Button1" />
    </Triggers>
</asp:UpdatePanel>
