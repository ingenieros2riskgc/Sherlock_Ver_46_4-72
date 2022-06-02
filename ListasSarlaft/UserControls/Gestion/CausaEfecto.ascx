<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CausaEfecto.ascx.cs"
    Inherits="ListasSarlaft.UserControls.Gestion.CausaEfecto" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
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
        <table id="Principal" align="center" bgcolor="#EEEEEE">
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
                <td colspan="3">
                    <asp:Label ID="Label7" runat="server" ForeColor="White" Text="Causa y Efecto" Font-Bold="False"
                        Font-Names="Calibri" Font-Size="Large"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <br />
                    <table id="FiltroPE" runat="server" align="center" visible="true">
                        <tr>
                            <td>
                                <asp:GridView ID="GridViewPlanEstratagico" runat="server" AutoGenerateColumns="False"
                                    BorderStyle="Solid" CellPadding="4" Font-Names="Calibri" Font-Size="Small" ForeColor="#333333"
                                    GridLines="Vertical" HeaderStyle-CssClass="gridViewHeader" HorizontalAlign="Center"
                                    OnRowCommand="GridViewPlanEstratagico_RowCommand" ShowHeaderWhenEmpty="True">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:BoundField DataField="IdPlan" HeaderText="Id" Visible="false" />
                                        <asp:BoundField DataField="CodigoPlan" HeaderText="Codigo" />
                                        <asp:BoundField DataField="Nombre" HeaderText="Plan Estratégico" />
                                        <asp:ButtonField ButtonType="Image" CommandName="Filtrar" HeaderText="" ImageUrl="~/Imagenes/Icons/select.png"
                                            Text="Filtrar">
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
                        </caption>
                    </table>
                    <table id="FiltroAplicado" runat="server" align="center" class="style3" visible="false">
                        <tr align="center" bgcolor="#5D7B9D">
                            <td colspan="2" bgcolor="#5D7B9D">
                                <asp:Label ID="Label24" runat="server" Text="Plan Estratégico" Font-Bold="False"
                                    Font-Names="Calibri" Font-Size="small" Style="font-weight: 700; color: #FFFFFF;"></asp:Label>
                            </td>
                        </tr>
                        <tr align="left">
                            <td>
                                <asp:Label ID="Label23" runat="server" Text="Id:" Font-Names="Calibri" Font-Size="Small"
                                    Visible="false"></asp:Label>
                            </td>
                            <td class="style6">
                                <asp:Label ID="LabelIdPlan" runat="server" Font-Names="Calibri" Font-Size="Small"
                                    Visible="false"></asp:Label>
                                <asp:Label ID="LabelFechaFin" runat="server" Font-Names="Calibri" Font-Size="Small"
                                    Visible="false"></asp:Label>
                            </td>
                        </tr>
                        <tr align="left">
                            <td bgcolor="#5D7B9D">
                                <asp:Label ID="Label19" runat="server" Text="Código:" Font-Names="Calibri" Font-Size="Small"
                                    Style="color: #FFFFFF; font-weight: 700;"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="LabelCodigoPlan" runat="server" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                        </tr>
                        <tr align="left">
                            <td bgcolor="#5D7B9D">
                                <asp:Label ID="Label22" runat="server" Text="Nombre:" Font-Names="Calibri" Font-Size="Small"
                                    Style="color: #FFFFFF; background-color: #5D7B9D"></asp:Label>
                            </td>
                            <td class="style6">
                                <asp:Label ID="LabelNombrePlan" runat="server" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="center">
                                <asp:Button ID="VerPlanEstrategico" runat="server" Font-Bold="False" Font-Names="Calibri"
                                    Font-Size="small" Text="Planes Estratégicos" OnClick="VerPlanEstrategico_Click"
                                    Width="126px" ToolTip="Ver Planes" />
                            </td>
                        </tr>
                    </table>
                    <br />
                    <br />
                </td>
            </tr>
            <tr>
                <td class="style27">
                    <table id="TbCausa" runat="server" align="center" visible="false">
                        <tr align="center">
                            <td class="style23">
                                <asp:Label ID="Label2" runat="server" Text="Objetivo(s) Causa" Font-Bold="False"
                                    Font-Names="Calibri" Font-Size="small" Style="font-weight: 700; color: #284775;"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="style23">
                                <asp:GridView ID="GridViewCausa" runat="server" AutoGenerateColumns="False" BorderStyle="Solid"
                                    CellPadding="4" Font-Names="Calibri" Font-Size="Small" ForeColor="#333333" GridLines="Vertical"
                                    HeaderStyle-CssClass="gridViewHeader" HorizontalAlign="Center" ShowHeaderWhenEmpty="True"
                                    Width="265px" OnRowCommand="GridViewCausa_RowCommand">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:BoundField DataField="IdObjetivo" HeaderText="Id" Visible="false" />
                                        <asp:BoundField DataField="CodigoObjetivo" HeaderText="Código" />
                                        <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                                        <asp:ButtonField ButtonType="Image" CommandName="FiltrarCausa" HeaderText="" ImageUrl="~/Imagenes/Icons/select.png"
                                            Text="FiltrarCausa">
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
                    <br />
                    <br />
                </td>
                <td class="style28" align="center">
                    <table id="TbObjSelecionado" runat="server" align="center" visible="false">
                        <tr align="left">
                            <td bgcolor="#5D7B9D" align="center">
                                <asp:Label ID="Label9" runat="server" Text="Código" Font-Names="Calibri" Font-Size="Small"
                                    Style="color: #FFFFFF; font-weight: 700;"></asp:Label>
                            </td>
                            <td bgcolor="#5D7B9D" align="center">
                                <asp:Label ID="Label10" runat="server" Text="Objetivo" Font-Names="Calibri" Font-Size="Small"
                                    Style="color: #FFFFFF; font-weight: 700;"></asp:Label>
                            </td>
                        </tr>
                        <tr align="left">
                            <td bgcolor="#F7F6F3" align="center">
                                <asp:Label ID="Label6" runat="server" Text="Código" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                            <td bgcolor="#F7F6F3" align="center">
                                <asp:Label ID="Label8" runat="server" Text="Objetivo" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <asp:Label ID="LabelAviso" runat="server" Font-Bold="False" Font-Names="Calibri"
                        Font-Size="Small" Style="font-weight: 700; color: #284775;" Text="Seleccione un objetivo Efecto"
                        Visible="false"></asp:Label>
                    <br />
                    <asp:Label ID="LabelAviso1" runat="server" Font-Bold="False" Font-Names="Calibri"
                        Font-Size="Small" Style="font-weight: 700; color: Red;" Text="Debe selecionar un objetivo Causa"
                        Visible="false"></asp:Label>
                    <br />
                    <asp:Label ID="LabelIdObjetivoCausa" runat="server" Font-Names="Calibri" Font-Size="Small"
                        Visible="false"></asp:Label>
                    <br />
                </td>
                <td class="style26">
                    <table id="TbEfecto" runat="server" align="center" visible="false">
                        <tr align="center">
                            <td>
                                <asp:Label ID="Label3" runat="server" Text="Objetivo(s) Efecto" Font-Bold="False"
                                    Font-Names="Calibri" Font-Size="small" Style="font-weight: 700; color: #284775;"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="GridViewEfecto" runat="server" AutoGenerateColumns="False" BorderStyle="Solid"
                                    CellPadding="4" Font-Names="Calibri" Font-Size="Small" ForeColor="#333333" GridLines="Vertical"
                                    HeaderStyle-CssClass="gridViewHeader" HorizontalAlign="Center" ShowHeaderWhenEmpty="True"
                                    Width="230px" OnRowCommand="GridViewEfecto_RowCommand">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:BoundField DataField="IdObjetivo" HeaderText="Id" Visible="false" />
                                        <asp:BoundField DataField="CodigoObjetivo" HeaderText="Código" />
                                        <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                                        <asp:ButtonField ButtonType="Image" CommandName="FiltrarEfecto" HeaderText="" ImageUrl="~/Imagenes/Icons/select.png"
                                            Text="FiltrarEfecto">
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
                    <br />
                    <br />
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <table id="TbCausaEfectoAsociados" runat="server" align="center" visible="false">
                        <tr align="center" bgcolor="#5D7B9D">
                            <td bgcolor="#333399">
                                <asp:Label ID="Label41" runat="server" Font-Bold="False" Font-Names="Calibri" Font-Size="small"
                                    Style="font-weight: 700; color: #FFFFFF;" Text="Objetivos Causa y Efecto Asociados"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="GridViewCausaEfecto" runat="server" AutoGenerateColumns="False"
                                    BorderStyle="Solid" CellPadding="4" Font-Names="Calibri" Font-Size="Small" ForeColor="#333333"
                                    GridLines="Vertical" HeaderStyle-CssClass="gridViewHeader" HorizontalAlign="Center"
                                    OnSelectedIndexChanged="GridView1_SelectedIndexChanged" OnRowCommand="GridViewCausaEfecto_RowCommand"
                                    ShowHeaderWhenEmpty="True">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:BoundField DataField="IdCausaEfecto" HeaderText="ID" Visible="false" />
                                        <asp:BoundField DataField="CodigoObjetivoC" HeaderText="Código Objetivo Causa" />
                                        <asp:BoundField DataField="DescripcionC" HeaderText="Descripción Objetivo Causa" />
                                        <asp:BoundField DataField="CodigoObjetivoE" HeaderText="Código Objetivo Efecto" />
                                        <asp:BoundField DataField="DescripcionE" HeaderText="Descripción Objetivo Efecto" />
                                        <asp:BoundField DataField="Usuario" HeaderText="Usuario" />
                                        <asp:BoundField DataField="FechaRegistro" HeaderText="Fecha Registro" Visible="False" />
                                        <asp:ButtonField ButtonType="Image" CommandName="EliminarCausaEfecto" HeaderText="Eliminar"
                                            ImageUrl="~/Imagenes/Icons/delete.png" Text="EliminarCausaEfecto">
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
                                <br />
                            </td>
                        </tr>
                        </caption>
                    </table>
                </td>
            </tr>
            </td> </tr>
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
                    <td colspan="2" align="center" runat="server" id="td1">
                        &nbsp;
                        <asp:Label ID="Label1" runat="server" Text="Atención" ForeColor="White" Font-Names="Calibri"
                            Font-Size="Small"></asp:Label><br />
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
