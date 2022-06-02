<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RpteRegistroSarlaft.ascx.cs"
    Inherits="ListasSarlaft.UserControls.RpteRegistroSarlaft" %>
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
<asp:SqlDataSource ID="SqlDataSourceRegistro" runat="server" ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
    SelectCommand="SP_SarlaftPorRegistro" SelectCommandType="StoredProcedure">
    <SelectParameters>
        <asp:ControlParameter ControlID="DropDownList5" DefaultValue="0" Name="IdTipoIden"
            PropertyName="SelectedValue" Type="String" />
        <asp:ControlParameter ControlID="TextBox1" DefaultValue="0" Name="Identificacion"
            PropertyName="Text" Type="String" />
    </SelectParameters>
</asp:SqlDataSource>
<asp:SqlDataSource ID="SqlDataSourcePorRegistroComentario" runat="server" ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
    SelectCommand="SP_SarlaftPorRegistroComentarios" SelectCommandType="StoredProcedure">
    <SelectParameters>
        <asp:ControlParameter ControlID="LabelIdRegOper" DefaultValue="0" Name="IdRegistroOperacion"
            PropertyName="Text" Type="String" />
    </SelectParameters>
</asp:SqlDataSource>
<asp:SqlDataSource ID="SqlDataSourceDctosAdjuntos" runat="server" ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
    SelectCommand="SELECT IdArchivo,IdRegistroOperacion,NombreUsuario,FechaRegistro,UrlArchivo FROM Proceso.ArchivoRegistroOperacion WHERE  IdRegistroOperacion = @IdRegistroOperacion">
    <SelectParameters>
        <asp:ControlParameter ControlID="LabelIdRegOper" DefaultValue="0" Name="IdRegistroOperacion"
            PropertyName="Text" />
    </SelectParameters>
</asp:SqlDataSource>
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
                    <asp:Label ID="Label61" runat="server" ForeColor="White" Text="Reporte Por Registro"
                        Font-Bold="False" Font-Names="Calibri" Font-Size="Large"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                </td>
            </tr>
            <tr>
                <td>
                    <table align="center">
                        <tr id="TbTIdentificacion" runat="server" class="no-visible">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label5" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Tipo Identificación"></asp:Label>
                            </td>
                            <td bgcolor="#EEEEEE">
                                <asp:DropDownList ID="DropDownList5" runat="server" Font-Names="Calibri" Font-Size="Small"
                                    Width="200px">
                                    <asp:ListItem Value="0">---</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr id="TbNIdentificacion" runat="server">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label6" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Número Identificación"></asp:Label>
                            </td>
                            <td bgcolor="#EEEEEE">
                                <asp:TextBox ID="TextBox1" runat="server" Font-Names="Calibri" Font-Size="Small"
                                    Width="200px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TextBox1"
                                    InitialValue="" ValidationGroup="VerRpte" Font-Size="Small" Font-Bold="true"
                                    ForeColor="Red">*</asp:RequiredFieldValidator>
                                <asp:Label ID="LabelIdRegOper" runat="server" Visible="false"></asp:Label>
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
                    <table align="center" runat="server" id="TbPorRegistro" visible="false">
                        <tr>
                            <td>
                                <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                    ForeColor="#333333" GridLines="Vertical" ShowHeaderWhenEmpty="True" HeaderStyle-CssClass="gridViewHeader"
                                    BorderStyle="Solid" HorizontalAlign="Center" Font-Names="Calibri" Font-Size="Small"
                                    DataKeyNames="IdRegistroOperacion" DataSourceID="SqlDataSourceRegistro" OnRowCommand="GridView3_RowCommand">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:BoundField DataField="IdRegistroOperacion" HeaderText="Id Registro Operación"
                                            InsertVisible="False" ReadOnly="True" SortExpression="IdRegistroOperacion" />
                                        <asp:BoundField DataField="Número Identificación" HeaderText="Número Identificación"
                                            ReadOnly="True" SortExpression="Número Identificación" />
                                        <asp:BoundField DataField="Nombre / Razón Social" HeaderText="Nombre / Razón Social"
                                            ReadOnly="True" SortExpression="Nombre / Razón Social" />
                                        <asp:BoundField DataField="Indicador / Señal de Alerta" HeaderText="Indicador / Señal de Alerta"
                                            ReadOnly="True" SortExpression="Indicador / Señal de Alerta" />
                                        <asp:BoundField DataField="NombreTipoRegistro" HeaderText="Tipo Registro" ReadOnly="True"
                                            SortExpression="NombreTipoRegistro" />
                                        <asp:BoundField DataField="NombreEstado" HeaderText="Estado" ReadOnly="True"
                                            SortExpression="NombreEstado" />
                                        <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Icons/print.png" Text="Imprimir"
                                            CommandName="Imprimir" HeaderText="Imprimir">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:ButtonField>
                                        <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Icons/downloads.png" Text="Descargar Adjuntos"
                                            CommandName="Descargar" HeaderText="Adjuntos">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:ButtonField>
                                    </Columns>
                                    <EditRowStyle BackColor="#999999" />
                                    <FooterStyle BackColor="White" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Left" />
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
            <tr>
                <td>
                    <table align="center" runat="server" id="TbAdjuntos" visible="false">
                        <tr>
                            <td>
                                <br />
                                <br />
                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                    ForeColor="#333333" GridLines="Vertical" ShowHeaderWhenEmpty="True" HeaderStyle-CssClass="gridViewHeader"
                                    BorderStyle="Solid" HorizontalAlign="Center" Font-Names="Calibri" Font-Size="Small"
                                    OnRowCommand="GridView1_RowCommand" DataKeyNames="IdArchivo" DataSourceID="SqlDataSourceDctosAdjuntos">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:BoundField DataField="IdArchivo" HeaderText="IdArchivo" InsertVisible="False"
                                            ReadOnly="True" SortExpression="IdArchivo" />
                                        <asp:BoundField DataField="IdRegistroOperacion" HeaderText="IdRegistroOperacion"
                                            SortExpression="IdRegistroOperacion" />
                                        <asp:BoundField DataField="NombreUsuario" HeaderText="NombreUsuario" SortExpression="NombreUsuario" />
                                        <asp:BoundField DataField="FechaRegistro" HeaderText="FechaRegistro" SortExpression="FechaRegistro" />
                                        <asp:BoundField DataField="UrlArchivo" HeaderText="UrlArchivo" SortExpression="UrlArchivo" />
                                        <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Icons/downloads.png" Text="Descargar"
                                            CommandName="Descargar" />
                                    </Columns>
                                    <EditRowStyle BackColor="#999999" />
                                    <FooterStyle BackColor="White" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Left" />
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
            <tr>
                <td>
                    <table>
                        <tr>
                            <td>
                                <br />
                                <br />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr id="TbPorRegistroComentario" runat="server">
                <td>
                    <rsweb:ReportViewer ID="ReportViewer3" runat="server" Font-Names="Verdana" Font-Size="8pt"
                        InteractiveDeviceInfos="(Colección)" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt"
                        Width="900px">
                        <LocalReport ReportPath="Reportes\SarlaftPorRegistro.rdlc">
                            <DataSources>
                                <rsweb:ReportDataSource DataSourceId="SqlDataSourcePorRegistroComentario" Name="DataSetPorRegistro" />
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
    <Triggers>
        <asp:PostBackTrigger ControlID="GridView1" />
    </Triggers>
</asp:UpdatePanel>
