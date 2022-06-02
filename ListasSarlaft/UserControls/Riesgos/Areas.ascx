<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Areas.ascx.cs" Inherits="ListasSarlaft.UserControls.Riesgos.Areas" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControls/Sitio/OkMessageBox.ascx" TagPrefix="uc" TagName="OkMessageBox" %>
<style type="text/css">
    .style1
    {
        width: 100%;
    }
    .gridViewHeader a:link
    {
        text-decoration: none;
    }
    .ajax__html_editor_extender_texteditor
    {
        background-color: White;
        font-size: small;
        font-weight: lighter;
        font-family: Calibri;
        font-style: normal;
    }
</style>
<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
    DeleteCommand="DELETE FROM [Parametrizacion].[Area] WHERE [IdArea] = @IdArea"
    InsertCommand="INSERT INTO [Parametrizacion].[Area] ([NombreArea], [IdUsuario], [FechaUltModificacion],[Codigo]) VALUES (@NombreArea, @IdUsuario, @FechaUltModificacion, @Codigo)"
    SelectCommand="SELECT [IdArea] ,[NombreArea], [IdUsuario], [FechaUltModificacion], [Codigo]  FROM [Parametrizacion].[Area]"
    UpdateCommand="UPDATE [Parametrizacion].[Area] SET [NombreArea] = @NombreArea, [IdUsuario] = @IdUsuario, [FechaUltModificacion] = @FechaUltModificacion, [Codigo] = @Codigo WHERE [IdArea] = @IdArea">
    <DeleteParameters>
        <asp:Parameter Name="IdArea" Type="Int64" />
    </DeleteParameters>
    <InsertParameters>
        <asp:Parameter Name="NombreArea" Type="String" />
        <asp:Parameter Name="IdUsuario" Type="Int64" />
        <asp:Parameter Name="FechaUltModificacion" Type="DateTime" />
        <asp:Parameter Name="Codigo" Type ="String" />
    </InsertParameters>
    <UpdateParameters>
        <asp:Parameter Name="NombreArea" Type="String" />
        <asp:Parameter Name="IdUsuario" Type="Int64" />
        <asp:Parameter Name="FechaUltModificacion" Type="DateTime" />
        <asp:Parameter Name="Codigo" Type ="String" />
        <asp:Parameter Name="IdArea" Type="Int64" />
    </UpdateParameters>
