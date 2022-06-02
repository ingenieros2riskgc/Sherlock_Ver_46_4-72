<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ReporteHallazgoVsPlanAccion.ascx.cs"
    Inherits="ListasSarlaft.UserControls.MAuditoria.ReporteHallazgoVsPlanAccion" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%--<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>--%>
<%--<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>--%>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=14.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
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

    .scrollingControlContainer
    {
        overflow-x: hidden;
        overflow-y: scroll;
    }

    .scrollingCheckBoxList
    {
        border: 1px #808080 solid;
        margin: 10px 10px 10px 10px;
        height: 200px;
    }

    .popup
    {
        border: Silver 1px solid;
        color: #060F40;
        background: #ffffff;
    }

    .style1
    {
        width: 815px;
    }
</style>
<asp:SqlDataSource ID="SqlDataSource10" runat="server" ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
    SelectCommand="SELECT [IdMacroProceso], [Nombre] FROM [Procesos].[Macroproceso]"></asp:SqlDataSource>
<asp:SqlDataSource ID="SqlDataSource11" runat="server" ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
    SelectCommand="SELECT [IdProceso], [Nombre] FROM [Procesos].[Proceso] WHERE [IdMacroProceso] = @IdMacroProceso">
    <SelectParameters>
        <asp:ControlParameter ControlID="ddlMacroProceso" Name="IdMacroProceso" PropertyName="SelectedValue"
            Type="Int32" />
    </SelectParameters>
</asp:SqlDataSource>
<asp:SqlDataSource ID="SqlDataSource24" runat="server" ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
    SelectCommand="SELECT PDT.IdDetalleTipo, PDT.NombreDetalle FROM [Parametrizacion].[Tipos] PT 
    INNER JOIN [Parametrizacion].[DetalleTipos] PDT ON PT.IdTipo = PDT.IdTipo WHERE PT.NombreTipo = 'Nivel de Riesgo' ORDER BY NombreDetalle"></asp:SqlDataSource>
