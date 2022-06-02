<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AgregarUsuario.ascx.cs"
    Inherits="ListasSarlaft.UserControls.AgregarUsuario" %>
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
<asp:SqlDataSource ID="SqlDataSource10" runat="server" ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
    SelectCommand="SELECT [IdMacroProceso], [Nombre] FROM [Procesos].[Macroproceso]">
</asp:SqlDataSource>
<asp:SqlDataSource ID="SqlDataSource11" runat="server" ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
    SelectCommand="SELECT [IdProceso], [Nombre] FROM [Procesos].[Proceso] WHERE [IdMacroProceso] = @IdMacroProceso">
    <SelectParameters>
        <asp:ControlParameter ControlID="ddlMacroProceso" Name="IdMacroProceso" PropertyName="SelectedValue"
            Type="Int32" />
    </SelectParameters>
</asp:SqlDataSource>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <table align="center" bgcolor="#EEEEEE">
            <tr align="center" bgcolor="#333399">
                <td>
                    <asp:Label ID="Label61" runat="server" ForeColor="White" Text="Registrar Usuario"
                        Font-Bold="False" Font-Names="Calibri" Font-Size="Large"></asp:Label>
                </td>
            </tr>
            <tr align="left">
                <td>
                    <table>
                        <tr>
                            <td bgcolor="#FFFFFF">
                                <asp:Label ID="Label1" runat="server" Text="Rol" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="DropDownList1" runat="server" Width="150px" Font-Names="Calibri"
                                    Font-Size="Small">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label123" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Jerarquía Organizacional"></asp:Label>
                            </td>
                            <td bgcolor="#EEEEEE">
                                <asp:TextBox ID="TextBox34" runat="server" Font-Names="Calibri" Font-Size="Small"
                                    Width="200px" Enabled="False"></asp:TextBox>
                                <asp:Label ID="lblIdDependencia1" runat="server" Visible="False" Font-Names="Calibri"
                                    Font-Size="Small"></asp:Label>
                                <asp:ImageButton ID="imgDependencia1" runat="server" ImageUrl="~/Imagenes/Icons/Organization-Chart.png"
                                    OnClientClick="return false;" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator57" runat="server" ControlToValidate="TextBox34"
                                    Display="Dynamic" ForeColor="Red" ValidationGroup="addUser">*</asp:RequiredFieldValidator>
                                <asp:PopupControlExtender ID="popupDependencia1" runat="server" DynamicServicePath=""
                                    Enabled="True" ExtenderControlID="" TargetControlID="imgDependencia1" BehaviorID="popup1"
                                    PopupControlID="pnlDependencia1" OffsetY="-200">
                                </asp:PopupControlExtender>
                                <asp:Panel ID="pnlDependencia1" runat="server" CssClass="popup" Width="400px" Style="display: none">
                                    <table width="100%" border="1" cellspacing="0" cellpadding="2" bordercolor="White">
                                        <tr align="right" bgcolor="#5D7B9D">
                                            <td>
                                                <asp:ImageButton ID="btnClosepp1" runat="server" ImageUrl="~/Imagenes/Icons/dialog-close2.png"
                                                    OnClientClick="$find('popup1').hidePopup(); return false;" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TreeView ID="TreeView1" ExpandDepth="3" runat="server" Font-Names="Calibri"
                                                    Font-Size="Small" LineImagesFolder="~/TreeLineImages" ForeColor="Black" ShowLines="True"
                                                    OnSelectedNodeChanged="TreeView1_SelectedNodeChanged" AutoGenerateDataBindings="False">
                                                    <SelectedNodeStyle BackColor="Silver" BorderColor="#66CCFF" BorderStyle="Inset" />
                                                </asp:TreeView>
                                            </td>
                                        </tr>
                                        <tr align="center">
                                            <td>
                                                <asp:Button ID="BtnOk1" runat="server" Text="Aceptar" CssClass="Apariencia" OnClientClick="$find('popup1').hidePopup(); return false;" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label2" runat="server" Text="Numero Documento" Font-Names="Calibri"
                                    Font-Size="Small"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox1" runat="server" MaxLength="15" Width="200px" Font-Names="Calibri"
                                    Font-Size="Small"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="addUser"
                                    ControlToValidate="TextBox1" Display="Dynamic" ForeColor="Red">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label3" runat="server" Text="Nombres" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox2" runat="server" MaxLength="40" Width="200px" Font-Names="Calibri"
                                    Font-Size="Small"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="addUser"
                                    ControlToValidate="TextBox2" Display="Dynamic" ForeColor="Red">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label4" runat="server" Text="Apellidos" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox3" runat="server" MaxLength="40" Width="200px" Font-Names="Calibri"
                                    Font-Size="Small"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="addUser"
                                    ControlToValidate="TextBox3" Display="Dynamic" ForeColor="Red">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label9" runat="server" Text="Ver todos los procesos:" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                            <td bgcolor="#EEEEEE">
                                <asp:CheckBox ID="CheckBox1" runat="server" Font-Names="Calibri" AutoPostBack="True"
                                    Font-Size="Small" oncheckedchanged="CheckBox1_CheckedChanged"/>
                            </td>
                        </tr>
                        <tr>
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label8" runat="server" Text="Macroproceso:" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                            <td bgcolor="#EEEEEE">
                                <asp:DropDownList ID="ddlMacroProceso" runat="server" Width="200px" Font-Names="Calibri" Font-Size="Small"
                                    OnDataBound="ddlMacroProceso_DataBound" DataSourceID="SqlDataSource10" DataTextField="Nombre"
                                    DataValueField="IdMacroProceso" AutoPostBack="True" OnSelectedIndexChanged="ddlMacroProceso_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label25" runat="server" Text="Proceso:" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                            <td bgcolor="#EEEEEE">
                                <asp:DropDownList ID="ddlProceso" runat="server" Width="200px" Font-Names="Calibri" Font-Size="Small"
                                    OnDataBound="ddlProceso_DataBound" AutoPostBack="True" DataSourceID="SqlDataSource11"
                                    DataTextField="Nombre" OnSelectedIndexChanged="ddlProceso_SelectedIndexChanged"
                                    DataValueField="IdProceso">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label5" runat="server" Text="Usuario" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox4" runat="server" MaxLength="20" Width="200px" Font-Names="Calibri"
                                    Font-Size="Small"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="addUser"
                                    ControlToValidate="TextBox4" Display="Dynamic" ForeColor="Red">*</asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="TextBox4ExpVal" runat="server" ControlToValidate="TextBox4"
                                    ValidationGroup="addUser"  ValidationExpression="^[0-9A-Za.-zÑñ]*$" ForeColor="Red">*</asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label7" runat="server" Text="Estado" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="DropDownList2" runat="server" Font-Names="Calibri" Font-Size="Small"
                                    Width="150px">
                                    <asp:ListItem Value="0">Activo</asp:ListItem>
                                    <asp:ListItem Value="1">Bloqueado</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr align="center">
                            <td colspan="3">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                ToolTip="Guardar" OnClick="Button1_Click" ValidationGroup="addUser" />
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                                ToolTip="Limpiar" OnClick="Button2_Click" />
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
        <asp:ModalPopupExtender ID="mpeMsgBox1" runat="server" TargetControlID="btndummy1"
            PopupControlID="pnlMsgBox1" OkControlID="btnAceptar1" BackgroundCssClass="FondoAplicacion"
            Enabled="True" DropShadow="true">
        </asp:ModalPopupExtender>
        <asp:Button ID="btndummy1" runat="server" Text="Button1" Style="display: none" />
        <asp:Panel ID="pnlMsgBox1" runat="server" Width="400px" Style="display: none;" BorderColor="#575757"
            BackColor="#FFFFFF" BorderStyle="Solid">
            <table width="100%">
                <tr class="topHandle" style="background-color: #5D7B9D">
                    <td colspan="2" align="center" runat="server" id="td1">
                        &nbsp;
                        <asp:Label ID="Label6" runat="server" Text="Atención" Font-Names="Calibri" Font-Size="Small"></asp:Label><br />
                    </td>
                </tr>
                <tr>
                    <td style="width: 60px" valign="middle" align="center">
                        <asp:Image ID="imgInfo1" runat="server" ImageUrl="~/Imagenes/Icons/Alerta.png" />
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
    </ContentTemplate>
</asp:UpdatePanel>
