<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AuditoriaGestionTemas.ascx.cs"
    Inherits="ListasSarlaft.UserControls.MAuditoria.AuditoriaGestionTemas" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
    SelectCommand="SELECT [Descripcion], [Numero], [IdEnfoque] FROM [Auditoria].[Enfoque]"></asp:SqlDataSource>
<asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
    SelectCommand="SELECT * FROM [Auditoria].[DetalleEnfoque]"></asp:SqlDataSource>
<asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
    SelectCommand="SELECT [IdHallazgo], [IdAuditoria], [IdStandar], [IdDetalleEnfoque], [Observacion], [Tipo], [FechaCreacion], [IdUsuario], [IdObjetivo], [NivelHallazgo], [IdTiposHallazgo], [ComentarioAuditado] FROM [Auditoria].[Hallazgo]"
    DeleteCommand="DELETE FROM [Auditoria].[Hallazgo] WHERE [IdHallazgo] = @IdHallazgo"
    InsertCommand="INSERT INTO [Auditoria].[Hallazgo] ([IdAuditoria], [IdStandar], [IdDetalleEnfoque], [Observacion], [Tipo], [FechaCreacion], [IdUsuario], [IdObjetivo], [NivelHallazgo], [IdTiposHallazgo], [ComentarioAuditado]) VALUES (@IdAuditoria, @IdStandar, @IdDetalleEnfoque, @Observacion, @Tipo, @FechaCreacion, @IdUsuario, @IdObjetivo, @NivelHallazgo, @IdTiposHallazgo, @ComentarioAuditado)"
    UpdateCommand="UPDATE [Auditoria].[Hallazgo] SET [IdAuditoria] = @IdAuditoria, [IdStandar] = @IdStandar, [IdDetalleEnfoque] = @IdDetalleEnfoque, [Observacion] = @Observacion, [Tipo] = @Tipo, [FechaCreacion] = @FechaCreacion, [IdUsuario] = @IdUsuario, [IdObjetivo] = @IdObjetivo, [NivelHallazgo] = @NivelHallazgo, [IdTiposHallazgo] = @IdTiposHallazgo, [ComentarioAuditado] = @ComentarioAuditado WHERE [IdHallazgo] = @IdHallazgo">
    <DeleteParameters>
        <asp:Parameter Name="IdHallazgo" Type="Int64" />
    </DeleteParameters>
    <InsertParameters>
        <asp:Parameter Name="IdAuditoria" Type="Int64" />
        <asp:Parameter Name="IdStandar" Type="Int64" />
        <asp:Parameter Name="IdDetalleEnfoque" Type="Int64" />
        <asp:Parameter Name="Observacion" Type="String" />
        <asp:Parameter Name="Tipo" Type="String" />
        <asp:Parameter Name="FechaCreacion" Type="DateTime" />
        <asp:Parameter Name="IdUsuario" Type="Int64" />
        <asp:Parameter Name="IdObjetivo" Type="Int64" />
        <asp:Parameter Name="NivelHallazgo" Type="String" />
        <asp:Parameter Name="IdTiposHallazgo" Type="Int64" />
        <asp:Parameter Name="ComentarioAuditado" Type="String" />
    </InsertParameters>
    <UpdateParameters>
        <asp:Parameter Name="IdAuditoria" Type="Int64" />
        <asp:Parameter Name="IdStandar" Type="Int64" />
        <asp:Parameter Name="IdDetalleEnfoque" Type="Int64" />
        <asp:Parameter Name="Observacion" Type="String" />
        <asp:Parameter Name="Tipo" Type="String" />
        <asp:Parameter Name="FechaCreacion" Type="DateTime" />
        <asp:Parameter Name="IdUsuario" Type="Int64" />
        <asp:Parameter Name="IdObjetivo" Type="Int64" />
        <asp:Parameter Name="NivelHallazgo" Type="String" />
        <asp:Parameter Name="IdTiposHallazgo" Type="Int64" />
        <asp:Parameter Name="ComentarioAuditado" Type="String" />
        <asp:Parameter Name="IdHallazgo" Type="Int64" />
    </UpdateParameters>
