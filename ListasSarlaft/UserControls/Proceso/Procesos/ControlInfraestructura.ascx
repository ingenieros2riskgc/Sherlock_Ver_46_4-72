<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ControlInfraestructura.ascx.cs" Inherits="ListasSarlaft.UserControls.Proceso.Procesos.ControlInfraestructura" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Src="~/UserControls/Sitio/OkMessageBox.ascx" TagPrefix="uc" TagName="OkMessageBox" %>
<link href="../../../Styles/AdminSitio.css" rel="stylesheet" type="text/css" />
<style type="text/css">
    
    
div.ajax__calendar_days table tr td{padding-right: 0px;}
div.ajax__calendar_body{width: 225px;}
div.ajax__calendar_container{width: 225px;}

    .Apariencia {}

    </style>
<asp:UpdatePanel ID="CIbody" runat="server">
    <ContentTemplate>
        <uc:OkMessageBox ID="omb" runat="server" />
        <div class="TituloLabel" id="HeadCI" runat="server">
                    <asp:Label ID="Ltitulo" runat="server" ForeColor="White" Text="Control de Infraestructura" Font-Bold="False"
                        Font-Names="Calibri" Font-Size="Large"></asp:Label>
            </div>
        <div id="BodyGridCI" class="ColumnStyle" runat="server">
            <Table class="tabla" align="center" width="100%">
                        <tr align="center">
                            <td>
                                <asp:GridView ID="GVcontrolInfraestructura" runat="server" CellPadding="4"
                                    ForeColor="#333333" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
                                    ShowHeaderWhenEmpty="True" DataKeyNames="dtFechaRegistro,struserName,intIdMacroProceso,intResponsable,intAllProcess"
                                    HeaderStyle-CssClass="gridViewHeader" BorderStyle="Solid" GridLines="Vertical"
                                    CssClass="Apariencia" Font-Bold="False" OnRowCommand="GVcontrolInfraestructura_RowCommand" OnPageIndexChanging="GVcontrolInfraestructura_PageIndexChanging">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:BoundField DataField="intId" HeaderText="Código" SortExpression="intId" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="no-visible" HeaderStyle-CssClass="no-visible" />
                                        <asp:BoundField DataField="intIdMacroProceso" HeaderText="CódigoProceso" SortExpression="intIdMacroProceso" Visible="false" ItemStyle-HorizontalAlign="Center" />
                                        <asp:TemplateField HeaderText="Nombre Proceso" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 150px">
                                                    <asp:Label ID="NombreProceso" runat="server" Text='<% # Bind("strNombreproceso")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" Width="150" HorizontalAlign="center" />
                                            <ItemStyle Wrap="false" Width="150" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="intResponsable" HeaderText="Cargo Responsable" SortExpression="intResponsable" HtmlEncodeFormatString="True" HtmlEncode="False" Visible="False" />
                                        <asp:BoundField DataField="strCargoResponsable" HeaderText="Cargo Responsable" SortExpression="strCargoResponsable" HtmlEncodeFormatString="True"/>
                                         <asp:BoundField DataField="strActividad" HeaderText="Actividad" SortExpression="strActividad" HtmlEncodeFormatString="True"/>
                                        <asp:BoundField DataField="dtFechaProgramada" HeaderText="Fecha Programada" ReadOnly="True" SortExpression="dtFechaProgramada" ItemStyle-HorizontalAlign="Center" />
                                        <asp:TemplateField HeaderText="Fecha Cumplimiento" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 150px">
                                                    <asp:Label ID="dtFechaCumplimiento" runat="server" Text='<% # Bind("dtFechaCumplimiento")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" Width="150" HorizontalAlign="center" />
                                            <ItemStyle Wrap="false" Width="150" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Observaciones" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 150px">
                                                    <asp:Label ID="Observaciones" runat="server" Text='<% # Bind("strObservaciones")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" Width="150" HorizontalAlign="center" />
                                            <ItemStyle Wrap="false" Width="150" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="intIdUsuario" HeaderText="Usuario" ReadOnly="True" SortExpression="intIdUsuario" ItemStyle-HorizontalAlign="Center" Visible ="false"/>
                                        <asp:BoundField DataField="struserName" HeaderText="Usuario" ReadOnly="True" SortExpression="struserName" ItemStyle-HorizontalAlign="Center" Visible ="false"/>
                                        <asp:BoundField DataField="intAllProcess" HeaderText="intAllProcess" ReadOnly="True" SortExpression="intAllProcess" ItemStyle-HorizontalAlign="Center" Visible ="false"/>
                                        <asp:BoundField DataField="dtFechaRegistro" HeaderText="Fecha Registro" ReadOnly="True" SortExpression="dtFechaRegistro" ItemStyle-HorizontalAlign="Center" Visible="false" />
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
        <div id="BodyFormCI" class="ColumnStyle" runat="server" visible="false">
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
                            <td><asp:RequiredFieldValidator ID="rfvCadenaValor" runat="server" ControlToValidate="ddlCadenaValor"
                                                ErrorMessage="Debe ingresar la cadena de valor." ToolTip="Debe ingresar la cadena de valor."
                                                ValidationGroup="ControlInfraestructura" ForeColor="Red" InitialValue="0">*</asp:RequiredFieldValidator>
                                            <asp:TextBox ID="tbxProcIndica" runat="server" Width="90px" MaxLength="20" CssClass="Apariencia" Visible="false"></asp:TextBox></td>
                                    </tr>
                                    <tr id="Macroproceso">
                                        <td class="RowsText">
                                            <asp:Label ID="Label22" runat="server" Text="Macroproceso:" CssClass="Apariencia"></asp:Label></td>
                                        <td>
                                            <asp:DropDownList ID="ddlMacroproceso" runat="server" Width="300px"
                                                CssClass="Apariencia" AutoPostBack="True" DataTextField="Nombre" DataValueField="IdMacroproceso"
                                                >
                                            </asp:DropDownList></td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="rfvMacroProceso" runat="server" ControlToValidate="ddlMacroproceso"
                                                ErrorMessage="Debe ingresar el Macroproceso." ToolTip="Debe ingresar el Macroproceso."
                                                ValidationGroup="ControlInfraestructura" ForeColor="Red" InitialValue="0">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                    <tr>
                        <td class="RowsText">
                            <asp:Label ID="LallProcess" runat="server" Text="Todos los Procesos:" CssClass="Apariencia"></asp:Label>
                        </td>
                        <td align="center">
                            <asp:CheckBox ID="CBallProcess" runat="server" AutoPostBack="True" OnCheckedChanged="CBallProcess_CheckedChanged" />
                        </td>
                        <td></td>
                    </tr>
                        <tr>
                            <td class="RowsText">
                                <asp:Label ID="Lcargo" runat="server" Text="Cargo:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="tbxResponsable" runat="server" Width="300px" CssClass="Apariencia"></asp:TextBox>
                                
                            </td>
                            <td>
                                
                                <asp:Label ID="lblIdDependencia4" runat="server" Visible="False" Font-Names="Calibri"
                                    Font-Size="Small"></asp:Label>
                                <asp:ImageButton ID="imgDependencia4" runat="server" ImageUrl="~/Imagenes/Icons/Organization-Chart.png"
                                    OnClientClick="return false;" />
                                <asp:RequiredFieldValidator ID="RFVcargo" runat="server" ControlToValidate="tbxResponsable"
                                    ErrorMessage="Debe ingresar el Cargo." ToolTip="Debe ingresar el Cargo."
                                    ValidationGroup="ControlInfraestructura" ForeColor="Red">*</asp:RequiredFieldValidator>
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
                            <td class="RowsText">
                                <asp:Label ID="Lactividad" runat="server" Text="Actividad:" CssClass="Apariencia" Width="300px" style="height: 13px"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtActividad" runat="server"  CssClass="Apariencia" MaxLength="150" Width="300px"></asp:TextBox>
                                
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="rfvActividad" runat="server" ControlToValidate="txtActividad"
                                    ErrorMessage="Debe ingresar la Actividad." ToolTip="Debe ingresar la Actividad."
                                    ValidationGroup="ControlInfraestructura" ForeColor="Red">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    <tr >
                        <td class="RowsText">
                            <asp:Label ID="LfechaProg" runat="server" Text="Fecha Programación Actividad:" CssClass="Apariencia" Width="300px"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TXfechaProg" runat="server" Font-Names="Calibri" Font-Size="Small" ValidationGroup="ControlInfraestructura" Width="300px"></asp:TextBox>
                        
                        </td>
                        <td>
                            <asp:CalendarExtender ID="CEfechaProg" runat="server" Enabled="true" TargetControlID="TXfechaProg"
                            Format="yyyy-MM-dd">
                        </asp:CalendarExtender>
                        <asp:RequiredFieldValidator ID="RVfechaProg" runat="server" ControlToValidate="TXfechaProg"
                                    ErrorMessage="Debe ingresar la fecha de la programación de la Actividad." ToolTip="Debe ingresar la fecha de la programación de la Actividad."
                                    ValidationGroup="ControlInfraestructura" ForeColor="Red">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr >
                        <td class="RowsText">
                            <asp:Label ID="LFechaCump" runat="server" Text="Fecha Cumplimiento Actividad:" CssClass="Apariencia" Width="300px"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TXfechaCump" runat="server" Font-Names="Calibri" Font-Size="Small" ValidationGroup="ControlInfraestructura" Width="300px"></asp:TextBox>
                        
                        </td>
                        <td>
                            <asp:CalendarExtender ID="CEfechaCump" runat="server" Enabled="true" TargetControlID="TXfechaCump"
                            Format="yyyy-MM-dd" >
                        </asp:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td class="RowsText">
                                <asp:Label ID="Lobservaciones" runat="server" Text="Observaciones:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TXobservaciones" runat="server" Width="300px" CssClass="Apariencia" Wrap="true" TextMode="MultiLine" Height="60px" MaxLength="1000"
                                    ></asp:TextBox>
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
                                    ImageUrl="~/Imagenes/Icons/guardar.png" Text="Insert" ValidationGroup="ControlInfraestructura" ToolTip="Insertar" Visible="false" OnClick="IBinsertGVC_Click"/>
            <asp:ImageButton ID="IBupdateGVC" runat="server" CausesValidation="true" CommandName="Update"
                                    ImageUrl="~/Imagenes/Icons/guardar.png" Text="Update" ValidationGroup="ControlInfraestructura" ToolTip="Actualizar" Visible="false" OnClick="IBupdateGVC_Click"/>
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