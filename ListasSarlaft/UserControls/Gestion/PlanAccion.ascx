<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PlanAccion.ascx.cs"
    Inherits="ListasSarlaft.UserControls.Gestion.PlanAccion" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControls/Sitio/OkMessageBox.ascx" TagPrefix="uc" TagName="OkMessageBox" %>
<script type="text/javascript">
    function fnClick(sender, e) {
        __doPostBack(sender, e);
    }
</script>
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
        background: #ffffcc;
    }
    
    #Background
    {
        position: fixed;
        top: 0px;
        bottom: 0px;
        left: 0px;
        right: 0px;
        overflow: hidden;
        padding: 0;
        margin: 0;
        background: #EEEEEE;
        filter: alpha(opacity=80);
        opacity: 0.8;
        z-index: 100000;
    }
    
    #Progress
    {
        position: fixed;
        top: 40%;
        left: 40%;
        height: 10%;
        width: 20%;
        z-index: 100001;
        background-color: #FFFFFF;
        border: 1px solid Gray;
        background-image: url('./Imagenes/Icons/loading.gif');
        background-repeat: no-repeat;
        background-position: center;
    }
</style>
<asp:SqlDataSource ID="SqlDataSource200" runat="server" ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
    DeleteCommand="DELETE FROM [Notificaciones].[CorreosEnviados] WHERE [IdCorreosEnviados] = @IdCorreosEnviados"
    InsertCommand="INSERT INTO [Notificaciones].[CorreosEnviados] ([IdEvento], [Destinatario], [Copia], [Otros], [Asunto], [Cuerpo], [Estado], [IdRegistro], [FechaEnvio], [FechaRegistro], [IdUsuario], [Tipo]) VALUES (@IdEvento, @Destinatario, @Copia, @Otros, @Asunto, @Cuerpo, @Estado, @IdRegistro, @FechaEnvio, @FechaRegistro, @IdUsuario, @Tipo) SET @NewParameter2=SCOPE_IDENTITY();"
    SelectCommand="SELECT [IdCorreosEnviados], [IdEvento], [Destinatario], [Copia], [Otros], [Asunto], [Cuerpo], [Estado], [IdRegistro], [FechaEnvio], [FechaRegistro], [IdUsuario] FROM [Notificaciones].[CorreosEnviados]"
    UpdateCommand="UPDATE [Notificaciones].[CorreosEnviados] SET [FechaEnvio] = @FechaEnvio, [Estado] = @Estado WHERE [IdCorreosEnviados] = @IdCorreosEnviados"
    OnInserted="SqlDataSource200_On_Inserted">
    <DeleteParameters>
        <asp:Parameter Name="IdCorreosEnviados" Type="Decimal" />
    </DeleteParameters>
    <InsertParameters>
        <asp:Parameter Name="IdEvento" Type="Decimal" />
        <asp:Parameter Name="Destinatario" Type="String" />
        <asp:Parameter Name="Copia" Type="String" />
        <asp:Parameter Name="Otros" Type="String" />
        <asp:Parameter Name="Asunto" Type="String" />
        <asp:Parameter Name="Cuerpo" Type="String" />
        <asp:Parameter Name="Estado" Type="String" />
        <asp:Parameter Name="Tipo" Type="String" />
        <asp:Parameter Name="IdRegistro" Type="Decimal" />
        <asp:Parameter Name="FechaEnvio" Type="DateTime" />
        <asp:Parameter Name="FechaRegistro" Type="DateTime" />
        <asp:Parameter Name="IdUsuario" Type="Decimal" />
        <asp:Parameter Direction="Output" Name="NewParameter2" Type="Int32" />
    </InsertParameters>
    <UpdateParameters>
        <asp:Parameter Name="Estado" Type="String" />
        <asp:Parameter Name="FechaEnvio" Type="DateTime" />
        <asp:Parameter Name="IdCorreosEnviados" Type="Decimal" />
    </UpdateParameters>
</asp:SqlDataSource>
<asp:SqlDataSource ID="SqlDataSource201" runat="server" ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
    DeleteCommand="DELETE FROM [Notificaciones].[CorreosRecordatorio] WHERE [IdCorreosRecordatorio] = @IdCorreosRecordatorio"
    InsertCommand="INSERT INTO [Notificaciones].[CorreosRecordatorio] ([IdCorreosEnviados], [NroDiasRecordatorio], [FechaFinal], [Estado], [FechaRegistro], [IdUsuario]) VALUES (@IdCorreosEnviados, @NroDiasRecordatorio, @FechaFinal, @Estado, @FechaRegistro, @IdUsuario)"
    SelectCommand="SELECT [IdCorreosRecordatorio], [IdCorreosEnviados], [NroDiasRecordatorio], [FechaFinal], [Estado], [FechaRegistro], [IdUsuario] FROM [Notificaciones].[CorreosRecordatorio]"
    UpdateCommand="UPDATE [Estado] = @Estado WHERE [IdCorreosRecordatorio] = @IdCorreosRecordatorio">
    <DeleteParameters>
        <asp:Parameter Name="IdCorreosRecordatorio" Type="Decimal" />
    </DeleteParameters>
    <InsertParameters>
        <asp:Parameter Name="IdCorreosEnviados" Type="Decimal" />
        <asp:Parameter Name="NroDiasRecordatorio" Type="Int32" />
        <asp:Parameter Name="FechaFinal" Type="DateTime" />
        <asp:Parameter Name="Estado" Type="String" />
        <asp:Parameter Name="FechaRegistro" Type="DateTime" />
        <asp:Parameter Name="IdUsuario" Type="Decimal" />
    </InsertParameters>
    <UpdateParameters>
        <asp:Parameter Name="Estado" Type="String" />
        <asp:Parameter Name="IdCorreosRecordatorio" Type="Decimal" />
    </UpdateParameters>
