<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Objetivo.ascx.cs" Inherits="ListasSarlaft.UserControls.MAuditoria.Objetivo" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register Src="~/UserControls/Sitio/OkMessageBox.ascx" TagPrefix="uc" TagName="OkMessageBox" %>

<style type="text/css">
    .ajax__html_editor_extender_texteditor {
        background-color: White;
        font-size: small;
        font-weight: lighter;
        font-family: Calibri;
        font-style: normal;
    }      
    .style1
    {
        width: 100%;
    }
    .gridViewHeader a:link  
    {
     text-decoration:none;
    }      
</style>

<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
    DeleteCommand="DELETE FROM [Auditoria].[Objetivo] WHERE [IdObjetivo] = @IdObjetivo"
    InsertCommand="INSERT INTO [Auditoria].[Objetivo] ([IdEstandar], [IdUsuario], [Nombre], [Descripcion], [FechaRegistro], [Numero]) VALUES (@IdEstandar, @IdUsuario, @Nombre, @Descripcion, @FechaRegistro, @Numero)"
    SelectCommand="SELECT [IdObjetivo], [Objetivo].[IdEstandar] AS IdEstandar, [Estandar].[Nombre] as NombreEstandar, [Objetivo].[IdUsuario], [Usuario],  [Objetivo].[Nombre], [Objetivo].[Descripcion], CONVERT(VARCHAR(10),[Objetivo].[FechaRegistro],120) AS FechaRegistro, Numero 
                   FROM   [Auditoria].[Objetivo], [Auditoria].[Estandar], [Listas].[Usuarios] 
                   WHERE  [Objetivo].[IdEstandar] = [Estandar].[IdEstandar] AND 
                          [Objetivo].[idUsuario] = [Usuarios].[idUsuario]
                   ORDER BY NombreEstandar, Numero"
    UpdateCommand="UPDATE [Auditoria].[Objetivo] SET [IdEstandar] = @IdEstandar, [Nombre] = @Nombre, [Descripcion] = @Descripcion WHERE [IdObjetivo] = @IdObjetivo">
    <DeleteParameters>
        <asp:Parameter Name="IdObjetivo" Type="Int32" />
    </DeleteParameters>
    <InsertParameters>
        <asp:Parameter Name="IdEstandar" Type="Int32" />
        <asp:Parameter Name="IdUsuario" Type="Int32" />
        <asp:Parameter Name="Nombre" Type="String" />
        <asp:Parameter Name="Descripcion" Type="String" />
        <asp:Parameter Name="FechaRegistro" Type="DateTime" />
        <asp:Parameter Name="Numero" Type="Int64" />
    </InsertParameters>
    <UpdateParameters>
        <asp:Parameter Name="IdEstandar" Type="Int32" />
        <asp:Parameter Name="Nombre" Type="String" />
        <asp:Parameter Name="Descripcion" Type="String" />
        <asp:Parameter Name="IdObjetivo" Type="Int32" />
    </UpdateParameters>
</asp:SqlDataSource>

<asp:SqlDataSource ID="SqlDataSource2" runat="server" 
    ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>" 
    SelectCommand="SELECT [IdEstandar], [Nombre] FROM [Auditoria].[Estandar]"></asp:SqlDataSource>

