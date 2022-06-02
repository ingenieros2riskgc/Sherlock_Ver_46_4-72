<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ControlDocumentoVersiones.ascx.cs" Inherits="ListasSarlaft.UserControls.Proceso.Procesos.ControlDocumentoVersiones" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Src="~/UserControls/Sitio/OkMessageBox.ascx" TagPrefix="uc" TagName="OkMessageBox" %>
<link href="../../../Styles/AdminSitio.css" rel="stylesheet" type="text/css" />
<style type="text/css">
    div.ajax__calendar_days table tr td {
        padding-right: 0px;
    }

    div.ajax__calendar_body {
        width: 225px;
    }

    div.ajax__calendar_container {
        width: 225px;
    }
</style>
<asp:SqlDataSource ID="SqlDataSource200" runat="server" ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
    DeleteCommand="DELETE FROM [Notificaciones].[CorreosEnviados] WHERE [IdCorreosEnviados] = @IdCorreosEnviados"
    InsertCommand="INSERT INTO [Notificaciones].[CorreosEnviados] ([IdEvento], [Destinatario], [Copia], [Otros], [Asunto], [Cuerpo], [Estado], [IdRegistro], [FechaEnvio], [FechaRegistro], [IdUsuario], [Tipo]) VALUES (@IdEvento, 
@Destinatario, @Copia, @Otros, @Asunto, @Cuerpo, @Estado, @IdRegistro, @FechaEnvio, @FechaRegistro, @IdUsuario, @Tipo) SET @NewParameter2=SCOPE_IDENTITY();"
    SelectCommand="SELECT [IdCorreosEnviados], [IdEvento], [Destinatario], [Copia], [Otros], [Asunto], [Cuerpo], [Estado], [IdRegistro], [FechaEnvio], [FechaRegistro], [IdUsuario] FROM [Notificaciones].[CorreosEnviados]"
    UpdateCommand="UPDATE [Notificaciones].[CorreosEnviados] SET [FechaEnvio] = @FechaEnvio, [Estado] = @Estado WHERE [IdCorreosEnviados] = @IdCorreosEnviados" OnInserted="SqlDataSource200_Inserted">
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
    InsertCommand="INSERT INTO [Notificaciones].[CorreosRecordatorio] ([IdCorreosEnviados], [NroDiasRecordatorio], [FechaFinal], [Estado], [FechaRegistro], [IdUsuario]) VALUES (@IdCorreosEnviados, @NroDiasRecordatorio, CONVERT(datetime, @FechaFinal, 120), @Estado, @FechaRegistro, @IdUsuario)"
    SelectCommand="SELECT [IdCorreosRecordatorio], [IdCorreosEnviados], [NroDiasRecordatorio], [FechaFinal], [Estado], [FechaRegistro], [IdUsuario] FROM [Notificaciones].[CorreosRecordatorio]"
    UpdateCommand="UPDATE [Estado] = @Estado WHERE [IdCorreosRecordatorio] = @IdCorreosRecordatorio">
    <DeleteParameters>
        <asp:Parameter Name="IdCorreosRecordatorio" Type="Decimal" />
    </DeleteParameters>
    <InsertParameters>
        <asp:Parameter Name="IdCorreosEnviados" Type="Decimal" />
        <asp:Parameter Name="NroDiasRecordatorio" Type="Int32" />
        <asp:Parameter Name="FechaFinal" Type="String" />
        <asp:Parameter Name="Estado" Type="String" />
        <asp:Parameter Name="FechaRegistro" Type="DateTime" />
        <asp:Parameter Name="IdUsuario" Type="Decimal" />
    </InsertParameters>
    <UpdateParameters>
        <asp:Parameter Name="Estado" Type="String" />
        <asp:Parameter Name="IdCorreosRecordatorio" Type="Decimal" />
    </UpdateParameters>
