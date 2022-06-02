<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ParametrizacionOT.ascx.cs"
    Inherits="ListasSarlaft.UserControls.Gestion.ParametrizacionOT" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControls/Sitio/OkMessageBox.ascx" TagPrefix="uc" TagName="OkMessageBox" %>
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
<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
    DeleteCommand="DELETE FROM [Gestion].[Tipos] WHERE [IdTipo] = @IdTipo" InsertCommand="INSERT INTO [Gestion].[Tipos] ([NombreTipo],[FechaRegistro]) VALUES (@NombreTipo, @FechaRegistro)"
    SelectCommand="SELECT [IdTipo], [NombreTipo], CONVERT(VARCHAR(10),[Tipos].[FechaRegistro],120) AS FechaRegistro
                    FROM [Gestion].[Tipos] WHERE [IdTipo] in (1,3)" UpdateCommand="UPDATE [Gestion].[Tipos] SET [NombreTipo] = @NombreTipo WHERE [IdTipo] = @IdTipo">
    <DeleteParameters>
        <asp:Parameter Name="IdTipo" Type="Int64" />
    </DeleteParameters>
    <InsertParameters>
        <asp:Parameter Name="NombreTipo" Type="String" />
        <asp:Parameter Name="FechaRegistro" Type="DateTime" />
    </InsertParameters>
    <UpdateParameters>
        <asp:Parameter Name="NombreTipo" Type="String" />
        <asp:Parameter Name="IdTipo" Type="Int64" />
    </UpdateParameters>
</asp:SqlDataSource>
<asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
    DeleteCommand="DELETE FROM [Gestion].[DetalleTipos] WHERE [IdDetalleTipo] = @IdDetalleTipo"
    InsertCommand="INSERT INTO [Gestion].[DetalleTipos] ([NombreDetalle],[FechaRegistro],[IdTipo]) VALUES (@NombreDetalle, @FechaRegistro, @IdTipo)"
    SelectCommand=" SELECT [IdDetalleTipo], [NombreDetalle], CONVERT(VARCHAR(10),[DetalleTipos].[FechaRegistro],120) AS FechaRegistro
                    FROM [Gestion].[DetalleTipos]
                    WHERE [IdTipo] = @IdTipo" UpdateCommand="UPDATE [Gestion].[DetalleTipos] SET [NombreDetalle] = @NombreDetalle WHERE [IdDetalleTipo] = @IdDetalleTipo">
    <SelectParameters>
        <asp:ControlParameter ControlID="txtId" Name="IdTipo" PropertyName="Text" Type="Int64" />
    </SelectParameters>
    <DeleteParameters>
        <asp:Parameter Name="IdDetalleTipo" Type="Int64" />
    </DeleteParameters>
    <InsertParameters>
        <asp:Parameter Name="NombreDetalle" Type="String" />
        <asp:Parameter Name="FechaRegistro" Type="DateTime" />
        <asp:Parameter Name="IdTipo" Type="Int64" />
    </InsertParameters>
    <UpdateParameters>
        <asp:Parameter Name="NombreDetalle" Type="String" />
        <asp:Parameter Name="IdDetalleTipo" Type="Int64" />
    </UpdateParameters>