<asp:SqlDataSource ID="SqlDataSource3" runat="server" 
    ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>" 
    SelectCommand="SELECT COALESCE(Max([Numero]+1),1) AS Maximo FROM [Auditoria].[Objetivo] WHERE  [IdEstandar] = @IdEstandar">
    <SelectParameters>
        <asp:Parameter Name="IdEstandar" Type="Int64" />   
    </SelectParameters>
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
                        <asp:Label ID="Label13" runat="server" ForeColor="White" Text="Objetivo" Font-Bold="False"
                            Font-Names="Calibri" Font-Size="Large"></asp:Label>
                    </td>
                </tr>
            <tr align="center" bgcolor="#EEEEEE"  id="filaGrid" runat="server" visible="true">
                <td bgcolor="#EEEEEE">
                    <br />
                    <table>
                        <tr>
                            <td>
                                <asp:GridView ID="GridView1" runat="server" CellPadding="4" DataSourceID="SqlDataSource1"
                                    ForeColor="#333333" AllowPaging="True" AllowSorting="True" 
                                    AutoGenerateColumns="False" OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
                                    ShowHeaderWhenEmpty="True" HeaderStyle-CssClass="gridViewHeader" DataKeyNames="Usuario,IdEstandar,Descripcion,Numero,IdObjetivo"
                                    BorderStyle="Solid" GridLines="Vertical" CssClass="Apariencia" 
                                    Font-Bold="False" onrowcommand="GridView1_RowCommand">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:BoundField DataField="IdObjetivo" HeaderText="Código" InsertVisible="False" ReadOnly="True"
                                            SortExpression="IdObjetivo" Visible="false">
                                        <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="NombreEstandar" HeaderText="Estándar" 
                                            SortExpression="NombreEstandar" >
                                        <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Numero" HeaderText="Número" SortExpression="Numero"
                                            InsertVisible="False" ReadOnly="True" Visible="True" >
                                                <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="IdEstandar" HeaderText="IdEstandar" 
                                            SortExpression="IdEstandar" InsertVisible="False" ReadOnly="True" 
                                            Visible="False" />
                                        <asp:BoundField DataField="IdUsuario" HeaderText="IdUsuario" 
                                            SortExpression="IdUsuario" Visible="False" />
                                        <asp:BoundField DataField="Usuario" HeaderText="Usuario" 
                                            SortExpression="Usuario" Visible="False" />
                                        <asp:BoundField DataField="Nombre" HeaderText="Nombre" 
                                            SortExpression="Nombre" HtmlEncode="False" >
                                        <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Descripcion" HeaderText="Descripcion"  Visible="False" 
                                            SortExpression="Descripcion" HtmlEncode="False" />
                                        <asp:BoundField DataField="FechaRegistro" HeaderText="Fecha de Creación" 
                                            SortExpression="FechaRegistro">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:TemplateField ShowHeader="False" HeaderText="Acción">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" CommandName="Select" CommandArgument="Select"
                                                    ImageUrl="~/Imagenes/Icons/edit.png" Text="Seleccionar" ToolTip="Editar"/>
                                                <asp:ImageButton ID="btnImgEliminar" runat="server" CausesValidation="False" OnClick="btnImgEliminar_Click" CommandArgument="Eliminar"
                                                    ImageUrl="~/Imagenes/Icons/delete.png" Text="Eliminar" CommandName="Select" ToolTip="Eliminar"/>
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
                                            ImageUrl="~/Imagenes/Icons/Add.png" Text="Insert" OnClick="imgBtnInsertar_Click" ToolTip="Insertar"/>
                                    </td>
                                </tr>
                                </td>
                                </tr>
                    </table>
                    <br />
                </td>
            </tr>
            <tr align="left" id="filaDetalle" runat="server" visible="false">
                <td bgcolor="#EEEEEE">

                        <table class="tabla" width="100%">
                            <tr>
                                <td align="left" bgcolor="#BBBBBB">
                                    <asp:Label ID="Label1" runat="server" Text="Id:" CssClass="Apariencia"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtId" runat="server" Enabled="False" Width="70px" 
                                        CssClass="Apariencia" Visible="False"></asp:TextBox>
                                    <asp:TextBox ID="txtNumero" runat="server" Enabled="False" 
                                        CssClass="Apariencia" TextMode="SingleLine"
                                      Width="70px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" bgcolor="#BBBBBB">
                                    <asp:Label ID="Label3" runat="server" Text="Estándar:" CssClass="Apariencia"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlEstandar" runat="server" Width="300px" 
                                        CssClass="Apariencia" DataSourceID="SqlDataSource2" 
                                        DataTextField="Nombre" DataValueField="IdEstandar" 
                                        AppendDataBoundItems=True>
                                        <asp:ListItem Text="" Value="-1" Selected="True"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" bgcolor="#BBBBBB">
                                    <asp:Label ID="Label2" runat="server" Text="Nombre:" CssClass="Apariencia"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtNombre" runat="server" Width="300px" CssClass="Apariencia" 
                                        Rows="3"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" bgcolor="#BBBBBB">
                                    <asp:Label ID="Label6" runat="server" Text="Descripcion:" CssClass="Apariencia"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDescripcion" runat="server" Width="800px" CssClass="Apariencia"
                                        Columns="100" Rows="8" TextMode="MultiLine" Font-Size="10pt" Font-Bold="False"></asp:TextBox>
                                </td>
                                    <%--<asp:HtmlEditorExtender ID="htmlEDescripcion" runat="server"
                                        Enabled="True" TargetControlID="txtDescripcion">
                                        <Toolbar> 
                                                        <asp:Undo />
                                                        <asp:Redo />
                                                        <asp:Bold/>
                                                        <asp:Italic />
                                                        <asp:Underline />
                                                        <asp:StrikeThrough />
                                                        <asp:JustifyLeft />
                                                        <asp:JustifyCenter />
                                                        <asp:JustifyRight />
                                                        <asp:JustifyFull />
                                                        <asp:InsertOrderedList />
                                                        <asp:InsertUnorderedList />
                                                        <asp:CreateLink />
                                                        <asp:UnLink />
                                                        <asp:Delete />
                                                        <asp:Cut />
                                                        <asp:Copy />                                                       
                                                        <asp:Paste />
                                                        <asp:Indent />
                                                        <asp:Outdent />
                                                        <asp:InsertHorizontalRule />
                                                        <asp:HorizontalSeparator />
                                                    </Toolbar>
                                    </asp:HtmlEditorExtender>--%>
                            </tr>
                            <tr>
                                <td align="left" bgcolor="#BBBBBB">
                                    <asp:Label ID="Label4" runat="server" Text="Usuario:" CssClass="Apariencia"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtUsuario" runat="server" Width="100px" CssClass="Apariencia" 
                                        Enabled="False"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" bgcolor="#BBBBBB">
                                    <asp:Label ID="Label5" runat="server" Text="Fecha de Creación:" CssClass="Apariencia"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtFecha" runat="server" Width="100px" CssClass="Apariencia" 
                                        Enabled="False"></asp:TextBox>
                                </td>
                            </tr>

                            <tr align="center">
                                <td colspan="2">
                                    <table  class="tablaSinBordes">
                                        <tr>
                                            <td>
                                                <asp:ImageButton ID="btnImgInsertar" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                    Visible="False" onclick="btnImgInsertar_Click" ToolTip="Insertar" 
                                                    Height="20px"/>
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="btnImgActualizar" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                    Style="text-align: right" onclick="btnImgActualizar_Click" ToolTip="Actualizar"/>
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