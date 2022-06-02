<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EventosVsUsuario.ascx.cs" Inherits="ListasSarlaft.UserControls.Eventos.EventosVsUsuario" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Src="~/UserControls/Sitio/OkMessageBox.ascx" TagPrefix="uc" TagName="OkMessageBox" %>
<link href="../../../Styles/AdminSitio.css" rel="stylesheet" type="text/css" />
<asp:UpdatePanel ID="EVUbody" runat="server">
    <ContentTemplate>
        <uc:OkMessageBox ID="omb" runat="server" />
        <div class="TituloLabel" id="HeadEVU" runat="server">
                    <asp:Label ID="Ltitulo" runat="server" ForeColor="White" Text="Eventos Vs Usuario" Font-Bold="False"
                        Font-Names="Calibri" Font-Size="Large"></asp:Label>
            </div>
        <div id="BodyGridEVU" class="ColumnStyle" runat="server">
            <Table class="tabla" align="center" width="100%">
                        <tr align="center">
                            <td>
                                <asp:GridView ID="GVeventosUsuario" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                                ForeColor="#333333" GridLines="Vertical" ShowHeaderWhenEmpty="True" HeaderStyle-CssClass="gridViewHeader"
                                                BorderStyle="Solid" HorizontalAlign="Center" Font-Names="Calibri" Font-Size="Small"
                                                 AllowPaging="True" >
                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                <Columns>
                                                    
                                                    <asp:TemplateField HeaderText="Código" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 150px">
                                                    <asp:Label ID="strCodigoEvento" runat="server" Text='<% # Bind("strCodigoEvento")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" HorizontalAlign="center" />
                                            <ItemStyle Wrap="false"  />
                                        </asp:TemplateField>
                                                    <asp:BoundField HeaderText="Descripción" DataField="strDescripcionEvento" />
                                                    <asp:BoundField HeaderText="Clase" DataField="strNombreClaseEvento" />
                                                    <asp:BoundField HeaderText="Tipo de perdida" DataField="decCuantiaPerdida" />
                                                    <asp:BoundField HeaderText="Responsable Generación" DataField="strNombreGenerador" />
                                                    <asp:BoundField HeaderText="Responsable Solución" DataField="strNombreResponsable" />
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
                
            </Table>
        </div>
        </ContentTemplate>
    </asp:UpdatePanel>