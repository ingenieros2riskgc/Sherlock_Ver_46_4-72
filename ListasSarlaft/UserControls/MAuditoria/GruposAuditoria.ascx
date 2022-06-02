<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GruposAuditoria.ascx.cs" Inherits="ListasSarlaft.UserControls.MAuditoria.GruposAuditoria" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register Src="~/UserControls/Sitio/OkMessageBox.ascx" TagPrefix="uc" TagName="OkMessageBox" %>

<style type="text/css">
    .gridViewHeader a:link  
    {
     text-decoration:none;
    }      
    .style1
    {
        width: 100%;
    }
    .tablaS
{
    border-color: Gray;
    border:1;
    border-bottom-color: Gray;
    border-left-color: Gray;
    border-right-color: Gray;    
    vertical-align: middle;    
}
.tablaS tbody td
{
    border:hidden;
    border:0;
}
        .rootNode
        {
            font-size:18px;
            width:100%;
            border-bottom:Solid 1px black;
        }
        .leafNode
        {
            border:Dotted 2px Blue;
            padding:4px;
            background-color:#eeeeee;
            font-weight:bold;
        }
.tablaS tbody td     { border: solid black }
</style>

<asp:SqlDataSource ID="SqlDataSource1" runat="server" OnInserted="SqlDataSource1_On_Inserted" ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
    DeleteCommand="DELETE FROM [Auditoria].[GrupoAuditoria] WHERE [IdGrupoAuditoria] = @IdGrupoAuditoria"
    InsertCommand="INSERT INTO [Auditoria].[GrupoAuditoria] ([Nombre], [FechaRegistro], [IdUsuario]) VALUES (@Nombre, @FechaRegistro, @IdUsuario) SET @NewParameter=SCOPE_IDENTITY();"
    SelectCommand="SELECT [IdGrupoAuditoria], [Nombre], CONVERT(VARCHAR(10),[GrupoAuditoria].[FechaRegistro],120) AS FechaRegistro,[GrupoAuditoria].[IdUsuario], [Usuario]
                   FROM [Auditoria].[GrupoAuditoria], [Listas].[Usuarios]
                   WHERE 
                        [Usuarios].IdUsuario = [GrupoAuditoria].IdUsuario"
    
    
    UpdateCommand="UPDATE [Auditoria].[GrupoAuditoria] SET [Nombre] = @Nombre WHERE [IdGrupoAuditoria] = @IdGrupoAuditoria" 
    ProviderName="<%$ ConnectionStrings:SarlaftConnectionString.ProviderName %>">
    <DeleteParameters>
        <asp:Parameter Name="IdGrupoAuditoria" Type="Int64" />
    </DeleteParameters>
    <InsertParameters>
        <asp:Parameter Name="Nombre" Type="String" />
        <asp:Parameter Name="FechaRegistro" Type="DateTime" />
        <asp:Parameter Name="IdUsuario" Type="Decimal" />
        <asp:Parameter Direction="Output" Name="NewParameter" Type="Int32" />
    </InsertParameters>
    <UpdateParameters>
        <asp:Parameter Name="Nombre" Type="String" />
        <asp:Parameter Name="IdGrupoAuditoria" Type="Int64" />
    </UpdateParameters>
</asp:SqlDataSource>

<asp:SqlDataSource ID="SqlDataSource2" runat="server" OnInserted="SqlDataSource2_On_Inserted" 
    ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>" 
    DeleteCommand="DELETE FROM [Auditoria].[JerarquiaGrupoAuditoria] WHERE [idGrupoAuditoria] = @idGrupoAuditoria AND [idHijo] = @idHijo" 
    InsertCommand="INSERT INTO [Auditoria].[JerarquiaGrupoAuditoria] ([idGrupoAuditoria], [idHijo], [idPadre], [NombreHijoAuditoria], [FechaRegistro], [IdUsuario]) VALUES (@idGrupoAuditoria, @idHijo, @idPadre, @NombreHijoAuditoria, @FechaRegistro, @IdUsuario)" 
    SelectCommand="SELECT [idGrupoAuditoria], [idHijo], [idPadre], [NombreHijoAuditoria], [FechaRegistro], [IdUsuario] FROM [Auditoria].[JerarquiaGrupoAuditoria]" 
    
    UpdateCommand="UPDATE [Auditoria].[JerarquiaGrupoAuditoria] SET [NombreHijoAuditoria] = @NombreHijoAuditoria WHERE [idGrupoAuditoria] = @idGrupoAuditoria AND [idHijo] = @idHijo">
    <DeleteParameters>
        <asp:Parameter Name="idGrupoAuditoria" Type="Int64" />
        <asp:Parameter Name="idHijo" Type="Int64" />
    </DeleteParameters>
    <InsertParameters>
        <asp:Parameter Name="idGrupoAuditoria" Type="Int64" />
        <asp:Parameter Name="idHijo" Type="Int64" />
        <asp:Parameter Name="idPadre" Type="Int64" />
        <asp:Parameter Name="NombreHijoAuditoria" Type="String" />
        <asp:Parameter Name="FechaRegistro" Type="DateTime" />
        <asp:Parameter Name="IdUsuario" Type="Decimal" />
    </InsertParameters>
    <UpdateParameters>
        <asp:Parameter Name="NombreHijoAuditoria" Type="String" />
        <asp:Parameter Name="idGrupoAuditoria" Type="Int64" />
        <asp:Parameter Name="idHijo" Type="Int64" />
    </UpdateParameters>