<asp:SqlDataSource ID="SqlDataSource25" runat="server" ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
    SelectCommand="SELECT PDT.IdDetalleTipo, PDT.NombreDetalle FROM [Parametrizacion].[Tipos] PT 
    INNER JOIN [Parametrizacion].[DetalleTipos] PDT ON PT.IdTipo = PDT.IdTipo WHERE PT.NombreTipo = 'Estado del Hallazgo' ORDER BY NombreDetalle"></asp:SqlDataSource>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <asp:Panel ID="pnlDependencia" runat="server" CssClass="popup" Width="400px" Style="display: none">
            <table width="100%" class="tablaSinBordes">
                <tr align="right" bgcolor="#5D7B9D">
                    <td>
                        <asp:ImageButton ID="ImageButton7" runat="server" ImageUrl="~/Imagenes/Icons/dialog-close2.png"
                            OnClientClick="$find('popup').hidePopup(); return false;" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:TreeView ID="TreeView1" ExpandDepth="3" runat="server" Font-Names="Calibri"
                            Style="overflow: auto" Font-Size="Small" LineImagesFolder="~/TreeLineImages"
                            ForeColor="Black" ShowLines="True" Target="_self" OnSelectedNodeChanged="TreeView1_SelectedNodeChanged"
                            Font-Bold="False" Height="400px">
                            <SelectedNodeStyle BackColor="Silver" BorderColor="#66CCFF" BorderStyle="Inset" />
                        </asp:TreeView>
                    </td>
                </tr>
                <tr align="center">
                    <td>
                        <asp:Button ID="BtnOk" runat="server" Text="Aceptar" CssClass="Apariencia" OnClientClick="$find('popup').hidePopup(); return false;" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="pnlProceso" runat="server" CssClass="popup" Width="350px" Style="display: none">
            <table width="100%" class="tabla">
                <tr align="right" bgcolor="#5D7B9D">
                    <td colspan="2">
                        <asp:ImageButton ID="ImageButton9" runat="server" ImageUrl="~/Imagenes/Icons/dialog-close2.png"
                            OnClientClick="$find('popupProceso').hidePopup(); return false;" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#BBBBBB">
                        <asp:Label ID="Label7" runat="server" Text="Macroproceso:" CssClass="Apariencia"></asp:Label>
                    </td>
                    <td align="center" bgcolor="#EEEEEE">
                        <asp:DropDownList ID="ddlMacroProceso" runat="server" Width="250px" CssClass="Apariencia"
                            OnDataBound="ddlMacroProceso_DataBound" DataSourceID="SqlDataSource10" DataTextField="Nombre"
                            DataValueField="IdMacroProceso" AutoPostBack="True" OnSelectedIndexChanged="ddlMacroProceso_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#BBBBBB">
                        <asp:Label ID="Label25" runat="server" Text="Proceso:" CssClass="Apariencia"></asp:Label>
                    </td>
                    <td align="center" bgcolor="#EEEEEE">
                        <asp:DropDownList ID="ddlProceso" runat="server" Width="250px" CssClass="Apariencia"
                            OnDataBound="ddlProceso_DataBound" AutoPostBack="True" DataSourceID="SqlDataSource11"
                            DataTextField="Nombre" OnSelectedIndexChanged="ddlProceso_SelectedIndexChanged"
                            DataValueField="IdProceso">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr align="center">
                    <td colspan="2">
                        <asp:Button ID="Button3" runat="server" Text="Aceptar" CssClass="Apariencia" OnClientClick="$find('popupProceso').hidePopup(); return false;" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <table align="center" bgcolor="#EEEEEE">
            <tr align="center" bgcolor="#333399">
                <td class="style1">
                    <asp:Label ID="Label61" runat="server" ForeColor="White" Text="Reporte Hallazgos VS Plan de Acción"
                        Font-Bold="False" Font-Names="Calibri" Font-Size="Large"></asp:Label>
                </td>
            </tr>
            <tr align="center">
                <td class="style1">
                    <table>
                        <tr id="trNivelRiesgo" align="left">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label4" runat="server" Text="Nivel de Riesgo:" Font-Names="Calibri"
                                    Font-Size="Small"></asp:Label>
                            </td>
                            <td bgcolor="#EEEEEE">
                                <asp:DropDownList ID="ddlNivelRiesgo" runat="server" Width="200px" Font-Names="Calibri"
                                    Font-Size="Small" AutoPostBack="False" DataSourceID="SqlDataSource24" DataTextField="NombreDetalle"
                                    DataValueField="IdDetalleTipo" OnDataBound="ddlNivelRiesgo_DataBound">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr id="trEdadHallazgo" align="left">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label1" runat="server" Text="Edad Hallazgo:" Font-Names="Calibri"
                                    Font-Size="Small"></asp:Label>
                            </td>
                            <td bgcolor="#EEEEEE">
                                <asp:TextBox ID="tbxEdadHallazgo" runat="server" Width="190px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr id="filaDependencia" runat="server" align="left" visible="False" bgcolor="#EEEEEE">
                            <td id="Td1" runat="server" bgcolor="#BBBBBB">
                                <asp:Label ID="Label5" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Dependencia:"></asp:Label>
                            </td>
                            <td id="Td2" runat="server">
                                <table class="tablaSinBordes">
                                    <tr bgcolor="#EEEEEE">
                                        <td bgcolor="#EEEEEE">
                                            <asp:TextBox ID="txtDependencia" runat="server" Enabled="False" Font-Names="Calibri"
                                                Font-Size="Small" Width="377px"></asp:TextBox>
                                            <asp:Label ID="lblIdDependencia" runat="server" Visible="False"></asp:Label>
                                        </td>
                                        <td bgcolor="#EEEEEE">
                                            <asp:ImageButton ID="imgDependencia" runat="server" ImageUrl="~/Imagenes/Icons/Organization-Chart.png"
                                                OnClientClick="return false;" />
                                            <asp:PopupControlExtender ID="popupDependencia" runat="server" BehaviorID="popup"
                                                DynamicServicePath="" Enabled="True" ExtenderControlID="" OffsetY="-300" PopupControlID="pnlDependencia"
                                                Position="Right" TargetControlID="imgDependencia">
                                            </asp:PopupControlExtender>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr id="filaProceso" runat="server" align="left" bgcolor="#EEEEEE">
                            <td id="Td3" runat="server" align="left" bgcolor="#BBBBBB">
                                <asp:Label ID="Label9" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Proceso:"></asp:Label>
                            </td>
                            <td id="Td4" runat="server">
                                <table class="tablaSinBordes">
                                    <tr>
                                        <td bgcolor="#EEEEEE">
                                            <asp:TextBox ID="txtProceso" runat="server" Enabled="False" Font-Names="Calibri"
                                                Font-Size="Small" Width="377px"></asp:TextBox>
                                        </td>
                                        <td align="left" bgcolor="#EEEEEE">
                                            <asp:ImageButton ID="imgProceso" runat="server" ImageUrl="~/Imagenes/Icons/FlowChart.png"
                                                OnClientClick="return false;" />
                                            <asp:PopupControlExtender ID="popupEProceso" runat="server" BehaviorID="popupProceso"
                                                DynamicServicePath="" Enabled="True" ExtenderControlID="" PopupControlID="pnlProceso"
                                                Position="Right" TargetControlID="imgProceso">
                                            </asp:PopupControlExtender>
                                            <asp:Label ID="lblIdProceso" runat="server" Visible="False"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr align="left">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label3" runat="server" Text="Estado:" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                            <td bgcolor="#EEEEEE">
                                <asp:DropDownList ID="ddlEstado" runat="server" Width="200px" Font-Names="Calibri"
                                    Font-Size="Small" AutoPostBack="False" DataSourceID="SqlDataSource25" DataTextField="NombreDetalle"
                                    DataValueField="IdDetalleTipo" OnDataBound="ddlEstado_DataBound">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr align="center">
                <td class="style1">
                    <table>
                        <tr>
                            <td>
                                <asp:ImageButton ID="btnConsultar" runat="server" ImageUrl="~/Imagenes/Icons/lupa.png"
                                    ToolTip="Consultar" OnClick="btnConsultar_Click" />
                            </td>
                            <td>
                                <asp:ImageButton ID="btnCancelar" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                    ToolTip="Cancelar" OnClick="btnCancelar_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr align="center" id="trRptFactorSenal" runat="server" visible="false">
                <td class="style1">
                    <table>
                        <tr align="left">
                            <td>
                                <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt"
                                    WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Height="700px" Width="750px"
                                    InteractiveDeviceInfos="(Colección)" ShowParameterPrompts="False" ShowPrintButton="False"
                                    ShowRefreshButton="False" ShowZoomControl="False">
                                    <LocalReport ReportPath="Reportes\RptHallazgoVSPlanAccion.rdlc">
                                        <DataSources>
                                            <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="DSHallazgoVsPlanAccion_DataTable1" />
                                        </DataSources>
                                    </LocalReport>
                                </rsweb:ReportViewer>
                                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetData"
                                    TypeName="DSHallazgoVsPlanAccionTableAdapters."></asp:ObjectDataSource>
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
                    <td colspan="2" align="center" runat="server" id="tdCaption">&nbsp;
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
