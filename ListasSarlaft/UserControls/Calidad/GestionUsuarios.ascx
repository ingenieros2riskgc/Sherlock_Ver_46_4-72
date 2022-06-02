<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GestionUsuarios.ascx.cs"
    Inherits="ListasSarlaft.UserControls.Calidad.GestionUsuarios" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<style type="text/css">
    .gridViewHeader a:link {
        text-decoration: none;
    }

    .FondoAplicacion {
        background-color: Gray;
        filter: alpha(opacity=80);
        opacity: 0.8;
    }

    .scrollingControlContainer {
        overflow-x: hidden;
        overflow-y: scroll;
    }

    .scrollingCheckBoxList {
        border: 1px #808080 solid;
        margin: 10px 10px 10px 10px;
        height: 200px;
    }

    .popup {
        border: Silver 1px solid;
        color: #060F40;
        background: #ffffff;
    }

    .no-visible {
        display: none
    }
    .auto-style1 {
        height: 30px;
    }
</style>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <table align="center" bgcolor="#EEEEEE">
            <tr align="center" bgcolor="#333399">
                <td>
                    <asp:Label ID="Label61" runat="server" ForeColor="White" Text="Gestión de grupos"
                        Font-Bold="False" Font-Names="Calibri" Font-Size="Large" Width="400px"></asp:Label>
                </td>
            </tr>
        </table>
        <br />
        <table align="center">
        <tr align="center">
            <td style="margin-left: 40px">
                <asp:GridView ID="gvGrupoTrabajo" runat="server" CellPadding="4" EnableModelValidation="True" 
                    ForeColor="#333333" GridLines="Vertical" AutoGenerateColumns="False" AllowPaging="False"
                    OnPageIndexChanging="gvGrupoTrabajo_PageIndexChanging" OnRowCommand="gvGrupoTrabajo_RowCommand" 
                    ShowHeaderWhenEmpty="True" BorderStyle="Solid"
                    HorizontalAlign="Center" Font-Names="Calibri" Font-Size="Small" >
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775"></AlternatingRowStyle>
                    <Columns>
                        <asp:BoundField DataField="idGrupoSoporte" HeaderText="IdEvidencia" Visible="False"></asp:BoundField>
                        <asp:BoundField DataField="NombreGrupoSoporte" HeaderText="Nombre del grupo" ItemStyle-HorizontalAlign="Center" ></asp:BoundField>
                        <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Icons/edit.png" Text="Modificar" HeaderText="Modificar"
                            CommandName="Modificar" ItemStyle-HorizontalAlign="Center"></asp:ButtonField>
                        <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Icons/select.png" Text="Integrantes" HeaderText="Integrantes"
                            CommandName="Integrantes" ItemStyle-HorizontalAlign="Center"></asp:ButtonField>
                    </Columns>
                        <EditRowStyle BackColor="#999999" />
                        <FooterStyle BackColor="White" Font-Bold="True" ForeColor="White"></FooterStyle>
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" CssClass="gridViewHeader"></HeaderStyle>
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center"></PagerStyle>
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Left"></RowStyle>
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333"></SelectedRowStyle>
                        <SortedAscendingCellStyle BackColor="#E9E7E2"></SortedAscendingCellStyle>
                        <SortedAscendingHeaderStyle BackColor="#506C8C"></SortedAscendingHeaderStyle>
                        <SortedDescendingCellStyle BackColor="#FFFDF8"></SortedDescendingCellStyle>
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE"></SortedDescendingHeaderStyle>
                </asp:GridView>
            </td>
        </tr>
        </table>
        <br />
        <table align="center">
            <tr>
                <td >
                    <asp:ImageButton ID="imgBtnInsertar" runat="server" CausesValidation="False" CommandName="Insert"
                        ImageUrl="~/Imagenes/Icons/Add.png" Text="Insert"
                        OnClick="imgBtnInsertar_Click" ToolTip="Insertar" />
                </td>
            </tr>
        </table>
        <br />
        <div id="divDatoGrupo" runat="server" visible="false" class="ColumnStyle">
        <table align="center">
         <tr class="visible">
                     <td align="left" bgcolor="#BBBBBB">
                         <asp:Label ID="lblNuevoNombreGrupo" runat="server" Text="Nombre:" CssClass="Apariencia" Width="120px" align="center"></asp:Label>
                     </td>
                     <td>
                         <asp:TextBox ID="StrNombreGrupoTrabajo" runat="server" Width="300px" CssClass="Apariencia"></asp:TextBox>
                     </td>
          </tr>
         </table>
            </div>
        <table align="center">
            <tr>
                <td class="auto-style1">
                    <asp:ImageButton ID="btnRegGrupo" runat="server" CausesValidation="False" CommandName="Insert"
                        ImageUrl="~/Imagenes/Icons/guardar.png" Text="Insert"
                        OnClick="imgbtnRegGrupo_Click" ToolTip="Insertar" />
                    <asp:ImageButton ID="btnUpdGrupo" runat="server" CausesValidation="False" CommandName="Update"
                        ImageUrl="~/Imagenes/Icons/guardar.png" Text="Insert"
                        OnClick="imgbtnUpdGrupo_Click" ToolTip="Update" />
                    <asp:ImageButton ID="btnNoModGrupo" runat="server" CausesValidation="False" CommandName="Insert"
                        ImageUrl="~/Imagenes/Icons/cancel.png" Text="Insert"
                        OnClick="imgbtnNoModGrupo_Click" ToolTip="Insertar" />
                </td>
            </tr>
        </table>
        <br />
    <div id="divUsuarios" runat="server" visible="false" class="ColumnStyle">
        <table align="center" bgcolor="#EEEEEE">
            <tr align="center" bgcolor="#333399">
                <td>
                    <asp:Label ID="Label1" runat="server" ForeColor="White" Text="Usuarios" 
                        Font-Bold="False" Font-Names="Calibri" Font-Size="Large" Width="400px"></asp:Label>
                </td>
            </tr>
        </table>
        <br />
    <table align="center">
        <tr align="center">
            <td>
                <asp:GridView ID="gvIntegrantes" runat="server" AutoGenerateColumns="False" CellPadding="4"
                    ForeColor="#333333" GridLines="Vertical" ShowHeaderWhenEmpty="True" BorderStyle="Solid"
                    HorizontalAlign="Center" Font-Names="Calibri" Font-Size="Small" OnRowCommand="gvIntegrantes_RowCommand">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775"></AlternatingRowStyle>
                    <Columns>
                        <asp:BoundField DataField="StrIdIntegrante" HeaderText="Id Integrante" Visible="False"></asp:BoundField>
                        <asp:BoundField DataField="StrNombreIntegrante" HeaderText="Nombre del integrante" ItemStyle-HorizontalAlign="Center" ></asp:BoundField>
                        <asp:BoundField DataField="StrUsuarioIntegrante" HeaderText="Usuario" Visible="true" ItemStyle-HorizontalAlign="Center" ></asp:BoundField>
                        <asp:BoundField DataField="StrCorreoIntegrante" HeaderText="Correo" Visible="true" ItemStyle-HorizontalAlign="Center" ></asp:BoundField>
                        <asp:BoundField DataField="StrIdGrupoTrabajo" HeaderText="Id del grupo" ItemStyle-HorizontalAlign="Center" Visible="false" ></asp:BoundField>
                        <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Icons/edit.png" Text="Modificar" HeaderText="Modificar"
                            CommandName="Modificar" ItemStyle-HorizontalAlign="Center"></asp:ButtonField>
                        <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Icons/delete.png" Text="Eliminar" HeaderText="Eliminar"
                            CommandName="Eliminar" ItemStyle-HorizontalAlign="Center"></asp:ButtonField>
                    </Columns>
                        <EditRowStyle BackColor="#999999" />
                        <FooterStyle BackColor="White" Font-Bold="True" ForeColor="White"></FooterStyle>
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" CssClass="gridViewHeader"></HeaderStyle>
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center"></PagerStyle>
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Left"></RowStyle>
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333"></SelectedRowStyle>
                        <SortedAscendingCellStyle BackColor="#E9E7E2"></SortedAscendingCellStyle>
                        <SortedAscendingHeaderStyle BackColor="#506C8C"></SortedAscendingHeaderStyle>
                        <SortedDescendingCellStyle BackColor="#FFFDF8"></SortedDescendingCellStyle>
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE"></SortedDescendingHeaderStyle>
                </asp:GridView>
            </td>
        </tr>
        </table>
        </div>
        <br />
        <table align="center">
            <tr>
                <td >
                    <asp:ImageButton ID="imgBtnInsertarIntegrante" runat="server" CausesValidation="False" CommandName="Insert"
                        ImageUrl="~/Imagenes/Icons/Add.png" Text="Insert"
                        OnClick="imgBtnInsertarIntegrante_Click" ToolTip="Insertar" />
                    <asp:ImageButton ID="imgBtnCancelConsul" runat="server" CausesValidation="False" CommandName="Insert"
                        ImageUrl="~/Imagenes/Icons/cancel.png" Text="Insert"
                        OnClick="imgbtnCancelConsul_Click" ToolTip="Insertar" />
                </td>
            </tr>
        </table>
        <br />
    <div id="divDatosUsuarios" runat="server" visible="false" class="ColumnStyle">
        <table align="center">
         <tr class="visible">
             <td align="left" bgcolor="#BBBBBB">
                 <asp:Label ID="NombreIntegrante" runat="server" Text="Nombre:" CssClass="Apariencia" Width="120px" align="center"></asp:Label>
             </td>
             <td>
                 <asp:TextBox ID="StrNombreIntegrante" runat="server" Width="300px" CssClass="Apariencia"></asp:TextBox>
             </td>
         </tr>
         <tr class="visible">
              <td align="left" bgcolor="#BBBBBB">
                  <asp:Label ID="Usuario" runat="server" Text="Usuario:" CssClass="Apariencia" Width="120px" align="center"></asp:Label>
              </td>
              <td>
                  <asp:TextBox ID="StrUsuarioIntegrante" runat="server" Width="300px" CssClass="Apariencia"></asp:TextBox>
              </td>
          </tr>
         <tr class="visible">
             <td align="left" bgcolor="#BBBBBB">
                 <asp:Label ID="Correo" runat="server" Text="Correo:" CssClass="Apariencia" Width="120px" align="center"></asp:Label>
             </td>
             <td>
                 <asp:TextBox ID="StrCorreoIntegrante" runat="server" Width="300px" CssClass="Apariencia"></asp:TextBox>
             </td>
         </tr>
         </table>
    </div>
        <br />
        <table align="center">
            <tr>
                <td class="auto-style1">
                    <asp:ImageButton ID="btnRegIntegrante" runat="server" CausesValidation="False" CommandName="Guardar"
                        ImageUrl="~/Imagenes/Icons/guardar.png" Text="Guardar"
                        OnClick="imgbtnRegIntegrante_Click" ToolTip="Guardar" />
                    <asp:ImageButton ID="btnUpdIntegrante" runat="server" CausesValidation="False" CommandName="Update"
                        ImageUrl="~/Imagenes/Icons/guardar.png" Text="Insert"
                        OnClick="imgbtnUpdIntegrante_Click" ToolTip="Update" />
                    <asp:ImageButton ID="btnNoRegIntegrante" runat="server" CausesValidation="False" CommandName="Cancelar"
                        ImageUrl="~/Imagenes/Icons/cancel.png" Text="Cancelar"
                        OnClick="imgbtnNoRegIntegrante_Click" ToolTip="Cancelar" />
                </td>
            </tr>
        </table>





        <asp:ModalPopupExtender ID="mpeMsgBox" runat="server" TargetControlID="btndummy"
            PopupControlID="pnlMsgBox" OkControlID="btnAceptar" BackgroundCssClass="FondoAplicacion"
            Enabled="True" DropShadow="true">
        </asp:ModalPopupExtender>
        <asp:Button ID="btndummy" runat="server" Text="Button" Style="display: none" />
        <asp:Panel ID="pnlMsgBox" runat="server" Width="400px" Style="display: none;" BorderColor="#575757"
            BackColor="#FFFFFF" BorderStyle="Solid">
            <table width="100%">
                <tr class="topHandle" style="background-color: #5D7B9D">
                    <td colspan="2" align="center" runat="server" id="tdCaption">&nbsp;
                        <asp:Label ID="lblCaption" runat="server" Text="Atención" Font-Names="Calibri" Font-Size="Small"></asp:Label><br />
                    </td>
                </tr>
                <tr>
                    <td style="width: 60px" valign="middle" align="center">
                        <asp:Image ID="imgInfo" runat="server" ImageUrl="~/Imagenes/Icons/icontexto-webdev-about.png" />
                    </td>
                    <td valign="middle" align="left">
                        <asp:Label ID="lblMsgBox" runat="server" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="right" colspan="2">
                        <asp:Button ID="btnAceptar" runat="server" Text="Ok" Font-Names="Calibri" Font-Size="Small" />
                    </td>
                       <td>
                         <asp:TextBox ID="TexBoxTemp" runat="server" Width="300px" CssClass="Apariencia" Visible="false"></asp:TextBox>
                     </td>
                </tr>
            </table>
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>
