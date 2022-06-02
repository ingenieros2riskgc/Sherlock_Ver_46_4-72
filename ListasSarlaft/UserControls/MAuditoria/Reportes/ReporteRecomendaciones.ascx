<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ReporteRecomendaciones.ascx.cs"
    Inherits="ListasSarlaft.UserControls.MAuditoria.Reportes.ReporteRecomendaciones" %>
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
        <table align="center" width="100%" bgcolor="#EEEEEE">
            <tr align="center" bgcolor="#333399">
                <td>
                    <asp:Label ID="Label140" runat="server" Font-Bold="False" Font-Names="Calibri" Font-Size="Large"
                        ForeColor="White" Text="Reportes Recomendaciones"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <table runat="server" align="center" id="TblPlaAccion">
                        <tr>
                            <td colspan="3">
                                <br />
                            </td>
                        </tr>
                        <tr align="center">
                            <td bgcolor="#5D7B9D" colspan="3">
                                <asp:Label ID="Label7" runat="server" Text="Planeación" Font-Names="Calibri" Font-Size="Small"
                                    ForeColor="White"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:ListBox ID="ListBox1" runat="server" SelectionMode="Multiple" Font-Names="Calibri"
                                    Font-Size="Small"></asp:ListBox>
                            </td>
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Button ID="BtnSelectAll" runat="server" Text=">>" Height="20px" Width="30px"
                                                OnClick="BtnSelectAll_Click" Font-Names="Calibri" Font-Size="Small" ToolTip="Seleccionar todos" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Button ID="BtnSelectOne" runat="server" Text=">" Height="20px" Width="30px"
                                                OnClick="BtnSelectOne_Click" Font-Names="Calibri" Font-Size="Small" ToolTip="Seleccionar uno" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Button ID="BtnUnSelectAll" runat="server" Text="<<" Height="20px" Width="30px"
                                                OnClick="BtnUnSelectAll_Click" Font-Names="Calibri" Font-Size="Small" ToolTip="Quitar todos" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Button ID="BtnUnSelectOne" runat="server" Text="<" Height="20px" Width="30px"
                                                OnClick="BtnUnSelectOne_Click" Font-Names="Calibri" Font-Size="Small" ToolTip="Quitar uno" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                <asp:ListBox ID="ListBox2" runat="server" SelectionMode="Multiple" Font-Names="Calibri"
                                    Font-Size="Small" Visible="false"></asp:ListBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table runat="server" align="center" id="TblAuditoria" visible="false">
                        <tr>
                            <td colspan="3">
                                <br />
                            </td>
                        </tr>
                        <tr align="center">
                            <td bgcolor="#5D7B9D" colspan="3">
                                <asp:Label ID="Label1" ForeColor="White" runat="server" Text="Auditoría" Font-Names="Calibri"
                                    Font-Size="Small"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:ListBox ID="ListBox3" runat="server" SelectionMode="Multiple" Font-Names="Calibri"
                                    Font-Size="Small"></asp:ListBox>
                            </td>
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Button ID="AuBtnSelectAll" runat="server" Text=">>" Height="20px" Width="30px"
                                                OnClick="AuBtnSelectAll_Click" Font-Names="Calibri" Font-Size="Small" ToolTip="Seleccionar todos" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Button ID="AuBtnSelectOne" runat="server" Text=">" Height="20px" Width="30px"
                                                OnClick="AuBtnSelectOne_Click" Font-Names="Calibri" Font-Size="Small" ToolTip="Seleccionar uno" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Button ID="AuBtnUnSelectAll" runat="server" Text="<<" Height="20px" Width="30px"
                                                OnClick="AuBtnUnSelectAll_Click" Font-Names="Calibri" Font-Size="Small" ToolTip="Quitar todos" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Button ID="AuBtnUnSelectOne" runat="server" Text="<" Height="20px" Width="30px"
                                                OnClick="AuBtnUnSelectOne_Click" Font-Names="Calibri" Font-Size="Small" ToolTip="Quitar uno" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                <asp:ListBox ID="ListBox4" runat="server" SelectionMode="Multiple" Font-Names="Calibri"
                                    Font-Size="Small" Visible="false"></asp:ListBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table runat="server" align="center" id="TblUsuarios" visible="false">
                        <tr>
                            <td colspan="3">
                                <br />
                            </td>
                        </tr>
                        <tr align="center">
                            <td bgcolor="#5D7B9D" colspan="3">
                                <asp:Label ID="Label6" ForeColor="White" runat="server" Text="Usuarios" Font-Names="Calibri"
                                    Font-Size="Small"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:ListBox ID="ListBox11" runat="server" SelectionMode="Multiple" Font-Names="Calibri"
                                    Font-Size="Small"></asp:ListBox>
                            </td>
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Button ID="Button13" runat="server" Text=">>" Height="20px" Width="30px"
                                                 Font-Names="Calibri" Font-Size="Small" ToolTip="Seleccionar todos" 
                                                onclick="Button13_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Button ID="Button14" runat="server" Text=">" Height="20px" Width="30px"
                                                 Font-Names="Calibri" Font-Size="Small" ToolTip="Seleccionar uno" 
                                                onclick="Button14_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Button ID="Button15" runat="server" Text="<<" Height="20px" Width="30px"
                                                 Font-Names="Calibri" Font-Size="Small" ToolTip="Quitar todos" 
                                                onclick="Button15_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Button ID="Button16" runat="server" Text="<" Height="20px" Width="30px"
                                                 Font-Names="Calibri" Font-Size="Small" ToolTip="Quitar uno" 
                                                onclick="Button16_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                <asp:ListBox ID="ListBox12" runat="server" SelectionMode="Multiple" Font-Names="Calibri"
                                    Font-Size="Small" Visible="false"></asp:ListBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table runat="server" id="TblDependenciaProceso" align="center" visible="false">
                        <tr>
                            <td>
                                <br />
                            </td>
                        </tr>
                        <tr align="center">
                            <td bgcolor="#5D7B9D">
                                <asp:Label ID="Label2" runat="server" Text="Dependencia / Proceso" Font-Names="Calibri"
                                    Font-Size="Small" ForeColor="White"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="True" TextAlign="Left"
                                    RepeatDirection="Horizontal" Font-Size="Small" Font-Names="Calibri" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged">
                                    <asp:ListItem Value="0">Dependencia</asp:ListItem>
                                    <asp:ListItem Value="1">Proceso</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table runat="server" align="center" id="TblMacroproceso" visible="false">
                        <tr>
                            <td colspan="3">
                                <br />
                            </td>
                        </tr>
                        <tr align="center">
                            <td bgcolor="#5D7B9D" colspan="3">
                                <asp:Label ID="Label3" runat="server" Text="Macroproceso" Font-Names="Calibri" Font-Size="Small"
                                    ForeColor="White"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:ListBox ID="ListBox5" runat="server" SelectionMode="Multiple" Font-Names="Calibri"
                                    Font-Size="Small"></asp:ListBox>
                            </td>
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Button ID="Button1" runat="server" Text=">>" Height="20px" Width="30px" OnClick="Button1_Click"
                                                ToolTip="Seleccionar todos" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Button ID="Button2" runat="server" Text=">" Height="20px" Width="30px" OnClick="Button2_Click"
                                                Font-Names="Calibri" Font-Size="Small" ToolTip="Seleccionar uno" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Button ID="Button3" runat="server" Text="<<" Height="20px" Width="30px" Font-Names="Calibri"
                                                Font-Size="Small" OnClick="Button3_Click" ToolTip="Quitar todos" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Button ID="Button4" runat="server" Text="<" Height="20px" Width="30px" Font-Names="Calibri"
                                                Font-Size="Small" OnClick="Button4_Click" ToolTip="Quitar uno" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                <asp:ListBox ID="ListBox6" runat="server" SelectionMode="Multiple" Font-Names="Calibri"
                                    Font-Size="Small" Visible="false"></asp:ListBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table runat="server" align="center" id="TblProceso" visible="false">
                        <tr>
                            <td colspan="3">
                                <br />
                            </td>
                        </tr>
                        <tr align="center">
                            <td bgcolor="#5D7B9D" colspan="3">
                                <asp:Label ID="Label4" runat="server" Text="Proceso" Font-Names="Calibri" Font-Size="Small"
                                    ForeColor="White"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:ListBox ID="ListBox7" runat="server" SelectionMode="Multiple" Font-Names="Calibri"
                                    Font-Size="Small"></asp:ListBox>
                            </td>
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Button ID="Button5" runat="server" Text=">>" Height="20px" Width="30px" Font-Names="Calibri"
                                                Font-Size="Small" OnClick="Button5_Click" ToolTip="Seleccionar todos" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Button ID="Button6" runat="server" Text=">" Height="20px" Width="30px" Font-Names="Calibri"
                                                Font-Size="Small" OnClick="Button6_Click" ToolTip="Seleccionar uno" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Button ID="Button7" runat="server" Text="<<" Height="20px" Width="30px" Font-Names="Calibri"
                                                Font-Size="Small" OnClick="Button7_Click" ToolTip="Quitar todos" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Button ID="Button8" runat="server" Text="<" Height="20px" Width="30px" Font-Names="Calibri"
                                                Font-Size="Small" OnClick="Button8_Click" ToolTip="Quitar uno" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                <asp:ListBox ID="ListBox8" runat="server" SelectionMode="Multiple" Font-Names="Calibri"
                                    Font-Size="Small" Visible="false"></asp:ListBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table runat="server" align="center" id="TblDependencia" visible="false" >
                        <tr>
                            <td colspan="3">
                                <br />
                            </td>
                        </tr>
                        <tr align="center">
                            <td bgcolor="#5D7B9D" colspan="3">
                                <asp:Label ID="Label5" runat="server" Text="Dependencia" Font-Names="Calibri" Font-Size="Small"
                                    ForeColor="White"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:ListBox ID="ListBox9" runat="server" SelectionMode="Multiple" Font-Names="Calibri"
                                    Font-Size="Small"></asp:ListBox>
                            </td>
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Button ID="Button9" runat="server" Text=">>" Height="20px" Width="30px" Font-Names="Calibri"
                                                Font-Size="Small" OnClick="Button9_Click" ToolTip="Seleccionar todos" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Button ID="Button10" runat="server" Text=">" Height="20px" Width="30px" Font-Names="Calibri"
                                                Font-Size="Small" OnClick="Button10_Click" ToolTip="Seleccionar uno" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Button ID="Button11" runat="server" Text="<<" Height="20px" Width="30px" Font-Names="Calibri"
                                                Font-Size="Small" OnClick="Button11_Click" ToolTip="Quitar todos" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Button ID="Button12" runat="server" Text="<" Height="20px" Width="30px" Font-Names="Calibri"
                                                Font-Size="Small" OnClick="Button12_Click" ToolTip="Quitar uno" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                <asp:ListBox ID="ListBox10" runat="server" SelectionMode="Multiple" Font-Names="Calibri"
                                    Font-Size="Small" Visible="false"></asp:ListBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table runat="server" align="center" id="TblBotones">
                        <tr>
                            <td>
                                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Imagenes/Icons/lupa.png"
                                    ToolTip="Ver Reportes" OnClick="ImageButton1_Click" />
                            </td>
                            <td>
                                <asp:ImageButton ID="ImageButton5" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                    ToolTip="Cancelar" onclick="ImageButton5_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table id="TblReporte" runat="server" visible="false" align="center">
                        <tr>
                            <td>
                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                    ForeColor="#333333" GridLines="Vertical" ShowHeaderWhenEmpty="True" HeaderStyle-CssClass="gridViewHeader"
                                    BorderStyle="Solid" HorizontalAlign="Center" Font-Names="Calibri" Font-Size="Small"
                                    AllowPaging="True" OnPageIndexChanging="GridView1_PageIndexChanging" >
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:BoundField HeaderText="Planeación" DataField="Planeación" />
                                        <asp:BoundField HeaderText="Id Auditoría" DataField="Id Auditoría" />
                                        <asp:BoundField HeaderText="Nombre Auditoría" DataField="Nombre Auditoría" />
                                        <asp:BoundField HeaderText="Programa / Estandar" DataField="Programa / Estandar" />
                                        <asp:BoundField HeaderText="Objetivo" DataField="Objetivo" />
                                        <asp:BoundField HeaderText="Id Hallazgo" DataField="Id Hallazgo" />
                                        <asp:BoundField HeaderText="Estado de la Recomendación" DataField="Estado de la Recomendación" />
                                        <asp:BoundField HeaderText="Responsable / Gerencia" DataField="Responsable / Gerencia" />
                                        <asp:BoundField HeaderText="Estado de la Auditoría" DataField="Estado de la Auditoría" />
                                        <asp:BoundField HeaderText="Nombre de Usuario" DataField="Nombre de Usuario" />
                                    </Columns>
                                    <EditRowStyle BackColor="#999999" />
                                    <FooterStyle BackColor="White" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#5D7B9D" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Left" />
                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="False" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table runat="server" align="center" id="Table1">
                                    <tr>
                                        <td>
                                            <asp:ImageButton ID="ImageButton2" runat="server" 
                                                ImageUrl="~/Imagenes/Icons/excel.png" ToolTip="Exportar a Excel" 
                                                onclick="ImageButton2_Click" />
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                                ToolTip="Cancelar" onclick="ImageButton4_Click" />
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
                    <td colspan="2" align="center" runat="server" id="tdCaption">
                        &nbsp;
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
    <Triggers>
        <asp:PostBackTrigger ControlID="BtnSelectAll" />
        <asp:PostBackTrigger ControlID="BtnSelectOne" />
        <asp:PostBackTrigger ControlID="BtnUnSelectAll" />
        <asp:PostBackTrigger ControlID="BtnUnSelectOne" />
        <asp:PostBackTrigger ControlID="ImageButton1" />
        <asp:PostBackTrigger ControlID="ImageButton2" />
    </Triggers>
</asp:UpdatePanel>