</asp:SqlDataSource>

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <uc:OkMessageBox ID="omb" runat="server" /> 

         <asp:Panel ID="pnlDetalleNodo" runat="server" CssClass="popup" Width="400px" Style="display: none">
            <table width="100%" border="1" cellspacing="0" cellpadding="2" bordercolor="White">
                <tr align="right" bgcolor="#5D7B9D">
                    <td>
                        <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Imagenes/Icons/dialog-close2.png"
                            OnClientClick="$find('popup1').hidePopup(); return false;" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <table align="left" border="1" cellspacing="0" cellpadding="2" bordercolor="White">
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label56" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Insignificante:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label57" runat="server" Font-Names="Calibri" Font-Size="Small" Text="De poca importancia, no repercute negativamente en el Good Will de la empresa y puede acompañarse de perdidas financieras mínimas."></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label58" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Mínimo:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label59" runat="server" Font-Names="Calibri" Font-Size="Small" Text="De poca importancia, con leve impacto y puede acompañarse de perdidas financieras bajas."></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </asp:Panel>

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
        <asp:Label ID="lblIdGrupo" runat="server" Text="Label" Visible="False"></asp:Label>

        <table align="center"  width="100%">
            <tr align="center" bgcolor="#333399">
                <td>
                    <asp:Label ID="Label13" runat="server" ForeColor="White" Text="Grupos de Auditoría" Font-Bold="False"
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
                                    OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
                                    ShowHeaderWhenEmpty="True" onrowcommand="GridView1_RowCommand"
                                    HeaderStyle-CssClass="gridViewHeader" BorderStyle="Solid" GridLines="Vertical"
                                    CssClass="Apariencia" Font-Bold="False"
                                    DataKeyNames="IdUsuario,Usuario">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:BoundField DataField="IdGrupoAuditoria" HeaderText="Código" InsertVisible="False"
                                            ReadOnly="True" SortExpression="IdGrupoAuditoria" >
                                        <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Nombre" HeaderText="Nombre" 
                                            SortExpression="Nombre" HtmlEncode="False" />
                                        <asp:BoundField DataField="FechaRegistro" HeaderText="Fecha de Creación" 
                                            SortExpression="FechaRegistro" ReadOnly="True" >
                                        <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="IdUsuario" HeaderText="IdUsuario" 
                                            SortExpression="IdUsuario" Visible="False" >
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Usuario" HeaderText="Usuario" 
                                            SortExpression="Usuario"  Visible="False" >
                                        <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Jerarquía" ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imgBtnDetArbol" runat="server" CausesValidation="False" CommandName="Select"  CommandArgument = "DetalleArbol"
                                                    ImageUrl="~/Imagenes/Icons/group_edit.png" Text="Seleccionar" ToolTip="Jerarquía del Grupo"/>
                                             </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Acción" ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" CommandName="Select"  CommandArgument = "Editar"
                                                    ImageUrl="~/Imagenes/Icons/edit.png" Text="Seleccionar" ToolTip="Editar"/>
                                                <asp:ImageButton ID="btnImgEliminar" runat="server" CausesValidation="False" OnClick="btnImgEliminar_Click" CommandArgument="<%# Container.DataItemIndex %>"
                                                    ImageUrl="~/Imagenes/Icons/delete.png" Text="Eliminar" CommandName="Eliminar" ToolTip="Eliminar"/>
                                            </ItemTemplate>
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
                                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:ImageButton ID="imgBtnInsertar" runat="server" CausesValidation="False" CommandName="Insert"
                                    ImageUrl="~/Imagenes/Icons/Add.png" Text="Insert" 
                                    OnClick="imgBtnInsertar_Click" ToolTip="Insertar" />
                            </td>
                        </tr>
                    </table>
                    <br />
                </td>
            </tr>
            <tr  align="left" id="filaDetalle" runat="server" visible="false">
                <td bgcolor="#EEEEEE">

                        <table class="tabla" width="100%">
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
                                    <asp:TextBox ID="txtNombre" runat="server" Width="400px" CssClass="Apariencia"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" bgcolor="#BBBBBB">
                                    <asp:Label ID="Label3" runat="server" Text="Usuario:" CssClass="Apariencia"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtUsuario" runat="server" Width="100px" CssClass="Apariencia" 
                                        Enabled="False"></asp:TextBox>
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
                                    <table class="tablaSinBordes">
                                        <tr>
                                            <td>
                                                <asp:ImageButton ID="btnImgInsertar" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                    Visible="False" onclick="btnImgInsertar_Click" ToolTip="Guardar"/>
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
            <tr align="center" id="filaArbol" runat="server" visible="false">
                <td>
                    <table width="100%" align="center">
                        <tr bgcolor="#EEEEEE">
                            <td colspan="2">
                                <table align="center" class="tabla">
                                    <tr align="left">
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label5" runat="server" Text="Código:" CssClass="Apariencia"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCodGrupoA" runat="server" Width="70px" CssClass="Apariencia"
                                                Enabled="False"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr align="left">
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label6" runat="server" Text="Nombre:" CssClass="Apariencia"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtNomGrupoA" runat="server" Width="300px" CssClass="Apariencia"
                                                Enabled="False"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <table width="100%" border="1px">
                                    <tr valign="top">
                                        <td align="left">
                                            <asp:Panel ID="Panel1" runat="server" Width="200px">
                                                <asp:TreeView ID="TreeView1" ExpandDepth="6" runat="server" Font-Names="Calibri"
                                                    Font-Bold="False" Font-Size="Small" LineImagesFolder="~/TreeLineImages" ForeColor="Black"
                                                    ShowLines="True" OnSelectedNodeChanged="TreeView1_SelectedNodeChanged" BorderStyle="NotSet"
                                                    BorderWidth="0" HoverNodeStyle-BorderStyle="Ridge" SelectedNodeStyle-BorderStyle="None">
                                                    <SelectedNodeStyle BackColor="#CCCCCC" BorderColor="#CCCCCC" BorderStyle="None" />
                                                </asp:TreeView>
                                            </asp:Panel>
                                        </td>
                                        <td align="left">
                                            <asp:Panel ID="Panel2" runat="server" Width="200px">
                                                <asp:TreeView ID="TreeView2" ExpandDepth="6" runat="server" Font-Names="Calibri"
                                                    Font-Bold="False" Font-Size="Small" LineImagesFolder="~/TreeLineImages" ForeColor="Black"
                                                    ShowLines="True" OnSelectedNodeChanged="TreeView2_SelectedNodeChanged">
                                                    <SelectedNodeStyle BackColor="#CCCCCC" BorderColor="#CCCCCC" BorderStyle="None" />
                                                </asp:TreeView>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr align="center" bgcolor="#EEEEEE">
                            <td width="50%">
                                <asp:ImageButton ID="imgBtnAddHijoAud" runat="server" Enabled="True" ImageUrl="~/Imagenes/Icons/arrow_right.png"
                                    OnClick="imgBtnAddHijoAud_Click" ToolTip="Adicionar" />
                            </td>
                            <td width="50%">
                                <table class="tablaSinBordes">
                                    <tr>
                                        <td>
                                            <asp:ImageButton ID="imgBtnEditHijoAud" runat="server" CausesValidation="False" ImageUrl="~/Imagenes/Icons/edit.png"
                                                ToolTip="Editar" onclick="imgBtnEditHijoAud_Click" />
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="imgBtnDelHijoAud" runat="server" Enabled="True" ImageUrl="~/Imagenes/Icons/delete.png"
                                                OnClick="imgBtnDelHijoAud_Click" ToolTip="Eliminar" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr bgcolor="#EEEEEE">
                            <td align="center" colspan="2">
                                <asp:ImageButton ID="imgBtnCancelarDetalle" runat="server" Enabled="True" ImageUrl="~/Imagenes/Icons/cancel.png"
                                    OnClick="imgBtnCancelarDetalle_Click" ToolTip="Cancelar" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
           <tr  align="left" id="filaDetalleJGA" runat="server" visible="false">
                <td bgcolor="#EEEEEE">

                        <table class="tabla" width="100%">
                            <tr>
                                <td align="left" bgcolor="#BBBBBB">
                                    <asp:Label ID="Label7" runat="server" Text="Código:" CssClass="Apariencia"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCodJGA" runat="server" Enabled="False" Width="70px" 
                                        CssClass="Apariencia"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" bgcolor="#BBBBBB">
                                    <asp:Label ID="Label8" runat="server" Text="Nombre:" CssClass="Apariencia"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtNomJGA" runat="server" Width="400px" CssClass="Apariencia"></asp:TextBox>
                                </td>
                            </tr>
                            <tr align="center">
                                <td colspan="2">
                                    <table class="tablaSinBordes">
                                        <tr>
                                            <td>
                                                <asp:ImageButton ID="btnImgActualizarJGA" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                    Style="text-align: right" onclick="btnImgActualizarJGA_Click" 
                                                    ToolTip="Guardar"/>
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="btnImgCancelarJGA" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                                    OnClick="btnImgCancelarJGA_Click" ToolTip="Cancelar"/>
                                            </td>
                                        </tr>
                                    </table>
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


<%--The fnClick javascript function will force the btnOk button to post back. 
(to actually achieve this we need to add a little piece of code in 
the Page_Load method of the user control.--%>
<script type="text/javascript">
    function fnClick(sender, e) {
        __doPostBack(sender, e);
    }
</script>