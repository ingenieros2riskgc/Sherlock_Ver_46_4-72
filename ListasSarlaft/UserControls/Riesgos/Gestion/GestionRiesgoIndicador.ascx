<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GestionRiesgoIndicador.ascx.cs" Inherits="ListasSarlaft.UserControls.Riesgos.Gestion.GestionRiesgoIndicador" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Src="~/UserControls/Sitio/OkMessageBox.ascx" TagPrefix="uc" TagName="OkMessageBox" %>
<link href="../../../Styles/AdminSitio.css" rel="stylesheet" type="text/css" />
<style type="text/css">
    
    
div.ajax__calendar_days table tr td{padding-right: 0px;}
div.ajax__calendar_body{width: 225px;}
div.ajax__calendar_container{width: 225px;}

    </style>
<asp:UpdatePanel ID="GRIbody" runat="server">
    <ContentTemplate>
        <uc:OkMessageBox ID="omb" runat="server" />
        <div class="TituloLabel" id="HeadGRI" runat="server">
                    <asp:Label ID="Ltitulo" runat="server" ForeColor="White" Text="Gestión de Indicadores de Riesgo" Font-Bold="False"
                        Font-Names="Calibri" Font-Size="Large"></asp:Label>
            </div>
        <div id="BodyGridGRI" class="ColumnStyle" runat="server">
            <table class="tabla" align="center" width="100%">
                <tr align="center">
                    <td>
                        <asp:GridView ID="GVriesgosIndicadores" runat="server" CellPadding="4"
                            ForeColor="#333333" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
                            ShowHeaderWhenEmpty="True" DataKeyNames="strUsuario,dtFechaCreacion,intIdMeta,intIdEsquemaSeguimiento,intIdFormula, intIProcesoIndicador, intIdFrecuenciaMedicion,intIdProceso,booActivo"
                            HeaderStyle-CssClass="gridViewHeader" BorderStyle="Solid" GridLines="Vertical"
                            CssClass="Apariencia" Font-Bold="False" OnRowCommand="GVriesgosIndicadores_RowCommand" OnPageIndexChanging="GVriesgosIndicadores_PageIndexChanging" >
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
                                <asp:ButtonField ButtonType="Image" HeaderText="Gestionar Valor Variable" CommandName="gestionar" ImageUrl="~/Imagenes/Icons/select.png"
                                                        Text="Asociar">
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
            </table>
        </div>
        <div runat="server" id="dvVariables" visible="false" class="ColumnStyle">
            <table class="tabla" align="center" width="100%">
                <tr>
                    <td>
                        <div class="TituloLabel" id="dvTitutloVariable" runat="server">
                    <asp:Label ID="lblVariableT" runat="server" ForeColor="White" Text="Variables disponibles a gestionar" Font-Bold="False"
                        Font-Names="Calibri" Font-Size="Large"></asp:Label>
            </div>
                    </td>
                </tr>
                <tr align="center">
                    <td>
            <asp:GridView ID="GridViewVariables" runat="server" AutoGenerateColumns="False" BorderStyle="Solid"
                                                                    CellPadding="4" Font-Names="Calibri" Font-Size="Small" ForeColor="#333333" GridLines="Vertical"
                                                                    DataKeyNames="intIdFormato,intIdVariableRiesgoIndicador"
                                                                    ShowHeaderWhenEmpty="True" Width="962px" OnRowCommand="GridViewVariables_RowCommand">
                                                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                                    <Columns>
                                                                        <asp:BoundField DataField="intIdVariableRiesgoIndicador" HeaderText="Id" Visible="False" />
                                                                        <asp:TemplateField HeaderText="Descripción" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 400px">
                                            <asp:Label ID="strDescripcion" runat="server" Text='<% # Bind("strDescripcion")%>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                    <HeaderStyle Wrap="false" HorizontalAlign="center" />
                                    <ItemStyle Wrap="false" />
                                </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Formato" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 100px">
                                            <asp:Label ID="strFormato" runat="server" Text='<% # Bind("strFormato")%>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                    <HeaderStyle Wrap="false" HorizontalAlign="center" />
                                    <ItemStyle Wrap="false" />
                                </asp:TemplateField>                         
                                                                        <asp:ButtonField ButtonType="Image" HeaderText="Valor Variable" CommandName="valor" ImageUrl="~/Imagenes/Icons/select.png"
                                    Text="Asociar">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:ButtonField>
                                                                    </Columns>
                                                                    <EditRowStyle BackColor="#999999" />
                                                                    <FooterStyle BackColor="White" Font-Bold="True" ForeColor="White" />
                                                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" CssClass="gridViewHeader" />
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
                        <asp:ImageButton ID="btnCancelIndicador" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                                                        ToolTip="Cancelar" OnClick="btnCancelIndicador_Click"  />
                    </td>
                </tr>
                </table>
        </div>
        <div id="dvVariableGestion" runat="server" class="ColumnStyle" visible="false">
            <table id="TbAddVaiables" runat="server" align="center">
                <tr id="trGridVariable" runat="server">
                    <td align="center" colspan="2">
                        <asp:GridView ID="GVvalorVariable" runat="server" AutoGenerateColumns="False" BorderStyle="Solid"
                                                                    CellPadding="4" Font-Names="Calibri" Font-Size="Small" ForeColor="#333333" GridLines="Vertical"
                                                                    DataKeyNames="intIdValorVariable,intIdVariable,intIdFrecuencia,intIdRiesgoIndicador,intIdDetalleFrecuencia"
                                                                    ShowHeaderWhenEmpty="True" Width="91px" OnRowCommand="GVvalorVariable_RowCommand">
                                                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                                    <Columns>
                                                                        <asp:BoundField DataField="intIdValorVariable" HeaderText="Id" Visible="False" />
                                                                        <asp:TemplateField HeaderText="FrecuenciaMedicion" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 100px">
                                            <asp:Label ID="strFrecuenciaMedicion" runat="server" Text='<% # Bind("strFrecuenciaMedicion")%>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                    <HeaderStyle Wrap="false" HorizontalAlign="center" />
                                    <ItemStyle Wrap="false" />
                                </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Valor Frecuencia" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 100px">
                                            <asp:Label ID="strValorFrecuencia" runat="server" Text='<% # Bind("strValorFrecuencia")%>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                    <HeaderStyle Wrap="false" HorizontalAlign="center" />
                                    <ItemStyle Wrap="false" />
                                </asp:TemplateField>   
                                                                        <asp:TemplateField HeaderText="Descripción Detalle Frecuencia" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 100px">
                                            <asp:Label ID="strDescripcionDetalle" runat="server" Text='<% # Bind("strDescripcionDetalle")%>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                    <HeaderStyle Wrap="false" HorizontalAlign="center" />
                                    <ItemStyle Wrap="false" />
                                </asp:TemplateField> 
                                                                        <asp:BoundField DataField="dblValorVariable" HeaderText="Valor Variable"/>     
                                                                        <asp:TemplateField HeaderText="Año" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 100px">
                                            <asp:Label ID="strAño" runat="server" Text='<% # Bind("strAño")%>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                    <HeaderStyle Wrap="false" HorizontalAlign="center" />
                                    <ItemStyle Wrap="false" />
                                </asp:TemplateField> 
                                                                        <asp:TemplateField HeaderText="Mes" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 100px">
                                            <asp:Label ID="strMes" runat="server" Text='<% # Bind("strMes")%>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                    <HeaderStyle Wrap="false" HorizontalAlign="center" />
                                    <ItemStyle Wrap="false" />
                                </asp:TemplateField>                 
                                                                        <asp:ButtonField ButtonType="Image" HeaderText="Modificar" CommandName="modificar" ImageUrl="~/Imagenes/Icons/select.png"
                                    Text="Asociar">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:ButtonField>
                                                                    </Columns>
                                                                    <EditRowStyle BackColor="#999999" />
                                                                    <FooterStyle BackColor="White" Font-Bold="True" ForeColor="White" />
                                                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" CssClass="gridViewHeader" />
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
                <tr runat="server" id="trButtonNewV">
                    <td colspan="2">
                        <asp:ImageButton ID="btnInsertarNuevo" runat="server" CausesValidation="False" CommandName="Insert"
                            ImageUrl="~/Imagenes/Icons/Add.png" Text="Insert" ToolTip="Insertar" OnClick="btnInsertarNuevo_Click" />
                        <asp:ImageButton ID="btnCancelVariable" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                                                        ToolTip="Cancelar" OnClick="btnCancelVariable_Click"  />
                    </td>
                </tr>
                                                        <tr id="Tr9" align="center" runat="server" visible="false">
                                                            <td id="Td36" bgcolor="#5D7B9D" runat="server" colspan="4">
                                                                <asp:Label ID="Label10" runat="server" Text="Valor Variable " Font-Bold="False" Font-Names="Calibri"
                                                                    Font-Size="Small" Style="font-weight: 700; color: #FFFFFF;"></asp:Label>
                                                                
                                                            </td>
                                                        </tr>
                                                        <tr id="Tr30" runat="server" visible="false">
                                                            <td id="Td61" bgcolor="#5D7B9D" style="color: #FFFFFF; font-weight: 700" runat="server">
                                                                <asp:Label ID="lblIdVariable" runat="server" Visible="false"></asp:Label>
                                                                <asp:Label ID="Label30" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Periodo:"></asp:Label>
                                                                <asp:Label ID="lblFrecuenciaMedicion" runat="server" Font-Bold="False" Font-Names="Calibri"
                                                                    Font-Size="Small" Style="font-weight: 700; color: #FFFFFF;"></asp:Label>
                                                            </td>
                                                            <td id="Td62" runat="server">
                                                                <asp:TextBox ID="TextBox12" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                                    Width="100px" Visible="False"></asp:TextBox>
                                                                <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="TextBox12"
                                                                        Format="yyyy-MM-dd" Enabled="True"></asp:CalendarExtender>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorDia" runat="server" ControlToValidate="TextBox12" Enabled="false"
                                                                    Display="Dynamic" ForeColor="Red" ValidationGroup="AddVariable" ToolTip="Debe Ingresar un Día" Text="Debe Ingresar un Día">*</asp:RequiredFieldValidator>
                                                                <asp:DropDownList ID="ddlDetalleFrecuencias" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                                        Width="200px" Visible="False" /><asp:CompareValidator ID="CompareValidatorAno" Enabled="false" runat="server" ForeColor="Red" ControlToValidate="ddlDetalleFrecuencias"
                                                                            ValueToCompare="---" Operator="NotEqual" ToolTip="Se debe seleccionar el Periodo a gestionar">*</asp:CompareValidator>
                                                                <asp:TextBox ID="txtFrecuenciaAno" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                                                Width="100px" Visible="False"></asp:TextBox>
                                                                <asp:CalendarExtender ID="ceFrecuenciaAno" runat="server" Enabled="true" Format="yyyy" TargetControlID="txtFrecuenciaAno" DefaultView="Years"></asp:CalendarExtender>
                                                                <asp:RequiredFieldValidator ID="rfvFrecuenciaAno" Enabled="false" runat="server" ControlToValidate="txtFrecuenciaAno"
                                                                    Display="Dynamic" ForeColor="Red" ValidationGroup="AddVariable" ToolTip="Debe Ingresar un Año" Text="Debe Ingresar un Año">*</asp:RequiredFieldValidator>
                                                            </td>
                                                            <td></td>
                                                            <td></td>
                                                        </tr>
                                                        <tr id="TrValorVariable" runat="server" visible="false">
                                                            <td id="Td1" bgcolor="#5D7B9D" style="color: #FFFFFF; font-weight: 700" runat="server">
                                                                <asp:Label ID="lblValorVariable" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Valor de la variable:"></asp:Label></td>
                                                            <td id="Td2" runat="server">
                                                                <asp:TextBox ID="txtValorVariable" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                                    Width="100px"></asp:TextBox><asp:RequiredFieldValidator ID="rfvValorVariable" runat="server" ControlToValidate="txtValorVariable"
                                                                        Display="Dynamic" ForeColor="Red" ValidationGroup="AddVariable">*</asp:RequiredFieldValidator><asp:RegularExpressionValidator ID="revValorVariable" runat="server" ControlToValidate="txtValorVariable"
                                                                            ValidationExpression="^(\d|-)?(\d|,)*\.?\d*$" ForeColor="Red" ErrorMessage="Solo números Enteros o Decimales" ToolTip="Solo números Enteros o Decimales"
                                                                            ValidationGroup="AddVariable">*</asp:RegularExpressionValidator></td>
                                                            <td></td>
                                                            <td></td>
                                                        </tr>
                <tr id="TrfechasIns" runat="server" visible="false">
                                                            <td id="TdAñoMeta" runat="server" bgcolor="#5D7B9D" style="color: #FFFFFF; font-weight: 700">
                                                                <asp:Label ID="lblAnñoMeta" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Año:"></asp:Label>
                                                            </td>
                                                            <td id="TdValorAñoMeta" runat="server">
                                                                <asp:TextBox ID="txbAñoMeta" runat="server" Font-Names="Calibri" Font-Size="Small" Width="100px"></asp:TextBox>
                                                                <asp:CalendarExtender ID="ceAñoMeta" runat="server" Format="yyyy"
                                                                    Enabled="True" TargetControlID="txbAñoMeta">
                                                                </asp:CalendarExtender>
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
                                                        
                                                        <tr runat="server" id="trButtonAdd" visible="false">
                                                            <td runat="server" align="center" colspan="2">
                                                                <asp:ImageButton ID="BtnGuardarVariable" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                                    ToolTip="Guardar" ValidationGroup="AddVariable"
                                                                    Style="height: 20px; width: 20px;" OnClick="BtnGuardarVariable_Click" />
                                                                <asp:ImageButton ID="BtnActualiarVariable" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                                    ToolTip="Guardar" ValidationGroup="AddVariable"
                                                                    Style="height: 20px; " OnClick="BtnActualiarVariable_Click" />
                                                                <asp:ImageButton ID="BtnCancelaVariable" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                                                        ToolTip="Cancelar" OnClick="BtnCancelaVariable_Click"  /></td>
                                                        </tr>
                                                    </table>
        </div>
        </ContentTemplate>
    </asp:UpdatePanel>