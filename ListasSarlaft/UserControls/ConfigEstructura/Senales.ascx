<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Senales.ascx.cs" Inherits="ListasSarlaft.UserControls.ConfigEstructura.Senales" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<style type="text/css">
    .gridViewHeader a:link {
        text-decoration: none;
    }

    .FondoAplicacion {
        background-color: Gray;
        filter: alpha(opacity=80);
        opacity: 0.8;
    }

    .scrollingControlContainer {
        overflow-x: hidden;
        overflow-y: scroll;
    }

    .scrollingCheckBoxList {
        border: 1px #808080 solid;
        margin: 10px 10px 10px 10px;
        height: 200px;
    }

    .popup {
        border: Silver 1px solid;
        color: #060F40;
        background: #ffffff;
    }

    .auto-style1 {
        height: 300px;
    }
</style>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <table align="center" bgcolor="#EEEEEE">
            <tr align="center" bgcolor="#333399">
                <td>
                    <asp:Label ID="Label61" runat="server" ForeColor="White" Text="Configuración Señales Alerta"
                        Font-Bold="False" Font-Names="Calibri" Font-Size="Large"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <table id="TblMostrarSenales" runat="server" align="center">
                        <tr>
                            <td>
                                <asp:GridView ID="gvSenales" runat="server" AutoGenerateColumns="False" BorderStyle="Solid"
                                    CellPadding="4" Font-Names="Calibri" Font-Size="Small" ForeColor="#333333" GridLines="Vertical"
                                    HeaderStyle-CssClass="gridViewHeader" HorizontalAlign="Center" OnPageIndexChanging="gvSenales_PageIndexChanging"
                                    OnRowCommand="gvSenales_RowCommand" ShowHeaderWhenEmpty="True" AllowPaging="False">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:BoundField DataField="StrIdUsuario" HeaderText="IdUsuario" Visible="False" />
                                        <asp:BoundField DataField="StrIdSenal" HeaderText="IdSenal" Visible="false" />
                                        <asp:BoundField DataField="StrCodigoSenal" HeaderText="Código Señal" />
                                        <asp:BoundField DataField="StrDescripcionSenal" HeaderText="Descripción Señal" />
                                        <asp:TemplateField HeaderText="Automático" Visible="false">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chbEsAutomatico" runat="server" Checked='<% # Bind("BooEsAutomatico")%>'
                                                    Enabled="false" />
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:ButtonField ButtonType="Image" CommandName="ModificarSenal" ImageUrl="~/Imagenes/Icons/edit.png"
                                            Text="Modificar Senal" HeaderText="Modificar">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:ButtonField>
                                        <asp:ButtonField ButtonType="Image" CommandName="EliminarSenal" ImageUrl="~/Imagenes/Icons/delete.png"
                                            Text="Eliminar Senal" HeaderText="Eliminar">
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
                    </table>
                </td>
            </tr>
            <tr align="center">
                <td>
                    <asp:ImageButton ID="btnAgregarSenal" runat="server" ImageUrl="~/Imagenes/Icons/Add.png"
                        OnClick="btnAgregarSenal_Click" ToolTip="Agregar" />
                </td>
            </tr>
            <tr>
                <td>
                    <table id="TblGestionSenal" runat="server" align="center" visible="false">
                        <tr id="Tr8" align="center" runat="server">
                            <td id="Td20" bgcolor="#333399" runat="server">
                                <asp:Label ID="lblTituloGestion" runat="server" Text="Creación de Señal de Alerta"
                                    Font-Bold="False" Font-Names="Calibri" Font-Size="Small" Style="font-weight: 700; color: #FFFFFF;"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TabContainer ID="TabContainerSenal" runat="server" Font-Names="Calibri" Font-Size="Small"
                                    Width="700px" Height="1500px" ActiveTabIndex="1">
                                    <asp:TabPanel ID="TabPanelSenal" runat="server" HeaderText="Definición de Señales"
                                        Font-Names="Calibri" Font-Size="Small">
                                        <ContentTemplate>
                                            <table id="TblSenal" runat="server" align="center">
                                                <tr id="Tr1" runat="server">
                                                    <td id="Td1" runat="server">
                                                        <table id="TblInsertarSenal" runat="server" align="center">
                                                            <tr id="Tr2" align="left" runat="server">
                                                                <td id="Td2" bgcolor="#5D7B9D" style="color: #FFFFFF; font-weight: 700" runat="server">
                                                                    <asp:Label ID="Label1" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Código señal:"></asp:Label>
                                                                </td>
                                                                <td id="Td3" runat="server">
                                                                    <asp:TextBox ID="tbxCodigoSenal" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                                        Width="97px"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="InsSenal"
                                                                        ControlToValidate="tbxCodigoSenal" Display="Dynamic" Font-Names="Calibri" Font-Size="Small"
                                                                        ForeColor="Red">*</asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                            <tr id="Tr3" align="left" runat="server">
                                                                <td id="Td4" bgcolor="#5D7B9D" style="font-weight: 700; color: #FFFFFF" runat="server">
                                                                    <asp:Label ID="Label2" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Descripción señal:"></asp:Label>
                                                                </td>
                                                                <td id="Td5" runat="server">
                                                                    <asp:TextBox ID="tbxDescripcionSenal" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                                        Width="150px"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="InsSenal"
                                                                        ControlToValidate="tbxDescripcionSenal" Display="Dynamic" Font-Names="Calibri" Font-Size="Small"
                                                                        ForeColor="Red">*</asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                            <tr id="Tr4" align="left" runat="server" visible="false">
                                                                <td id="Td9" bgcolor="#5D7B9D" style="font-weight: 700; color: #FFFFFF" runat="server">
                                                                    <asp:Label ID="Label4" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Automático:"></asp:Label>
                                                                </td>
                                                                <td id="Td10" runat="server">
                                                                    <asp:CheckBox ID="chbAutomatico" Checked="false" runat="server" />
                                                                </td>
                                                            </tr>
                                                            <tr id="Tr7" align="left" runat="server">
                                                                <td id="Td18" runat="server" colspan="2" align="center">
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:ImageButton ID="ibtnGuardarSenal" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                                                    ToolTip="Guardar" Visible="false" OnClick="ibtnGuardarSenal_Click" ValidationGroup="InsSenal" />
                                                                            </td>
                                                                            <td>
                                                                                <asp:ImageButton ID="ibtnGuardarUpdSenal" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                                                    ToolTip="Modificar" Visible="false" OnClick="ibtnGuardarUpdSenal_Click" ValidationGroup="InsSenal" />
                                                                            </td>
                                                                            <td>
                                                                                <asp:ImageButton ID="ibtnCancelSenal" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                                                                    ToolTip="Cancelar" OnClick="ibtnCancelSenal_Click" />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                    <br />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </asp:TabPanel>
                                    <asp:TabPanel ID="TabPanelFormulacion" runat="server" HeaderText="Formulación" Font-Names="Calibri"
                                        Font-Size="Small" ScrollBars="Vertical">
                                        <ContentTemplate>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <table id="TblSeleccionarVariables" runat="server" align="center" visible="False">
                                                            <tr id="Tr10" align="center" runat="server">
                                                                <td id="Td7" bgcolor="#5D7B9D" runat="server">
                                                                    <asp:Label ID="Label3" runat="server" Text="Variables Disponibles" Font-Bold="False"
                                                                        Font-Names="Calibri" Font-Size="Small" Style="font-weight: 700; color: #FFFFFF;"></asp:Label>
                                                                    <asp:Label ID="LidSeñal" runat="server" Font-Bold="False"
                                                                        Font-Names="Calibri" Font-Size="Small" Style="font-weight: 700; color: #FFFFFF;" Visible="False"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr id="Tr11" runat="server">
                                                                
                                                                <td id="Td6" runat="server">
                                                                    <table>
                                                                        <tr>
                                                                            <td id="TdOperGlobal" runat="server" valign="top">
                                                                                <table>
                                                                                    <tr>
                                                                                        <td id="Td13" runat="server" align="center" bgcolor="#5D7B9D" colspan="2">
                                                                                            <asp:Label ID="Label5" runat="server" Font-Bold="False" Font-Names="Calibri" Font-Size="Small" Style="font-weight: 700; color: #FFFFFF;" Text="Operación Global"></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td id="Td12" runat="server">
                                                                                            <asp:DropDownList ID="ddlOpGlobal" runat="server">
                                                                                            </asp:DropDownList>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:ImageButton ID="ibtnSelecOpGlobal" runat="server" ImageUrl="~/Imagenes/Icons/select.png" OnClick="ibtnSelecOpGlobal_Click" ToolTip="Seleccionar" />
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                            <td id="Td8" runat="server"></td>
                                                                            <td id="TdVariable" runat="server" valign="top">
                                                                                <asp:GridView ID="gvVariable" runat="server" AutoGenerateColumns="False" BorderStyle="Solid" CellPadding="4" Font-Names="Calibri" Font-Size="Small" ForeColor="#333333" GridLines="Vertical" HorizontalAlign="Center" OnPageIndexChanging="gvVariable_PageIndexChanging" OnRowCommand="gvVariable_RowCommand" ShowHeaderWhenEmpty="True" Width="177px">
                                                                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                                                    <Columns>
                                                                                        <asp:BoundField DataField="StrIdEstructCampo" HeaderText="IdVariable" Visible="False" />
                                                                                        <asp:BoundField DataField="StrNombreCampo" HeaderText="Nombre Variable" />
                                                                                        <asp:ButtonField ButtonType="Image" CommandName="SelecVariable" ImageUrl="~/Imagenes/Icons/select.png" Text="SelecVariable" />
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
                                                                            <td id="Td11" runat="server"></td>
                                                                            <td id="TdOperador" runat="server" valign="top">
                                                                                <asp:GridView ID="gvOperador" runat="server" AutoGenerateColumns="False" BorderStyle="Solid" CellPadding="4" Font-Names="Calibri" Font-Size="Small" ForeColor="#333333" GridLines="Vertical" HorizontalAlign="Center" OnPageIndexChanging="gvOperador_PageIndexChanging" OnRowCommand="gvOperador_RowCommand" ShowHeaderWhenEmpty="True">
                                                                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                                                    <Columns>
                                                                                        <asp:BoundField DataField="StrIdOperador" HeaderText="IdOperador" Visible="False" />
                                                                                        <asp:BoundField DataField="StrNombreOperador" HeaderText="Nombre Operador" />
                                                                                        <asp:BoundField DataField="StrIdentificadorOperador" HeaderText="Identificador" />
                                                                                        <asp:ButtonField ButtonType="Image" CommandName="SelecOperador" ImageUrl="~/Imagenes/Icons/select.png" Text="SelecOperador" />
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
                                                                            <td id="Td14" runat="server"></td>
                                                                            <td id="TdOtro" runat="server" align="left" valign="top">
                                                                                <table>
                                                                                    <tr id="trOtroValor" align="left">
                                                                                        <td>
                                                                                            <asp:Label ID="lblOtroValor" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Otro Valor:"></asp:Label>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:TextBox ID="tbxOtroValor" runat="server" Enabled="False" Font-Names="Calibri" Font-Size="Small" Width="65px"></asp:TextBox>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:ImageButton ID="ibtnSelecOtroValor" runat="server" ImageUrl="~/Imagenes/Icons/select.png" OnClick="ibtnSelecOtroValor_Click" Style="height: 16px" ToolTip="Seleccionar" />
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr align="left">
                                                                                        <td colspan="3">
                                                                                            <table id="TblRango">
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <asp:Label ID="lblDesde" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Desde:"></asp:Label>
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:TextBox ID="tbxDesde" runat="server" Font-Names="Calibri" Font-Size="Small" Width="65px"></asp:TextBox>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <asp:Label ID="lblHasta" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Hasta:"></asp:Label>
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:TextBox ID="tbxHasta" runat="server" Font-Names="Calibri" Font-Size="Small" Width="65px"></asp:TextBox>
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:ImageButton ID="ibtnRango" runat="server" ImageUrl="~/Imagenes/Icons/select.png" OnClick="ibtnRango_Click" ToolTip="Seleccionar" />
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                                
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr id="TrInfoFormula" runat="server">
                                                    <td id="Td15" runat="server">
                                                        <table>
                                                            <tr>
                                                                <td id="Td16" runat="server" align="center">
                                                                    <asp:TextBox ID="tbxFormula" runat="server" AutoPostBack="True" BorderStyle="Solid"
                                                                        BorderWidth="1px" Font-Names="Calibri" Font-Size="Small" ReadOnly="True" TextMode="MultiLine"
                                                                        Width="400px"></asp:TextBox>
                                                                </td>
                                                                <td id="Td17" runat="server" align="center">
                                                                    <asp:ImageButton ID="ibtnLimpiarFormula" runat="server" ImageUrl="~/Imagenes/Icons/Trash-icon.png"
                                                                        OnClick="ibtnLimpiarFormula_Click" ToolTip="Limpiar" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr id="TrBotonesFormula" runat="server" align="center">
                                                    <td id="Td19" runat="server">
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:ImageButton ID="ibtnGuardarFormula" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                                        OnClick="ibtnGuardarFormula_Click" ToolTip="Guardar" />
                                                                </td>
                                                                <td>
                                                                    <asp:ImageButton ID="ibtnCancelFormula" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                                                        OnClick="ibtnCancelFormula_Click" ToolTip="Cancelar" />
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
        </table>
        <asp:ModalPopupExtender ID="mpeMsgBox" runat="server" TargetControlID="btndummy"
            PopupControlID="pnlMsgBox" OkControlID="btnAceptar" BackgroundCssClass="FondoAplicacion"
            Enabled="True" DropShadow="true">
        </asp:ModalPopupExtender>
        <asp:Button ID="btndummy" runat="server" Text="Button" Style="display: none" />
        <asp:Panel ID="pnlMsgBox" runat="server" Width="400px" Style="display: none;" BorderColor="#575757"
            BackColor="#FFFFFF" BorderStyle="Solid">
            <table width="100%">
                <tr class="topHandle" style="background-color: #5D7B9D">
                    <td colspan="2" align="center" runat="server" id="tdCaption">&nbsp;
                        <asp:Label ID="lblCaption" runat="server" Text="Atención" Font-Names="Calibri" Font-Size="Small"></asp:Label><br />
                    </td>
                </tr>
                <tr>
                    <td style="width: 60px" valign="middle" align="center">
                        <asp:Image ID="imgInfo" runat="server" ImageUrl="~/Imagenes/Icons/icontexto-webdev-about.png" />
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
    </ContentTemplate>
</asp:UpdatePanel>
