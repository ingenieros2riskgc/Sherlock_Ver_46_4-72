<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OtrosTipos.ascx.cs" Inherits="ListasSarlaft.UserControls.Parametrizacion.OtrosTipos" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register Src="~/UserControls/Sitio/OkMessageBox.ascx" TagPrefix="uc" TagName="OkMessageBox" %>

<style type="text/css">
    .gridViewHeader a:link  
    {
     text-decoration:none
     
    }      
    .style1
    {
        width: 100%;
    }
</style>

<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
    DeleteCommand="DELETE FROM [Parametrizacion].[Tipos] WHERE [IdTipo] = @IdTipo"
    InsertCommand="INSERT INTO [Parametrizacion].[Tipos] ([NombreTipo],[FechaRegistro]) VALUES (@NombreTipo, @FechaRegistro)"
    SelectCommand="SELECT [IdTipo], [NombreTipo], CONVERT(VARCHAR(10),[Tipos].[FechaRegistro],103) AS FechaRegistro
                    FROM [Parametrizacion].[Tipos]"
    UpdateCommand="UPDATE [Parametrizacion].[Tipos] SET [NombreTipo] = @NombreTipo WHERE [IdTipo] = @IdTipo">
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
    DeleteCommand="DELETE FROM [Parametrizacion].[DetalleTipos] WHERE [IdDetalleTipo] = @IdDetalleTipo"
    InsertCommand="INSERT INTO [Parametrizacion].[DetalleTipos] ([NombreDetalle],[FechaRegistro],[IdTipo]) VALUES (@NombreDetalle, @FechaRegistro, @IdTipo)"
    SelectCommand=" SELECT [IdDetalleTipo], [NombreDetalle], CONVERT(VARCHAR(10),[DetalleTipos].[FechaRegistro],103) AS FechaRegistro
                    FROM [Parametrizacion].[DetalleTipos]
                    WHERE [IdTipo] = @IdTipo"
    UpdateCommand="UPDATE [Parametrizacion].[DetalleTipos] SET [NombreDetalle] = @NombreDetalle WHERE [IdDetalleTipo] = @IdDetalleTipo">
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
        <uc:OkMessageBox ID="omb" runat="server" /> 

        <asp:Panel ID="pnlMsgBox" runat="server" Width="400px" CssClass="modalPopup" Style="display: none;">
            <table width="100%">
                <tr class="topHandle">
                    <td colspan="2" align="center" runat="server" id="tdCaption">
                        <asp:Label ID="lblCaption" runat="server" Text="Atención" 
                            Font-Bold="False" Font-Names="Tahoma" Font-Size="11px"></asp:Label><br />
                    </td>
                </tr>
                <tr>
                    <td style="width: 60px" valign="middle" align="center" >
                        <asp:Image ID="imgInfo" runat="server" 
                            ImageUrl="~/Imagenes/Icons/icontexto-webdev-about.png"/>
                    </td>
                    <td valign="middle" align="left">
                        <asp:Label ID="lblMsgBox" runat="server" Font-Bold="False" Font-Names="Tahoma" 
                            Font-Size="11px"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="right" colspan="2">
                        <asp:Button ID="btnImgokEliminar" runat="server" Text="Ok" OnClick="btnImgokEliminar_Click" />
                        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" OnClientClick="$find('mypopup').hide(); return false;"/>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:ModalPopupExtender ID="mpeMsgBox" runat="server" PopupControlID="pnlMsgBox" BehaviorId="mypopup" 
            Enabled="True" TargetControlID="btndummy" BackgroundCssClass="modalBackground"  DropShadow="true" >
        </asp:ModalPopupExtender>
        <asp:Button ID="btndummy" runat="server" Text="Button" style="display:none"/>
        <asp:Label ID="lblOk" runat="server" Text="Label" Visible="False"></asp:Label>
        <asp:Label ID="lblCancelar" runat="server" Text="Label" Visible="False"></asp:Label>

        <table align="center"  width="100%">
            <tr align="center" bgcolor="#333399">
                <td>
                    <asp:Label ID="Label13" runat="server" ForeColor="White" Text="Otros Tipos" Font-Bold="False"
                        Font-Names="Calibri" Font-Size="Large"></asp:Label>
                </td>
            </tr>
            <tr align="center" bgcolor="#EEEEEE"  id="filaGrid" runat="server" visible="true" >
                <td bgcolor="#EEEEEE">
                    <br />
                    <table>
                        <tr >
                            <td>
                                <asp:GridView ID="GridView1" runat="server" CellPadding="4" DataSourceID="SqlDataSource1"
                                    ForeColor="#333333" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
                                    DataKeyNames="IdTipo" OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
                                    ShowHeaderWhenEmpty="True" onrowcommand="GridView1_RowCommand"
                                    HeaderStyle-CssClass="gridViewHeader" BorderStyle="Solid" GridLines="Vertical"
                                    CssClass="Apariencia" Font-Bold="False">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:BoundField DataField="IdTipo" HeaderText="Código" InsertVisible="False"
                                            ReadOnly="True" SortExpression="IdTipo" >
                                        <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="NombreTipo" HeaderText="Nombre del Tipo" 
                                            SortExpression="NombreTipo" HtmlEncode="False" >
                                        <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="FechaRegistro" HeaderText="Fecha de Creación" 
                                            SortExpression="FechaRegistro" ReadOnly="True" >
                                        <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Opciones">
                                            <ItemTemplate>
                                                    <asp:ImageButton ID="btnImgOpcion" runat="server" CausesValidation="False" CommandName="Select" CommandArgument = "Opcion"
                                                            ImageUrl="~/Imagenes/Icons/Literal.png" Text="Opcion" ToolTip="Opciones"/>                                                    
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Acción" ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" CommandName="Select" CommandArgument = "Seleccionar"
                                                    ImageUrl="~/Imagenes/Icons/edit.png" Text="Editar" ToolTip="Editar"/>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
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
                    </table>
                    <br />
                </td>
            </tr>
            <tr  align="left" id="filaDetalle" runat="server" visible="false">
                <td bgcolor="#EEEEEE">

                        <table  class = "tabla" width="100%">
                            <tr>
                                <td align="left" bgcolor="#BBBBBB">
                                    <asp:Label ID="Label1" runat="server" Text="Código:" CssClass="Apariencia"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtId" runat="server" Enabled="False" Width="70px" 
                                        CssClass="Apariencia"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" bgcolor="#BBBBBB">
                                    <asp:Label ID="Label2" runat="server" Text="Nombre:" CssClass="Apariencia"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDescripicion" runat="server" Width="300px" CssClass="Apariencia"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" bgcolor="#BBBBBB">
                                    <asp:Label ID="Label4" runat="server" Text="Fecha de Creación:" CssClass="Apariencia"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtFecha" runat="server" Width="100px" CssClass="Apariencia" 
                                        Enabled="False"></asp:TextBox>
                                </td>
                            </tr>
                            <tr align="center">
                                <td colspan="2">
                                    <table  class = "tablaSinBordes">
                                        <tr>
                                            <td>
                                                <asp:ImageButton ID="btnImgInsertar" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                    Visible="False" onclick="btnImgInsertar_Click" ToolTip="Guardar" 
                                                    Height="20px"/>
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="btnImgActualizar" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                    Style="text-align: right" onclick="btnImgActualizar_Click" 
                                                    ToolTip="Guardar"/>
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="btnImgCancelar" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                                    OnClick="btnImgCancelar_Click" ToolTip="Cancelar"/>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>

                </td>
            </tr>
            <tr id="filaOpcion" runat="server" visible="false">
                <td bgcolor="#EEEEEE">
                    <table width="100%"  class = "tabla">
                     <tr align="center" bgcolor="#333399">
                        <td>
                            <asp:Label ID="Label12" runat="server" ForeColor="White" Text="Detalle del Tipo" Font-Bold="False"
                                Font-Names="Calibri" Font-Size="Large"></asp:Label>
                        </td>
                    </tr>
                        <tr align="center" bgcolor="#EEEEEE">
                            <td>
                                <br />
                                <table   class = "tabla">
                                    <tr>
                                        <td align="left" bgcolor="#BBBBBB">
                                            <asp:Label ID="Label10" runat="server" Text="Tipo:" CssClass="Apariencia" Width="80px"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtTipo" runat="server" Enabled="false" CssClass="Apariencia" Width="300px"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                            </td>
                        </tr>
                        <tr align="center" bgcolor="#EEEEEE">
                            <td>
                                <br />
                                <table  class = "tablaSinBordes">
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
                                                    <asp:BoundField DataField="NombreDetalle" HeaderText="Nombre" 
                                                        SortExpression="NombreDetalle" InsertVisible="False"
                                                        ReadOnly="True" Visible="True" HtmlEncode="False" 
                                                        HtmlEncodeFormatString="False">
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="FechaRegistro" HeaderText="Fecha de Creación" SortExpression="FechaRegistro">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField ShowHeader="False" HeaderText="Acción">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" CommandName="Select"
                                                                ImageUrl="~/Imagenes/Icons/edit.png" Text="Seleccionar" ToolTip="Editar" CommandArgument="SelectOp" />
                                                            <asp:ImageButton ID="btnImgEliminarOpcion" runat="server" CausesValidation="False"
                                                                OnClick="btnImgEliminarOpcion_Click" CommandArgument="<%# Container.DataItemIndex %>"
                                                                ImageUrl="~/Imagenes/Icons/delete.png" Text="Eliminar" CommandName="Select" ToolTip="Eliminar" />
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
                                        <td align="right">
                                            <asp:ImageButton ID="imgBtnInsertarOpcion" runat="server" CausesValidation="False" CommandName="Insert"
                                                ImageUrl="~/Imagenes/Icons/Add.png" Text="Insert" OnClick="imgBtnInsertarOpcion_Click"
                                                ToolTip="Insertar" />
                                        </td>
                                    </tr>
                                </table>
                                <br />
                            </td>
                        </tr>
                        <tr align="center">
                            <td>
                                <asp:Button ID="btnVolverTipos" runat="server" Text="Volver a Tipos" OnClick="btnVolverTipos_Click"
                                    CausesValidation="False" Font-Bold="False" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr id="filaDetalleOpcion" runat="server" visible="false">
                <td bgcolor="#EEEEEE">
                   <table width="100%"  class = "tabla">
                    <tr align="center" bgcolor="#333399">
                        <td colspan="2">
                            <asp:Label ID="Label21" runat="server" ForeColor="White" Text="Detalle del Tipo" Font-Bold="False"
                                Font-Names="Calibri" Font-Size="Large"></asp:Label>
                        </td>
                    </tr>
                        <tr>
                            <td align="left" bgcolor="#BBBBBB">
                                <asp:Label ID="Label16" runat="server" Text="Código:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCodigoOpcion" runat="server" Enabled="False" Visible="true" 
                                    CssClass="Apariencia" TextMode="SingleLine"
                                    Width="70px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" bgcolor="#BBBBBB">
                                <asp:Label ID="Label17" runat="server" Text="Nombre:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtNombreOpcion" runat="server" Enabled="True" CssClass="Apariencia"
                                    Width="300px"  ></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" bgcolor="#BBBBBB">
                                <asp:Label ID="Label3" runat="server" Text="Fecha de Creación:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtFechaOpcion" runat="server" CssClass="Apariencia" Enabled="False"
                                    Width="300px"  ></asp:TextBox>
                            </td>
                        </tr>
                        <tr align="center">
                            <td colspan="2">
                                <table  class = "tablaSinBordes">
                                    <tr>
                                        <td>
                                            <asp:ImageButton ID="btnImgInsertarOp" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                Visible="False" OnClick="btnImgInsertarOp_Click" ToolTip="Guardar" />
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="btnImgActualizarOp" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                Style="text-align: right" OnClick="btnImgActualizarOp_Click" 
                                                ToolTip="Guardar" />
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
