<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ReportePlanAuditoria.ascx.cs" Inherits="ListasSarlaft.UserControls.MAuditoria.ReportePlanAuditoria" %>
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
            <rsweb:reportviewer id="ReportViewer1" runat="server" font-names="Verdana" font-size="8pt"
                width="21.59cm" interactivedeviceinfos="(Colección)" waitmessagefont-names="Verdana"
                waitmessagefont-size="14pt" height="27.94cm" sizetoreportcontent="True">
                        <LocalReport ReportPath="Reportes\ReportePlanAuditoria.rdlc">
                            <DataSources>
                                <rsweb:ReportDataSource DataSourceId="odsAuditoria" Name="DSPlanAuditoria" />
                            </DataSources>
                        </LocalReport> 
                    </rsweb:reportviewer>
            <asp:ObjectDataSource ID="odsAuditoria" runat="server" SelectMethod="GetData"
                TypeName="ListasSarlaft.DataSet.DSPlanAuditoriaTableAdapters.Sp_PlanAuditoriaTableAdapter"
                OldValuesParameterFormatString="original_{0}">
                <SelectParameters>
                    <asp:ControlParameter ControlID="txtCodPlaneacion" Name="IdPlaneacion" PropertyName="Text"
                        Type="Int32" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </td>
        <td>
            &nbsp;
        </td>
        <td>
            &nbsp;
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;
        </td>
        <td>
            &nbsp;
        </td>
    </tr>
    <tr>
        <td>
            <asp:TextBox ID="txtCodPlaneacion" runat="server" 
               Visible="False"></asp:TextBox>
        </td>
        <td>
            &nbsp;
        </td>
        <td>
            &nbsp;
        </td>
    </tr>
</table>

