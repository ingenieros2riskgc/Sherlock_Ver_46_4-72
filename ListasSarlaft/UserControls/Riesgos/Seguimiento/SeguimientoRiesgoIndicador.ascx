<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SeguimientoRiesgoIndicador.ascx.cs" Inherits="ListasSarlaft.UserControls.Riesgos.Seguimiento.SeguimientoRiesgoIndicador" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Src="~/UserControls/Sitio/OkMessageBox.ascx" TagPrefix="uc" TagName="OkMessageBox" %>

<link href="../../../Styles/AdminSitio.css" rel="stylesheet" type="text/css" />
<style type="text/css">
    
    
div.ajax__calendar_days table tr td{padding-right: 0px;}
div.ajax__calendar_body{width: 225px;}
div.ajax__calendar_container{width: 225px;}

    </style>
<asp:UpdatePanel ID="SRIbody" runat="server">
    <ContentTemplate>
        <uc:OkMessageBox ID="omb" runat="server" />
        <div class="TituloLabel" id="HeadSRI" runat="server">
                    <asp:Label ID="Ltitulo" runat="server" ForeColor="White" Text="Seguimiento de Indicadores de Riesgo" Font-Bold="False"
                        Font-Names="Calibri" Font-Size="Large"></asp:Label>
            </div>
        <div id="BodyFormSRI" class="ColumnStyle" runat="server">
            <div id="form" class="TableContains">
                </div>
            <div id="DbuttonsSearch" class="TableContains">
                <table class="tabla" align="center" width="80%">
                    <tr>
                        <td class="RowsText">
                            <asp:Label ID="lblRiesgo" runat="server" Text="Riesgo Asociado:" CssClass="Apariencia"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txbRiesgo" runat="server" CssClass="Apariencia" Width="300px"></asp:TextBox>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="RowsText">
                            <asp:Label ID="lblFactor" runat="server" Text="Factor de Riesgo:" CssClass="Apariencia"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlFactorRiesgo" runat="server" Width="300px" Font-Names="Calibri"
                                    Font-Size="Small" AutoPostBack="True">
                                    <asp:ListItem Value="---">---</asp:ListItem>
                                </asp:DropDownList>
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
                        <td></td>
                    </tr>
                    <tr id="Macroproceso">
                        <td class="RowsText">
                            <asp:Label ID="Label22" runat="server" Text="Macroproceso:" CssClass="Apariencia"></asp:Label></td>
                        <td>
                            <asp:DropDownList ID="ddlMacroproceso" runat="server" Width="300px"
                                CssClass="Apariencia" AutoPostBack="True" DataTextField="Nombre" DataValueField="IdMacroproceso"
                                OnSelectedIndexChanged="ddlMacroproceso_SelectedIndexChanged">
                            </asp:DropDownList></td>
                        <td></td>
                    </tr>
                    <tr id="Proceso">
                        <td class="RowsText">
                            <asp:Label ID="Label7" runat="server" Text="Proceso:" CssClass="Apariencia"></asp:Label></td>
                        <td>
                            <asp:DropDownList ID="ddlProceso" runat="server" Width="300px" OnSelectedIndexChanged="ddlProceso_SelectedIndexChanged"
                                CssClass="Apariencia" AutoPostBack="True" DataTextField="Nombre" DataValueField="IdProceso">
                            </asp:DropDownList></td>
                        <td></td>
                    </tr>
                    <tr id="Subproceso">
                        <td class="RowsText">
                            <asp:Label ID="Label8" runat="server" Text="Subproceso:" CssClass="Apariencia"></asp:Label></td>
                        <td>
                            <asp:DropDownList ID="ddlSubproceso" runat="server" Width="300px" OnSelectedIndexChanged="ddlSubproceso_SelectedIndexChanged"
                                CssClass="Apariencia" AutoPostBack="True" DataTextField="Nombre" DataValueField="IdProceso">
                            </asp:DropDownList></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="RowsText">
                            <asp:Label ID="Lresponsable" runat="server" Text="Responsable:" CssClass="Apariencia" Width="300px"></asp:Label>
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
                    </tr>
                                                <tr>
                                                    <td colspan="3">
                                                        <asp:ImageButton ID="IBconsultar" runat="server" ImageUrl="~/Imagenes/Icons/lupa.png"
                                                            ToolTip="Consultar" OnClick="IBconsultar_Click" ></asp:ImageButton>
                                                    
                                                        <asp:ImageButton ID="IBcancel" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                                            ToolTip="Cancelar" OnClick="IBcancel_Click" ></asp:ImageButton>
                                                    </td>
                                                </tr>
                                            <tr>
                                                <td colspan="3">
                                                    &nbsp;<asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Imagenes/Icons/excel.png" onclick="GenerarReporte_Click" ToolTip="Exportar a Excel" />
                                                </td>
                    </tr>
                                            </table>
            </div>
            
            </div>
        <div id="dvGirdData" class="ColumnStyle" runat="server">
            <table class="tabla" align="center" width="100%">
                <tr align="center">
                    <td>
                <asp:GridView ID="GVseguimientoRiesgoInsicador" runat="server" CellPadding="4"
                            ForeColor="#333333" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
                            ShowHeaderWhenEmpty="True" DataKeyNames="strUsuario,dtFechaCreacion,intIdMeta,intIdEsquemaSeguimiento,intIdFormula, intIProcesoIndicador, intIdFrecuenciaMedicion,intIdProceso,booActivo"
                            HeaderStyle-CssClass="gridViewHeader" BorderStyle="Solid" GridLines="Vertical"
                            CssClass="Apariencia" Font-Bold="False" OnPageIndexChanging="GVseguimientoRiesgoInsicador_PageIndexChanging" OnPreRender="GVseguimientoRiesgoInsicador_PreRender" PageSize="25"  >
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
                                <asp:TemplateField HeaderText="Descripción Frecuencia" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 150px">
                                            <asp:Label ID="strDescripcionFrecuencia" runat="server" Text='<% # Bind("strDescripcionFrecuencia")%>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                    <HeaderStyle Wrap="false" HorizontalAlign="center" />
                                    <ItemStyle Wrap="false" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Meta" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 75px">
                                            <asp:Label ID="dblMeta" runat="server" Text='<% # Bind("dblMeta")%>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                    <HeaderStyle Wrap="false" HorizontalAlign="center" />
                                    <ItemStyle Wrap="false" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Año" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 75px">
                                            <asp:Label ID="strAño" runat="server" Text='<% # Bind("strAño")%>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                    <HeaderStyle Wrap="false" HorizontalAlign="center" />
                                    <ItemStyle Wrap="false" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Mes" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 75px">
                                            <asp:Label ID="strMes" runat="server" Text='<% # Bind("strMes")%>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                    <HeaderStyle Wrap="false" HorizontalAlign="center" />
                                    <ItemStyle Wrap="false" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Resultado" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 150px">
                                            <asp:Label ID="dblResultado" runat="server" Text='<%# Eval("dblResultado","{0:N2}") %>'></asp:Label><%--Text='<% # Bind("dblResultado")%>' --%>
                                        </div>
                                    </ItemTemplate>
                                    <HeaderStyle Wrap="false" HorizontalAlign="center" />
                                    <ItemStyle Wrap="false" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Descripción Seguimiento" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 150px">
                                            <asp:Label ID="strDescripcionSeguimiento" runat="server" Text='<% # Bind("strDescripcionSeguimiento")%>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                    <HeaderStyle Wrap="false" HorizontalAlign="center" />
                                    <ItemStyle Wrap="false" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Rango Cumplimiento" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 150px">
                                            <asp:Label ID="strColor" runat="server" Text='<% # Bind("strColor")%>'></asp:Label>
                                            <asp:ImageButton runat="server" ID="ImbRango" ImageUrl="~/Imagenes/Aplicacion/Arriba.png" Width="20px" Height="20px"  CommandName="Activar" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"/>
                                        </div>
                                    </ItemTemplate>
                                    <HeaderStyle Wrap="false" HorizontalAlign="center" />
                                    <ItemStyle Wrap="false" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="dtFechaCreacion" HeaderText="fechaRegistro" ReadOnly="True" Visible="false" SortExpression="dtFechaCreacion" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="strUsuario" HeaderText="Usuario" ReadOnly="True" Visible="false" SortExpression="strUsuario" ItemStyle-HorizontalAlign="Center" />
                                
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
        </ContentTemplate>
        <Triggers>
        <asp:PostBackTrigger ControlID="ImageButton2" />

    </Triggers>
    </asp:UpdatePanel>