</asp:SqlDataSource>
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
            <tr bgcolor="#333399">
                <td align="center">
                    <asp:Label ID="Label13" runat="server" ForeColor="White" Text="Parametrización Tipos"
                        Font-Bold="False" Font-Names="Calibri" Font-Size="Large"></asp:Label>
                </td>
            </tr>
            <tr id="filaGrid" runat="server" visible="true">
                <td>
                    <br />
                    <table>
                        <tr>
                            <td>
                                <asp:GridView ID="GridView1" runat="server" CellPadding="4" DataSourceID="SqlDataSource1"
                                    ForeColor="#333333" AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="IdTipo"
                                    OnSelectedIndexChanged="GridView1_SelectedIndexChanged" ShowHeaderWhenEmpty="True"
                                    OnRowCommand="GridView1_RowCommand" BorderStyle="Solid" GridLines="Vertical"
                                    HeaderStyle-CssClass="gridViewHeader" Font-Bold="False" Font-Names="Calibri"
                                    Font-Size="Small">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:BoundField DataField="IdTipo" HeaderText="Código" InsertVisible="False" ReadOnly="True"
                                            SortExpression="IdTipo">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="NombreTipo" HeaderText="Nombre del Tipo" SortExpression="NombreTipo"
                                            HtmlEncode="False">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="FechaRegistro" HeaderText="Fecha de Creación" SortExpression="FechaRegistro"
                                            ReadOnly="True">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Opciones">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnImgOpcion" runat="server" CausesValidation="False" CommandName="Select"
                                                    CommandArgument="Opcion" ImageUrl="~/Imagenes/Icons/Literal.png" Text="Opcion"
                                                    ToolTip="Opciones" />
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
                                    <SortedAscendingHeaderStyle BackColor="#506C8C" Font-Names="Rod" />
                                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                    <br />
                </td>
            </tr>
            <tr id="filaDetalle" runat="server" visible="false">
                <td>
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="Label1" runat="server" Text="Código:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtId" runat="server" Enabled="False" Width="70px" CssClass="Apariencia"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label2" runat="server" Text="Nombre:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtDescripicion" runat="server" Width="200px" CssClass="Apariencia"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label4" runat="server" Text="Fecha de Creación:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtFecha" runat="server" Width="100px" CssClass="Apariencia" Enabled="False"></asp:TextBox>
                            </td>
                        </tr>
                        <tr align="center">
                            <td colspan="2">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:ImageButton ID="btnImgInsertar" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                Visible="False" OnClick="btnImgInsertar_Click" ToolTip="Guardar" />
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="btnImgActualizar" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                Style="text-align: right" OnClick="btnImgActualizar_Click" ToolTip="Guardar" />
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="btnImgCancelar" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                                OnClick="btnImgCancelar_Click" ToolTip="Cancelar" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr id="filaOpcion" runat="server" visible="false">
                <td>
                    <table align="center">
                        <tr bgcolor="#333399" align="center">
                            <td>
                                <asp:Label ID="Label12" runat="server" ForeColor="White" Text="Detalle del Tipo"
                                    Font-Bold="False" Font-Names="Calibri" Font-Size="Large"></asp:Label>
                            </td>
                        </tr>
                        <tr align="center">
                            <td>
                                <br />
                                <table>
                                    <tr>
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label10" runat="server" Text="Tipo:" CssClass="Apariencia" Width="80px"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtTipo" runat="server" Enabled="false" CssClass="Apariencia" Width="200px"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                            </td>
                        </tr>
                        <tr align="center">
                            <td>
                                <br />
                                <table>
                                    <tr>
                                        <td>
                                            <asp:GridView ID="GridView2" runat="server" CellPadding="4" DataSourceID="SqlDataSource2"
                                                ForeColor="#333333" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
                                                OnSelectedIndexChanged="GridView2_SelectedIndexChanged" ShowHeaderWhenEmpty="True"
                                                HeaderStyle-CssClass="gridViewHeader" BorderStyle="Solid" GridLines="Vertical"
                                                CssClass="Apariencia" Font-Bold="False" DataKeyNames="IdDetalleTipo,NombreDetalle"
                                                OnRowCommand="GridView2_RowCommand" PageSize="5">
                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                <Columns>
                                                    <asp:BoundField DataField="IdDetalleTipo" HeaderText="Código" SortExpression="IdDetalleTipo"
                                                        InsertVisible="False" ReadOnly="True" Visible="True">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="NombreDetalle" HeaderText="Nombre" SortExpression="NombreDetalle"
                                                        InsertVisible="False" ReadOnly="True" Visible="True" HtmlEncode="False" HtmlEncodeFormatString="False">
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="FechaRegistro" HeaderText="Fecha de Creación" SortExpression="FechaRegistro">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField ShowHeader="False" HeaderText="Acción">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" CommandName="Select"
                                                                ImageUrl="~/Imagenes/Icons/edit.png" Text="Seleccionar" ToolTip="Editar" CommandArgument="SelectOp" />
                                                            <%--<asp:ImageButton ID="btnImgEliminarOpcion" runat="server" CausesValidation="False"
                                                                OnClick="btnImgEliminarOpcion_Click" CommandArgument="<%# Container.DataItemIndex %>"
                                                                ImageUrl="~/Imagenes/Icons/delete.png" Text="Eliminar" CommandName="Select" ToolTip="Eliminar" />--%>
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
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:ImageButton ID="imgBtnInsertarOpcion" runat="server" CausesValidation="False"
                                                CommandName="Insert" ImageUrl="~/Imagenes/Icons/Add.png" Text="Insert" OnClick="imgBtnInsertarOpcion_Click"
                                                ToolTip="Insertar" />
                                        </td>
                                    </tr>
                                </table>
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Button ID="btnVolverTipos" runat="server" Text="Volver a Tipos" OnClick="btnVolverTipos_Click"
                                    CausesValidation="False" Font-Bold="False" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr id="filaDetalleOpcion" runat="server" visible="false">
                <td>
                    <table align="center">
                        <tr bgcolor="#333399">
                            <td colspan="2" align="center">
                                <asp:Label ID="Label21" runat="server" ForeColor="White" Text="Detalle del Tipo"
                                    Font-Bold="False" Font-Names="Calibri" Font-Size="Large"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label16" runat="server" Text="Código:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCodigoOpcion" runat="server" Enabled="False" Visible="true" CssClass="Apariencia"
                                    TextMode="SingleLine" Width="70px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label17" runat="server" Text="Nombre:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtNombreOpcion" runat="server" Enabled="True" CssClass="Apariencia"
                                    Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label3" runat="server" Text="Fecha de Creación:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtFechaOpcion" runat="server" CssClass="Apariencia" Enabled="False"
                                    Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <table align="center" runat="server">
                                    <tr>
                                        <td>
                                            <asp:ImageButton ID="btnImgInsertarOp" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                Visible="False" OnClick="btnImgInsertarOp_Click" ToolTip="Guardar" />
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="btnImgActualizarOp" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                Style="text-align: right" OnClick="btnImgActualizarOp_Click" ToolTip="Guardar" />
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="btnImgCancelarOp" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                                OnClick="btnImgCancelarOp_Click" ToolTip="Cancelar" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <uc:OkMessageBox ID="omb" runat="server" />
        <asp:Panel ID="pnlMsgBox" runat="server" Width="400px" CssClass="modalPopup" Style="display: none;">
            <table width="100%" bgcolor="#EEEEEE">
                <tr>
                    <td colspan="2" align="center" runat="server" id="tdCaption">
                        <asp:Label ID="lblCaption" runat="server" Text="Atención" ForeColor="White" Font-Bold="False"
                            Font-Names="Tahoma" Font-Size="11px"></asp:Label><br />
                    </td>
                </tr>
                <tr>
                    <td style="width: 60px" valign="middle" align="center">
                        <asp:Image ID="imgInfo" runat="server" ImageUrl="~/Imagenes/Icons/icontexto-webdev-about.png" />
                    </td>
                    <td valign="middle">
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
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="GridView1" EventName="SelectedIndexChanged" />
    </Triggers>
</asp:UpdatePanel>
<%--The fnClick javascript function will force the btnOk button to post back. 
(to actually achieve this we need to add a little piece of code in 
the Page_Load method of the user control.--%>
<script type="text/javascript">
    function fnClick(sender, e) {
        __doPostBack(sender, e);
    }
</script>
