<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Segmentacion.ascx.cs"
    Inherits="ListasSarlaft.UserControls.Segmentacion" %>
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
</style>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <table align="center" bgcolor="#EEEEEE">
            <tr align="center" bgcolor="#333399">
                <td>
                    <asp:Label ID="Label61" runat="server" ForeColor="White" Text="Arbol de segmentación"
                        Font-Bold="False" Font-Names="Calibri" Font-Size="Large"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <table align="center">
                        <tr id="trFactorRiesgo" runat="server" visible="false">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label8" runat="server" Text="Codigo de Factor de Riesgo SARLAFT:"
                                    Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                            <td bgcolor="#EEEEEE">
                                <asp:Label ID="Label9" runat="server" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label10" runat="server" Text="Nombre de Factor de Riesgo SARLAFT:"
                                    Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                            <td bgcolor="#EEEEEE">
                                <asp:Label ID="Label11" runat="server" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                            <td bgcolor="#EEEEEE">
                                <asp:Button ID="Button2" runat="server" Text="Ver" OnClick="Button2_Click" Font-Names="Calibri"
                                    Font-Size="Small" />
                            </td>
                        </tr>
                        <tr id="trSegmentos" runat="server" visible="false">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label18" runat="server" Text="Codigo Segmentación de Factores de Riesgo:"
                                    Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                            <td bgcolor="#EEEEEE">
                                <asp:Label ID="Label19" runat="server" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label20" runat="server" Text="Nombre Segmentación de Factores de Riesgo:"
                                    Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                            <td bgcolor="#EEEEEE">
                                <asp:Label ID="Label21" runat="server" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                            <td bgcolor="#EEEEEE">
                                <asp:Button ID="Button4" runat="server" Text="Ver" OnClick="Button4_Click" Font-Names="Calibri"
                                    Font-Size="Small" />
                            </td>
                        </tr>
                        <tr id="trTiposSegmento" runat="server" visible="false">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label28" runat="server" Text="Codigo del Tipo Segmento: " Font-Names="Calibri"
                                    Font-Size="Small"></asp:Label>
                            </td>
                            <td bgcolor="#EEEEEE">
                                <asp:Label ID="Label29" runat="server" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label30" runat="server" Text="Nombre del Tipo Segmento: " Font-Names="Calibri"
                                    Font-Size="Small"></asp:Label>
                            </td>
                            <td bgcolor="#EEEEEE">
                                <asp:Label ID="Label31" runat="server" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                            <td bgcolor="#EEEEEE">
                                <asp:Button ID="Button6" runat="server" Text="Ver" OnClick="Button6_Click" Font-Names="Calibri"
                                    Font-Size="Small" />
                            </td>
                        </tr>
                        <tr id="trAtributos" runat="server" visible="false">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label38" runat="server" Text="Codigo Atributo:" Font-Names="Calibri"
                                    Font-Size="Small"></asp:Label>
                            </td>
                            <td bgcolor="#EEEEEE">
                                <asp:Label ID="Label39" runat="server" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label40" runat="server" Text="Nombre Atributo:" Font-Names="Calibri"
                                    Font-Size="Small"></asp:Label>
                            </td>
                            <td bgcolor="#EEEEEE">
                                <asp:Label ID="Label41" runat="server" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                            <td bgcolor="#EEEEEE">
                                <asp:Button ID="Button8" runat="server" Text="Ver" OnClick="Button8_Click" Font-Names="Calibri"
                                    Font-Size="Small" />
                            </td>
                        </tr>
                        <tr id="trPerfilSegmento" runat="server" visible="false">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label48" runat="server" Text="Codigo Perfil:" Font-Names="Calibri"
                                    Font-Size="Small"></asp:Label>
                            </td>
                            <td bgcolor="#EEEEEE">
                                <asp:Label ID="Label49" runat="server" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label50" runat="server" Text="Nombre Perfil:" Font-Names="Calibri"
                                    Font-Size="Small"></asp:Label>
                            </td>
                            <td bgcolor="#EEEEEE">
                                <asp:Label ID="Label51" runat="server" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                            <td bgcolor="#EEEEEE">
                                <asp:Button ID="Button1" runat="server" Text="Ver" Font-Names="Calibri" Font-Size="Small"
                                    OnClick="Button1_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table id="tbFactorRiesgo" runat="server" visible="true" width="100%">
                        <tr align="center">
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label1" runat="server" Text="FACTORES DE RIESGO SARLAFT" Font-Names="Calibri"
                                                Font-Size="Small"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr align="center">
                            <td>
                                <table>
                                    <tr align="left">
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label3" runat="server" Text="Codigo:" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                        </td>
                                        <td bgcolor="#EEEEEE">
                                            <asp:TextBox ID="TextBox1" runat="server" MaxLength="10" Width="150px" Enabled="False"
                                                Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="InsFR"
                                                ControlToValidate="TextBox1" Display="Dynamic" Font-Names="Calibri" Font-Size="Small"
                                                ForeColor="Red">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr align="left">
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label4" runat="server" Text="Nombre:" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                        </td>
                                        <td bgcolor="#EEEEEE">
                                            <asp:TextBox ID="TextBox2" runat="server" MaxLength="40" Width="300px" Font-Names="Calibri"
                                                Font-Size="Small"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="InsFR"
                                                ControlToValidate="TextBox2" Display="Dynamic" Font-Names="Calibri" Font-Size="Small"
                                                ForeColor="Red">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr align="left">
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label6" runat="server" Text="Descripción:" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                        </td>
                                        <td bgcolor="#EEEEEE">
                                            <asp:TextBox ID="TextBox4" runat="server" MaxLength="1000" Width="300px" Height="50px"
                                                TextMode="MultiLine" Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr align="left">
                                        <td>
                                            <asp:Label ID="Label5" runat="server" Text="Indicador:" Visible="False" Font-Names="Calibri"
                                                Font-Size="Small"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextBox3" runat="server" MaxLength="80" Width="300px" Visible="false"
                                                Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="InsFR"
                                                ControlToValidate="TextBox3" Display="Dynamic" Font-Names="Calibri" Font-Size="Small"
                                                ForeColor="Red">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Imagenes/Icons/Add.png"
                                                            ToolTip="Insertar" OnClick="ImageButton1_Click" ValidationGroup="InsFR" />
                                                    </td>
                                                    <td>
                                                        <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                            ToolTip="Modificar" Visible="false" OnClick="ImageButton2_Click" ValidationGroup="InsFR" />
                                                    </td>
                                                    <td>
                                                        <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                                            ToolTip="Cancelar" Visible="false" OnClick="Button2_Click" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr align="center">
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label7" runat="server" Text="Detalle de Factores de Riesgo SARLAFT"
                                                Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                                ForeColor="#333333" GridLines="Vertical" OnRowCommand="GridView1_RowCommand"
                                                ShowHeaderWhenEmpty="True" HeaderStyle-CssClass="gridViewHeader" BorderStyle="Solid"
                                                HorizontalAlign="Center" Font-Names="Calibri" Font-Size="Small">
                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                <Columns>
                                                    <asp:BoundField DataField="IdFactorRiesgo" HeaderText="IdFactorRiesgo" Visible="false" />
                                                    <asp:BoundField DataField="Codigo" HeaderText="Codigo" />
                                                    <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                                                    <asp:BoundField DataField="Indicador" HeaderText="Indicador" Visible="false" />
                                                    <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                                                    <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Icons/select.png" Text="Seleccionar"
                                                        CommandName="Seleccionar" />
                                                    <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Icons/edit.png" Text="Modificar"
                                                        CommandName="Modificar" />
                                                    <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Icons/delete.png" Text="Borrar"
                                                        CommandName="Borrar" />
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
                    <table id="tbSegmentos" runat="server" visible="false" width="100%">
                        <tr align="center">
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label12" runat="server" Text="SEGMENTACIÓN DE FACTORES DE RIESGO"
                                                Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr align="center">
                            <td>
                                <table>
                                    <tr align="left">
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label14" runat="server" Text="Codigo: " Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                        </td>
                                        <td bgcolor="#EEEEEE">
                                            <asp:TextBox ID="TextBox5" runat="server" Width="150px" MaxLength="10" Enabled="False"
                                                Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="InsSegmentacion"
                                                ControlToValidate="TextBox5" Display="Dynamic" Font-Names="Calibri" Font-Size="Small"
                                                ForeColor="Red">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr align="left">
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label15" runat="server" Text="Nombre: " Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                        </td>
                                        <td bgcolor="#EEEEEE">
                                            <asp:TextBox ID="TextBox6" runat="server" Width="300px" MaxLength="40" Font-Names="Calibri"
                                                Font-Size="Small"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ValidationGroup="InsSegmentacion"
                                                ControlToValidate="TextBox6" Display="Dynamic" Font-Names="Calibri" Font-Size="Small"
                                                ForeColor="Red">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr align="left">
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label16" runat="server" Text="Descripción: " Font-Names="Calibri"
                                                Font-Size="Small"></asp:Label>
                                        </td>
                                        <td bgcolor="#EEEEEE">
                                            <asp:TextBox ID="TextBox7" runat="server" Width="300px" MaxLength="1000" Height="50px"
                                                TextMode="MultiLine" Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/Imagenes/Icons/Add.png"
                                                            ToolTip="Insertar" OnClick="ImageButton4_Click" ValidationGroup="InsSegmentacion" />
                                                    </td>
                                                    <td>
                                                        <asp:ImageButton ID="ImageButton5" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                            ToolTip="Modificar" Visible="false" OnClick="ImageButton5_Click" ValidationGroup="InsSegmentacion" />
                                                    </td>
                                                    <td>
                                                        <asp:ImageButton ID="ImageButton6" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                                            ToolTip="Cancelar" Visible="false" OnClick="Button4_Click" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr align="center">
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label17" runat="server" Text="Detalle Segmentación de factores de riesgo"
                                                Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                                ForeColor="#333333" GridLines="Vertical" OnRowCommand="GridView2_RowCommand"
                                                ShowHeaderWhenEmpty="True" HeaderStyle-CssClass="gridViewHeader" BorderStyle="Solid"
                                                HorizontalAlign="Center" Font-Names="Calibri" Font-Size="Small">
                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                <Columns>
                                                    <asp:BoundField DataField="IdSegmento" HeaderText="IdSegmento" Visible="false" />
                                                    <asp:BoundField DataField="Codigo" HeaderText="Codigo" />
                                                    <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                                                    <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                                                    <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Icons/select.png" Text="Seleccionar"
                                                        CommandName="Seleccionar" />
                                                    <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Icons/edit.png" Text="Modificar"
                                                        CommandName="Modificar" />
                                                    <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Icons/delete.png" Text="Borrar"
                                                        CommandName="Borrar" />
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
                    <table id="tbTiposSegmento" runat="server" visible="false" width="100%">
                        <tr align="center">
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label22" runat="server" Text="TIPO SEGMENTO" Font-Names="Calibri"
                                                Font-Size="Small"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr align="center">
                            <td>
                                <table>
                                    <tr align="left">
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label24" runat="server" Text="Codigo: " Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                        </td>
                                        <td bgcolor="#EEEEEE">
                                            <asp:TextBox ID="TextBox8" runat="server" Width="150px" MaxLength="10" Enabled="False"
                                                Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ValidationGroup="InsSegmento"
                                                ControlToValidate="TextBox8" Display="Dynamic" Font-Names="Calibri" Font-Size="Small"
                                                ForeColor="Red">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr align="left">
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label25" runat="server" Text="Nombre: " Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                        </td>
                                        <td bgcolor="#EEEEEE">
                                            <asp:TextBox ID="TextBox9" runat="server" Width="300px" MaxLength="40" Font-Names="Calibri"
                                                Font-Size="Small"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ValidationGroup="InsSegmento"
                                                ControlToValidate="TextBox9" Display="Dynamic" Font-Names="Calibri" Font-Size="Small"
                                                ForeColor="Red">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr align="left">
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label26" runat="server" Text="Descripción: " Font-Names="Calibri"
                                                Font-Size="Small"></asp:Label>
                                        </td>
                                        <td bgcolor="#EEEEEE">
                                            <asp:TextBox ID="TextBox10" runat="server" Width="300px" MaxLength="1000" Height="50px"
                                                TextMode="MultiLine" Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:ImageButton ID="ImageButton7" runat="server" ImageUrl="~/Imagenes/Icons/Add.png"
                                                            ToolTip="Insertar" OnClick="ImageButton7_Click" ValidationGroup="InsSegmento" />
                                                    </td>
                                                    <td>
                                                        <asp:ImageButton ID="ImageButton8" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                            ToolTip="Modificar" Visible="false" OnClick="ImageButton8_Click" ValidationGroup="InsSegmento" />
                                                    </td>
                                                    <td>
                                                        <asp:ImageButton ID="ImageButton9" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                                            ToolTip="Cancelar" Visible="false" OnClick="Button6_Click" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr align="center">
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label27" runat="server" Text="Detalle de Tipos Segmentos" Font-Names="Calibri"
                                                Font-Size="Small"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                                ForeColor="#333333" GridLines="Vertical" OnRowCommand="GridView3_RowCommand"
                                                ShowHeaderWhenEmpty="True" HeaderStyle-CssClass="gridViewHeader" BorderStyle="Solid"
                                                HorizontalAlign="Center" Font-Names="Calibri" Font-Size="Small">
                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                <Columns>
                                                    <asp:BoundField DataField="IdTipoSegmento" HeaderText="IdTipoSegmento" Visible="false" />
                                                    <asp:BoundField DataField="Codigo" HeaderText="Codigo" />
                                                    <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                                                    <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                                                    <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Icons/select.png" Text="Seleccionar"
                                                        CommandName="Seleccionar" />
                                                    <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Icons/edit.png" Text="Modificar"
                                                        CommandName="Modificar" />
                                                    <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Icons/delete.png" Text="Borrar"
                                                        CommandName="Borrar" />
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
                    <table id="tbAtributos" runat="server" visible="false" width="100%">
                        <tr align="center">
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label33" runat="server" Text="ATRIBUTOS" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr align="center">
                            <td>
                                <table>
                                    <tr align="left">
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label35" runat="server" Text="Codigo: " Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                        </td>
                                        <td bgcolor="#EEEEEE">
                                            <asp:TextBox ID="TextBox11" runat="server" Width="150px" MaxLength="10" Enabled="False"
                                                Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ValidationGroup="InsAtributo"
                                                ControlToValidate="TextBox11" Display="Dynamic" Font-Names="Calibri" Font-Size="Small"
                                                ForeColor="Red">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr align="left">
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label36" runat="server" Text="Nombre: " Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                        </td>
                                        <td bgcolor="#EEEEEE">
                                            <asp:TextBox ID="TextBox12" runat="server" Width="300px" MaxLength="40" Font-Names="Calibri"
                                                Font-Size="Small"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ValidationGroup="InsAtributo"
                                                ControlToValidate="TextBox12" Display="Dynamic" Font-Names="Calibri" Font-Size="Small"
                                                ForeColor="Red">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr align="left">
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label37" runat="server" Text="Descripción: " Font-Names="Calibri"
                                                Font-Size="Small"></asp:Label>
                                        </td>
                                        <td bgcolor="#EEEEEE">
                                            <asp:TextBox ID="TextBox13" runat="server" Width="300px" MaxLength="1000" Height="50px"
                                                TextMode="MultiLine" Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:ImageButton ID="ImageButton10" runat="server" ImageUrl="~/Imagenes/Icons/Add.png"
                                                            ToolTip="Insertar" OnClick="ImageButton10_Click" ValidationGroup="InsAtributo" />
                                                    </td>
                                                    <td>
                                                        <asp:ImageButton ID="ImageButton11" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                            ToolTip="Modificar" Visible="false" OnClick="ImageButton11_Click" ValidationGroup="InsAtributo" />
                                                    </td>
                                                    <td>
                                                        <asp:ImageButton ID="ImageButton12" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                                            ToolTip="Cancelar" Visible="false" OnClick="Button8_Click" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr align="center">
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label32" runat="server" Text="Detalle de Atibutos" Font-Names="Calibri"
                                                Font-Size="Small"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:GridView ID="GridView4" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                                ForeColor="#333333" GridLines="Vertical" OnRowCommand="GridView4_RowCommand"
                                                ShowHeaderWhenEmpty="True" HeaderStyle-CssClass="gridViewHeader" BorderStyle="Solid"
                                                HorizontalAlign="Center" Font-Names="Calibri" Font-Size="Small">
                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                <Columns>
                                                    <asp:BoundField DataField="IdAtributo" HeaderText="IdAtributo" Visible="false" />
                                                    <asp:BoundField DataField="Codigo" HeaderText="Codigo" />
                                                    <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                                                    <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                                                    <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Icons/select.png" Text="Seleccionar"
                                                        CommandName="Seleccionar" />
                                                    <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Icons/edit.png" Text="Modificar"
                                                        CommandName="Modificar" />
                                                    <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Icons/delete.png" Text="Borrar"
                                                        CommandName="Borrar" />
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
                    <table id="tbPerfilSegmento" runat="server" visible="false" width="100%">
                        <tr align="center">
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label42" runat="server" Text="PERFIL DE SEGMENTO" Font-Names="Calibri"
                                                Font-Size="Small"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr align="center">
                            <td>
                                <table>
                                    <tr align="left">
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label2" runat="server" Text="Codigo: " Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                        </td>
                                        <td bgcolor="#EEEEEE">
                                            <asp:TextBox ID="TextBox14" runat="server" Width="150px" MaxLength="10" Enabled="False"
                                                Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ValidationGroup="InsPerfil"
                                                ControlToValidate="TextBox14" Display="Dynamic" Font-Names="Calibri" Font-Size="Small"
                                                ForeColor="Red">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr align="left">
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label13" runat="server" Text="Nombre: " Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                        </td>
                                        <td bgcolor="#EEEEEE">
                                            <asp:TextBox ID="TextBox15" runat="server" Width="300px" MaxLength="40" Font-Names="Calibri"
                                                Font-Size="Small"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ValidationGroup="InsPerfil"
                                                ControlToValidate="TextBox15" Display="Dynamic" Font-Names="Calibri" Font-Size="Small"
                                                ForeColor="Red">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr align="left">
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label23" runat="server" Text="Descripción: " Font-Names="Calibri"
                                                Font-Size="Small"></asp:Label>
                                        </td>
                                        <td bgcolor="#EEEEEE">
                                            <asp:TextBox ID="TextBox16" runat="server" Width="300px" MaxLength="1000" Height="50px"
                                                TextMode="MultiLine" Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr align="center">
                                        <td colspan="2">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:ImageButton ID="ImageButton13" runat="server" ImageUrl="~/Imagenes/Icons/Add.png"
                                                            ToolTip="Insertar" OnClick="ImageButton13_Click" ValidationGroup="InsPerfil" />
                                                    </td>
                                                    <td>
                                                        <asp:ImageButton ID="ImageButton14" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                            ToolTip="Modificar" Visible="false" OnClick="ImageButton14_Click" Height="20px" ValidationGroup="InsPerfil" />
                                                    </td>
                                                    <td>
                                                        <asp:ImageButton ID="ImageButton15" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                                            ToolTip="Cancelar" Visible="false" OnClick="Button1_Click" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr align="center">
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label44" runat="server" Text="Detalle Perfil de Segmento" Font-Names="Calibri"
                                                Font-Size="Small"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:GridView ID="GridView5" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                                ForeColor="#333333" GridLines="Vertical" OnRowCommand="GridView5_RowCommand"
                                                ShowHeaderWhenEmpty="True" HeaderStyle-CssClass="gridViewHeader" BorderStyle="Solid"
                                                HorizontalAlign="Center" Font-Names="Calibri" Font-Size="Small">
                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                <Columns>
                                                    <asp:BoundField DataField="IdPerfil" HeaderText="IdPerfil" Visible="false" />
                                                    <asp:BoundField DataField="Codigo" HeaderText="Codigo" />
                                                    <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                                                    <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                                                    <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Icons/select.png" Text="Seleccionar"
                                                        CommandName="Seleccionar" />
                                                    <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Icons/edit.png" Text="Modificar"
                                                        CommandName="Modificar" />
                                                    <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Icons/delete.png" Text="Borrar"
                                                        CommandName="Borrar" />
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
                    <table id="tbIndicador" runat="server" visible="false" width="100%">
                        <tr align="center">
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label52" runat="server" Text="INDICADOR" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr align="center">
                            <td>
                                <table>
                                    <tr align="left">
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label54" runat="server" Text="Codigo: " Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                        </td>
                                        <td bgcolor="#EEEEEE">
                                            <asp:TextBox ID="TextBox18" runat="server" Width="150px" MaxLength="10" Enabled="False"
                                                Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ValidationGroup="InsIndicador"
                                                ControlToValidate="TextBox18" Display="Dynamic" Font-Names="Calibri" Font-Size="Small"
                                                ForeColor="Red">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr align="left">
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label55" runat="server" Text="Nombre: " Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                        </td>
                                        <td bgcolor="#EEEEEE">
                                            <asp:TextBox ID="TextBox19" runat="server" Width="300px" MaxLength="40" Font-Names="Calibri"
                                                Font-Size="Small"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ValidationGroup="InsIndicador"
                                                ControlToValidate="TextBox19" Display="Dynamic" Font-Names="Calibri" Font-Size="Small"
                                                ForeColor="Red">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr align="left">
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label56" runat="server" Text="Indicador: " Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                        </td>
                                        <td bgcolor="#EEEEEE">
                                            <asp:TextBox ID="TextBox20" runat="server" Width="300px" MaxLength="1000" Height="50px"
                                                TextMode="MultiLine" Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ValidationGroup="InsIndicador"
                                                ControlToValidate="TextBox20" Display="Dynamic" Font-Names="Calibri" Font-Size="Small"
                                                ForeColor="Red">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr align="left">
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label57" runat="server" Text="Mensaje señal de alerta: " Font-Names="Calibri"
                                                Font-Size="Small"></asp:Label>
                                        </td>
                                        <td bgcolor="#EEEEEE">
                                            <asp:TextBox ID="TextBox21" runat="server" Width="300px" MaxLength="1000" Height="50px"
                                                TextMode="MultiLine" Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ValidationGroup="InsIndicador"
                                                ControlToValidate="TextBox21" Display="Dynamic" Font-Names="Calibri" Font-Size="Small"
                                                ForeColor="Red">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr align="left">
                                        <td colspan="2">
                                            <table>
                                                <tr>
                                                    <td bgcolor="#BBBBBB">
                                                        <asp:Label ID="Label43" runat="server" Text="Atributo 1:" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                                    </td>
                                                    <td bgcolor="#EEEEEE">
                                                        <asp:DropDownList ID="DropDownList1" runat="server" Width="155px" AutoPostBack="True"
                                                            Font-Names="Calibri" Font-Size="Small" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                                                            <asp:ListItem Value="---">---</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td bgcolor="#BBBBBB">
                                                        <asp:Label ID="Label45" runat="server" Text="Rango 1:" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                                    </td>
                                                    <td bgcolor="#EEEEEE">
                                                        <asp:DropDownList ID="DropDownList2" runat="server" Width="155px" Font-Names="Calibri"
                                                            Font-Size="Small">
                                                            <asp:ListItem Value="---">---</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr align="left">
                                        <td colspan="2">
                                            <table>
                                                <tr>
                                                    <td bgcolor="#BBBBBB">
                                                        <asp:Label ID="Label46" runat="server" Text="Atributo 2:" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                                    </td>
                                                    <td bgcolor="#EEEEEE">
                                                        <asp:DropDownList ID="DropDownList3" runat="server" Width="155px" AutoPostBack="True"
                                                            Font-Names="Calibri" Font-Size="Small" OnSelectedIndexChanged="DropDownList3_SelectedIndexChanged">
                                                            <asp:ListItem Value="---">---</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td bgcolor="#BBBBBB">
                                                        <asp:Label ID="Label47" runat="server" Text="Rango 2:" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                                    </td>
                                                    <td bgcolor="#EEEEEE">
                                                        <asp:DropDownList ID="DropDownList4" runat="server" Width="155px" Font-Names="Calibri"
                                                            Font-Size="Small">
                                                            <asp:ListItem Value="---">---</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr align="center">
                                        <td colspan="2">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:ImageButton ID="ImageButton16" runat="server" ImageUrl="~/Imagenes/Icons/Add.png"
                                                            ToolTip="Insertar" OnClick="ImageButton16_Click" ValidationGroup="InsIndicador" />
                                                    </td>
                                                    <td>
                                                        <asp:ImageButton ID="ImageButton17" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                            ToolTip="Modificar" Visible="false" OnClick="ImageButton17_Click" ValidationGroup="InsIndicador" />
                                                    </td>
                                                    <td>
                                                        <asp:ImageButton ID="ImageButton18" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                                            ToolTip="Cancelar" Visible="false" OnClick="ImageButton18_Click" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr align="center">
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label53" runat="server" Text="Detalle Indicador" Font-Names="Calibri"
                                                Font-Size="Small"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:GridView ID="GridView6" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                                ForeColor="#333333" GridLines="Vertical" ShowHeaderWhenEmpty="True" HeaderStyle-CssClass="gridViewHeader"
                                                BorderStyle="Solid" HorizontalAlign="Center" Font-Names="Calibri" Font-Size="Small"
                                                OnRowCommand="GridView6_RowCommand">
                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                <Columns>
                                                    <asp:BoundField DataField="IdIndicador" HeaderText="IdIndicador" Visible="false" />
                                                    <asp:BoundField DataField="Codigo" HeaderText="Codigo" />
                                                    <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                                                    <asp:BoundField DataField="Indicador" HeaderText="Indicador" />
                                                    <asp:BoundField DataField="MensajeSenalAlerta" HeaderText="Mensaje" />
                                                    <asp:BoundField DataField="NombreAtributo1" HeaderText="Atributo 1" />
                                                    <asp:BoundField DataField="NombreRango1" HeaderText="Rango 1" />
                                                    <asp:BoundField DataField="NombreAtributo2" HeaderText="Atributo 2" />
                                                    <asp:BoundField DataField="NombreRango2" HeaderText="Rango 2" />
                                                    <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Icons/edit.png" Text="Modificar"
                                                        CommandName="Modificar" />
                                                    <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Icons/delete.png" Text="Borrar"
                                                        CommandName="Borrar" />
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
