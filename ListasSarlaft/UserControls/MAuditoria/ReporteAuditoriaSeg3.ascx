<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ReporteAuditoriaSeg3.ascx.cs" Inherits="ListasSarlaft.UserControls.MAuditoria.ReporteAuditoriaSeg3" %>
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
            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Calibri" Font-Size="Small"
                Width="21.59cm" interactivedeviceinfos="(Colección)" WaitMessageFont-Names="Calibri"
                WaitMessageFont-Size="Small" Height="27.94cm" SizeToReportContent="True">
                <LocalReport ReportPath="Reportes\ReporteHallazgosAuditoria.rdlc">
                    <DataSources>
                        <rsweb:ReportDataSource DataSourceId="odsRiesgos" Name="DSRiesgosHallazgo" />
                        <rsweb:ReportDataSource DataSourceId="odsConclusion" Name="DSConclusion" />
                        <rsweb:ReportDataSource DataSourceId="odsHallazgos" Name="DSHallazgosSeguimientoAuditoria" />
                    </DataSources>
                </LocalReport>
            </rsweb:ReportViewer>

            <asp:ObjectDataSource ID="odsConclusion" runat="server" SelectMethod="GetData"
                TypeName="ListasSarlaft.DataSet.DSConclusionTableAdapters.SP_ConclusionAuditoriaTableAdapter"
                OldValuesParameterFormatString="original_{0}">
                <SelectParameters>
                    <asp:ControlParameter ControlID="txtCodAuditoria" Name="IdAuditoria" PropertyName="Text"
                        Type="Int32" />
                </SelectParameters>
            </asp:ObjectDataSource>

            <asp:ObjectDataSource ID="odsHallazgos" runat="server" SelectMethod="GetData"
                TypeName="ListasSarlaft.DataSet.DSHallazgosSeguimientoAuditoriaTableAdapters.SP_HallazgosSeguimientoAuditoriaTableAdapter"
                OldValuesParameterFormatString="original_{0}">
                <SelectParameters>
                    <asp:ControlParameter ControlID="txtCodAuditoria" Name="IdAuditoria" PropertyName="Text"
                        Type="Int32" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="odsRecomendaciones" runat="server" SelectMethod="GetData"
                TypeName="ListasSarlaft.DataSet.DSRecomendacionesHallazgoTableAdapters.SP_RecomendacionesHallazgoTableAdapter"
                OldValuesParameterFormatString="{0}"></asp:ObjectDataSource>
            <asp:ObjectDataSource ID="odsRiesgos" runat="server" SelectMethod="GetData"
                TypeName="ListasSarlaft.DataSet.DSRiesgosHallazgoTableAdapters.SP_RiesgosHallazgoTableAdapter"
                OldValuesParameterFormatString="{0}"></asp:ObjectDataSource>

        </td>

        <td>&nbsp;
        </td>
        <td>&nbsp;
        </td>
    </tr>
    <tr>
        <td>&nbsp;</td>
        <td>&nbsp;
        </td>
        <td>&nbsp;
        </td>
    </tr>
    <tr>
        <td>
            <asp:TextBox ID="txtCodAuditoria" runat="server" Visible="False"></asp:TextBox>
        </td>
        <td>&nbsp;
        </td>
        <td>&nbsp;
        </td>
    </tr>
</table>
