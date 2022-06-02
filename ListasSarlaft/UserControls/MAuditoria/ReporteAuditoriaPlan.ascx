<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ReporteAuditoriaPlan.ascx.cs" Inherits="ListasSarlaft.UserControls.MAuditoria.ReporteAuditoriaPlan" %>
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
                <LocalReport ReportPath="Reportes\ReporteAuditoriaPlan.rdlc">
                    <DataSources>
                        <rsweb:ReportDataSource DataSourceId="odsHallazgos" Name="DSHallazgos" />
                        <rsweb:ReportDataSource Name="DSRecomendacionesHallazgo" DataSourceId="odsRecomendaciones"></rsweb:ReportDataSource>
                        <rsweb:ReportDataSource Name="DSRiesgosHallazgo" DataSourceId="odsRiesgos"></rsweb:ReportDataSource>
                        <rsweb:ReportDataSource DataSourceId="odsConclusion" Name="DSConclusion" />
                    </DataSources>
                    <DataSources>
                        <rsweb:ReportDataSource DataSourceId="odsRecomendaciones" Name="DSRecomendacionesHallazgo" />
                    </DataSources>
                    <DataSources>
                        <rsweb:ReportDataSource DataSourceId="odsRiesgos" Name="DSRiesgosHallazgo" />
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
                TypeName="ListasSarlaft.DataSet.DSHallazgosTableAdapters.SP_HallazgosAuditoriaTableAdapter"
                OldValuesParameterFormatString="original_{0}">
                <SelectParameters>
                    <asp:ControlParameter ControlID="txtCodAuditoria" Name="IdAuditoria" PropertyName="Text"
                        Type="Int32" />
                </SelectParameters>
            </asp:ObjectDataSource>

            <asp:ObjectDataSource ID="odsRecomendaciones" runat="server" SelectMethod="GetData"
                TypeName="ListasSarlaft.DataSet.DSRecomendacionesHallazgoTableAdapters.SP_RecomendacionesHallazgoTableAdapter"
                OldValuesParameterFormatString="original_{0}"></asp:ObjectDataSource>

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
    <tr>
        <td>
            <asp:TextBox ID="TextBox1" runat="server" Visible="false" Width="10px"></asp:TextBox>
            <asp:TextBox ID="TextBox2" runat="server" Visible="false" Width="10px"></asp:TextBox>
            <asp:TextBox ID="TextBox3" runat="server" Visible="false" Width="10px"></asp:TextBox>
            <asp:TextBox ID="TextBox4" runat="server" Visible="false" Width="10px"></asp:TextBox>
            <asp:TextBox ID="TextBox5" runat="server" Visible="false" Width="10px"></asp:TextBox>
            <asp:TextBox ID="TextBox6" runat="server" Visible="false" Width="10px"></asp:TextBox>
            <asp:TextBox ID="TextBox7" runat="server" Visible="false" Width="10px"></asp:TextBox>
            <asp:TextBox ID="TextBox8" runat="server" Visible="false" Width="10px"></asp:TextBox>
            <asp:TextBox ID="TextBox9" runat="server" Visible="false" Width="10px"></asp:TextBox>
            <asp:TextBox ID="TextBox10" runat="server" Visible="false" Width="10px"></asp:TextBox>
            <asp:TextBox ID="TextBox11" runat="server" Visible="false" Width="10px"></asp:TextBox>
            <asp:TextBox ID="TextBox12" runat="server" Visible="false" Width="10px"></asp:TextBox>
        </td>
    </tr>
</table>
