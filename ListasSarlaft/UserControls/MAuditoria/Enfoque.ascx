<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Enfoque.ascx.cs" Inherits="ListasSarlaft.UserControls.MAuditoria.Enfoque" %>
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
    DeleteCommand="DELETE FROM [Auditoria].[Enfoque] WHERE [IdEnfoque] = @IdEnfoque"
    InsertCommand="INSERT INTO [Auditoria].[Enfoque] ([IdObjetivo], [Descripcion], [FechaRegistro], [IdUsuario], [Numero]) VALUES (@IdObjetivo, @Descripcion, @FechaRegistro, @IdUsuario, @Numero)"
    SelectCommand="SELECT [IdEnfoque], [IdObjetivo], [Descripcion], [Enfoque].[IdUsuario], [Usuarios].Usuario, CONVERT(VARCHAR(10),[Enfoque].[FechaRegistro],120) AS FechaRegistro, [Numero] 
                   FROM   [Auditoria].[Enfoque], [Listas].[Usuarios]
                   WHERE  [IdObjetivo]= @IdObjetivo AND
                          [Usuarios].IdUsuario = [Enfoque].IdUsuario"
    
    UpdateCommand="UPDATE [Auditoria].[Enfoque] SET [Descripcion] = @Descripcion WHERE [IdEnfoque] = @IdEnfoque">
    <SelectParameters>
        <asp:ControlParameter ControlID="ddlObjetivo" Name="IdObjetivo" PropertyName="SelectedValue" Type="Int32" />
    </SelectParameters>
    <DeleteParameters>
        <asp:Parameter Name="IdEnfoque" Type="Int64" />
    </DeleteParameters>
    <InsertParameters>
        <asp:Parameter Name="IdObjetivo" Type="Int64" />
        <asp:Parameter Name="Descripcion" Type="String" />
        <asp:Parameter Name="FechaRegistro" Type="DateTime" />
        <asp:Parameter Name="IdUsuario" Type="Int32" />
        <asp:Parameter Name="Numero" Type="Int32" />
    </InsertParameters>
    <UpdateParameters>
        <asp:Parameter Name="Descripcion" Type="String" />
        <asp:Parameter Name="IdEnfoque" Type="Int64" />
    </UpdateParameters>
</asp:SqlDataSource>

<asp:SqlDataSource ID="SqlDataSource2" runat="server" 
    ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>" 
    SelectCommand="SELECT [IdEstandar], [Nombre] FROM [Auditoria].[Estandar]">
</asp:SqlDataSource>

<asp:SqlDataSource ID="SqlDataSource3" runat="server" 
    ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>" 
    SelectCommand="SELECT [IdObjetivo], [Nombre] FROM [Auditoria].[Objetivo]  WHERE [IdEstandar] = @IdEstandar">
    <SelectParameters>
        <asp:ControlParameter ControlID="ddlEstandar" Name="IdEstandar" PropertyName="SelectedValue" Type="Int32" />
    </SelectParameters>
</asp:SqlDataSource>

<asp:SqlDataSource ID="SqlDataSource4" runat="server" 
    ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>" 
    DeleteCommand="DELETE FROM [Auditoria].[DetalleEnfoque] WHERE [IdDetalleEnfoque] = @IdDetalleEnfoque" 
    InsertCommand="INSERT INTO [Auditoria].[DetalleEnfoque] ([IdEnfoque], [Numero], [Descripcion], [FechaRegistro], [IdUsuario]) VALUES (@IdEnfoque, @Numero, @Descripcion, @FechaRegistro, @IdUsuario)" 
    SelectCommand="SELECT [IdDetalleEnfoque], [IdEnfoque], [Descripcion], [DetalleEnfoque].[IdUsuario], [Usuarios].Usuario, CONVERT(VARCHAR(10),[DetalleEnfoque].[FechaRegistro],120) AS FechaRegistro, [Numero]
                   FROM   [Auditoria].[DetalleEnfoque], [Listas].[Usuarios]
                   WHERE  [IdEnfoque] = @IdEnfoque AND
                          [Usuarios].IdUsuario = [DetalleEnfoque].IdUsuario" 
    UpdateCommand="UPDATE [Auditoria].[DetalleEnfoque] SET [Descripcion] = @Descripcion WHERE [IdDetalleEnfoque] = @IdDetalleEnfoque">
    <SelectParameters>
        <%--<asp:ControlParameter ControlID="GridView1" Name="IdEnfoque" PropertyName="SelectedValue" Type="Int64" />--%>
        <asp:ControlParameter ControlID="txtId" Name="IdEnfoque" PropertyName="Text" Type="Int64" />   
    </SelectParameters>
    <DeleteParameters>
        <asp:Parameter Name="IdDetalleEnfoque" Type="Int64" />
    </DeleteParameters>
    <InsertParameters>
        <asp:Parameter Name="IdEnfoque" Type="Int64" />
        <asp:Parameter Name="Numero" Type="Int32" />
        <asp:Parameter Name="Descripcion" Type="String" />
        <asp:Parameter Name="FechaRegistro" Type="DateTime" />
        <asp:Parameter Name="IdUsuario" Type="Int32" />
    </InsertParameters>
    <UpdateParameters>
        <asp:Parameter Name="IdEnfoque" Type="Int64" />
        <asp:Parameter Name="Descripcion" Type="String" />
        <asp:Parameter Name="IdDetalleEnfoque" Type="Int64" />
    </UpdateParameters>