</asp:SqlDataSource>
<asp:SqlDataSource ID="SqlDataSource5" runat="server" ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
    SelectCommand="SELECT [IdHallazgo], [IdAuditoria], [IdStandar], [IdDetalleEnfoque], [Observacion], [Tipo], [IdObjetivo], [NivelHallazgo], [IdTiposHallazgo], [ComentarioAuditado] FROM [Auditoria].[Hallazgo]">
</asp:SqlDataSource>
<asp:SqlDataSource ID="SqlDataSource6" runat="server" ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
    SelectCommand="SELECT [IdRecomendacion], [NumeroRecomendacion], [IdHallazgo], [IdSubdependencia], [IdSubdependenciaRespuesta], [IdSubproceso], [Observacion], [FechaRegistro], [IdUsuario], [Respuesta], [Estado] FROM [Auditoria].[Recomendacion]"
    DeleteCommand="DELETE FROM [Auditoria].[Recomendacion] WHERE [IdRecomendacion] = @IdRecomendacion"
    InsertCommand="INSERT INTO [Auditoria].[Recomendacion] ([NumeroRecomendacion], [IdHallazgo], [IdSubdependencia], [IdSubdependenciaRespuesta], [IdSubproceso], [Observacion], [FechaRegistro], [IdUsuario], [Respuesta], [Estado]) VALUES (@NumeroRecomendacion, @IdHallazgo, @IdSubdependencia, @IdSubdependenciaRespuesta, @IdSubproceso, @Observacion, @FechaRegistro, @IdUsuario, @Respuesta, @Estado)"
    UpdateCommand="UPDATE [Auditoria].[Recomendacion] SET [NumeroRecomendacion] = @NumeroRecomendacion, [IdHallazgo] = @IdHallazgo, [IdSubdependencia] = @IdSubdependencia, [IdSubdependenciaRespuesta] = @IdSubdependenciaRespuesta, [IdSubproceso] = @IdSubproceso, [Observacion] = @Observacion, [FechaRegistro] = @FechaRegistro, [IdUsuario] = @IdUsuario, [Respuesta] = @Respuesta, [Estado] = @Estado WHERE [IdRecomendacion] = @IdRecomendacion">
    <DeleteParameters>
        <asp:Parameter Name="IdRecomendacion" Type="Int64" />
    </DeleteParameters>
    <InsertParameters>
        <asp:Parameter Name="NumeroRecomendacion" Type="Int32" />
        <asp:Parameter Name="IdHallazgo" Type="Int64" />
        <asp:Parameter Name="IdSubdependencia" Type="Int64" />
        <asp:Parameter Name="IdSubdependenciaRespuesta" Type="Int64" />
        <asp:Parameter Name="IdSubproceso" Type="Int64" />
        <asp:Parameter Name="Observacion" Type="String" />
        <asp:Parameter Name="FechaRegistro" Type="DateTime" />
        <asp:Parameter Name="IdUsuario" Type="Int64" />
        <asp:Parameter Name="Respuesta" Type="String" />
        <asp:Parameter Name="Estado" Type="String" />
    </InsertParameters>
    <UpdateParameters>
        <asp:Parameter Name="NumeroRecomendacion" Type="Int32" />
        <asp:Parameter Name="IdHallazgo" Type="Int64" />
        <asp:Parameter Name="IdSubdependencia" Type="Int64" />
        <asp:Parameter Name="IdSubdependenciaRespuesta" Type="Int64" />
        <asp:Parameter Name="IdSubproceso" Type="Int64" />
        <asp:Parameter Name="Observacion" Type="String" />
        <asp:Parameter Name="FechaRegistro" Type="DateTime" />
        <asp:Parameter Name="IdUsuario" Type="Int64" />
        <asp:Parameter Name="Respuesta" Type="String" />
        <asp:Parameter Name="Estado" Type="String" />
        <asp:Parameter Name="IdRecomendacion" Type="Int64" />
    </UpdateParameters>