</asp:SqlDataSource>
<asp:UpdatePanel ID="GEPbody" runat="server">
    <ContentTemplate>
        <uc:OkMessageBox ID="omb" runat="server" />
        <div class="TituloLabel" id="HeadCDV" runat="server">
            <asp:Label ID="Ltitulo" runat="server" ForeColor="White" Text="Control de Documentos y Versiones" Font-Bold="False"
                Font-Names="Calibri" Font-Size="Large"></asp:Label>

        </div>
        <div style="height: 20px"></div>

        <div id="divFiltrosBusqueda" class="ColumnStyle" runat="server">
            <div style="text-align: center">
                Filtros de búsqueda
            </div>
            <div class="TableContains">
                <table class="tabla" align="center" width="80%">
                    <tr>
                        <td class="RowsText">
                            <asp:Label ID="lCadenaValorFiltro" runat="server" Text="Cadena de Valor:" CssClass="Apariencia" Width="300px"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlCadenaValorFiltro" runat="server" Width="300px" CssClass="Apariencia" AutoPostBack="True" OnSelectedIndexChanged="ddlCadenaValorFiltro_SelectedIndexChanged"></asp:DropDownList>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="RowsText">
                            <asp:Label ID="lMacroprocesoFiltro" runat="server" Text="MacroProceso:" CssClass="Apariencia" Width="300px"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlMacroprocesoFiltro" runat="server" Width="300px" CssClass="Apariencia" AutoPostBack="True" OnSelectedIndexChanged="ddlMacroprocesoFiltro_SelectedIndexChanged"></asp:DropDownList>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="RowsText">
                            <asp:Label ID="lProcesoFiltro" runat="server" Text="Proceso:" CssClass="Apariencia" Width="300px"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlProcesoFiltro" runat="server" Width="300px" CssClass="Apariencia" AutoPostBack="True" OnSelectedIndexChanged="ddlProcesoFiltro_SelectedIndexChanged"></asp:DropDownList>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="RowsText">
                            <asp:Label ID="lSubprocesoFiltro" runat="server" Text="Subproceso:" CssClass="Apariencia" Width="300px"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlSubprocesoFiltro" runat="server" Width="300px" CssClass="Apariencia"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="RowsText">
                            <asp:Label ID="lNombreFiltro" runat="server" Text="Nombre Documento:" CssClass="Apariencia" Width="300px"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtNombreFiltro" runat="server" Width="300px" CssClass="Apariencia"></asp:TextBox>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="RowsText">
                            <asp:Label ID="lCodigoFiltro" runat="server" Text="Código:" CssClass="Apariencia" Width="300px"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCodigoFiltro" runat="server" Width="300px" CssClass="Apariencia"></asp:TextBox>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="RowsText">
                            <asp:Label ID="lFechaImplementacionFiltro" runat="server" Text="Fecha Implementación:" CssClass="Apariencia"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtFechaImplementacionFiltro" runat="server" CssClass="Apariencia" Width="300px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:CalendarExtender ID="CalendarExtenderFiltro" runat="server" Enabled="true" TargetControlID="txtFechaImplementacionFiltro"
                                Format="yyyy-MM-dd"></asp:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td class="RowsText">
                            <asp:Label ID="lTipoDocumentoFiltro" runat="server" Text="Tipo Documento:" CssClass="Apariencia" Width="300px"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlTipoDocumentoFiltro" runat="server" Width="300px" CssClass="Apariencia"></asp:DropDownList>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:ImageButton ID="btnBusquedaFiltro" runat="server" ImageUrl="~/Imagenes/Icons/conocimiento.png" ToolTip="Buscar" OnClick="btnBusquedaFiltro_Click" />
                            <asp:ImageButton ID="btnLimpiarFiltro" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png" ToolTip="Limpiar" OnClick="btnLimpiarFiltro_Click" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div id="BodyGridRNC" class="ColumnStyle" runat="server">
            <table class="tabla" align="center" width="100%">
                <tr align="center">
                    <td>
                        <asp:GridView ID="GVcontrolDocumento" runat="server" CellPadding="1"
                            ForeColor="#333333" AllowPaging="False" AllowSorting="True" AutoGenerateColumns="False"
                            ShowHeaderWhenEmpty="True" DataKeyNames="strUsuario,dtFechaRegistro,intIdMacroProceso,strCodigoDocumento,intVersionActual,
                                    intIdTipoDocumento,intidCargoResponsable,strNombreCargo,strUbicacionAlmacenamiento,strRecuperacion,strTiempoRetencionActivo,
                                    strTiempoRetencionInactivo,strDisposicionFinal,strMedioSoporte,strFormato,intIdUsuario,dtFechaModificacion,dtFechaEliminacion,intIdTipoProceso"
                            HeaderStyle-CssClass="gridViewHeader" BorderStyle="Solid" GridLines="Vertical"
                            CssClass="Apariencia" Font-Bold="False" OnRowCommand="GVcontrolDocumento_RowCommand" OnPageIndexChanging="GVcontrolDocumento_PageIndexChanging">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:BoundField DataField="intId" HeaderText="Código" SortExpression="intId" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="no-visible" HeaderStyle-CssClass="no-visible" />
                                <asp:TemplateField HeaderText="Proceso" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 75%">
                                            <asp:Label ID="proceso" runat="server" Text='<% # Bind("NombreProceso")%>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                    <HeaderStyle Wrap="false" HorizontalAlign="center" />
                                    <ItemStyle Wrap="false" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Nombre Documento" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 100%">
                                            <asp:Label ID="NombreDocumento" CssClass="GridDataWrap" runat="server" Text='<% # Bind("strNombreDocumento")%>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                    <HeaderStyle Wrap="false" HorizontalAlign="center" />
                                    <ItemStyle Wrap="false" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Tipo Documento" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 150px">
                                            <asp:Label ID="NombreTipoDocumento" runat="server" Text='<%# Bind("NombreTipoDocumento") %>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="dtFechaImplementacion" HeaderText="Fecha Implementación" ReadOnly="True" SortExpression="dtFechaImplementacion" ItemStyle-HorizontalAlign="Center" />
                                <asp:TemplateField HeaderText="Código" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 150px">
                                            <asp:Label ID="CodigoDocumento" runat="server" Text='<% # Bind("strCodigoDocumento")%>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                    <HeaderStyle Wrap="false" HorizontalAlign="center" />
                                    <ItemStyle Wrap="false" />
                                </asp:TemplateField>
                                <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Icons/select.png" Text="Seleccionar" HeaderText="Seleccionar" CommandName="Seleccionar" ItemStyle-HorizontalAlign="Center" />
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
                    <td>
                        <asp:ImageButton ID="btnInsertarNuevo" runat="server" CausesValidation="False" CommandName="Insert"
                            ImageUrl="~/Imagenes/Icons/Add.png" Text="Insert" OnClick="btnInsertarNuevo_Click" ToolTip="Insertar" />
                    </td>
                </tr>
            </table>
        </div>
        <div id="BodyFormCDV" class="ColumnStyle" runat="server" visible="false">
            <div id="form" class="TableContains">
                <table class="tabla" align="center" width="80%">
                    <tr>
                        <td class="RowsText">
                            <asp:Label ID="Lcodigo" runat="server" Text="Código Registro:" CssClass="Apariencia"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtId" runat="server" Enabled="False"
                                CssClass="Apariencia" Width="300px"></asp:TextBox>
                        </td>
                        <td></td>
                    </tr>
                    <tr id="CadenaValor">
                        <td class="RowsText">
                            <asp:Label ID="LcadenaValor" runat="server" Text="Cadena de Valor:" CssClass="Apariencia"></asp:Label></td>
                        <td>
                            <asp:DropDownList ID="ddlCadenaValor" runat="server" Width="300px"
                                CssClass="Apariencia" AutoPostBack="True"
                                DataTextField="NombreCadenaValor" DataValueField="IdCadenaValor"
                                OnSelectedIndexChanged="ddlCadenaValor_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="rfvCadenaValor" runat="server" ControlToValidate="ddlCadenaValor"
                                ErrorMessage="Debe ingresar la cadena de valor." ToolTip="Debe ingresar la cadena de valor."
                                ValidationGroup="ControlDocument" ForeColor="Red" InitialValue="0">*</asp:RequiredFieldValidator>
                            <asp:TextBox ID="tbxProcIndica" runat="server" Width="90px" MaxLength="20" CssClass="Apariencia" Visible="false"></asp:TextBox></td>
                    </tr>
                    <tr id="Macroproceso">
                        <td class="RowsText">
                            <asp:Label ID="LMacroproceso" runat="server" Text="Macroproceso:" CssClass="Apariencia"></asp:Label></td>
                        <td>
                            <asp:DropDownList ID="ddlMacroproceso" runat="server" Width="300px"
                                CssClass="Apariencia" AutoPostBack="True" DataTextField="Nombre" DataValueField="IdMacroproceso"
                                OnSelectedIndexChanged="ddlMacroproceso_SelectedIndexChanged">
                            </asp:DropDownList></td>
                        <td>
                            <asp:RequiredFieldValidator ID="rfvMacroProceso" runat="server" ControlToValidate="ddlMacroproceso"
                                ErrorMessage="Debe ingresar el Macroproceso." ToolTip="Debe ingresar el Macroproceso."
                                ValidationGroup="ControlDocument" ForeColor="Red" InitialValue="0">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr id="Proceso">
                        <td class="RowsText">
                            <asp:Label ID="Lproceso" runat="server" Text="Proceso:" CssClass="Apariencia"></asp:Label></td>
                        <td>
                            <asp:DropDownList ID="ddlProceso" runat="server" Width="300px" OnSelectedIndexChanged="ddlProceso_SelectedIndexChanged"
                                CssClass="Apariencia" AutoPostBack="True" DataTextField="Nombre" DataValueField="IdProceso">
                            </asp:DropDownList></td>
                        <td>
                            <asp:RequiredFieldValidator ID="rfvProceso" runat="server" ControlToValidate="ddlProceso"
                                ErrorMessage="Debe ingresar el Proceso." ToolTip="Debe ingresar el Proceso." Enabled="False"
                                ValidationGroup="iIndicador" ForeColor="Red" InitialValue="0">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr id="Subproceso">
                        <td class="RowsText">
                            <asp:Label ID="Lsubproceso" runat="server" Text="Subproceso:" CssClass="Apariencia"></asp:Label></td>
                        <td>
                            <asp:DropDownList ID="ddlSubproceso" runat="server" Width="300px" OnSelectedIndexChanged="ddlSubproceso_SelectedIndexChanged"
                                CssClass="Apariencia" AutoPostBack="True" DataTextField="Nombre" DataValueField="IdProceso">
                            </asp:DropDownList></td>
                        <td>
                            <asp:RequiredFieldValidator ID="rfvSubproceso" runat="server" ControlToValidate="ddlSubproceso"
                                ErrorMessage="Debe ingresar el Subproceso." ToolTip="Debe ingresar el Subproceso." Enabled="False"
                                ValidationGroup="iIndicador" ForeColor="Red" InitialValue="0">*</asp:RequiredFieldValidator>
                            <asp:Label ID="LprocesoSave" runat="server" CssClass="Apariencia" Visible="false"></asp:Label></td>
                        </td>
                    </tr>
                    <tr id="name">
                        <td class="RowsText">
                            <asp:Label ID="Lname" runat="server" Text="Nombre:" CssClass="Apariencia"></asp:Label></td>
                        </td>
                        <td>
                            <asp:TextBox ID="TXname" runat="server" CssClass="Apariencia" Width="300px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="RFVname" runat="server" ControlToValidate="TXname"
                                ErrorMessage="Debe ingresar el Nombre." ToolTip="Debe ingresar el Nombre."
                                ValidationGroup="ControlDocument" ForeColor="Red">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr id="codigo">
                        <td class="RowsText">
                            <asp:Label ID="LcodigoDoc" runat="server" Text="Código:" CssClass="Apariencia"></asp:Label></td>
                        </td>
                        <td>
                            <asp:TextBox ID="TXcodigoDoc" runat="server" CssClass="Apariencia" Width="300px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="RFVcodigo" runat="server" ControlToValidate="TXcodigoDoc"
                                ErrorMessage="Debe ingresar el Código." ToolTip="Debe ingresar el Código."
                                ValidationGroup="ControlDocument" ForeColor="Red">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="RowsText">
                            <asp:Label ID="Lversion" runat="server" Text="Versión Actual:" CssClass="Apariencia"></asp:Label></td>
                        </td>
                        <td>
                            <asp:TextBox ID="TXversion" runat="server" CssClass="Apariencia" Width="300px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="RFVversion" runat="server" ControlToValidate="TXversion"
                                ErrorMessage="Debe ingresar el Version Actual." ToolTip="Debe ingresar el Version Actual."
                                ValidationGroup="ControlDocument" ForeColor="Red">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="RowsText">
                            <asp:Label ID="LfechaImplementacion" runat="server" Text="Fecha Implementación:" CssClass="Apariencia"></asp:Label></td>
                        </td>
                        <td>
                            <asp:TextBox ID="TXfechaimplementacion" runat="server" CssClass="Apariencia" Width="300px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:CalendarExtender ID="CEfechaImplementacion" runat="server" Enabled="true" TargetControlID="TXfechaimplementacion"
                                Format="yyyy-MM-dd"></asp:CalendarExtender>
                            <asp:RequiredFieldValidator ID="rfvFechaImplementacion" runat="server" ControlToValidate="TXfechaimplementacion"
                                ErrorMessage="Debe ingresar la fecha de Implementacion." ToolTip="Debe ingresar la fecha de Implementacion."
                                ValidationGroup="ControlDocument" ForeColor="Red">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr style="display: none">
                        <td class="RowsText">
                            <asp:Label ID="LfechaActual" runat="server" Text="Fecha Actual:" CssClass="Apariencia"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TXfechaActual" runat="server" CssClass="Apariencia" Width="300px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:CalendarExtender ID="CEfechaActual" runat="server" Enabled="true" TargetControlID="TXfechaActual"
                                Format="yyyy-MM-dd"></asp:CalendarExtender>
                            <%-- <asp:RequiredFieldValidator ID="RFVfechaActual" runat="server" ControlToValidate="TXfechaActual"
                                    ErrorMessage="Debe ingresar la fecha Actual." ToolTip="Debe ingresar la fecha Actual."
                                    ValidationGroup="ControlDocument" ForeColor="Red">*</asp:RequiredFieldValidator>--%>
                        </td>
                    </tr>
                    <tr>
                        <td class="RowsText">
                            <asp:Label ID="LfechaUp" runat="server" Text="Fecha Modificación:" CssClass="Apariencia"></asp:Label></td>
                        </td>
                        <td>
                            <asp:TextBox ID="TXfechaUp" runat="server" CssClass="Apariencia" Width="300px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:CalendarExtender ID="CEfechaUp" runat="server" Enabled="true" TargetControlID="TXfechaUp"
                                Format="yyyy-MM-dd"></asp:CalendarExtender>
                            <asp:RequiredFieldValidator ID="RFVfechaMod" runat="server" ControlToValidate="TXfechaUp"
                                ErrorMessage="Debe ingresar la fecha de Modificacion." ToolTip="Debe ingresar la fecha de Modificacion."
                                ValidationGroup="ControlDocumentUP" ForeColor="Red">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="RowsText">
                            <asp:Label ID="LfechaDel" runat="server" Text="Fecha Eliminación:" CssClass="Apariencia"></asp:Label></td>
                        </td>
                        <td>
                            <asp:TextBox ID="TXfechaDel" runat="server" CssClass="Apariencia" Width="300px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:CalendarExtender ID="CEfechaDel" runat="server" Enabled="true" TargetControlID="TXfechaDel"
                                Format="yyyy-MM-dd"></asp:CalendarExtender>
                            <asp:RequiredFieldValidator ID="RFVfechaDel" runat="server" ControlToValidate="TXfechaDel"
                                ErrorMessage="Debe ingresar la fecha de Modificacion." ToolTip="Debe ingresar la fecha de Modificacion."
                                ValidationGroup="ControlDocumentUP" ForeColor="Red" Enabled="false">*</asp:RequiredFieldValidator>

                        </td>
                    </tr>
                    <tr>
                        <td class="RowsText">
                            <asp:Label ID="LtipoDoc" runat="server" Text="Tipo:" CssClass="Apariencia"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="DDLtipo" runat="server" Width="300px"
                                CssClass="Apariencia" AutoPostBack="True" OnSelectedIndexChanged="DDLtipo_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="rfvTipo" runat="server" ControlToValidate="DDLtipo"
                                ErrorMessage="Debe ingresar el tipo." ToolTip="Debe ingresar el tipo."
                                ValidationGroup="ControlDocument" ForeColor="Red" InitialValue="0">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="RowsText">
                            <asp:Label ID="Lresponsable" runat="server" Text="Cargo Responsable:" CssClass="Apariencia" Width="300px"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="tbxResponsable" runat="server" Width="300px" CssClass="Apariencia"></asp:TextBox>

                        </td>
                        <td>
                            <asp:Label ID="lblIdDependencia4" runat="server" Visible="False" Font-Names="Calibri"
                                Font-Size="Small"></asp:Label>
                            <asp:ImageButton ID="imgDependencia4" runat="server" ImageUrl="~/Imagenes/Icons/Organization-Chart.png"
                                OnClientClick="return false;" />
                            <asp:PopupControlExtender ID="popupDependencia4" runat="server" TargetControlID="imgDependencia4" BehaviorID="popup4"
                                PopupControlID="pnlDependencia4" OffsetY="-200">
                            </asp:PopupControlExtender>
                            <asp:Panel ID="pnlDependencia4" runat="server" CssClass="popup" Width="400px" Style="display: none">
                                <table width="100%" border="1" cellspacing="0" cellpadding="2" bordercolor="White">
                                    <tr align="right" bgcolor="#5D7B9D">
                                        <td>
                                            <asp:ImageButton ID="btnClosepp4" runat="server" ImageUrl="~/Imagenes/Icons/dialog-close3.png"
                                                OnClientClick="$find('popup4').hidePopup(); return false;" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:TreeView ID="TreeView4" ExpandDepth="3" runat="server" Font-Names="Calibri"
                                                Font-Size="Small" LineImagesFolder="~/TreeLineImages" ForeColor="Black" ShowLines="True"
                                                AutoGenerateDataBindings="False" OnSelectedNodeChanged="TreeView4_SelectedNodeChanged">
                                                <SelectedNodeStyle BackColor="Silver" BorderColor="#66CCFF" BorderStyle="Inset" />
                                            </asp:TreeView>
                                        </td>
                                    </tr>
                                    <tr align="center">
                                        <td>
                                            <asp:Button ID="BtnOk4" runat="server" Text="Aceptar" CssClass="Apariencia" OnClientClick="$find('popup4').hidePopup(); return false;" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>

                            <asp:RequiredFieldValidator ID="RFVresponsable" runat="server" ControlToValidate="tbxResponsable"
                                ErrorMessage="Debe ingresar el nombre del Responsable." ToolTip="Debe ingresar el nombre del Responsable."
                                ValidationGroup="ControlDocument" ForeColor="Red">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="RowsText">
                            <asp:Label ID="Lalmacenamiento" runat="server" Text="Almacenamiento  Y Protección :" CssClass="Apariencia"></asp:Label></td>
                        </td>
                        <td>
                            <asp:TextBox ID="TXalmace" runat="server" CssClass="Apariencia" Width="300px"></asp:TextBox>
                        </td>
                        <td>
                            <%--<asp:RequiredFieldValidator ID="RFValamacen" runat="server" ControlToValidate="TXalmace"
                                                ErrorMessage="Debe ingresar el Alamacenamiento  Y Protección." ToolTip="Debe ingresar el Alamacenamiento  Y Protección."
                                                ValidationGroup="ControlDocument" ForeColor="Red">*</asp:RequiredFieldValidator>--%>
                        </td>
                    </tr>
                    <tr>
                        <td class="RowsText">
                            <asp:Label ID="Lrollback" runat="server" Text="Recuperación:" CssClass="Apariencia"></asp:Label></td>
                        </td>
                        <td>
                            <asp:TextBox ID="TXrollback" runat="server" CssClass="Apariencia" Width="300px"></asp:TextBox>
                        </td>
                        <td>
                            <%--<asp:RequiredFieldValidator ID="RFVrollback" runat="server" ControlToValidate="TXalmace"
                                                ErrorMessage="Debe ingresar la Recuperacion." ToolTip="Debe ingresar la Recuperacion."
                                                ValidationGroup="ControlDocument" ForeColor="Red" >*</asp:RequiredFieldValidator>--%>
                        </td>
                    </tr>
                    <tr id="trEstadoDocumento" runat="server" visible="false">
                        <td class="RowsText">
                            <asp:Label ID="LEstadoDocumento" runat="server" Text="Estado Documento:" CssClass="Apariencia"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlEstadoDocumento" runat="server" Width="300px" CssClass="Apariencia" DataValueField="IdEstadoDocumento" 
                                DataTextField="NombreEstadoDocumento" AutoPostBack="true" OnSelectedIndexChanged="ddlEstadoDocumento_SelectedIndexChanged"></asp:DropDownList>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="RowsText">
                            <asp:Label ID="LtiempoActivo" runat="server" Text="Tiempo de Retención del Archivo Activo:" CssClass="Apariencia"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TXtiempoactivo" runat="server" CssClass="Apariencia" Width="300px"></asp:TextBox>
                        </td>
                        <td>
                            <%--<asp:RequiredFieldValidator ID="RFCtiempoactivo" runat="server" ControlToValidate="TXtiempoactivo"
                                                ErrorMessage="Debe ingresar el Tiempo de Retención del Archivo Activo." ToolTip="Debe ingresar el Tiempo de Retención del Archivo Activo."
                                                ValidationGroup="ControlDocument" ForeColor="Red" >*</asp:RequiredFieldValidator>--%>
                        </td>
                    </tr>
                    <tr>
                        <td class="RowsText">
                            <asp:Label ID="LtiempoInactivo" runat="server" Text="Tiempo de Retención del Archivo Inactivo:" CssClass="Apariencia"></asp:Label></td>
                        </td>
                        <td>
                            <asp:TextBox ID="TXtiempoinactivo" runat="server" CssClass="Apariencia" Width="300px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="RFVtiempoinactivo" runat="server" ControlToValidate="TXtiempoinactivo"
                                ErrorMessage="Debe ingresar el Tiempo de Retención del Archivo Inactivo." ToolTip="Debe ingresar el Tiempo de Retención del Archivo Inactivo."
                                ValidationGroup="ControlDocument" ForeColor="Red" InitialValue="0">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="RowsText">
                            <asp:Label ID="Lmedio" runat="server" Text="Medio Soporte:" CssClass="Apariencia"></asp:Label></td>
                        </td>
                        <td>
                            <asp:TextBox ID="TXmedio" runat="server" CssClass="Apariencia" Width="300px"></asp:TextBox>
                        </td>
                        <td>
                            <%--<asp:RequiredFieldValidator ID="RFVmedio" runat="server" ControlToValidate="TXmedio"
                                                ErrorMessage="Debe ingresar el medio de soporte." ToolTip="Debe ingresar el medio de soporte."
                                                ValidationGroup="ControlDocument" ForeColor="Red">*</asp:RequiredFieldValidator>--%>
                        </td>
                    </tr>
                    <tr>
                        <td class="RowsText">
                            <asp:Label ID="Lformato" runat="server" Text="Formato Archivo:" CssClass="Apariencia"></asp:Label></td>
                        </td>
                        <td>
                            <asp:TextBox ID="TXformato" runat="server" CssClass="Apariencia" Width="300px"></asp:TextBox>
                        </td>
                        <td>
                            <%--<asp:RequiredFieldValidator ID="RFVformato" runat="server" ControlToValidate="TXformato"
                                                ErrorMessage="Debe ingresar el formato del archivo a cargar." ToolTip="Debe ingresar el formato del archivo a cargar."
                                                ValidationGroup="ControlDocument" ForeColor="Red" >*</asp:RequiredFieldValidator>--%>
                        </td>
                    </tr>
                    <tr>
                        <td class="RowsText">
                            <asp:Label ID="Ldisposicion" runat="server" Text="Disposición Final:" CssClass="Apariencia"></asp:Label></td>
                        </td>
                        <td>
                            <asp:TextBox ID="Txdisposicion" runat="server" CssClass="Apariencia" Width="300px"></asp:TextBox>
                        </td>
                        <td>
                            <%-- <asp:RequiredFieldValidator ID="RFVdisposicion" runat="server" ControlToValidate="Txdisposicion"
                                                ErrorMessage="Debe ingresar la Disposición final." ToolTip="Debe ingresar la Disposición final."
                                                ValidationGroup="ControlDocument" ForeColor="Red">*</asp:RequiredFieldValidator>--%>
                        </td>
                    </tr>
                    <tr>
                        <td class="RowsText">
                            <asp:Label ID="Label79" runat="server" Text="Cargar documento:" Font-Names="Calibri"
                                Font-Size="Small"></asp:Label>
                        </td>
                        <td>
                            <asp:FileUpload ID="fuArchivoPerfil" runat="server" Font-Names="Calibri" Font-Size="Small"></asp:FileUpload>

                        </td>
                        <td>
                            <asp:Label ID="Larchivo" runat="server" CssClass="Apariencia" Visible="false"></asp:Label></td>
                    </tr>
                    <tr>
                        <td class="RowsText">
                            <asp:Label ID="Lobservaciones" runat="server" Text="Observaciones:" CssClass="Apariencia"></asp:Label></td>
                        </td>
                        <td>
                            <asp:TextBox ID="TXobservaciones" runat="server" CssClass="Apariencia" Width="300px"></asp:TextBox>
                        </td>
                        <td></td>
                    </tr>
                    <tr id="trJustificacion" runat="server" visible="false">
                        <td class="RowsText">
                            <asp:Label ID="lJustificacion" runat="server" Text="Justificación:" CssClass="Apariencia"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtJustificacion" runat="server" CssClass="Apariencia" Width="300px" TextMode="MultiLine" Rows="5"></asp:TextBox>
                        </td>
                        <td></td>
                    </tr>
                    <tr id="trJustificacionAprobacion" runat="server" visible="false">
                        <td class="RowsText">
                            <asp:Label ID="lblJA" runat="server" Text="Justificación Aprobación:" CssClass="Apariencia"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtJustificacionAprobacion" runat="server" CssClass="Apariencia" Width="300px" TextMode="MultiLine" Rows="5"></asp:TextBox>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="RowsText">
                            <asp:Label ID="Lusuario" runat="server" Text="Usuario Creación:" CssClass="Apariencia" Width="300px"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="tbxUsuarioCreacion" runat="server" Width="300px" CssClass="Apariencia"
                                Enabled="False"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="LidUsuario" runat="server" CssClass="Apariencia" Visible="false"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="RowsText">
                            <asp:Label ID="LfechaCreacion" runat="server" Text="Fecha de Creación:" CssClass="Apariencia"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtFecha" runat="server" Width="300px" CssClass="Apariencia"
                                Enabled="False"></asp:TextBox>
                            <asp:TextBox ID="txtFechaMod" runat="server" Width="300px" CssClass="Apariencia"
                                Visible="false"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="LblDocumentoActivo" runat="server" Visible="false" ></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:ImageButton ID="IBinsertGVC" runat="server" CausesValidation="true" CommandName="Insert"
                                ImageUrl="~/Imagenes/Icons/guardar.png" Text="Insert" ValidationGroup="ControlDocument" ToolTip="Insertar" Visible="false" OnClick="IBinsertGVC_Click" />
                            <asp:ImageButton ID="IBupdateGVC" runat="server" CausesValidation="true" CommandName="Update"
                                ImageUrl="~/Imagenes/Icons/guardar.png" Text="Update" ValidationGroup="ControlDocumentUP" ToolTip="Actualizar" Visible="false" OnClick="IBupdateGVC_Click" />
                            <asp:ImageButton ID="btnImgCancelar" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                OnClick="btnImgCancelar_Click" ToolTip="Cancelar" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div id="DVVersiones" runat="server" visible="false" class="ColumnStyle" >
            <div id="DVtable" class="center-content">
                <table class="tabla">
                    <tr>
                        <td>
                            <div style="overflow-x:auto;width:960px">
                                <asp:GridView ID="GVversiones" runat="server" CellPadding="4"
                                ForeColor="#333333" AllowPaging="False" AllowSorting="True" AutoGenerateColumns="False"
                                ShowHeaderWhenEmpty="True"
                                HeaderStyle-CssClass="gridViewHeader" BorderStyle="Solid" GridLines="Vertical"
                                    OnPreRender="GVversiones_PreRender"
                                CssClass="Apariencia" Font-Bold="False" OnRowCommand="GVversiones_RowCommand" DataKeyNames="intbitActivo,intId">
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>
                                    <asp:BoundField DataField="intId" HeaderText="Código" SortExpression="intId" ItemStyle-HorizontalAlign="Center" />
                                    <asp:TemplateField HeaderText="Versión" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 80px">
                                                <asp:Label ID="version" runat="server" Text='<% # Bind("strVersion")%>'></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                        <HeaderStyle Wrap="false" HorizontalAlign="center" />
                                        <ItemStyle Wrap="false" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Tipo Documento" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 80px">
                                                <asp:Label ID="strTipoDocumento" runat="server" Text='<% # Bind("strTipoDocumento")%>'></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                        <HeaderStyle Wrap="false" HorizontalAlign="center" />
                                        <ItemStyle Wrap="false" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Fecha Modificación" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 80px">
                                                <asp:Label ID="dtFechaModificacion" runat="server" Text='<% # Bind("dtFechaModificacion")%>'></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                        <HeaderStyle Wrap="false" HorizontalAlign="center" />
                                        <ItemStyle Wrap="false" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Fecha Eliminación" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 80px">
                                                <asp:Label ID="dtFechaEliminacion" runat="server" Text='<% # Bind("dtFechaEliminacion")%>'></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                        <HeaderStyle Wrap="false" HorizontalAlign="center" />
                                        <ItemStyle Wrap="false" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Observaciones" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 150px">
                                                <asp:Label ID="observaciones" runat="server" Text='<% # Bind("strObservaciones")%>'></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                        <HeaderStyle Wrap="false" HorizontalAlign="center" />
                                        <ItemStyle Wrap="false" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Justificación" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 150px">
                                                <asp:Label ID="justificacion" runat="server" Text='<% # Bind("JustificacionCambios")%>' ToolTip='<% # Bind("JustificacionCambios")%>'></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                        <HeaderStyle Wrap="false" HorizontalAlign="center" />
                                        <ItemStyle Wrap="false" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Usuario edita">
                                        <ItemTemplate>
                                            <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 80px">
                                                <asp:Label ID="lNombresUsuario" runat="server" Text='<% # Bind("Nombres")%>'></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Justificación Aprobación" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 150px">
                                                <asp:Label ID="justificacionAprueba" runat="server" Text='<% # Bind("JustificacionAprobacion")%>' ToolTip='<% # Bind("JustificacionAprobacion")%>'></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                        <HeaderStyle Wrap="false" HorizontalAlign="center" />
                                        <ItemStyle Wrap="false" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Usuario Aprueba">
                                        <ItemTemplate>
                                            <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 80px">
                                                <asp:Label ID="lNombresAprueba" runat="server" Text='<% # Bind("NomUsuarioAprobacion")%>'></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Archivo" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 150px">
                                                <asp:Label ID="archivo" runat="server" Text='<% # Bind("strPathFile")%>'></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                        <HeaderStyle Wrap="false" HorizontalAlign="center" />
                                        <ItemStyle Wrap="false" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="intbitActivo" HeaderText="Activo" SortExpression="intbitActivo" ItemStyle-HorizontalAlign="Center" Visible="false" />
                                    <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Icons/select.png" Text="Descargar" HeaderText="Descargar" CommandName="Descargar" ItemStyle-HorizontalAlign="Center" />
                                    <%-- <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Icons/switch-on-icon.png" Text="(In)Activar" HeaderText="(In)Activar" CommandName="Activar" ItemStyle-HorizontalAlign="Center" />--%>
                                    <asp:TemplateField HeaderText="(In)Activar" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 150px" align="center">
                                            <asp:Label ID="booActivo" runat="server" Visible="false" Text='<% # Bind("intBitActivo")%>'></asp:Label>
                                            <asp:ImageButton runat="server" ID="ImgBtnInact" ImageUrl="~/Imagenes/Icons/switch-on-icon.png" CommandName="Activar" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                        </div>
                                    </ItemTemplate>
                                    <HeaderStyle Wrap="false" HorizontalAlign="center" />
                                    <ItemStyle Wrap="false" />
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
                            </div>
                            
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </ContentTemplate>

</asp:UpdatePanel>
