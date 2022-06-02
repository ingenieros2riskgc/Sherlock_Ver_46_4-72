<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Gestion.aspx.cs" Inherits="ListasSarlaft.Formularios.Auditoria.Admin.WebForm1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
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
<%--            border: #060F40 1px solid;
            color: #060F40; Silver--%>
            border: Silver 1px solid;
            color: #060F40;             
            background: #ffffff;
        }        
    </style>
    <link href="../../../Styles/MyStyleSheet.css" rel="stylesheet" 
        type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Panel ID="pnlImpacto" runat="server" CssClass="popup" Width="400px" style="display:none">
        <table width="100%"  border="1" cellspacing="0" cellpadding="2" bordercolor="White">
            <tr align="right" bgcolor="#5D7B9D">
                <td>
                    <asp:ImageButton ID="btnClosepp1" runat="server" 
                        ImageUrl="~/Imagenes/Icons/dialog-close2.png" OnClientClick="$find('popup').hidePopup(); return false;"  />
                </td>
            </tr>
           <table  Width="100%">
                <tr>
                    <td>
                        <table Width="100%"align="left" border="1" cellspacing="0" cellpadding="2" bordercolor="White">
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
                                    <asp:Label ID="Label62" runat="server" Font-Names="Calibri" Font-Size="Small" 
                                        Text="Eventual:"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="Label63" runat="server" Font-Names="Calibri" Font-Size="Small" 
                                        Text="Esta sujeto a la conjución de circunstancias."></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label64" runat="server" Font-Names="Calibri" Font-Size="Small" 
                                        Text="Probable:"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="Label65" runat="server" Font-Names="Calibri" Font-Size="Small" 
                                        Text="Puede ocurrir en algún momento."></asp:Label>
                                </td>
                            </tr>
                </tr>
            </table>

        </table>
    </asp:Panel>
        <asp:Panel ID="pnlProbabilidad" runat="server" CssClass="popup" Width="400px" style="display:none">
        <table width="100%"  border="1" cellspacing="0" cellpadding="2" bordercolor="White">
            <tr align="right" bgcolor="#5D7B9D">
                <td>
                    <asp:ImageButton ID="ImageButton3" runat="server" 
                        ImageUrl="~/Imagenes/Icons/dialog-close2.png" OnClientClick="$find('popup1').hidePopup(); return false;"  />
                </td>
            </tr>
            <table >
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
                                    <asp:Label ID="Label58" runat="server" Font-Names="Calibri" Font-Size="Small" 
                                        Text="Mínimo:"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="Label59" runat="server" Font-Names="Calibri" Font-Size="Small" 
                                        Text="De poca importancia, con leve impacto y puede acompañarse de perdidas financieras bajas."></asp:Label>
                                </td>
                            </tr>
                </tr>
            </table> 
        </table>
    </asp:Panel>
    </div>

    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
        SelectCommand="SELECT [IdEnfoque], [IdObjetivo], [Numero], [Descripcion] FROM [Parametrizacion].[Enfoques]">
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
        SelectCommand="SELECT [IdDetalleEnfoque], [IdEnfoque], [Descripcion], [Numero] FROM [Parametrizacion].[DetalleEnfoques]">
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
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
    <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
        SelectCommand="SELECT [IdHallazgo], [IdAuditoria], [IdStandar], [IdDetalleEnfoque], [Observacion], [Tipo], [IdObjetivo], [NivelHallazgo], [IdTiposHallazgo], [ComentarioAuditado] FROM [Auditoria].[Hallazgo]">
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSource5" runat="server" ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
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
    <asp:SqlDataSource ID="SqlDataSource6" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>" 
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
    <%--<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>--%>
    <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" 
        Height="1020px" Width="996px">
        <asp:TabPanel runat="server" HeaderText="Hallazgos" ID="TabPanel1" Font-Underline="True">
            <ContentTemplate>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <table width="100%" bgcolor="#EEEEEE">
                            <tr align="center" bgcolor="#333399">
                                <td colspan="4">
                                    <asp:Label ID="Label3" runat="server" Text="Hallazgos" CssClass="AparienciaTitulo"
                                        ForeColor="White"></asp:Label>
                                </td>
                            </tr>
                            <tr align="left">
                                <td>
                                    <asp:Label ID="Label17" runat="server" Text="Objetivo:" Width="100px" CssClass="Apariencia"></asp:Label>
                                </td>
                                <td class="style3">
                                    <asp:DropDownList ID="DropDownList5" runat="server" Width="350px" CssClass="Apariencia">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:Label ID="Label4" runat="server" Text="Estandar:" Width="100px" CssClass="Apariencia"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBox5" runat="server" Enabled="False" Width="200px" TextMode="SingleLine"
                                        CssClass="Apariencia"></asp:TextBox>
                                </td>
                            </tr>
                            <tr align="center" bgcolor="#5D7B9D">
                                <td colspan="4">
                                    <asp:Label ID="Label5" runat="server" Text="Listado de Enfoques Por Objetivo" CssClass="AparienciaTitulo" ForeColor="White"></asp:Label>
                                </td>
                            </tr>
                            <tr align="center">
                                <td colspan="4">
                                    <table width="100%" border="1">
                                        <tr align="center">
                                            <td>
                                                <br />
                                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                                    DataKeyNames="IdEnfoque" DataSourceID="SqlDataSource1" CssClass="Apariencia" ForeColor="#333333"
                                                    GridLines="None" ShowHeaderWhenEmpty="True" AllowPaging="True" AllowSorting="True"
                                                    HeaderStyle-CssClass="gridViewHeader">
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
                                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
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
                                                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                                    DataKeyNames="IdDetalleEnfoque" DataSourceID="SqlDataSource2" Font-Names="Calibri"
                                                    ForeColor="#333333" GridLines="None" ShowHeaderWhenEmpty="True" AllowPaging="True"
                                                    AllowSorting="True" HeaderStyle-CssClass="gridViewHeader" CssClass="Apariencia">
                                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                    <Columns>
                                                        <asp:BoundField DataField="IdDetalleEnfoque" HeaderText="IdDetalleEnfoque" InsertVisible="False"
                                                            ReadOnly="True" SortExpression="IdDetalleEnfoque" Visible="False" />
                                                        <asp:BoundField DataField="IdEnfoque" HeaderText="IdEnfoque" SortExpression="IdEnfoque"
                                                            Visible="False" />
                                                        <asp:BoundField DataField="Numero" HeaderText="Literal" SortExpression="Numero" />
                                                        <asp:BoundField DataField="Descripcion" HeaderText="Descripción" SortExpression="Descripcion" />
                                                        <asp:CommandField ButtonType="Image" HeaderText="Acción" SelectImageUrl="~/Imagenes/Icons/edit.png"
                                                            ShowSelectButton="True">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:CommandField>
                                                    </Columns>
                                                    <EditRowStyle BackColor="#999999" />
                                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
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
                                    <asp:Label ID="Label14" runat="server" Text="Detalle de Hallazgos" CssClass="AparienciaTitulo" ForeColor="White"></asp:Label>
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
                                                            <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                                                DataKeyNames="IdHallazgo" DataSourceID="SqlDataSource3" Font-Names="Calibri"
                                                                ForeColor="#333333" GridLines="None" ShowHeaderWhenEmpty="True" AllowPaging="True"
                                                                AllowSorting="True" HeaderStyle-CssClass="gridViewHeader" CssClass="Apariencia" OnSelectedIndexChanged="GridView3_SelectedIndexChanged">
                                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                                <Columns>
                                                                    <asp:BoundField DataField="IdStandar" HeaderText="Estándar" SortExpression="IdStandar" />
                                                                    <asp:BoundField DataField="IdObjetivo" HeaderText="Objetivo" SortExpression="IdObjetivo" />
                                                                    <asp:BoundField DataField="IdDetalleEnfoque" HeaderText="Literal Enfoque" SortExpression="IdDetalleEnfoque" />
                                                                    <asp:BoundField DataField="Tipo" HeaderText="Estado" SortExpression="Tipo" />
                                                                    <asp:BoundField DataField="Observacion" HeaderText="Hallazgos" SortExpression="Observacion" />
                                                                    <asp:BoundField DataField="IdHallazgo" HeaderText="IdHallazgo" InsertVisible="False"
                                                                        ReadOnly="True" SortExpression="IdHallazgo" Visible="False" />
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
                                                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
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
                                                <asp:Button ID="btnIrAuditoria1" runat="server" Text="Ir a Auditoria" 
                                                    Font-Names="Calibri" Font-Size="Small" onclick="btnIrAuditoria1_Click" />
                                            </td>
                                            <td>
                                                <asp:Button ID="Button3" runat="server" Text="Ver..." Font-Names="Calibri" 
                                                    Font-Size="Small"/>
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
                                                <asp:Panel ID="Panel3" runat="server" Visible="False">
                                                    <table width="100%" border="1" cellspacing="0" cellpadding="2" bordercolor="White">
                                                        <tr align="left">
                                                            <td>
                                                                <asp:Label ID="Label6" runat="server" Text="Hallazgos:" Width="100px" CssClass="Apariencia"></asp:Label>
                                                            </td>
                                                            <td class="style3">
                                                                <asp:TextBox ID="TextBox1" runat="server" Width="429px" TextMode="MultiLine" CssClass="Apariencia" Rows="3"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label7" runat="server" Text="Tipo Hallazgo:" Width="137px" CssClass="Apariencia"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlTipoHallazgo" runat="server" Width="250px" CssClass="Apariencia" OnSelectedIndexChanged="ddlTipoHallazgo_SelectedIndexChanged">
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
                                                                <asp:Label ID="Label8" runat="server" Text="Comentario del Auditado:" Width="100px"
                                                                    CssClass="Apariencia"></asp:Label>
                                                            </td>
                                                            <td colspan="3">
                                                                <asp:TextBox ID="TextBox2" runat="server" Width="429px" TextMode="MultiLine" CssClass="Apariencia" Rows="2"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr align="left">
                                                            <td colspan="2">
                                                                <table width="100%">
                                                                    <tr>
                                                                        <td class="style1">
                                                                            <asp:CheckBox ID="CheckBox1" Text="Aplica criterio propio: " runat="server" TextAlign="Left" CssClass="Apariencia"/>
                                                                        </td>
                                                                        <td>
                                                                            <asp:CheckBox ID="CheckBox2" Text="Nivel Gerencial: " runat="server" TextAlign="Left" CssClass="Apariencia"/>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label10" runat="server" Text="Estado hallazgo:" Width="119px" CssClass="Apariencia">
                                                                </asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlEstadoHallazgo" runat="server" Width="187px" CssClass="Apariencia">
                                                                    <asp:ListItem></asp:ListItem>
                                                                    <asp:ListItem>Por Mejorar</asp:ListItem>
                                                                    <asp:ListItem>Positivo</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label9" runat="server" Text="Usuario:" Width="119px" CssClass="Apariencia"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label11" runat="server" Text="" Width="119px" CssClass="Apariencia"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label12" runat="server" Text="Fecha:" Width="119px" CssClass="Apariencia"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label13" runat="server" Text="" Width="119px" CssClass="Apariencia"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr align="center">
                                                            <td colspan="4">
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
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </ContentTemplate>
        </asp:TabPanel>
        <asp:TabPanel ID="TabPanel2" runat="server" HeaderText="Recomendaciones" >
            <ContentTemplate>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <table width="100%" bgcolor="#EEEEEE">
                            <tr align="center" bgcolor="#333399">
                                <td colspan="4">
                                    <asp:Label ID="Label1" runat="server" Text="Recomendaciones" CssClass="AparienciaTitulo" ForeColor="White"></asp:Label>
                                </td>
                            </tr>
                            <tr align="center" bgcolor="#5D7B9D">
                                <td colspan="4">
                                    <asp:Label ID="Label16" runat="server" Text="Consulta del Hallazgo" CssClass="AparienciaTitulo" ForeColor="White"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="style4">
                                    <asp:Label ID="Label2" runat="server" Text="Estándar:" CssClass="Apariencia"></asp:Label>
                                </td>
                                <td class="style3">
                                    <asp:Label ID="Label18" runat="server" CssClass="Apariencia"></asp:Label>
                                </td>
                                <td class="style1">
                                    <asp:Label ID="Label15" runat="server" Text="Objetivo:" CssClass="Apariencia"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="Label19" runat="server" CssClass="Apariencia"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="style4">
                                    <asp:Label ID="Label20" runat="server" Text="Nro Enfoque:" CssClass="Apariencia"></asp:Label>
                                </td>
                                <td class="style3">
                                    <asp:Label ID="Label21" runat="server" CssClass="Apariencia"></asp:Label>
                                </td>
                                <td class="style1">
                                    <asp:Label ID="Label22" runat="server" Text="Tipo:" CssClass="Apariencia"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="Label23" runat="server" CssClass="Apariencia"></asp:Label>
                                </td>
                            </tr>
                            <tr align="center">
                                <td colspan="4">
                                    <table width="100%" border="1">
                                        <tr align="center">
                                            <td>
                                                <br />
                                                <asp:GridView ID="GridView4" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                                    DataKeyNames="IdHallazgo" DataSourceID="SqlDataSource4" Font-Names="Calibri"
                                                    ForeColor="#333333" GridLines="None" ShowHeaderWhenEmpty="True" AllowPaging="True"
                                                    AllowSorting="True" HeaderStyle-CssClass="gridViewHeader" CssClass="Apariencia">
                                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                    <Columns>
                                                        <asp:BoundField DataField="IdHallazgo" HeaderText="Código" InsertVisible="False"
                                                            ReadOnly="True" SortExpression="IdHallazgo" />
                                                        <asp:BoundField DataField="IdAuditoria" HeaderText="IdAuditoria" SortExpression="IdAuditoria"
                                                            Visible="False" />
                                                        <asp:BoundField DataField="IdStandar" HeaderText="IdStandar" SortExpression="IdStandar"
                                                            Visible="False" />
                                                        <asp:BoundField DataField="IdDetalleEnfoque" HeaderText="IdDetalleEnfoque" SortExpression="IdDetalleEnfoque"
                                                            Visible="False" />
                                                        <asp:BoundField DataField="Observacion" HeaderText="Hallazgo" 
                                                            SortExpression="Observacion" />
                                                        <asp:BoundField DataField="Tipo" HeaderText="Tipo" SortExpression="Tipo" Visible="False" />
                                                        <asp:BoundField DataField="IdObjetivo" HeaderText="IdObjetivo" SortExpression="IdObjetivo"
                                                            Visible="False" />
                                                        <asp:BoundField DataField="NivelHallazgo" HeaderText="NivelHallazgo" SortExpression="NivelHallazgo"
                                                            Visible="False" />
                                                        <asp:BoundField DataField="IdTiposHallazgo" HeaderText="IdTiposHallazgo" SortExpression="IdTiposHallazgo"
                                                            Visible="False" />
                                                        <asp:BoundField DataField="ComentarioAuditado" HeaderText="ComentarioAuditado" SortExpression="ComentarioAuditado"
                                                            Visible="False" />
                                                        <asp:CommandField ButtonType="Image" HeaderText="Acción" SelectImageUrl="~/Imagenes/Icons/edit.png"
                                                            ShowSelectButton="True">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:CommandField>
                                                    </Columns>
                                                    <EditRowStyle BackColor="#999999" />
                                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
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
                            <tr align="center">
                                <td colspan="4">
                                    <asp:Button ID="btnIrAuditoria2" runat="server" Text="Ir a Auditoría" 
                                        Font-Names="Calibri" Font-Size="Small"/>
                                </td>
                            </tr>
                            <tr align="center" bgcolor="#5D7B9D">
                                <td colspan="4">
                                    <asp:Label ID="Label32" runat="server" Text="Detalle de Recomendaciones" CssClass="AparienciaTitulo" ForeColor="White"></asp:Label>
                                </td>
                            </tr>
                            <tr align="center">
                                <td colspan="4">
                                    <table width="100%" border="1">
                                        <tr align="center">
                                            <td>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <br />
                                                            <asp:GridView ID="GridView5" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                                                DataKeyNames="IdRecomendacion" DataSourceID="SqlDataSource5" Font-Names="Calibri"
                                                                ForeColor="#333333" GridLines="None" ShowHeaderWhenEmpty="True" AllowPaging="True"
                                                                AllowSorting="True" HeaderStyle-CssClass="gridViewHeader" 
                                                                onselectedindexchanged="GridView5_SelectedIndexChanged">
                                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                                <Columns>
                                                                    <asp:BoundField DataField="IdRecomendacion" HeaderText="IdRecomendacion" InsertVisible="False"
                                                                        ReadOnly="True" SortExpression="IdRecomendacion" Visible="False" />
                                                                    <asp:BoundField DataField="NumeroRecomendacion" HeaderText="NumeroRecomendacion"
                                                                        SortExpression="NumeroRecomendacion" Visible="False" />
                                                                    <asp:BoundField DataField="IdHallazgo" HeaderText="IdHallazgo" SortExpression="IdHallazgo"
                                                                        Visible="False" />
                                                                    <asp:BoundField DataField="IdSubdependencia" HeaderText="Subdependencia Auditada"
                                                                        SortExpression="IdSubdependencia" />
                                                                    <asp:BoundField DataField="IdSubdependenciaRespuesta" HeaderText="Subdependencia Respuesta"
                                                                        SortExpression="IdSubdependenciaRespuesta" />
                                                                    <asp:BoundField DataField="IdSubproceso" HeaderText="Subproceso" SortExpression="IdSubproceso" />
                                                                    <asp:BoundField DataField="Observacion" HeaderText="Recomendación" SortExpression="Observacion" />
                                                                    <asp:BoundField DataField="FechaRegistro" HeaderText="FechaRegistro" SortExpression="FechaRegistro"
                                                                        Visible="False" />
                                                                    <asp:BoundField DataField="IdUsuario" HeaderText="IdUsuario" SortExpression="IdUsuario"
                                                                        Visible="False" />
                                                                    <asp:BoundField DataField="Respuesta" HeaderText="Respuesta" SortExpression="Respuesta"
                                                                        Visible="False" />
                                                                    <asp:BoundField DataField="Estado" HeaderText="Estado" SortExpression="Estado" Visible="False" />
                                                                    <asp:TemplateField HeaderText="Acción" ShowHeader="False">
                                                                        <ItemTemplate>
                                                                            <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" CommandName="Select"
                                                                                ImageUrl="~/Imagenes/Icons/edit.png" Text="Seleccionar" />
                                                                            <asp:ImageButton ID="ImageButton2" runat="server" CausesValidation="False" CommandName="Delete"
                                                                                ImageUrl="~/Imagenes/Icons/delete.png" Text="Eliminar" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <EditRowStyle BackColor="#999999" />
                                                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
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
                                                            <asp:ImageButton ID="imgBtnInsertarRec" runat="server" CausesValidation="False" CommandName="Insert"
                                                                ImageUrl="~/Imagenes/Icons/Add.png" Text="Insert" OnClick="imgBtnInsertarRec_Click" />
                                                        </td>
                                                    </tr>
                                                </table>
                                                <br />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <table width="100%">
                                        <tr>
                                            <td>
                                                <asp:Panel ID="Panel2" runat="server" Visible="false">
                                                    <table width="100%" border="1" cellspacing="0" cellpadding="2" bordercolor="White">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label24" runat="server" Text="Tipo de Auditoría:" Width="167px" CssClass="Apariencia"></asp:Label>
                                                            </td>
                                                            <td class="style5">
                                                                <asp:Label ID="Label26" runat="server" CssClass="Apariencia"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label25" runat="server" Text="Subdependencia Respuesta:" Width="208px"
                                                                    CssClass="Apariencia"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="DropDownList1" runat="server" Width="254px" 
                                                                    CssClass="Apariencia" 
                                                                    OnSelectedIndexChanged="ddlTipoHallazgo_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label27" runat="server" Text="Dependencia Auditada:" Width="167px"
                                                                    CssClass="Apariencia"></asp:Label>
                                                            </td>
                                                            <td class="style5">
                                                                <asp:TextBox ID="TextBox3" runat="server" Width="250px" TextMode="SingleLine" CssClass="Apariencia" Rows="1" Enabled="False"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label29" runat="server" Text="Subdependencia:" Width="208px" CssClass="Apariencia"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="TextBox4" runat="server" Width="250px" TextMode="SingleLine" CssClass="Apariencia" Rows="1" Enabled="False"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label28" runat="server" Text="Proceso Auditado:" Width="167px" CssClass="Apariencia"></asp:Label>
                                                            </td>
                                                            <td class="style5">
                                                                <asp:TextBox ID="TextBox6" runat="server" Width="250px" TextMode="SingleLine" CssClass="Apariencia" Rows="1"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label30" runat="server" Text="Subproceso:" Width="208px" CssClass="Apariencia"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="DropDownList2" runat="server" Width="254px" 
                                                                    CssClass="Apariencia" 
                                                                    OnSelectedIndexChanged="ddlTipoHallazgo_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label31" runat="server" Text="Recomendación:" Width="167px" CssClass="Apariencia"></asp:Label>
                                                            </td>
                                                            <td colspan="3">
                                                                <asp:TextBox ID="TextBox7" runat="server" Width="751px" CssClass="Apariencia"
                                                                    Rows="3" TextMode="MultiLine"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr align="center">
                                                            <td colspan="4">
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png" />
                                                                        </td>
                                                                        <td>
                                                                            <asp:ImageButton ID="btnImgCancelarRec_Click" runat="server" 
                                                                                ImageUrl="~/Imagenes/Icons/cancel.png" 
                                                                                onclick="btnImgCancelarRec_Click_Click" />
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
                </asp:UpdatePanel>
            </ContentTemplate>
        </asp:TabPanel>
        <asp:TabPanel ID="TabPanel3" runat="server" HeaderText="Riesgos">
            <ContentTemplate>
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <table width="100%" bgcolor="#EEEEEE">
                            <tr align="center" bgcolor="#333399">
                                <td colspan="4">
                                    <asp:Label ID="Label33" runat="server" Text="Riesgos" CssClass="AparienciaTitulo" ForeColor="White"></asp:Label>
                                </td>
                            </tr>
                            <tr align="center" bgcolor="#5D7B9D">
                                <td colspan="4">
                                    <asp:Label ID="Label34" runat="server" Text="Consulta del Hallazgo" CssClass="AparienciaTitulo" ForeColor="White"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="style4">
                                    <asp:Label ID="Label35" runat="server" Text="Estándar:" CssClass="Apariencia"></asp:Label>
                                </td>
                                <td class="style3">
                                    <asp:Label ID="Label36" runat="server" CssClass="Apariencia"></asp:Label>
                                </td>
                                <td class="style1">
                                    <asp:Label ID="Label37" runat="server" Text="Objetivo:" CssClass="Apariencia"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="Label38" runat="server" CssClass="Apariencia"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="style4">
                                    <asp:Label ID="Label39" runat="server" Text="Nro Enfoque:" CssClass="Apariencia"></asp:Label>
                                </td>
                                <td class="style3">
                                    <asp:Label ID="Label40" runat="server" CssClass="Apariencia"></asp:Label>
                                </td>
                                <td class="style1">
                                    <asp:Label ID="Label41" runat="server" Text="Tipo:" CssClass="Apariencia"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="Label42" runat="server" CssClass="Apariencia"></asp:Label>
                                </td>
                            </tr>
                            <tr align="center">
                                <td colspan="4">
                                    <table width="100%" border="1">
                                        <tr align="center">
                                            <td>
                                                <br />
                                                <asp:GridView ID="GridView6" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                                    DataKeyNames="IdHallazgo" DataSourceID="SqlDataSource4" Font-Names="Calibri"
                                                    ForeColor="#333333" GridLines="None" ShowHeaderWhenEmpty="True" AllowPaging="True"
                                                    AllowSorting="True" HeaderStyle-CssClass="gridViewHeader" CssClass="Apariencia">
                                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                    <Columns>
                                                        <asp:BoundField DataField="IdHallazgo" HeaderText="Código" InsertVisible="False"
                                                            ReadOnly="True" SortExpression="IdHallazgo" />
                                                        <asp:BoundField DataField="IdAuditoria" HeaderText="IdAuditoria" SortExpression="IdAuditoria"
                                                            Visible="False" />
                                                        <asp:BoundField DataField="IdStandar" HeaderText="IdStandar" SortExpression="IdStandar"
                                                            Visible="False" />
                                                        <asp:BoundField DataField="IdDetalleEnfoque" HeaderText="IdDetalleEnfoque" SortExpression="IdDetalleEnfoque"
                                                            Visible="False" />
                                                        <asp:BoundField DataField="Observacion" HeaderText="Hallazgo" 
                                                            SortExpression="Observacion" />
                                                        <asp:BoundField DataField="Tipo" HeaderText="Tipo" SortExpression="Tipo" Visible="False" />
                                                        <asp:BoundField DataField="IdObjetivo" HeaderText="IdObjetivo" SortExpression="IdObjetivo"
                                                            Visible="False" />
                                                        <asp:BoundField DataField="NivelHallazgo" HeaderText="NivelHallazgo" SortExpression="NivelHallazgo"
                                                            Visible="False" />
                                                        <asp:BoundField DataField="IdTiposHallazgo" HeaderText="IdTiposHallazgo" SortExpression="IdTiposHallazgo"
                                                            Visible="False" />
                                                        <asp:BoundField DataField="ComentarioAuditado" HeaderText="ComentarioAuditado" SortExpression="ComentarioAuditado"
                                                            Visible="False" />
                                                        <asp:CommandField ButtonType="Image" HeaderText="Acción" SelectImageUrl="~/Imagenes/Icons/edit.png"
                                                            ShowSelectButton="True">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:CommandField>
                                                    </Columns>
                                                    <EditRowStyle BackColor="#999999" />
                                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
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
                            <tr align="center">
                                <td colspan="4">
                                    <asp:Button ID="btnIrAuditoria3" runat="server" Text="Ir a Auditoría"  
                                        Font-Names="Calibri" Font-Size="Small"/>
                                </td>
                            </tr>
                            <tr align="center" bgcolor="#5D7B9D">
                                <td colspan="4">
                                    <asp:Label ID="Label43" runat="server" Text="Detalle de Riesgos" CssClass="AparienciaTitulo" ForeColor="White"></asp:Label>
                                </td>
                            </tr>
                            <tr align="center">
                                <td colspan="4">
                                    <table width="100%" border="1">
                                        <tr align="center">
                                            <td>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <br />
                                                            <asp:GridView ID="GridView7" runat="server" AutoGenerateColumns="False" 
                                                    CellPadding="4" DataSourceID="SqlDataSource6" 
                                                    ForeColor="#333333" GridLines="None" ShowHeaderWhenEmpty="True" AllowPaging="True"
                                                    AllowSorting="True" HeaderStyle-CssClass="gridViewHeader" 
                                                    CssClass="Apariencia" DataKeyNames="IdRiesgo" 
                                                                onselectedindexchanged="GridView7_SelectedIndexChanged">
                                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                    <Columns>
                                                        <asp:BoundField DataField="IdRiesgo" HeaderText="IdRiesgo" 
                                                            InsertVisible="False" ReadOnly="True" SortExpression="IdRiesgo" 
                                                            Visible="False" />
                                                        <asp:BoundField DataField="NumeroRiesgo" HeaderText="NumeroRiesgo" 
                                                            SortExpression="NumeroRiesgo" Visible="False" />
                                                        <asp:BoundField DataField="IdHallazgo" HeaderText="IdHallazgo" 
                                                            SortExpression="IdHallazgo" Visible="False" />
                                                        <asp:BoundField DataField="IdTipoRiesgo" HeaderText="Tipo de Riesgo" 
                                                            SortExpression="IdTipoRiesgo" />
                                                        <asp:BoundField DataField="IdSubdependencia" HeaderText="Subdependencia" 
                                                            SortExpression="IdSubdependencia" />
                                                        <asp:BoundField DataField="IdProceso" HeaderText="IdProceso" 
                                                            SortExpression="IdProceso" Visible="False" />
                                                        <asp:BoundField DataField="Estado" HeaderText="Estado" 
                                                            SortExpression="Estado" />
                                                        <asp:BoundField DataField="Observacion" HeaderText="Observacion" 
                                                            SortExpression="Observacion" />
                                                        <asp:BoundField DataField="FechaRegistro" HeaderText="FechaRegistro" 
                                                            SortExpression="FechaRegistro" />
                                                        <asp:BoundField DataField="IdUsuario" HeaderText="IdUsuario" 
                                                            SortExpression="IdUsuario" Visible="False" />
                                                        <asp:BoundField DataField="ComentarioAuditado" HeaderText="ComentarioAuditado" 
                                                            SortExpression="ComentarioAuditado" Visible="False" />
                                                        <asp:TemplateField HeaderText="Acción" ShowHeader="False">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" 
                                                                    CommandName="Select" ImageUrl="~/Imagenes/Icons/edit.png" Text="Seleccionar" />
                                                                <asp:ImageButton ID="ImageButton2" runat="server" CausesValidation="False" 
                                                                    CommandName="Delete" ImageUrl="~/Imagenes/Icons/delete.png" Text="Eliminar" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <EditRowStyle BackColor="#999999" />
                                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
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
                            <tr>
                                <td colspan="4">
                                    <table width="100%">
                                        <tr>
                                            <td>
                                                <asp:Panel ID="Panel1" runat="server" Visible="false">
                                                    <table width="100%" border="1" cellspacing="0" cellpadding="2" bordercolor="White">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label51" runat="server" Text="Nombre del Riesgo:" Width="167px" CssClass="Apariencia"></asp:Label>
                                                            </td>
                                                            <td colspan="3">
                                                                <asp:TextBox ID="TextBox11" runat="server" Width="756px" CssClass="Apariencia"
                                                                    Rows="1" TextMode="SingleLine"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label52" runat="server" Text="Descripción del Riesgo:" Width="167px" CssClass="Apariencia"></asp:Label>
                                                            </td>
                                                            <td colspan="3">
                                                                <asp:TextBox ID="TextBox12" runat="server" Width="756px" CssClass="Apariencia"
                                                                    Rows="3" TextMode="MultiLine"></asp:TextBox>
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
                                                                <asp:Label ID="Label46" runat="server" Text="Tipo de Riesgo:" Width="208px"
                                                                    CssClass="Apariencia"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="DropDownList3" runat="server" Width="250px" CssClass="Apariencia" OnSelectedIndexChanged="ddlTipoHallazgo_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label47" runat="server" Text="Dependencia:" Width="167px"
                                                                    CssClass="Apariencia"></asp:Label>
                                                            </td>
                                                            <td class="style5">
                                                                <asp:TextBox ID="TextBox8" runat="server" Width="250px" TextMode="SingleLine" CssClass="Apariencia" Rows="1" Enabled="False"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label48" runat="server" Text="Subdependencia:" Width="208px" CssClass="Apariencia"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="TextBox9" runat="server" Width="250px" TextMode="SingleLine" CssClass="Apariencia" Rows="1" Enabled="False"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label49" runat="server" Text="Proceso:" Width="167px" CssClass="Apariencia"></asp:Label>
                                                            </td>
                                                            <td class="style5">
                                                                <asp:TextBox ID="TextBox10" runat="server" Width="250px" TextMode="SingleLine" CssClass="Apariencia" Rows="1"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label50" runat="server" Text="Subproceso:" Width="208px" CssClass="Apariencia"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="DropDownList4" runat="server" Width="250px" CssClass="Apariencia" OnSelectedIndexChanged="ddlTipoHallazgo_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label53" runat="server" Text="Probabilidad:" Width="90px" 
                                                                    CssClass="Apariencia"></asp:Label>
                                                                <asp:ImageButton ID="ImageButton6" runat="server" 
                                                                    ImageUrl="~/Imagenes/Icons/information2.png" />

                                                                <asp:PopupControlExtender ID="ImageButton6_PopupControlExtender" runat="server" 
                                                                    DynamicServicePath="" Enabled="True" ExtenderControlID="" 
                                                                    TargetControlID="ImageButton6" PopupControlID="pnlProbabilidad" Position="Right" BehaviorID="popup1" >
                                                                </asp:PopupControlExtender>

                                                            </td>
                                                            <td class="style5">
                                                                <asp:DropDownList ID="ddlProbabilidad" runat="server" Width="250px" CssClass="Apariencia" OnSelectedIndexChanged="ddlProbabilidad_SelectedIndexChanged">
                                                                    <asp:ListItem></asp:ListItem>
                                                                    <asp:ListItem>Insignificante</asp:ListItem>
                                                                    <asp:ListItem>Mínimo</asp:ListItem>
                                                                    <asp:ListItem>Moderado</asp:ListItem>
                                                                    <asp:ListItem>Alto</asp:ListItem>
                                                                    <asp:ListItem>Grave</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label54" runat="server" Text="Impacto:" Width="60px" 
                                                                    CssClass="Apariencia"></asp:Label>
                                                                <asp:ImageButton ID="ImageButton5" runat="server" ImageUrl="~/Imagenes/Icons/information2.png" />
                                                                <asp:PopupControlExtender ID="ImageButton5_PopupControlExtender" runat="server" DynamicServicePath=""
                                                                    Enabled="True" ExtenderControlID="" TargetControlID="ImageButton5" BehaviorID="popup" 
                                                                    PopupControlID="pnlImpacto" Position="Right">
                                                                </asp:PopupControlExtender>
 
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlImpacto" runat="server" Width="250px" CssClass="Apariencia" OnSelectedIndexChanged="ddlImpacto_SelectedIndexChanged">
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
                                                                <asp:Label ID="Label55" runat="server" Text="Tratamiento:" Width="167px" CssClass="Apariencia"></asp:Label>
                                                            </td>
                                                            <td colspan="3">
                                                                <asp:CheckBoxList ID="CheckBoxList1" runat="server"  CssClass="Apariencia">
                                                                    <asp:ListItem>EVITARLO</asp:ListItem>
                                                                    <asp:ListItem>MINIMIZARLO</asp:ListItem>
                                                                    <asp:ListItem>TRANSFERIRLO</asp:ListItem>
                                                                    <asp:ListItem>ACEPTARLO</asp:ListItem>
                                                                    <asp:ListItem>IGNORARLO</asp:ListItem>
                                                                </asp:CheckBoxList>
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
                                                                            <asp:ImageButton ID="btnImgCancelarRie" runat="server" 
                                                                                ImageUrl="~/Imagenes/Icons/cancel.png" 
                                                                                onclick="btnImgCancelarRie_Click_Click" />
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
                </asp:UpdatePanel>
            </ContentTemplate>
        </asp:TabPanel>
        &nbsp;&nbsp;&nbsp;
    </asp:TabContainer>
    </form>

</body>

</html>
