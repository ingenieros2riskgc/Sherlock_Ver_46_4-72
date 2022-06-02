<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ConfFactorRiesgo.ascx.cs"
    Inherits="ListasSarlaft.UserControls.ConfigEstructura.ConfFactorRiesgo" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<style type="text/css">
    .gridViewHeader a:link
    {
        text-decoration: none;
    }

    .FondoAplicacion
    {
        background-color: Gray;
        filter: alpha(opacity=80);
        opacity: 0.8;
    }

    .scrollingControlContainer
    {
        overflow-x: hidden;
        overflow-y: scroll;
    }

    .scrollingCheckBoxList
    {
        border: 1px #808080 solid;
        margin: 10px 10px 10px 10px;
        height: 200px;
    }

    .popup
    {
        border: Silver 1px solid;
        color: #060F40;
        background: #ffffff;
    }
</style>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <table align="center" bgcolor="#EEEEEE">
            <tr align="center" bgcolor="#333399">
                <td>
                    <asp:Label ID="Label61" runat="server" ForeColor="White" Text="Configuración Factores de Riesgo"
                        Font-Bold="False" Font-Names="Calibri" Font-Size="Large"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <table id="TblMostrarFactor" runat="server" align="center">
                        <tr>
                            <td>
                                <asp:GridView ID="gvFactores" runat="server" AutoGenerateColumns="False" BorderStyle="Solid"
                                    CellPadding="4" Font-Names="Calibri" Font-Size="Small" ForeColor="#333333" GridLines="Vertical"
                                    HeaderStyle-CssClass="gridViewHeader" HorizontalAlign="Center" OnPageIndexChanging="gvFactores_PageIndexChanging"
                                    OnRowCommand="gvFactores_RowCommand" ShowHeaderWhenEmpty="True" AllowPaging="False">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:BoundField DataField="StrIdFactorRiesgo" HeaderText="IdFactor" Visible="false" />
                                        <asp:BoundField DataField="StrCodigoFactorRiesgo" HeaderText="Código Factor Riesgo" />
                                        <asp:BoundField DataField="StrDescFactorRiesgo" HeaderText="Descripción Factor Riesgo" />
                                        <asp:ButtonField ButtonType="Image" CommandName="ModificarFactor" ImageUrl="~/Imagenes/Icons/edit.png"
                                            Text="Modificar Factor" HeaderText="Modificar">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:ButtonField>
                                        <asp:ButtonField ButtonType="Image" CommandName="EliminarFactor" ImageUrl="~/Imagenes/Icons/delete.png"
                                            Text="Eliminar Factor" HeaderText="Eliminar">
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
                    <asp:ImageButton ID="btnAgregarFactor" runat="server" ImageUrl="~/Imagenes/Icons/Add.png"
                        OnClick="btnAgregarFactor_Click" ToolTip="Agregar" />
                </td>
            </tr>
            <tr>
                <td>
                    <table id="TblGestionFactor" runat="server" align="center" visible="false">
                        <tr id="Tr8" align="center" runat="server">
                            <td id="Td20" bgcolor="#333399" runat="server">
                                <asp:Label ID="lblTituloGestion" runat="server" Text="Creación de Factor Riesgo"
                                    Font-Bold="False" Font-Names="Calibri" Font-Size="Small" Style="font-weight: 700; color: #FFFFFF;"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TabContainer ID="TabContainerFactor" runat="server" Font-Names="Calibri" Font-Size="Small"
                                    Width="700px" Height="405px" ActiveTabIndex="0">
                                    <asp:TabPanel ID="TabPanelFactor" runat="server" HeaderText="Definición de Factor de Riesgo"
                                        Font-Names="Calibri" Font-Size="Small">
                                        <ContentTemplate>
                                            <table id="TblFactor" runat="server" align="center">
                                                <tr id="Tr1" runat="server">
                                                    <td id="Td1" runat="server">
                                                        <table id="TblInsertarFactor" runat="server" align="center">
                                                            <tr id="Tr2" align="left" runat="server">
                                                                <td id="Td2" bgcolor="#5D7B9D" style="color: #FFFFFF; font-weight: 700" runat="server">
                                                                    <asp:Label ID="Label1" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Código Factor:"></asp:Label>
                                                                </td>
                                                                <td id="Td3" runat="server">
                                                                    <asp:TextBox ID="tbxCodigoFactor" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                                        Width="97px"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="InsFactor"
                                                                        ControlToValidate="tbxCodigoFactor" Display="Dynamic" Font-Names="Calibri" Font-Size="Small"
                                                                        ForeColor="Red">*</asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                            <tr id="Tr3" align="left" runat="server">
                                                                <td id="Td4" bgcolor="#5D7B9D" style="font-weight: 700; color: #FFFFFF" runat="server">
                                                                    <asp:Label ID="Label2" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Descripción Factor:"></asp:Label>
                                                                </td>
                                                                <td id="Td5" runat="server">
                                                                    <asp:TextBox ID="tbxDescFactor" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                                        Width="150px"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="InsFactor"
                                                                        ControlToValidate="tbxDescFactor" Display="Dynamic" Font-Names="Calibri" Font-Size="Small"
                                                                        ForeColor="Red">*</asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                            <tr id="Tr7" align="left" runat="server">
                                                                <td id="Td18" runat="server" colspan="2" align="center">
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:ImageButton ID="ibtnGuardarFactor" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                                                    ToolTip="Guardar" Visible="false" OnClick="ibtnGuardarFactor_Click" ValidationGroup="InsFactor" />
                                                                            </td>
                                                                            <td>
                                                                                <asp:ImageButton ID="ibtnGuardarUpdFactor" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                                                    ToolTip="Modificar" Visible="false" OnClick="ibtnGuardarUpdFactor_Click" ValidationGroup="InsFactor" />
                                                                            </td>
                                                                            <td>
                                                                                <asp:ImageButton ID="ibtnCancelFactor" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                                                                    ToolTip="Cancelar" OnClick="ibtnCancelFactor_Click" />
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
                                    <asp:TabPanel ID="TabPanelRelacion" runat="server" HeaderText="Relacion Señales - Factor de Riesgo"
                                        Font-Names="Calibri" Font-Size="Small">
                                        <ContentTemplate>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <table id="TblSenalAsoc" runat="server" align="center" visible="False">
                                                            <tr align="left" runat="server">
                                                                <td bgcolor="#EEEEEE" runat="server">
                                                                    <asp:Panel ID="PnlSenalAsoc" runat="server" CssClass="scrollingControlContainer scrollingCheckBoxList"
                                                                        Width="300px">
                                                                        <asp:CheckBoxList ID="chbSenalAsoc" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                                            Enabled="False">
                                                                        </asp:CheckBoxList>
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                            <tr runat="server">
                                                                <td runat="server">
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:ImageButton ID="ibtnGuardarRelacion" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                                                    OnClick="ibtnGuardarRelacion_Click" ToolTip="Guardar" />
                                                                            </td>
                                                                            <td>
                                                                                <asp:ImageButton ID="ibtnCancelRelacion" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                                                                    OnClick="ibtnCancelRelacion_Click" ToolTip="Cancelar" />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
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

        <asp:ModalPopupExtender ID="mpeMsgBoxOkNo" runat="server" TargetControlID="btndummyOkNo"
            PopupControlID="pnlMsgBoxOkNo" OkControlID="btnCancelarOkNo" BackgroundCssClass="FondoAplicacion"
            Enabled="True" DropShadow="true">
        </asp:ModalPopupExtender>
        <asp:Button ID="btndummyOkNo" runat="server" Text="Button" Style="display: none" />
        <asp:Label ID="lbldummyOkNo" runat="server" Text="" Visible="False"></asp:Label>
        <asp:Panel ID="pnlMsgBoxOkNo" runat="server" Width="400px" Style="display: none;"
            BorderColor="#575757" BackColor="#FFFFFF" BorderStyle="Solid">
            <table width="100%">
                <tr class="topHandle" style="background-color: #5D7B9D">
                    <td colspan="2" align="center" runat="server" id="tdCaptionOkNo">&nbsp;
                        <asp:Label ID="lblCaptionOkNo" runat="server" Text="Atención" Font-Names="Calibri"
                            Font-Size="Small"></asp:Label><br />
                    </td>
                </tr>
                <tr>
                    <td style="width: 60px" valign="middle" align="center">
                        <asp:Image ID="imgInfoOkNo" runat="server" ImageUrl="~/Imagenes/Icons/icontexto-webdev-about.png" />
                    </td>
                    <td valign="middle" align="left">
                        <asp:Label ID="lblMsgBoxOkNo" runat="server" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="right" colspan="2">
                        <asp:Button ID="btnAceptarOkNo" runat="server" Text="Ok" Font-Names="Calibri" Font-Size="Small"
                            OnClick="btnAceptarOkNo_Click" />
                        <asp:Button ID="btnCancelarOkNo" runat="server" Text="Cancelar" Font-Names="Calibri"
                            Font-Size="Small" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>
