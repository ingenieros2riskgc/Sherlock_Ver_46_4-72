<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Indicadores.ascx.cs"
    Inherits="ListasSarlaft.UserControls.Gestion.Indicadores" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<style type="text/css">
     
    
</style>
<%--<link href="../../Styles/MastersPages.css" rel="stylesheet" type="text/css" />--%>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <table align="center" bgcolor="#EEEEEE">
           
            <tr align="center" bgcolor="#333399">
                <td>
                    <asp:Label ID="Label7" runat="server" ForeColor="White" Text="Indicadores" Font-Bold="False"
                        Font-Names="Calibri" Font-Size="Large"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                    <table id="TbMostrarIndicadores" runat="server" align="center">
                        <tr>
                            <td>
                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BorderStyle="Solid"
                                    CellPadding="4" Font-Names="Calibri" Font-Size="Small" ForeColor="#333333" GridLines="Vertical"
                                    HeaderStyle-CssClass="gridViewHeader" HorizontalAlign="Center" OnPageIndexChanging="GridView1_PageIndexChanging"
                                    OnRowCommand="GridView1_RowCommand" ShowHeaderWhenEmpty="True" AllowPaging="True">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:BoundField DataField="IdIndicador" HeaderText="Id" Visible="false" />
                                        <asp:BoundField DataField="CodigoIndicador" HeaderText="Código" />
                                        <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                                        <asp:BoundField DataField="Periodicidad" HeaderText="Periodicidad" />
                                        <asp:BoundField DataField="Meta" HeaderText="Meta" Visible="False">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Nominador" HeaderText="Nominador" Visible="false" />
                                        <asp:BoundField DataField="Denominador" HeaderText="Denominador" Visible="false" />
                                        <asp:BoundField DataField="Activo_SN" HeaderText="Activo">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Usuario" HeaderText="Usuario" Visible="false" />
                                        <asp:BoundField DataField="FechaRegistro" HeaderText="Fecha Registro" Visible="false" />
                                        <asp:ButtonField ButtonType="Image" CommandName="ModificarIndicador" ImageUrl="~/Imagenes/Icons/edit.png"
                                            Text="ModificarIndicador" HeaderText="Modificar">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:ButtonField>
                                        <asp:ButtonField ButtonType="Image" CommandName="EliminarIndicador" ImageUrl="~/Imagenes/Icons/delete.png"
                                            Text="EliminarIndicador" HeaderText="Eliminar">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:ButtonField>
                                    </Columns>
                                    <EditRowStyle BackColor="#999999" />
                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
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
                        <tr align="center">
                            <td>
                                <asp:ImageButton ID="BtnAdIndicador" runat="server" ImageUrl="~/Imagenes/Icons/Add.png"
                                    OnClick="BtnAdIndicador_Click" ToolTip="Agregar" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table id="TbIndicadorSeleccionado" runat="server" align="center" visible="false">
                                    <tr align="left">
                                        <td bgcolor="#5D7B9D" align="center">
                                            <asp:Label ID="Label18" runat="server" Text="Código" Font-Names="Calibri" Font-Size="Small"
                                                Style="color: #FFFFFF; font-weight: 700;"></asp:Label>
                                        </td>
                                        <td bgcolor="#5D7B9D" align="center">
                                            <asp:Label ID="Label19" runat="server" Text="Indicador" Font-Names="Calibri" Font-Size="Small"
                                                Style="color: #FFFFFF; font-weight: 700;"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr align="left">
                                        <td bgcolor="#F7F6F3" align="center">
                                            <asp:Label ID="Label20" runat="server" Text="Código" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                        </td>
                                        <td bgcolor="#F7F6F3" align="center">
                                            <asp:Label ID="Label21" runat="server" Text="Objetivo" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table id="TbIndicadores" runat="server" align="center" visible="false">
                        <tr id="Tr8" align="center" runat="server">
                            <td id="Td20" bgcolor="#333399" runat="server">
                                <asp:Label ID="Label5" runat="server" Text="Creación de Indicadores" Font-Bold="False"
                                    Font-Names="Calibri" Font-Size="Small" Style="font-weight: 700; color: #FFFFFF;"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TabContainer ID="TabContainerIndicadores" runat="server" ActiveTabIndex="1"
                                    Font-Names="Calibri" Font-Size="Small" Width="700px" Height="305px">
                                    <asp:TabPanel ID="TabPanelVariables" runat="server" HeaderText="Definición de Variables"
                                        Font-Names="Calibri" Font-Size="Small">
                                        <HeaderTemplate>
                                            Definición de Variables
                                        </HeaderTemplate>
                                        <ContentTemplate>
                                            <br />
                                            <br />
                                            <table id="TbVariables" runat="server" align="center">
                                                <tr runat="server">
                                                    <td runat="server">
                                                        <table id="TbNombreIndicador" runat="server" align="center">
                                                            <tr id="Tr1" align="left" runat="server">
                                                                <td id="Td1" bgcolor="#5D7B9D" style="color: #FFFFFF; font-weight: 700" runat="server">
                                                                    <asp:Label ID="Label1" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Código:"></asp:Label>
                                                                </td>
                                                                <td id="Td2" runat="server">
                                                                    <asp:TextBox ID="TextBox3" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                                        Width="97px" Enabled="False"></asp:TextBox>
                                                                    <asp:Label ID="Label6" runat="server" Font-Names="Calibri" Font-Size="Small" Visible="False"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr id="Tr2" align="left" runat="server">
                                                                <td id="Td3" bgcolor="#5D7B9D" style="font-weight: 700; color: #FFFFFF" runat="server">
                                                                    <asp:Label ID="Label2" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Nombre:"></asp:Label>
                                                                </td>
                                                                <td id="Td4" runat="server">
                                                                    <asp:TextBox ID="TextBox4" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                                        Width="150px"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="AddIndicador"
                                                                        ControlToValidate="TextBox4" Display="Dynamic" ForeColor="Red">*</asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                            <tr id="Tr3" align="left" runat="server">
                                                                <td id="Td5" bgcolor="#5D7B9D" runat="server">
                                                                    <asp:Label ID="Label4" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Periodicidad:"
                                                                        Style="color: #FFFFFF; font-weight: 700"></asp:Label>
                                                                </td>
                                                                <td id="Td6" runat="server">
                                                                    <asp:DropDownList ID="DropDownListPeriodicidad" runat="server" Font-Names="Calibri"
                                                                        Font-Size="Small" OnSelectedIndexChanged="DropDownListPeriodicidad_SelectedIndexChanged"
                                                                        Width="150px" AutoPostBack="True">
                                                                        <asp:ListItem Selected="True" Value="---">---</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ForeColor="Red" ControlToValidate="DropDownListPeriodicidad"
                                                                        ValidationGroup="AddIndicador" ValueToCompare="---" Operator="NotEqual">*</asp:CompareValidator>
                                                                </td>
                                                            </tr>
                                                            <tr id="Tr7" align="left" runat="server">
                                                                <td id="Td18" runat="server" colspan="2" align="center">
                                                                    <asp:Button ID="BtnIndicador" runat="server" Font-Bold="False" Font-Names="Calibri"
                                                                        Font-Size="Small" OnClick="BtnIndicador_Click" Text="Guardar Indicador" ToolTip="Ver Planes"
                                                                        Width="126px" ValidationGroup="AddIndicador" />
                                                                    <br />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td runat="server">
                                                        &nbsp;
                                                    </td>
                                                    <td runat="server">
                                                        <table id="TbCrearVariables" runat="server" align="center" visible="False">
                                                            <tr id="Tr4" align="center" runat="server">
                                                                <td bgcolor="#5D7B9D" runat="server">
                                                                    <asp:Label ID="Label24" runat="server" Text="Variables" Font-Bold="False" Font-Names="Calibri"
                                                                        Font-Size="Small" Style="font-weight: 700; color: #FFFFFF;"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr id="Tr5" runat="server">
                                                                <td runat="server">
                                                                    <asp:GridView ID="GridViewVariables" runat="server" AutoGenerateColumns="False" BorderStyle="Solid"
                                                                        CellPadding="4" Font-Names="Calibri" Font-Size="Small" ForeColor="#333333" GridLines="Vertical"
                                                                        ShowHeaderWhenEmpty="True" Width="91px" OnRowCommand="GridViewVariables_RowCommand">
                                                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                                        <Columns>
                                                                            <asp:BoundField DataField="IdVariable" HeaderText="Id" Visible="False" />
                                                                            <asp:BoundField DataField="Nombre" HeaderText="Descripción" />
                                                                            <asp:BoundField DataField="Formato" HeaderText="Formato">
                                                                                <ItemStyle HorizontalAlign="Center" />
                                                                            </asp:BoundField>
                                                                            <asp:ButtonField ButtonType="Image" CommandName="ModificarVariable" ImageUrl="~/Imagenes/Icons/edit.png"
                                                                                Text="ModificarVariable" HeaderText="Modificar">
                                                                                <ItemStyle HorizontalAlign="Center" />
                                                                            </asp:ButtonField>
                                                                            <asp:ButtonField ButtonType="Image" CommandName="EliminarVariable" ImageUrl="~/Imagenes/Icons/delete.png"
                                                                                Text="EliminarVariable" HeaderText="Eliminar">
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
                                                            <tr id="Tr6" align="center" runat="server">
                                                                <td id="Td9" runat="server">
                                                                    <asp:ImageButton ID="BtnAdicionaVariable" runat="server" ImageUrl="~/Imagenes/Icons/Add.png"
                                                                        OnClick="BtnAdicionaVariable_Click" ToolTip="Agregar" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <table id="TbAddVaiables" runat="server" align="left" visible="False">
                                                            <tr id="Tr9" align="center" runat="server">
                                                                <td id="Td36" bgcolor="#5D7B9D" runat="server" colspan="2">
                                                                    <asp:Label ID="Label10" runat="server" Text="Crear Variable" Font-Bold="False" Font-Names="Calibri"
                                                                        Font-Size="Small" Style="font-weight: 700; color: #FFFFFF;"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr id="Tr16" runat="server">
                                                                <td id="Td37" bgcolor="#5D7B9D" style="color: #FFFFFF; font-weight: 700" runat="server">
                                                                    <asp:Label ID="Label11" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Descipción:"></asp:Label>
                                                                </td>
                                                                <td id="Td38" runat="server">
                                                                    <asp:TextBox ID="TextBox1" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                                        Width="150px"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBox1"
                                                                        Display="Dynamic" ForeColor="Red" ValidationGroup="AddVariable">*</asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                            <tr id="Tr17" runat="server">
                                                                <td id="Td39" bgcolor="#5D7B9D" style="color: #FFFFFF; font-weight: 700" runat="server">
                                                                    <asp:Label ID="Label12" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Formato:"></asp:Label>
                                                                </td>
                                                                <td id="Td41" runat="server">
                                                                    <asp:DropDownList ID="DropDownListFormato" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                                        OnSelectedIndexChanged="DropDownListFormato_SelectedIndexChanged" Width="150px">
                                                                        <asp:ListItem Selected="True" Value="---">---</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:CompareValidator ID="CompareValidator2" runat="server" ForeColor="Red" ControlToValidate="DropDownListFormato"
                                                                        ValidationGroup="AddVariable" ValueToCompare="---" Operator="NotEqual">*</asp:CompareValidator>
                                                                </td>
                                                            </tr>
                                                            <tr runat="server">
                                                                <td runat="server" align="center" colspan="2">
                                                                    <asp:ImageButton ID="BtnGuardarVariable" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                                        ToolTip="Guardar" OnClick="BtnGuardarVariable_Click" ValidationGroup="AddVariable"
                                                                        Style="height: 20px; width: 20px;" />
                                                                    <asp:ImageButton ID="BtnCancelaVariable" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                                                        ToolTip="Cancelar" OnClick="BtnCancelaVariable_Click" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <table id="TbModificaVariable" runat="server" align="left" visible="False">
                                                            <tr id="Tr21" align="center" runat="server">
                                                                <td id="Td47" bgcolor="#5D7B9D" runat="server" colspan="2">
                                                                    <asp:Label ID="Label14" runat="server" Text="Modificar Variable" Font-Bold="False"
                                                                        Font-Names="Calibri" Font-Size="Small" Style="font-weight: 700; color: #FFFFFF;"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr id="Tr22" runat="server">
                                                                <td id="Td48" bgcolor="#5D7B9D" style="color: #FFFFFF; font-weight: 700" runat="server">
                                                                    <asp:Label ID="Label17" runat="server" Font-Names="Calibri" Font-Size="Small" Visible="False"></asp:Label>
                                                                    <asp:Label ID="Label15" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Descipción:"></asp:Label>
                                                                </td>
                                                                <td id="Td50" runat="server">
                                                                    <asp:TextBox ID="TextBox2" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                                        Width="150px"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TextBox2"
                                                                        Display="Dynamic" ForeColor="Red" ValidationGroup="updateVariable">*</asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                            <tr id="Tr23" runat="server">
                                                                <td id="Td52" bgcolor="#5D7B9D" style="color: #FFFFFF; font-weight: 700" runat="server">
                                                                    <asp:Label ID="Label16" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Formato:"></asp:Label>
                                                                </td>
                                                                <td id="Td53" runat="server">
                                                                    <asp:DropDownList ID="DropDownList1" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                                        Width="150px">
                                                                        <asp:ListItem Selected="True" Value="---">---</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:CompareValidator ID="CompareValidator3" runat="server" ForeColor="Red" ControlToValidate="DropDownList1"
                                                                        ValidationGroup="updateVariable" ValueToCompare="---" Operator="NotEqual">*</asp:CompareValidator>
                                                                </td>
                                                            </tr>
                                                            <tr id="Tr24" runat="server">
                                                                <td id="Td54" runat="server" align="center" colspan="2">
                                                                    <asp:ImageButton ID="BtnModificaVariable" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                                        ToolTip="Guardar" OnClick="BtnModificaVariable_Click" ValidationGroup="updateVariable"
                                                                        Style="height: 20px; width: 20px;" />
                                                                    <asp:ImageButton ID="BtnCancelaAUpVariable" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                                                        ToolTip="Cancelar" OnClick="BtnCancelaAUpVariable_Click" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </asp:TabPanel>
                                    <asp:TabPanel ID="TabPanelMetas" runat="server" HeaderText="Definición de Metas"
                                        Font-Names="Calibri" Font-Size="Small">
                                        <ContentTemplate>
                                            <br />
                                            <br />
                                            <table id="VerlaMetas" runat="server" align="center">
                                                <tr runat="server">
                                                    <td id="Td8" runat="server">
                                                        <table id="TbMetas" runat="server" align="center">
                                                            <tr id="Tr25" align="center" runat="server">
                                                                <td id="Td17" bgcolor="#5D7B9D" runat="server">
                                                                    <asp:Label ID="Label23" runat="server" Text="Metas" Font-Bold="False" Font-Names="Calibri"
                                                                        Font-Size="Small" Style="font-weight: 700; color: #FFFFFF;"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr id="Tr26" runat="server">
                                                                <td id="Td56" runat="server">
                                                                    <asp:GridView ID="GridViewMetas" runat="server" AutoGenerateColumns="False" BorderStyle="Solid"
                                                                        CellPadding="4" Font-Names="Calibri" Font-Size="Small" ForeColor="#333333" GridLines="Vertical"
                                                                        ShowHeaderWhenEmpty="True" OnRowCommand="GridViewMetas_RowCommand" OnPageIndexChanging="GridViewMetas_PageIndexChanging"
                                                                        AllowPaging="True" PageSize="6">
                                                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                                        <Columns>
                                                                            <asp:BoundField DataField="IdIndicador" HeaderText="Id" Visible="False" />
                                                                            <asp:BoundField DataField="IdMetaValor" HeaderText="IdMeta" Visible="False" />
                                                                            <asp:BoundField DataField="Periodo" HeaderText="Periodo">
                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="Meta" HeaderText="Meta">
                                                                                <ItemStyle HorizontalAlign="Right" />
                                                                            </asp:BoundField>
                                                                            <asp:ButtonField ButtonType="Image" CommandName="ModificarMeta" ImageUrl="~/Imagenes/Icons/edit.png"
                                                                                Text="Modificar" HeaderText="Modificar">
                                                                                <ItemStyle HorizontalAlign="Center" />
                                                                            </asp:ButtonField>
                                                                            <asp:ButtonField ButtonType="Image" CommandName="EliminarMeta" ImageUrl="~/Imagenes/Icons/delete.png"
                                                                                Text="Eliminar" HeaderText="Eliminar">
                                                                                <ItemStyle HorizontalAlign="Center" />
                                                                            </asp:ButtonField>
                                                                            <asp:BoundField DataField="Dia" HeaderText="Dia" Visible="False" />
                                                                            <asp:BoundField DataField="Mes" HeaderText="Mes" Visible="False" />
                                                                            <asp:BoundField DataField="Ano" HeaderText="Año" Visible="False" />
                                                                        </Columns>
                                                                        <EditRowStyle BackColor="#999999" />
                                                                        <FooterStyle BackColor="White" Font-Bold="True" ForeColor="White" />
                                                                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" CssClass="gridViewHeader" />
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
                                                            <tr id="Tr27" align="center" runat="server">
                                                                <td id="Td57" runat="server">
                                                                    <asp:ImageButton ID="BtnAdicionaMeta" runat="server" ImageUrl="~/Imagenes/Icons/Add.png"
                                                                        OnClick="BtnAdiciona_Click" ToolTip="Agregar" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <table id="TbAddMetas" runat="server" align="left" visible="False">
                                                            <tr id="Tr28" align="center" runat="server">
                                                                <td id="Td58" bgcolor="#5D7B9D" runat="server" colspan="2">
                                                                    <asp:Label ID="Label28" runat="server" Text="Crear Metas" Font-Bold="False" Font-Names="Calibri"
                                                                        Font-Size="Small" Style="font-weight: 700; color: #FFFFFF;"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr id="Tr30" runat="server">
                                                                <td id="Td61" bgcolor="#5D7B9D" style="color: #FFFFFF; font-weight: 700" runat="server">
                                                                    <asp:Label ID="Label30" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Periodo:"></asp:Label>
                                                                </td>
                                                                <td id="Td62" runat="server">
                                                                    <asp:TextBox ID="TextBox12" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                                        Width="100px" Visible="False"></asp:TextBox>
                                                                    <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="TextBox12"
                                                                        Format="yyyy-MM-dd" BehaviorID="_content_CalendarExtender3">
                                                                    </asp:CalendarExtender>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorDia" runat="server" ControlToValidate="TextBox12"
                                                                        Display="Dynamic" ForeColor="Red">*</asp:RequiredFieldValidator>
                                                                    <asp:DropDownList ID="DropDownListSemana" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                                        Height="21px" Width="100px" Visible="False">
                                                                        <asp:ListItem Selected="True" Value="---">---</asp:ListItem>
                                                                        <asp:ListItem Value="1">Semana 1</asp:ListItem>
                                                                        <asp:ListItem Value="8">Semana 2</asp:ListItem>
                                                                        <asp:ListItem Value="15">Semana 3</asp:ListItem>
                                                                        <asp:ListItem Value="29">Semana 4</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:CompareValidator ID="CompareValidatorSemana" runat="server" ForeColor="Red"
                                                                        ControlToValidate="DropDownListSemana" ValueToCompare="---" Operator="NotEqual">*</asp:CompareValidator>
                                                                    <asp:DropDownList ID="DropDownListQuincena" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                                        Height="21px" Width="100px" Visible="False">
                                                                        <asp:ListItem Selected="True" Value="---">---</asp:ListItem>
                                                                        <asp:ListItem Value="1">Quincena 1</asp:ListItem>
                                                                        <asp:ListItem Value="15">Quincena 2</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:CompareValidator ID="CompareValidatorQuincena" runat="server" ForeColor="Red"
                                                                        ControlToValidate="DropDownListQuincena" ValueToCompare="---" Operator="NotEqual">*</asp:CompareValidator>
                                                                    <asp:DropDownList ID="DropDownListMes" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                                        Height="21px" Width="100px" Visible="False">
                                                                        <asp:ListItem Selected="True" Value="---">---</asp:ListItem>
                                                                        <asp:ListItem Value="1">Enero</asp:ListItem>
                                                                        <asp:ListItem Value="2">Febrero</asp:ListItem>
                                                                        <asp:ListItem Value="3">Marzo</asp:ListItem>
                                                                        <asp:ListItem Value="4">Abril</asp:ListItem>
                                                                        <asp:ListItem Value="5">Mayo</asp:ListItem>
                                                                        <asp:ListItem Value="6">Junio</asp:ListItem>
                                                                        <asp:ListItem Value="7">Julio</asp:ListItem>
                                                                        <asp:ListItem Value="8">Agosto</asp:ListItem>
                                                                        <asp:ListItem Value="9">Septiembre</asp:ListItem>
                                                                        <asp:ListItem Value="10">Octubre</asp:ListItem>
                                                                        <asp:ListItem Value="11">Noviembre</asp:ListItem>
                                                                        <asp:ListItem Value="12">Diciembre</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:CompareValidator ID="CompareValidatorMes" runat="server" ForeColor="Red" ControlToValidate="DropDownListMes"
                                                                        ValueToCompare="---" Operator="NotEqual">*</asp:CompareValidator>
                                                                    <asp:DropDownList ID="DropDownListBimestre" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                                        Height="21px" Width="100px" Visible="False">
                                                                        <asp:ListItem Selected="True" Value="---">---</asp:ListItem>
                                                                        <asp:ListItem Value="1">Ene - Feb</asp:ListItem>
                                                                        <asp:ListItem Value="3">Mar - Abr</asp:ListItem>
                                                                        <asp:ListItem Value="5">May - Jun</asp:ListItem>
                                                                        <asp:ListItem Value="7">Jul - Ago</asp:ListItem>
                                                                        <asp:ListItem Value="9">Sep - Oct</asp:ListItem>
                                                                        <asp:ListItem Value="11">Nov - Dic</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:CompareValidator ID="CompareValidatorBimestre" runat="server" ForeColor="Red"
                                                                        ControlToValidate="DropDownListBimestre" ValueToCompare="---" Operator="NotEqual">*</asp:CompareValidator>
                                                                    <asp:DropDownList ID="DropDownListTrimestre" runat="server" Font-Names="Calibri"
                                                                        Font-Size="Small" Height="21px" Width="100px" Visible="False">
                                                                        <asp:ListItem Selected="True" Value="---">---</asp:ListItem>
                                                                        <asp:ListItem Value="1">Ene - Mar</asp:ListItem>
                                                                        <asp:ListItem Value="4">Abr - Jun</asp:ListItem>
                                                                        <asp:ListItem Value="7">Jul - Sep</asp:ListItem>
                                                                        <asp:ListItem Value="10">Oct - Dic</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:CompareValidator ID="CompareValidatorTrimestre" runat="server" ForeColor="Red"
                                                                        ControlToValidate="DropDownListTrimestre" ValueToCompare="---" Operator="NotEqual">*</asp:CompareValidator>
                                                                    <asp:DropDownList ID="DropDownListSemestre" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                                        Height="21px" Width="100px" Visible="False">
                                                                        <asp:ListItem Selected="True" Value="---">---</asp:ListItem>
                                                                        <asp:ListItem Value="1">Ene - Jun</asp:ListItem>
                                                                        <asp:ListItem Value="7">Jul -  Dic</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:CompareValidator ID="CompareValidatorSemestre" runat="server" ForeColor="Red"
                                                                        ControlToValidate="DropDownListSemestre" ValueToCompare="---" Operator="NotEqual">*</asp:CompareValidator>
                                                                    <asp:DropDownList ID="DropDownListAno" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                                        Height="21px" Width="100px" Visible="False">
                                                                        <asp:ListItem Selected="True" Value="---">---</asp:ListItem>
                                                                        <asp:ListItem Value="2010">2010</asp:ListItem>
                                                                        <asp:ListItem Value="2011">2011</asp:ListItem>
                                                                        <asp:ListItem Value="2012">2012</asp:ListItem>
                                                                        <asp:ListItem Value="2013">2013</asp:ListItem>
                                                                        <asp:ListItem Value="2014">2014</asp:ListItem>
                                                                        <asp:ListItem Value="2015">2015</asp:ListItem>
                                                                        <asp:ListItem Value="2016">2016</asp:ListItem>
                                                                        <asp:ListItem Value="2017">2017</asp:ListItem>
                                                                        <asp:ListItem Value="2018">2018</asp:ListItem>
                                                                        <asp:ListItem Value="2019">2019</asp:ListItem>
                                                                        <asp:ListItem Value="2020">2020</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:CompareValidator ID="CompareValidatorAno" runat="server" ForeColor="Red" ControlToValidate="DropDownListAno"
                                                                        ValueToCompare="---" Operator="NotEqual">*</asp:CompareValidator>
                                                                </td>
                                                            </tr>
                                                            <tr id="Tr29" runat="server">
                                                                <td id="Td59" bgcolor="#5D7B9D" style="color: #FFFFFF; font-weight: 700" runat="server">
                                                                    <asp:Label ID="Label29" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Valor:"></asp:Label>
                                                                </td>
                                                                <td id="Td60" runat="server">
                                                                    <asp:TextBox ID="TextBox10" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                                        Width="100px"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="TextBox10"
                                                                        Display="Dynamic" ForeColor="Red" ValidationGroup="AddMeta">*</asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                            <tr id="Tr31" runat="server">
                                                                <td id="Td63" runat="server" align="center" colspan="2">
                                                                    <asp:ImageButton ID="BtnGuardarMeta" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                                        ToolTip="Guardar" OnClick="BtnGuardarMeta_Click" ValidationGroup="AddMeta" Style="height: 20px;
                                                                        width: 20px;" />
                                                                    <asp:ImageButton ID="BtnUpdaterMeta" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                                        ToolTip="Guardar" OnClick="BtnUpdaterMeta_Click" ValidationGroup="AddMeta" Style="height: 20px;
                                                                        width: 20px;" />
                                                                    <asp:ImageButton ID="BtnCancelaMeta" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                                                        ToolTip="Cancelar" OnClick="BtnCancelaMeta_Click" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </asp:TabPanel>
                                    <asp:TabPanel ID="TabPanelFormulacion" runat="server" HeaderText="Formulación" Font-Names="Calibri"
                                        Font-Size="Small">
                                        <ContentTemplate>
                                            <br />
                                            <br />
                                            <table>
                                                <tr>
                                                    <td>
                                                        <table id="TbSeleccionarVariables" runat="server" align="center" visible="False">
                                                            <tr id="Tr10" align="center" runat="server">
                                                                <td id="Td7" bgcolor="#5D7B9D" runat="server">
                                                                    <asp:Label ID="Label3" runat="server" Text="Variables Disponibles" Font-Bold="False"
                                                                        Font-Names="Calibri" Font-Size="Small" Style="font-weight: 700; color: #FFFFFF;"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr id="Tr11" runat="server">
                                                                <td runat="server">
                                                                    <asp:GridView ID="GridViewSeleccVariables" runat="server" AutoGenerateColumns="False"
                                                                        BorderStyle="Solid" CellPadding="4" Font-Names="Calibri" Font-Size="Small" ForeColor="#333333"
                                                                        GridLines="Vertical" HorizontalAlign="Center" ShowHeaderWhenEmpty="True" Width="127px"
                                                                        OnRowCommand="GridViewSeleccVariables_RowCommand" OnSelectedIndexChanged="GridViewSeleccVariables_SelectedIndexChanged">
                                                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                                        <Columns>
                                                                            <asp:BoundField DataField="IdVariable" HeaderText="Id" Visible="False" />
                                                                            <asp:BoundField DataField="Nombre" HeaderText="Descripción" />
                                                                            <asp:BoundField DataField="Formato" HeaderText="Formato">
                                                                                <ItemStyle HorizontalAlign="Center" />
                                                                            </asp:BoundField>
                                                                            <asp:ButtonField ButtonType="Image" CommandName="SelecVariable" ImageUrl="~/Imagenes/Icons/select.png"
                                                                                Text="SelecVariable" HeaderText="Acción">
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
                                                        </table>
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                    <td>
                                                        <table id="TbCalculadora" runat="server" align="center">
                                                            <tr id="T2" align="center" runat="server">
                                                                <td id="Td10" runat="server" colspan="2">
                                                                </td>
                                                                <td id="Td12" runat="server">
                                                                    <asp:Button ID="ButtonMas" runat="server" Text="+" Width="30px" Font-Names="Calibri"
                                                                        Font-Size="Small" OnClick="ButtonMas_Click" />
                                                                </td>
                                                                <td id="Td13" runat="server">
                                                                    <asp:Button ID="ButtonPor" runat="server" Text="x" Width="30px" Font-Names="Calibri"
                                                                        Font-Size="Small" OnClick="ButtonPor_Click" />
                                                                </td>
                                                                <td id="Td14" runat="server">
                                                                    <asp:Button ID="ButtonAbreP" runat="server" Text="(" Width="30px" Font-Names="Calibri"
                                                                        Font-Size="Small" OnClick="ButtonAbreP_Click" />
                                                                </td>
                                                                <td id="Td15" runat="server">
                                                                    <asp:Button ID="ButtonCierraP" runat="server" Text=")" Width="30px" Font-Names="Calibri"
                                                                        Font-Size="Small" OnClick="ButtonCierraP_Click" />
                                                                </td>
                                                                <td id="Td19" runat="server">
                                                                    <asp:Button ID="ButtonDel" runat="server" Text="C" Width="30px" Font-Names="Calibri"
                                                                        Font-Size="Small" OnClick="ButtonDel_Click" Style="font-weight: 700; color: #FF0000" />
                                                                </td>
                                                            </tr>
                                                            <tr id="T3" align="center" runat="server">
                                                                <td id="Td22" runat="server" colspan="2">
                                                                    <asp:Label ID="Label26" runat="server" Text="Label" Visible="False"></asp:Label>
                                                                </td>
                                                                <td id="Td24" runat="server">
                                                                    <asp:Button ID="ButtonMenos" runat="server" Text="-" Width="30px" Font-Names="Calibri"
                                                                        Font-Size="Small" OnClick="ButtonMenos_Click" />
                                                                </td>
                                                                <td id="Td25" runat="server">
                                                                    <asp:Button ID="ButtonDivide" runat="server" Text="/" Width="30px" Font-Names="Calibri"
                                                                        Font-Size="Small" OnClick="ButtonDivide_Click" />
                                                                </td>
                                                                <td id="Td26" runat="server">
                                                                    <asp:Button ID="ButtonPorc" runat="server" Text="%" Width="30px" Font-Names="Calibri"
                                                                        Font-Size="Small" OnClick="ButtonPorc_Click" />
                                                                </td>
                                                                <td id="Td27" runat="server">
                                                                    <asp:Button ID="ButtonCero" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                                        OnClick="ButtonCero_Click" Text="0" Width="30px" />
                                                                </td>
                                                                <td id="Td28" runat="server">
                                                                    <asp:Button ID="ButtonCien" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                                        OnClick="ButtonCien_Click" Text="100" Width="30px" />
                                                                </td>
                                                            </tr>
                                                            <tr id="T5" align="center" runat="server">
                                                                <td id="Td40" runat="server">
                                                                </td>
                                                                <td id="Td11" runat="server" align="center">
                                                                    <asp:Button ID="BtnNominador" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                                        OnClick="BtnNominador_Click" Text="Nominador:" Width="104px" ToolTip="Nominador" />
                                                                    <br />
                                                                    <asp:Image ID="ImageNominadr" runat="server" ImageUrl="~/Imagenes/Icons/suc.png"
                                                                        Visible="False" />
                                                                    <br />
                                                                </td>
                                                                <td id="Td21" runat="server" colspan="5">
                                                                    <asp:TextBox ID="TextBox5" runat="server" Font-Names="Calibri" Enabled="False" Font-Size="Small"
                                                                        TextMode="MultiLine" Width="274px" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="AddIndicadorAll"
                                                                        ControlToValidate="TextBox5" Display="Dynamic" ForeColor="Red">*</asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                            <tr id="T6" align="center" runat="server">
                                                                <td id="Td49" runat="server">
                                                                </td>
                                                                <td id="Td16" runat="server" align="center">
                                                                    <asp:Button ID="BtnDenominador" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                                        OnClick="BtnDenominador_Click" Text="Denominador:" Width="103px" ToolTip="Denominador" />
                                                                    <br />
                                                                    <asp:Image ID="ImageDenominador" runat="server" ImageUrl="~/Imagenes/Icons/suc.png"
                                                                        Visible="False" />
                                                                    <br />
                                                                </td>
                                                                <td id="Td51" runat="server" colspan="5">
                                                                    <asp:TextBox ID="TextBox6" runat="server" Font-Names="Calibri" Enabled="False" Font-Size="Small"
                                                                        TextMode="MultiLine" Width="274px" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr runat="server">
                                                                <td runat="server" colspan="7" align="center">
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <br />
                                                    </td>
                                            </table>
                                        </ContentTemplate>
                                    </asp:TabPanel>
                                    <asp:TabPanel ID="TabPanelVerFormula" runat="server" HeaderText="Ver Fórmula" Font-Names="Calibri"
                                        Font-Size="Small">
                                        <ContentTemplate>
                                            <br />
                                            <table id="VerlaFormula" runat="server" align="center">
                                                <tr runat="server">
                                                    <td runat="server">
                                                        <table id="LaFormula" runat="server" align="center">
                                                            <tr id="Tr12" align="center" runat="server">
                                                                <td id="Td29" bgcolor="#5D7B9D" runat="server" colspan="2">
                                                                    <asp:Label ID="Label8" runat="server" Text="Fórmula del Indicador" Font-Bold="False"
                                                                        Font-Names="Calibri" Font-Size="Small" Style="font-weight: 700; color: #FFFFFF;"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr id="Tr13" runat="server">
                                                                <td id="Td30" runat="server">
                                                                </td>
                                                                <td id="Td31" runat="server">
                                                                </td>
                                                            </tr>
                                                            <tr id="Tr18" runat="server">
                                                                <td id="Td42" runat="server">
                                                                </td>
                                                                <td id="Td43" runat="server" align="center">
                                                                    <asp:TextBox ID="TextBox7" runat="server" Enabled="False" Font-Names="Calibri" Font-Size="Small"
                                                                        TextMode="MultiLine" Width="274px" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr id="Tr14" runat="server">
                                                                <td id="Td32" runat="server">
                                                                    <asp:Label ID="Label9" runat="server" Font-Bold="False" Font-Names="Calibri" Font-Size="Medium"
                                                                        Style="font-weight: 700; color: #5D7B9D;"></asp:Label>
                                                                </td>
                                                                <td id="Td33" runat="server">
                                                                    <asp:Label ID="Label13" Font-Bold="True" runat="server" Text="_________________________________________________________"
                                                                        Font-Names="Calibri" Font-Size="Small" Style="font-weight: 700; color: #5D7B9D;"></asp:Label>
                                                                    <br />
                                                                    <br />
                                                                </td>
                                                            </tr>
                                                            <tr id="Tr15" runat="server">
                                                                <td id="Td34" runat="server">
                                                                </td>
                                                                <td id="Td35" runat="server" align="center">
                                                                    <asp:TextBox ID="TextBox8" runat="server" Enabled="False" Font-Names="Calibri" Font-Size="Small"
                                                                        TextMode="MultiLine" Width="274px" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr id="Tr19" runat="server">
                                                                <td id="Td44" runat="server">
                                                                    <br />
                                                                    <asp:Label ID="Label27" runat="server" Font-Bold="False" Text="Activo:" Font-Names="Calibri"
                                                                        Font-Size="Small" Style="font-weight: 700; color: #5D7B9D;"></asp:Label>
                                                                </td>
                                                                <td id="Td45" runat="server">
                                                                    <br />
                                                                    <asp:DropDownList ID="DropDownListActivo" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                                        Width="63px">
                                                                        <asp:ListItem Selected="True" Value="---">---</asp:ListItem>
                                                                        <asp:ListItem Value="S">Si</asp:ListItem>
                                                                        <asp:ListItem Value="N">No</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:CompareValidator ID="CompareValidator4" runat="server" ForeColor="Red" ControlToValidate="DropDownListActivo"
                                                                        ValidationGroup="AddIndicadorAll" ValueToCompare="---" Operator="NotEqual">*</asp:CompareValidator>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </asp:TabPanel>
                                </asp:TabContainer>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr align="center">
                <td>
                    <table id="TbBotones" runat="server" align="center" visible="false">
                        <tr>
                            <td>
                                <asp:ImageButton ID="BtnGuardarIndicador" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                    ToolTip="Guardar" OnClick="BtnGuardarIndicador_Click" ValidationGroup="AddIndicadorAll"
                                    Style="width: 20px;" />
                            </td>
                            <td>
                                <asp:ImageButton ID="BtnCancelaIndicador" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                    ToolTip="Cancelar" OnClick="BtnCancelaIndicador_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <asp:ModalPopupExtender ID="mpeMsgBox" runat="server" TargetControlID="btndummy"
            PopupControlID="pnlMsgBox" OkControlID="btnAceptar" BackgroundCssClass="FondoAplicacion"
            Enabled="True" DropShadow="true">
        </asp:ModalPopupExtender>
        <asp:Button ID="btndummy" runat="server" Text="Button" Style="display: none" />
        <asp:Panel ID="pnlMsgBox" runat="server" Width="400px" Style="display: none;" BorderColor="#575757"
            BorderWidth="1px" BackColor="White" BorderStyle="Solid">
            <table width="100%">
                <tr class="topHandle" style="background-color: #333399">
                    <td colspan="2" align="center" runat="server" id="tdCaption">
                        &nbsp;
                        <asp:Label ID="lblCaption" runat="server" Text="Atención" ForeColor="White" Font-Names="Calibri"
                            Font-Size="Small"></asp:Label><br />
                    </td>
                </tr>
                <tr>
                    <td style="width: 60px" valign="middle" align="center">
                        <asp:Image ID="imgInfo" runat="server" ImageUrl="~/Imagenes/Icons/icontexto-webdev-ok.png" />
                    </td>
                    <td valign="middle" align="left">
                        <asp:Label ID="lblMsgBox" runat="server" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="right" colspan="2">
                        <asp:Button ID="btnAceptar" runat="server" Text="Ok" Font-Names="Calibri" Font-Size="Small" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:ModalPopupExtender ID="mpeMsgBox1" runat="server" TargetControlID="btndummy1"
            PopupControlID="pnlMsgBox1" OkControlID="btnAceptar1" BackgroundCssClass="FondoAplicacion"
            Enabled="True" DropShadow="true">
        </asp:ModalPopupExtender>
        <asp:Button ID="btndummy1" runat="server" Text="Button1" Style="display: none" />
        <asp:Panel ID="pnlMsgBox1" runat="server" Width="400px" Style="display: none;" BorderColor="#575757"
            BorderWidth="1px" BackColor="White" BorderStyle="Solid">
            <table width="100%">
                <tr class="topHandle" style="background-color: #333399">
                    <td colspan="2" align="center" runat="server" id="td23">
                        &nbsp;
                        <asp:Label ID="Label25" runat="server" Text="Atención" ForeColor="White" Font-Names="Calibri"
                            Font-Size="Small"></asp:Label><br />
                    </td>
                </tr>
                <tr>
                    <td style="width: 60px" valign="middle" align="center">
                        <asp:Image ID="imgInfo1" runat="server" ImageUrl="~/Imagenes/Icons/icontexto-webdev-cancel.png" />
                    </td>
                    <td valign="middle" align="left">
                        <asp:Label ID="lblMsgBox1" runat="server" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="right" colspan="2">
                        <asp:Button ID="btnAceptar1" runat="server" Text="Ok" Font-Names="Calibri" Font-Size="Small" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:ModalPopupExtender ID="mpeMsgBox2" runat="server" TargetControlID="btndummy2"
            PopupControlID="pnlMsgBox2" OkControlID="btnAceptar2" BackgroundCssClass="FondoAplicacion"
            Enabled="True" DropShadow="true">
        </asp:ModalPopupExtender>
        <asp:Button ID="btndummy2" runat="server" Text="Button2" Style="display: none" />
        <asp:Panel ID="pnlMsgBox2" runat="server" Width="400px" Style="display: none;" BorderColor="#575757"
            BorderWidth="1px" BackColor="White" BorderStyle="Solid">
            <table width="100%">
                <tr class="topHandle" style="background-color: #333399">
                    <td colspan="2" align="center" runat="server" id="td64">
                        &nbsp;
                        <asp:Label ID="Label31" runat="server" Text="Atención" ForeColor="White" Font-Names="Calibri"
                            Font-Size="Small"></asp:Label><br />
                    </td>
                </tr>
                <tr>
                    <td style="width: 60px" valign="middle" align="center">
                        <asp:Image ID="Image1" runat="server" ImageUrl="~/Imagenes/Icons/Alerta.png" />
                    </td>
                    <td valign="middle" align="left">
                        <asp:Label ID="lblMsgBox2" runat="server" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="right" colspan="2">
                        <asp:Button ID="btnAceptar2" runat="server" Text="Ok" Font-Names="Calibri" Font-Size="Small" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>
