<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="JerarquiaOrganizacional.ascx.cs"
    Inherits="ListasSarlaft.UserControls.Parametrizacion.JerarquiaOrganizacional" %>
<%@ Register Src="~/UserControls/Sitio/OkMessageBox.ascx" TagPrefix="uc" TagName="OkMessageBox" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <uc:OkMessageBox ID="omb" runat="server" />
        <asp:Panel ID="pnlMsgBox" runat="server" Width="400px" CssClass="modalPopup" Style="display: none;">
            <table width="100%">
                <tr class="topHandle">
                    <td colspan="2" align="center" runat="server" id="tdCaption">&nbsp;
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
                        <asp:Button ID="btnAceptar" runat="server" Text="Ok" OnClick="btnAceptar_Click" />
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
        <asp:Panel ID="Panel2" runat="server">
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" OnInserted="SqlDataSource1_On_Inserted"
                ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>" DeleteCommand="DELETE FROM [Parametrizacion].[JerarquiaOrganizacional] WHERE [idHijo] = @idHijo AND [idPadre] = @idPadre"
                InsertCommand="INSERT INTO [Parametrizacion].[JerarquiaOrganizacional] ([idPadre], [NombreHijo], [TipoArea], [FechaRegistro], [IdUsuario]) VALUES (@idPadre, @NombreHijo, @TipoArea, @FechaRegistro, @IdUsuario) SET @NewParameter=SCOPE_IDENTITY();"
                SelectCommand="SELECT [idHijo], [idPadre], [NombreHijo], [FechaRegistro], [IdUsuario] FROM [Parametrizacion].[JerarquiaOrganizacional]"
                UpdateCommand="UPDATE [Parametrizacion].[JerarquiaOrganizacional] SET [NombreHijo] = @NombreHijo, [TipoArea] = @TipoArea, [FechaRegistro] = @FechaRegistro, [IdUsuario] = @IdUsuario WHERE [idHijo] = @idHijo AND [idPadre] = @idPadre">
                <DeleteParameters>
                    <asp:Parameter Name="idHijo" Type="Int64" />
                    <asp:Parameter Name="idPadre" Type="Int64" />
                </DeleteParameters>
                <InsertParameters>
                    <asp:Parameter Name="idPadre" Type="Int64" />
                    <asp:Parameter Name="NombreHijo" Type="String" />
                    <asp:Parameter Name="TipoArea" Type="String" />
                    <asp:Parameter Name="FechaRegistro" Type="DateTime" />
                    <asp:Parameter Name="IdUsuario" Type="Int64" />
                    <asp:Parameter Direction="Output" Name="NewParameter" Type="Int32" />
                </InsertParameters>
                <UpdateParameters>
                    <asp:Parameter Name="NombreHijo" Type="String" />
                    <asp:Parameter Name="TipoArea" Type="String" />
                    <asp:Parameter Name="FechaRegistro" Type="DateTime" />
                    <asp:Parameter Name="IdUsuario" Type="Int64" />
                    <asp:Parameter Name="idHijo" Type="Int64" />
                    <asp:Parameter Name="idPadre" Type="Int64" />
                </UpdateParameters>
            </asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
                DeleteCommand="DELETE FROM [Parametrizacion].[DetalleJerarquiaOrg] WHERE [idDetalleJerarquiaOrg] = @idDetalleJerarquiaOrg"
                InsertCommand="INSERT INTO [Parametrizacion].[DetalleJerarquiaOrg] ([idHijo], [NombreResponsable], [CorreoResponsable], [CargoResponsable], [IdArea]) VALUES (@idHijo, @NombreResponsable, @CorreoResponsable, @CargoResponsable, @IdArea)"
                SelectCommand="SELECT [idDetalleJerarquiaOrg], [NombreResponsable], [CorreoResponsable], [CargoResponsable], [IdArea] FROM [Parametrizacion].[DetalleJerarquiaOrg] Where [idHijo] = @idHijo"
                UpdateCommand="UPDATE [Parametrizacion].[DetalleJerarquiaOrg] SET [NombreResponsable] = @NombreResponsable, [CorreoResponsable] = @CorreoResponsable, [CargoResponsable] = @CargoResponsable, [IdArea] = @IdArea  WHERE [idDetalleJerarquiaOrg] = @idDetalleJerarquiaOrg">
                <SelectParameters>
                    <asp:Parameter Name="idHijo" Type="Int64" />
                </SelectParameters>
                <DeleteParameters>
                    <asp:Parameter Name="idDetalleJerarquiaOrg" Type="Int64" />
                </DeleteParameters>
                <InsertParameters>
                    <asp:Parameter Name="idHijo" Type="Int64" />
                    <asp:Parameter Name="NombreResponsable" Type="String" />
                    <asp:Parameter Name="CorreoResponsable" Type="String" />
                    <asp:Parameter Name="CargoResponsable" Type="String" />
                    <asp:Parameter Name="idDetalleJerarquiaOrg" Type="Int64" />
                    <asp:Parameter Name="IdArea" Type="Int64" />
                </InsertParameters>
                <UpdateParameters>
                    <asp:Parameter Name="NombreResponsable" Type="String" />
                    <asp:Parameter Name="CorreoResponsable" Type="String" />
                    <asp:Parameter Name="CargoResponsable" Type="String" />
                    <asp:Parameter Name="idDetalleJerarquiaOrg" Type="Int64" />
                    <asp:Parameter Name="IdArea" Type="Int64" />
                </UpdateParameters>
            </asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
                SelectCommand="SELECT [IdArea],[NombreArea] FROM [Parametrizacion].[Area]"></asp:SqlDataSource>
            <table align="center">
                <tr>
                    <td>
                        <table align="center">
                            <tr>
                                <td>
                                    <asp:Panel ID="pnlJerarquia" runat="server">
                                        <tr align="center" bgcolor="#333399">
                                            <td>
                                                <asp:Label ID="Label6" runat="server" ForeColor="White" Text="Jerarquía Organizacional"
                                                    CssClass="AparienciaTitulo" Font-Bold="false"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table width="100%" border="1">
                                                    <tr>
                                                        <td>
                                                            <asp:Panel ID="Panel1" runat="server" Width="800px">
                                                                <asp:TreeView ID="TreeView1" ExpandDepth="6" runat="server" Font-Names="Calibri"
                                                                    Font-Bold="False" Font-Size="Small" LineImagesFolder="~/TreeLineImages" ForeColor="Black"
                                                                    ShowLines="True" OnSelectedNodeChanged="TreeView1_SelectedNodeChanged" Style="overflow: auto"
                                                                    Height="500px" BackColor="#FFFFFF" HoverNodeStyle-BorderStyle="Ridge" SelectedNodeStyle-BorderStyle="None">
                                                                    <SelectedNodeStyle BackColor="#FFFF99" BorderColor="#FFCC99" BorderStyle="Ridge" />
                                                                </asp:TreeView>
                                                            </asp:Panel>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr bgcolor="#EEEEEE" id="filaAccion" runat="server" visible="false">
                                            <td>
                                                <table align="center">
                                                    <tr>
                                                        <td>
                                                            <asp:ImageButton ID="btnImgEditar" runat="server" ImageUrl="~/Imagenes/Icons/edit.png"
                                                                OnClick="btnImgEditar_Click" EnableTheming="True" ToolTip="Editar" />
                                                        </td>
                                                        <td>
                                                            <asp:ImageButton ID="btnImgEliminar" runat="server" ImageUrl="~/Imagenes/Icons/delete.png"
                                                                OnClick="btnImgEliminar_Click" ToolTip="Eliminar" />
                                                        </td>
                                                        <td>
                                                            <asp:ImageButton ID="btnImgInsertar" runat="server" ImageUrl="~/Imagenes/Icons/Add.png"
                                                                OnClick="btnImgInsertar_Click" ToolTip="Insertar" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr bgcolor="#EEEEEE">
                    <td>
                        <asp:Panel ID="pnlDetalle" runat="server" Visible="false">
                            <table width="100%" align="center">
                                <tr>
                                    <td>
                                    <tr align="center" bgcolor="#5D7B9D">
                                        <td align="center" bgcolor="#333399">
                                            <asp:Label ID="Label13" runat="server" Text="Detalle" CssClass="AparienciaTitulo"
                                                ForeColor="White"></asp:Label>
                                        </td>
                                    </tr>
                                <tr bgcolor="#EEEEEE">
                                    <td>
                                        <table width="100%" class="tabla">
                                            <tr>
                                                <td align="left" bgcolor="#BBBBBB">
                                                    <asp:Label ID="Label1" runat="server" Text="Nivel Superior:" CssClass="Apariencia"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblNodoPadre" runat="server" Width="360px" CssClass="Apariencia"></asp:Label>
                                                    <asp:Label ID="lblIdHijo" runat="server" Text="Label" Visible="False"></asp:Label>
                                                    <asp:Label ID="lblIdPadre" runat="server" Text="Label" Visible="False"></asp:Label>
                                                    <asp:Label ID="lblIdNuevoHijo" runat="server" Text="Label" Visible="False"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" bgcolor="#BBBBBB">
                                                    <asp:Label ID="Label2" runat="server" Text="Nombre:" CssClass="Apariencia"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtNombreHijo" runat="server" Width="360px" CssClass="Apariencia"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNombreHijo"
                                                        ForeColor="Red" ValidationGroup="validarCampos">*</asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" bgcolor="#BBBBBB">
                                                    <asp:Label ID="Label7" runat="server" Text="Tipo Area:" CssClass="Apariencia"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlTipoArea" runat="server" CssClass="Apariencia" Width="200px"
                                                        OnDataBound="ddlTipoArea_DataBound">
                                                        <asp:ListItem Value="">---</asp:ListItem>
                                                        <asp:ListItem Value="A">Auditoría</asp:ListItem>
                                                        <asp:ListItem Value="R">Riesgos</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlTipoArea"
                                                        InitialValue="" ForeColor="Red" ValidationGroup="validarCampos">*</asp:RequiredFieldValidator>--%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" bgcolor="#BBBBBB">
                                                    <asp:Label ID="Label3" runat="server" Text="Nombre Responsable:" CssClass="Apariencia"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtNombreResponsable" runat="server" Width="360px" CssClass="Apariencia"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtNombreResponsable"
                                                        ForeColor="Red" ValidationGroup="validarCampos">*</asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" bgcolor="#BBBBBB">
                                                    <asp:Label ID="Label4" runat="server" Text="Correo Electrónico Responsable:" CssClass="Apariencia"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCorreoResponsable" runat="server" Width="360px" CssClass="Apariencia"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtCorreoResponsable"
                                                        ForeColor="Red" ValidationGroup="validarCampos">*</asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" bgcolor="#BBBBBB">
                                                    <asp:Label ID="Label5" runat="server" Text="Cargo Responsable:" CssClass="Apariencia"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCargoResponsable" runat="server" Width="360px" CssClass="Apariencia"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtCargoResponsable"
                                                        ForeColor="Red" ValidationGroup="validarCampos">*</asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" bgcolor="#BBBBBB">
                                                    <asp:Label ID="Label8" runat="server" Text="Area:" CssClass="Apariencia"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlArea" runat="server" CssClass="Apariencia" Width="200px"
                                                        DataSourceID="SqlDataSource3" DataTextField="NombreArea" DataValueField="IdArea"
                                                        AppendDataBoundItems="true">
                                                        <asp:ListItem Text="" Value="-1" Selected="True"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr bgcolor="#EEEEEE">
                                    <td>
                                        <table align="center">
                                            <tr>
                                                <td>
                                                    <asp:ImageButton ID="btnImgGrabar" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                        OnClick="btnImgGrabar_Click" ToolTip="Guardar" ValidationGroup="validarCampos" />
                                                </td>
                                                <td>
                                                    <asp:ImageButton ID="btnImgActualizar" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                        OnClick="btnImgActualizar_Click" ToolTip="Guardar" ValidationGroup="validarCampos" />
                                                </td>
                                                <td>
                                                    <asp:ImageButton ID="btnImgCancelar" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                                        ToolTip="Cancelar" OnClick="btnImgCancelar_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>

<script type="text/javascript">
    function fnClick(sender, e) {
        __doPostBack(sender, e);
    }
</script>