</asp:SqlDataSource>

<asp:SqlDataSource ID="SqlDataSource5" runat="server" 
    ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>" 
    SelectCommand="SELECT COALESCE(Max([Numero]+1),1) AS Maximo FROM [Auditoria].[DetalleEnfoque] WHERE  [IdEnfoque] = @IdEnfoque">
    <SelectParameters>
        <asp:Parameter Name="IdEnfoque" Type="Int64" />   
    </SelectParameters>
</asp:SqlDataSource>
    
<asp:SqlDataSource ID="SqlDataSource6" runat="server" 
    ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>" 
    SelectCommand="SELECT COALESCE(Max([Numero]+1),1) AS Maximo FROM [Auditoria].[Enfoque] WHERE  [IdObjetivo] = @IdObjetivo">
    <SelectParameters>
        <asp:Parameter Name="IdObjetivo" Type="Int64" />   
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

        <table align="center" width="100%">
            <tr align="center" bgcolor="#333399">
                <td>
                    <asp:Label ID="Label13" runat="server" ForeColor="White" Text="Enfoque" Font-Bold="False"
                        Font-Names="Calibri" Font-Size="Large"></asp:Label>
                </td>
            </tr>
            <tr id="filaGrid" runat="server" visible="true">
                <td>
                    <table width="100%">
                        <tr align="center" bgcolor="#EEEEEE">
                            <td>
                                <br />
                                <table   class="tabla">
                                    <tr>
                                        <td align="left" bgcolor="#BBBBBB">
                                            <asp:Label ID="Label7" runat="server" Text="Estandar:" CssClass="Apariencia" Width="80px"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlEstandar" runat="server" CssClass="Apariencia" Width="300px"
                                                DataSourceID="SqlDataSource2" DataTextField="Nombre" DataValueField="IdEstandar"
                                                AutoPostBack="True" OnDataBound="ddlEstandar_DataBound" OnSelectedIndexChanged="ddlEstandar_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" bgcolor="#BBBBBB">
                                            <asp:Label ID="Label8" runat="server" Text="Objetivo:" CssClass="Apariencia" Width="80px"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlObjetivo" runat="server" CssClass="Apariencia" Width="300px"
                                                DataSourceID="SqlDataSource3" DataTextField="Nombre" DataValueField="IdObjetivo"
                                                AutoPostBack="True" OnDataBound="ddlObjetivo_DataBound" OnSelectedIndexChanged="ddlObjetivo_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                            </td>
                        </tr>
                        <tr align="center" bgcolor="#EEEEEE">
                            <td bgcolor="#EEEEEE">
                                <br />
                                <table>
                                    <tr>
                                        <td>
                                            <asp:GridView ID="GridView1" runat="server" CellPadding="4" DataSourceID="SqlDataSource1"
                                                ForeColor="#333333" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
                                                OnSelectedIndexChanged="GridView1_SelectedIndexChanged" ShowHeaderWhenEmpty="True"
                                                HeaderStyle-CssClass="gridViewHeader" BorderStyle="Solid" GridLines="Vertical"
                                                CssClass="Apariencia" Font-Bold="False" DataKeyNames="Usuario,IdEnfoque,IdObjetivo,Numero"
                                                OnRowCommand="GridView1_RowCommand">
                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                <Columns>
                                                    <asp:BoundField DataField="IdEnfoque" HeaderText="Código" SortExpression="IdEnfoque"
                                                        InsertVisible="False" ReadOnly="True" Visible="False">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Numero" HeaderText="Número" SortExpression="Numero"
                                                        InsertVisible="False" ReadOnly="True" Visible="True" >
                                                         <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="IdObjetivo" HeaderText="Código" SortExpression="IdObjetivo"
                                                        InsertVisible="False" ReadOnly="True" Visible="False" />
                                                    <asp:BoundField DataField="Descripcion" HeaderText="Descripción" SortExpression="Descripcion"
                                                        HtmlEncode="False" HeaderStyle-Width="620px" >
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="FechaRegistro" HeaderText="Fecha de Creación" SortExpression="FechaRegistro">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="IdUsuario" HeaderText="IdUsuario" SortExpression="IdUsuario"
                                                        Visible="False" />
                                                    <asp:BoundField DataField="Usuario" HeaderText="Usuario" SortExpression="Usuario"
                                                        Visible="False" />
                                                    <asp:TemplateField HeaderText="Literales">
                                                        <ItemTemplate>
                                                             <asp:ImageButton ID="btnImgLiteral" runat="server" CausesValidation="False" CommandName="Select" CommandArgument = "Literal"
                                                                        ImageUrl="~/Imagenes/Icons/Literal.png" Text="Literal" ToolTip="Literales"/>                                                    
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ShowHeader="False" HeaderText="Acción">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" CommandName="Select" CommandArgument = "Editar"
                                                                ImageUrl="~/Imagenes/Icons/edit.png" Text="Seleccionar" ToolTip="Editar" />
                                                            <asp:ImageButton ID="btnImgEliminar" runat="server" CausesValidation="False" OnClick="btnImgEliminar_Click"
                                                                CommandArgument="<%# Container.DataItemIndex %>" ImageUrl="~/Imagenes/Icons/delete.png"
                                                                Text="Eliminar" CommandName="Select" ToolTip="Eliminar" />
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
                                            <asp:ImageButton ID="imgBtnInsertar" runat="server" CausesValidation="False" CommandName="Insert"
                                                ImageUrl="~/Imagenes/Icons/Add.png" Text="Insert" OnClick="imgBtnInsertar_Click"
                                                ToolTip="Insertar" Visible="False" />
                                        </td>
                                    </tr>
                                </table>
                                <br />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr align="left" id="filaDetalle" runat="server" visible="false">
                <td bgcolor="#EEEEEE">
                    <table width="100%"  class="tabla">
                        <tr>
                            <td align="left" bgcolor="#BBBBBB">
                                <asp:Label ID="Label3" runat="server" Text="Estandar:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtEstandar" runat="server" Enabled="False" CssClass="Apariencia"
                                    TextMode="SingleLine" Width="400px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" bgcolor="#BBBBBB">
                                <asp:Label ID="Label4" runat="server" Text="Objetivo:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtObjetivo" runat="server" Enabled="False" CssClass="Apariencia"
                                    Width="400px" Rows="1"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" bgcolor="#BBBBBB">
                                <asp:Label ID="Label1" runat="server" Text="Número:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtId" runat="server" Enabled="True" CssClass="Apariencia" TextMode="SingleLine"
                                    Width="70px" Visible="False"></asp:TextBox>
                                <asp:TextBox ID="txtNumeroE" runat="server" Enabled="False" 
                                    CssClass="Apariencia" TextMode="SingleLine"
                                  Width="70px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" bgcolor="#BBBBBB">
                                <asp:Label ID="Label2" runat="server" Text="Nombre:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtDescripcion" runat="server" Width="800px" CssClass="Apariencia"
                                        Columns="100" Rows="8" TextMode="MultiLine" Font-Size="10pt" Font-Bold="False"></asp:TextBox>
                               <%-- <asp:HtmlEditorExtender ID="htmlEDescripcion" runat="server"
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
                            </td>
                        </tr>
                        <tr>
                            <td align="left" bgcolor="#BBBBBB">
                                <asp:Label ID="Label5" runat="server" Text="Usuario:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtUsuario" runat="server" Width="100px" CssClass="Apariencia" Enabled="False"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" bgcolor="#BBBBBB">
                                <asp:Label ID="Label6" runat="server" Text="Fecha de Creación:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtFecha" runat="server" Width="100px" CssClass="Apariencia" Enabled="False"></asp:TextBox>
                            </td>
                        </tr>
                        <tr align="center">
                            <td colspan="2">
                                <table  class="tablaSinBordes">
                                    <tr>
                                        <td>
                                            <asp:ImageButton ID="btnImgInsertar" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                Visible="False" OnClick="btnImgInsertar_Click" ToolTip="Guardar" />
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="btnImgActualizar" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                Style="text-align: right" OnClick="btnImgActualizar_Click" 
                                                ToolTip="Guardar" />
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
            <tr id="filaLiteral" runat="server" visible="false">
                <td bgcolor="#EEEEEE">
                    <table width="100%"  class="tabla">
                     <tr align="center" bgcolor="#5D7B9D">
                         <td>
                             <asp:Label ID="Label12" runat="server" Font-Bold="False" Font-Names="Calibri" Font-Size="Small"
                                 ForeColor="White" Text="Literales por Enfoque"></asp:Label>
                         </td>
                    </tr>
                        <tr align="center" bgcolor="#EEEEEE" >
                            <td>
                                <br />
                                <table  class="tabla">
                                    <tr align="left">
                                        <td align="left" bgcolor="#BBBBBB">
                                            <asp:Label ID="Label10" runat="server" Text="Estándar:" CssClass="Apariencia" 
                                                Width="80px"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtEstandarL" runat="server" Enabled="false" 
                                                CssClass="Apariencia" Width="400px"></asp:TextBox>
                                   </td>
                                    </tr>
                                    <tr align="left">
                                        <td align="left" bgcolor="#BBBBBB">
                                            <asp:Label ID="Label11" runat="server" Text="Objetivo:" CssClass="Apariencia" 
                                                Width="80px"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtObjetivoL" runat="server" Enabled="false" 
                                                CssClass="Apariencia" Width="400px"></asp:TextBox>
                                   </td>
                                    </tr>
                                    <tr align="left">
                                        <td align="left" bgcolor="#BBBBBB">
                                            <asp:Label ID="Label9" runat="server" Text="Enfoque:" CssClass="Apariencia" 
                                                Width="80px"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="txtEnfoque"  CssClass="Apariencia" Width="402px" runat="server" 
                                                ForeColor="#666666" BorderColor="Silver" BorderStyle="Ridge" BorderWidth="1px" Font-Bold="False" Height="18px"></asp:Label>
                                   </td>
                                    </tr>
                                </table>
                               <br />
                            </td>
                        </tr>
                        <tr align="center" bgcolor="#EEEEEE">
                            <td>
                                <br />
                                <table  class="tablaSinBordes">
                                    <tr>
                                        <td>
                                            <asp:GridView ID="GridView2" runat="server" CellPadding="4" DataSourceID="SqlDataSource4"
                                                ForeColor="#333333" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
                                                OnSelectedIndexChanged="GridView2_SelectedIndexChanged" ShowHeaderWhenEmpty="True"
                                                HeaderStyle-CssClass="gridViewHeader" BorderStyle="Solid" GridLines="Vertical"
                                                CssClass="Apariencia" Font-Bold="False" DataKeyNames="Usuario,IdDetalleEnfoque,IdEnfoque,Numero"
                                                OnRowCommand="GridView2_RowCommand" PageSize="5">
                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                <Columns>
                                                    <asp:BoundField DataField="IdDetalleEnfoque" HeaderText="Código" SortExpression="IdDetalleEnfoque"
                                                        InsertVisible="False" ReadOnly="True" Visible="False">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Numero" HeaderText="Número" SortExpression="Numero"
                                                        InsertVisible="False" ReadOnly="True" Visible="True" >
                                                         <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="IdEnfoque" HeaderText="Código" SortExpression="IdEnfoque"
                                                        InsertVisible="False" ReadOnly="True" Visible="False" />
                                                    <asp:BoundField DataField="Descripcion" HeaderText="Descripción" SortExpression="Descripcion"
                                                        HtmlEncode="False" HeaderStyle-Width="620px">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="FechaRegistro" HeaderText="Fecha de Creación" SortExpression="FechaRegistro">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="IdUsuario" HeaderText="IdUsuario" SortExpression="IdUsuario"
                                                        Visible="False" />
                                                    <asp:BoundField DataField="Usuario" HeaderText="Usuario" SortExpression="Usuario"
                                                        Visible="False" />
                                                    <asp:TemplateField ShowHeader="False" HeaderText="Acción">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" CommandName="Select"
                                                                ImageUrl="~/Imagenes/Icons/edit.png" Text="Seleccionar" ToolTip="Editar" CommandArgument="SelectDL"/>
                                                            <asp:ImageButton ID="btnImgEliminarLiteral" runat="server" CausesValidation="False"
                                                                OnClick="btnImgEliminarLiteral_Click" CommandArgument="<%# Container.DataItemIndex %>"
                                                                ImageUrl="~/Imagenes/Icons/delete.png" Text="Eliminar" CommandName="Select"
                                                                ToolTip="Eliminar" />
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
                                            <asp:ImageButton ID="imgBtnInsertarDL" runat="server" CausesValidation="False"
                                                CommandName="Insert" ImageUrl="~/Imagenes/Icons/Add.png" Text="Insert" OnClick="imgBtnInsertarDL_Click"
                                                ToolTip="Insertar" />
                                        </td>
                                    </tr>
                                </table>
                                <br />
                            </td>
                        </tr>
                        <tr align="center">
                            <td>
                                <asp:Button ID="btnVolverEnfoque" runat="server" Text="Volver a Enfoques" OnClick="btnVolverEnfoque_Click"
                                    CausesValidation="False" Font-Bold="False" />
                            </td>
                        </tr>
                    </table>
 
                </td>
            </tr>
            <tr id="filaDetalleLiteral" runat="server" visible="false">
                <td bgcolor="#EEEEEE">
                   <table width="100%"  class="tabla">
                                       <tr align="center" bgcolor="#5D7B9D">
                        <td colspan="2">
                            <asp:Label ID="Label78" runat="server" Font-Bold="False" Font-Names="Calibri" Font-Size="Small"
                                ForeColor="White" Text="Detalle del Literal"></asp:Label>
                        </td>

                    </tr>
                        <tr>
                            <td align="left" bgcolor="#BBBBBB">
                                <asp:Label ID="Label14" runat="server" Text="Estandar:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtEstandarDL" runat="server" Enabled="False" CssClass="Apariencia"
                                    TextMode="SingleLine" Width="400px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" bgcolor="#BBBBBB">
                                <asp:Label ID="Label15" runat="server" Text="Objetivo:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtObjetivoDL" runat="server" Enabled="False" CssClass="Apariencia"
                                    Width="400px" Rows="1"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" bgcolor="#BBBBBB">
                                <asp:Label ID="Label20" runat="server" Text="Enfoque:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="txtEnfoqueDL"  CssClass="Apariencia" Width="402px" runat="server" 
                                                ForeColor="#666666" BorderColor="Silver" BorderStyle="Ridge" BorderWidth="1px" Font-Bold="False" Height="18px"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" bgcolor="#BBBBBB">
                                <asp:Label ID="Label16" runat="server" Text="Número:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCodigoDL" runat="server" Enabled="False" Visible="false" 
                                    CssClass="Apariencia" TextMode="SingleLine"
                                    Width="70px"></asp:TextBox>
                                <asp:TextBox ID="txtNumeroDL" runat="server" Enabled="False" 
                                    CssClass="Apariencia" TextMode="SingleLine"
                                  Width="70px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" bgcolor="#BBBBBB">
                                <asp:Label ID="Label17" runat="server" Text="Descripcion:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtDescripcionDL" runat="server" Width="800px" CssClass="Apariencia"
                                        Columns="100" Rows="8" TextMode="MultiLine" Font-Size="10pt" Font-Bold="False"></asp:TextBox>
                                <%--<asp:HtmlEditorExtender ID="HtmlEditorExtender1" runat="server"
                                        Enabled="True" TargetControlID="txtDescripcionDL">
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
                            </td>
                        </tr>
                        <tr>
                            <td align="left" bgcolor="#BBBBBB">
                                <asp:Label ID="Label18" runat="server" Text="Usuario:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtUsuarioDL" runat="server" Width="100px" CssClass="Apariencia" Enabled="False"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" bgcolor="#BBBBBB">
                                <asp:Label ID="Label19" runat="server" Text="Fecha de Creación:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtFechaDL" runat="server" Width="100px" CssClass="Apariencia" Enabled="False"></asp:TextBox>
                            </td>
                        </tr>
                        <tr align="center">
                            <td colspan="2">
                                <table  class="tablaSinBordes">
                                    <tr>
                                        <td>
                                            <asp:ImageButton ID="btnImgInsertarDL" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                Visible="False" OnClick="btnImgInsertarDL_Click" ToolTip="Guardar" />
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="btnImgActualizarDL" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                Style="text-align: right" OnClick="btnImgActualizarDL_Click" 
                                                ToolTip="Guardar" />
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="btnImgCancelarDL" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                                OnClick="btnImgCancelarDL_Click" ToolTip="Cancelar" />
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