</asp:SqlDataSource>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <uc:OkMessageBox ID="omb" runat="server" />
        <table align="center" bgcolor="#EEEEEE">
            <tr>
                <td align="center">
                    <asp:UpdateProgress ID="UpdateProgress4" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
                        DisplayAfter="0">
                        <ProgressTemplate>
                            <div id="Background">
                            </div>
                            <div id="Progress">
                                <asp:Label ID="Lbl11" runat="server" Text="Procesando, por favor espere..." Font-Names="Calibri"
                                    Font-Size="Small"></asp:Label>
                                <br />
                                <asp:Image ID="Img11" runat="server" ImageUrl="~/Imagenes/Icons/loading.gif" />
                            </div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                </td>
            </tr>
            <tr align="center" bgcolor="#333399">
                <td>
                    <asp:Label ID="Label7" runat="server" ForeColor="White" Text="Planes de Acción" Font-Bold="False"
                        Font-Names="Calibri" Font-Size="Large"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                    <table id="FiltroPE" runat="server" align="center" visible="true">
                        <tr>
                            <td>
                                <asp:GridView ID="GridViewPlanEstratagico" runat="server" AutoGenerateColumns="False"
                                    BorderStyle="Solid" CellPadding="4" Font-Names="Calibri" Font-Size="Small" ForeColor="#333333"
                                    GridLines="Vertical" HeaderStyle-CssClass="gridViewHeader" HorizontalAlign="Center"
                                    OnRowCommand="GridViewPlanEstratagico_RowCommand" ShowHeaderWhenEmpty="True"
                                    OnSelectedIndexChanged="GridViewPlanEstratagico_SelectedIndexChanged" Width="284px">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:BoundField DataField="IdPlan" HeaderText="Id" Visible="false" />
                                        <asp:BoundField DataField="CodigoPlan" HeaderText="Código" />
                                        <asp:BoundField DataField="Nombre" HeaderText="Plan Estratégico" />
                                        <asp:ButtonField ButtonType="Image" CommandName="Filtrar" HeaderText="Acción" ImageUrl="~/Imagenes/Icons/select.png"
                                            Text="Seleccionar">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:ButtonField>
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
                        </caption>
                    </table>
                    <table id="FiltroAplicado" runat="server" align="center" visible="false">
                        <tr align="center" bgcolor="#5D7B9D">
                            <td colspan="4" bgcolor="#5D7B9D">
                                <asp:Label ID="Label24" runat="server" Text="Plan Estratégico" Font-Bold="False"
                                    Font-Names="Calibri" Font-Size="small" Style="font-weight: 700; color: #FFFFFF;"></asp:Label>
                            </td>
                        </tr>
                        <tr align="left">
                            <td>
                                <asp:Label ID="Label23" runat="server" Text="Id:" Font-Names="Calibri" Font-Size="Small"
                                    Visible="false"></asp:Label>
                            </td>
                            <td colspan="3">
                                <asp:Label ID="LabelIdPlan" runat="server" Font-Names="Calibri" Font-Size="Small"
                                    Visible="false"></asp:Label>
                            </td>
                        </tr>
                        <tr align="left">
                            <td bgcolor="#5D7B9D">
                                <asp:Label ID="Label19" runat="server" Text="Código:" Font-Names="Calibri" Font-Size="Small"
                                    Style="color: #FFFFFF"></asp:Label>
                            </td>
                            <td colspan="3">
                                <asp:Label ID="LabelCodigoPlan" runat="server" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                        </tr>
                        <tr align="left">
                            <td bgcolor="#5D7B9D">
                                <asp:Label ID="Label22" runat="server" Text="Nombre:" Font-Names="Calibri" Font-Size="Small"
                                    Style="color: #FFFFFF; background-color: #5D7B9D"></asp:Label>
                            </td>
                            <td colspan="3">
                                <asp:Label ID="LabelNombrePlan" runat="server" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                        </tr>
                        <tr align="left">
                            <td bgcolor="#5D7B9D">
                                <asp:Label ID="Label21" runat="server" Text="Fecha inicio:" Font-Names="Calibri"
                                    Font-Size="Small" Visible="true" Style="color: #FFFFFF; background-color: #5D7B9D"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox12" runat="server" Enabled="false" Font-Names="Calibri" Font-Size="Small"
                                    Width="89px" Visible="true"></asp:TextBox>
                            </td>
                            <td bgcolor="#5D7B9D">
                                <asp:Label ID="Label26" runat="server" Text="Fecha fin:" Font-Names="Calibri" Font-Size="Small"
                                    Visible="true" Style="color: #FFFFFF; background-color: #5D7B9D"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox11" runat="server" Enabled="false" Font-Names="Calibri" Font-Size="Small"
                                    Width="89px" Visible="true"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" align="center">
                                <asp:Button ID="VerPlanEstrategico" runat="server" Font-Bold="False" Font-Names="Calibri"
                                    Font-Size="small" Text="Planes..." OnClick="VerPlanEstrategico_Click" Width="100px"
                                    ToolTip="Ver Planes" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                            </td>
                        </tr>
                    </table>
                    <table id="FiltroOBJ" runat="server" align="center" visible="true">
                        <tr>
                            <td>
                                <asp:GridView ID="GridViewOBJ" runat="server" AutoGenerateColumns="False" BorderStyle="Solid"
                                    CellPadding="4" Font-Names="Calibri" Font-Size="Small" ForeColor="#333333" GridLines="Vertical"
                                    HeaderStyle-CssClass="gridViewHeader" HorizontalAlign="Center" OnRowCommand="GridViewOBJ_RowCommand"
                                    ShowHeaderWhenEmpty="True" Width="274px">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:BoundField DataField="IdObjetivo" HeaderText="Id" Visible="false" />
                                        <asp:BoundField DataField="CodigoObjetivo" HeaderText="Código" />
                                        <asp:BoundField DataField="Descripcion" HeaderText="Objetivo Estratégico" />
                                        <asp:ButtonField ButtonType="Image" CommandName="FiltrarOBJ" HeaderText="Acción"
                                            ImageUrl="~/Imagenes/Icons/select.png" Text="Seleccionar">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:ButtonField>
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
                        </caption>
                    </table>
                    <table id="FiltroAplicadoOBJ" runat="server" align="center" visible="false">
                        <tr align="center" bgcolor="#5D7B9D">
                            <td colspan="2" bgcolor="#5D7B9D">
                                <asp:Label ID="Label8" runat="server" Text="Objetivo Estratégico" Font-Bold="False"
                                    Font-Names="Calibri" Font-Size="small" Style="font-weight: 700; color: #FFFFFF;"></asp:Label>
                            </td>
                        </tr>
                        <tr align="left">
                            <td>
                                <asp:Label ID="Label11" runat="server" Text="Id:" Font-Names="Calibri" Font-Size="Small"
                                    Visible="false"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="LabelIdOBJ" runat="server" Font-Names="Calibri" Font-Size="Small"
                                    Visible="false"></asp:Label>
                            </td>
                        </tr>
                        <tr align="left">
                            <td bgcolor="#5D7B9D">
                                <asp:Label ID="Label17" runat="server" Text="Código:" Font-Names="Calibri" Font-Size="Small"
                                    Style="color: #FFFFFF"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="LabelCodigoOBJ" runat="server" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                        </tr>
                        <tr align="left">
                            <td bgcolor="#5D7B9D">
                                <asp:Label ID="Label25" runat="server" Text="Descripción:" Font-Names="Calibri" Font-Size="Small"
                                    Style="color: #FFFFFF; background-color: #5D7B9D"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="LabelDescOBJ" runat="server" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="center">
                                <asp:Button ID="VerObjetivos" runat="server" Font-Bold="False" Font-Names="Calibri"
                                    Font-Size="small" Text="Objetivos..." OnClick="VerObjetivos_Click" Width="100px"
                                    ToolTip="Ver Objetivos" />
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                            </td>
                        </tr>
                    </table>
                    <table id="FiltroEST" runat="server" align="center" visible="true">
                        <tr>
                            <td>
                                <asp:GridView ID="GridViewEST" runat="server" AutoGenerateColumns="False" BorderStyle="Solid"
                                    CellPadding="4" Font-Names="Calibri" Font-Size="Small" ForeColor="#333333" GridLines="Vertical"
                                    HeaderStyle-CssClass="gridViewHeader" HorizontalAlign="Center" OnRowCommand="GridViewEST_RowCommand"
                                    ShowHeaderWhenEmpty="True">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:BoundField DataField="IdEstrategia" HeaderText="Id" Visible="false" />
                                        <asp:BoundField DataField="CodigoEstrategia" HeaderText="Código" />
                                        <asp:BoundField DataField="Descripcion" HeaderText="Estrategia" />
                                        <asp:ButtonField ButtonType="Image" CommandName="FiltrarEST" HeaderText="Acción"
                                            ImageUrl="~/Imagenes/Icons/select.png" Text="Seleccionar">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:ButtonField>
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
                        </caption>
                    </table>
                    <table id="FiltroAplicadoEST" runat="server" align="center" visible="false">
                        <tr align="center" bgcolor="#5D7B9D">
                            <td colspan="2" bgcolor="#5D7B9D">
                                <asp:Label ID="Label47" runat="server" Text="Estrategia" Font-Bold="False" Font-Names="Calibri"
                                    Font-Size="small" Style="font-weight: 700; color: #FFFFFF;"></asp:Label>
                            </td>
                        </tr>
                        <tr align="left">
                            <td>
                                <asp:Label ID="Label48" runat="server" Text="Id:" Font-Names="Calibri" Font-Size="Small"
                                    Visible="false"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label49" runat="server" Font-Names="Calibri" Font-Size="Small" Visible="false"></asp:Label>
                            </td>
                        </tr>
                        <tr align="left">
                            <td bgcolor="#5D7B9D">
                                <asp:Label ID="Label50" runat="server" Text="Código:" Font-Names="Calibri" Font-Size="Small"
                                    Style="color: #FFFFFF"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label51" runat="server" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                        </tr>
                        <tr align="left">
                            <td bgcolor="#5D7B9D">
                                <asp:Label ID="Label52" runat="server" Text="Descripción:" Font-Names="Calibri" Font-Size="Small"
                                    Style="color: #FFFFFF; background-color: #5D7B9D"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label53" runat="server" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="center">
                                <asp:Button ID="Button1" runat="server" Font-Bold="False" Font-Names="Calibri" Font-Size="small"
                                    Text="Estrategias..." OnClick="VerEstrategias_Click" Width="100px" ToolTip="Ver Estrategias..." />
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                            </td>
                        </tr>
                    </table>
                    <table id="TbEstrategia" runat="server" align="center" visible="false">
                        <tr align="center" bgcolor="#5D7B9D">
                            <td bgcolor="#5D7B9D">
                                <asp:Label ID="Label54" runat="server" Text="Planes de Acción" Font-Bold="False"
                                    Font-Names="Calibri" Font-Size="small" Style="font-weight: 700; color: #FFFFFF;"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BorderStyle="Solid"
                                    CellPadding="4" Font-Names="Calibri" Font-Size="Small" ForeColor="#333333" GridLines="Vertical"
                                    HeaderStyle-CssClass="gridViewHeader" OnRowCommand="GridView1_RowCommand" OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
                                    ShowHeaderWhenEmpty="True">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:BoundField DataField="IdPlanAccion" HeaderText="ID" Visible="false" />
                                        <asp:BoundField DataField="CodigoPlanAccion" HeaderText="Código" />
                                        <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                                        <asp:BoundField DataField="FechaInicio" HeaderText="Fecha Inicio" />
                                        <asp:BoundField DataField="FechaFin" HeaderText="Fecha Fin" />
                                        <asp:BoundField DataField="Responsable" HeaderText="Responsable" Visible="false" />
                                        <asp:BoundField DataField="Usuario" HeaderText="Usuario" Visible="false" />
                                        <asp:BoundField DataField="FechaRegistro" HeaderText="Fecha Registro" Visible="false" />
                                        <asp:ButtonField ButtonType="Image" CommandName="Indicadores" HeaderText="Indicadores"
                                            ImageUrl="~/Imagenes/Icons/Literal.png" Text="Indicadores" HeaderStyle-HorizontalAlign="Center">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:ButtonField>
                                        <asp:ButtonField ButtonType="Image" CommandName="Modificar" HeaderText="Modificar"
                                            ImageUrl="~/Imagenes/Icons/edit.png" Text="Modificar">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:ButtonField>
                                        <asp:ButtonField ButtonType="Image" CommandName="Eliminar" HeaderText="Eliminar"
                                            ImageUrl="~/Imagenes/Icons/delete.png" Text="Eliminar">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:ButtonField>
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
                        <tr align="center">
                            <td>
                                <asp:ImageButton ID="BtnAdicionaPlan" runat="server" ImageUrl="~/Imagenes/Icons/Add.png"
                                    OnClick="BtnAdicionaPlan_Click" ToolTip="Agregar" ValidationGroup="addLista" />
                            </td>
                        </tr>
                        </caption>
                    </table>
                    <table id="TbFiltroEstrategia" runat="server" align="center" visible="false">
                        <tr align="center" bgcolor="#5D7B9D">
                            <td colspan="2" bgcolor="#5D7B9D">
                                <asp:Label ID="Label14" runat="server" Text="Plan de Acción" Font-Bold="False" Font-Names="Calibri"
                                    Font-Size="small" Style="font-weight: 700; color: #FFFFFF;"></asp:Label>
                            </td>
                        </tr>
                        <tr align="left">
                            <td>
                                <asp:Label ID="Label20" runat="server" Text="Id:" Font-Names="Calibri" Font-Size="Small"
                                    Visible="false"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="LabelIdEstrategia" runat="server" Font-Names="Calibri" Font-Size="Small"
                                    Visible="false"></asp:Label>
                            </td>
                        </tr>
                        <tr align="left">
                            <td bgcolor="#5D7B9D">
                                <asp:Label ID="Label28" runat="server" Text="Código:" Font-Names="Calibri" Font-Size="Small"
                                    Style="color: #FFFFFF"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label29" runat="server" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                        </tr>
                        <tr align="left">
                            <td bgcolor="#5D7B9D">
                                <asp:Label ID="Label30" runat="server" Text="Descripción:" Font-Names="Calibri" Font-Size="Small"
                                    Style="color: #FFFFFF; background-color: #5D7B9D"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label31" runat="server" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="center">
                                <asp:Button ID="VerEstrategias" runat="server" Font-Bold="False" Font-Names="Calibri"
                                    Font-Size="small" Text="Planes de Acción..." OnClick="VerEstrategias_Click" Width="120px"
                                    ToolTip="Ver Estrategias" />
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table id="TbModificarVision" runat="server" visible="false" align="center">
                        <tr align="center" bgcolor="#333399">
                            <td colspan="2">
                                <asp:Label ID="Label15" runat="server" Text="Modificar Plan de Acción" Font-Bold="False"
                                    Font-Names="Calibri" Font-Size="medium" ForeColor="White"></asp:Label>
                            </td>
                        </tr>
                        <tr align="left">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label12" runat="server" Text="Código:" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox20" runat="server" Enabled="false" Font-Names="Calibri" Font-Size="Small"
                                    Width="55px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr align="left">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label5" runat="server" Text="Descipción:" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox2" runat="server" Width="404px" Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="updateLista"
                                    ControlToValidate="TextBox2" Display="Dynamic" ForeColor="Red" InitialValue="">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr align="left">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label41" runat="server" Text="Fecha inicio:" Font-Names="Calibri"
                                    Font-Size="Small"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox13" runat="server" Font-Names="Calibri" Font-Size="Small"
                                    Width="96px"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="true" TargetControlID="TextBox13"
                                    Format="yyyy-MM-dd">
                                </asp:CalendarExtender>
                                <asp:CompareValidator ID="CompareValidator6" Display="Dynamic" ForeColor="Red" Font-Names="Calibri"
                                    runat="server" ControlToValidate="TextBox13" ControlToCompare="TextBox12" Font-Size="Small"
                                    ErrorMessage="Fecha Inválida. Fecha inicio no puede ser menor a Fecha inicio del Plan Estratégico"
                                    Type="Date" Operator="GreaterThanEqual" ValidationGroup="updateLista"> </asp:CompareValidator>
                                <asp:CompareValidator ID="CompareValidator7" Display="Dynamic" ForeColor="Red" Font-Names="Calibri"
                                    runat="server" ControlToValidate="TextBox13" ControlToCompare="TextBox11" Font-Size="Small"
                                    ErrorMessage="Fecha Inválida. Fecha inicio no puede ser mayor a Fecha fin del Plan Estratégico"
                                    Type="Date" Operator="LessThanEqual" ValidationGroup="updateLista"> </asp:CompareValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ValidationGroup="updateLista"
                                    ControlToValidate="TextBox13" Display="Dynamic" ForeColor="Red" InitialValue="">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr align="left">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label42" runat="server" Text="Fecha fin:" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox14" runat="server" Font-Names="Calibri" Font-Size="Small"
                                    Width="96px"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender4" runat="server" Enabled="true" TargetControlID="TextBox14"
                                    Format="yyyy-MM-dd">
                                </asp:CalendarExtender>
                                <asp:CompareValidator ID="CompareValidator1" Display="Dynamic" ForeColor="Red" Font-Names="Calibri"
                                    runat="server" ControlToValidate="TextBox14" ControlToCompare="TextBox13" Font-Size="Small"
                                    ErrorMessage="Fecha Inválida. Fecha fin no puede ser menor a Fecha inicio" Type="Date"
                                    Operator="GreaterThanEqual" ValidationGroup="updateLista"> </asp:CompareValidator>
                                <asp:CompareValidator ID="CompareValidator5" Display="Dynamic" ForeColor="Red" Font-Names="Calibri"
                                    runat="server" ControlToValidate="TextBox14" ControlToCompare="TextBox11" Font-Size="Small"
                                    ErrorMessage="Fecha Inválida. Fecha fin no puede ser mayor a Fecha fin del Plan Estratégico"
                                    Type="Date" Operator="LessThanEqual" ValidationGroup="updateLista"> </asp:CompareValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ValidationGroup="updateLista"
                                    ControlToValidate="TextBox14" Display="Dynamic" ForeColor="Red" InitialValue="">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr align="left">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label43" runat="server" Text="Responsable:" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox15" runat="server" Width="405px" Font-Names="Calibri" Font-Size="Small"
                                    Enabled="false"></asp:TextBox>
                                <asp:Label ID="Label55" runat="server" Font-Names="Calibri" Font-Size="Small" Visible="false"
                                    Text=""></asp:Label>
                                <asp:ImageButton ID="imgDependencia2" runat="server" ImageUrl="~/Imagenes/Icons/Organization-Chart.png"
                                    OnClientClick="return false;" ToolTip="Responsable" />
                                <asp:PopupControlExtender ID="popupDependencia2" runat="server" DynamicServicePath=""
                                    Enabled="True" ExtenderControlID="" TargetControlID="imgDependencia2" BehaviorID="popup3"
                                    PopupControlID="pnlDependencia2" OffsetY="-200">
                                </asp:PopupControlExtender>
                                <asp:Panel ID="pnlDependencia2" runat="server" CssClass="popup" Width="400px" Style="display: none">
                                    <table width="100%" border="1" cellspacing="0" cellpadding="2" bordercolor="White">
                                        <tr align="right" bgcolor="#333399">
                                            <td>
                                                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Imagenes/Icons/dialog-close2.png"
                                                    OnClientClick="$find('popup3').hidePopup(); return false;" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TreeView ID="TreeView2" ExpandDepth="3" runat="server" Font-Names="Calibri"
                                                    Font-Size="Small" LineImagesFolder="~/TreeLineImages" ForeColor="Black" ShowLines="True"
                                                    AutoGenerateDataBindings="False" OnSelectedNodeChanged="TreeView2_SelectedNodeChanged">
                                                    <SelectedNodeStyle BackColor="Silver" BorderColor="#66CCFF" BorderStyle="Inset" />
                                                </asp:TreeView>
                                            </td>
                                        </tr>
                                        <tr align="center">
                                            <td>
                                                <asp:Button ID="Button2" runat="server" Text="Aceptar" CssClass="Apariencia" OnClientClick="$find('popup3').hidePopup(); return false;" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ValidationGroup="updateLista"
                                    ControlToValidate="TextBox15" Display="Dynamic" ForeColor="Red" InitialValue="">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr align="left">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label10" runat="server" Text="Usuario:" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox7" runat="server" Width="156px" Font-Names="Calibri" Font-Size="Small"
                                    Enabled="false"></asp:TextBox>
                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ValidationGroup="updateLista"
                                    ControlToValidate="TextBox2" Display="Dynamic" ForeColor="Red" InitialValue="">*</asp:RequiredFieldValidator>--%>
                            </td>
                        </tr>
                        <tr align="left">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label9" runat="server" Text="Fecha Registro:" Font-Names="Calibri"
                                    Font-Size="Small"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox6" runat="server" Width="155px" Font-Names="Calibri" Font-Size="Small"
                                    Enabled="false"></asp:TextBox>
                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ValidationGroup="updateLista"
                                    ControlToValidate="TextBox6" Display="Dynamic" ForeColor="Red" InitialValue="">*</asp:RequiredFieldValidator>--%>
                            </td>
                        </tr>
                        <tr align="center">
                            <td colspan="2">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:ImageButton ID="BtnModificaPlan" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                ToolTip="Guardar" OnClick="BtnModificaPlan_Click" ValidationGroup="updateLista"
                                                Style="height: 20px;" />
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="BtnCancelaModPlan" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                                ToolTip="Cancelar" OnClick="BtnCancelaModPlan_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table id="TbAdicionarVision" runat="server" visible="false" align="center">
                        <tr align="center" bgcolor="#333399">
                            <td colspan="2">
                                <asp:Label ID="Label1" runat="server" Text="Adicionar Plan de Acción" Font-Bold="False"
                                    Font-Names="Calibri" Font-Size="medium" ForeColor="White"></asp:Label>
                            </td>
                        </tr>
                        <tr align="left">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label2" runat="server" Text="Código:" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox19" runat="server" Enabled="false" Font-Names="Calibri" Font-Size="Small"
                                    Width="55px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr align="left">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label4" runat="server" Text="Descipción:" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox1" runat="server" Width="404px" Font-Names="Calibri" Font-Size="Small"
                                    Style="margin-left: 0px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="addOBJ"
                                    ControlToValidate="TextBox1" Display="Dynamic" ForeColor="Red" InitialValue="">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr align="left">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label44" runat="server" Text="Fecha inicio:" Font-Names="Calibri"
                                    Font-Size="Small"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox16" runat="server" Font-Names="Calibri" Font-Size="Small"
                                    Width="96px"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="true" TargetControlID="TextBox16"
                                    Format="yyyy-MM-dd">
                                </asp:CalendarExtender>
                                <asp:CompareValidator ID="CompareValidator2" Display="Dynamic" ForeColor="Red" Font-Names="Calibri"
                                    runat="server" ControlToValidate="TextBox16" ControlToCompare="TextBox12" Font-Size="Small"
                                    ErrorMessage="Fecha Inválida. Fecha inicio no puede ser menor a Fecha inicio del Plan Estratégico"
                                    Type="Date" Operator="GreaterThanEqual" ValidationGroup="updateLista"> </asp:CompareValidator>
                                <asp:CompareValidator ID="CompareValidator3" Display="Dynamic" ForeColor="Red" Font-Names="Calibri"
                                    runat="server" ControlToValidate="TextBox16" ControlToCompare="TextBox11" Font-Size="Small"
                                    ErrorMessage="Fecha Inválida. Fecha inicio no puede ser mayor a Fecha fin del Plan Estratégico"
                                    Type="Date" Operator="LessThanEqual" ValidationGroup="updateLista"> </asp:CompareValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ValidationGroup="addOBJ"
                                    ControlToValidate="TextBox16" Display="Dynamic" ForeColor="Red" InitialValue="">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr align="left">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label45" runat="server" Text="Fecha fin:" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox17" runat="server" Font-Names="Calibri" Font-Size="Small"
                                    Width="96px"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="true" TargetControlID="TextBox17"
                                    Format="yyyy-MM-dd">
                                </asp:CalendarExtender>
                                <asp:CompareValidator ID="CompareValidator4" Display="Dynamic" ForeColor="Red" Font-Names="Calibri"
                                    runat="server" ControlToValidate="TextBox17" ControlToCompare="TextBox16" Font-Size="Small"
                                    ErrorMessage="Fecha Inválida. Fecha fin no puede ser menor a Fecha inicio" Type="Date"
                                    Operator="GreaterThanEqual" ValidationGroup="updateLista"> </asp:CompareValidator>
                                <asp:CompareValidator ID="CompareValidator8" Display="Dynamic" ForeColor="Red" Font-Names="Calibri"
                                    runat="server" ControlToValidate="TextBox17" ControlToCompare="TextBox11" Font-Size="Small"
                                    ErrorMessage="Fecha Inválida. Fecha fin no puede ser mayor a Fecha fin del Plan Estratégico"
                                    Type="Date" Operator="LessThanEqual" ValidationGroup="updateLista"> </asp:CompareValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ValidationGroup="addOBJ"
                                    ControlToValidate="TextBox17" Display="Dynamic" ForeColor="Red" InitialValue="">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr align="left">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label46" runat="server" Text="Responsable:" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox18" runat="server" Width="360px" Font-Names="Calibri" Font-Size="Small"
                                    Enabled="false"></asp:TextBox>
                                <asp:Label ID="Label3" runat="server" Text="" Font-Names="Calibri" Visible="false"
                                    Font-Size="Small"></asp:Label>
                                <asp:ImageButton ID="imgDependencia1" runat="server" ImageUrl="~/Imagenes/Icons/Organization-Chart.png"
                                    OnClientClick="return false;" ToolTip="Responsable" />
                                <asp:PopupControlExtender ID="popupDependencia3" runat="server" DynamicServicePath=""
                                    Enabled="True" ExtenderControlID="" TargetControlID="imgDependencia1" BehaviorID="popup3"
                                    PopupControlID="pnlDependencia3" OffsetY="-200">
                                </asp:PopupControlExtender>
                                <asp:Panel ID="pnlDependencia3" runat="server" CssClass="popup" Width="400px" Style="display: none">
                                    <table width="100%" border="1" cellspacing="0" cellpadding="2" bordercolor="White">
                                        <tr align="right" bgcolor="#333399">
                                            <td>
                                                <asp:ImageButton ID="btnClosepp3" runat="server" ImageUrl="~/Imagenes/Icons/dialog-close2.png"
                                                    OnClientClick="$find('popup3').hidePopup(); return false;" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TreeView ID="TreeView3" ExpandDepth="3" runat="server" Font-Names="Calibri"
                                                    Font-Size="Small" LineImagesFolder="~/TreeLineImages" ForeColor="Black" ShowLines="True"
                                                    AutoGenerateDataBindings="False" OnSelectedNodeChanged="TreeView3_SelectedNodeChanged">
                                                    <SelectedNodeStyle BackColor="Silver" BorderColor="#66CCFF" BorderStyle="Inset" />
                                                </asp:TreeView>
                                            </td>
                                        </tr>
                                        <tr align="center">
                                            <td>
                                                <asp:Button ID="BtnOk3" runat="server" Text="Aceptar" CssClass="Apariencia" OnClientClick="$find('popup3').hidePopup(); return false;" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ValidationGroup="addOBJ"
                                    ControlToValidate="TextBox18" Display="Dynamic" ForeColor="Red" InitialValue="">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr align="left">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label16" runat="server" Text="Usuario:" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox8" runat="server" Width="156px" Font-Names="Calibri" Font-Size="Small"
                                    Enabled="false"></asp:TextBox>
                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="addOBJ"
                                    ControlToValidate="TextBox8" Display="Dynamic" ForeColor="Red" InitialValue="">*</asp:RequiredFieldValidator>--%>
                            </td>
                        </tr>
                        <tr align="left">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label18" runat="server" Text="Fecha Registro:" Font-Names="Calibri"
                                    Font-Size="Small"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox9" runat="server" Width="155px" Font-Names="Calibri" Font-Size="Small"
                                    Enabled="false"></asp:TextBox>
                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="addOBJ"
                                    ControlToValidate="TextBox9" Display="Dynamic" ForeColor="Red" InitialValue="">*</asp:RequiredFieldValidator>--%>
                            </td>
                        </tr>
                        <tr align="center">
                            <td colspan="2">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:ImageButton ID="BtnGuardaPlan" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                ToolTip="Guardar" OnClick="BtnGuardaPlan_Click" ValidationGroup="addOBJ" Style="height: 20px;
                                                width: 20px;" />
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="BtnCancelaPlan" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                                ToolTip="Cancelar" OnClick="BtnCancelaPlan_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table id="TbIndicadores" runat="server" align="center" visible="false">
                        <caption>
                            <br />
                            <br />
                            <tr align="center" bgcolor="#333399">
                                <td>
                                    <asp:Label ID="Label6" runat="server" Font-Bold="False" Font-Names="Calibri" Font-Size="Small"
                                        ForeColor="White" Text="Indicadores Asociados al Plan de Acción"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" BorderStyle="Solid"
                                        CellPadding="4" Font-Names="Calibri" Font-Size="Small" ForeColor="#333333" GridLines="Vertical"
                                        HeaderStyle-CssClass="gridViewHeader" HorizontalAlign="Center" OnRowCommand="GridView3_RowCommand"
                                        OnSelectedIndexChanged="GridView3_SelectedIndexChanged" ShowHeaderWhenEmpty="True">
                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                        <Columns>
                                            <asp:BoundField DataField="IdIndicador" HeaderText="id" Visible="false" />
                                            <asp:BoundField DataField="CodigoIndicador" HeaderText="Código" />
                                            <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                                            <asp:BoundField DataField="Periodicidad" HeaderText="Periodicidad" />
                                            <asp:BoundField DataField="Usuario" HeaderText="Usuario" />
                                            <asp:BoundField DataField="FechaRegistro" HeaderText="Fecha Registro" Visible="False" />
                                            <asp:ButtonField ButtonType="Image" CommandName="Desasociar" HeaderText="Desasociar"
                                                ImageUrl="~/Imagenes/Icons/delete.png" Text="Desasociar">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:ButtonField>
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
                            <tr align="center">
                                <td>
                                    <br />
                                </td>
                            </tr>
                            <tr align="center" bgcolor="#333399">
                                <td>
                                    <asp:Label ID="Label27" runat="server" Font-Bold="False" Font-Names="Calibri" Font-Size="Small"
                                        ForeColor="White" Text="Indicadores Activos sin Asociar"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" BorderStyle="Solid"
                                        CellPadding="4" Font-Names="Calibri" Font-Size="Small" ForeColor="#333333" GridLines="Vertical"
                                        HeaderStyle-CssClass="gridViewHeader" HorizontalAlign="Center" OnRowCommand="GridView2_RowCommand"
                                        OnSelectedIndexChanged="GridView2_SelectedIndexChanged" ShowHeaderWhenEmpty="True">
                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                        <Columns>
                                            <asp:BoundField DataField="IdIndicador" HeaderText="id" Visible="false" />
                                            <asp:BoundField DataField="CodigoIndicador" HeaderText="Código" />
                                            <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                                            <asp:BoundField DataField="Periodicidad" HeaderText="Periodicidad" />
                                            <asp:BoundField DataField="Usuario" HeaderText="Usuario" />
                                            <asp:BoundField DataField="FechaRegistro" HeaderText="Fencha Registro" Visible="False" />
                                            <asp:ButtonField ButtonType="Image" CommandName="SelecIndicador" HeaderText="Asociar"
                                                ImageUrl="~/Imagenes/Icons/select.png" Text="SelecIndicador">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:ButtonField>
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
                            <tr align="center">
                                <td>
                                    <br />
                                    <asp:ImageButton ID="GuardaAsoIndicadores" runat="server" ImageUrl="~/Imagenes/Icons/backhome.png"
                                        OnClick="GuardaAsoIndicadores_Click" Style="height: 20px;" ToolTip="Cerrar" />
                                    <br />
                                </td>
                            </tr>
                        </caption>
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
            BorderWidth="1px" BackColor="White" BorderStyle="Solid">
            <table width="100%">
                <tr class="topHandle" style="background-color: #333399">
                    <td colspan="2" align="center" runat="server" id="tdCaption">
                        &nbsp;
                        <asp:Label ID="lblCaption" runat="server" Text="Atención" ForeColor="White" Font-Names="Calibri"
                            Font-Size="Small"></asp:Label><br />
                    </td>
                </tr>
                <tr>
                    <td style="width: 60px" valign="middle" align="center">
                        <asp:Image ID="imgInfo" runat="server" ImageUrl="~/Imagenes/Icons/icontexto-webdev-ok.png" />
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
        <asp:ModalPopupExtender ID="mpeMsgBox1" runat="server" TargetControlID="btndummy1"
            PopupControlID="pnlMsgBox1" OkControlID="btnAceptar1" BackgroundCssClass="FondoAplicacion"
            Enabled="True" DropShadow="true">
        </asp:ModalPopupExtender>
        <asp:Button ID="btndummy1" runat="server" Text="Button1" Style="display: none" />
        <asp:Panel ID="pnlMsgBox1" runat="server" Width="400px" Style="display: none;" BorderColor="#575757"
            BorderWidth="1px" BackColor="White" BorderStyle="Solid">
            <table width="100%">
                <tr class="topHandle" style="background-color: #333399">
                    <td colspan="2" align="center" runat="server" id="td1">
                        &nbsp;
                        <asp:Label ID="Label13" runat="server" Text="Atención" ForeColor="White" Font-Names="Calibri"
                            Font-Size="Small"></asp:Label><br />
                    </td>
                </tr>
                <tr>
                    <td style="width: 60px" valign="middle" align="center">
                        <asp:Image ID="imgInfo1" runat="server" ImageUrl="~/Imagenes/Icons/Alerta.png" />
                    </td>
                    <td valign="middle" align="left">
                        <asp:Label ID="lblMsgBox1" runat="server" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="right" colspan="2">
                        <asp:Button ID="btnAceptar1" runat="server" Text="Ok" Font-Names="Calibri" Font-Size="Small" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>
