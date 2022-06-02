<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GestionAcm.ascx.cs" Inherits="ListasSarlaft.UserControls.Proceso.Acm.GestionAcm" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Src="~/UserControls/Sitio/OkMessageBox.ascx" TagPrefix="uc" TagName="OkMessageBox" %>
<link href="../../../Styles/AdminSitio.css" rel="stylesheet" type="text/css" />
<style>
    .popup {
        border: Silver 1px solid;
        color: #060F40;
        background: #ffffff;
    }

    .spacing {
        height: 20px
    }

    .text-center {
        text-align: center;
        font-family: Verdana, Arial, Helvetica, sans-serif;
    }
    .auto-style1 {
        text-align: "left";
        background-color: #BBBBBB;
        height: 25px;
    }
    .auto-style2 {
        height: 25px;
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
<asp:UpdatePanel ID="updPanel" runat="server">
    <ContentTemplate>
        <uc:OkMessageBox ID="omb" runat="server" />
        <asp:Panel ID="popupSeguimiento" runat="server" CssClass="popup" Width="800px" Style="display: none">
            <table width="100%" class="tablaSinBordes">
                <tr>
                    <td align="center">
                        <asp:UpdateProgress ID="UpdateProgress4" runat="server" AssociatedUpdatePanelID="updPanel"
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
                <tr align="right" bgcolor="#5D7B9D">
                    <td>
                        <asp:ImageButton ID="ImageButton19" runat="server" ImageUrl="~/Imagenes/Icons/dialog-close3.png"
                            OnClientClick="$find('popupActividad2').hidePopup(); return false;" />
                    </td>
                </tr>
                <tr align="center">
                    <td>
                        <div style="text-align: center">
                            Seguimiento al plan de acción
                            <asp:Label ID="lblActividad" runat="server"></asp:Label>
                        </div>
                        <div style="height: 20px"></div>
                        <div id="divSegimiento" class="ColumnStyle" runat="server" visible="true">
                            <div style="border: 1px solid; width: 100%">
                                <table style="width: 90%; margin: 0 auto">
                                    <tr>
                                        <td colspan="3">
                                            <div class="spacing"></div>
                                            <asp:GridView ID="gvSeguimiento" runat="server" CellPadding="4"
                                                ForeColor="#333333" AllowPaging="False" AllowSorting="True" AutoGenerateColumns="False"
                                                ShowHeaderWhenEmpty="True"
                                                HeaderStyle-CssClass="gridViewHeader" BorderStyle="Solid" GridLines="Vertical"
                                                CssClass="Apariencia" Font-Bold="False" Width="100%">
                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                <Columns>
                                                    <asp:BoundField DataField="FechaRegistro" HeaderText="Fecha Seguimiento" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="25%" ItemStyle-Width="25%" FooterStyle-Width="25%" />
                                                    <asp:TemplateField HeaderText="Detalle Seguimiento" ControlStyle-Width="60%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDetalle" runat="server" Text='<% # Bind("Descripcion")%>' CssClass="ellipsis" ToolTip='<% # Bind("Descripcion")%>' HeaderStyle-Width="55%" ItemStyle-Width="55%" FooterStyle-Width="60%"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="NombreUsuario" HeaderText="Usuario" HeaderStyle-Width="15%" ItemStyle-Width="20%" FooterStyle-Width="20%" />
                                                </Columns>
                                                <EditRowStyle BackColor="#999999" />
                                                <FooterStyle BackColor="White" Font-Bold="True" ForeColor="White" />
                                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Justify" />
                                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                            </asp:GridView>
                                            <asp:HiddenField ID="hidForModel" runat="server" />
                                            <div class="spacing"></div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="RowsText">
                                            <asp:Label ID="lSeguimiento" runat="server" Text="Comentario:" CssClass="Apariencia" Width="300px"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtComentario" runat="server" Width="300px" CssClass="Apariencia" Rows="3" TextMode="MultiLine"></asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtComentario"
                                                ForeColor="Red" ValidationGroup="Seguimiento" Display="Static">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="RowsText">
                                            <asp:Label ID="lEstadoActividad" runat="server" Text="Estado:" CssClass="Apariencia" Width="300px"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlEstadoActividad" runat="server" Width="300px" CssClass="Apariencia" AutoPostBack="true" OnSelectedIndexChanged="DdlEstadoActividad_SelectedIndexChanged"></asp:DropDownList>
                                            <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlEstadoActividad"
                                                ForeColor="Red" ValidationGroup="Validacion" Display="Static" InitialValue="0">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr id="trObservacioneCierreAct" runat="server" visible="false">
                                        <td class="RowsText">
                                            <asp:Label ID="lObservacionesCierreAct" runat="server" Text="Observaciones cierre:" CssClass="Apariencia" Width="300px"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtObservacionesCierreAct" runat="server" Width="300px" CssClass="Apariencia" Rows="3" TextMode="MultiLine"></asp:TextBox>
                                            <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="CustomValidator" OnServerValidate="CustomValidator1_ServerValidate" Display="Static"></asp:CustomValidator>
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr id="trFechaCierreAct" runat="server" visible="false">
                                        <td class="RowsText">
                                             <asp:Label ID="lFechaCierre" runat="server" Text="Fecha cierre:" CssClass="Apariencia" Width="300px"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtFechaCierreAct" runat="server" Width="300px" CssClass="Apariencia" Enabled="false"></asp:TextBox>
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <div style="height: 20px"></div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </td>
                </tr>
                <tr align="center">
                    <td>
                        <div class="spacing"></div>
                        <asp:ImageButton ID="btnInsertarSeguimiento" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png" ToolTip="Guardar" OnClick="BtnInsertarSeguimiento_Click" ValidationGroup="Seguimiento" />
                        <asp:ImageButton ID="btnCancelarSeguimiento" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png" ToolTip="Cancelar" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <div class="TituloLabel" id="divTitulo" runat="server">
            <asp:Label ID="Ltitulo" runat="server" ForeColor="White" Text="Gestión de Acm" Font-Bold="False"
                Font-Names="Calibri" Font-Size="Large"></asp:Label>
        </div>
        <div style="height: 20px"></div>
        <div id="divForm" class="ColumnStyle" runat="server" visible="true">
            <div class="TableContains">
                <table align="center" width="100%">
                    <thead>
                        <tr>
                            <th style="width: 40%!important"></th>
                            <th style="width: 2%!important"></th>
                            <th style="width: 8%!important"></th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr id="trGrillaAcm" runat="server">
                            <td colspan="3">
                                <div>
                                    <table style="width: 100%">
                                        <tr>
                                            <td style="width: 100%">
                                                <asp:GridView ID="gvAcm" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                                    ForeColor="#333333" HeaderStyle-CssClass="gridViewHeader" BorderStyle="Solid" GridLines="Vertical"
                                                    CssClass="Apariencia" Font-Bold="False" Width="100%" OnRowCommand="GvAcm_RowCommand" OnSelectedIndexChanged="gvAcm_SelectedIndexChanged">
                                                    <Columns>
                                                        <asp:BoundField DataField="IdAcm" HeaderText="Id" HeaderStyle-CssClass="no-visible" ItemStyle-CssClass="no-visible" />
                                                        <asp:BoundField DataField="Codigo" HeaderText="Código" />
                                                        <asp:BoundField DataField="NombreProceso" HeaderText="Proceso" />
                                                        <asp:BoundField DataField="NombreOrigenNoConformidad" HeaderText="Origen no conformidad" />
                                                        <asp:BoundField DataField="FechaRegistro" HeaderText="Fecha Registro" />
                                                        <asp:BoundField DataField="NombreUsuarioRegistra" HeaderText="Usuario" />
                                                        <asp:BoundField DataField="NombreEstado" HeaderText="Estado" />
                                                        <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Icons/select.png" Text="Seleccionar" HeaderText="Seleccionar"
                                                            CommandName="Seleccionar" ItemStyle-HorizontalAlign="Center" />
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
                                                <asp:ImageButton ID="btnInsertar" runat="server" CausesValidation="False"
                                                    ImageUrl="~/Imagenes/Icons/Add.png" Text="Insert" ToolTip="Insertar" OnClick="BtnInsertar_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="spacing"></div>
                            </td>
                        </tr>
                        <tr id="trNuevoAcm" runat="server" visible="false">
                            <td colspan="3">
                                <table style="width: 100%">
                                    <tr>
                                        <td class="RowsText">
                                            <asp:Label ID="lResponsable" runat="server" Text="Responsable Ejecución:" CssClass="Apariencia" Width="300px"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtNResponsableEjecucion" runat="server" Width="300px" CssClass="Apariencia" ReadOnly="true" Rows="3" TextMode="MultiLine"></asp:TextBox>
                                            <asp:Label ID="lblIdDependencia1" runat="server" Visible="False" Font-Names="Calibri"
                                                Font-Size="Small"></asp:Label>
                                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtNResponsableEjecucion"
                                                ForeColor="Red" ValidationGroup="Validacion" Display="Dynamic">*</asp:RequiredFieldValidator>
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
                                                            <asp:TreeView ID="TreeView1" ExpandDepth="3" runat="server" Font-Names="Calibri"
                                                                Font-Size="Small" LineImagesFolder="~/TreeLineImages" ForeColor="Black" ShowLines="True" OnSelectedNodeChanged="TreeView1_SelectedNodeChanged"
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
                                        <td style="text-align: left">
                                            <asp:ImageButton ID="btnBorrarResponsables" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                                ToolTip="Borrar Responsables Ejecución" OnClick="BtnBorrarResponsables_Click" />
                                            <asp:ImageButton ID="imgDependencia1" runat="server" ImageUrl="~/Imagenes/Icons/Organization-Chart.png"
                                                OnClientClick="return false;" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="RowsText">
                                            <asp:Label ID="lGrupo" runat="server" Text="Grupo Trabajo:" CssClass="Apariencia" Width="300px"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtGrupo" runat="server" Width="300px" CssClass="Apariencia" ReadOnly="false" Rows="5" TextMode="MultiLine"></asp:TextBox>
                                            <asp:Label ID="lDependeciaGrupo" runat="server" Visible="False" Font-Names="Calibri"
                                                Font-Size="Small"></asp:Label>
                                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtGrupo"
                                                ForeColor="Red" ValidationGroup="Validacion" Display="Dynamic">*</asp:RequiredFieldValidator>
                                            <asp:PopupControlExtender ID="PopupControlExtenderTablaParam" runat="server" DynamicServicePath=""
                                                Enabled="True" ExtenderControlID="" TargetControlID="ImageButtonTablaParam" BehaviorID="popupTablaParam"
                                                PopupControlID="pnlTablaParam" OffsetY="-200" OffsetX="30">
                                            </asp:PopupControlExtender>
                                            <asp:Panel ID="pnlTablaParam" runat="server" CssClass="popup" Width="400px" Style="display: none">
                                                <table width="100%" border="1" cellspacing="0" cellpadding="2" bordercolor="White">
                                                    <tr align="right" bgcolor="#5D7B9D">
                                                        <td>
                                                            <asp:ImageButton ID="ImageButton18" runat="server" ImageUrl="~/Imagenes/Icons/dialog-close2.png"
                                                                OnClientClick="$find('popupTablaParam').hidePopup(); return false;" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:TreeView ID="TreeViewTablaParam" ExpandDepth="3" runat="server" Font-Names="Calibri"
                                                                Font-Size="Small" LineImagesFolder="~/TreeLineImages" ForeColor="Black" ShowLines="True"
                                                                AutoGenerateDataBindings="False" OnSelectedNodeChanged="TreeViewTablaParam_SelectedNodeChanged">
                                                                <SelectedNodeStyle BackColor="Silver" BorderColor="#66CCFF" BorderStyle="Inset" />
                                                            </asp:TreeView>
                                                        </td>
                                                    </tr>
                                                    <tr align="center">
                                                        <td>
                                                            <asp:Button ID="BtnOk2" runat="server" Text="Aceptar" CssClass="Apariencia" OnClientClick="$find('popupTablaParam').hidePopup(); return false;" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                        <td style="text-align: left" >
                                            <asp:ImageButton ID="btnBorrarGrupos" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                                ToolTip="Borrar Grupos Trabajo" OnClick="BtnBorrarGrupos_Click" Visible="false" />
                                            <asp:ImageButton ID="ImageButtonTablaParam" runat="server" ImageUrl="~/Imagenes/Icons/workflow (24).png"
                                                OnClientClick="return false;" Visible="false" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="RowsText">
                                            <asp:Label ID="lCadenaValor" runat="server" Text="Cadena de Valor:" CssClass="Apariencia" Width="300px"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlCadenaValor" runat="server" Width="300px" CssClass="Apariencia" AutoPostBack="True" OnSelectedIndexChanged="DdlCadenaValor_SelectedIndexChanged"></asp:DropDownList>
                                            <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlCadenaValor"
                                                ForeColor="Red" ValidationGroup="Validacion" Display="Dynamic" InitialValue="0">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="RowsText">
                                            <asp:Label ID="lMacroproceso" runat="server" Text="Macroproceso:" CssClass="Apariencia" Width="300px"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlMacroproceso" runat="server" Width="300px" CssClass="Apariencia" AutoPostBack="True" OnSelectedIndexChanged="DdlMacroproceso_SelectedIndexChanged"></asp:DropDownList>
                                            <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlMacroproceso"
                                                ForeColor="Red" ValidationGroup="Validacion" Display="Dynamic" InitialValue="0">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="RowsText">
                                            <asp:Label ID="lProceso" runat="server" Text="Proceso:" CssClass="Apariencia" Width="300px"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlProceso" runat="server" Width="300px" CssClass="Apariencia" AutoPostBack="True" OnSelectedIndexChanged="DdlProceso_SelectedIndexChanged"></asp:DropDownList>
                                            <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlProceso"
                                                ForeColor="Red" ValidationGroup="Validacion" Display="Dynamic" InitialValue="0">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="RowsText">
                                            <asp:Label ID="lSubproceso" runat="server" Text="Subproceso:" CssClass="Apariencia" Width="300px"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlSubproceso" runat="server" Width="300px" CssClass="Apariencia"></asp:DropDownList>
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="RowsText">
                                            <asp:Label ID="lDescripcion" runat="server" Text="Descripción de la no conformidad:" CssClass="Apariencia" Width="300px"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtDescNoConformidad" runat="server" Width="400px" CssClass="Apariencia" Rows="3" TextMode="MultiLine" MaxLength="1000"></asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDescNoConformidad"
                                                ForeColor="Red" ValidationGroup="Validacion" Display="Dynamic">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="RowsText">
                                            <asp:Label ID="lOrigenNoConformidad" runat="server" Text="Origen no Conformidad:" CssClass="Apariencia" Width="300px"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlOrigenNoConformidad" runat="server" Width="300px" CssClass="Apariencia"></asp:DropDownList>
                                            <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlOrigenNoConformidad"
                                                ForeColor="Red" ValidationGroup="Validacion" Display="Dynamic" InitialValue="0">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="RowsText">
                                            <asp:Label ID="lCausasRaiz" runat="server" Text="Causas Raíz:" CssClass="Apariencia" Width="300px"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCausasRaiz" runat="server" Width="400px" CssClass="Apariencia" Rows="3" TextMode="MultiLine" MaxLength="1000"></asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtCausasRaiz"
                                                ForeColor="Red" ValidationGroup="Validacion" Display="Dynamic">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="RowsText">
                                            <asp:Label ID="lCodigo" runat="server" Text="Código:" CssClass="Apariencia" Width="300px"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCodigo" runat="server" Width="300px" CssClass="Apariencia"></asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtCodigo"
                                                ForeColor="Red" ValidationGroup="Validacion" Display="Dynamic">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style1">
                                            <asp:Label ID="lAnalisisCausa" runat="server" Text="Análisis de Causa:" CssClass="Apariencia" Width="300px"></asp:Label>
                                        </td>
                                        <td class="auto-style2">
                                            <asp:FileUpload ID="fuAnalisisCausa" runat="server" CssClass="Apariencia" Width="300px"></asp:FileUpload>
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_fuAnalisisCausa" ControlToValidate="fuAnalisisCausa"
                                                ForeColor="Red" ValidationGroup="Validacion" Display="Dynamic" Enabled="true">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td class="auto-style2">
                                            <asp:ImageButton ID="btnDescargarArchivo" runat="server" ImageUrl="~/Imagenes/Icons/download.png" OnClick="BtnDescargarArchivo_Click" Visible="false" /></td>
                                    </tr>
                                    <tr>
                                        <td class="RowsText">
                                            <asp:Label ID="LFechaCraacion" runat="server" Text="Fecha Creación:" CssClass="Apariencia" Width="300px"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtFechaCreacion" runat="server" Width="300px" CssClass="Apariencia" ReadOnly="true"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Imagenes/Icons/download.png" OnClick="BtnDescargarArchivo_Click" Visible="false" /></td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <div class="spacing"></div>
                                            <div class="TituloLabel" id="div1" runat="server">
                                                <asp:Label ID="Label1" runat="server" ForeColor="White" Text="Plan de acción" Font-Bold="False"
                                                    Font-Names="Calibri" Font-Size="Large"></asp:Label>
                                            </div>
                                            <div>
                                                <table style="width: 100%">
                                                    <tr>
                                                        <td style="width: 100%">
                                                            <asp:GridView ID="gvActividades" runat="server" ShowFooter="True" AutoGenerateColumns="False" Width="100%" >
                                                                <Columns>
                                                                    <asp:BoundField DataField="RowNumber" HeaderText=""/>
                                                                    <asp:TemplateField HeaderText="No.&nbsp;&nbsp;&nbsp;Actividad">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="TextBox1" runat="server" Width="99%" CssClass="text-center" BorderStyle="None"></asp:TextBox>
                                                                            <asp:Label ID="lblIdActividadGv" runat="server" Visible="false"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Fecha Inicio">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="TextBox2" runat="server" Width="99%" CssClass="text-center" BorderStyle="None"></asp:TextBox>
                                                                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="true" TargetControlID="TextBox2"
                                                                                Format="yyyy-MM-dd">
                                                                            </asp:CalendarExtender>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Fecha Fin">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="TextBox3" runat="server" Width="99%" CssClass="text-center" BorderStyle="None"></asp:TextBox>
                                                                            <asp:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="true" TargetControlID="TextBox3"
                                                                                Format="yyyy-MM-dd">
                                                                            </asp:CalendarExtender>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Estado">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblEstadoGv" runat="server" Width="99%" Enabled="true" CssClass="text-center" Font-Size="Smaller" Font-Bold="false" ForeColor="GrayText"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Responsable">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="TextBox4" runat="server" Width="80%" CssClass="text-center" ReadOnly="true" BorderStyle="None"></asp:TextBox>
                                                                            <asp:Label ID="lblIdDependenciaGv" runat="server" Visible="False" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                                                            <asp:PopupControlExtender ID="popupDependenciaGv" runat="server"
                                                                                Enabled="True" ExtenderControlID="" TargetControlID="imgDependenciaGv"
                                                                                PopupControlID="pnlDependenciaGv" OffsetY="-200" OffsetX="30">
                                                                            </asp:PopupControlExtender>
                                                                            <asp:Panel ID="pnlDependenciaGv" runat="server" CssClass="popup" Width="35%" Style="display: none">
                                                                                <table width="100%" border="1" cellspacing="0" cellpadding="2" bordercolor="White">
                                                                                    <tr align="right" bgcolor="#5D7B9D">
                                                                                        <td>
                                                                                            <asp:ImageButton ID="btnCloseppGv" runat="server" ImageUrl="~/Imagenes/Icons/dialog-close2.png"
                                                                                                OnClientClick="$find('popupGv').hidePopup(); return false;" />
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <div>
                                                                                                <asp:TreeView ID="TreeViewGv" ExpandDepth="3" runat="server" Font-Names="Calibri"
                                                                                                    Font-Size="Small" LineImagesFolder="~/TreeLineImages" ForeColor="Black" ShowLines="True"
                                                                                                    AutoGenerateDataBindings="False" OnSelectedNodeChanged="TreeViewGv_SelectedNodeChanged">
                                                                                                    <SelectedNodeStyle BackColor="Silver" BorderColor="#66CCFF" BorderStyle="Inset" />
                                                                                                </asp:TreeView>
                                                                                            </div>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr align="center">
                                                                                        <td>
                                                                                            <asp:Button ID="BtnOkGv" runat="server" Text="Aceptar" CssClass="Apariencia" OnClientClick="$find('popupGv').hidePopup(); return false;" />
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </asp:Panel>
                                                                            <asp:ImageButton ID="btnBorrarGruposGv" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                                                                ToolTip="Borrar Grupos Trabajo" OnClick="BtnBorrarGruposGv_Click" Height="17px" />
                                                                            <asp:ImageButton ID="imgDependenciaGv" runat="server" ImageUrl="~/Imagenes/Icons/Organization-Chart.png"
                                                                                OnClientClick="return false;" Height="12px" />
                                                                        </ItemTemplate>
                                                                        <FooterStyle HorizontalAlign="Right" />
                                                                        <FooterTemplate>
                                                                            <asp:Button ID="btnAgregarPlan" runat="server" Text="Agregar Actividad" OnClick="BtnAgregarPlan_Click" />
                                                                        </FooterTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <asp:ImageButton ID="btnConfigurar" runat="server" ImageUrl="~/Imagenes/Icons/icono_configurar.gif" ToolTip="Configurar" OnClick="BtnConfigurar_Click" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <RowStyle HorizontalAlign="Center" />
                                                                <PagerStyle HorizontalAlign="Center" />
                                                            </asp:GridView>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <div class="spacing"></div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr id="trCierreAcm" runat="server" visible="false">
                            <td colspan="3">
                                <div class="spacing"></div>
                                <div class="TituloLabel" id="divTituloCierre" runat="server">
                                    <asp:Label ID="lCierreAcm" runat="server" ForeColor="White" Text="Cierre del Acm" Font-Bold="False"
                                        Font-Names="Calibri" Font-Size="Large"></asp:Label>
                                </div>
                                <div>
                                    <table style="width: 100%">
                                        <thead>
                                            <tr>
                                                <th style="width: 40%!important"></th>
                                                <th style="width: 2%!important"></th>
                                                <th style="width: 8%!important"></th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr>
                                                <td class="RowsText">
                                                    <asp:Label ID="lVerificacionEficacia" runat="server" Text="Verificación eficacia plan de acción:" CssClass="Apariencia" Width="300px"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtVerificacionEficacia" runat="server" Width="300px" CssClass="Apariencia" Rows="3" TextMode="MultiLine"></asp:TextBox>
                                                </td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td class="RowsText">
                                                    <asp:Label ID="lObservacionesAcm" runat="server" Text="Observaciones Procesos:" CssClass="Apariencia" Width="300px"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtObservacionesAcm" runat="server" Width="300px" CssClass="Apariencia" Rows="3" TextMode="MultiLine"></asp:TextBox>
                                                </td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td class="RowsText">
                                                    <asp:Label ID="lEstadoAcm" runat="server" Text="Estado Acm:" CssClass="Apariencia" Width="300px"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlEstadoAcm" runat="server" Width="300px" CssClass="Apariencia"></asp:DropDownList>
                                                </td>
                                                <td></td>
                                            </tr>
                                            <tr id="trFechaCierreAcm" runat="server" visible="false">
                                                <td class="RowsText">
                                                    <asp:Label ID="lFechaCierreAcm" runat="server" Text="Fecha Cierre:" CssClass="Apariencia" Width="300px"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtFechaCierreAcm" runat="server" Width="300px" CssClass="Apariencia" Enabled="false"></asp:TextBox>
                                                </td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td class="RowsText">
                                                    <asp:Label ID="lRevisarAporbar" runat="server" Text="Revisar/ Aprobar:" CssClass="Apariencia" Width="300px"></asp:Label>
                                                </td>
                                                <td>
                                                    <div>
                                                        <span>
                                                            <asp:CheckBox ID="chkRevisado" runat="server" Text="Revisado" Checked="false" />
                                                        </span>
                                                        <span>
                                                            <asp:CheckBox ID="chkAprobado" runat="server" Text="Aprobado" Checked="false" />
                                                        </span>
                                                    </div>
                                                </td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td class="RowsText">
                                                    <asp:Label ID="Label2" runat="server" Text="Revisar/ Aprobar:" CssClass="Apariencia" Width="300px"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:FileUpload ID="fuCierreAdjuntos" runat="server" CssClass="Apariencia" Width="300px"></asp:FileUpload>
                                                </td>
                                                <td>
                                                    <asp:ImageButton ID="imbAddAdjunto" runat="server" CausesValidation="False"
                                                    ImageUrl="~/Imagenes/Icons/Add.png" Text="Insert" ToolTip="Adjuntar" OnClick="imbAddAdjunto_Click" />
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                                <div>
                                    <asp:Label ID="lblArchivos" runat="server" Text="Archivos Adjuntos:"></asp:Label>
                                    <table style="width: 100%">
                                        <tr>
                                            <td>
                                                <asp:GridView ID="gvAdjuntosCierre" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                                    OnRowCommand="gvAdjuntosCierre_RowCommand"
                                                    ForeColor="#333333" HeaderStyle-CssClass="gridViewHeader" BorderStyle="Solid" GridLines="Vertical"
                                                    CssClass="Apariencia" Font-Bold="False" Width="100%" >
                                                    <Columns>
                                                        <asp:BoundField DataField="NombreArchivo" HeaderText="Nombre" />
                                                        <asp:BoundField DataField="extension" HeaderText="Extension" />
                                                        <asp:BoundField DataField="pathFile" HeaderText="Ruta" />
                                                        <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Icons/cancel.png" Text="Eliminar" HeaderText="Eliminar"
                                                            CommandName="Eliminar" ItemStyle-HorizontalAlign="Center" />
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
                                                <asp:GridView ID="gvDocsAdjuntados" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                                    DataKeyNames="intidAcmCierreAdjunto,btArchivo" OnRowCommand="gvDocsAdjuntados_RowCommand"
                                                    ForeColor="#333333" HeaderStyle-CssClass="gridViewHeader" BorderStyle="Solid" GridLines="Vertical"
                                                    CssClass="Apariencia" Font-Bold="False" Width="100%" >
                                                    <Columns>
                                                        <asp:BoundField DataField="strnombreArchivo" HeaderText="Nombre" />
                                                        <asp:BoundField DataField="strextension" HeaderText="Extension" />
                                                        <asp:BoundField DataField="strpathFile" HeaderText="Ruta" />
                                                        <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Icons/download.png" Text="Descargar" HeaderText="Descargar"
                                                            CommandName="Descargar" ItemStyle-HorizontalAlign="Center" />
                                                        <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Icons/cancel.png" Text="Eliminar" HeaderText="Eliminar"
                                                            CommandName="Eliminar" ItemStyle-HorizontalAlign="Center" />
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
                                </div>
                                <div>
                                    <table style="width: 100%">
                                        <tr>
                                            <td style="border: 1px solid">Registrado por:
                                                <div>
                                                    <span>
                                                        <asp:Label ID="lblRegistrado" runat="server" Text="adminWin" Width="98%"></asp:Label></span>
                                                </div>
                                            </td>
                                            <td style="border: 1px solid">Revisado por:
                                                <div>
                                                    <span>
                                                        <asp:Label ID="lblRevisadoPor" runat="server" Text="adminWin" Width="98%"></asp:Label></span>
                                                </div>
                                            </td>
                                            <td style="border: 1px solid">Cerrado por:
                                                <div>
                                                    <span>
                                                        <asp:Label ID="lblCerradoPor" runat="server" Text="adminWin" Width="98%"></asp:Label></span>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                        <tr id="trBotones" runat="server" visible="false">
                            <td colspan="3">
                                <asp:ImageButton ID="btnGuardar" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png" ToolTip="Guardar" OnClick="BtnGuardar_Click" ValidationGroup="Validacion" />
                                <asp:ImageButton ID="btnCancelar" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png" ToolTip="Cancelar" OnClick="BtnCancelar_Click" />
                                <asp:ImageButton ID="btnDescargarAcm" runat="server" ImageUrl="~/Imagenes/Icons/pdfImg.jpg" ToolTip="Descargar" OnClick="BtnDescargarAcm_Click" Visible="false" />
                            </td>
                        </tr>
                        <tr id="trGridviewsReporte">
                            <asp:GridView ID="gvSeguimientoAcm" runat="server" AutoGenerateColumns="false" Visible="false">
                                <Columns>
                                    <asp:BoundField DataField="IdActividad" HeaderText="No" />
                                    <asp:BoundField DataField="NombreActividad" HeaderText="Actividad" />
                                    <asp:BoundField DataField="FechaRegistro" HeaderText="Fecha Registro" />
                                    <asp:BoundField DataField="Descripcion" HeaderText="Detalle Seguimiento" />
                                    <asp:BoundField DataField="NombreUsuario" HeaderText="Usuario" />
                                </Columns>
                            </asp:GridView>
                            <asp:GridView ID="gvObservacionesCierreAct" runat="server" AutoGenerateColumns="false" Visible="false">
                                <Columns>
                                    <asp:BoundField DataField="idActividad" HeaderText="No" />
                                    <asp:BoundField DataField="ActividadCierre" HeaderText="Actividad" />
                                    <asp:BoundField DataField="Observaciones" HeaderText="Observaciones" />
                                    <asp:BoundField DataField="FechaCierre" HeaderText="Fecha Cierre" />
                                </Columns>
                            </asp:GridView>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
        <asp:ModalPopupExtender ID="modalPopup" runat="server" PopupControlID="popupSeguimiento" TargetControlID="hidForModel" BackgroundCssClass="modalBackground" DropShadow="True">
        </asp:ModalPopupExtender>
    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="btnGuardar" />
        <asp:PostBackTrigger ControlID="btnDescargarArchivo" />
        <asp:PostBackTrigger ControlID="btnDescargarAcm" />
        <asp:PostBackTrigger ControlID="imbAddAdjunto" />
        <asp:PostBackTrigger ControlID="gvDocsAdjuntados" />
    </Triggers>
</asp:UpdatePanel>