</asp:SqlDataSource>
<asp:SqlDataSource ID="SqlDataSource7" runat="server" ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
    DeleteCommand="DELETE FROM [Auditoria].[Riesgo] WHERE [IdRiesgo] = @IdRiesgo"
    InsertCommand="INSERT INTO [Auditoria].[Riesgo] ([NumeroRiesgo], [IdHallazgo], [IdTipoRiesgo], [IdSubdependencia], [IdProceso], [Estado], [Observacion], [FechaRegistro], [IdUsuario], [ComentarioAuditado]) VALUES (@NumeroRiesgo, @IdHallazgo, @IdTipoRiesgo, @IdSubdependencia, @IdProceso, @Estado, @Observacion, @FechaRegistro, @IdUsuario, @ComentarioAuditado)"
    SelectCommand="SELECT [IdRiesgo], [NumeroRiesgo], [IdHallazgo], [IdTipoRiesgo], [IdSubdependencia], [IdProceso], [Estado], [Observacion], CONVERT(VARCHAR(10),[FechaRegistro],103) AS FechaRegistro, [IdUsuario], [ComentarioAuditado] FROM [Auditoria].[Riesgo]"
    UpdateCommand="UPDATE [Auditoria].[Riesgo] SET [NumeroRiesgo] = @NumeroRiesgo, [IdHallazgo] = @IdHallazgo, [IdTipoRiesgo] = @IdTipoRiesgo, [IdSubdependencia] = @IdSubdependencia, [IdProceso] = @IdProceso, [Estado] = @Estado, [Observacion] = @Observacion, [FechaRegistro] = @FechaRegistro, [IdUsuario] = @IdUsuario, [ComentarioAuditado] = @ComentarioAuditado WHERE [IdRiesgo] = @IdRiesgo">
    <DeleteParameters>
        <asp:Parameter Name="IdRiesgo" Type="Int64" />
    </DeleteParameters>
    <InsertParameters>
        <asp:Parameter Name="NumeroRiesgo" Type="Int32" />
        <asp:Parameter Name="IdHallazgo" Type="Int64" />
        <asp:Parameter Name="IdTipoRiesgo" Type="Int64" />
        <asp:Parameter Name="IdSubdependencia" Type="Int64" />
        <asp:Parameter Name="IdProceso" Type="Int64" />
        <asp:Parameter Name="Estado" Type="String" />
        <asp:Parameter Name="Observacion" Type="String" />
        <asp:Parameter Name="FechaRegistro" Type="DateTime" />
        <asp:Parameter Name="IdUsuario" Type="Int64" />
        <asp:Parameter Name="ComentarioAuditado" Type="String" />
    </InsertParameters>
    <UpdateParameters>
        <asp:Parameter Name="NumeroRiesgo" Type="Int32" />
        <asp:Parameter Name="IdHallazgo" Type="Int64" />
        <asp:Parameter Name="IdTipoRiesgo" Type="Int64" />
        <asp:Parameter Name="IdSubdependencia" Type="Int64" />
        <asp:Parameter Name="IdProceso" Type="Int64" />
        <asp:Parameter Name="Estado" Type="String" />
        <asp:Parameter Name="Observacion" Type="String" />
        <asp:Parameter Name="FechaRegistro" Type="DateTime" />
        <asp:Parameter Name="IdUsuario" Type="Int64" />
        <asp:Parameter Name="ComentarioAuditado" Type="String" />
        <asp:Parameter Name="IdRiesgo" Type="Int64" />
    </UpdateParameters>
</asp:SqlDataSource>
<asp:SqlDataSource ID="SqlDataSource8" runat="server" ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
    SelectCommand="SELECT [IdPlaneacion], [Nombre] FROM [Auditoria].[Planeacion]">
