<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MAuditoriaGestion.ascx.cs" Inherits="ListasSarlaft.UserControls.MAuditoria.MAuditoriaGestion" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<style type="text/css">
    .style1
    {
        width: 221px;
    }
    .style3
    {
        width: 244px;
    }
    .style4
    {
        width: 264px;
    }
    .style5
    {
        width: 270px;
    }
    
    .gridViewHeader a:link
    {
        text-decoration: none;
    }
    .style7
    {
        width: 173px;
    }
    .popup
    {
        border: Silver 1px solid;
        color: #060F40;
        background: #ffffff;
    }
    .style8
    {
        width: 96px;
    }
</style>

                       <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
                            DeleteCommand="DELETE FROM [Auditoria].[Auditoria] WHERE [IdAuditoria] = @IdAuditoria"
                            InsertCommand="INSERT INTO [Auditoria].[Auditoria] ([IdPlaneacion], [Tema], [IdEstandar], [Tipo], [IdDependencia], [IdProceso], [FechaRegistro], [IdUsuario], [IdEmpresa]) VALUES (@IdPlaneacion, @Tema, @IdEstandar, @Tipo, @IdDependencia, @IdProceso, @FechaRegistro, @IdUsuario, @IdEmpresa)"
                            SelectCommand="SELECT [IdAuditoria], [IdPlaneacion], [Tema], [IdEstandar], [Tipo], [IdDependencia], [IdProceso], [FechaRegistro], [IdUsuario], [IdEmpresa] FROM [Auditoria].[Auditoria]"
                            
    
    UpdateCommand="UPDATE [Auditoria].[Auditoria] SET [IdPlaneacion] = @IdPlaneacion, [Tema] = @Tema, [IdEstandar] = @IdEstandar, [Tipo] = @Tipo, [IdDependencia] = @IdDependencia, [IdProceso] = @IdProceso, [FechaRegistro] = @FechaRegistro, [IdUsuario] = @IdUsuario, [IdEmpresa] = @IdEmpresa WHERE [IdAuditoria] = @IdAuditoria">
                            <DeleteParameters>
                                <asp:Parameter Name="IdAuditoria" Type="Int64" />
                            </DeleteParameters>
                            <InsertParameters>
                                <asp:Parameter Name="IdPlaneacion" Type="Int64" />
                                <asp:Parameter Name="Tema" Type="String" />
                                <asp:Parameter Name="IdEstandar" Type="Int64" />
                                <asp:Parameter Name="Tipo" Type="String" />
                                <asp:Parameter Name="IdDependencia" Type="Int64" />
                                <asp:Parameter Name="IdProceso" Type="Int64" />
                                <asp:Parameter Name="FechaRegistro" Type="DateTime" />
                                <asp:Parameter Name="IdUsuario" Type="Int32" />
                                <asp:Parameter Name="IdEmpresa" Type="Int64" />
                            </InsertParameters>
                            <UpdateParameters>
                                <asp:Parameter Name="IdPlaneacion" Type="Int64" />
                                <asp:Parameter Name="Tema" Type="String" />
                                <asp:Parameter Name="IdEstandar" Type="Int64" />
                                <asp:Parameter Name="Tipo" Type="String" />
                                <asp:Parameter Name="IdDependencia" Type="Int64" />
                                <asp:Parameter Name="IdProceso" Type="Int64" />
                                <asp:Parameter Name="FechaRegistro" Type="DateTime" />
                                <asp:Parameter Name="IdUsuario" Type="Int32" />
                                <asp:Parameter Name="IdEmpresa" Type="Int64" />
                                <asp:Parameter Name="IdAuditoria" Type="Int64" />
                            </UpdateParameters>
                        </asp:SqlDataSource>

        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
            
    SelectCommand="SELECT [IdEnfoque], [Numero], [Descripcion] FROM [Auditoria].[Enfoque]">
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
            
    SelectCommand="SELECT [IdDetalleEnfoque], [Numero], [Descripcion] FROM [Auditoria].[DetalleEnfoque]">
        </asp:SqlDataSource>
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
        <asp:SqlDataSource ID="SqlDataSource8" runat="server" 
            ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>" 
            SelectCommand="SELECT [IdPlaneacion], [Nombre] FROM [Auditoria].[Planeacion]">
        </asp:SqlDataSource>

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <asp:Panel ID="pnlDependencia" runat="server" CssClass="popup" Width="400px" Style="display: none">
            <table width="100%" border="1" cellspacing="0" cellpadding="2" bordercolor="White">
                <tr align="right" bgcolor="#5D7B9D">
                    <td>
                        <asp:ImageButton ID="ImageButton7" runat="server" ImageUrl="~/Imagenes/Icons/dialog-close2.png"
                            OnClientClick="$find('popup').hidePopup(); return false;" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:TreeView ID="TreeView1" ExpandDepth="3" runat="server" Font-Names="Calibri"
                            Font-Size="Small" LineImagesFolder="~/TreeLineImages" ForeColor="Black" ShowLines="True"
                            OnSelectedNodeChanged="TreeView1_SelectedNodeChanged">
                            <SelectedNodeStyle BackColor="Silver" BorderColor="#66CCFF" BorderStyle="Inset" />
                        </asp:TreeView>
                    </td>
                </tr>
                <tr align="center" >
                    <td>
                        <asp:Button ID="BtnOk" runat="server" Text="Aceptar" CssClass="Apariencia"
                            OnClientClick="$find('popup').hidePopup(); return false;" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="pnlImpacto" runat="server" CssClass="popup" Width="400px" Style="display: none">
            <table width="100%" border="1" cellspacing="0" cellpadding="2" bordercolor="White">
                <tr align="right" bgcolor="#5D7B9D">
                    <td>
                        <asp:ImageButton ID="btnClosepp1" runat="server" ImageUrl="~/Imagenes/Icons/dialog-close2.png"
                            OnClientClick="$find('popup').hidePopup(); return false;" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <table width="100%" align="left" border="1" cellspacing="0" cellpadding="2" bordercolor="White">
                            <tr>
                                <td>
                                    <asp:Label ID="Label60" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Poco Probable:"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="Label61" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Es remoto y poco probable de que suceda."></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label62" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Eventual:"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="Label63" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Esta sujeto a la conjución de circunstancias."></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label64" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Probable:"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="Label65" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Puede ocurrir en algún momento."></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="pnlProbabilidad" runat="server" CssClass="popup" Width="400px" Style="display: none">
            <table width="100%" border="1" cellspacing="0" cellpadding="2" bordercolor="White">
                <tr align="right" bgcolor="#5D7B9D">
                    <td>
                        <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Imagenes/Icons/dialog-close2.png"
                            OnClientClick="$find('popup1').hidePopup(); return false;" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <table align="left" border="1" cellspacing="0" cellpadding="2" bordercolor="White">
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label56" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Insignificante:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label57" runat="server" Font-Names="Calibri" Font-Size="Small" Text="De poca importancia, no repercute negativamente en el Good Will de la empresa y puede acompañarse de perdidas financieras mínimas."></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label58" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Mínimo:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label59" runat="server" Font-Names="Calibri" Font-Size="Small" Text="De poca importancia, con leve impacto y puede acompañarse de perdidas financieras bajas."></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="pnlPlaneacion" runat="server" CssClass="popup" Width="400px" Style="display: none">
            <table width="100%" border="1" cellspacing="0" cellpadding="2" bordercolor="White">
                <tr align="right" bgcolor="#5D7B9D">
                    <td>
                        <asp:ImageButton ID="ImageButton8" runat="server" ImageUrl="~/Imagenes/Icons/dialog-close2.png"
                            OnClientClick="$find('popupPlaneacion').hidePopup(); return false;" />
                    </td>
                </tr>
                <tr align="center">
                    <td>
                        <asp:GridView ID="GridView8" runat="server" DataSourceID="SqlDataSource8" 
                            Font-Names="Calibri" Font-Size="Small"  HeaderStyle-CssClass="gridViewHeader"
                            AutoGenerateColumns="False" DataKeyNames="IdPlaneacion" AllowPaging="True" 
                            AllowSorting="True" ShowHeaderWhenEmpty="True"
                            Forecolor="#333333" HeaderStyle-BackColor="#000066" 
                            HeaderStyle-ForeColor="White" CellPadding="4" GridLines="None" 
                            onselectedindexchanged="GridView8_SelectedIndexChanged">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:BoundField DataField="IdPlaneacion" HeaderText="Código" 
                                    InsertVisible="False" ReadOnly="True" SortExpression="IdPlaneacion" >
                                <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Nombre" HeaderText="Nombre" 
                                    SortExpression="Nombre" />
                                <asp:CommandField ButtonType="Image" HeaderText="Acción" 
                                    SelectImageUrl="~/Imagenes/Icons/edit.png" ShowSelectButton="True" />
                            </Columns>
                            <EditRowStyle BackColor="#999999" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#5D7B9D" CssClass="gridViewHeader" ForeColor="White" 
                                Font-Bold="True" />
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
                <tr align="center" >
                    <td>
                        <asp:Button ID="Button1" runat="server" Text="Aceptar" CssClass="Apariencia"
                            OnClientClick="$find('popupPlaneacion').hidePopup(); return false;" />
                    </td>
                </tr>
            </table>
        </asp:Panel>

        <asp:Panel ID="pnlAuditoria" runat="server">
            <table width="80%">
                <%--<tr align="center" bgcolor="#333399">--%>
                <tr align="center" bgcolor="Red">
                    <td>
                        <asp:Label ID="Label6" runat="server" ForeColor="White" Text="Temas a Auditar" Font-Bold="False"
                            Font-Names="Calibri" Font-Size="Large"></asp:Label>
                    </td>
                </tr>
                <tr align="center" bgcolor="#EEEEEE">
                    <td>
                        <table border="1" cellspacing="0" cellpadding="2" bordercolor="White">
                        <tr>
                        <td  bgcolor="#BBBBBB">
                            <asp:Label ID="Label67" runat="server" Text="Código:" CssClass="Apariencia"></asp:Label>
                        </td>
                        <td >
                            <asp:TextBox ID="txtCodPlaneacion" runat="server" CssClass="Apariencia" 
                                Enabled="False" Width="40px"></asp:TextBox>
                        </td>
                        <td  bgcolor="#BBBBBB">
                            <asp:Label ID="Label66" runat="server" Text="Planeación:" CssClass="Apariencia"></asp:Label>
                        </td>
                        <td >
                            <asp:TextBox ID="txtNomPlaneacion" runat="server" CssClass="Apariencia" 
                                Enabled="False" Width="350px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:ImageButton ID="imgBtnPlaneacion" runat="server" 
                                ImageUrl="~/Imagenes/Icons/DateTime.png" />
                            <asp:PopupControlExtender ID="popupPlanea" runat="server" 
                                Enabled="True" ExtenderControlID="" TargetControlID="imgBtnPlaneacion" BehaviorID="popupPlaneacion"
                                PopupControlID="pnlPlaneacion" Position="Bottom"></asp:PopupControlExtender>
                        </td>
                        </tr>
                        </table>
                    </td>
                </tr>
                <tr align="center">
                    <td bgcolor="#EEEEEE">
 
                        <table width="100%" border="1">
                            <tr align="center">
                                <td>
                                    <br />
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:GridView ID="GridView1" runat="server" CellPadding="4" DataSourceID="SqlDataSource1"
                                                    ForeColor="#333333" GridLines="Vertical" AllowPaging="True" AllowSorting="True"
                                                    AutoGenerateColumns="False" DataKeyNames="IdAuditoria" OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
                                                    ShowHeaderWhenEmpty="True" HeaderStyle-CssClass="gridViewHeader" BorderStyle="Solid"
                                                    HorizontalAlign="Center" Font-Names="Calibri" Font-Size="Small">
                                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                    <Columns>
                                                        <asp:BoundField DataField="IdAuditoria" HeaderText="IdAuditoria" 
                                                            ReadOnly="True" SortExpression="IdAuditoria"
                                                            InsertVisible="False"></asp:BoundField>
                                                        <asp:BoundField DataField="IdPlaneacion" HeaderText="IdPlaneacion" 
                                                            SortExpression="IdPlaneacion"></asp:BoundField>
                                                        <asp:BoundField DataField="Tema" HeaderText="Tema" SortExpression="Tema" />
                                                        <asp:BoundField DataField="IdEstandar" HeaderText="IdEstandar" 
                                                            SortExpression="IdEstandar" />
                                                        <asp:BoundField DataField="Tipo" HeaderText="Tipo" SortExpression="Tipo" />
                                                        <asp:BoundField DataField="IdDependencia" HeaderText="IdDependencia" 
                                                            SortExpression="IdDependencia" />
                                                        <asp:BoundField DataField="IdProceso" HeaderText="IdProceso" 
                                                            SortExpression="IdProceso" />
                                                        <asp:BoundField DataField="FechaRegistro" HeaderText="FechaRegistro" 
                                                            SortExpression="FechaRegistro" />
                                                        <asp:BoundField DataField="IdUsuario" HeaderText="IdUsuario" 
                                                            SortExpression="IdUsuario" />
                                                        <asp:BoundField DataField="IdEmpresa" HeaderText="IdEmpresa" 
                                                            SortExpression="IdEmpresa" />
                                                        <asp:TemplateField ShowHeader="False" HeaderText="Acción">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" CommandName="Select"
                                                                    ImageUrl="~/Imagenes/Icons/edit.png" Text="Seleccionar" />
                                                                <asp:ImageButton ID="ImageButton2" runat="server" CausesValidation="False" CommandName="Delete"
                                                                    ImageUrl="~/Imagenes/Icons/delete.png" Text="Eliminar" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
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
                                                <asp:ImageButton ID="imgBtnInsertar" runat="server" CausesValidation="False" CommandName="Insert"
                                                    ImageUrl="~/Imagenes/Icons/Add.png" Text="Insert" OnClick="imgBtnInsertar_Click" />
                                                <br />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr align="center">
                    <td bgcolor="#EEEEEE">
                        <asp:Panel ID="Panel11" runat="server" Visible="False">
                            <table width="100%" border="1" cellspacing="0" cellpadding="2" bordercolor="White">
                                <tr align="left">
                                    <td align="left" bgcolor="#BBBBBB">
                                        <asp:Label ID="Label1" runat="server" Text="Código:" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                    </td>
                                    <td bgcolor="#EEEEEE">
                                        <asp:TextBox ID="TextBox1" runat="server" Enabled="False" Width="145px" TextMode="SingleLine"
                                            Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr align="left">
                                    <td align="left" bgcolor="#BBBBBB">
                                        <asp:Label ID="Label3" runat="server" Text="Tema:" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                    </td>
                                    <td bgcolor="#EEEEEE">
                                        <asp:TextBox ID="TextBox3" runat="server" Enabled="True" Width="377px" TextMode="SingleLine"
                                            Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr align="left">
                                    <td align="left" bgcolor="#BBBBBB">
                                        <asp:Label ID="Label4" runat="server" Text="Programa/Estándar:" 
                                            Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                    </td>
                                    <td bgcolor="#EEEEEE">
                                        <asp:DropDownList ID="DropDownList1" runat="server" Width="221px" Font-Names="Calibri"
                                            Font-Size="Small">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr align="left">
                                    <td align="left" bgcolor="#BBBBBB">
                                        <asp:Label ID="Label7" runat="server" Text="Auditor:" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                    </td>
                                    <td bgcolor="#EEEEEE">
                                        <asp:Label ID="Label338" runat="server" CssClass="Apariencia"></asp:Label>
                                    </td>
                                </tr>
                                <tr align="left">
                                    <td bgcolor="#BBBBBB">
                                        <asp:Label ID="Label8" runat="server" Text="Tipo:" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                    </td>
                                    <td bgcolor="#EEEEEE">
                                        <asp:DropDownList ID="ddlTipo" runat="server" Width="221px" Font-Names="Calibri"
                                            Font-Size="Small"
                                            onselectedindexchanged="ddlTipo_SelectedIndexChanged" AutoPostBack="True">
                                            <asp:ListItem>Procesos</asp:ListItem>
                                            <asp:ListItem>Dependencia</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr align="left" id="filaDependencia" runat="server" visible="false">
                                    <td bgcolor="#BBBBBB">
                                        <asp:Label ID="Label5" runat="server" Text="Dependencia:" Font-Names="Calibri" Font-Size="Small"></asp:Label>

                                    </td>
                                <td>
                                    <table>
                                        <tr>
                                            <td bgcolor="#EEEEEE">
                                                <asp:TextBox ID="txtDependencia" runat="server" Enabled="False" Width="377px" TextMode="SingleLine"
                                                    Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
                                                 <asp:Label ID="lblIdDependencia" runat="server" Text="" Visible="false"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="imgDependencia" runat="server" ImageUrl="~/Imagenes/Icons/Organization-Chart.png"
                                                     />
                                                <asp:PopupControlExtender ID="popupDependencia" runat="server" DynamicServicePath=""
                                                    Enabled="True" ExtenderControlID="" TargetControlID="imgDependencia" BehaviorID="popup"
                                                    PopupControlID="pnlDependencia" Position="Right" OffsetY="-150"></asp:PopupControlExtender>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                </tr>
                                <tr align="left" id="filaProceso" runat="server" visible="true">
                                    <td align="left" bgcolor="#BBBBBB">
                                        <asp:Label ID="Label9" runat="server" Text="Macroproceso:" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                    </td>
                                    <td bgcolor="#EEEEEE">
                                        <table width="60%" >
                                            <tr>
                                                <td>
                                                    <asp:DropDownList ID="ddlMacroProceso" runat="server" Width="221px" cssclass="Apariencia">
                                                    </asp:DropDownList>
                                                </td>
                                                <td align="left" bgcolor="#BBBBBB">
                                                    <asp:Label ID="Label2" runat="server" Text="Proceso:" cssclass="Apariencia" 
                                                        Width="100px"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlProceso" runat="server" Width="221px" cssclass="Apariencia">
                                                    </asp:DropDownList>
                                                </td>
                                                <td align="left" bgcolor="#BBBBBB">
                                                    <asp:Label ID="Label70" runat="server" Text="Subproceso:" cssclass="Apariencia" 
                                                        Width="100px"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlSubproceso" runat="server" Width="221px" cssclass="Apariencia">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr align="left">
                                    <td align="left" bgcolor="#BBBBBB">
                                        <asp:Label ID="Label16" runat="server" Text="Fecha:" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                    </td>
                                    <td bgcolor="#EEEEEE">
                                        <asp:Label ID="Label339" runat="server" CssClass="Apariencia"></asp:Label>
                                    </td>
                                </tr>
                                <tr align="left">
                                    <td align="left" bgcolor="#BBBBBB">
                                        <asp:Label ID="Label17" runat="server" Text="Usuario:" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                    </td>
                                    <td bgcolor="#EEEEEE">
                                        <asp:Label ID="Label340" runat="server" CssClass="Apariencia"></asp:Label>
                                    </td>
                                </tr>
                                <tr align="center">
                                    <td colspan="2">
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:Button ID="btnGestion" runat="server" Text="Gestión" Font-Names="Calibri" Font-Size="Small"
                                                        OnClick="btnGestion_Click" />
                                                </td>
                                                <td>
                                                    <asp:Button ID="Button2" runat="server" Text="Informe de Cierre" Font-Names="Calibri"
                                                        Font-Size="Small" />
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
                                                    <asp:ImageButton ID="btnImgInsertar" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                        Visible="False" />
                                                </td>
                                                <td>
                                                    <asp:ImageButton ID="btnImgActualizar" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                        Style="text-align: right" />
                                                </td>
                                                <td>
                                                    <asp:ImageButton ID="btnImgCancelar" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                                        OnClick="btnImgCancelar_Click" />
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
        </asp:Panel>
        <asp:Panel ID="pnlGestion" runat="server" Visible="False">
            <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="2"
                Width="960px">
                <asp:TabPanel runat="server" HeaderText="Desarrollo Programa/Estandar" ID="TabPanel1" Font-Underline="True">
                    <ContentTemplate>
                        <table width="100%" bgcolor="#EEEEEE">
                            <tr align="center" bgcolor="#333399">
                                <td colspan="4">
                                    <asp:Label ID="Label10" runat="server" Text="Hallazgos" CssClass="AparienciaTitulo"
                                        ForeColor="White"></asp:Label>
                                </td>
                            </tr>
                            <tr align="left">
                                <td  bgcolor="#BBBBBB">
                                    <asp:Label ID="Label11" runat="server" Text="Objetivo:" Width="100px" CssClass="Apariencia"></asp:Label>
                                </td>
                                <td class="style3">
                                    <asp:DropDownList ID="DropDownList5" runat="server" Width="350px" CssClass="Apariencia">
                                    </asp:DropDownList>
                                </td>
                                <td  bgcolor="#BBBBBB">
                                    <asp:Label ID="Label12" runat="server" Text="Programa/Estandar:" Width="100px" CssClass="Apariencia"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBox20" runat="server" Enabled="False" Width="200px" CssClass="Apariencia"></asp:TextBox>
                                </td>
                            </tr>
                            <tr align="center" bgcolor="#5D7B9D">
                                <td colspan="4">
                                    <asp:Label ID="Label13" runat="server" Text="Listado de Actividades Por Objetivo" CssClass="AparienciaTitulo"
                                        ForeColor="White"></asp:Label>
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
                                                            ReadOnly="True" SortExpression="IdEnfoque" />
                                                        <asp:BoundField DataField="Numero" HeaderText="Numero" 
                                                            SortExpression="Numero" />
                                                        <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" 
                                                            SortExpression="Descripcion" />
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
                                                            ReadOnly="True" SortExpression="IdDetalleEnfoque" />
                                                        <asp:BoundField DataField="Numero" HeaderText="Numero" 
                                                            SortExpression="Numero" />
                                                        <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" 
                                                            SortExpression="Descripcion" />
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
                                    <asp:Label ID="Label14" runat="server" CssClass="AparienciaTitulo" 
                                        ForeColor="White" Text="Detalle de Hallazgos"></asp:Label>
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
                                                            <asp:GridView ID="GridView4" runat="server" AllowPaging="True" 
                                                                AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" 
                                                                CssClass="Apariencia" DataKeyNames="IdHallazgo" DataSourceID="SqlDataSource4" 
                                                                Font-Names="Calibri" ForeColor="#333333" GridLines="None" 
                                                                OnSelectedIndexChanged="GridView4_SelectedIndexChanged" 
                                                                ShowHeaderWhenEmpty="True">
                                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                                <Columns>
                                                                    <asp:BoundField DataField="IdStandar" HeaderText="Estándar" 
                                                                        SortExpression="IdStandar" Visible="False" />
                                                                    <asp:BoundField DataField="IdObjetivo" HeaderText="Objetivo" 
                                                                        SortExpression="IdObjetivo" Visible="False" />
                                                                    <asp:BoundField DataField="IdDetalleEnfoque" HeaderText="Literal" 
                                                                        SortExpression="IdDetalleEnfoque" Visible="False" />
                                                                    <asp:BoundField DataField="IdHallazgo" HeaderText="IdHallazgo" 
                                                                        InsertVisible="False" ReadOnly="True" SortExpression="IdHallazgo" />
                                                                    <asp:BoundField DataField="Observacion" HeaderText="Hallazgos" 
                                                                        SortExpression="Observacion" />
                                                                    <asp:BoundField DataField="Tipo" HeaderText="Estado" SortExpression="Tipo" />
                                                                    <asp:BoundField DataField="IdAuditoria" HeaderText="IdAuditoria" 
                                                                        SortExpression="IdAuditoria" Visible="False" />
                                                                    <asp:BoundField DataField="FechaCreacion" HeaderText="FechaCreacion" 
                                                                        SortExpression="FechaCreacion" Visible="False" />
                                                                    <asp:BoundField DataField="IdUsuario" HeaderText="IdUsuario" 
                                                                        SortExpression="IdUsuario" Visible="False" />
                                                                    <asp:BoundField DataField="NivelHallazgo" HeaderText="NivelHallazgo" 
                                                                        SortExpression="NivelHallazgo" Visible="False" />
                                                                    <asp:BoundField DataField="IdTiposHallazgo" HeaderText="IdTiposHallazgo" 
                                                                        SortExpression="IdTiposHallazgo" Visible="False" />
                                                                    <asp:BoundField DataField="ComentarioAuditado" HeaderText="ComentarioAuditado" 
                                                                        SortExpression="ComentarioAuditado" Visible="False" />
                                                                    <asp:TemplateField HeaderText="Gestión">
                                                                        <ItemTemplate>
                                                                            <asp:ImageButton ID="imgBtnRecomendacion" runat="server" 
                                                                                CausesValidation="False" CommandName="Select" 
                                                                                ImageUrl="~/Imagenes/Icons/regular_folder (16).png" 
                                                                                OnClick="imgBtnRecomendacion_Click" ToolTip="Recomendaciones" />
                                                                            <asp:ImageButton ID="imgBtnRiesgo" runat="server" CausesValidation="False" 
                                                                                CommandName="Select" ImageUrl="~/Imagenes/Icons/Light_Alert.png" 
                                                                                OnClick="imgBtnRiesgo_Click" ToolTip="Riesgos" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Acción" ShowHeader="False">
                                                                        <ItemTemplate>
                                                                            <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" 
                                                                                CommandName="Select" ImageUrl="~/Imagenes/Icons/edit.png" 
                                                                                OnClick="ImageButton1_Click" Text="Seleccionar" />
                                                                            <asp:ImageButton ID="ImageButton2" runat="server" CausesValidation="False" 
                                                                                CommandName="Delete" ImageUrl="~/Imagenes/Icons/delete.png" Text="Eliminar" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <EditRowStyle BackColor="#999999" />
                                                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                                <HeaderStyle BackColor="#5D7B9D" CssClass="gridViewHeader" Font-Bold="True" 
                                                                    ForeColor="White" />
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
                                                            <asp:ImageButton ID="imgBtnInsertarP2" runat="server" CausesValidation="False" 
                                                                CommandName="Insert" ImageUrl="~/Imagenes/Icons/Add.png" 
                                                                OnClick="imgBtnInsertarP2_Click" Text="Insert" />
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
                                                <asp:Button ID="bntIrAuditoria1" runat="server" Font-Names="Calibri" 
                                                    Font-Size="Small" OnClick="bntIrAuditoria1_Click" Text="Ir a Auditoria" />
                                            </td>
                                            <td>
                                                <asp:Button ID="Button4" runat="server" Font-Names="Calibri" Font-Size="Small" 
                                                    Text="Ver..." />
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
                                                    <table border="1" bordercolor="White" cellpadding="2" cellspacing="0" 
                                                        width="100%">
                                                        <tr align="left">
                                                            <td>
                                                                <asp:Label ID="Label114" runat="server" CssClass="Apariencia" Text="Hallazgos:" 
                                                                    Width="100px"></asp:Label>
                                                            </td>
                                                            <td class="style3">
                                                                <asp:TextBox ID="TextBox114" runat="server" CssClass="Apariencia" Rows="3" 
                                                                    TextMode="MultiLine" Width="429px"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label15" runat="server" CssClass="Apariencia" 
                                                                    Text="Tipo Hallazgo:" Width="137px"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlTipoHallazgo" runat="server" CssClass="Apariencia" 
                                                                    Width="250px">
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
                                                                <asp:Label ID="Label18" runat="server" CssClass="Apariencia" 
                                                                    Text="Comentario del Auditado:" Width="100px"></asp:Label>
                                                            </td>
                                                            <td colspan="3">
                                                                <asp:TextBox ID="TextBox5" runat="server" CssClass="Apariencia" Rows="2" 
                                                                    TextMode="MultiLine" Width="429px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr align="left">
                                                            <td colspan="2">
                                                                <table width="100%">
                                                                    <tr>
                                                                        <td class="style1">
                                                                            
                                                                        </td>
                                                                        <td>

                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label19" runat="server" CssClass="Apariencia" 
                                                                    Text="Estado hallazgo:" Width="119px"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlEstadoHallazgo" runat="server" CssClass="Apariencia" 
                                                                    Width="187px">
                                                                    <asp:ListItem></asp:ListItem>
                                                                    <asp:ListItem>Por Mejorar</asp:ListItem>
                                                                    <asp:ListItem>Positivo</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label20" runat="server" CssClass="Apariencia" Text="Usuario:" 
                                                                    Width="119px"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label21" runat="server" CssClass="Apariencia" Width="119px"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label22" runat="server" CssClass="Apariencia" Text="Fecha:" 
                                                                    Width="119px"></asp:Label>
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
                                                                            <asp:ImageButton ID="ImageButton14" runat="server" 
                                                                                ImageUrl="~/Imagenes/Icons/guardar.png" Visible="False" />
                                                                        </td>
                                                                        <td>
                                                                            <asp:ImageButton ID="ImageButton15" runat="server" 
                                                                                ImageUrl="~/Imagenes/Icons/guardar.png" Style="text-align: right" />
                                                                        </td>
                                                                        <td>
                                                                            <asp:ImageButton ID="btnImgCancelarP1" runat="server" 
                                                                                ImageUrl="~/Imagenes/Icons/cancel.png" OnClick="btnImgCancelarP1_Click" />
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
                <asp:TabPanel ID="TabPanel2" runat="server" HeaderText="Recomendaciones">
                    <ContentTemplate>
                        <table width="100%" bgcolor="#EEEEEE">
                            <tr align="center" bgcolor="#333399">
                                <td>
                                    <asp:Label ID="Label24" runat="server" Text="Hallazgo" CssClass="AparienciaTitulo"
                                        ForeColor="White"></asp:Label>
                                </td>
                            </tr>
                            <tr align ="center">
                            <td>
                            <table border="1" bordercolor="White" cellpadding="2" cellspacing="0">
                                <tr>
                                    <td bgcolor="#BBBBBB">
                                        <asp:Label ID="Label26" runat="server" CssClass="Apariencia" Text="Estándar:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label27" runat="server" CssClass="Apariencia"></asp:Label>
                                    </td>
                                    <td bgcolor="#BBBBBB">
                                        <asp:Label ID="Label28" runat="server" CssClass="Apariencia" Text="Objetivo:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label29" runat="server" CssClass="Apariencia"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td bgcolor="#BBBBBB">
                                        <asp:Label ID="Label30" runat="server" CssClass="Apariencia" Text="Nro Enfoque:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label331" runat="server" CssClass="Apariencia"></asp:Label>
                                    </td>
                                    <td bgcolor="#BBBBBB">
                                        <asp:Label ID="Label32" runat="server" CssClass="Apariencia" Text="Tipo:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label333" runat="server" CssClass="Apariencia"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td bgcolor="#BBBBBB" width="100" >
                                        <asp:Label ID="Label68" runat="server" CssClass="Apariencia" Text="Código Hallazgo:"></asp:Label>
                                    </td>
                                    <td width="150" align="center">
                                        <asp:Label ID="lblCodHallazgoRec" runat="server" CssClass="Apariencia" Enabled="False"></asp:Label>
                                    </td>
                                    <td bgcolor="#BBBBBB" width="60">
                                        <asp:Label ID="Label71" runat="server" CssClass="Apariencia" Text="Hallazgo:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblHallazgoRec" runat="server" CssClass="Apariencia" Enabled="False"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            </td>
                            </tr>

                        <tr align="center" bgcolor="#5D7B9D">
                            <td>
                                <asp:Label ID="Label334" runat="server" CssClass="AparienciaTitulo" 
                                    ForeColor="White" Text="Detalle de Recomendaciones"></asp:Label>
                            </td>
                        </tr>
                        <tr align="center">
                            <td>
                                <table border="1" width="100%">
                                    <tr align="center">
                                        <td>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <br />
                                                        <asp:GridView ID="GridView6" runat="server" AllowPaging="True" 
                                                            AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" 
                                                            DataKeyNames="IdRecomendacion" DataSourceID="SqlDataSource6" 
                                                            Font-Names="Calibri" ForeColor="#333333" GridLines="None" 
                                                            OnSelectedIndexChanged="GridView5_SelectedIndexChanged" 
                                                            ShowHeaderWhenEmpty="True">
                                                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                            <Columns>
                                                                <asp:BoundField DataField="IdRecomendacion" HeaderText="IdRecomendacion" 
                                                                    InsertVisible="False" ReadOnly="True" SortExpression="IdRecomendacion" 
                                                                    Visible="False" />
                                                                <asp:BoundField DataField="NumeroRecomendacion" 
                                                                    HeaderText="NumeroRecomendacion" SortExpression="NumeroRecomendacion" 
                                                                    Visible="False" />
                                                                <asp:BoundField DataField="IdHallazgo" HeaderText="IdHallazgo" 
                                                                    SortExpression="IdHallazgo" Visible="False" />
                                                                <asp:BoundField DataField="IdSubdependencia" 
                                                                    HeaderText="Subdependencia Auditada" SortExpression="IdSubdependencia" />
                                                                <asp:BoundField DataField="IdSubdependenciaRespuesta" 
                                                                    HeaderText="Subdependencia Respuesta" 
                                                                    SortExpression="IdSubdependenciaRespuesta" />
                                                                <asp:BoundField DataField="IdSubproceso" HeaderText="Subproceso" 
                                                                    SortExpression="IdSubproceso" />
                                                                <asp:BoundField DataField="Observacion" HeaderText="Recomendación" 
                                                                    SortExpression="Observacion" />
                                                                <asp:BoundField DataField="FechaRegistro" HeaderText="FechaRegistro" 
                                                                    SortExpression="FechaRegistro" Visible="False" />
                                                                <asp:BoundField DataField="IdUsuario" HeaderText="IdUsuario" 
                                                                    SortExpression="IdUsuario" Visible="False" />
                                                                <asp:BoundField DataField="Respuesta" HeaderText="Respuesta" 
                                                                    SortExpression="Respuesta" Visible="False" />
                                                                <asp:BoundField DataField="Estado" HeaderText="Estado" SortExpression="Estado" 
                                                                    Visible="False" />
                                                                <asp:TemplateField HeaderText="Acción" ShowHeader="False">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="ImageButton120" runat="server" CausesValidation="False" 
                                                                            CommandName="Select" ImageUrl="~/Imagenes/Icons/edit.png" Text="Seleccionar" />
                                                                        <asp:ImageButton ID="ImageButton2" runat="server" CausesValidation="False" 
                                                                            CommandName="Delete" ImageUrl="~/Imagenes/Icons/delete.png" Text="Eliminar" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <EditRowStyle BackColor="#999999" />
                                                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                            <HeaderStyle BackColor="#5D7B9D" CssClass="gridViewHeader" Font-Bold="True" 
                                                                ForeColor="White" />
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
                                                        <asp:ImageButton ID="imgBtnInsertarRec" runat="server" CausesValidation="False" 
                                                            CommandName="Insert" ImageUrl="~/Imagenes/Icons/Add.png" 
                                                            OnClick="imgBtnInsertarRec_Click" Text="Insert" />
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
                            <td>
                                <asp:Button ID="bntIrAuditoria2" runat="server" Font-Names="Calibri" 
                                    Font-Size="Small" OnClick="bntIrAuditoria2_Click" Text="Ir a Auditoría" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <asp:Panel ID="Panel3" runat="server" Visible="False">
                                                <table border="1" bordercolor="White" cellpadding="2" cellspacing="0" 
                                                    width="100%">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label335" runat="server" CssClass="Apariencia" 
                                                                Text="Tipo de Auditoría:" Width="167px"></asp:Label>
                                                        </td>
                                                        <td class="style5">
                                                            <asp:Label ID="Label336" runat="server" CssClass="Apariencia"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label337" runat="server" CssClass="Apariencia" 
                                                                Text="Subdependencia Respuesta:" Width="208px"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="DropDownList44" runat="server" CssClass="Apariencia" 
                                                                Width="254px">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label138" runat="server" CssClass="Apariencia" 
                                                                Text="Dependencia Auditada:" Width="167px"></asp:Label>
                                                        </td>
                                                        <td class="style5">
                                                            <asp:TextBox ID="TextBox113" runat="server" CssClass="Apariencia" 
                                                                Enabled="False" Rows="1" Width="250px"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label129" runat="server" CssClass="Apariencia" 
                                                                Text="Subdependencia:" Width="208px"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TextBox4" runat="server" CssClass="Apariencia" Enabled="False" 
                                                                Rows="1" Width="250px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label128" runat="server" CssClass="Apariencia" 
                                                                Text="Proceso Auditado:" Width="167px"></asp:Label>
                                                        </td>
                                                        <td class="style5">
                                                            <asp:TextBox ID="TextBox6" runat="server" CssClass="Apariencia" Rows="1" 
                                                                Width="250px"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label130" runat="server" CssClass="Apariencia" 
                                                                Text="Subproceso:" Width="208px"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlTipoHallazgoP2" runat="server" CssClass="Apariencia" 
                                                                Width="254px">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label31" runat="server" CssClass="Apariencia" 
                                                                Text="Recomendación:" Width="167px"></asp:Label>
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:TextBox ID="TextBox7" runat="server" CssClass="Apariencia" Rows="3" 
                                                                TextMode="MultiLine" Width="751px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr align="center">
                                                        <td colspan="4">
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:ImageButton ID="ImageButton130" runat="server" 
                                                                            ImageUrl="~/Imagenes/Icons/guardar.png" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:ImageButton ID="btnImgCancelarRec_Click" runat="server" 
                                                                            ImageUrl="~/Imagenes/Icons/cancel.png" 
                                                                            OnClick="btnImgCancelarRec_Click_Click" />
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
                        </table>
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel ID="TabPanel3" runat="server" HeaderText="Riesgos">
                    <ContentTemplate>
                        <table width="100%" bgcolor="#EEEEEE">
                            <tr align="center" bgcolor="#333399">
                                <td>
                                    <asp:Label ID="Label33" runat="server" Text="Hallazgo" CssClass="AparienciaTitulo"
                                        ForeColor="White"></asp:Label>
                                </td>
                            </tr>
                            <tr align ="center">
                            <td>
                            <table border="1" bordercolor="White" cellpadding="2" cellspacing="0">
                                <tr>
                                    <td bgcolor="#BBBBBB">
                                        <asp:Label ID="Label35" runat="server" CssClass="Apariencia" Text="Estándar:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label36" runat="server" CssClass="Apariencia"></asp:Label>
                                    </td>
                                    <td bgcolor="#BBBBBB">
                                        <asp:Label ID="Label37" runat="server" CssClass="Apariencia" Text="Objetivo:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label38" runat="server" CssClass="Apariencia"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td bgcolor="#BBBBBB">
                                        <asp:Label ID="Label39" runat="server" CssClass="Apariencia" Text="Nro Enfoque:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label40" runat="server" CssClass="Apariencia"></asp:Label>
                                    </td>
                                    <td bgcolor="#BBBBBB">
                                        <asp:Label ID="Label41" runat="server" CssClass="Apariencia" Text="Tipo:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label42" runat="server" CssClass="Apariencia"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td bgcolor="#BBBBBB" width="100" >
                                        <asp:Label ID="Label69" runat="server" CssClass="Apariencia" Text="Código Hallazgo:"></asp:Label>
                                    </td>
                                    <td width="150" align="center">
                                        <asp:Label ID="lblCodHallazgoRie" runat="server" CssClass="Apariencia" Enabled="False"></asp:Label>
                                    </td>
                                    <td bgcolor="#BBBBBB" width="60">
                                        <asp:Label ID="Label73" runat="server" CssClass="Apariencia" Text="Hallazgo:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblHallazgoRie" runat="server" CssClass="Apariencia" Enabled="False"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            </td>
                            </tr>
                            <tr align="center" bgcolor="#5D7B9D">
                                <td>
                                    <asp:Label ID="Label43" runat="server" Text="Detalle de Riesgos" CssClass="AparienciaTitulo"
                                        ForeColor="White"></asp:Label>
                                </td>
                            </tr>
                            <tr align="center">
                                <td>
                                    <table width="100%" border="1">
                                        <tr align="center">
                                            <td>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <br />
                                                            <asp:GridView ID="GridView7" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                                                DataSourceID="SqlDataSource7" ForeColor="#333333" GridLines="None" ShowHeaderWhenEmpty="True"
                                                                AllowPaging="True" AllowSorting="True"
                                                                CssClass="Apariencia" DataKeyNames="IdRiesgo" 
                                                                OnSelectedIndexChanged="GridView7_SelectedIndexChanged">
                                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                                <Columns>
                                                                    <asp:BoundField DataField="IdRiesgo" HeaderText="IdRiesgo" InsertVisible="False"
                                                                        ReadOnly="True" SortExpression="IdRiesgo" Visible="False" />
                                                                    <asp:BoundField DataField="NumeroRiesgo" HeaderText="NumeroRiesgo" SortExpression="NumeroRiesgo"
                                                                        Visible="False" />
                                                                    <asp:BoundField DataField="IdHallazgo" HeaderText="IdHallazgo" SortExpression="IdHallazgo"
                                                                        Visible="False" />
                                                                    <asp:BoundField DataField="IdTipoRiesgo" HeaderText="Tipo de Riesgo" SortExpression="IdTipoRiesgo" />
                                                                    <asp:BoundField DataField="IdSubdependencia" HeaderText="Subdependencia" SortExpression="IdSubdependencia" />
                                                                    <asp:BoundField DataField="IdProceso" HeaderText="IdProceso" SortExpression="IdProceso"
                                                                        Visible="False" />
                                                                    <asp:BoundField DataField="Estado" HeaderText="Estado" SortExpression="Estado" />
                                                                    <asp:BoundField DataField="Observacion" HeaderText="Observacion" SortExpression="Observacion" />
                                                                    <asp:BoundField DataField="FechaRegistro" HeaderText="FechaRegistro" SortExpression="FechaRegistro" />
                                                                    <asp:BoundField DataField="IdUsuario" HeaderText="IdUsuario" SortExpression="IdUsuario"
                                                                        Visible="False" />
                                                                    <asp:BoundField DataField="ComentarioAuditado" HeaderText="ComentarioAuditado" SortExpression="ComentarioAuditado"
                                                                        Visible="False" />
                                                                    <asp:TemplateField HeaderText="Acción" ShowHeader="False">
                                                                        <ItemTemplate>
                                                                            <asp:ImageButton ID="ImageButton111" runat="server" CausesValidation="False" CommandName="Select"
                                                                                ImageUrl="~/Imagenes/Icons/edit.png" Text="Seleccionar" /><asp:ImageButton ID="ImageButton2"
                                                                                    runat="server" CausesValidation="False" CommandName="Delete" ImageUrl="~/Imagenes/Icons/delete.png"
                                                                                    Text="Eliminar" /></ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <EditRowStyle BackColor="#999999" />
                                                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" 
                                                                    CssClass="gridViewHeader" />
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
                                                            <asp:ImageButton ID="imgBtnInsertarRie" runat="server" CausesValidation="False" CommandName="Insert"
                                                                ImageUrl="~/Imagenes/Icons/Add.png" Text="Insert" OnClick="imgBtnInsertarRie_Click" />
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
                                <td>
                                    <asp:Button ID="bntIrAuditoria3" runat="server" Text="Ir a Auditoría" Font-Names="Calibri"
                                        Font-Size="Small" OnClick="bntIrAuditoria3_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table width="100%">
                                        <tr>
                                            <td>
                                                <asp:Panel ID="Panel1" runat="server" Visible="False">
                                                    <table width="100%" border="1" cellspacing="0" cellpadding="2" bordercolor="White">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label51" runat="server" Text="Nombre del Riesgo:" Width="167px" CssClass="Apariencia"></asp:Label>
                                                            </td>
                                                            <td colspan="3">
                                                                <asp:TextBox ID="TextBox11" runat="server" Width="756px" CssClass="Apariencia" 
                                                                    Rows="1"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label52" runat="server" Text="Descripción del Riesgo:" Width="167px"
                                                                    CssClass="Apariencia"></asp:Label>
                                                            </td>
                                                            <td colspan="3">
                                                                <asp:TextBox ID="TextBox12" runat="server" Width="756px" CssClass="Apariencia" Rows="3"
                                                                    TextMode="MultiLine"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label44" runat="server" Text="Descripción:" Width="167px" CssClass="Apariencia"></asp:Label>
                                                            </td>
                                                            <td class="style5">
                                                                <asp:Label ID="Label45" runat="server" CssClass="Apariencia"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label46" runat="server" Text="Tipo de Riesgo:" Width="208px" CssClass="Apariencia"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="DropDownList3" runat="server" Width="250px" CssClass="Apariencia">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label47" runat="server" Text="Dependencia:" Width="167px" CssClass="Apariencia"></asp:Label>
                                                            </td>
                                                            <td class="style5">
                                                                <asp:TextBox ID="TextBox8" runat="server" Width="250px" CssClass="Apariencia"
                                                                    Rows="1" Enabled="False"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label48" runat="server" Text="Subdependencia:" Width="208px" CssClass="Apariencia"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="TextBox9" runat="server" Width="250px" CssClass="Apariencia"
                                                                    Rows="1" Enabled="False"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label49" runat="server" Text="Proceso:" Width="167px" CssClass="Apariencia"></asp:Label>
                                                            </td>
                                                            <td class="style5">
                                                                <asp:TextBox ID="TextBox10" runat="server" Width="250px" CssClass="Apariencia"
                                                                    Rows="1"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label50" runat="server" Text="Subproceso:" Width="208px" CssClass="Apariencia"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="DropDownList4" runat="server" Width="250px" CssClass="Apariencia">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label53" runat="server" Text="Probabilidad:" Width="90px" CssClass="Apariencia"></asp:Label><asp:ImageButton
                                                                    ID="ImageButton6" runat="server" ImageUrl="~/Imagenes/Icons/information2.png" /><asp:PopupControlExtender
                                                                        ID="ImageButton6_PopupControlExtender" runat="server" DynamicServicePath="" Enabled="True"
                                                                        ExtenderControlID="" TargetControlID="ImageButton6" PopupControlID="pnlProbabilidad"
                                                                        Position="Bottom" BehaviorID="popup1">
                                                                    </asp:PopupControlExtender>
                                                            </td>
                                                            <td class="style5">
                                                                <asp:DropDownList ID="ddlProbabilidad" runat="server" Width="250px" CssClass="Apariencia">
                                                                    <asp:ListItem></asp:ListItem>
                                                                    <asp:ListItem>Insignificante</asp:ListItem>
                                                                    <asp:ListItem>Mínimo</asp:ListItem>
                                                                    <asp:ListItem>Moderado</asp:ListItem>
                                                                    <asp:ListItem>Alto</asp:ListItem>
                                                                    <asp:ListItem>Grave</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label54" runat="server" Text="Impacto:" Width="60px" CssClass="Apariencia"></asp:Label><asp:ImageButton
                                                                    ID="ImageButton5" runat="server" ImageUrl="~/Imagenes/Icons/information2.png" /><asp:PopupControlExtender
                                                                        ID="ImageButton5_PopupControlExtender" runat="server" DynamicServicePath="" Enabled="True"
                                                                        ExtenderControlID="" TargetControlID="ImageButton5" BehaviorID="popup" PopupControlID="pnlImpacto"
                                                                        Position="Bottom">
                                                                    </asp:PopupControlExtender>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlImpacto" runat="server" Width="250px" CssClass="Apariencia">
                                                                    <asp:ListItem></asp:ListItem>
                                                                    <asp:ListItem>Poco Probable</asp:ListItem>
                                                                    <asp:ListItem>Eventual</asp:ListItem>
                                                                    <asp:ListItem>Probable</asp:ListItem>
                                                                    <asp:ListItem>Muy Probable</asp:ListItem>
                                                                    <asp:ListItem>Casi un hecho</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="4">
                                                                <table width="100%">
                                                                    <tr>
                                                                        <td class="style7">
                                                                        </td>
                                                                        <td align="left">
                                                                            <asp:TextBox ID="TextBox13" runat="server" Enabled="False" Rows="2" Style="margin-left: 0px"
                                                                                TextMode="MultiLine" Width="515px"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                            </td>
                                                        </tr>
                                                        <tr align="center">
                                                            <td colspan="4">
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png" />
                                                                        </td>
                                                                        <td>
                                                                            <asp:ImageButton ID="btnImgCancelarRie" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                                                                OnClick="btnImgCancelarRie_Click_Click" />
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
                        </table>
                    </ContentTemplate>
                </asp:TabPanel>
            </asp:TabContainer>
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>
