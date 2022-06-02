<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PlanMejoramiento.ascx.cs" Inherits="ListasSarlaft.UserControls.Proceso.Procesos.PlanMejoramiento" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Src="~/UserControls/Sitio/OkMessageBox.ascx" TagPrefix="uc" TagName="OkMessageBox" %>
<link href="../../../Styles/AdminSitio.css" rel="stylesheet" type="text/css" />
<style type="text/css">
    
    
div.ajax__calendar_days table tr td{padding-right: 0px;}
div.ajax__calendar_body{width: 225px;}
div.ajax__calendar_container{width: 225px;}

    </style>
<asp:UpdatePanel ID="PMbody" runat="server">
    <ContentTemplate>
        <uc:OkMessageBox ID="omb" runat="server" />
        <div class="TituloLabel" id="HeadPM" runat="server">
                    <asp:Label ID="Ltitulo" runat="server" ForeColor="White" Text="Plan de Mejoramiento" Font-Bold="False"
                        Font-Names="Calibri" Font-Size="Large"></asp:Label>
            </div>
        <div id="BodyGridPM" class="ColumnStyle" runat="server">
            <Table class="tabla" align="center" width="100%">
                        <tr align="center">
                            <td>
                                <asp:GridView ID="GVplanmejoramiento" runat="server" CellPadding="4"
                                    ForeColor="#333333" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
                                    ShowHeaderWhenEmpty="True" DataKeyNames="strUsuario,dtFechaRegistro,intIdMacroProceso, intIdTipoProceso"
                                    HeaderStyle-CssClass="gridViewHeader" BorderStyle="Solid" GridLines="Vertical"
                                    CssClass="Apariencia" Font-Bold="False" OnRowCommand="GVplanmejoramiento_RowCommand" OnPageIndexChanging="GVplanmejoramiento_PageIndexChanging">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:BoundField DataField="intId" HeaderText="Código" SortExpression="intId" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="no-visible" HeaderStyle-CssClass="no-visible" />
                                        <asp:BoundField DataField="intIdMacroProceso" HeaderText="IdMacroProceso" ReadOnly="True" Visible="false" SortExpression="intIdMacroProceso" ItemStyle-HorizontalAlign="Center" />
                                        <asp:TemplateField HeaderText="Nombre Proceso" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 150px">
                                                    <asp:Label ID="NombreProceso" runat="server" Text='<% # Bind("strProceso")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" HorizontalAlign="center" />
                                            <ItemStyle Wrap="false"  />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Descripcion Actividad" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 150px">
                                                    <asp:Label ID="DescripcionActividad" runat="server" Text='<% # Bind("strDescripcionActividad")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false"  HorizontalAlign="center" />
                                            <ItemStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="dtPeriodoEvaluarInicial" HeaderText="Periodo Inicial" ReadOnly="True" SortExpression="dtPeriodoEvaluarInicial" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="dtPeriodoEvaluarFinal" HeaderText="Periodo Final" ReadOnly="True" SortExpression="dtPeriodoEvaluarFinal" ItemStyle-HorizontalAlign="Center" />
                                        <asp:TemplateField HeaderText="Plan de Mejoramiento" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 150px">
                                                    <asp:Label ID="PlanMejoramiento" runat="server" Text='<% # Bind("strPlanMejoramiento")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false"  HorizontalAlign="center" />
                                            <ItemStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Recursos" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 150px">
                                                    <asp:Label ID="Recursos" runat="server" Text='<% # Bind("strRecursos")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false"  HorizontalAlign="center" />
                                            <ItemStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="dtFechaRegistro" HeaderText="fechaRegistro" ReadOnly="True" Visible="false" SortExpression="dtFechaRegistro" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="intIdUsuario" HeaderText="IdUsuario" ReadOnly="True" Visible="false" SortExpression="intIdUsuario" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="strUsuario" HeaderText="Usuario" ReadOnly="True" Visible="false" SortExpression="strUsuario" ItemStyle-HorizontalAlign="Center" />
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
            </Table>
        </div>
        <div id="BodyFormPM" class="ColumnStyle" runat="server" visible="false">
            <div id="form" class="TableContains">
                <Table class="tabla" align="center" width="80%">
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
                                                ValidationGroup="CrlSalida" ForeColor="Red" InitialValue="0">*</asp:RequiredFieldValidator>
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
                                        <td><asp:RequiredFieldValidator ID="rfvMacroProceso" runat="server" ControlToValidate="ddlMacroproceso"
                                                ErrorMessage="Debe ingresar el Macroproceso." ToolTip="Debe ingresar el Macroproceso."
                                                ValidationGroup="CrlSalida" ForeColor="Red" InitialValue="0">*</asp:RequiredFieldValidator></td>
                                    </tr>
                                    <tr id="Proceso">
                                        <td class="RowsText">
                                            <asp:Label ID="Label7" runat="server" Text="Proceso:" CssClass="Apariencia"></asp:Label></td>
                                        <td>
                                            <asp:DropDownList ID="ddlProceso" runat="server" Width="300px" OnSelectedIndexChanged="ddlProceso_SelectedIndexChanged"
                                                CssClass="Apariencia" AutoPostBack="True" DataTextField="Nombre" DataValueField="IdProceso">
                                            </asp:DropDownList></td>
                                        <td><asp:RequiredFieldValidator ID="rfvProceso" runat="server" ControlToValidate="ddlProceso"
                                                ErrorMessage="Debe ingresar el Proceso." ToolTip="Debe ingresar el Proceso." Enabled="False"
                                                ValidationGroup="CrlSalida2" ForeColor="Red" InitialValue="0">*</asp:RequiredFieldValidator></td>
                                    </tr>
                                    <tr id="Subproceso">
                                        <td class="RowsText">
                                            <asp:Label ID="Label8" runat="server" Text="Subproceso:" CssClass="Apariencia"></asp:Label></td>
                                        <td>
                                            <asp:DropDownList ID="ddlSubproceso" runat="server" Width="300px" OnSelectedIndexChanged="ddlSubproceso_SelectedIndexChanged"
                                                CssClass="Apariencia" AutoPostBack="True" DataTextField="Nombre" DataValueField="IdProceso">
                                            </asp:DropDownList></td>
                                        <td><asp:RequiredFieldValidator ID="rfvSubproceso" runat="server" ControlToValidate="ddlSubproceso"
                                                ErrorMessage="Debe ingresar el Subproceso." ToolTip="Debe ingresar el Subproceso." Enabled="False"
                                                ValidationGroup="CrlSalida2" ForeColor="Red" InitialValue="0">*</asp:RequiredFieldValidator></td>
                                    </tr>
                        <tr>
                            <td class="RowsText">
                                <asp:Label ID="LdescripcionActividad" runat="server" Text="Descripción Actividad:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TXdescripcionActividad" runat="server" Font-Names="Calibri" Font-Size="Small" ValidationGroup="CrlProducto" Width="300px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RVDescripcion" runat="server" ControlToValidate="TXdescripcionActividad"
                                    ErrorMessage="Debe ingresar la descripcion de la actividad." ToolTip="Debe ingresar la descripcion de la actividad."
                                    ValidationGroup="CrlSalida" ForeColor="Red">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                        <td class="RowsText">
                            <asp:Label ID="LfechaIni" runat="server" Text="Fecha Periodo Inicial:" CssClass="Apariencia"></asp:Label></td>
                        </td>
                        <td>
                            <asp:TextBox ID="TXfechaIni" runat="server" CssClass="Apariencia" Width="300px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:CalendarExtender ID="CEfechaIni" runat="server" Enabled="true" TargetControlID="TXfechaIni"
                            Format="yyyy-MM-dd">
                        </asp:CalendarExtender>
                        <asp:RequiredFieldValidator ID="RFVfechaIni" runat="server" ControlToValidate="TXfechaIni"
                                    ErrorMessage="Debe ingresar la fecha Inicial." ToolTip="Debe ingresar la fecha Inicial."
                                    ValidationGroup="CrlSalida" ForeColor="Red">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="RowsText">
                            <asp:Label ID="LfechaFin" runat="server" Text="Fecha Periodo Final:" CssClass="Apariencia"></asp:Label></td>
                        </td>
                        <td>
                            <asp:TextBox ID="TXfechaFin" runat="server" CssClass="Apariencia" Width="300px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:CalendarExtender ID="CEfechaFin" runat="server" Enabled="true" TargetControlID="TXfechaFin"
                            Format="yyyy-MM-dd">
                        </asp:CalendarExtender>
                        <asp:RequiredFieldValidator ID="RFVfechaFin" runat="server" ControlToValidate="TXfechaFin"
                                    ErrorMessage="Debe ingresar la fecha final." ToolTip="Debe ingresar la fecha final."
                                    ValidationGroup="CrlSalida" ForeColor="Red">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="RowsText">
                            <asp:Label ID="Lplan" runat="server" Text="Plan de Mejoramiento:" CssClass="Apariencia" Width="300px"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TXplan" runat="server" Font-Names="Calibri" Font-Size="Small" TextMode="MultiLine" ValidationGroup="CrlProducto" Width="300px" Height="89px"></asp:TextBox>
                        
                        </td>
                        <td>
                           
                        <asp:RequiredFieldValidator ID="RVplan" runat="server" ControlToValidate="TXplan"
                                    ErrorMessage="Debe ingresar el plan de mejoramiento." ToolTip="Debe ingresar el plan de mejoramiento."
                                    ValidationGroup="CrlSalida" ForeColor="Red">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="RowsText">
                            <asp:Label ID="Lrecursos" runat="server" Text="Recursos que se requieren:" CssClass="Apariencia" Width="300px"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TXrecursos" runat="server" Font-Names="Calibri" Font-Size="Small" ValidationGroup="CrlProducto" Width="300px"></asp:TextBox>
                        
                        </td>
                        <td>
                           
                        <asp:RequiredFieldValidator ID="RFVrecursos" runat="server" ControlToValidate="TXrecursos"
                                    ErrorMessage="Debe ingresar los recursos." ToolTip="Debe ingresar los recursos."
                                    ValidationGroup="CrlSalida" ForeColor="Red">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="RowsText">
                            <asp:Label ID="Larea" runat="server" Text="Área:" CssClass="Apariencia" Width="300px"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TXarea" runat="server" Font-Names="Calibri" Font-Size="Small" ValidationGroup="CrlProducto" Width="300px"></asp:TextBox>
                        
                        </td>
                        <td>
                           
                        <asp:RequiredFieldValidator ID="RFVarea" runat="server" ControlToValidate="TXarea"
                                    ErrorMessage="Debe ingresar el area." ToolTip="Debe ingresar el area."
                                    ValidationGroup="CrlSalida" ForeColor="Red">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="RowsText">
                            <asp:Label ID="Lactividad" runat="server" Text="Actividad:" CssClass="Apariencia" Width="300px"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TXactividad" runat="server" Font-Names="Calibri" Font-Size="Small" ValidationGroup="CrlProducto" Width="300px"></asp:TextBox>
                        
                        </td>
                        <td>
                           
                        <asp:RequiredFieldValidator ID="RFVactividadd" runat="server" ControlToValidate="TXactividad"
                                    ErrorMessage="Debe ingresar la actividad." ToolTip="Debe ingresar la actividad."
                                    ValidationGroup="CrlSalida" ForeColor="Red">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="RowsText">
                            <asp:Label ID="LfechaProgramada" runat="server" Text="Fecha Programación:" CssClass="Apariencia"></asp:Label></td>
                        </td>
                        <td>
                            <asp:TextBox ID="TXfechaProgramada" runat="server" CssClass="Apariencia" Width="300px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:CalendarExtender ID="CEfechaProgramada" runat="server" Enabled="true" TargetControlID="TXfechaProgramada"
                            Format="yyyy-MM-dd" >
                        </asp:CalendarExtender>
                        <asp:RequiredFieldValidator ID="RFVfechaProgramada" runat="server" ControlToValidate="TXfechaProgramada"
                                    ErrorMessage="Debe ingresar la fecha Programada." ToolTip="Debe ingresar la fecha Programada."
                                    ValidationGroup="CrlSalida" ForeColor="Red">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="RowsText">
                            <asp:Label ID="LfechaRealizada" runat="server" Text="Fecha Realización:" CssClass="Apariencia"></asp:Label></td>
                        </td>
                        <td>
                            <asp:TextBox ID="TXfechaRealizada" runat="server" CssClass="Apariencia" Width="300px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:CalendarExtender ID="CEfechaRealizada" runat="server" Enabled="true" TargetControlID="TXfechaRealizada"
                            Format="yyyy-MM-dd" >
                        </asp:CalendarExtender>
                        
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
                            </td>
                        <td>
                            <asp:RequiredFieldValidator ID="RFVresponsable" runat="server" ControlToValidate="tbxResponsable"
                                    ErrorMessage="Debe ingresar el nombre del Responsable." ToolTip="Debe ingresar el nombre del Responsable."
                                    ValidationGroup="CrlSalida" ForeColor="Red">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="RowsText">
                            <asp:Label ID="Lseguimiento" runat="server" Text="Seguimiento:" CssClass="Apariencia" Width="300px"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TXseguimiento" runat="server" Font-Names="Calibri" Font-Size="Small" ValidationGroup="CrlProducto" Width="300px"></asp:TextBox>
                        </td>
                        <td>
                        </td>
                    </tr>
                        
                    <tr>
                            <td class="RowsText">
                                <asp:Label ID="Lusuario" runat="server" Text="Usuario Creación:" CssClass="Apariencia" Width="300px"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="tbxUsuarioCreacion" runat="server" Width="300px" CssClass="Apariencia"
                                    Enabled="False"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="RowsText">
                                <asp:Label ID="LfechaCreacion" runat="server" Text="Fecha de Creación:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtFecha" runat="server" Width="300px" CssClass="Apariencia"
                                    Enabled="False"></asp:TextBox>
                            </td>
                        </tr>
                    <tr>
                        <td colspan="3">
                            <asp:ImageButton ID="IBinsertGVC" runat="server" CausesValidation="true" CommandName="Insert"
                                    ImageUrl="~/Imagenes/Icons/guardar.png" Text="Insert" ValidationGroup="CrlSalida" ToolTip="Insertar" Visible="false" OnClick="IBinsertGVC_Click" style="width: 20px"/>
            <asp:ImageButton ID="IBupdateGVC" runat="server" CausesValidation="true" CommandName="Update"
                                    ImageUrl="~/Imagenes/Icons/guardar.png" Text="Update" ValidationGroup="CrlSalida" ToolTip="Actualizar" Visible="false" OnClick="IBupdateGVC_Click"/>
            <asp:ImageButton ID="btnImgCancelar" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                    OnClick="btnImgCancelar_Click" ToolTip="Cancelar" />
                        </td>
                    </tr>
                    </table>
            </div>
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
        </ContentTemplate>
    </asp:UpdatePanel>