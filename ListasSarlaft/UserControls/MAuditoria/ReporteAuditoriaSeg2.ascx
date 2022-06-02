<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ReporteAuditoriaSeg2.ascx.cs" Inherits="ListasSarlaft.UserControls.MAuditoria.ReporteAuditoriaSeg2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
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
                        <LocalReport ReportPath="Reportes\ReporteHallazgosAuditoria.rdlc">
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
                    </rsweb:reportviewer>
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
                OldValuesParameterFormatString="original_{0}">
            </asp:ObjectDataSource>
            
            <asp:ObjectDataSource ID="odsRiesgos" runat="server" SelectMethod="GetData"
                TypeName="ListasSarlaft.DataSet.DSRiesgosHallazgoTableAdapters.SP_RiesgosHallazgoTableAdapter"
                OldValuesParameterFormatString="{0}">
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
            <asp:TextBox ID="txtCodAuditoria" runat="server" Visible="False"></asp:TextBox>
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