<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Gestion.ascx.cs" Inherits="ListasSarlaft.UserControls.Gestion.Gestion" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControls/Sitio/OkMessageBox.ascx" TagPrefix="uc" TagName="OkMessageBox" %>
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
        height: 74px;
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
            <tr align="center" bgcolor="#333399">
                <td>
                    <asp:Label ID="Label7" runat="server" ForeColor="White" Text="Gestión Planes de Acción"
                        Font-Bold="False" Font-Names="Calibri" Font-Size="Large"></asp:Label>
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
                                    OnSelectedIndexChanged="GridViewPlanEstratagico_SelectedIndexChanged">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:BoundField DataField="IdPlan" HeaderText="Id" Visible="false" />
                                        <asp:BoundField DataField="CodigoPlan" HeaderText="Codigo" />
                                        <asp:BoundField DataField="Nombre" HeaderText="Plan Estratégico" />
                                        <asp:ButtonField ButtonType="Image" CommandName="Filtrar" HeaderText="Acción" ImageUrl="~/Imagenes/Icons/select.png"
                                            Text="Filtrar">
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
                                    ForeColor="White"></asp:Label>
                            </td>
                            <td colspan="3">
                                <asp:Label ID="LabelCodigoPlan" runat="server" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                        </tr>
                        <tr align="left">
                            <td bgcolor="#5D7B9D">
                                <asp:Label ID="Label22" runat="server" Text="Nombre:" Font-Names="Calibri" Font-Size="Small"
                                    ForeColor="White"></asp:Label>
                            </td>
                            <td colspan="3">
                                <asp:Label ID="LabelNombrePlan" runat="server" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                        </tr>
                        <tr align="left">
                            <td bgcolor="#5D7B9D">
                                <asp:Label ID="Label21" runat="server" Text="Fecha inicio:" Font-Names="Calibri"
                                    Font-Size="Small" Visible="true" ForeColor="White"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox12" runat="server" Enabled="false" Font-Names="Calibri" Font-Size="Small"
                                    Width="89px" Visible="true"></asp:TextBox>
                            </td>
                            <td bgcolor="#5D7B9D">
                                <asp:Label ID="Label26" runat="server" Text="Fecha fin:" Font-Names="Calibri" Font-Size="Small"
                                    Visible="true" ForeColor="White"></asp:Label>
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
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                    <table id="FiltroOBJ" runat="server" align="center" visible="true">
                        <tr>
                            <td>
                                <asp:GridView ID="GridViewOBJ" runat="server" AutoGenerateColumns="False" BorderStyle="Solid"
                                    CellPadding="4" Font-Names="Calibri" Font-Size="Small" ForeColor="#333333" GridLines="Vertical"
                                    HeaderStyle-CssClass="gridViewHeader" HorizontalAlign="Center" OnRowCommand="GridViewOBJ_RowCommand"
                                    ShowHeaderWhenEmpty="True">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:BoundField DataField="IdObjetivo" HeaderText="Id" Visible="false" />
                                        <asp:BoundField DataField="CodigoObjetivo" HeaderText="Código" />
                                        <asp:BoundField DataField="Descripcion" HeaderText="Objetivo Estratégico" />
                                        <asp:ButtonField ButtonType="Image" CommandName="FiltrarOBJ" HeaderText="Acción"
                                            ImageUrl="~/Imagenes/Icons/select.png" Text="FiltrarOBJ">
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
                            <td colspan="4" bgcolor="#5D7B9D">
                                <asp:Label ID="Label8" runat="server" Text="Objetivo Estratégico" Font-Bold="True"
                                    Font-Names="Calibri" Font-Size="small" ForeColor="White"></asp:Label>
                            </td>
                        </tr>
                        <tr align="left">
                            <td>
                                <asp:Label ID="Label11" runat="server" Text="Id:" Font-Names="Calibri" Font-Size="Small"
                                    Visible="false"></asp:Label>
                            </td>
                            <td colspan="3">
                                <asp:Label ID="LabelIdOBJ" runat="server" Font-Names="Calibri" Font-Size="Small"
                                    Visible="false"></asp:Label>
                            </td>
                        </tr>
                        <tr align="left">
                            <td bgcolor="#5D7B9D">
                                <asp:Label ID="Label17" runat="server" Text="Código:" Font-Names="Calibri" Font-Size="Small"
                                    ForeColor="White"></asp:Label>
                            </td>
                            <td colspan="3">
                                <asp:Label ID="LabelCodigoOBJ" runat="server" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                        </tr>
                        <tr align="left">
                            <td bgcolor="#5D7B9D">
                                <asp:Label ID="Label25" runat="server" Text="Descripción:" Font-Names="Calibri" Font-Size="Small"
                                    ForeColor="White"></asp:Label>
                            </td>
                            <td colspan="3">
                                <asp:Label ID="LabelDescOBJ" runat="server" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" align="center">
                                <asp:Button ID="VerObjetivos" runat="server" Font-Bold="False" Font-Names="Calibri"
                                    Font-Size="small" Text="Objetivos..." OnClick="VerObjetivos_Click" Width="100px"
                                    ToolTip="Ver Objetivos" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
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
                                            ImageUrl="~/Imagenes/Icons/select.png" Text="Estrategia">
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
                            <td colspan="4" bgcolor="#5D7B9D">
                                <asp:Label ID="Label47" runat="server" Text="Estrategia" Font-Bold="True" Font-Names="Calibri"
                                    Font-Size="small" ForeColor="White"></asp:Label>
                            </td>
                        </tr>
                        <tr align="left">
                            <td>
                                <asp:Label ID="Label48" runat="server" Text="Id:" Font-Names="Calibri" Font-Size="Small"
                                    Visible="false"></asp:Label>
                            </td>
                            <td colspan="3">
                                <asp:Label ID="Label49" runat="server" Font-Names="Calibri" Font-Size="Small" Visible="false"></asp:Label>
                            </td>
                        </tr>
                        <tr align="left">
                            <td bgcolor="#5D7B9D">
                                <asp:Label ID="Label50" runat="server" Text="Código:" Font-Names="Calibri" Font-Size="Small"
                                    ForeColor="White"></asp:Label>
                            </td>
                            <td colspan="3">
                                <asp:Label ID="Label51" runat="server" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                        </tr>
                        <tr align="left">
                            <td bgcolor="#5D7B9D">
                                <asp:Label ID="Label52" runat="server" Text="Descripción:" Font-Names="Calibri" Font-Size="Small"
                                    ForeColor="White"></asp:Label>
                            </td>
                            <td colspan="3">
                                <asp:Label ID="Label53" runat="server" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" align="center">
                                <asp:Button ID="BtnVerEstrategias" runat="server" Font-Bold="False" Font-Names="Calibri"
                                    Font-Size="small" Text="Estrategias..." OnClick="VerEstrategias_Click" Width="100px"
                                    ToolTip="Ver Estrategias..." />
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
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
                                    HeaderStyle-CssClass="gridViewHeader" HorizontalAlign="Center" OnRowCommand="GridView1_RowCommand"
                                    ShowHeaderWhenEmpty="True">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:BoundField DataField="CodigoPlanAccion" HeaderText="Código" />
                                        <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                                        <asp:ButtonField ButtonType="Image" CommandName="Indicadores" HeaderText="Indicadores"
                                            ImageUrl="~/Imagenes/Icons/Literal.png" Text="Indicadores" HeaderStyle-HorizontalAlign="Center">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:ButtonField>
                                        <asp:ButtonField ButtonType="Image" CommandName="FiltrarPA" HeaderText="Gestiones"
                                            ImageUrl="~/Imagenes/Icons/select.png" Text="Gestiones">
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
                                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                </asp:GridView>
                            </td>
                        </tr>
                        </caption>
                    </table>
                    <table id="TbFiltroPlanAccion" runat="server" align="center" visible="false">
                        <tr align="center" bgcolor="#5D7B9D">
                            <td colspan="4" bgcolor="#5D7B9D">
                                <asp:Label ID="Label14" runat="server" Text="Plan de Acción" Font-Bold="True" Font-Names="Calibri"
                                    Font-Size="small" ForeColor="White"></asp:Label>
                            </td>
                        </tr>
                        <tr align="left">
                            <td>
                                <asp:Label ID="Label20" runat="server" Text="Id:" Font-Names="Calibri" Font-Size="Small"
                                    Visible="false"></asp:Label>
                            </td>
                            <td colspan="3">
                                <asp:Label ID="LabelIdPlanAccion" runat="server" Font-Names="Calibri" Font-Size="Small"
                                    Visible="false"></asp:Label>
                                <asp:Label ID="Label60" runat="server" Font-Names="Calibri" Font-Size="Small" Visible="false"></asp:Label>
                                <asp:Label ID="Label61" runat="server" Font-Names="Calibri" Font-Size="Small" Visible="false"></asp:Label>
                            </td>
                        </tr>
                        <tr align="left">
                            <td bgcolor="#5D7B9D">
                                <asp:Label ID="Label28" runat="server" Text="Código:" Font-Names="Calibri" Font-Size="Small"
                                    ForeColor="White"></asp:Label>
                            </td>
                            <td colspan="3">
                                <asp:Label ID="Label29" runat="server" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                        </tr>
                        <tr align="left">
                            <td bgcolor="#5D7B9D">
                                <asp:Label ID="Label30" runat="server" Text="Descripción:" Font-Names="Calibri" Font-Size="Small"
                                    ForeColor="White"></asp:Label>
                            </td>
                            <td colspan="3">
                                <asp:Label ID="Label31" runat="server" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                        </tr>
                        <tr align="left">
                            <td bgcolor="#5D7B9D">
                                <asp:Label ID="Label55" runat="server" Text="Fecha inicio:" Font-Names="Calibri"
                                    Font-Size="Small" Visible="true" ForeColor="White"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox19" runat="server" Enabled="false" Font-Names="Calibri" Font-Size="Small"
                                    Width="89px" Visible="true"></asp:TextBox>
                            </td>
                            <td bgcolor="#5D7B9D">
                                <asp:Label ID="Label56" runat="server" Text="Fecha fin:" Font-Names="Calibri" Font-Size="Small"
                                    Visible="true" ForeColor="White"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox20" runat="server" Enabled="false" Font-Names="Calibri" Font-Size="Small"
                                    Width="89px" Visible="true"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" align="center">
                                <asp:Button ID="VerPlanAccion" runat="server" Font-Bold="False" Font-Names="Calibri"
                                    Font-Size="small" Text="Planes de Acción" OnClick="VerPlanAccion_Click" Width="120px"
                                    ToolTip="Ver Planes de Acción..." />
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                    <table id="TbGestionesPA" runat="server" align="center" visible="false">
                        <tr align="center" bgcolor="#5D7B9D">
                            <td bgcolor="#5D7B9D">
                                <asp:Label ID="Label57" runat="server" Text="Gestiones" Font-Bold="False" Font-Names="Calibri"
                                    Font-Size="small" Style="font-weight: 700; color: #FFFFFF;"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="GridViewGestiones" runat="server" AutoGenerateColumns="False" BorderStyle="Solid"
                                    CellPadding="4" Font-Names="Calibri" Font-Size="Small" ForeColor="#333333" GridLines="Vertical"
                                    HeaderStyle-CssClass="gridViewHeader" HorizontalAlign="Center" OnRowCommand="GridViewGestiones_RowCommand"
                                    ShowHeaderWhenEmpty="True">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:BoundField DataField="IdGestion" HeaderText="ID" Visible="false" />
                                        <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                                        <asp:BoundField DataField="Usuario" HeaderText="Usuario" />
                                        <asp:BoundField DataField="FechaRegistro" HeaderText="Fecha Registro" />
                                        <asp:ButtonField ButtonType="Image" CommandName="ModificarGestion" HeaderText="Acción"
                                            ImageUrl="~/Imagenes/Icons/edit.png" Text="Modificar">
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
                                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr align="center">
                            <td>
                                <asp:ImageButton ID="BtnAddGestion" runat="server" ImageUrl="~/Imagenes/Icons/Add.png"
                                    OnClick="BtnAddGestion_Click" ToolTip="Agregar" />
                            </td>
                        </tr>
                        </caption>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table id="TbModificarGestion" runat="server" visible="false" align="center">
                        <tr align="center" bgcolor="#333399">
                            <td colspan="2">
                                <asp:Label ID="Label15" runat="server" Text="Modificar Gestión" Font-Bold="False"
                                    Font-Names="Calibri" Font-Size="medium" ForeColor="White"></asp:Label>
                            </td>
                        </tr>
                        <tr align="left">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label5" runat="server" Text="Descipción de la gestión:" Font-Names="Calibri"
                                    Font-Size="Small"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox2" runat="server" Width="483px" Font-Names="Calibri" Font-Size="Small"
                                    Height="80px" TextMode="MultiLine"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="updateLista"
                                    ControlToValidate="TextBox2" Display="Dynamic" ForeColor="Red" InitialValue="">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr align="left">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label59" runat="server" Text="Estado Plan Acción:" Font-Names="Calibri"
                                    Font-Size="Small"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="DropDownListEstadoPA" runat="server" Font-Names="Calibri" Font-Size="Small"
                                    Height="21px" Width="156px">
                                    <asp:ListItem Value="1">Abierto</asp:ListItem>
                                    <asp:ListItem Value="0">Cerrado</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr align="left">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label10" runat="server" Text="Usuario:" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox7" runat="server" Width="156px" Font-Names="Calibri" Font-Size="Small"
                                    Enabled="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr align="left">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label9" runat="server" Text="Fecha Registro:" Font-Names="Calibri"
                                    Font-Size="Small"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox6" runat="server" Width="155px" Font-Names="Calibri" Font-Size="Small"
                                    Enabled="false" Height="22px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr align="center">
                            <td colspan="2">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:ImageButton ID="BtnModificaPlan" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                ToolTip="Guardar" OnClick="BtnModificaPlan_Click" ValidationGroup="updateLista" />
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
                    <table id="TbAdicionarGestion" runat="server" visible="false" align="center">
                        <tr align="center" bgcolor="#333399">
                            <td colspan="2">
                                <asp:Label ID="Label1" runat="server" Text="Adicionar Gestión" Font-Bold="False"
                                    Font-Names="Calibri" Font-Size="medium" ForeColor="White"></asp:Label>
                            </td>
                        </tr>
                        <tr align="left">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label4" runat="server" Text="Descipción de la gestión:" Font-Names="Calibri"
                                    Font-Size="Small"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox1" runat="server" Width="483px" Font-Names="Calibri" Font-Size="Small"
                                    Height="80px" TextMode="MultiLine"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="addgestion"
                                    ControlToValidate="TextBox1" Display="Dynamic" ForeColor="Red" InitialValue="">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr align="left">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label58" runat="server" Text="Estado Plan Acción:" Font-Names="Calibri"
                                    Font-Size="Small"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="DropDownListEstadoPA1" runat="server" Font-Names="Calibri"
                                    Font-Size="Small" Height="21px" Width="156px">
                                    <asp:ListItem Selected="True" Value="S">Abierto</asp:ListItem>
                                    <asp:ListItem Value="N">Cerrado</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr align="left">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label16" runat="server" Text="Usuario:" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox8" runat="server" Width="95px" Font-Names="Calibri" Font-Size="Small"
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
                                <asp:TextBox ID="TextBox9" runat="server" Width="95px" Font-Names="Calibri" Font-Size="Small"
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
                                            <asp:ImageButton ID="BtnGuardaGestion" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                ToolTip="Guardar" OnClick="BtnGuardaGestion_Click" ValidationGroup="addgestion"
                                                Style="height: 20px; width: 20px;" />
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
                    <table id="TbArchivos" runat="server" visible="false" align="center">
                        <tr align="center" bgcolor="#333399">
                            <td colspan="3">
                                <asp:Label ID="Label64" runat="server" Text="Datos Adjuntos" Font-Bold="False" Font-Names="Calibri"
                                    Font-Size="medium" ForeColor="White"></asp:Label>
                            </td>
                        </tr>
                        <tr align="center" id="TrFileUpload1" runat="server">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label65" runat="server" Text="Adjuntar documento .pdf:" Font-Names="Calibri"
                                    Font-Size="Small"></asp:Label>
                            </td>
                            <td>
                                <asp:FileUpload ID="FileUpload1" runat="server" Font-Names="Calibri" Font-Size="Small"
                                    Width="248px" />
                            </td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <asp:ImageButton ID="ImageButton7" runat="server" ImageUrl="~/Imagenes/Icons/Add.png"
                                            ToolTip="Adjuntar" OnClick="ImageButton7_Click" />
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="ImageButton7" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr align="center">
                            <td colspan="3">
                                <asp:GridView ID="GridViewArchivos" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                    ForeColor="#333333" GridLines="Vertical" ShowHeaderWhenEmpty="True" BorderStyle="Solid"
                                    HorizontalAlign="Center" Font-Names="Calibri" Font-Size="Small" OnRowCommand="GridViewArchivos_RowCommand">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:BoundField DataField="IdArchivo" HeaderText="IdArchivo" Visible="False" />
                                        <asp:BoundField DataField="IdSeguimiento" HeaderText="IdSeguimiento" Visible="False" />
                                        <asp:BoundField DataField="IdPlanAccion" HeaderText="IdPlanAccion" Visible="False" />
                                        <asp:BoundField DataField="NombreArchivo" HeaderText="NombreArchivo" />
                                        <asp:BoundField DataField="Usuario" HeaderText="Usuario" />
                                        <asp:BoundField DataField="FechaRegistro" HeaderText="FechaRegistro" />
                                        <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Icons/downloads.png" Text="Descargar"
                                            CommandName="Descargar" />
                                    </Columns>
                                    <EditRowStyle BackColor="#999999" />
                                    <FooterStyle BackColor="White" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" CssClass="gridViewHeader" />
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
                    <table id="TbIndicadores" runat="server" align="center" visible="false">
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
                                        <asp:BoundField DataField="FechaRegistro" HeaderText="Fecha Registro" />
                                        <asp:ButtonField ButtonType="Image" CommandName="SelecVariables" HeaderText="Variables"
                                            ImageUrl="~/Imagenes/Icons/select.png" Text="Variables" FooterStyle-HorizontalAlign="Center">
                                            <FooterStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:ButtonField>
                                        <asp:ButtonField ButtonType="Image" CommandName="SelecPAI" HeaderText="Planes Acción"
                                            ImageUrl="~/Imagenes/Icons/engranaje.png" Text="Planes de Acción" FooterStyle-HorizontalAlign="Center">
                                            <FooterStyle HorizontalAlign="Center" />
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
                                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr align="center">
                            <td>
                                <asp:ImageButton ID="CerrarIndicadores" runat="server" ImageUrl="~/Imagenes/Icons/backhome.png"
                                    OnClick="CerrarIndicadores_Click" Style="height: 20px;" ToolTip="Cerrar" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table id="TbIndicadorSeleccionado" runat="server" align="center" visible="false">
                                    <tr align="left">
                                        <td bgcolor="#5D7B9D" align="center">
                                            <asp:Label ID="Label40" runat="server" Text="Código" Font-Names="Calibri" Font-Size="Small"
                                                ForeColor="White"></asp:Label>
                                        </td>
                                        <td bgcolor="#5D7B9D" align="center">
                                            <asp:Label ID="Label41" runat="server" Text="Indicador" Font-Names="Calibri" Font-Size="Small"
                                                ForeColor="White"></asp:Label>
                                        </td>
                                        <td bgcolor="#5D7B9D" align="center">
                                            <asp:Label ID="Label44" runat="server" Text="Periodicidad" Font-Names="Calibri" Font-Size="Small"
                                                ForeColor="White"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr align="left">
                                        <td bgcolor="#F7F6F3" align="center">
                                            <asp:Label ID="Label42" runat="server" Text="" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                        </td>
                                        <td bgcolor="#F7F6F3" align="center">
                                            <asp:Label ID="Label43" runat="server" Text="" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                        </td>
                                        <td bgcolor="#F7F6F3" align="center">
                                            <asp:Label ID="Label45" runat="server" Text="" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <table id="TbFechaPeriodo" runat="server" visible="false" align="center">
                                    <tr align="center" bgcolor="#333399">
                                        <td colspan="2">
                                            <asp:Label ID="Label46" runat="server" Font-Bold="False" Font-Names="Calibri" Font-Size="Small"
                                                ForeColor="White" Text="Fecha del Periodo"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr id="Trdia" runat="server" align="center">
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label39" runat="server" Text="Dia:" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextBox5" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                Width="100px"></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="TextBox5"
                                                Format="yyyy-MM-dd"></asp:CalendarExtender>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorDia" runat="server" ControlToValidate="TextBox5"
                                                Display="Dynamic" ForeColor="Red" InitialValue="">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr id="Trsemana" runat="server">
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label27" runat="server" Text="Semana:" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="DropDownListSemana" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                Height="21px" Width="100px">
                                                <asp:ListItem Selected="True" Value="---">---</asp:ListItem>
                                                <asp:ListItem Value="1">Semana 1</asp:ListItem>
                                                <asp:ListItem Value="8">Semana 2</asp:ListItem>
                                                <asp:ListItem Value="15">Semana 3</asp:ListItem>
                                                <asp:ListItem Value="29">Semana 4</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:CompareValidator ID="CompareValidatorSemana" runat="server" ForeColor="Red"
                                                ControlToValidate="DropDownListSemana" ValueToCompare="---" Operator="NotEqual">*</asp:CompareValidator>
                                        </td>
                                    </tr>
                                    <tr id="Trquincena" runat="server">
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label34" runat="server" Text="Quincena:" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="DropDownListQuincena" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                Height="21px" Width="100px">
                                                <asp:ListItem Selected="True" Value="---">---</asp:ListItem>
                                                <asp:ListItem Value="1">Quincena 1</asp:ListItem>
                                                <asp:ListItem Value="15">Quincena 2</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:CompareValidator ID="CompareValidatorQuincena" runat="server" ForeColor="Red"
                                                ControlToValidate="DropDownListSemana" ValueToCompare="---" Operator="NotEqual">*</asp:CompareValidator>
                                        </td>
                                    </tr>
                                    <tr id="Trmes" runat="server">
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label36" runat="server" Text="Mes:" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="DropDownListMes" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                Height="21px" Width="100px">
                                                <asp:ListItem Selected="True" Value="---">---</asp:ListItem>
                                                <asp:ListItem Value="1">Enero</asp:ListItem>
                                                <asp:ListItem Value="2">Febrero</asp:ListItem>
                                                <asp:ListItem Value="3">Marzo</asp:ListItem>
                                                <asp:ListItem Value="4">Abril</asp:ListItem>
                                                <asp:ListItem Value="5">Mayo</asp:ListItem>
                                                <asp:ListItem Value="6">Junio</asp:ListItem>
                                                <asp:ListItem Value="7">Julio</asp:ListItem>
                                                <asp:ListItem Value="8">Agosto</asp:ListItem>
                                                <asp:ListItem Value="9">Septiembre</asp:ListItem>
                                                <asp:ListItem Value="10">Octubre</asp:ListItem>
                                                <asp:ListItem Value="11">Noviembre</asp:ListItem>
                                                <asp:ListItem Value="12">Diciembre</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:CompareValidator ID="CompareValidatorMes" runat="server" ForeColor="Red" ControlToValidate="DropDownListSemana"
                                                ValueToCompare="---" Operator="NotEqual">*</asp:CompareValidator>
                                        </td>
                                    </tr>
                                    <tr id="Trbimiestre" runat="server">
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label35" runat="server" Text="Bimestre:" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="DropDownListBimestre" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                Height="21px" Width="100px">
                                                <asp:ListItem Selected="True" Value="---">---</asp:ListItem>
                                                <asp:ListItem Value="1">Ene - Feb</asp:ListItem>
                                                <asp:ListItem Value="3">Mar - Abr</asp:ListItem>
                                                <asp:ListItem Value="5">May - Jun</asp:ListItem>
                                                <asp:ListItem Value="7">Jul - Ago</asp:ListItem>
                                                <asp:ListItem Value="9">Sep - Oct</asp:ListItem>
                                                <asp:ListItem Value="11">Nov - Dic</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:CompareValidator ID="CompareValidatorBimestre" runat="server" ForeColor="Red"
                                                ControlToValidate="DropDownListSemana" ValueToCompare="---" Operator="NotEqual">*</asp:CompareValidator>
                                        </td>
                                    </tr>
                                    <tr id="Trtrimestre" runat="server">
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label37" runat="server" Text="Trimestre:" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="DropDownListTrimestre" runat="server" Font-Names="Calibri"
                                                Font-Size="Small" Height="21px" Width="100px">
                                                <asp:ListItem Selected="True" Value="---">---</asp:ListItem>
                                                <asp:ListItem Value="1">Ene - Mar</asp:ListItem>
                                                <asp:ListItem Value="4">Abr - Jun</asp:ListItem>
                                                <asp:ListItem Value="7">Jul - Sep</asp:ListItem>
                                                <asp:ListItem Value="10">Oct - Dic</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:CompareValidator ID="CompareValidatorTrimestre" runat="server" ForeColor="Red"
                                                ControlToValidate="DropDownListSemana" ValueToCompare="---" Operator="NotEqual">*</asp:CompareValidator>
                                        </td>
                                    </tr>
                                    <tr id="Trsemestre" runat="server">
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label38" runat="server" Text="Semestre:" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="DropDownListSemestre" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                Height="21px" Width="100px">
                                                <asp:ListItem Selected="True" Value="---">---</asp:ListItem>
                                                <asp:ListItem Value="1">Ene - Jun</asp:ListItem>
                                                <asp:ListItem Value="7">Jul -  DicJul -  Dic</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:CompareValidator ID="CompareValidatorSemestre" runat="server" ForeColor="Red"
                                                ControlToValidate="DropDownListSemana" ValueToCompare="---" Operator="NotEqual">*</asp:CompareValidator>
                                        </td>
                                    </tr>
                                    <tr id="Trano" runat="server">
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label33" runat="server" Text="Año:" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="DropDownListAno" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                Height="21px" Width="100px">
                                                <asp:ListItem Selected="True" Value="---">---</asp:ListItem>
                                                <%--<asp:ListItem Value="2000">2000</asp:ListItem>
                                                <asp:ListItem Value="2001">2001</asp:ListItem>
                                                <asp:ListItem Value="2002">2002</asp:ListItem>
                                                <asp:ListItem Value="2003">2003</asp:ListItem>
                                                <asp:ListItem Value="2004">2004</asp:ListItem>
                                                <asp:ListItem Value="2005">2005</asp:ListItem>
                                                <asp:ListItem Value="2006">2006</asp:ListItem>
                                                <asp:ListItem Value="2007">2007</asp:ListItem>
                                                <asp:ListItem Value="2008">2008</asp:ListItem>
                                                <asp:ListItem Value="2009">2009</asp:ListItem>--%>
                                                <asp:ListItem Value="2010">2010</asp:ListItem>
                                                <asp:ListItem Value="2011">2011</asp:ListItem>
                                                <asp:ListItem Value="2012">2012</asp:ListItem>
                                                <asp:ListItem Value="2013">2013</asp:ListItem>
                                                <asp:ListItem Value="2014">2014</asp:ListItem>
                                                <asp:ListItem Value="2015">2015</asp:ListItem>
                                                <asp:ListItem Value="2016">2016</asp:ListItem>
                                                <asp:ListItem Value="2017">2017</asp:ListItem>
                                                <asp:ListItem Value="2018">2018</asp:ListItem>
                                                <asp:ListItem Value="2019">2019</asp:ListItem>
                                                <asp:ListItem Value="2020">2020</asp:ListItem>
                                                <%--   <asp:ListItem Value="2021">2021</asp:ListItem>
                                                <asp:ListItem Value="2022">2022</asp:ListItem>
                                                <asp:ListItem Value="2023">2023</asp:ListItem>
                                                <asp:ListItem Value="2024">2024</asp:ListItem>
                                                <asp:ListItem Value="2025">2025</asp:ListItem>
                                                <asp:ListItem Value="2026">2026</asp:ListItem>
                                                <asp:ListItem Value="2027">2027</asp:ListItem>
                                                <asp:ListItem Value="2028">2028</asp:ListItem>
                                                <asp:ListItem Value="2029">2029</asp:ListItem>
                                                <asp:ListItem Value="2030">2030</asp:ListItem>--%>
                                            </asp:DropDownList>
                                            <asp:CompareValidator ID="CompareValidatorAno" runat="server" ForeColor="Red" ControlToValidate="DropDownListSemana"
                                                ValueToCompare="---" Operator="NotEqual">*</asp:CompareValidator>
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
                    <table id="TbVariables" runat="server" align="center" visible="false">
                        <tr align="center" bgcolor="#333399">
                            <td>
                                <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Names="Calibri" Font-Size="Small"
                                    ForeColor="White" Text="Variables creadas para el Indicador"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="GridViewVariables" runat="server" AutoGenerateColumns="False" BorderStyle="Solid"
                                    CellPadding="4" Font-Names="Calibri" Font-Size="Small" ForeColor="#333333" GridLines="Vertical"
                                    HeaderStyle-CssClass="gridViewHeader" HorizontalAlign="Center" OnRowCommand="GridViewVariables_RowCommand"
                                    ShowHeaderWhenEmpty="True" Width="312px">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:BoundField DataField="IdVariable" HeaderText="id" Visible="false" />
                                        <asp:BoundField DataField="Nombre" HeaderText="Variabe" />
                                        <asp:BoundField DataField="Formato" HeaderText="Formato" />
                                        <asp:BoundField DataField="Valor" HeaderText="Valor" />
                                        <asp:ButtonField ButtonType="Image" CommandName="Valores" HeaderText="Asignar Valor"
                                            ImageUrl="~/Imagenes/Icons/select.png" Text="Valores" FooterStyle-HorizontalAlign="Center">
                                            <FooterStyle HorizontalAlign="Center" />
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
                                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr align="center">
                            <td>
                                <asp:ImageButton ID="CerrarVariables" runat="server" ImageUrl="~/Imagenes/Icons/backhome.png"
                                    OnClick="CerrarVariables_Click" ToolTip="Cerrar" Height="20px" />
                            </td>
                        </tr>
                        <tr align="center">
                            <td>
                                <asp:Button ID="BtnNuevoPeriodoHoy" runat="server" Font-Bold="False" Font-Names="Calibri"
                                    Font-Size="small" Text="Actualizar periodo" OnClick="BtnActualizarPeriodo_Click"
                                    Width="130px" ToolTip="Actualizar periodo" />
                                <asp:Label ID="Label63" runat="server" Text=""></asp:Label>
                                <asp:Button ID="BtnNuevoPeriodo" runat="server" Font-Bold="False" Font-Names="Calibri"
                                    Font-Size="small" Text="Iniciar nuevo periodo" OnClick="BtnNuevoPeriodo_Click"
                                    Width="130px" ToolTip="Iniciar nuevo periodo" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table id="TbAsignarValores" runat="server" visible="false" align="center">
                                    <tr align="center" bgcolor="#333399">
                                        <td colspan="4">
                                            <asp:Label ID="Label3" runat="server" Font-Bold="False" Font-Names="Calibri" Font-Size="Small"
                                                ForeColor="White" Text="Valores del Periodo"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label12" runat="server" Text="Variable:" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                        </td>
                                        <td colspan="3">
                                            <asp:TextBox ID="TextBox21" runat="server" Enabled="false" Font-Names="Calibri" Font-Size="Small"
                                                Width="213px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label32" runat="server" Text="Valor:" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextBox3" runat="server" Width="110px" Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="UpdateValorVariable"
                                                ControlToValidate="TextBox3" Display="Dynamic" ForeColor="Red" IniialValue="">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label13" runat="server" Text="Formato:" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextBox4" runat="server" Width="29px" Font-Names="Calibri" Font-Size="Small"
                                                Enabled="false" ReadOnly="True"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr align="center">
                                        <td colspan="4">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:ImageButton ID="BtnAsignarValores" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                            ToolTip="Guardar" OnClick="BtnAsignarValores_Click" ValidationGroup="UpdateValorVariable"
                                                            Style="height: 20px; width: 20px;" />
                                                    </td>
                                                    <td>
                                                        <asp:ImageButton ID="BtnCancelarValores" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                                            ToolTip="Cancelar" OnClick="BtnCancelarValores_Click" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr runat="server" id="TrVerPAI" visible="false">
                <td>
                    <table id="TblPAI" runat="server" align="center" visible="false">
                        <tr align="center" bgcolor="#5D7B9D">
                            <td bgcolor="#5D7B9D">
                                <asp:Label ID="Label66" runat="server" Text="Planes Acción Indicadores" Font-Bold="False"
                                    Font-Names="Calibri" Font-Size="small" Style="font-weight: 700; color: #FFFFFF;"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" BorderStyle="Solid"
                                    CellPadding="4" Font-Names="Calibri" Font-Size="Small" ForeColor="#333333" GridLines="Vertical"
                                    HeaderStyle-CssClass="gridViewHeader" HorizontalAlign="Center" ShowHeaderWhenEmpty="True"
                                    OnRowCommand="GridView2_RowCommand">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:BoundField DataField="CodigoPAI" HeaderText="Código" />
                                        <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                                        <asp:BoundField DataField="Responsable" HeaderText="Responsable" />
                                        <asp:BoundField DataField="Estado" HeaderText="Estado" />
                                        <asp:BoundField DataField="FechaCompromiso" HeaderText="Fecha Compromiso" />
                                        <asp:BoundField DataField="Usuario" HeaderText="Usuario" Visible="False" />
                                        <asp:BoundField DataField="FechaRegistro" HeaderText="Fecha Registro" Visible="False" />
                                        <asp:ButtonField ButtonType="Image" CommandName="ModificarPAI" HeaderText="" ImageUrl="~/Imagenes/Icons/select.png"
                                            Text="Ver Modificar Plan de Acción" FooterStyle-HorizontalAlign="Center">
                                            <FooterStyle HorizontalAlign="Center" />
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
                                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr align="center">
                            <td>
                                <asp:ImageButton ID="BtnAddPAI" runat="server" ImageUrl="~/Imagenes/Icons/Add.png"
                                    ToolTip="Agregar" OnClick="BtnAddPAI_Click" />
                                <asp:ImageButton ID="BtnCancelAddPAI" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                    ToolTip="Cancelar" OnClick="BtnCancelAddPAI_Click" />
                            </td>
                        </tr>
                        </caption>
                    </table>
                </td>
            </tr>
            <tr runat="server" id="TrAddPAI" visible="false">
                <td align="center">
                    <table align="center">
                        <tr align="center" bgcolor="#333399">
                            <td colspan="2">
                                <asp:Label ID="Label67" runat="server" Text="Adicionar Plan de Acción Indicador"
                                    Font-Bold="False" Font-Names="Calibri" Font-Size="medium" ForeColor="White"></asp:Label>
                            </td>
                        </tr>
                        <tr align="left">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label68" runat="server" Text="Código:" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox10" runat="server" ReadOnly="true" Font-Names="Calibri" Font-Size="Small"
                                    Width="55px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr align="left">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label69" runat="server" Text="Descipción:" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox13" runat="server" Width="404px" Font-Names="Calibri" Font-Size="Small"
                                    TextMode="MultiLine" Height="50px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="AddPAI"
                                    ControlToValidate="TextBox13" Display="Dynamic" ForeColor="Red" InitialValue="">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr align="left">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label72" runat="server" Text="Responsable:" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox18" runat="server" Width="360px" Font-Names="Calibri" Font-Size="Small"
                                    ReadOnly="true"></asp:TextBox>
                                <asp:Label ID="Label73" runat="server" Text="" Font-Names="Calibri" Visible="false"
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
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ValidationGroup="AddPAI"
                                    ControlToValidate="TextBox18" Display="Dynamic" ForeColor="Red" InitialValue="">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr align="left">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label71" runat="server" Text="Estado:" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="DropDownList1" runat="server" Font-Names="Calibri" Font-Size="Small"
                                    Height="21px" Width="156px">
                                    <asp:ListItem Selected="True" Value="---">---</asp:ListItem>
                                    <asp:ListItem Value="1">Abierto</asp:ListItem>
                                    <asp:ListItem Value="0">Cerrado</asp:ListItem>
                                </asp:DropDownList>
                                <asp:CompareValidator ID="CompareValidator3" runat="server" ForeColor="Red" ControlToValidate="DropDownList1"
                                    ValueToCompare="---" Operator="NotEqual" ValidationGroup="AddPAI">*</asp:CompareValidator>
                            </td>
                        </tr>
                        <tr align="left">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label74" runat="server" Text="Periodo:" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                            <td>
                                Mes
                                <asp:DropDownList ID="DropDownList2" runat="server" Font-Names="Calibri" Font-Size="Small"
                                    Height="21px" Width="100px">
                                    <asp:ListItem Selected="True" Value="---">---</asp:ListItem>
                                    <asp:ListItem Value="1">Enero</asp:ListItem>
                                    <asp:ListItem Value="2">Febrero</asp:ListItem>
                                    <asp:ListItem Value="3">Marzo</asp:ListItem>
                                    <asp:ListItem Value="4">Abril</asp:ListItem>
                                    <asp:ListItem Value="5">Mayo</asp:ListItem>
                                    <asp:ListItem Value="6">Junio</asp:ListItem>
                                    <asp:ListItem Value="7">Julio</asp:ListItem>
                                    <asp:ListItem Value="8">Agosto</asp:ListItem>
                                    <asp:ListItem Value="9">Septiembre</asp:ListItem>
                                    <asp:ListItem Value="10">Octubre</asp:ListItem>
                                    <asp:ListItem Value="11">Noviembre</asp:ListItem>
                                    <asp:ListItem Value="12">Diciembre</asp:ListItem>
                                </asp:DropDownList>
                                Año
                                <asp:CompareValidator ID="CompareValidator1" runat="server" ForeColor="Red" ControlToValidate="DropDownList2"
                                    ValueToCompare="---" Operator="NotEqual" ValidationGroup="AddPAI">*</asp:CompareValidator>
                                <asp:DropDownList ID="DropDownList3" runat="server" Font-Names="Calibri" Font-Size="Small"
                                    Height="21px" Width="100px">
                                    <asp:ListItem Selected="True" Value="---">---</asp:ListItem>
                                    <asp:ListItem Value="2010">2010</asp:ListItem>
                                    <asp:ListItem Value="2011">2011</asp:ListItem>
                                    <asp:ListItem Value="2012">2012</asp:ListItem>
                                    <asp:ListItem Value="2013">2013</asp:ListItem>
                                    <asp:ListItem Value="2014">2014</asp:ListItem>
                                    <asp:ListItem Value="2015">2015</asp:ListItem>
                                    <asp:ListItem Value="2016">2016</asp:ListItem>
                                    <asp:ListItem Value="2017">2017</asp:ListItem>
                                    <asp:ListItem Value="2018">2018</asp:ListItem>
                                    <asp:ListItem Value="2019">2019</asp:ListItem>
                                    <asp:ListItem Value="2020">2020</asp:ListItem>
                                </asp:DropDownList>
                                <asp:CompareValidator ID="CompareValidator2" runat="server" ForeColor="Red" ControlToValidate="DropDownList3"
                                    ValueToCompare="---" Operator="NotEqual" ValidationGroup="AddPAI">*</asp:CompareValidator>
                            </td>
                        </tr>
                        <tr align="left">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label70" runat="server" Text="Fecha Compromiso:" Font-Names="Calibri"
                                    Font-Size="Small"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox16" runat="server" Font-Names="Calibri" Font-Size="Small"
                                    Width="96px"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="true" TargetControlID="TextBox16"
                                    Format="yyyy-MM-dd"></asp:CalendarExtender>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ValidationGroup="AddPAI"
                                    ControlToValidate="TextBox16" Display="Dynamic" ForeColor="Red" InitialValue="">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr runat="server" id="TrComentariosPAI" visible="false">
                            <td colspan="2">
                                <table>
                                    <tr>
                                    <td>
                                    <br />

                                    </td>
                                    </tr>
                                    <tr>
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label75" runat="server" Text="Justificación:" Font-Names="Calibri"
                                                Font-Size="Small"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextBox14" runat="server" Height="50px" TextMode="MultiLine" Width="400px"
                                                Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator64" runat="server" ControlToValidate="TextBox14"
                                                ValidationGroup="UpdatePAI" ForeColor="Red">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr align="center">
                                        <td colspan="2">
                                            <asp:GridView ID="GridView6" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                                ForeColor="#333333" GridLines="Vertical" ShowHeaderWhenEmpty="True" BorderStyle="Solid"
                                                HorizontalAlign="Center" Font-Names="Calibri" Font-Size="Small" >
                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775"></AlternatingRowStyle>
                                                <Columns>
                                                    <asp:BoundField DataField="CodigoPAI" HeaderText="CodigoPAI" Visible="False">
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="NombreUsuario" HeaderText="Nombre Usuario"></asp:BoundField>
                                                    <asp:BoundField DataField="FechaRegistro" HeaderText="Fecha Registro"></asp:BoundField>
                                                    <asp:BoundField DataField="Comentario" HeaderText="Justificación"></asp:BoundField>
                                                </Columns>
                                                <EditRowStyle BackColor="#999999" />
                                                <FooterStyle BackColor="White" Font-Bold="True" ForeColor="White"></FooterStyle>
                                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" CssClass="gridViewHeader">
                                                </HeaderStyle>
                                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center"></PagerStyle>
                                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Left"></RowStyle>
                                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333"></SelectedRowStyle>
                                                <SortedAscendingCellStyle BackColor="#E9E7E2"></SortedAscendingCellStyle>
                                                <SortedAscendingHeaderStyle BackColor="#506C8C"></SortedAscendingHeaderStyle>
                                                <SortedDescendingCellStyle BackColor="#FFFDF8"></SortedDescendingCellStyle>
                                                <SortedDescendingHeaderStyle BackColor="#6F8DAE"></SortedDescendingHeaderStyle>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr align="center">
                            <td colspan="2">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:ImageButton ID="BtnNewPAI" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                ToolTip="Guardar nuevo" Style="height: 20px; width: 20px;" OnClick="BtnNewPAI_Click"
                                                ValidationGroup="AddPAI" />
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="BtnGuardaPAI" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                ToolTip="Guardar modificar" Style="height: 20px; width: 20px;" OnClick="BtnGuardaPAI_Click"
                                                ValidationGroup="UpdatePAI" />
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="BtnCancelPAI" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                                ToolTip="Cancelar" OnClick="BtnCancelPAI_Click" />
                                        </td>
                                    </tr>
                                </table>
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
                        <asp:Label ID="Label62" runat="server" Text="Atención" ForeColor="White" Font-Names="Calibri"
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
    <Triggers>
        <asp:PostBackTrigger ControlID="ImageButton7" />
        <asp:PostBackTrigger ControlID="GridViewArchivos" />
    </Triggers>
</asp:UpdatePanel>
