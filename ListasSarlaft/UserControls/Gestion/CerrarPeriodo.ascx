<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CerrarPeriodo.ascx.cs"
    Inherits="ListasSarlaft.UserControls.Gestion.CerrarPeriodo" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%--<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>--%>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=14.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<%--<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>--%>
<link href="../../Styles/MastersPages.css" rel="stylesheet" type="text/css" />
<style type="text/css">
    #Background
    {
        position: fixed;
        top: 0px;
        bottom: 0px;
        left: 0px;
        right: 0px;
        overflow: hidden;
        padding: 0;
        margin: 0;
        background: #EEEEEE;
        filter: alpha(opacity=80);
        opacity: 0.8;
        z-index: 100000;
    }

    #Progress
    {
        position: fixed;
        top: 40%;
        left: 40%;
        height: 10%;
        width: 20%;
        z-index: 100001;
        background-color: #FFFFFF;
        border: 1px solid Gray;
        background-image: url('./Imagenes/Icons/loading.gif');
        background-repeat: no-repeat;
        background-position: center;
    }
</style>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <table align="center" bgcolor="#EEEEEE">
            <tr>
                <td align="center">
                    <asp:UpdateProgress ID="UpdateProgress4" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
                        DisplayAfter="0">
                        <ProgressTemplate>
                            <div id="Background">
                            </div>
                            <div id="Progress">
                                <asp:Label ID="Lbl11" runat="server" Text="Procesando, por favor espere..." Font-Names="Calibri"
                                    Font-Size="Small"></asp:Label>
                                <br />
                                <asp:Image ID="Img11" runat="server" ImageUrl="~/Imagenes/Icons/loading.gif" />
                            </div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                </td>
            </tr>
            <tr align="center" bgcolor="#333399">
                <td>
                    <asp:Label ID="Label7" runat="server" ForeColor="White" Text="Periodos Cerrados"
                        Font-Bold="False" Font-Names="Calibri" Font-Size="Large"></asp:Label>
                </td>
            </tr>
            <caption>
                <tr>
                    <td>
                        <br />
                        <table id="TableVision" runat="server" align="center">
                            <tr>
                                <td>
                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BorderStyle="Solid"
                                        CellPadding="4" Font-Names="Calibri" Font-Size="Small" ForeColor="#333333" GridLines="Vertical"
                                        HeaderStyle-CssClass="gridViewHeader" HorizontalAlign="Center" OnRowCommand="GridView1_RowCommand"
                                        ShowHeaderWhenEmpty="True">
                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                        <Columns>
                                            <asp:BoundField DataField="IdPeriodo" FooterStyle-HorizontalAlign="Center" FooterStyle-VerticalAlign="Middle"
                                                HeaderText="IdPeriodo" Visible="false">
                                                <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Mes" HeaderText="Mes" />
                                            <asp:BoundField DataField="Ano" HeaderText="Ano" />
                                            <asp:BoundField DataField="Usuario" HeaderText="Usuario" />
                                            <asp:BoundField DataField="FechaRegistro" HeaderText="Fecha Registro" />
                                            <asp:ButtonField ButtonType="Image" CommandName="Eliminar" HeaderText="Acción" ImageUrl="~/Imagenes/Icons/delete.png"
                                                Text="Eliminar">
                                                <HeaderStyle HorizontalAlign="Center" />
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
                            <tr align="center">
                                <td>
                                    <asp:ImageButton ID="BtnVerPeriodo" runat="server" ImageUrl="~/Imagenes/Icons/Add.png"
                                        OnClick="BtnVerPeriodo_Click" ToolTip="Agregar" />
                                </td>
                            </tr>
                        </table>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <table id="TbAddPeriodo" runat="server" align="center" visible="false">
                            <tr align="center" bgcolor="#333399">
                                <td colspan="2">
                                    <asp:Label ID="Label15" runat="server" Font-Bold="False" Font-Names="Calibri" Font-Size="medium"
                                        Text="Cerrar Periodo" ForeColor="White"></asp:Label>
                                </td>
                            </tr>
                            <tr align="left">
                                <td bgcolor="#BBBBBB">
                                    <asp:Label ID="Label1" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Periodo:"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="DropDownListMes" runat="server" Font-Names="Calibri" Font-Size="Small"
                                        Height="21px" Width="120px">
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
                                    <asp:CompareValidator ID="CompareValidatorMes" runat="server" ControlToValidate="DropDownListMes"
                                        ForeColor="Red" Operator="NotEqual" ValidationGroup="AddPeriodo" ValueToCompare="---">*</asp:CompareValidator>
                                    <asp:DropDownList ID="DropDownListAno" runat="server" Font-Names="Calibri" Font-Size="Small"
                                        Height="21px" Width="80px">
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
                                    <asp:CompareValidator ID="CompareValidatorAno" runat="server" ControlToValidate="DropDownListAno"
                                        ForeColor="Red" Operator="NotEqual" ValidationGroup="AddPeriodo" ValueToCompare="---">*</asp:CompareValidator>
                                </td>
                            </tr>
                            <tr align="left">
                                <td bgcolor="#BBBBBB">
                                    <asp:Label ID="Label10" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Usuario:"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBox1" runat="server" Enabled="false" Font-Names="Calibri" Font-Size="Small"
                                        Width="156px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr align="left">
                                <td bgcolor="#BBBBBB">
                                    <asp:Label ID="Label9" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Fecha Registro:"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBox2" runat="server" Enabled="false" Font-Names="Calibri" Font-Size="Small"
                                        Width="155px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr align="center">
                                <td colspan="2">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:ImageButton ID="BtnAddPeriodo" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                    OnClick="BtnAddPeriodo_Click" Style="height: 20px" ToolTip="Guardar" ValidationGroup="AddPeriodo" />
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="BtnNoAddPeriodo" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                                    OnClick="BtnNoAddPeriodo_Click" ToolTip="Cancelar" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </caption>
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
                    <td colspan="2" align="center" runat="server" id="tdCaption">&nbsp;
                        <asp:Label ID="lblCaption" runat="server" Text="Atención" Font-Names="Calibri" Font-Size="Small"
                            ForeColor="White"></asp:Label><br />
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
    </ContentTemplate>
</asp:UpdatePanel>