</asp:SqlDataSource>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Width="960px">
            <asp:TabPanel runat="server" HeaderText="Desarrollo Programa/Estandar" ID="TabPanel1"
                Font-Underline="True">
                <ContentTemplate>
                    <table width="100%" bgcolor="#EEEEEE">
                        <%--<tr align="center" bgcolor="#333399">--%>
                        <tr align="center" bgcolor="Red">
                            <td colspan="4">
                                <asp:Label ID="Label10" runat="server" Text="Hallazgos" CssClass="AparienciaTitulo"
                                    ForeColor="White"></asp:Label>
                            </td>
                        </tr>
                        <tr align="left">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label11" runat="server" Text="Objetivo:" Width="100px" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td class="style3">
                                <asp:DropDownList ID="DropDownList5" runat="server" Width="350px" CssClass="Apariencia">
                                </asp:DropDownList>
                            </td>
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label12" runat="server" Text="Programa/Estandar:" Width="100px" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox20" runat="server" Enabled="False" Width="200px" CssClass="Apariencia"></asp:TextBox>
                            </td>
                        </tr>
                        <tr align="center" bgcolor="#5D7B9D">
                            <td colspan="4">
                                <asp:Label ID="Label13" runat="server" Text="Listado de Actividades Por Objetivo"
                                    CssClass="AparienciaTitulo" ForeColor="White"></asp:Label>
                            </td>
                        </tr>
                        <tr align="center">
                            <td colspan="4">
                                <table width="100%" border="1">
                                    <tr align="center">
                                        <td>
                                            <br />
                                            <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                                DataKeyNames="IdEnfoque" DataSourceID="SqlDataSource2" CssClass="Apariencia"
                                                ForeColor="#333333" GridLines="None" ShowHeaderWhenEmpty="True" AllowPaging="True"
                                                AllowSorting="True">
                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                <Columns>
                                                    <asp:BoundField DataField="IdEnfoque" HeaderText="IdEnfoque" InsertVisible="False"
                                                        ReadOnly="True" SortExpression="IdEnfoque" Visible="False" />
                                                    <asp:BoundField DataField="IdObjetivo" HeaderText="IdObjetivo" SortExpression="IdObjetivo"
                                                        Visible="False" />
                                                    <asp:BoundField DataField="Numero" HeaderText="Numero" SortExpression="Numero" />
                                                    <asp:BoundField DataField="Descripcion" HeaderText="Descripción" SortExpression="Descripcion" />
                                                    <asp:CommandField ButtonType="Image" HeaderText="Acción" SelectImageUrl="~/Imagenes/Icons/edit.png"
                                                        ShowSelectButton="True">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:CommandField>
                                                </Columns>
                                                <EditRowStyle BackColor="#999999" />
                                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" CssClass="gridViewHeader" />
                                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                            </asp:GridView>
                                            <br />
                                        </td>
                                    </tr>
                                    <tr align="center">
                                        <td>
                                            <br />
                                            <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                                DataKeyNames="IdDetalleEnfoque" DataSourceID="SqlDataSource3" Font-Names="Calibri"
                                                ForeColor="#333333" GridLines="None" ShowHeaderWhenEmpty="True" AllowPaging="True"
                                                AllowSorting="True" CssClass="Apariencia">
                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                <Columns>
                                                    <asp:BoundField DataField="IdDetalleEnfoque" HeaderText="IdDetalleEnfoque" InsertVisible="False"
                                                        ReadOnly="True" SortExpression="IdDetalleEnfoque" Visible="False" />
                                                    <asp:BoundField DataField="IdEnfoque" HeaderText="IdEnfoque" SortExpression="IdEnfoque"
                                                        Visible="False" />
                                                    <asp:BoundField DataField="Numero" HeaderText="Punto de Evaluación" SortExpression="Numero" />
                                                    <asp:BoundField DataField="Descripcion" HeaderText="Descripción" SortExpression="Descripcion" />
                                                    <asp:CommandField ButtonType="Image" HeaderText="Acción" SelectImageUrl="~/Imagenes/Icons/edit.png"
                                                        ShowSelectButton="True">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:CommandField>
                                                </Columns>
                                                <EditRowStyle BackColor="#999999" />
                                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" CssClass="gridViewHeader" />
                                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                            </asp:GridView>
                                            <br />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr align="center" bgcolor="#5D7B9D">
                            <td colspan="4">
                                <asp:Label ID="Label14" runat="server" CssClass="AparienciaTitulo" ForeColor="White"
                                    Text="Detalle de Hallazgos"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <table border="1" width="100%">
                                    <tr align="center">
                                        <td>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <br />
                                                        <asp:GridView ID="GridView4" runat="server" AllowPaging="True" AllowSorting="True"
                                                            AutoGenerateColumns="False" CellPadding="4" CssClass="Apariencia" DataKeyNames="IdHallazgo"
                                                            DataSourceID="SqlDataSource4" Font-Names="Calibri" ForeColor="#333333" GridLines="None"
                                                            OnSelectedIndexChanged="GridView4_SelectedIndexChanged" ShowHeaderWhenEmpty="True">
                                                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                            <Columns>
                                                                <asp:BoundField DataField="IdStandar" HeaderText="Estándar" SortExpression="IdStandar"
                                                                    Visible="False" />
                                                                <asp:BoundField DataField="IdObjetivo" HeaderText="Objetivo" SortExpression="IdObjetivo"
                                                                    Visible="False" />
                                                                <asp:BoundField DataField="IdDetalleEnfoque" HeaderText="Literal" SortExpression="IdDetalleEnfoque"
                                                                    Visible="False" />
                                                                <asp:BoundField DataField="IdHallazgo" HeaderText="IdHallazgo" InsertVisible="False"
                                                                    ReadOnly="True" SortExpression="IdHallazgo" />
                                                                <asp:BoundField DataField="Observacion" HeaderText="Hallazgos" SortExpression="Observacion" />
                                                                <asp:BoundField DataField="Tipo" HeaderText="Estado" SortExpression="Tipo" />
                                                                <asp:BoundField DataField="IdAuditoria" HeaderText="IdAuditoria" SortExpression="IdAuditoria"
                                                                    Visible="False" />
                                                                <asp:BoundField DataField="FechaCreacion" HeaderText="FechaCreacion" SortExpression="FechaCreacion"
                                                                    Visible="False" />
                                                                <asp:BoundField DataField="IdUsuario" HeaderText="IdUsuario" SortExpression="IdUsuario"
                                                                    Visible="False" />
                                                                <asp:BoundField DataField="NivelHallazgo" HeaderText="NivelHallazgo" SortExpression="NivelHallazgo"
                                                                    Visible="False" />
                                                                <asp:BoundField DataField="IdTiposHallazgo" HeaderText="IdTiposHallazgo" SortExpression="IdTiposHallazgo"
                                                                    Visible="False" />
                                                                <asp:BoundField DataField="ComentarioAuditado" HeaderText="ComentarioAuditado" SortExpression="ComentarioAuditado"
                                                                    Visible="False" />
                                                                <asp:TemplateField HeaderText="Gestión">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="imgBtnRecomendacion" runat="server" CausesValidation="False"
                                                                            CommandName="Select" ImageUrl="~/Imagenes/Icons/regular_folder (16).png" OnClick="imgBtnRecomendacion_Click"
                                                                            ToolTip="Recomendaciones" />
                                                                        <asp:ImageButton ID="imgBtnRiesgo" runat="server" CausesValidation="False" CommandName="Select"
                                                                            ImageUrl="~/Imagenes/Icons/Light_Alert.png" OnClick="imgBtnRiesgo_Click" ToolTip="Riesgos" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Acción" ShowHeader="False">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" CommandName="Select"
                                                                            ImageUrl="~/Imagenes/Icons/edit.png" OnClick="ImageButton1_Click" Text="Seleccionar" />
                                                                        <asp:ImageButton ID="ImageButton2" runat="server" CausesValidation="False" CommandName="Delete"
                                                                            ImageUrl="~/Imagenes/Icons/delete.png" Text="Eliminar" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <EditRowStyle BackColor="#999999" />
                                                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                            <HeaderStyle BackColor="#5D7B9D" CssClass="gridViewHeader" Font-Bold="True" ForeColor="White" />
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
                                                        <asp:ImageButton ID="imgBtnInsertarP2" runat="server" CausesValidation="False" CommandName="Insert"
                                                            ImageUrl="~/Imagenes/Icons/Add.png" OnClick="imgBtnInsertarP2_Click" Text="Insert" />
                                                    </td>
                                                </tr>
                                            </table>
                                            <br />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr align="center">
                            <td colspan="4">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Button ID="bntIrAuditoria1" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                OnClick="bntIrAuditoria1_Click" Text="Ir a Auditoria" />
                                        </td>
                                        <td>
                                            <asp:Button ID="Button4" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Ver..." />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Panel ID="Panel20" runat="server" Visible="False">
                                                <table border="1" bordercolor="White" cellpadding="2" cellspacing="0" width="100%">
                                                    <tr align="left">
                                                        <td>
                                                            <asp:Label ID="Label114" runat="server" CssClass="Apariencia" Text="Hallazgos:" Width="100px"></asp:Label>
                                                        </td>
                                                        <td class="style3">
                                                            <asp:TextBox ID="TextBox114" runat="server" CssClass="Apariencia" Rows="3" TextMode="MultiLine"
                                                                Width="429px"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label15" runat="server" CssClass="Apariencia" Text="Tipo Hallazgo:"
                                                                Width="137px"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlTipoHallazgo" runat="server" CssClass="Apariencia" Width="250px">
                                                                <asp:ListItem></asp:ListItem>
                                                                <asp:ListItem>ADMINISTRATIVO</asp:ListItem>
                                                                <asp:ListItem>CALIDAD</asp:ListItem>
                                                                <asp:ListItem>CONTROL</asp:ListItem>
                                                                <asp:ListItem>ESTRATEGICO</asp:ListItem>
                                                                <asp:ListItem>OPERATIVO</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr align="left">
                                                        <td>
                                                            <asp:Label ID="Label18" runat="server" CssClass="Apariencia" Text="Comentario del Auditado:"
                                                                Width="100px"></asp:Label>
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:TextBox ID="TextBox5" runat="server" CssClass="Apariencia" Rows="2" TextMode="MultiLine"
                                                                Width="429px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr align="left">
                                                        <td colspan="2">
                                                            <table width="100%">
                                                                <tr>
                                                                    <td class="style1">
                                                                        <asp:CheckBox ID="CheckBox1" runat="server" CssClass="Apariencia" Text="Aplica criterio propio: "
                                                                            TextAlign="Left" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:CheckBox ID="CheckBox2" runat="server" CssClass="Apariencia" Text="Nivel Gerencial: "
                                                                            TextAlign="Left" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label19" runat="server" CssClass="Apariencia" Text="Estado hallazgo:"
                                                                Width="119px"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlEstadoHallazgo" runat="server" CssClass="Apariencia" Width="187px">
                                                                <asp:ListItem></asp:ListItem>
                                                                <asp:ListItem>Por Mejorar</asp:ListItem>
                                                                <asp:ListItem>Positivo</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label20" runat="server" CssClass="Apariencia" Text="Usuario:" Width="119px"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label21" runat="server" CssClass="Apariencia" Width="119px"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label22" runat="server" CssClass="Apariencia" Text="Fecha:" Width="119px"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label23" runat="server" CssClass="Apariencia" Width="119px"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr align="center">
                                                        <td colspan="4">
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:ImageButton ID="ImageButton14" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                                            Visible="False" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:ImageButton ID="ImageButton15" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                                            Style="text-align: right" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:ImageButton ID="btnImgCancelarP1" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                                                            OnClick="btnImgCancelarP1_Click" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:TabPanel>


        </asp:TabContainer>
    </ContentTemplate>
</asp:UpdatePanel>
