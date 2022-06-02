<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SeguimientoIndicadores.ascx.cs" Inherits="ListasSarlaft.UserControls.Proceso.SeguimientoIndicadores" %>
<%--<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>--%>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=14.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Src="~/UserControls/Sitio/OkMessageBox.ascx" TagPrefix="uc" TagName="OkMessageBox" %>

<style type="text/css">
    .ajax__html_editor_extender_texteditor
    {
        background-color: White;
        font-size: small;
        font-weight: lighter;
        font-family: Calibri;
        font-style: normal;
    }

    .gridViewHeader a:link
    {
        text-decoration: none;
    }

    .style1
    {
        width: 100%;
    }

    .popup
    {
        border: Silver 1px solid;
        color: #060F40;
        background: #ffffff;
    }

    .Apariencia
    {
    }

    .centerTable
    {
        margin-left: auto;
        margin-right: auto;
    }

    .centertdtr
    {
        text-align: center;
    }

    .center333399
    {
        text-align: center;
        background: #333399;
    }

    .centerEEEEEE
    {
        text-align: center;
        background: #EEEEEE;
    }

    .righttdtr
    {
        text-align: right;
    }

    .right5D7B9D
    {
        text-align: right;
        background: #5D7B9D;
    }

    .lefttdtr
    {
        text-align: left;
    }

    .leftBBBBBB
    {
        text-align: left;
        background: #BBBBBB;
    }

    .Tablewidth
    {
        width: 100%;
    }

    .TablaEspecial
    {
        width: 100%;
        border: hidden;
        border: 0;
        vertical-align: middle;
    }

    .centerMiddle
    {
        text-align: center;
        vertical-align: middle;
    }

    .LeftMiddle
    {
        text-align: left;
        vertical-align: middle;
    }

    .Toptdtr
    {
        vertical-align: top;
    }
    .exito {
background: #DFF2BF url(Url-de-tu-icono) center no-repeat;
background-position: 15px 50%;
color: #4F8A10;
font-family: Arial;
font-size: 16px;
text-align: center;
border-top: 5px double #4F8A10;
border-bottom: 5px double #4F8A10;
}
</style>

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <uc:OkMessageBox ID="omb" runat="server" />
        <asp:Panel ID="pnlMsgBox" runat="server" Width="400px" CssClass="modalPopup" Style="display: none;">
            <table width="100%">
                <tr class="topHandle">
                    <td colspan="2" align="center" runat="server" id="tdCaption">
                        <asp:Label ID="lblCaption" runat="server" Text="Atención" Font-Bold="False" Font-Names="Tahoma"
                            Font-Size="11px"></asp:Label><br />
                    </td>
                </tr>
                <tr>
                    <td  valign="middle" align="center">
                        <asp:Label ID="lblMsgBox" runat="server" Font-Bold="False" Font-Names="Tahoma" Font-Size="11px"
                            Text="Digite el año que desea consultar"
                            ></asp:Label>
                    </td>
                    <td valign="middle" align="left">
                        <asp:TextBox ID="txtPeridoFiltro" runat="server" Width="50px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvPeriodoFiltro" runat="server" ControlToValidate="txtPeridoFiltro"
                                    ErrorMessage="Debe ingresar el año." ToolTip="Debe ingresar el año."
                                    ValidationGroup="pIndicador" ForeColor="Red" InitialValue="">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td align="right" colspan="2">
                        <asp:Button ID="btnImgok" ValidationGroup="pIndicador" runat="server" Text="Ok" OnClick="btnImgok_Click" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:ModalPopupExtender ID="mpeMsgBox" runat="server" PopupControlID="pnlMsgBox"
            BehaviorID="mypopup" Enabled="True" TargetControlID="btndummy" BackgroundCssClass="modalBackground"
            DropShadow="true">
        </asp:ModalPopupExtender>
        <asp:Button ID="btndummy" runat="server" Text="Button" Style="display: none" />
        <table align="center" width="80%">
            <tr id="FilaTituloPpal" class="center333399">
                <td>
                    <asp:Label ID="Label13" runat="server" ForeColor="White" Text="SEGUIMIENTO A INDICADORES" Font-Bold="False"
                        Font-Names="Calibri" Font-Size="Large"></asp:Label>
                </td>
            </tr>
            <tr id="FilaProcesos" runat="server">
                <td style="background: #EEEEEE">
                    <table>
                        <tr id="CadenaValor">
                            <td class="leftBBBBBB">
                                <asp:Label ID="Label19" runat="server" Text="Cadena de Valor:" CssClass="Apariencia"></asp:Label></td>
                            <td>
                                <asp:DropDownList ID="ddlCadenaValor" runat="server" Width="300px"
                                    CssClass="Apariencia" AutoPostBack="True"
                                    DataTextField="NombreCadenaValor" DataValueField="IdCadenaValor"
                                    OnSelectedIndexChanged="ddlCadenaValor_SelectedIndexChanged">
                                </asp:DropDownList><asp:RequiredFieldValidator ID="rfvCadenaValor" runat="server" ControlToValidate="ddlCadenaValor"
                                    ErrorMessage="Debe ingresar la cadena de valor." ToolTip="Debe ingresar la cadena de valor."
                                    ValidationGroup="iIndicador" ForeColor="Red" InitialValue="0">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr id="Macroproceso">
                            <td class="leftBBBBBB">
                                <asp:Label ID="Label22" runat="server" Text="Macroproceso:" CssClass="Apariencia"></asp:Label></td>
                            <td>
                                <asp:DropDownList ID="ddlMacroproceso" runat="server" Width="300px"
                                    CssClass="Apariencia" AutoPostBack="True" DataTextField="Nombre" DataValueField="IdMacroproceso"
                                    OnSelectedIndexChanged="ddlMacroproceso_SelectedIndexChanged">
                                </asp:DropDownList><asp:RequiredFieldValidator ID="rfvMacroProceso" runat="server" ControlToValidate="ddlMacroproceso"
                                    ErrorMessage="Debe ingresar el Macroproceso." ToolTip="Debe ingresar el Macroproceso."
                                    ValidationGroup="iIndicador" ForeColor="Red" InitialValue="0">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr id="Proceso" runat="server" visible="false">
                            <td class="leftBBBBBB">
                                <asp:Label ID="Label7" runat="server" Text="Proceso:" CssClass="Apariencia"></asp:Label></td>
                            <td>
                                <asp:DropDownList ID="ddlProceso" runat="server" Width="300px" OnSelectedIndexChanged="ddlProceso_SelectedIndexChanged"
                                    CssClass="Apariencia" AutoPostBack="True" DataTextField="Nombre" DataValueField="IdProceso">
                                </asp:DropDownList><%--<asp:RequiredFieldValidator ID="rfvProceso" runat="server" ControlToValidate="ddlProceso"
                                    ErrorMessage="Debe ingresar el Proceso." ToolTip="Debe ingresar el Proceso." Enabled="False"
                                    ValidationGroup="iIndicador" ForeColor="Red" InitialValue="0">*</asp:RequiredFieldValidator>--%>
                            </td>
                        </tr>
                        <tr id="Subproceso" runat="server" visible="false">
                            <td class="leftBBBBBB">
                                <asp:Label ID="Label8" runat="server" Text="Subproceso:" CssClass="Apariencia"></asp:Label></td>
                            <td>
                                <asp:DropDownList ID="ddlSubproceso" runat="server" Width="300px" OnSelectedIndexChanged="ddlSubproceso_SelectedIndexChanged"
                                    CssClass="Apariencia" AutoPostBack="True" DataTextField="Nombre" DataValueField="IdProceso">
                                </asp:DropDownList><%--<asp:RequiredFieldValidator ID="rfvSubproceso" runat="server" ControlToValidate="ddlSubproceso"
                                    ErrorMessage="Debe ingresar el Subproceso." ToolTip="Debe ingresar el Subproceso." Enabled="False"
                                    ValidationGroup="iIndicador" ForeColor="Red" InitialValue="0">*</asp:RequiredFieldValidator>--%>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr id="FilaBotonBuscar" runat="server">
                <td align="center" style="background: #EEEEEE">
                    <asp:ImageButton ID="btnBuscarIndicador" runat="server" ValidationGroup="iIndicador" CausesValidation="true" OnClick="btnBuscarIndicador_Click"
                        ImageUrl="~/Imagenes/Icons/Apps-Search-And-Replace-icon.png" ToolTip="Buscar" />
                    <asp:ImageButton ID="btnImgCancelar" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png" OnClick="btnImgCancelar_Click" ToolTip="Cancelar" />
                </td>
            </tr>

            <tr id="FilaTituloIndicador" runat="server" class="center333399">
                <td>
                    <asp:Label ID="Label1" runat="server" ForeColor="White" Text="Indicadores" Font-Bold="False"
                        Font-Names="Calibri" Font-Size="Large"></asp:Label>
                </td>
            </tr>
            <tr id="FilaGridIndicador" style="background: #EEEEEE" runat="server" visible="false">
                <td style="background: #EEEEEE" align="center">
                    <table>
                        <tr>
                            <td>
                                <asp:GridView ID="GridView1" runat="server" CellPadding="4"
                                    ForeColor="#333333" AllowPaging="False" AllowSorting="True" AutoGenerateColumns="False"
                                    ShowHeaderWhenEmpty="True" OnRowCommand="GridView1_RowCommand"
                                    HeaderStyle-CssClass="gridViewHeader" BorderStyle="Solid" GridLines="Vertical"
                                    CssClass="Apariencia" Font-Bold="False">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:BoundField DataField="intId" HeaderText="Código" SortExpression="intId" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="no-visible" HeaderStyle-CssClass="no-visible" />
                                        <asp:TemplateField HeaderText="Descripción Indicador" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 300px">
                                                    <asp:Label ID="DescripcionIndicador" runat="server" Text='<% # Bind("strDescripcion")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" Width="300" HorizontalAlign="center" />
                                            <ItemStyle Wrap="false" Width="300" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="intIdPeriodicidad" HeaderText="Id Periodicidad" SortExpression="intIdPeriodicidad" HtmlEncodeFormatString="True" HtmlEncode="False" Visible="False" />
                                        <asp:TemplateField HeaderText="Periodicidad" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 100px">
                                                    <asp:Label ID="NombrePeriodo" runat="server" Text='<% # Bind("strNombrePeriodicidad")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" Width="100" HorizontalAlign="center" />
                                            <ItemStyle Wrap="false" Width="100" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="intMeta" HeaderText="Meta" SortExpression="intMeta" HtmlEncodeFormatString="True" HtmlEncode="False" Visible="False" />
                                        <asp:TemplateField HeaderText="Estado" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" Visible="False">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chbActivo" runat="server" Checked='<% # Bind("booEstado")%>' Enabled="false" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="intIdObjetivoCalidad" HeaderText="Id Objetivo Calidad" SortExpression="intIdObjetivoCalidad" Visible="False" />
                                        <asp:BoundField DataField="strDescObjetivo" HeaderText="Desc. Objetivo" SortExpression="strDescObjetivo" Visible="False" />
                                        <asp:BoundField DataField="intIdProcesoIndicador" HeaderText="Id ProcesoIndicador" SortExpression="intIdProcesoIndicador" Visible="False" />
                                        <asp:BoundField DataField="intIdUsuario" HeaderText="IdUsuario" SortExpression="intIdUsuario" Visible="False" />
                                        <asp:BoundField DataField="strNombreUsuario" HeaderText="Usuario" SortExpression="strNombreUsuario" Visible="False" />
                                        <asp:BoundField DataField="dtFechaRegistro" HeaderText="FechaRegistro" ReadOnly="True" SortExpression="dtFechaRegistro" ItemStyle-HorizontalAlign="Center" Visible="False" />
                                        <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Icons/select.png" Text="Seguimiento" HeaderText="Seguimiento" CommandName="Seguimiento" ItemStyle-HorizontalAlign="Center" />
                                    </Columns>
                                    <EditRowStyle BackColor="#999999" />
                                    <FooterStyle BackColor="White" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr id="BotonRegresarFiltros" runat="server" class="centertdtr" visible="false">
                <td style="background: #EEEEEE">
                    <asp:ImageButton ID="btnRegresarFiltros" runat="server" CausesValidation="False" CommandName="Back"
                        ImageUrl="~/Imagenes/Icons/undo-icon32x32.png" Text="Regresar a Filtros" OnClick="btnRegresarFiltros_Click" ToolTip="Regresar a Filtros" />
                </td>
            </tr>

            <tr id="FilaTituloAnalisis" class="center333399" runat="server">
                <td>
                    <asp:Label ID="Label2" runat="server" ForeColor="White" Text="Análisis a indicadores" Font-Bold="False"
                        Font-Names="Calibri" Font-Size="Large"></asp:Label>
                </td>
            </tr>
            <tr id="FilaInformativa" runat="server" align="Center" visible="false" style="background: #EEEEEE">
                <td>
                    <table>
                        <tr>
                            <td>
                                <table>
                                     <div runat="server" id="theDiv" visible="false" class="exito">
                                         <asp:Label ID="Lmensaje" runat="server"></asp:Label>  
                                     </div>
                                    <tr>
                                        <td class="leftBBBBBB">
                                            <asp:Label ID="Label9" runat="server" Text="ESTRATEGIA:" CssClass="Apariencia"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="tbxCabEstrategia" runat="server" Enabled="False" Width="400px" CssClass="Apariencia"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="leftBBBBBB">
                                            <asp:Label ID="Label10" runat="server" Text="META:" CssClass="Apariencia"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="tbxCabMeta" runat="server" Enabled="False" Width="400px" CssClass="Apariencia"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="leftBBBBBB">
                                            <asp:Label ID="Label11" runat="server" Text="FRECUENCIA DE MEDICION:" CssClass="Apariencia"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="tbxCabFrecuencia" runat="server" Enabled="False" Width="400px" CssClass="Apariencia"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="leftBBBBBB">
                                            <asp:Label ID="Label12" runat="server" Text="OBJETIVO DE CALIDAD:" CssClass="Apariencia"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="tbxCabObjetivo" runat="server" Enabled="False" Width="400px" CssClass="Apariencia"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="leftBBBBBB">
                                            <asp:Label ID="Label14" runat="server" Text="PROCESO:" CssClass="Apariencia"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="tbxCabProceso" runat="server" Enabled="False" Width="400px" CssClass="Apariencia"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="leftBBBBBB">
                                            <asp:Label ID="Label15" runat="server" Text="RESPONSABLE:" CssClass="Apariencia"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="tbxCabResponsable" runat="server" Enabled="False" Width="400px" CssClass="Apariencia"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="leftBBBBBB">
                                            <asp:Label ID="Label16" runat="server" Text="FORMA DE CALCULAR:" CssClass="Apariencia"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="tbxCabFormula" runat="server" Enabled="False" Width="400px" CssClass="Apariencia"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr id="GridCuadroMando">
                            <td>
                                <asp:GridView ID="GridView3" runat="server" CellPadding="4"
                                    ForeColor="#333333" AllowPaging="False" AllowSorting="True" AutoGenerateColumns="true"
                                    ShowHeaderWhenEmpty="True" OnRowCommand="GridView1_RowCommand"
                                    HeaderStyle-CssClass="gridViewHeader" BorderStyle="Solid" GridLines="Vertical"
                                    CssClass="Apariencia" Font-Bold="False">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                    </Columns>
                                    <EditRowStyle BackColor="#999999" />
                                    <FooterStyle BackColor="White" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                </asp:GridView>

                            </td>
                        </tr>
                        <tr id="Grafico">
                            <td>
                                <asp:Chart ID="Chart1" runat="server" Width="530px">
                                    <Legends>
                                        <asp:Legend Alignment="Center" Docking="Bottom" IsTextAutoFit="False" Name="Default" LegendStyle="Row" />
                                    </Legends>
                                    <Series>
                                        <asp:Series Name="Periodo" ChartType="Column"></asp:Series>
                                    </Series>
                                    <ChartAreas>
                                        <asp:ChartArea Name="ChartArea1" BorderWidth="0" />
                                    </ChartAreas>
                                </asp:Chart>
                            </td>
                        </tr>
                    </table>
                    <%--<rsweb:ReportViewer ID="rptSeguimientoIndicador" runat="server" Font-Names="Verdana" Font-Size="8pt"
                        InteractiveDeviceInfos="(Collection)" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt"
                        Height="1308px" Width="876px">--%>
                    <%--<LocalReport ReportPath="Reportes\.rdlc"></LocalReport>--%>
                    <%--</rsweb:ReportViewer>--%>
                </td>
            </tr>
            <tr id="FilaGridAnalisis" style="background: #EEEEEE" runat="server" visible="false">
                <td style="background: #EEEEEE" align="center">
                    <table>
                        <tr>
                            <td>
                                <asp:GridView ID="GridView2" runat="server" CellPadding="4"
                                    ForeColor="#333333" AllowPaging="False" AllowSorting="True" AutoGenerateColumns="False"
                                    ShowHeaderWhenEmpty="True" OnRowCommand="GridView2_RowCommand"
                                    HeaderStyle-CssClass="gridViewHeader" BorderStyle="Solid" GridLines="Vertical"
                                    CssClass="Apariencia" Font-Bold="False">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:BoundField DataField="intId" HeaderText="Código" SortExpression="intId" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="intIdSeguimientoIndicador" HeaderText="Id. Seguimiento" ItemStyle-HorizontalAlign="Center" Visible="false" />
                                        <asp:BoundField DataField="intIdIndicador" HeaderText="Id. Indicador" ItemStyle-HorizontalAlign="Center" Visible="false" />

                                        <asp:TemplateField HeaderText="Descripción Análisis" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 300px">
                                                    <asp:Label ID="DescAnalisis" runat="server" Text='<% # Bind("strDescripcionAnalisis")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" Width="300" />
                                            <ItemStyle Wrap="false" Width="300" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Descripción Acc. Correctiva" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 300px">
                                                    <asp:Label ID="DescAccCorrectiva" runat="server" Text='<% # Bind("strDescripcionAccionCorrectiva")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" Width="300" />
                                            <ItemStyle Wrap="false" Width="300" />
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="intIdDetPeriodo" HeaderText="Id Periodicidad" Visible="False" />
                                        <asp:BoundField DataField="dtFechaDescripcionAnalisis" HeaderText="Fecha Desc. Análisis" Visible="False" />
                                        <asp:BoundField DataField="dtFechaAccionCorrectiva" HeaderText="Fecha Acc. Correctiva" Visible="False" />

                                        <asp:BoundField DataField="intIdUsuario" HeaderText="IdUsuario" Visible="False" />
                                        <asp:BoundField DataField="strNombreUsuario" HeaderText="Usuario" Visible="False" />
                                        <asp:BoundField DataField="dtFechaRegistro" HeaderText="Fecha Registro" ReadOnly="True" ItemStyle-HorizontalAlign="Center" />
                                        <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Icons/edit.png" Text="Modificar" HeaderText="Modificar" CommandName="Modificar" ItemStyle-HorizontalAlign="Center" />
                                    </Columns>
                                    <EditRowStyle BackColor="#999999" />
                                    <FooterStyle BackColor="White" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:ImageButton ID="btnNuevo" runat="server" CausesValidation="False" CommandName="Nuevo"
                                    ImageUrl="~/Imagenes/Icons/Add.png" Text="Nuevo" OnClick="btnNuevo_Click" ToolTip="Nuevo" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr id="FilaAnalisis" runat="server" style="background: #EEEEEE" visible="false">
                <td>
                    <table class="tabla" width="100%">
                        <tr id="codigo">
                            <td class="leftBBBBBB">
                                <asp:Label ID="Label3" runat="server" Text="Código:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="tbxIdDetSeg" runat="server" Enabled="False" Width="100px" CssClass="Apariencia"></asp:TextBox>
                                <asp:TextBox ID="tbxIdIndicador" runat="server" Enabled="False" Width="100px" CssClass="Apariencia" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="tbxIdPeriodicidad" runat="server" Enabled="False" Width="100px" CssClass="Apariencia" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="tbxEstaEvaluado" runat="server" Enabled="False" Width="100px" CssClass="Apariencia" Visible="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr id="Periodo">
                            <td class="leftBBBBBB">
                                <asp:Label ID="Label5" runat="server" Text="Periodo:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td id="Td2" runat="server" class="lefttdtr">
                                <asp:DropDownList ID="ddlDetPeriodo" runat="server" Width="300px"
                                    CssClass="Apariencia" AutoPostBack="true" DataTextField="Nombre" DataValueField="Id"
                                    OnSelectedIndexChanged="ddlDetPeriodo_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvPeriodoVariable" runat="server" ControlToValidate="ddlDetPeriodo"
                                    ErrorMessage="Debe ingresar el periodo." ToolTip="Debe ingresar el periodo."
                                    ValidationGroup="iAnalisis" ForeColor="Red" InitialValue="0">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr id="Analisis">
                            <td class="leftBBBBBB">
                                <asp:Label ID="Label6" runat="server" Text="Análisis:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="tbxDescAnalisis" runat="server" CssClass="Apariencia" MaxLength="250" Height="42px"
                                    Width="600px" Columns="100" Rows="8" TextMode="MultiLine" Font-Size="10pt" Font-Bold="False"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvDescripcionAnalisis" runat="server" ControlToValidate="tbxDescAnalisis"
                                    ErrorMessage="Debe ingresar el Análisis." ToolTip="Debe ingresar el Análisis."
                                    ValidationGroup="iAnalisis" ForeColor="Red">*</asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator runat="server" ID="rfvDescLen" Display="Dynamic" ForeColor="Red"
                                    ControlToValidate="tbxDescAnalisis" ValidationExpression="^[\s\S]{0,250}$" ValidationGroup="iAnalisis"
                                    ErrorMessage="La longitud máxima es 250 caracteres" ToolTip="La longitud máxima es 250 caracteres">*</asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr id="Correctiva">
                            <td class="leftBBBBBB">
                                <asp:Label ID="Label4" runat="server" Text="Acción Correctiva:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="tbxAccCorrectiva" runat="server" CssClass="Apariencia" MaxLength="250" Height="42px"
                                    Width="600px" Columns="100" Rows="8" TextMode="MultiLine" Font-Size="10pt" Font-Bold="False"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvAccCorrectiva" runat="server" ControlToValidate="tbxAccCorrectiva"
                                    ErrorMessage="Debe ingresar la acción correctiva. Si no hay información por favor poner NA"
                                    ToolTip="Debe ingresar la acción correctiva. Si no hay información por favor poner NA"
                                    ValidationGroup="iAnalisis" ForeColor="Red">*</asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator runat="server" ID="revAccCorrectiva" Display="Dynamic" ForeColor="Red"
                                    ControlToValidate="tbxAccCorrectiva" ValidationExpression="^[\s\S]{0,250}$" ValidationGroup="iAnalisis"
                                    ErrorMessage="La longitud máxima es 250 caracteres" ToolTip="La longitud máxima es 250 caracteres">*</asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <!-- Jhon R -->
                        <tr align="center">
                            <td class="leftBBBBBB" runat="server">
                                <asp:Label ID="PeriodoAnual" runat="server" BackColor="Silver" CssClass="Apariencia" Text="Período anual:" Width="300px"></asp:Label>
                            </td>
                            <td align="left">
                                
                                <asp:TextBox ID="txtperiodoanual" runat="server" Width="100px" CssClass="Apariencia" MaxLength="4"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revPeriodonaual" runat="server" 
                                    ControlToValidate="txtperiodoanual" ErrorMessage="Obligatorio solo números" ForeColor="Red" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <!-- Jhon R -->
                        <tr id="botonesAnalisis" align="center">
                            <td colspan="2">
                                <table class="tablaSinBordes">
                                    <tr>
                                        <td>
                                            <asp:ImageButton ID="btnInsertarAnalisis" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                Visible="False" OnClick="btnInsertarAnalisis_Click" ToolTip="Guardar" ValidationGroup="iAnalisis" />
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="btnActualizarAnalisis" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                Style="text-align: right" OnClick="btnActualizarAnalisis_Click" ToolTip="Guardar" ValidationGroup="iAnalisis" />
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="btnCancelarAnalisis" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                                OnClick="btnCancelarAnalisis_Click" ToolTip="Cancelar" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>

            <tr id="BotonRegresarIndicador" runat="server" class="centertdtr" visible="false">
                <td style="background: #EEEEEE">
                    <asp:ImageButton ID="btnRegresarIndicador" runat="server" CausesValidation="False" CommandName="Back"
                        ImageUrl="~/Imagenes/Icons/undo-icon32x32.png" Text="Regresar a Indicadores" OnClick="btnRegresarIndicador_Click" ToolTip="Regresar a Indicadores" />
                </td>
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>
<%--The fnClick javascript function will force the btnOk button to post back. 
(to actually achieve this we need to add a little piece of code in 
the Page_Load method of the user control.--%>
<script type="text/javascript">
    function fnClick(sender, e) {
        __doPostBack(sender, e);
    }
</script>