</asp:SqlDataSource>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <uc:OkMessageBox ID="omb" runat="server" />
        <asp:Panel ID="pnlMsgBox" runat="server" Width="400px" CssClass="modalPopup" Style="display: none;">
            <table width="100%">
                <tr class="topHandle">
                    <td colspan="2" align="center" runat="server" id="tdCaption">
                        <asp:Label ID="lblCaption" runat="server" Text="Atención" Font-Bold="False" Font-Names="Tahoma"
                            Font-Size="11px"></asp:Label><br />
                    </td>
                </tr>
                <tr>
                    <td style="width: 60px" valign="middle" align="center">
                        <asp:Image ID="imgInfo" runat="server" ImageUrl="~/Imagenes/Icons/icontexto-webdev-about.png" />
                    </td>
                    <td valign="middle" align="left">
                        <asp:Label ID="lblMsgBox" runat="server" Font-Bold="False" Font-Names="Tahoma" Font-Size="11px"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="right" colspan="2">
                        <asp:Button ID="btnImgokEliminar" runat="server" Text="Ok" OnClick="btnImgokEliminar_Click" />
                        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" OnClientClick="$find('mypopup').hide(); return false;" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:ModalPopupExtender ID="mpeMsgBox" runat="server" PopupControlID="pnlMsgBox"
            BehaviorID="mypopup" Enabled="True" TargetControlID="btndummy" BackgroundCssClass="modalBackground"
            DropShadow="true">
        </asp:ModalPopupExtender>
        <asp:Button ID="btndummy" runat="server" Text="Button" Style="display: none" />
        <asp:Label ID="lblOk" runat="server" Text="Label" Visible="False"></asp:Label>
        <asp:Label ID="lblCancelar" runat="server" Text="Label" Visible="False"></asp:Label>
        <table align="center" width="100%">
            <tr align="center" bgcolor="#333399">
                <td>
                    <asp:Label ID="Label13" runat="server" ForeColor="White" Text="Áreas" Font-Bold="False"
                        Font-Names="Calibri" Font-Size="Large"></asp:Label>
                </td>
            </tr>
            <tr align="center" bgcolor="#EEEEEE" id="filaGrid" runat="server" visible="true">
                <td bgcolor="#EEEEEE">
                    <br />
                    <table>
                        <tr>
                            <td>
                                <asp:GridView ID="GridView1" runat="server" CellPadding="4" DataSourceID="SqlDataSource1"
                                    ForeColor="#333333" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
                                    ShowHeaderWhenEmpty="True" CssClass="Apariencia" Font-Bold="False" HeaderStyle-CssClass="gridViewHeader"
                                    BorderStyle="Solid" GridLines="Vertical" OnRowCommand="GridView1_RowCommand"
                                    OnPageIndexChanging="GridView1_PageIndexChanging" 
                                    OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:BoundField DataField="IdArea" HeaderText="Id. Area" Visible="true" />
                                        <asp:BoundField DataField="NombreArea" HeaderText="Nombre Área" />
                                        <asp:BoundField DataField="Codigo" HeaderText="Codigo" 
                                            SortExpression="Codigo" />
                                        <asp:BoundField DataField="IdUsuario" HeaderText="Id. Usuario" Visible="false" />
                                        <asp:BoundField DataField="FechaUltModificacion" HeaderText="Fecha Modificación"
                                            Visible="false" />
                                        <asp:TemplateField ShowHeader="False" HeaderText="Acción">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" CommandName="Select"
                                                    CommandArgument="<%# Container.DataItemIndex %>" ImageUrl="~/Imagenes/Icons/edit.png" Text="Seleccionar" ToolTip="Editar" />
                                                <asp:ImageButton ID="btnImgEliminar" runat="server" CausesValidation="False" OnClick="btnEliminarArea_Click"
                                                    CommandArgument="<%# Container.DataItemIndex %>" ImageUrl="~/Imagenes/Icons/delete.png"
                                                    Text="Eliminar" CommandName="Eliminar" ToolTip="Eliminar" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
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
                                <tr>
                                    <td align="right">
                                        <asp:ImageButton ID="imgBtnInsertar" runat="server" CausesValidation="False" CommandName="Insert"
                                            ImageUrl="~/Imagenes/Icons/Add.png" Text="Insert" OnClick="BtnPreInsertar_Click"
                                            ToolTip="Insertar" />
                                    </td>
                                </tr>
                            </td>
                        </tr>
                    </table>
                    <br />
                </td>
            </tr>
            <tr align="left" id="trFilaDetalle" runat="server" visible="false">
                <td bgcolor="#EEEEEE">
                    <table class="tabla" width="100%">
                        <tr>
                            <td align="left" bgcolor="#BBBBBB">
                                <asp:Label ID="lblIdArea" runat="server" Text="Id:" Enabled="false" CssClass="Apariencia"
                                    Visible="false"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtIdArea" runat="server" Enabled="False" Width="70px" CssClass="Apariencia"
                                    Visible="false" MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" bgcolor="#BBBBBB">
                                <asp:Label ID="lblNombreArea" runat="server" Text="Nombre:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtNombreArea" runat="server" Width="500px" MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" bgcolor="#BBBBBB">
                                <asp:Label ID="Label1" runat="server" Text="Codigo:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TexCodigo" runat="server" Width="70px" CssClass="Apariencia" MaxLength="10"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" bgcolor="#BBBBBB">
                                <asp:Label ID="lblIdUsuario" runat="server" Text="Id Usuario:" Enabled="false" CssClass="Apariencia"
                                    Visible="false"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtIdUsuario" runat="server" Enabled="False" Width="70px" CssClass="Apariencia"
                                    Visible="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" bgcolor="#BBBBBB">
                                <asp:Label ID="lblFechaUltModificacion" runat="server" Text="Fecha Modificación:"
                                    Enabled="false" CssClass="Apariencia" Visible="false"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtFechaUltModificacion" runat="server" Enabled="False" Width="70px"
                                    CssClass="Apariencia" Visible="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr align="center">
                            <td colspan="2">
                                <table class="tablaSinBordes">
                                    <tr>
                                        <td>
                                            <asp:ImageButton ID="btnImgInsertar" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                Visible="False" OnClick="btnInsertarArea_Click" ToolTip="Guardar" />
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="btnImgActualizar" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                Style="text-align: right" OnClick="btnActualizarArea_Click" ToolTip="Actualizar" />
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="btnImgCancelar" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                                OnClick="btnCancelar_Click" ToolTip="Cancelar" />
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
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="GridView1" EventName="SelectedIndexChanged" />
    </Triggers>
</asp:UpdatePanel>
<script type="text/javascript">
    function fnClick(sender, e) {
        __doPostBack(sender, e);
    }
</script>