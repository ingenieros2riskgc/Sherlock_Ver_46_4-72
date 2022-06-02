<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ConfEstructuraArchivo.ascx.cs"
    Inherits="ListasSarlaft.UserControls.ConfigEstructura.ConfEstructuraArchivo" %>
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

    .no-visible {
        display: none
    }
</style>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <table align="center" bgcolor="#EEEEEE">
            <tr align="center" bgcolor="#333399">
                <td>
                    <asp:Label ID="Label61" runat="server" ForeColor="White" Text="Configuración Estructura Archivo"
                        Font-Bold="False" Font-Names="Calibri" Font-Size="Large"></asp:Label>
                </td>
            </tr>
            <tr align="left">
                <td>
                    <table>
                        <tr>
                            <td>
                                <asp:Panel ID="Panel1" runat="server">
                                    <asp:GridView ID="gvEstructura" runat="server" CellPadding="4" EnableModelValidation="True"
                                        ForeColor="#333333" GridLines="Vertical" AutoGenerateColumns="False" AllowPaging="False"
                                        OnRowCommand="gvEstructura_RowCommand" ShowHeaderWhenEmpty="True" HeaderStyle-CssClass="gridViewHeader"
                                        BorderStyle="Solid" HorizontalAlign="Center" Font-Names="Calibri" Font-Size="Small">
                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                        <Columns>
                                            <asp:BoundField DataField="StrIdEstructCampo" HeaderText="IdParametros" HeaderStyle-CssClass="no-visible" ItemStyle-CssClass="no-visible"  />
                                            <asp:BoundField DataField="StrNombreCampo" HeaderText="Nombre Campo" />
                                            <asp:BoundField DataField="StrLongitud" HeaderText="Longitud" Visible="False" />
                                            <asp:TemplateField HeaderText="Parametrizable">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chbParametrizable" runat="server" Checked='<% # Bind("BooEsParametrico") %>'
                                                        Enabled="false" />
                                                </ItemTemplate>
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="StrIdUsuario" HeaderText="IdUsuario" Visible="False" />
                                            <asp:BoundField DataField="StrIdVariable" HeaderText="IdVariable" Visible="False" />
                                            <asp:BoundField DataField="StrNombreVariable" HeaderText="Nombre Variable" />
                                            <asp:BoundField DataField="StrIdTipoDato" HeaderText="IdTipoDato" Visible="False" />
                                            <asp:BoundField DataField="StrNombreTipoDato" HeaderText="Nombre Tipo Dato" Visible="False" />
                                            <asp:BoundField DataField="StrPosicion" HeaderText="Posición" />
                                            <asp:TemplateField HeaderText="Estado">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkEstado" runat="server" OnCheckedChanged="chkEstado_CheckedChanged" AutoPostBack="true" 
                                                        Enabled="false"/>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Numerico">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkNumerico" runat="server"
                                                        Enabled="false"/>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            
                                            <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Icons/edit.png" Text="Modificar"
                                                CommandName="Modificar" />
                                        </Columns>
                                        <EditRowStyle BackColor="#999999" />
                                        <FooterStyle BackColor="White" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Left" />
                                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                    </asp:GridView>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr align="right">
                            <td>
                                <asp:ImageButton ID="ibtnAgregar" runat="server" ImageUrl="~/Imagenes/Icons/Add.png"
                                    ToolTip="Agregar" OnClick="ibtnAgregar_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr align="center">
                <td>
                    <table id="updateUser" runat="server" visible="false">
                        <tr align="left">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label5" runat="server" Text="Nombre Campo" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="tbNombreCampo" runat="server" MaxLength="25" Font-Names="Calibri"
                                    Font-Size="Small" Width="200px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="updateUser1"
                                    ControlToValidate="tbNombreCampo" Display="Dynamic" Font-Names="Calibri" Font-Size="Small"
                                    ForeColor="Red">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr align="left" visible="false">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label4" runat="server" Text="Tipo Dato" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="tbTipoDato" runat="server" MaxLength="25" Font-Names="Calibri" Font-Size="Small"
                                    Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr align="left" visible="false">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label2" runat="server" Text="Longitud" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="tbLongitud" runat="server" MaxLength="5" Font-Names="Calibri" Font-Size="Small"
                                    Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr align="left">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label6" runat="server" Text="Posición" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="tbPosicion" runat="server" MaxLength="5" Font-Names="Calibri" Font-Size="Small"
                                    Width="200px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="updateUser1"
                                    ControlToValidate="tbPosicion" Display="Dynamic" Font-Names="Calibri" Font-Size="Small"
                                    ForeColor="Red">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr align="left">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label3" runat="server" Text="Es Paramétrico?" Font-Names="Calibri"
                                    Font-Size="Small"></asp:Label>
                            </td>
                            <td>
                                <asp:CheckBox ID="chbEsParametrico" runat="server" OnCheckedChanged="chbEsParametrico_CheckedChanged"
                                    AutoPostBack="true" />
                            </td>
                        </tr>
                        <tr align="left">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label7" runat="server" Text="Estado" Font-Names="Calibri"
                                    Font-Size="Small"></asp:Label>
                            </td>
                            <td>
                                <asp:CheckBox ID="ChBEstado" runat="server" />
                            </td>
                        </tr>
                        <tr align="left">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label1" runat="server" Text="Tipo Parámetro" Font-Names="Calibri"
                                    Font-Size="Small"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlTipoParametro" runat="server" Width="150px" Font-Names="Calibri"
                                    Font-Size="Small" Enabled="false">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr align="left">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="lblNumerico" runat="server" Text="Es Numerico?" Font-Names="Calibri"
                                    Font-Size="Small"></asp:Label>
                            </td>
                            <td>
                                <asp:CheckBox ID="cbNumerico" runat="server"  />
                            </td>
                        </tr>
                        <tr align="center">
                            <td colspan="2">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:ImageButton ID="ibtnGuardar" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                ToolTip="Guardar" Visible="false" OnClick="ibtnGuardar_Click" ValidationGroup="updateUser1" />
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="ibtnGuardarUpd" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                ToolTip="Modificar" Visible="false" OnClick="ibtnGuardarUpd_Click" ValidationGroup="updateUser1" />
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="ibtnCancelUpd" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                                ToolTip="Cancelar" OnClick="ibtnCancelUpd_Click" />
                                        </td>
                                    </tr>
                                </table>
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
