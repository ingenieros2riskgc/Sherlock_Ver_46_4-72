<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ReporteAuditoriaAnulada.ascx.cs" Inherits="ListasSarlaft.UserControls.MAuditoria.ReporteAuditoriaAnulada" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%--<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>--%>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=14.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<%--<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>--%>
<style type="text/css">
    .style1
    {
        width: 100%;
    }
</style>
<table class="style1" width="100%">
    <tr align="center">
        <td>
            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt"
                Width="21.59cm" interactivedeviceinfos="(Colección)" WaitMessageFont-Names="Verdana"
                WaitMessageFont-Size="14pt" Height="27.94cm" SizeToReportContent="True">
                <LocalReport ReportPath="Reportes\ReporteAuditoriaAnulada.rdlc">

                    <DataSources>
                        <rsweb:ReportDataSource DataSourceId="SqlDataSource1" Name="DataSet1" />
                    </DataSources>

                </LocalReport>
            </rsweb:ReportViewer>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server"
                ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
                SelectCommand="SP_AuditoriaAnulada" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
</table>

