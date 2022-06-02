<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PruebadeReporte.aspx.cs" Inherits="ListasSarlaft.Formularios.Auditoria.Admin.PruebadeReporte" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table class="style1">
            <tr>
                <td>
                    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" 
                        Font-Size="8pt" Width="21.59cm"
                        InteractiveDeviceInfos="(Colección)" WaitMessageFont-Names="Verdana" 
                        WaitMessageFont-Size="14pt" Height="27.94cm" SizeToReportContent="True">
                        <LocalReport ReportPath="Reportes\Report2.rdlc">
                            <DataSources>
                                <rsweb:ReportDataSource DataSourceId="odsRecomendaciones" Name="DataSet2" />
                            </DataSources>
                        </LocalReport>
                    </rsweb:ReportViewer>
                    <asp:ObjectDataSource ID="odsRecomendaciones" runat="server" 
                        SelectMethod="GetData" 
                        TypeName="ListasSarlaft.DataSet.DataSet2TableAdapters.Sp_RecomendacionesTableAdapter" 
                        OldValuesParameterFormatString="original_{0}">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="TextBox1" Name="IdAuditoria" PropertyName="Text"
                                Type="Int32" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="Button1" runat="server" Text="Button" onclick="Button1_Click" />
                </td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                </td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
    
    </div>
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
    </form>
</body>
</html>
