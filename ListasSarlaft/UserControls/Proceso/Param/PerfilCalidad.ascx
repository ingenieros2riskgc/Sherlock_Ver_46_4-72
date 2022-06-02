<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PerfilCalidad.ascx.cs" Inherits="ListasSarlaft.UserControls.Proceso.PerfilCalidad" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControls/Sitio/OkMessageBox.ascx" TagPrefix="uc" TagName="OkMessageBox" %>
<link href="../../../Styles/AdminSitio.css" rel="stylesheet" type="text/css" />
<style type="text/css">
    .ajax__html_editor_extender_texteditor {
        background-color: White;
        font-size: small;
        font-weight: lighter;
        font-family: Calibri;
        font-style: normal;
    }

    .gridViewHeader a:link {
        text-decoration: none;
    }

    .style1 {
        width: 100%;
    }

    .popup {
        border: Silver 1px solid;
        color: #060F40;
        background: #ffffff;
        display: table-cell;
        margin-left: 25px;
    }

    .auto-style1 {
        width: 120px;
    }

    .no-visible {
        display: none
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
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <uc:OkMessageBox ID="omb" runat="server" />
        <asp:Panel ID="pnlMsgBox" runat="server" Width="400px" CssClass="modalPopup" Style="display: none;">
            <table style="width: 100%">
                <tr class="topHandle">
                    <td colspan="2" align="center" runat="server" id="tdCaption">
                        <asp:Label ID="lblCaption" runat="server" Text="Atención" Font-Bold="False" Font-Names="Tahoma" Font-Size="11px"></asp:Label><br />
                    </td>
                </tr>
                <tr>
                    <td style="width: 60px; text-align: center">
                        <asp:Image ID="imgInfo" runat="server" ImageUrl="~/Imagenes/Icons/icontexto-webdev-about.png" />
                    </td>
                    <td style="text-align: left">
                        <asp:Label ID="lblMsgBox" runat="server" Font-Bold="False" Font-Names="Tahoma" Font-Size="11px"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right" colspan="2">
                        <asp:Button ID="btnModificarEstado" runat="server" Text="Ok" OnClick="btnModificarEstado_Click" />
                        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" OnClientClick="$find('mypopup').hide(); return false;" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:ModalPopupExtender ID="mpeMsgBox" runat="server" PopupControlID="pnlMsgBox" BehaviorID="mypopup"
            Enabled="True" TargetControlID="btndummy" BackgroundCssClass="modalBackground" DropShadow="true">
        </asp:ModalPopupExtender>
        <asp:Button ID="btndummy" runat="server" Text="Button" Style="display: none" />
        <asp:Label ID="lblOk" runat="server" Text="Label" Visible="False"></asp:Label>
        <asp:Label ID="lblCancelar" runat="server" Text="Label" Visible="False"></asp:Label>
        <div style="height: 20px"></div>
        <div class="ColumnStyle" id="divFiltro" runat="server">
            <div class="ColumnStyle" runat="server">
                <div style="text-align: center">
                    Filtros de búsqueda
                </div>
            </div>
            <div class="TableContains">
                <table class="tabla" align="center" width="80%">
                    <tr>
                        <td class="RowsText">
                            <asp:Label ID="lmacroProceso" runat="server" Text="Macroproceso:" CssClass="Apariencia" Width="300px"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlMacroProcesoFiltro" runat="server" CssClass="Apariencia" Width="300px"></asp:DropDownList>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="RowsText">
                            <asp:Label ID="lCargoFiltro" runat="server" Text="Cargo:" CssClass="Apariencia" Width="300px"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtCargoFiltro" Width="300px" CssClass="Apariencia"></asp:TextBox>

                        </td>
                        <td>
                            <asp:Label ID="lJerarquiaFiltro" runat="server" Visible="False" Font-Names="Calibri"
                                Font-Size="Small"></asp:Label>
                            <asp:ImageButton ID="ibtnJerarquiaFiltro" runat="server" ImageUrl="~/Imagenes/Icons/Organization-Chart.png"
                                OnClientClick="return false;" />
                            <asp:PopupControlExtender ID="PopupControlExtender1" runat="server" TargetControlID="ibtnJerarquiaFiltro" BehaviorID="popup5"
                                PopupControlID="PanelFiltro" OffsetY="-200">
                            </asp:PopupControlExtender>
                            <asp:Panel ID="PanelFiltro" runat="server" CssClass="popup">
                                <table width="100%" border="1" cellspacing="0" cellpadding="2" bordercolor="White">
                                    <tr align="right" bgcolor="#5D7B9D">
                                        <td>
                                            <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Imagenes/Icons/dialog-close2.png"
                                                OnClientClick="$find('popup5').hidePopup(); return false;" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:TreeView ID="TreeViewFiltro" ExpandDepth="3" runat="server" Font-Names="Calibri"
                                                Font-Size="Small" LineImagesFolder="~/TreeLineImages" ForeColor="Black" ShowLines="True"
                                                AutoGenerateDataBindings="False" OnSelectedNodeChanged="TreeViewFiltro_SelectedNodeChanged">
                                                <SelectedNodeStyle BackColor="Silver" BorderColor="#66CCFF" BorderStyle="Inset" />
                                            </asp:TreeView>
                                        </td>
                                    </tr>
                                    <tr style="text-align: center">
                                        <td>
                                            <asp:Button ID="Button1" runat="server" Text="Aceptar" CssClass="Apariencia" OnClientClick="$find('popup5').hidePopup(); return false;" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr style="display: none">
                        <td class="RowsText">
                            <asp:Label ID="lEstado" runat="server" Text="Estado:" CssClass="Apariencia" Width="300px"></asp:Label>
                        </td>
                        <td style="text-align: center">
                            <asp:RadioButton ID="rbtnEstadoActivo" runat="server" GroupName="Estado" Text="Activo" Font-Size="Small" Font-Bold="false" Checked="true" />
                            <asp:RadioButton ID="rbtnEstadoInactivo" runat="server" GroupName="Estado" Text="Inactivo" Font-Size="Small" Font-Bold="false" />
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
        <div style="height: 20px"></div>
        <table align="center" width="80%">
            <tr align="center" bgcolor="#333399">
                <td>
                    <asp:Label ID="Label13" runat="server" ForeColor="White" Text="Perfil" Font-Bold="False"
                        Font-Names="Calibri" Font-Size="Large"></asp:Label>
                </td>
            </tr>
            <tr align="center" bgcolor="#EEEEEE" id="filaGrid" runat="server" visible="true">
                <td bgcolor="#EEEEEE">
                    <br />
                    <table>
                        <tr>
                            <td>
                                <asp:GridView ID="GridView1" runat="server" CellPadding="4"
                                    ForeColor="#333333" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
                                    DataKeyNames="intId"
                                    ShowHeaderWhenEmpty="True" OnRowCommand="GridView1_RowCommand"
                                    HeaderStyle-CssClass="gridViewHeader" BorderStyle="Solid" GridLines="Vertical"
                                    CssClass="Apariencia" Font-Bold="False" OnPageIndexChanging="GridView1_PageIndexChanging">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:BoundField DataField="intId" HeaderText="Id"
                                            SortExpression="intId" Visible="False" />
                                        <asp:BoundField DataField="intIdJOrganizacional" HeaderText="IdCargo" ItemStyle-HorizontalAlign="Left"
                                            SortExpression="intIdJOrganizacional" HtmlEncode="False" HtmlEncodeFormatString="True" Visible="False" />
                                        <asp:BoundField DataField="strNombreJOrganizacional" HeaderText="Cargo Responsable"
                                            SortExpression="strNombreJOrganizacional" Visible="False" />
                                        <asp:BoundField DataField="strResumenCargo" HeaderText="Resumen Cargo" ItemStyle-HorizontalAlign="Left"
                                            SortExpression="strResumenCargo" Visible="False" />
                                        <asp:BoundField DataField="strPerfil" HeaderText="Cargo"
                                            SortExpression="strPerfil" HtmlEncode="False" HtmlEncodeFormatString="True" Visible="True" />
                                        <asp:TemplateField HeaderText="Estado" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" Visible="False">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chbActivo" runat="server" Checked='<% # Bind("booEstado")%>' Enabled="false" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="strRol" HeaderText="Roles"
                                            SortExpression="strRol" HtmlEncode="False" HtmlEncodeFormatString="True" Visible="False" />
                                        <asp:BoundField DataField="strHabilidades" HeaderText="Habilidades"
                                            SortExpression="strHabilidades" HtmlEncode="False" HtmlEncodeFormatString="True" Visible="False" />
                                        <asp:BoundField DataField="strEducacion" HeaderText="Educacion"
                                            SortExpression="strEducacion" HtmlEncode="False" HtmlEncodeFormatString="True" Visible="False" />
                                        <asp:BoundField DataField="strFormacion" HeaderText="Formacion"
                                            SortExpression="strFormacion" HtmlEncode="False" HtmlEncodeFormatString="True" Visible="False" />
                                        <asp:BoundField DataField="strExperiencia" HeaderText="Experiencia"
                                            SortExpression="strExperiencia" HtmlEncode="False" HtmlEncodeFormatString="True" Visible="False" />
                                        <asp:BoundField DataField="strFunciones" HeaderText="Funciones"
                                            SortExpression="strFunciones" HtmlEncode="False" HtmlEncodeFormatString="True" Visible="False" />
                                        <asp:BoundField DataField="intIdMacroproceso" HeaderText="Id Macroproceso"
                                            SortExpression="intIdMacroproceso" Visible="False" />
                                        <asp:BoundField DataField="intIdUsuario" HeaderText="IdUsuario" SortExpression="intIdUsuario" Visible="False" />
                                        <asp:BoundField DataField="strNombreUsuario" HeaderText="Usuario Creación" SortExpression="strNombreUsuario" Visible="False" />
                                        <asp:BoundField DataField="dtFechaRegistro" HeaderText="Fecha de Creación" SortExpression="dtFechaRegistro" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                        <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Icons/edit.png" Text="Modificar"
                                            HeaderText="Modificar" CommandName="Modificar" ItemStyle-HorizontalAlign="Center" />
                                        <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Icons/switch-on-icon.png" Text="(In)Activar"
                                            HeaderText="(In)Activar" CommandName="Activar" ItemStyle-HorizontalAlign="Center" Visible="false" />
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
                                    ImageUrl="~/Imagenes/Icons/Add.png" Text="Insert"
                                    OnClick="imgBtnInsertar_Click" ToolTip="Insertar" />
                            </td>
                        </tr>
                    </table>
                    <br />
                </td>
            </tr>
            <tr align="left" id="filaDetalle" runat="server" visible="false">
                <td bgcolor="#EEEEEE">
                    <table class="tabla" width="100%">
                        <tr>
                            <td align="left" bgcolor="#BBBBBB">
                                <asp:Label ID="Label1" runat="server" Text="Código:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="tbxId" runat="server" Enabled="False" Width="70px"
                                    CssClass="Apariencia"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" bgcolor="#BBBBBB">
                                <asp:Label ID="Label8" runat="server" Text="Macroproceso:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlMacroproceso" runat="server" Width="300px" CssClass="Apariencia"
                                    DataTextField="NombreMacroproceso" DataValueField="IdMacroproceso">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvMacroproceso" runat="server" ControlToValidate="ddlMacroproceso"
                                    ErrorMessage="Debe ingresar el Macroproceso." ToolTip="Debe ingresar el Macroproceso."
                                    ValidationGroup="iPerfil" ForeColor="Red" InitialValue="0">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr id="NombrePerfil" runat="server" visible="false">
                            <td align="left" bgcolor="#BBBBBB">
                                <asp:Label ID="Label6" runat="server" Text="Nombre Perfil:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="tbxNombrePerfil" runat="server" Width="300px" CssClass="Apariencia" MaxLength="60"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" bgcolor="#BBBBBB">
                                <asp:Label ID="Label3" runat="server" Text="Cargo:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="tbxResponsable" runat="server" Width="212px" CssClass="Apariencia" ReadOnly="true"></asp:TextBox>
                                <asp:Label ID="lblIdDependencia4" runat="server" Visible="False" Font-Names="Calibri"
                                    Font-Size="Small"></asp:Label>
                                 <asp:ImageButton ID="btnBorrarPerfil" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                    ToolTip="Borrar Responsables Ejecución" OnClick="BtnBorrarPerfil_Click" Width="17px" />
                                <asp:ImageButton ID="imgDependencia4" runat="server" ImageUrl="~/Imagenes/Icons/Organization-Chart.png"
                                    OnClientClick="return false;" Width="17px" />
                                <asp:PopupControlExtender ID="popupDependencia4" runat="server" TargetControlID="imgDependencia4" BehaviorID="popup4"
                                    PopupControlID="pnlDependencia4" OffsetY="-200">
                                </asp:PopupControlExtender>
                                <asp:Panel ID="pnlDependencia4" runat="server" CssClass="popup">
                                    <table style="width: 100%">
                                        <tr align="right" bgcolor="#5D7B9D">
                                            <td>
                                                <asp:ImageButton ID="btnClosepp4" runat="server" ImageUrl="~/Imagenes/Icons/dialog-close2.png"
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
                                <asp:RequiredFieldValidator ID="rfvCargoResponsable" runat="server" ControlToValidate="tbxResponsable"
                                    ErrorMessage="Debe ingresar el Cargo Responsable." ToolTip="Debe ingresar el Cargo Responsable."
                                    ValidationGroup="iPerfil" ForeColor="Red">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="lCodigo" runat="server" Text="Código:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCodigo" runat="server" CssClass="Apariencia" Width="212px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="RowsText">
                                <asp:Label ID="lJerarquiaAprueba" runat="server" Text="Aprueba:" CssClass="Apariencia" Width="300px"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="txtJerarquiaAprueba" Width="300px" CssClass="Apariencia" ReadOnly="true"></asp:TextBox>
                                <asp:Label ID="lblIdDependencia1" runat="server" Visible="False" Font-Names="Calibri"
                                    Font-Size="Small"></asp:Label>
                                 <asp:ImageButton ID="btnBorrarResponsables" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                    ToolTip="Borrar Responsables Ejecución" OnClick="BtnBorrarResponsables_Click" Width="17px" />
                                <asp:ImageButton ID="imgDependencia1" runat="server" ImageUrl="~/Imagenes/Icons/Organization-Chart.png"
                                    OnClientClick="return false;" Width="17px"/>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtJerarquiaAprueba"
                                    ForeColor="Red" ValidationGroup="iPerfil" Display="Dynamic">*</asp:RequiredFieldValidator>
                                <asp:PopupControlExtender ID="popupDependencia1" runat="server"
                                    Enabled="True" TargetControlID="imgDependencia1" BehaviorID="popup1"
                                    PopupControlID="pnlDependencia1" OffsetY="-200" OffsetX="30">
                                </asp:PopupControlExtender>
                                <asp:Panel ID="pnlDependencia1" runat="server" CssClass="popup" Width="50%" Style="display: none">
                                    <table width="100%" border="1" cellspacing="0" cellpadding="2" bordercolor="White">
                                        <tr align="right" bgcolor="#5D7B9D">
                                            <td>
                                                <asp:ImageButton ID="btnClosepp1" runat="server" ImageUrl="~/Imagenes/Icons/dialog-close2.png"
                                                    OnClientClick="$find('popup1').hidePopup(); return false;" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TreeView ID="treeviewAprueba" ExpandDepth="3" runat="server" Font-Names="Calibri"
                                                    Font-Size="Small" LineImagesFolder="~/TreeLineImages" ForeColor="Black" ShowLines="True" OnSelectedNodeChanged="TreeviewAprueba_SelectedNodeChanged"
                                                    AutoGenerateDataBindings="False">
                                                    <SelectedNodeStyle BackColor="Silver" BorderColor="#66CCFF" BorderStyle="Inset" />
                                                </asp:TreeView>
                                            </td>
                                        </tr>
                                        <tr align="center">
                                            <td>
                                                <asp:Button ID="BtnOk1" runat="server" Text="Aceptar" CssClass="Apariencia" OnClientClick="$find('popup1').hidePopup(); return false;" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                            <td>
                            </td>
                        </tr>

                        <tr class="no-visible">
                            <td align="left" bgcolor="#BBBBBB">
                                <asp:Label ID="Label5" runat="server" Text="Resumen del Cargo:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="tbxResumenCargo" runat="server" Width="800px" CssClass="Apariencia" Height="42px"
                                    Columns="100" Rows="8" TextMode="MultiLine" Font-Size="10pt" Font-Bold="False" MaxLength="500"></asp:TextBox>
                                <asp:RegularExpressionValidator runat="server" ID="rfvResumenCargoLen" Display="Dynamic" ForeColor="Red"
                                    ControlToValidate="tbxResumenCargo" ValidationExpression="^[\s\S]{0,500}$" ValidationGroup="iPerfil"
                                    ErrorMessage="La longitud máxima es 500 caracteres" ToolTip="La longitud máxima es 500 caracteres">*</asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr class="no-visible">
                            <td align="left" bgcolor="#BBBBBB">
                                <asp:Label ID="Label2" runat="server" Text="Funciones:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="tbxFunciones" runat="server" Width="800px" CssClass="Apariencia" Height="42px"
                                    Columns="100" Rows="8" TextMode="MultiLine" Font-Size="10pt" Font-Bold="False" MaxLength="500"></asp:TextBox>
                                <asp:RegularExpressionValidator runat="server" ID="rfvFuncionesLen" Display="Dynamic" ForeColor="Red"
                                    ControlToValidate="tbxFunciones" ValidationExpression="^[\s\S]{0,500}$" ValidationGroup="iPerfil"
                                    ErrorMessage="La longitud máxima es 500 caracteres" ToolTip="La longitud máxima es 500 caracteres">*</asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr class="no-visible">
                            <td align="left" bgcolor="#BBBBBB">
                                <asp:Label ID="Label10" runat="server" Text="Roles:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="tbxRoles" runat="server" Width="800px" CssClass="Apariencia" Height="42px"
                                    Columns="100" Rows="8" TextMode="MultiLine" Font-Size="10pt" Font-Bold="False" MaxLength="500"></asp:TextBox>
                                <asp:RegularExpressionValidator runat="server" ID="rfvRolesLen" Display="Dynamic" ForeColor="Red"
                                    ControlToValidate="tbxRoles" ValidationExpression="^[\s\S]{0,500}$" ValidationGroup="iPerfil"
                                    ErrorMessage="La longitud máxima es 500 caracteres" ToolTip="La longitud máxima es 500 caracteres">*</asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr class="no-visible">
                            <td align="left" bgcolor="#BBBBBB">
                                <asp:Label ID="Label11" runat="server" Text="Educación:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="tbxEducacion" runat="server" Width="800px" CssClass="Apariencia" Height="42px"
                                    Columns="100" Rows="8" TextMode="MultiLine" Font-Size="10pt" Font-Bold="False" MaxLength="500"></asp:TextBox>
                                <asp:RegularExpressionValidator runat="server" ID="rfvEducacionLen" Display="Dynamic" ForeColor="Red"
                                    ControlToValidate="tbxEducacion" ValidationExpression="^[\s\S]{0,500}$" ValidationGroup="iPerfil"
                                    ErrorMessage="La longitud máxima es 500 caracteres" ToolTip="La longitud máxima es 500 caracteres">*</asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr class="no-visible">
                            <td align="left" bgcolor="#BBBBBB">
                                <asp:Label ID="Label12" runat="server" Text="Habilidades:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="tbxHabilidades" runat="server" Width="800px" CssClass="Apariencia" Height="42px"
                                    Columns="100" Rows="8" TextMode="MultiLine" Font-Size="10pt" Font-Bold="False" MaxLength="500"></asp:TextBox>
                                <asp:RegularExpressionValidator runat="server" ID="rfvHabilidadesLen" Display="Dynamic" ForeColor="Red"
                                    ControlToValidate="tbxHabilidades" ValidationExpression="^[\s\S]{0,500}$" ValidationGroup="iPerfil"
                                    ErrorMessage="La longitud máxima es 500 caracteres" ToolTip="La longitud máxima es 500 caracteres">*</asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr class="no-visible">
                            <td align="left" bgcolor="#BBBBBB">
                                <asp:Label ID="Label14" runat="server" Text="Formación:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="tbxFormacion" runat="server" Width="800px" CssClass="Apariencia" Height="42px"
                                    Columns="100" Rows="8" TextMode="MultiLine" Font-Size="10pt" Font-Bold="False" MaxLength="500"></asp:TextBox>
                                <asp:RegularExpressionValidator runat="server" ID="rfvFormacionLen" Display="Dynamic" ForeColor="Red"
                                    ControlToValidate="tbxFormacion" ValidationExpression="^[\s\S]{0,500}$" ValidationGroup="iPerfil"
                                    ErrorMessage="La longitud máxima es 500 caracteres" ToolTip="La longitud máxima es 500 caracteres">*</asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr class="no-visible">
                            <td align="left" bgcolor="#BBBBBB">
                                <asp:Label ID="Label15" runat="server" Text="Experiencia:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="tbxExperiencia" runat="server" Width="800px" CssClass="Apariencia" Height="42px"
                                    Columns="100" Rows="8" TextMode="MultiLine" Font-Size="10pt" Font-Bold="False" MaxLength="500"></asp:TextBox>
                                <asp:RegularExpressionValidator runat="server" ID="rfvExperienciaLen" Display="Dynamic" ForeColor="Red"
                                    ControlToValidate="tbxExperiencia" ValidationExpression="^[\s\S]{0,500}$" ValidationGroup="iPerfil"
                                    ErrorMessage="La longitud máxima es 500 caracteres" ToolTip="La longitud máxima es 500 caracteres">*</asp:RegularExpressionValidator>
                            </td>
                        </tr>

                        <tr id="trEstadoDocumento" runat="server" visible="false">
                            <td align="left" bgcolor="#BBBBBB">
                                <asp:Label ID="Label7" runat="server" Text="Estado:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlEstadoPerfil" runat="server" Width="300px" CssClass="Apariencia"></asp:DropDownList>
                                <asp:CheckBox ID="ChBEstado" runat="server" Checked="true" CssClass="no-visible" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" bgcolor="#BBBBBB">
                                <asp:Label ID="Label9" runat="server" Text="Usuario Creación:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="tbxUsuarioCreacion" runat="server" Width="100px" CssClass="Apariencia"
                                    Enabled="False"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" bgcolor="#BBBBBB">
                                <asp:Label ID="Label4" runat="server" Text="Fecha de Creación:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="tbxFecha" runat="server" Width="100px" CssClass="Apariencia"
                                    Enabled="False"></asp:TextBox>
                            </td>
                        </tr>
                        <tr align="center">
                            <td colspan="2">
                                <table class="tablaSinBordes">
                                    <tr>
                                        <td>
                                            <asp:ImageButton ID="btnImgInsertar" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                Visible="False" OnClick="btnImgInsertar_Click" ToolTip="Guardar" ValidationGroup="iPerfil" />
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="btnImgActualizar" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                Style="text-align: right" OnClick="btnImgActualizar_Click" ValidationGroup="iPerfil"
                                                ToolTip="Guardar" />
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="btnImgCancelar" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                                OnClick="btnImgCancelar_Click" ToolTip="Cancelar" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr id="filaAdjuntos" runat="server" visible="false" align="center">
                <td bgcolor="#EEEEEE">
                    <table>
                        <tr align="center">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label79" runat="server" Text="Adjuntar documento .pdf:" Font-Names="Calibri"
                                    Font-Size="Small"></asp:Label>
                            </td>
                            <td>
                                <asp:FileUpload ID="FileUpload2" runat="server" Font-Names="Calibri" Font-Size="Small"></asp:FileUpload>
                            </td>
                            <td>
                                <asp:ImageButton ID="btnAgregarPDF" runat="server" ImageUrl="~/Imagenes/Icons/Add.png"
                                    ToolTip="Adjuntar" OnClick="btnAgregarPDF_Click"></asp:ImageButton>
                            </td>
                        </tr>
                        <tr align="center">
                            <td colspan="3">
                                <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                    ForeColor="#333333" GridLines="Vertical" ShowHeaderWhenEmpty="True" BorderStyle="Solid"
                                    HorizontalAlign="Center" Font-Names="Calibri" Font-Size="Small" OnRowCommand="GridView3_RowCommand">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775"></AlternatingRowStyle>
                                    <Columns>
                                        <asp:BoundField DataField="intId" HeaderText="IdArchivo" Visible="False"></asp:BoundField>
                                        <asp:BoundField DataField="intIdTipoControl" HeaderText="IdTipoControl" Visible="False"></asp:BoundField>
                                        <asp:BoundField DataField="strNombreArchivo" HeaderText=" Nombre Archivo"></asp:BoundField>
                                        <asp:BoundField DataField="bArchivoBinario" HeaderText="Archivo Binario" Visible="False"></asp:BoundField>
                                        <asp:TemplateField HeaderText="Estado">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chbActivo" runat="server" Checked='<% # Bind("booEstado")%>' Enabled="false" />
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="intIdUsuario" HeaderText=" Id Usuario" Visible="False"></asp:BoundField>
                                        <asp:BoundField DataField="dtFechaRegistro" HeaderText="Fecha Registro" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                        <asp:BoundField DataField="strNombreUsuario" HeaderText="Nombre Usuario" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                        <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Icons/downloads.png" Text="Descargar" HeaderText="Descargar"
                                            CommandName="Descargar" ItemStyle-HorizontalAlign="Center"></asp:ButtonField>
                                        <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Icons/switch-on-icon.png" Text="(In)Activar"
                                            HeaderText="(In)Activar" CommandName="Activar" ItemStyle-HorizontalAlign="Center" />
                                    </Columns>
                                    <EditRowStyle BackColor="#999999" />
                                    <FooterStyle BackColor="White" Font-Bold="True" ForeColor="White"></FooterStyle>
                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" CssClass="gridViewHeader"></HeaderStyle>
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
