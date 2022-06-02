<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ParConPorcentajeCalificacion.ascx.cs"
    Inherits="ListasSarlaft.UserControls.Riesgos.ParConPorcentajeCalificacion" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControls/Sitio/OkMessageBox.ascx" TagPrefix="uc" TagName="OkMessageBox" %>
<link href="../../../Styles/AdminSitio.css" rel="stylesheet" type="text/css" />
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
        <uc:OkMessageBox ID="omb" runat="server" />
        <div id="BodyGridPC" class="ColumnStyle" runat="server">
            <Table class="tabla" align="center" width="100%">
                <tr align="center" bgcolor="#333399">
                <td>
                    <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Names="Calibri" Font-Size="Large"
                        ForeColor="White" Text="Porcentaje calificación control"></asp:Label>
                </td>
            </tr>
                        <tr align="center">
                            <td>
                                <asp:GridView ID="GVporcentajeCalificacion" runat="server" CellPadding="4"
                                    ForeColor="#333333" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
                                    ShowHeaderWhenEmpty="True"
                                    HeaderStyle-CssClass="gridViewHeader" BorderStyle="Solid" GridLines="Vertical"
                                    CssClass="Apariencia" Font-Bold="False" OnRowCommand="GVporcentajeCalificacion_RowCommand" OnPageIndexChanging="GVporcentajeCalificacion_PageIndexChanging"  >
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:BoundField DataField="intIdPorcentajeCalificarControl" HeaderText="Código" SortExpression="intIdPorcentajeCalificarControl" ItemStyle-HorizontalAlign="Center" />
                                        <asp:TemplateField HeaderText="Descripción Variable" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 150px">
                                                    <asp:Label ID="strNombrePorcentajeCalificarControl" runat="server" Text='<% # Bind("strNombrePorcentajeCalificarControl")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" HorizontalAlign="center" />
                                            <ItemStyle Wrap="false"  />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="intValorPorcentajeCalificarControl" HeaderText="Valor Porcentaje Calificación" SortExpression="intValorPorcentajeCalificarControl" ItemStyle-HorizontalAlign="Center" />
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
        <div id="BodyFormPC" class="ColumnStyle" runat="server" visible="false">
            <div id="form" class="TableContains">
        <table align="center" bgcolor="#EEEEEE">
            <tr align="center" bgcolor="#333399">
                <td>
                    <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Calibri" Font-Size="Large"
                        ForeColor="White" Text="Porcentaje calificación control"></asp:Label>
                </td>
            </tr>
            <tr align="center">
                <td>
                    <table>
                        <tr align="left">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="lblId" runat="server" Text="Codígo:" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                            <td bgcolor="#EEEEEE">
                                <asp:TextBox ID="txtId" runat="server" Width="300px" Font-Names="Calibri" 
                                    Font-Size="Small" Enabled="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr align="left">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="lblVariables" runat="server" Text="Variables a Calificar" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                            <td bgcolor="#EEEEEE">
                                <asp:DropDownList ID="ddlVariables" runat="server" Width="300">
                                    
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvVariabl" runat="server" ControlToValidate="ddlVariables"
                                    InitialValue="---" ForeColor="Red" ValidationGroup="validarCampos">*</asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator ID="rfvVariablUp" runat="server" ControlToValidate="ddlVariables"
                                    InitialValue="" ForeColor="Red" ValidationGroup="validarCampos">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr align="left">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="lblValor" runat="server" Text="Valor del Porcentaje:" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                            <td bgcolor="#EEEEEE">
                                <asp:TextBox ID="txtValorPorcentaje" runat="server" Width="300px" Font-Names="Calibri" 
                                    Font-Size="Small" MaxLength="3"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvValorPorcentaje" runat="server" ControlToValidate="txtValorPorcentaje"
                                    InitialValue="" ForeColor="Red" ValidationGroup="validarCampos">*</asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="revValorPorcentaje" runat="server" ControlToValidate="txtValorPorcentaje"
                                                ValidationExpression="^[0-9]{0,10}$" ForeColor="Red" ErrorMessage="Solo números Enteros" ToolTip="Solo números Enteros"
                                                ValidationGroup="validarCampos">*</asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr align="center">
                            <td colspan="2">
                                <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                    ToolTip="Guardar" ValidationGroup="validarCampos" CausesValidation ="true" 
                                    onclick="ImageButton2_Click" Visible="false"/>
                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                    ToolTip="Guardar" ValidationGroup="validarCampos" OnClick="ImageButton1_Click"  CausesValidation="true"
                                Visible="false"
                                    />
                                <asp:ImageButton ID="btnImgCancelar" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                    OnClick="btnImgCancelar_Click" ToolTip="Cancelar" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
                </div>
            </div>
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
</asp:UpdatePanel>
