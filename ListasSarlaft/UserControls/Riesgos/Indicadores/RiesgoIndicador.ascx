<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RiesgoIndicador.ascx.cs" Inherits="ListasSarlaft.UserControls.Riesgos.Indicadores.RiesgoIndicador" %>
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
    .auto-style1 {
        width: 786px;
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
<asp:UpdatePanel ID="RIbody" runat="server">
    <ContentTemplate>
        <uc:OkMessageBox ID="omb" runat="server" />
        <asp:Panel ID="pnlMsgBox" runat="server"  CssClass="modalPopup" Style="display: none;">
            <table class="Tablewidth">
                <tr class="topHandle">
                    <td class="centertdtr" colspan="2" runat="server" id="tdCaption">
                        <asp:Label ID="lblCaption" runat="server" Text="Atención" Font-Bold="False" Font-Names="Tahoma" Font-Size="11px" ForeColor="White"></asp:Label>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td style="width: 60px" class="centerMiddle">
                        <asp:Image ID="imgInfo" runat="server" ImageUrl="~/Imagenes/Icons/icontexto-webdev-about.png" />
                    </td>
                    <td class="LeftMiddle">
                        <asp:Label ID="lblMsgBox" runat="server" Font-Bold="False" Font-Names="Tahoma" Font-Size="11px"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="righttdtr" colspan="2" align="center">
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
        <div class="TituloLabel" id="HeadRI" runat="server">
            <asp:Label ID="Ltitulo" runat="server" ForeColor="White" Text="Registro de Indicadores" Font-Bold="False"
                Font-Names="Calibri" Font-Size="Large"></asp:Label>
        </div>
        <div id="BodyGridRI" class="ColumnStyle" runat="server">
            <table class="tabla" align="center" width="100%">
                <tr align="center">
                    <td>
                        <asp:GridView ID="GVriesgosIndicadores" runat="server" CellPadding="4"
                            ForeColor="#333333" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
                            ShowHeaderWhenEmpty="True" DataKeyNames="strUsuario,dtFechaCreacion,intIdMeta,intIdEsquemaSeguimiento,intIdFormula, intIProcesoIndicador, intIdFrecuenciaMedicion,intIdProceso,booActivo,intIdResponsableMedicion"
                            HeaderStyle-CssClass="gridViewHeader" BorderStyle="Solid" GridLines="Vertical"
                            CssClass="Apariencia" Font-Bold="False" OnRowCommand="GVriesgosIndicadores_RowCommand" OnPreRender="GVriesgosIndicadores_PreRender" OnPageIndexChanging="GVriesgosIndicadores_PageIndexChanging">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:BoundField DataField="intIdRiesgoIndicador" HeaderText="Código" SortExpression="intIdRiesgoIndicador" ItemStyle-HorizontalAlign="Center" />
                                <asp:TemplateField HeaderText="Nombre Indicador" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 150px">
                                            <asp:Label ID="strNombreIndicador" runat="server" Text='<% # Bind("strNombreIndicador")%>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                    <HeaderStyle Wrap="false" HorizontalAlign="center" />
                                    <ItemStyle Wrap="false" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Objetivo Indicador" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 150px">
                                            <asp:Label ID="strObjetivoIndicador" runat="server" Text='<% # Bind("strObjetivoIndicador")%>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                    <HeaderStyle Wrap="false" HorizontalAlign="center" />
                                    <ItemStyle Wrap="false" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Proceso Indicador" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 150px">
                                            <asp:Label ID="strNombreProceso" runat="server" Text='<% # Bind("strNombreProceso")%>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                    <HeaderStyle Wrap="false" HorizontalAlign="center" />
                                    <ItemStyle Wrap="false" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Responsable Medicion" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 150px">
                                            <asp:Label ID="strResponsableMedicion" runat="server" Text='<% # Bind("strResponsableMedicion")%>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                    <HeaderStyle Wrap="false" HorizontalAlign="center" />
                                    <ItemStyle Wrap="false" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Frecuencia Medición" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 150px">
                                            <asp:Label ID="strFrecuenciaMedicion" runat="server" Text='<% # Bind("strFrecuenciaMedicion")%>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                    <HeaderStyle Wrap="false" HorizontalAlign="center" />
                                    <ItemStyle Wrap="false" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Código Riesgo" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 150px">
                                            <asp:Label ID="strCodRiesgo" runat="server" Text='<% # Bind("strCodRiesgo")%>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                    <HeaderStyle Wrap="false" HorizontalAlign="center" />
                                    <ItemStyle Wrap="false" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Nombre Riesgo" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 100px">
                                            <asp:Label ID="strNombreRiesgo" runat="server" Text='<% # Bind("strNombreRiesgo")%>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                    <HeaderStyle Wrap="false" HorizontalAlign="center" />
                                    <ItemStyle Wrap="false" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="dtFechaCreacion" HeaderText="fechaRegistro" ReadOnly="True" Visible="false" SortExpression="dtFechaCreacion" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="strUsuario" HeaderText="Usuario" ReadOnly="True" Visible="false" SortExpression="strUsuario" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="booActivo" HeaderText="Activo" ReadOnly="True" Visible="false" SortExpression="booActivo" ItemStyle-HorizontalAlign="Center" />
                                <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Icons/select.png" Text="Seleccionar" HeaderText="Seleccionar" CommandName="Seleccionar" ItemStyle-HorizontalAlign="Center" />
                                <asp:TemplateField HeaderText="(In)Activar" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 150px" align="center">
                                                    <asp:Label ID="booActivo" runat="server" Visible="false" Text='<% # Bind("booActivo")%>'></asp:Label>
                                                    <asp:ImageButton runat="server" ID="ImgBtnInact" ImageUrl="~/Imagenes/Icons/switch-on-icon.png"  CommandName="Activar" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"/>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false"  HorizontalAlign="center" />
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
        <div id="BodyFormRI" class="ColumnStyle" runat="server" visible="false">
            <div id="form" class="TableContains">
                <table class="tabla" align="center" width="80%">
                    <tr>
                        <td class="RowsText">
                            <asp:Label ID="Lcodigo" runat="server" Text="Código:" CssClass="Apariencia"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtId" runat="server" Enabled="False"
                                CssClass="Apariencia" Width="300px"></asp:TextBox>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="RowsText">
                            <asp:Label ID="lblNombreIndicador" runat="server" Text="Nombre del Indicador:" CssClass="Apariencia" Width="300px"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtNombreIndicador" runat="server" Font-Names="Calibri" Font-Size="Small" ValidationGroup="VGriesgosIndicadores" Width="300px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="rfvNombreIndicador" runat="server" ControlToValidate="txtNombreIndicador"
                                ErrorMessage="Debe ingresar el nombre del indicador." ToolTip="Debe ingresar el nombre del indicador."
                                ValidationGroup="VGriesgosIndicadores" ForeColor="Red">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="RowsText">
                            <asp:Label ID="lblObjetivoIndicador" runat="server" Text="Objetivo del Indicador:" CssClass="Apariencia" Width="300px"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtObjetivoIndicador" runat="server" Font-Names="Calibri" Font-Size="Small" ValidationGroup="VGriesgosIndicadores" Width="300px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="rfvObjetivoIndicador" runat="server" ControlToValidate="txtObjetivoIndicador"
                                ErrorMessage="Debe ingresar el objetivo del indicador." ToolTip="Debe ingresar el objetivo del indicador."
                                ValidationGroup="VGriesgosIndicadores" ForeColor="Red">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr id="CadenaValor">
                        <td class="RowsText">
                            <asp:Label ID="Label19" runat="server" Text="Cadena de Valor:" CssClass="Apariencia"></asp:Label></td>
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
                                ValidationGroup="VGriesgosIndicadores" ForeColor="Red" InitialValue="0">*</asp:RequiredFieldValidator>
                            <asp:TextBox ID="tbxProcIndica" runat="server" Width="90px" MaxLength="20" CssClass="Apariencia" Visible="false"></asp:TextBox>
                            <asp:Label ID="LtexProceso" runat="server" Visible="false"></asp:Label>
                        </td>
                    </tr>
                    <tr id="Macroproceso">
                        <td class="RowsText">
                            <asp:Label ID="Label22" runat="server" Text="Macroproceso:" CssClass="Apariencia"></asp:Label></td>
                        <td>
                            <asp:DropDownList ID="ddlMacroproceso" runat="server" Width="300px"
                                CssClass="Apariencia" AutoPostBack="True" DataTextField="Nombre" DataValueField="IdMacroproceso"
                                OnSelectedIndexChanged="ddlMacroproceso_SelectedIndexChanged">
                            </asp:DropDownList></td>
                        <td>
                            <asp:RequiredFieldValidator ID="rfvMacroProceso" runat="server" ControlToValidate="ddlMacroproceso"
                                ErrorMessage="Debe ingresar el Macroproceso." ToolTip="Debe ingresar el Macroproceso."
                                ValidationGroup="VGriesgosIndicadores" ForeColor="Red" InitialValue="0">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr id="Proceso">
                        <td class="RowsText">
                            <asp:Label ID="Label7" runat="server" Text="Proceso:" CssClass="Apariencia"></asp:Label></td>
                        <td>
                            <asp:DropDownList ID="ddlProceso" runat="server" Width="300px" OnSelectedIndexChanged="ddlProceso_SelectedIndexChanged"
                                CssClass="Apariencia" AutoPostBack="True" DataTextField="Nombre" DataValueField="IdProceso">
                            </asp:DropDownList></td>
                        <td>
                            <asp:RequiredFieldValidator ID="rfvProceso" runat="server" ControlToValidate="ddlProceso"
                                ErrorMessage="Debe ingresar el Proceso." ToolTip="Debe ingresar el Proceso." Enabled="False"
                                ValidationGroup="VGriesgosIndicadores" ForeColor="Red" >*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr id="Subproceso">
                        <td class="RowsText">
                            <asp:Label ID="Label8" runat="server" Text="Subproceso:" CssClass="Apariencia"></asp:Label></td>
                        <td>
                            <asp:DropDownList ID="ddlSubproceso" runat="server" Width="300px" OnSelectedIndexChanged="ddlSubproceso_SelectedIndexChanged"
                                CssClass="Apariencia" AutoPostBack="True" DataTextField="Nombre" DataValueField="IdProceso">
                            </asp:DropDownList></td>
                        <td>
                            <asp:RequiredFieldValidator ID="rfvSubproceso" runat="server" ControlToValidate="ddlSubproceso"
                                ErrorMessage="Debe ingresar el Subproceso." ToolTip="Debe ingresar el Subproceso." Enabled="False"
                                ValidationGroup="VGriesgosIndicadores" ForeColor="Red">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td class="RowsText">
                            <asp:Label ID="Lresponsable" runat="server" Text="Responsable Medición:" CssClass="Apariencia" Width="300px"></asp:Label>
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
                                ValidationGroup="VGriesgosIndicadores" ForeColor="Red">*</asp:RequiredFieldValidator>
                        </td>

                    </tr>

                    <tr>
                        <td class="RowsText">
                            <asp:Label ID="lblFrecuencia" runat="server" Text="Frecuencia de Medición:" CssClass="Apariencia"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlFrecuenciaMedicion" runat="server" Width="300px"
                                CssClass="Apariencia">
                            </asp:DropDownList>

                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="rfvFreciencia" runat="server" ControlToValidate="ddlFrecuenciaMedicion"
                                ErrorMessage="Debe ingresar la frecuencia de medición." ToolTip="Debe ingresar la frecuencia de medición."
                                ValidationGroup="VGriesgosIndicadores" ForeColor="Red" InitialValue="1">*</asp:RequiredFieldValidator>

                        </td>
                    </tr>
                    <tr>
                        <td class="RowsText">
                            <asp:Label ID="Lusuario" runat="server" CssClass="Apariencia" Text="Usuario Creación:" Width="300px"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="tbxUsuarioCreacion" runat="server" CssClass="Apariencia" Enabled="False" Width="300px"></asp:TextBox>
                        </td>
                        <td></td>
                        <tr>
                            <td class="RowsText">
                                <asp:Label ID="LfechaCreacion" runat="server" CssClass="Apariencia" Text="Fecha de Creación:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtFecha" runat="server" CssClass="Apariencia" Enabled="False" Width="300px"></asp:TextBox>
                            </td>
                            <td></td>
                        </tr>
                </table>
            </div>
            <div id="Dbutton" runat="server" visible="false" class="ColumnStyle">
            <Table id="Tbuttons" class="tabla" align="center" width="25%">
                <tr align="center">
                        <td>
                            Para exportar a PDF:
                        </td>
                    <td>
                        <asp:ImageButton ID="ImButtonPDFexport" runat="server" ImageUrl="~/Imagenes/Icons/pdfImg.jpg" OnClick="ImButtonPDFexport_Click" />
                    </td>
                    <td>
                            Para exportar a Excel:
                        </td>
                    <td>
                        <asp:ImageButton ID="ImButtonExcelExport" runat="server" ImageUrl="~/Imagenes/Icons/excel.png" OnClick="ImButtonExcelExport_Click"/>
                    </td>
                    </tr>
                </Table>
        </div>
            <div runat="server" id="dvContentDetalleIndicador" visible="false">
                <table id="TbIndicadores" runat="server" align="center" class="tabla" width="80%">
                    <tr id="Tr8" align="center" runat="server">
                        <td id="Td20" bgcolor="#333399" runat="server">
                            <asp:Label ID="Label5" runat="server" Text="Detalle Información Indicador" Font-Bold="False"
                                Font-Names="Calibri" Font-Size="Small" Style="font-weight: 700; color: #FFFFFF;"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TabContainer ID="TabContainerIndicadores" runat="server" ActiveTabIndex="2"
                                Font-Names="Calibri" Font-Size="Small" Width="100%">
                                <ajax:TabPanel ID="TabRiesgos" runat="server" Font-Names="Calibri" Font-Size="Small" HeaderText="Asociación de Riesgos">
                                    <ContentTemplate>
                                        <table id="tbAsociacionRiesgo" runat="server" align="center">
                                            <tr align="center" runat="server">
                                                <td id="trTitutloConsultarRiesgo" runat="server" bgcolor="#BBBBBB" colspan="2">
                                                    <asp:Label ID="Label46" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Consultar Riesgos"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr id="trCodRiesgoSearch" runat="server" align="left">
                                                <td bgcolor="#BBBBBB" runat="server">
                                                    <asp:Label ID="Label75" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Código:"></asp:Label>
                                                </td>
                                                <td bgcolor="#EEEEEE" runat="server">
                                                    <asp:TextBox ID="TextBox31" runat="server" Font-Names="Calibri" Font-Size="Small" Width="150px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr id="trNombreRiesgoSearch" runat="server" align="left">
                                                <td bgcolor="#BBBBBB" runat="server">
                                                    <asp:Label ID="Label76" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Nombre:"></asp:Label>
                                                </td>
                                                <td bgcolor="#EEEEEE" runat="server">
                                                    <asp:TextBox ID="TextBox32" runat="server" Font-Names="Calibri" Font-Size="Small" Width="300px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr id="trButtonsSearch" runat="server" align="center">
                                                <td colspan="2" runat="server">
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:ImageButton ID="IBconsultarRiesgo" runat="server" ImageUrl="~/Imagenes/Icons/lupa.png" OnClick="IBconsultarRiesgo_Click" ToolTip="Consultar" />
                                                            </td>
                                                            <td>
                                                                <asp:ImageButton ID="IBcancelConsultaR" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png" ToolTip="Cancelar" OnClick="IBcancelConsultaR_Click" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr id="trTituloRiesgoA" runat="server" align="center" visible="False">
                                                <td bgcolor="#BBBBBB" colspan="2" runat="server">
                                                    <asp:Label ID="Label1" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Riesgos a Asociar"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr id="trRiesgoA" runat="server" align="center">
                                                <td colspan="2" runat="server">
                                                    <asp:GridView ID="GVriesgos" runat="server" AutoGenerateColumns="False" BorderStyle="Solid" CellPadding="4" DataKeyNames="ListaCausas,IdRiesgo,IdProbabilidad,IdImpacto" Font-Names="Calibri" Font-Size="Small" ForeColor="#333333" GridLines="Vertical" HorizontalAlign="Center" OnRowCommand="GVriesgos_RowCommand" ShowHeaderWhenEmpty="True" Visible="False">
                                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                        <Columns>
                                                            <asp:BoundField DataField="Codigo" HeaderText="Código Riesgo" />
                                                            <asp:BoundField DataField="Nombre" HeaderText="Nombre Riesgo" />
                                                            <asp:ButtonField ButtonType="Image" CommandName="Relacionar" HeaderText="Asociar" ImageUrl="~/Imagenes/Icons/select.png" Text="Asociar">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                            </asp:ButtonField>
                                                        </Columns>
                                                        <EditRowStyle BackColor="#999999" />
                                                        <FooterStyle BackColor="White" Font-Bold="True" ForeColor="White" />
                                                        <HeaderStyle BackColor="#5D7B9D" CssClass="gridViewHeader" Font-Bold="True" ForeColor="White" />
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
                                            <tr id="trRiesgoAsociadosText" runat="server" align="center" visible="False">
                                                <td bgcolor="#BBBBBB" colspan="2" runat="server">
                                                    <asp:Label ID="Label34" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Riesgos relacionados"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr id="trRiesgoAsociadosGrid" runat="server" align="center" visible="False">
                                                <td colspan="2" runat="server">
                                                    <asp:GridView ID="GVriesgoAsociado" runat="server" AutoGenerateColumns="False" BorderStyle="Solid" CellPadding="4" DataKeyNames="intIdRiesgoIndicador,intIdRiesgoAsociado" Font-Names="Calibri" Font-Size="Small" ForeColor="#333333" GridLines="Vertical" HorizontalAlign="Center" OnRowCommand="GVriesgoAsociado_RowCommand" ShowHeaderWhenEmpty="True">
                                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Código Riesgo">
                                                                <ItemTemplate>
                                                                    <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 100px">
                                                                        <asp:Label ID="strCodRiesgo" runat="server" Text='<% # Bind("strCodRiesgo")%>'></asp:Label>
                                                                    </div>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                                                <ItemStyle Wrap="False" HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Nombre Riesgo">
                                                                <ItemTemplate>
                                                                    <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 100px">
                                                                        <asp:Label ID="strNombreRiesgo" runat="server" Text='<% # Bind("strNombreRiesgo")%>'></asp:Label>
                                                                    </div>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                                                <ItemStyle Wrap="False" HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:ButtonField ButtonType="Image" CommandName="Desasociar" HeaderText="Desasociar" ImageUrl="~/Imagenes/Icons/delete.png" Text="Desasociar">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                            </asp:ButtonField>
                                                        </Columns>
                                                        <EditRowStyle BackColor="#999999" />
                                                        <FooterStyle BackColor="White" Font-Bold="True" ForeColor="White" />
                                                        <HeaderStyle BackColor="#5D7B9D" CssClass="gridViewHeader" Font-Bold="True" ForeColor="White" />
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
                                    </ContentTemplate>
                                </ajax:TabPanel>
                                <ajax:TabPanel ID="TabPanelVariables" runat="server" Font-Names="Calibri" Font-Size="Small" HeaderText="Definición de Variables">
                                    <ContentTemplate>
                                        <table id="TbVariables" runat="server" align="center">
                                            <tr runat="server">
                                                <td runat="server">
                                                    <table id="TbCrearVariables" runat="server" align="center">
                                                        <tr id="Tr4" runat="server" align="center">
                                                            <td runat="server" bgcolor="#5D7B9D">
                                                                <asp:Label ID="Label24" runat="server" Font-Bold="False" Font-Names="Calibri" Font-Size="Small" Style="font-weight: 700; color: #FFFFFF;" Text="Variables"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr id="Tr5" runat="server">
                                                            <td runat="server">
                                                                <asp:GridView ID="GridViewVariables" runat="server" AutoGenerateColumns="False" BorderStyle="Solid" CellPadding="4" DataKeyNames="intIdFormato,intIdVariableRiesgoIndicador" Font-Names="Calibri" Font-Size="Small" ForeColor="#333333" GridLines="Vertical" OnRowCommand="GridViewVariables_RowCommand" ShowHeaderWhenEmpty="True" Width="91px">
                                                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                                    <Columns>
                                                                        <asp:BoundField DataField="intIdVariableRiesgoIndicador" HeaderText="Id" Visible="False" />
                                                                        <asp:TemplateField HeaderText="Descripción" ItemStyle-HorizontalAlign="Left">
                                                                            <ItemTemplate>
                                                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 100px">
                                                                                    <asp:Label ID="strDescripcion" runat="server" Text='<% # Bind("strDescripcion")%>'></asp:Label>
                                                                                </div>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="center" Wrap="false" />
                                                                            <ItemStyle Wrap="false" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Formato" ItemStyle-HorizontalAlign="Left">
                                                                            <ItemTemplate>
                                                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 100px">
                                                                                    <asp:Label ID="strFormato" runat="server" Text='<% # Bind("strFormato")%>'></asp:Label>
                                                                                </div>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="center" Wrap="false" />
                                                                            <ItemStyle Wrap="false" />
                                                                        </asp:TemplateField>
                                                                        <asp:BoundField DataField="dblValorVariable" HeaderText="Valor" Visible="false" />
                                                                        <asp:ButtonField ButtonType="Image" CommandName="ModificarVariable" HeaderText="Modificar" ImageUrl="~/Imagenes/Icons/edit.png" Text="ModificarVariable">
                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                        </asp:ButtonField>
                                                                        <asp:ButtonField ButtonType="Image" CommandName="EliminarVariable" HeaderText="Eliminar" ImageUrl="~/Imagenes/Icons/delete.png" Text="EliminarVariable">
                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                        </asp:ButtonField>
                                                                    </Columns>
                                                                    <EditRowStyle BackColor="#999999" />
                                                                    <FooterStyle BackColor="White" Font-Bold="True" ForeColor="White" />
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
                                                        <tr id="Tr6" runat="server" align="center">
                                                            <td id="Td9" runat="server">
                                                                <asp:ImageButton ID="BtnAdicionaVariable" runat="server" ImageUrl="~/Imagenes/Icons/Add.png" OnClick="BtnAdicionaVariable_Click1" ToolTip="Agregar" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <table id="TbAddVaiables" runat="server" align="left" visible="False">
                                                        <tr id="Tr9" runat="server" align="center">
                                                            <td id="Td36" runat="server" bgcolor="#5D7B9D" colspan="2">
                                                                <asp:Label ID="Label10" runat="server" Font-Bold="False" Font-Names="Calibri" Font-Size="Small" Style="font-weight: 700; color: #FFFFFF;" Text="Crear Variable"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr id="Tr16" runat="server">
                                                            <td id="Td37" runat="server" bgcolor="#5D7B9D" style="color: #FFFFFF; font-weight: 700">
                                                                <asp:Label ID="Label11" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Descipción:"></asp:Label>
                                                            </td>
                                                            <td id="Td38" runat="server">
                                                                <asp:TextBox ID="TextBox1" runat="server" Font-Names="Calibri" Font-Size="Small" Width="150px"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBox1" Display="Dynamic" ForeColor="Red" ValidationGroup="AddVariable">*</asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr id="TrValorVariable" runat="server" visible="false">
                                                            <td id="Td1" runat="server" bgcolor="#5D7B9D" style="color: #FFFFFF; font-weight: 700">
                                                                <asp:Label ID="lblValorVariable" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Valor de la variable:"></asp:Label>
                                                            </td>
                                                            <td id="Td2" runat="server">
                                                                <asp:TextBox ID="txtValorVariable" runat="server" Font-Names="Calibri" Font-Size="Small" Width="150px"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvValorVariable" runat="server" ControlToValidate="txtValorVariable" Display="Dynamic" ForeColor="Red" ValidationGroup="AddVariable">*</asp:RequiredFieldValidator>
                                                                <asp:RegularExpressionValidator ID="revValorVariable" runat="server" ControlToValidate="txtValorVariable" ErrorMessage="Solo números Enteros o Decimales" ForeColor="Red" ToolTip="Solo números Enteros o Decimales" ValidationExpression="^\d{1,2}(?:,\s?\d{2})*(?:\.\d*)?$" ValidationGroup="AddVariable">*</asp:RegularExpressionValidator>
                                                            </td>
                                                        </tr>
                                                        <tr id="Tr17" runat="server">
                                                            <td id="Td39" runat="server" bgcolor="#5D7B9D" style="color: #FFFFFF; font-weight: 700">
                                                                <asp:Label ID="Label12" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Formato:"></asp:Label>
                                                            </td>
                                                            <td id="Td41" runat="server">
                                                                <asp:DropDownList ID="DropDownListFormato" runat="server" Font-Names="Calibri" Font-Size="Small" Width="150px">
                                                                </asp:DropDownList>
                                                                <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="DropDownListFormato" ForeColor="Red" Operator="NotEqual" ValidationGroup="AddVariable" ValueToCompare="---">*</asp:CompareValidator>
                                                            </td>
                                                        </tr>
                                                        <tr runat="server">
                                                            <td runat="server" align="center" colspan="2">
                                                                <asp:ImageButton ID="BtnGuardarVariable" runat="server" CausesValidation="true" ImageUrl="~/Imagenes/Icons/guardar.png" OnClick="BtnGuardarVariable_Click" Style="height: 20px; width: 20px;" ToolTip="Guardar" ValidationGroup="AddVariable" />
                                                                <asp:ImageButton ID="BtnCancelaVariable" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png" OnClick="BtnCancelaVariable_Click" ToolTip="Cancelar" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <table id="TbModificaVariable" runat="server" align="left" visible="False">
                                                        <tr id="Tr21" runat="server" align="center">
                                                            <td id="Td47" runat="server" bgcolor="#5D7B9D" colspan="2">
                                                                <asp:Label ID="Label14" runat="server" Font-Bold="False" Font-Names="Calibri" Font-Size="Small" Style="font-weight: 700; color: #FFFFFF;" Text="Modificar Variable"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr id="Tr22" runat="server">
                                                            <td id="Td48" runat="server" bgcolor="#5D7B9D" style="color: #FFFFFF; font-weight: 700">
                                                                <asp:Label ID="Label17" runat="server" Font-Names="Calibri" Font-Size="Small" Visible="False"></asp:Label>
                                                                <asp:Label ID="Label15" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Descipción:"></asp:Label>
                                                            </td>
                                                            <td id="Td50" runat="server">
                                                                <asp:TextBox ID="TextBox2" runat="server" Font-Names="Calibri" Font-Size="Small" Width="150px"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TextBox2" Display="Dynamic" ForeColor="Red" ValidationGroup="updateVariable">*</asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr id="TrValorVariableUp" runat="server" visible="false">
                                                            <td id="Td3" runat="server" bgcolor="#5D7B9D" style="color: #FFFFFF; font-weight: 700">
                                                                <asp:Label ID="lblValorVariableUp" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Valor de la variable:"></asp:Label>
                                                            </td>
                                                            <td id="Td4" runat="server">
                                                                <asp:TextBox ID="txtValorVariableUp" runat="server" Font-Names="Calibri" Font-Size="Small" Width="150px"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvValorVariableUp" runat="server" ControlToValidate="txtValorVariableUp" Display="Dynamic" ForeColor="Red" ValidationGroup="AddVariable">*</asp:RequiredFieldValidator>
                                                                <asp:RegularExpressionValidator ID="revValorVariableUp" runat="server" ControlToValidate="txtValorVariableUp" ErrorMessage="Solo números Enteros o Decimales" ForeColor="Red" ToolTip="Solo números Enteros o Decimales" ValidationExpression="^\d{1,2}(?:,\s?\d{2})*(?:\.\d*)?$" ValidationGroup="updateVariable">*</asp:RegularExpressionValidator>
                                                            </td>
                                                        </tr>
                                                        <tr id="Tr23" runat="server">
                                                            <td id="Td52" runat="server" bgcolor="#5D7B9D" style="color: #FFFFFF; font-weight: 700">
                                                                <asp:Label ID="Label16" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Formato:"></asp:Label>
                                                            </td>
                                                            <td id="Td53" runat="server">
                                                                <asp:DropDownList ID="DropDownListFormatoUp" runat="server" Font-Names="Calibri" Font-Size="Small" Width="150px">
                                                                </asp:DropDownList>
                                                                <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="DropDownListFormatoUp" ForeColor="Red" Operator="NotEqual" ValidationGroup="updateVariable" ValueToCompare="---">*</asp:CompareValidator>
                                                            </td>
                                                        </tr>
                                                        <tr id="Tr24" runat="server">
                                                            <td id="Td54" runat="server" align="center" colspan="2">
                                                                <asp:ImageButton ID="BtnModificaVariable" runat="server" CausesValidation="true" ImageUrl="~/Imagenes/Icons/guardar.png" OnClick="BtnModificaVariable_Click" Style="height: 20px; width: 20px;" ToolTip="Guardar" ValidationGroup="updateVariable" />
                                                                <asp:ImageButton ID="BtnCancelaAUpVariable" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png" OnClick="BtnCancelaAUpVariable_Click" ToolTip="Cancelar" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </ajax:TabPanel>
                                <ajax:TabPanel ID="TabPanelMetas" runat="server" Font-Names="Calibri" Font-Size="Small" HeaderText="Definición de Metas">
                                    <ContentTemplate>
                                        <br />
                                        <br />
                                        <table id="VerlaMetas" runat="server" align="center">
                                            <tr runat="server">
                                                <td id="Td8" runat="server" class="auto-style1">
                                                    <table id="TbMetas" runat="server" align="center">
                                                        <tr id="Tr25" runat="server" align="center">
                                                            <td id="Td17" runat="server" bgcolor="#5D7B9D">
                                                                <asp:Label ID="Label23" runat="server" Font-Bold="False" Font-Names="Calibri" Font-Size="Small" Style="font-weight: 700; color: #FFFFFF;" Text="Metas"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr id="Tr26" runat="server">
                                                            <td id="Td56" runat="server">
                                                                <asp:GridView ID="GridViewMetas" runat="server" AllowPaging="True" AutoGenerateColumns="False" 
                                                                    BorderStyle="Solid" CellPadding="4" DataKeyNames="intIdMeta" Font-Names="Calibri" Font-Size="Small" ForeColor="#333333" 
                                                                    GridLines="Vertical" OnRowCommand="GridViewMetas_RowCommand" PageSize="6" ShowHeaderWhenEmpty="True" OnPreRender="GridViewMetas_PreRender" OnPageIndexChanging="GridViewMetas_PageIndexChanging">
                                                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                                    <Columns>
                                                                        <asp:BoundField DataField="intIdRiesgoIndicador" HeaderText="Id" Visible="False" />
                                                                        <asp:BoundField DataField="intIdMeta" HeaderText="IdMeta" Visible="False" />
                                                                        <asp:TemplateField HeaderText="Tipo Frecuencia">
                                                                            <ItemTemplate>
                                                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 100px">
                                                                                    <asp:Label ID="strDetalleFrecuencia" runat="server" Text='<% # Bind("strDetalleFrecuencia")%>'></asp:Label>
                                                                                </div>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                                                            <ItemStyle Wrap="False" HorizontalAlign="Left" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Valor Frecuencia">
                                                                            <ItemTemplate>
                                                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 100px">
                                                                                    <asp:Label ID="strValorOtraFrecuencia" runat="server" Text='<% # Bind("strValorOtraFrecuencia")%>'></asp:Label>
                                                                                </div>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                                                            <ItemStyle Wrap="False" HorizontalAlign="Left" />
                                                                        </asp:TemplateField>
                                                                        <asp:BoundField DataField="dblMeta" HeaderText="Meta">
                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="strAño" HeaderText="Año" HtmlEncode="False" />
                                                                        <asp:BoundField DataField="strMes" HeaderText="Mes" HtmlEncode="False" />
                                                                        <asp:ButtonField ButtonType="Image" CommandName="ModificarMeta" HeaderText="Modificar" ImageUrl="~/Imagenes/Icons/edit.png" Text="Modificar">
                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                        </asp:ButtonField>
                                                                        <asp:ButtonField ButtonType="Image" CommandName="EliminarMeta" HeaderText="Eliminar" ImageUrl="~/Imagenes/Icons/delete.png" Text="Eliminar">
                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                        </asp:ButtonField>
                                                                        <asp:BoundField DataField="Dia" HeaderText="Dia" Visible="False" />
                                                                        <asp:BoundField DataField="Mes" HeaderText="Mes" Visible="False" />
                                                                        <asp:BoundField DataField="Ano" HeaderText="Año" Visible="False" />
                                                                        
                                                                    </Columns>
                                                                    <EditRowStyle BackColor="#999999" />
                                                                    <FooterStyle BackColor="White" Font-Bold="True" ForeColor="White" />
                                                                    <HeaderStyle BackColor="#5D7B9D" CssClass="gridViewHeader" Font-Bold="True" ForeColor="White" />
                                                                    <PagerStyle BackColor="#5D7B9D" ForeColor="White" HorizontalAlign="Center" />
                                                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                                                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                                                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                                                </asp:GridView>
                                                            </td>
                                                        </tr>
                                                        <tr id="Tr27" runat="server" align="center">
                                                            <td id="Td57" runat="server">
                                                                <asp:ImageButton ID="BtnAdicionaMeta" runat="server" ImageUrl="~/Imagenes/Icons/Add.png" OnClick="BtnAdicionaMeta_Click" ToolTip="Agregar" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <table id="TbAddMetas" runat="server" align="left" visible="False">
                                                        <tr id="Tr28" runat="server" align="center">
                                                            <td id="Td58" runat="server" bgcolor="#5D7B9D" colspan="4">
                                                                <asp:Label ID="Label28" runat="server" Font-Bold="False" Font-Names="Calibri" Font-Size="Small" Style="font-weight: 700; color: #FFFFFF;" Text="Crear Metas"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr id="Tr30" runat="server">
                                                            <td id="Td61" runat="server" bgcolor="#5D7B9D" style="color: #FFFFFF; font-weight: 700">
                                                                <asp:Label ID="lblIdMeta" runat="server" Visible="False"></asp:Label>
                                                                <asp:Label ID="Label30" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Periodo:"></asp:Label>
                                                            </td>
                                                            <td id="Td62" runat="server">
                                                                <asp:TextBox ID="TextBox12" runat="server" Font-Names="Calibri" Font-Size="Small" Visible="False" Width="100px"></asp:TextBox>
                                                                <ajax:CalendarExtender ID="CalendarExtender3" runat="server" BehaviorID="_content_CalendarExtender3" Format="yyyy-MM-dd" TargetControlID="TextBox12" />
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorDia" runat="server" ControlToValidate="TextBox12" Display="Dynamic" ForeColor="Red">*</asp:RequiredFieldValidator>
                                                                <asp:DropDownList ID="ddlDetalleFrecuencias" runat="server" Font-Names="Calibri" Font-Size="Small" Visible="False" Width="200px" />
                                                                <asp:CompareValidator ID="CompareValidatorAno" runat="server" ControlToValidate="ddlDetalleFrecuencias" ForeColor="Red" Operator="NotEqual" ValueToCompare="---">*</asp:CompareValidator>
                                                                <asp:TextBox ID="txtFrecuenciaAno" runat="server" Font-Names="Calibri" Font-Size="Small" Visible="False" Width="100px"></asp:TextBox>
                                                                <ajax:CalendarExtender ID="ceFrecuenciaAno" runat="server" BehaviorID="_content_ceFrecuenciaAno" DefaultView="Years" Format="yyyy" TargetControlID="txtFrecuenciaAno" />
                                                            </td>
                                                            <td runat="server"></td>
                                                            <td runat="server"></td>
                                                        </tr>
                                                        <tr id="TrfechasIns" runat="server" visible="False">
                                                            <td id="TdAñoMeta" runat="server" bgcolor="#5D7B9D" style="color: #FFFFFF; font-weight: 700">
                                                                <asp:Label ID="lblAnñoMeta" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Año:"></asp:Label>
                                                            </td>
                                                            <td id="TdValorAñoMeta" runat="server">
                                                                <asp:TextBox ID="txbAñoMeta" runat="server" Font-Names="Calibri" Font-Size="Small" Width="100px"></asp:TextBox>
                                                                <ajax:CalendarExtender ID="ceAñoMeta" runat="server" BehaviorID="_content_ceAñoMeta" Format="yyyy" TargetControlID="txbAñoMeta" />
                                                                <asp:RequiredFieldValidator ID="rfvAñoMeta" runat="server" ControlToValidate="txbAñoMeta" Display="Dynamic" 
                                                                    ForeColor="Red" ValidationGroup="AddMeta">*</asp:RequiredFieldValidator>
                                                            </td>
                                                            <td id="TdmesMeta" runat="server" bgcolor="#5D7B9D" style="color: #FFFFFF; font-weight: 700">
                                                                <asp:Label ID="lblMesMeta" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Mes:"></asp:Label>
                                                            </td>
                                                            <td id="TdvalorMesMeta" runat="server">
                                                                <asp:DropDownList ID="ddlMesMetas" runat="server" Width="150px">
                                                    <asp:ListItem Text="--seleccionar--" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="Enero" Value="Enero"></asp:ListItem>
                                                    <asp:ListItem Text="Febrero" Value="Febrero"></asp:ListItem>
                                                    <asp:ListItem Text="Marzo" Value="Marzo"></asp:ListItem>
                                                    <asp:ListItem Text="Abril" Value="Abril"></asp:ListItem>
                                                    <asp:ListItem Text="Mayo" Value="Mayo"></asp:ListItem>
                                                    <asp:ListItem Text="Junio" Value="Junio"></asp:ListItem>
                                                    <asp:ListItem Text="Julio" Value="Julio"></asp:ListItem>
                                                    <asp:ListItem Text="Agosto" Value="Agosto"></asp:ListItem>
                                                    <asp:ListItem Text="Septiembre" Value="Septiembre"></asp:ListItem>
                                                    <asp:ListItem Text="Octubre" Value="Octubre"></asp:ListItem>
                                                    <asp:ListItem Text="Noviembre" Value="Noviembre"></asp:ListItem>
                                                    <asp:ListItem Text="Diciembre" Value="Diciembre"></asp:ListItem>
                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txbAñoMeta" Display="Dynamic" 
                                                                    ForeColor="Red" ValidationGroup="AddMeta">*</asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr id="Tr29" runat="server">
                                                            <td id="Td59" runat="server" bgcolor="#5D7B9D" style="color: #FFFFFF; font-weight: 700">
                                                                <asp:Label ID="Label29" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Valor:"></asp:Label>
                                                            </td>
                                                            <td id="Td60" runat="server">
                                                                <asp:TextBox ID="TextBox10" runat="server" Font-Names="Calibri" Font-Size="Small" Width="100px"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="TextBox10" 
                                                                    Display="Dynamic" ForeColor="Red" ValidationGroup="AddMeta">*</asp:RequiredFieldValidator>
                                                            </td>
                                                            <td runat="server"></td>
                                                            <td runat="server"></td>
                                                        </tr>
                                                        
                                                        <tr id="Tr31" runat="server">
                                                            <td id="Td63" runat="server" align="center" colspan="2">
                                                                <asp:ImageButton ID="BtnGuardarMeta" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png" OnClick="BtnGuardarMeta_Click" Style="height: 20px; width: 20px;" ToolTip="Guardar" ValidationGroup="AddMeta" Visible="False" />
                                                                <asp:ImageButton ID="BtnUpdaterMeta" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png" OnClick="BtnUpdaterMeta_Click" Style="height: 20px; width: 20px;" ToolTip="Guardar" ValidationGroup="AddMeta" Visible="False" />
                                                                <asp:ImageButton ID="BtnCancelaMeta" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png" OnClick="BtnCancelaMeta_Click" ToolTip="Cancelar" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </ajax:TabPanel>
                                <ajax:TabPanel ID="TabPanelFormulacion" runat="server" Font-Names="Calibri" Font-Size="Small" HeaderText="Formulación">
                                    <ContentTemplate>
                                        <table align="center" class="tabla" width="80%">
                                            <tr>
                                                <td>
                                                    <table id="TbSeleccionarVariables" runat="server" align="center" visible="False">
                                                        <tr id="Tr10" runat="server" align="center">
                                                            <td id="Td7" runat="server" bgcolor="#5D7B9D">
                                                                <asp:Label ID="Label3" runat="server" Font-Bold="False" Font-Names="Calibri" Font-Size="Small" Style="font-weight: 700; color: #FFFFFF;" Text="Variables Disponibles"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr id="Tr11" runat="server">
                                                            <td runat="server">
                                                                <asp:GridView ID="GridViewSeleccVariables" runat="server" AutoGenerateColumns="False" BorderStyle="Solid" CellPadding="4" Font-Names="Calibri" Font-Size="Small" ForeColor="#333333" GridLines="Vertical" HorizontalAlign="Center" OnRowCommand="GridViewSeleccVariables_RowCommand" ShowHeaderWhenEmpty="True" Width="127px">
                                                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                                    <Columns>
                                                                        <asp:BoundField DataField="intIdVariableRiesgoIndicador" HeaderText="Id" Visible="False" />
                                                                        <asp:BoundField DataField="strDescripcion" HeaderText="Descripción" />
                                                                        <asp:BoundField DataField="strFormato" HeaderText="Formato">
                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="dblValorVariable" HeaderText="Valor" Visible="false" />
                                                                        <asp:ButtonField ButtonType="Image" CommandName="SelecVariable" HeaderText="Acción" ImageUrl="~/Imagenes/Icons/select.png" Text="SelecVariable">
                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                        </asp:ButtonField>
                                                                    </Columns>
                                                                    <EditRowStyle BackColor="#999999" />
                                                                    <FooterStyle BackColor="White" Font-Bold="True" ForeColor="White" />
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
                                                    </table>
                                                </td>
                                                <td>
                                                    <table id="TbCalculadora" runat="server" align="center">
                                                        <tr id="T2" runat="server" align="center">
                                                            <td id="Td10" runat="server" colspan="2"></td>
                                                            <td id="Td12" runat="server">
                                                                <asp:Button ID="ButtonMas" runat="server" Font-Names="Calibri" Font-Size="Small" OnClick="ButtonMas_Click" Text="+" Width="30px" />
                                                            </td>
                                                            <td id="Td13" runat="server">
                                                                <asp:Button ID="ButtonPor" runat="server" Font-Names="Calibri" Font-Size="Small" OnClick="ButtonPor_Click" Text="x" Width="30px" />
                                                            </td>
                                                            <td id="Td14" runat="server">
                                                                <asp:Button ID="ButtonAbreP" runat="server" Font-Names="Calibri" Font-Size="Small" OnClick="ButtonAbreP_Click" Text="(" Width="30px" />
                                                            </td>
                                                            <td id="Td15" runat="server">
                                                                <asp:Button ID="ButtonCierraP" runat="server" Font-Names="Calibri" Font-Size="Small" OnClick="ButtonCierraP_Click" Text=")" Width="30px" />
                                                            </td>
                                                            <td id="Td19" runat="server">
                                                                <asp:Button ID="ButtonDel" runat="server" Font-Names="Calibri" Font-Size="Small" OnClick="ButtonDel_Click" Style="font-weight: 700; color: #FF0000" Text="C" Width="30px" />
                                                            </td>
                                                        </tr>
                                                        <tr id="T3" runat="server" align="center">
                                                            <td id="Td22" runat="server" colspan="2">
                                                                <asp:Label ID="Label26" runat="server" Text="Label" Visible="False"></asp:Label>
                                                            </td>
                                                            <td id="Td24" runat="server">
                                                                <asp:Button ID="ButtonMenos" runat="server" Font-Names="Calibri" Font-Size="Small" OnClick="ButtonMenos_Click" Text="-" Width="30px" />
                                                            </td>
                                                            <td id="Td25" runat="server">
                                                                <asp:Button ID="ButtonDivide" runat="server" Font-Names="Calibri" Font-Size="Small" OnClick="ButtonDivide_Click" Text="/" Width="30px" />
                                                            </td>
                                                            <td id="Td26" runat="server">
                                                                <asp:Button ID="ButtonPorc" runat="server" Font-Names="Calibri" Font-Size="Small" OnClick="ButtonPorc_Click" Text="%" Width="30px" />
                                                            </td>
                                                            <td id="Td27" runat="server">
                                                                <asp:Button ID="ButtonCero" runat="server" Font-Names="Calibri" Font-Size="Small" OnClick="ButtonCero_Click" Text="0" Width="30px" />
                                                            </td>
                                                            <td id="Td28" runat="server">
                                                                <asp:Button ID="ButtonCien" runat="server" Font-Names="Calibri" Font-Size="Small" OnClick="ButtonCien_Click" Text="100" Width="30px" />
                                                            </td>
                                                        </tr>
                                                        <tr id="T5" runat="server" align="center">
                                                            <td id="Td40" runat="server"></td>
                                                            <td id="Td11" runat="server" align="center">
                                                                <asp:Button ID="BtnNominador" runat="server" Font-Names="Calibri" Font-Size="Small" OnClick="BtnNominador_Click" Text="Nominador:" ToolTip="Nominador" Width="104px" />
                                                                <br />
                                                                <asp:Image ID="ImageNominadr" runat="server" ImageUrl="~/Imagenes/Icons/suc.png" Visible="False" />
                                                                <br />
                                                            </td>
                                                            <td id="Td21" runat="server" colspan="5">
                                                                <asp:TextBox ID="TextBox5" runat="server" BorderStyle="Solid" BorderWidth="1px" Enabled="False" Font-Names="Calibri" Font-Size="Small" TextMode="MultiLine" Width="274px"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="TextBox5" Display="Dynamic" ForeColor="Red" ValidationGroup="AddIndicadorAll">*</asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr id="T6" runat="server" align="center">
                                                            <td id="Td49" runat="server"></td>
                                                            <td id="Td16" runat="server" align="center">
                                                                <asp:Button ID="BtnDenominador" runat="server" Font-Names="Calibri" Font-Size="Small" OnClick="BtnDenominador_Click" Text="Denominador:" ToolTip="Denominador" Width="103px" />
                                                                <br />
                                                                <asp:Image ID="ImageDenominador" runat="server" ImageUrl="~/Imagenes/Icons/suc.png" Visible="False" />
                                                                <br />
                                                            </td>
                                                            <td id="Td51" runat="server" colspan="5">
                                                                <asp:TextBox ID="TextBox6" runat="server" BorderStyle="Solid" BorderWidth="1px" Enabled="False" Font-Names="Calibri" Font-Size="Small" TextMode="MultiLine" Width="274px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr runat="server">
                                                            <td runat="server" align="center" colspan="7">&nbsp;&nbsp;</td>
                                                        </tr>
                                                    </table>
                                                    <br />
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </ajax:TabPanel>
                                <ajax:TabPanel ID="TabPanelVerFormula" runat="server" Font-Names="Calibri" Font-Size="Small" HeaderText="Ver Fórmula">
                                    <ContentTemplate>
                                        <br />
                                        <table id="VerlaFormula" runat="server" align="center">
                                            <tr runat="server">
                                                <td runat="server">
                                                    <table id="LaFormula" runat="server" align="center">
                                                        <tr id="Tr12" runat="server" align="center">
                                                            <td id="Td29" runat="server" bgcolor="#5D7B9D" colspan="2">
                                                                <asp:Label ID="Label9" runat="server" Font-Bold="False" Font-Names="Calibri" Font-Size="Small" Style="font-weight: 700; color: #FFFFFF;" Text="Fórmula del Indicador"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr id="Tr13" runat="server">
                                                            <td id="Td30" runat="server"></td>
                                                            <td id="Td31" runat="server">Dar resultado en % <asp:CheckBox runat="server" ID="ckbResultPor" /></td>
                                                        </tr>
                                                        <tr id="Tr18" runat="server">
                                                            <td id="Td42" runat="server"></td>
                                                            <td id="Td43" runat="server" align="center">
                                                                <asp:TextBox ID="TextBox7" runat="server" BorderStyle="Solid" BorderWidth="1px" Enabled="False" Font-Names="Calibri" Font-Size="Small" TextMode="MultiLine" Width="274px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr id="Tr14" runat="server">
                                                            <td id="Td32" runat="server">
                                                                <asp:Label ID="Label13" runat="server" Font-Bold="False" Font-Names="Calibri" Font-Size="Medium" Style="font-weight: 700; color: #5D7B9D;"></asp:Label>
                                                                <asp:Label ID="lblIdFormula" runat="server" Visible="false"></asp:Label>
                                                            </td>
                                                            <td id="Td33" runat="server">
                                                                <asp:Label ID="Label18" runat="server" Font-Bold="True" Font-Names="Calibri" Font-Size="Small" Style="font-weight: 700; color: #5D7B9D;" Text="_________________________________________________________"></asp:Label>
                                                                <br />
                                                                <br />
                                                            </td>
                                                        </tr>
                                                        <tr id="Tr15" runat="server">
                                                            <td id="Td34" runat="server"></td>
                                                            <td id="Td35" runat="server" align="center">
                                                                <asp:TextBox ID="TextBox8" runat="server" BorderStyle="Solid" BorderWidth="1px" Enabled="False" Font-Names="Calibri" Font-Size="Small" TextMode="MultiLine" Width="274px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr runat="server">
                                                            <td align="center" colspan="2" runat="server">
                                                                <asp:ImageButton ID="IBinsertFormula" runat="server" CommandName="Insert" ImageUrl="~/Imagenes/Icons/guardar.png" OnClick="IBinsertFormula_Click" Text="Insert" ToolTip="Insertar" ValidationGroup="VGprograma" Width="20px" />
                                                                <asp:ImageButton ID="IBupdateFormula" runat="server" CommandName="Update" ImageUrl="~/Imagenes/Icons/guardar.png" OnClick="IBupdateFormula_Click" Text="Update" ToolTip="Actualizar" ValidationGroup="VGprograma" Visible="False" />
                                                                <asp:ImageButton ID="IBcancelFormula" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png" ToolTip="Cancelar" Visible="False" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </ajax:TabPanel>
                                <ajax:TabPanel ID="TabSeguimiento" runat="server" Font-Names="Calibri" Font-Size="Small" HeaderText="Seguimiento">
                                    <ContentTemplate>
                                        <div id="dvContentSeguimiento" runat="server">
                                            <table id="tbGridSeguimiento" runat="server" align="center">
                                                <tr id="trGridSeguimiento" runat="server">
                                                    <td>
                                                        <asp:GridView ID="GVseguimiento" runat="server" AllowPaging="True" AllowSorting="True" 
                                                            AutoGenerateColumns="False" BorderStyle="Solid" CellPadding="4" CssClass="Apariencia" DataKeyNames="strColor" Font-Bold="False" 
                                                            ForeColor="#333333" GridLines="Vertical" HeaderStyle-CssClass="gridViewHeader" OnRowCommand="GVseguimiento_RowCommand" 
                                                            ShowHeaderWhenEmpty="True">
                                                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Código" ItemStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 150px">
                                                                            <asp:Label ID="intIdEsquemaSeguimiento" runat="server" Text='<% # Bind("intIdEsquemaSeguimiento")%>'></asp:Label>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="center" Wrap="false" />
                                                                    <ItemStyle Wrap="false" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Descripción Seguimiento" ItemStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 150px">
                                                                            <asp:Label ID="strDescripcionSeguimiento" runat="server" Text='<% # Bind("strDescripcionSeguimiento")%>'></asp:Label>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="center" Wrap="false" />
                                                                    <ItemStyle Wrap="false" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Valor Minimo" ItemStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 150px">
                                                                            <asp:Label ID="dblValorMinimo" runat="server" Text='<% # Bind("dblValorMinimo")%>'></asp:Label>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="center" Wrap="false" />
                                                                    <ItemStyle Wrap="false" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Valor Máximo" ItemStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 150px">
                                                                            <asp:Label ID="dblValorMaximo" runat="server" Text='<% # Bind("dblValorMaximo")%>'></asp:Label>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="center" Wrap="false" />
                                                                    <ItemStyle Wrap="false" />
                                                                </asp:TemplateField>
                                                                <asp:ButtonField ButtonType="Image" CommandName="ModificarSeguimiento" HeaderText="Modificar" ImageUrl="~/Imagenes/Icons/edit.png" Text="ModificarSeguimiento">
                                                                <ItemStyle HorizontalAlign="Center" />
                                                                </asp:ButtonField>
                                                                <asp:ButtonField ButtonType="Image" CommandName="EliminarSeguimiento" HeaderText="Eliminar" ImageUrl="~/Imagenes/Icons/delete.png" Text="EliminarSeguimiento">
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
                                                <tr>
                                                    <td>
                                                        <asp:ImageButton ID="ImbNewSeguimiento" runat="server" ImageUrl="~/Imagenes/Icons/Add.png" OnClick="ImbNewSeguimiento_Click" ToolTip="Agregar" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <div id="dvFormSeguimiento" runat="server" visible="false">
                                            <table align="center" class="tabla" width="65%">
                                                <tr>
                                                    <td class="RowsText">
                                                        <asp:Label ID="lblCodSeg" runat="server" CssClass="Apariencia" Text="Código:"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtCodSeg" runat="server" CssClass="Apariencia" Enabled="false" Width="300px"></asp:TextBox>
                                                    </td>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td class="RowsText">
                                                        <asp:Label ID="lblValorMinimo" runat="server" CssClass="Apariencia" Text="Valor Minimo:"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtValorMinimo" runat="server" CssClass="Apariencia" Width="300px"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:RequiredFieldValidator ID="rfvValorMinimo" runat="server" ControlToValidate="txtValorMinimo" ErrorMessage="Debe ingresar el valor minimo." ForeColor="Red" ToolTip="Debe ingresar el valor minimo." ValidationGroup="VGseguimiento">*</asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="revValorMinimo" runat="server" ControlToValidate="txtValorMinimo" ErrorMessage="Solo números Enteros o Decimales" ForeColor="Red" ToolTip="Solo números Enteros o Decimales" ValidationExpression="^\d{1,2}(?:,\s?\d{2})*(?:\.\d*)?$" ValidationGroup="VGseguimiento">*</asp:RegularExpressionValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="RowsText">
                                                        <asp:Label ID="lblValorMaximo" runat="server" CssClass="Apariencia" Text="Valor Máximo:" Width="300px"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtValorMaximo" runat="server" Font-Names="Calibri" Font-Size="Small" ValidationGroup="VGseguimiento" Width="300px"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:RequiredFieldValidator ID="rfvValorMaximo" runat="server" ControlToValidate="txtValorMaximo" ErrorMessage="Debe ingresar el valor máximo." ForeColor="Red" ToolTip="Debe ingresar el valor máximo." ValidationGroup="VGseguimiento">*</asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="revValorMaximo" runat="server" ControlToValidate="txtValorMaximo" ErrorMessage="Solo números Enteros o Decimales" ForeColor="Red" ToolTip="Solo números Enteros o Decimales" ValidationExpression="^(\d{3}\,)?(\d+\,?)+(,\d{2})?$" ValidationGroup="VGseguimiento">*</asp:RegularExpressionValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="RowsText">
                                                        <asp:Label ID="lblSeguimiento" runat="server" CssClass="Apariencia" Text="Seguimiento:" Width="300px"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtSeguimiento" runat="server" Font-Names="Calibri" Font-Size="Small" Height="75px" TextMode="MultiLine" ValidationGroup="VGseguimiento" Width="300px" Wrap="false"></asp:TextBox>
                                                    </td>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td class="RowsText">
                                                        <asp:Label ID="lblColor" runat="server" CssClass="Apariencia" Text="Color:" Width="300px"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlColor" runat="server" CssClass="Apariencia" Width="300px">
                                                            <asp:ListItem Text="--Seleccioné--" Value="0"></asp:ListItem>
                                                            <asp:ListItem Text="Verde" Value="Verde"></asp:ListItem>
                                                            <asp:ListItem Text="Amarillo" Value="Amarillo"></asp:ListItem>
                                                            <asp:ListItem Text="Rojo" Value="Rojo"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        <asp:RequiredFieldValidator ID="rfvColor" runat="server" ControlToValidate="ddlColor" ErrorMessage="Debe ingresar el color del seguimiento." ForeColor="Red" InitialValue="0" ToolTip="Debe ingresar el valor color del seguimiento." ValidationGroup="VGseguimiento">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center" colspan="3">
                                                        <asp:ImageButton ID="IBinsertSeguimiento" runat="server" CausesValidation="true" CommandName="Insert" ImageUrl="~/Imagenes/Icons/guardar.png" OnClick="IBinsertSeguimiento_Click" Text="Insert" ToolTip="Insertar" ValidationGroup="VGseguimiento" Visible="false" />
                                                        <asp:ImageButton ID="IBUpdateSeguimiento" runat="server" CausesValidation="true" CommandName="Update" ImageUrl="~/Imagenes/Icons/guardar.png" OnClick="IBUpdateSeguimiento_Click" Text="Update" ToolTip="Actualizar" ValidationGroup="VGseguimiento" Visible="false" />
                                                        <asp:ImageButton ID="IBCancelSeguimiento" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png" OnClick="IBCancelSeguimiento_Click" ToolTip="Cancelar" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </ContentTemplate>
                                </ajax:TabPanel>
                            </asp:TabContainer>
                        </td>
                    </tr>
                </table>
            </div>
            <div>
                <table class="tabla" align="center" width="80%">
                    <tr>
                        <td align="center">
                            <asp:ImageButton ID="IBinsertGVC" runat="server" CausesValidation="true" CommandName="Insert" ImageUrl="~/Imagenes/Icons/guardar.png" OnClick="IBinsertGVC_Click" Text="Insert" ToolTip="Insertar" ValidationGroup="VGriesgosIndicadores" Visible="false" />
                            <asp:ImageButton ID="IBupdateGVC" runat="server" CausesValidation="true" CommandName="Update" ImageUrl="~/Imagenes/Icons/guardar.png" OnClick="IBupdateGVC_Click" Text="Update" ToolTip="Actualizar" ValidationGroup="VGriesgosIndicadores" Visible="false" />
                            <asp:ImageButton ID="btnImgCancelar" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png" OnClick="btnImgCancelar_Click" ToolTip="Cancelar" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
